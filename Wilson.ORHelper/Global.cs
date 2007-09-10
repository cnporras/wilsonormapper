//**************************************************************//
// Paul Wilson -- www.WilsonDotNet.com -- Paul@WilsonDotNet.com //
// Feel free to use and modify -- just leave these credit lines //
// I also always appreciate any other public credit you provide //
//**************************************************************//
// Includes Relationships for Mappings and Entities and Default Null Values by
// David D'Amico (ORMapper@datasolinc.com) and Craig Tucker (ORMapper@hrzdata.com)
// Thanks to Sam Smoot for the ability to create Mappings for just one Entity
// Thanks to Mark Kamoski (MKamoski@WebLogicArts.com - http://www.WebLogicArts.com)
//   for options for Data Types, LazyLoad False, DateTime Stamps, Escape Keywords
using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace Wilson.ORHelper
{
	public class Global
	{
		public const string defaultDateTimeFormatString = @"yyyy-MM-dd HH:mm:ss.fff";
		private static Hashtable schemaInfo = new Hashtable();

		/// <summary>
		/// This array is used for the keywords test.
		/// </summary>
		/// <remarks>
		/// This contains the unique set of all keywords from VB and CS, captialized for ease of use.
		/// </remarks>
		private readonly static string[] keywordsArray = new string[] { 
			"ANSI",	"ABSTRACT", "ADDHANDLER",	"ADDRESSOF", "ALIAS",	"AND", "ANDALSO",	"AS",	"ASSEMBLY",	"AUTO",
			"BASE",	"BOOL",	"BOOLEAN", "BREAK",	"BYREF", "BYTE", "BYVAL",
			"CALL",	"CASE",	"CATCH", "CBOOL",	"CBYTE", "CCHAR",	"CDATE", "CDBL", "CDEC", "CHAR", "CHECKED",
			"CINT", "CLASS",	"CLNG", "COBJ",	"CONST", "CONTINUE", "CSHORT", "CSNG", "CSTR", "CTYPE",
			"DATE",	"DECIMAL", "DECLARE",	"DEFAULT", "DELEGATE", "DELEGATE", "DIM",	"DIRECTCAST",	"DO",	"DOUBLE",
			"EACH",	"ELSE",	"ELSEIF",	"END", "ENUM", "ERASE",	"ERROR", "EVENT",	"EXIT",	"EXPLICIT",	"EXTERN",	"EXTERNALSOURCE",
			"FALSE", "FINALLY",	"FIXED", "FLOAT",	"FOR", "FOREACH",	"FRIEND",	"FUNCTION",
			"GET", "GETTYPE",	"GOSUB", "GOTO", "HANDLES",
			"IF",	"IMPLEMENTS",	"IMPLICIT",	"IMPORTS", "IN", "INHERITS", "INT",	"INTEGER", "INTERFACE",	"INTERNAL",	"IS",
			"LET", "LIB",	"LIKE",	"LOCK",	"LONG",	"LOOP",
			"ME",	"MOD","MODULE",	"MUSTINHERIT", "MUSTOVERRIDE", "MYBASE", "MYCLASS",
			"NAMESPACE", "NEW",	"NEXT",	"NOT", "NOTHING",	"NOTINHERITABLE",	"NOTOVERRIDABLE",	"NULL",
			"OBJECT",	"ON",	"OPERATOR", "OPTION",	"OPTIONAL",	"OR",	"ORELSE",
			"OUT", "OVERLOADS",	"OVERRIDABLE", "OVERRIDE", "OVERRIDES",
			"PARAMARRAY",	"PARAMS",	"PRESERVE",	"PRIVATE", "PROPERTY", "PROTECTED",	"PUBLIC",
			"RAISEEVENT",	"READONLY",	"REDIM", "REF",	"REGION",	"REM", "REMOVEHANDLER",	"RESUME",	"RETURN",
			"SBYTE", "SEALED", "SELECT", "SET",	"SHADOWS", "SHARED", "SHORT",	"SINGLE",	"SIZEOF",	"STACKALLOC",
			"STATIC",	"STEP",	"STOP",	"STRING",	"STRUCT",	"STRUCTURE", "SUB", "SWITCH",	"SYNCLOCK",
			"THEN",	"THIS",	"THROW", "TO", "TRUE", "TRY",	"TYPEOF",
			"UINT",	"ULONG", "UNCHECKED",	"UNICODE", "UNSAFE", "UNTIL",	"USHORT",	"USING",
			"VARIANT", "VIRTUAL",	"VOID",	"VOLATILE",
			"WHEN",	"WHILE", "WITH", "WITHEVENTS", "WRITEONLY",	"XOR"
		};

		[STAThread]
		static void Main() {
			Application.Run(new MainForm());
		}
	
		public static DataTable GetSchema(string connectString) {
			Global.schemaInfo.Clear();
			DataTable schema = new DataTable("Schema");
			DataColumn tableColumn = schema.Columns.Add("Table");
			tableColumn.ReadOnly = true;
			DataColumn fieldColumn = schema.Columns.Add("Field");
			fieldColumn.ReadOnly = true;
			DataColumn objectColumn = schema.Columns.Add("Object");
			objectColumn.ReadOnly = false;

			DataColumn myOLEDBTypeColumn = schema.Columns.Add("OLEDBType");
			myOLEDBTypeColumn.ReadOnly = true;
			DataColumn myCSTypeColumn = schema.Columns.Add("CSType");
			myCSTypeColumn.ReadOnly = true;
			DataColumn myVBTypeColumn = schema.Columns.Add("VBType");
			myVBTypeColumn.ReadOnly = true;

			OleDbConnection connection = null;
			try {
				connection = new OleDbConnection(connectString);
				connection.Open();
				for (int index = 0; index < 2; index++) {
					string type = (index == 0 ? "TABLE" : "VIEW");
					DataTable tables = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
						new object[] {null, null, null, type});
					foreach (DataRow table in tables.Rows) {
						string tableName = Global.Clean(table[2].ToString());
						string keyField = Global.GetKeyField(connectString, tableName);
						string className = Global.GetObjectName(tableName);

						schema.Rows.Add(new object[] {tableName, String.Empty, className, string.Empty, string.Empty, string.Empty});

						Global.schemaInfo.Add(tableName,keyField);
						DataView columns = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns,
							new object[] {null, null, tableName}).DefaultView;

						columns.Sort = "ORDINAL_POSITION";
						foreach (DataRowView column in columns) {
							string fieldName = Global.Clean(column[3].ToString());
							int fieldType = int.Parse(Global.Clean(column[11].ToString()));
							string propertyName = GetPropertyName(tableName, fieldName);

							OleDbType myFieldType = (OleDbType) fieldType;
							string myOLEDBType = myFieldType.ToString();
							string myCSType = Global.CSharpType(myFieldType);
							string myVBType = Global.VBNetType(myFieldType);

							schema.Rows.Add(new object[] {String.Empty, fieldName, propertyName, myOLEDBType, myCSType, myVBType});
							Global.schemaInfo.Add(tableName + "." + fieldName, fieldType);
							Global.schemaInfo.Add("null:" + tableName + "." + fieldName, 
								(bool)column[10]);
						}
					}
				}
			}
			finally {
				connection.Close();
			}

			return schema;
		}

		public static string GetPropertyName(string tableName, string fieldName) {
			string className = Global.GetObjectName(tableName);
			string propertyName = Global.GetObjectName(fieldName);
			if (propertyName.Length > tableName.Length && propertyName.StartsWith(tableName)) {
				propertyName = propertyName.Remove(0,tableName.Length);
			}
			else if (propertyName.Length > className.Length && propertyName.StartsWith(className)) {
				propertyName = propertyName.Remove(0,className.Length);
			}
			try {
				propertyName = propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1);
			}
			catch {
				// Nothing
			}
			return propertyName;
		}

		/// <summary>
		/// Replace the primary key field name with the primary table name in the foriegn key field name.
		/// </summary>
		public static string GetForiegnKeyObjectName(string fieldName, string pkTableName, string pkFieldName) {
			string fkPropertyName = Global.GetObjectName(fieldName);
			string pkClassName = Global.GetObjectName(pkTableName);
			string pkPropertyName = Global.GetObjectName(pkFieldName);
			
			fkPropertyName = fkPropertyName.Replace(pkPropertyName, pkClassName);

			try {
				fkPropertyName = fkPropertyName.Substring(0, 1).ToUpper() + fkPropertyName.Substring(1);
			}
			catch {
				// Nothing
			}
			return fkPropertyName;
		}

		private static string Tab(int tabs)
		{
			string tab = null;
			for(int x = 0; x < tabs; x++)
				tab += "\t";

			return tab;
		}

		public static string GetMappings(DataTable schemaTable, string nameSpace, string prefix, string connectString, bool showRelationships,
			string registryAppKey, bool includeRootElements, bool includeDataTypeInfo, bool isLazyLoad, bool includeDateTimeStamp)
		{
			bool isFirst = true;
			string keyField = null;
			string keyMember = null;
			string relationships = String.Empty;
			string nullable = String.Empty;
			OleDbType fieldType;
			string tableName = String.Empty;
			AssemblyName assemblyname = Assembly.GetCallingAssembly().GetName();

			int tabs = 0;

			StringBuilder mappings = new StringBuilder();
			if(includeRootElements)
			{
				mappings.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");

				if (includeDateTimeStamp)
				{
					mappings.Append(String.Format("<mappings version=\"{0}.{1}\" generated=\"{2}\">\r\n",
						assemblyname.Version.Major.ToString(), assemblyname.Version.Minor.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
				}
				else
				{
					mappings.Append(String.Format("<mappings version=\"{0}.{1}\">\r\n",
						assemblyname.Version.Major.ToString(), assemblyname.Version.Minor.ToString()));
				}

				tabs++;
			}

			foreach (DataRow row in schemaTable.Rows) 
			{
				string fieldName = row[1].ToString();
				string objectName = row[2].ToString();

				string myOLEDBType = string.Empty;
				string myCSType = string.Empty;
				string myVBtype = string.Empty;

				if (includeDataTypeInfo) 
				{
					myOLEDBType = row[3].ToString();
					myCSType = row[4].ToString();
					myVBtype = row[5].ToString();
				}
				else
				{
					//Continue.
				}

				string keyType;
				if (fieldName.Length == 0) {
					tableName = row[0].ToString();
					if (isFirst) {
						isFirst = false;
					}
					else {
						if (showRelationships && relationships.Length > 0) {
							mappings.Append (relationships);
							relationships = String.Empty;
						}
						tabs--;
						mappings.Append(String.Format("{0}</entity>\r\n", Tab(tabs)));
					}
					string typeName = nameSpace + "." + objectName;
					keyField = (string) Global.schemaInfo[tableName];
					keyMember = "<$" + keyField + "$>";
					if (keyField != null && keyField.Length > 0) {
						// Includes Guid Key Identification by Paul Hatcher (http://www.grassoc.co.uk)
						try {
							fieldType = (OleDbType) Global.schemaInfo[tableName + "." + keyField];
							keyType = (fieldType == OleDbType.Guid ? "Guid" : "Auto");
						}
						catch {
							keyType = "User";
						}
					}
					else {
						keyType = "None";
						keyMember = "";
					}
					mappings.Append(String.Format(
						"{0}<entity type=\"{1}\" table=\"{2}\" keyMember=\"{3}\" keyType=\"{4}\">\r\n",
						Tab(tabs), typeName, tableName, keyMember, keyType));
					tabs++;

					// Get relationships for the Table
					if (showRelationships) {
						relationships = GetRelationships(connectString, tableName, nameSpace, prefix, tabs, isLazyLoad);
					}
				}
				else {
					string memberName = Global.GetMemberName(objectName, prefix);
					if (keyField == fieldName && keyMember.Length > 0) {
						mappings.Replace(keyMember, memberName);
					}

					if (GetNullable(tableName, fieldName)) {
						fieldType = (OleDbType) Global.schemaInfo[tableName + "." + fieldName];
						nullable = GetNullValue(fieldType, registryAppKey);
					}
					else {
						nullable = string.Empty;
					}

					if (includeDataTypeInfo)
					{
						mappings.Append(String.Format(
							"{0}<attribute member=\"{1}\" field=\"{2}\" {3}alias=\"{4}\" oledbtype=\"{5}\" cstype=\"{6}\" vbtype=\"{7}\" />\r\n",
							Tab(tabs), memberName, fieldName, nullable, objectName, myOLEDBType, myCSType, myVBtype));
					}
					else
					{
						mappings.Append(String.Format(
							"{0}<attribute member=\"{1}\" field=\"{2}\" {3}alias=\"{4}\" />\r\n",
							Tab(tabs), memberName, fieldName, nullable, objectName));
					}
				}
			}
			if (showRelationships && relationships.Length > 0) {
				mappings.Append (relationships);
				relationships = String.Empty;
			}

			tabs--;
			mappings.Append(String.Format("{0}</entity>\r\n", Tab(tabs)));
			
			if(includeRootElements)
			{
				tabs--;
				mappings.Append(String.Format("{0}</mappings>\r\n", Tab(tabs)));
			}
			
			return mappings.ToString();
		}
		
		public static string GetObjectSpace(string nameSpace, bool isCSharp, string connectionString, string providerType, bool includeDateTimeStamp) {
			StringBuilder sb = new StringBuilder();

			if (isCSharp) {
				if (includeDateTimeStamp) 
				{
					sb.Append("//" + "This code was generated by a tool as of " + DateTime.Now.ToString(defaultDateTimeFormatString) + "." + "\r\n" + "\r\n");
				}
				else
				{
					//Continue.
				}

				sb.Append("using System;\r\n");
				sb.Append("using Wilson.ORMapper;\r\n");
				sb.Append("\r\n");
				sb.Append("namespace " + nameSpace.Substring(0, nameSpace.LastIndexOf(".")) + ".Data\r\n");
				sb.Append("{\r\n");
				sb.Append("\tsealed public class Manager\r\n");
				sb.Append("\t{\r\n");
				sb.Append("\t\tprivate static ObjectSpace engine;\r\n");
				sb.Append("\r\n");
				sb.Append("\t\tpublic static ObjectSpace Engine {\r\n");
				sb.Append("\t\t\tget { return Manager.engine; }\r\n");
				sb.Append("\t\t}\r\n");
				sb.Append("\r\n");
				sb.Append("\t\tstatic Manager() {\r\n");
				sb.Append("\t\t\tstring mappingFile = AppDomain.CurrentDomain.BaseDirectory\r\n");
				sb.Append("\t\t\t\t+ \"Data\\\\Mappings.config\";\r\n");
				sb.Append("\t\t\tstring connectString = \"" + connectionString + "\";\r\n");
				sb.Append("\t\t\tstring providerType = \"" + providerType + "\";\r\n");
				sb.Append("\r\n");
				sb.Append("\t\t\tProvider provider;\r\n");
				sb.Append("\t\t\ttry { provider = (Provider) System.Enum.Parse(typeof(Provider), providerType, true); }\r\n");
				sb.Append("\t\t\tcatch { provider = Provider.MsSql; }\r\n");
				sb.Append("\r\n");
				sb.Append("\t\t\t// Note: Non-Zero Session may be desirable for Server Applications\r\n");
				sb.Append("\t\t\tengine = new ObjectSpace(mappingFile, connectString, provider);\r\n");
				sb.Append("\t\t}\r\n");
				sb.Append("\r\n");
				sb.Append("\t\tprivate Manager() {\r\n");
				sb.Append("\t\t\t// Note: Static Class -- All Members are Static\r\n");
				sb.Append("\t\t}\r\n");
				sb.Append("\t}\r\n");
				sb.Append("}");
			}
			else {
				if (includeDateTimeStamp)
				{
					sb.Append("'" + "This code was generated by a tool as of " + DateTime.Now.ToString(defaultDateTimeFormatString) + "." + "\r\n" + "\r\n");
				}
				else
				{
					//Continue.
				}

				sb.Append("Option Explicit On\r\n");
				sb.Append("Option Strict On\r\n");
				sb.Append("\r\nImports System\r\n");
				sb.Append("Imports Wilson.ORMapper\r\n");
				sb.Append("\r\n");
				sb.Append("Namespace " + nameSpace.Substring(0, nameSpace.LastIndexOf(".")) + ".Data\r\n");
				sb.Append("\tNotInheritable Public Class Manager\r\n");
				sb.Append("\t\tPrivate Shared _engine As ObjectSpace\r\n");
				sb.Append("\r\n");
				sb.Append("\t\tPublic Shared ReadOnly Property Engine() As ObjectSpace\r\n");
				sb.Append("\t\t\tGet\r\n");
				sb.Append("\t\t\t\tReturn Manager._engine\r\n");
				sb.Append("\t\t\tEnd Get\r\n");
				sb.Append("\t\tEnd Property\r\n");
				sb.Append("\r\n");
				sb.Append("\t\tShared Sub New()\r\n");
				sb.Append("\t\t\tDim _mappingFile As String = AppDomain.CurrentDomain.BaseDirectory _\r\n");
				sb.Append("\t\t\t\t+ \"Data\\Mappings.config\"\r\n");
				sb.Append("\t\t\tDim _connectString As String = \"" + connectionString + "\"\r\n");
				sb.Append("\t\t\tDim _providerType As String = \"" + providerType + "\"\r\n");
				sb.Append("\r\n");
				sb.Append("\t\t\tDim _provider As Provider\r\n");
				sb.Append("\t\t\tTry\r\n");
				sb.Append("\t\t\t\t_provider = DirectCast(System.Enum.Parse(GetType(Provider), _providerType, True), Provider)\r\n");
				sb.Append("\t\t\tCatch\r\n");
				sb.Append("\t\t\t\t_provider = Provider.MsSql\r\n");
				sb.Append("\t\t\tEnd Try\r\n");
				sb.Append("\r\n");
				sb.Append("\t\t\t' Note: Non-Zero Session may be desirable for Server Applications\r\n");
				sb.Append("\t\t\t_engine = New ObjectSpace(_mappingFile, _connectString, _provider)\r\n");
				sb.Append("\t\tEnd Sub\r\n");
				sb.Append("\r\n");
				sb.Append("\t\tPrivate Sub New()\r\n");
				sb.Append("\t\t\t' Note: Shared Class -- All Members are Shared\r\n");
				sb.Append("\t\tEnd Sub\r\n");
				sb.Append("\tEnd Class\r\n");
				sb.Append("End Namespace");
			}
			return sb.ToString();
		}

		public static string GetClassCode(DataTable schemaTable, string nameSpace, string prefix, bool isCSharp, int row, bool helper,
			bool events, bool relationships, string connectionString, string dateTimeValue, bool includeDateTimeStamp, bool escapeKeywords, bool isLazyLoad)
		{
			if (isCSharp) {
				return Global.CSharpCode(schemaTable, nameSpace, prefix, row, helper, events, relationships, connectionString, dateTimeValue, includeDateTimeStamp, escapeKeywords, isLazyLoad);
			}
			else {
				return Global.VBNetCode(schemaTable, nameSpace, prefix, row, helper, events, relationships, connectionString, dateTimeValue, includeDateTimeStamp, escapeKeywords, isLazyLoad);
			}
		}
		
		private static string CSharpCode(DataTable schemaTable, string nameSpace, string prefix, int row, bool helper, bool events,
				bool relationships, string connectionString, string dateTimeValue, bool includeDateTimeStamp, bool escapeKeywords, bool isLazyLoad)
		{
			string tableName = schemaTable.Rows[row][0].ToString();
			string className = schemaTable.Rows[row][2].ToString();
			string keyField = (string) Global.schemaInfo[tableName];

			// Hold strings for relationships
			string relVarDeclaration = String.Empty;
			string relProps = String.Empty;
			string relObjHelper1 = String.Empty;
			string relObjHelper2 = String.Empty;
			
			// Create relationship info
			if (relationships) {
				GetCSharpRelations(connectionString, tableName, nameSpace, prefix,
					ref relVarDeclaration, ref relProps, ref relObjHelper1, ref relObjHelper2, isLazyLoad);
			}
			
			StringBuilder classCode = new StringBuilder();
			if (includeDateTimeStamp)
			{
				classCode.Append("//" + "This code was generated by a tool as of " + dateTimeValue + "." + "\r\n" + "\r\n");
			}
			else
			{
				//Continue.
			}

			classCode.Append("using System;\r\n");
			if (relationships && relVarDeclaration.Length > 0) {
				classCode.Append("using System.Collections;\r\n");
				classCode.Append("using Wilson.ORMapper;\r\n");
			}
			else if (helper || events) {
				classCode.Append("using Wilson.ORMapper;\r\n");
			}

			classCode.Append("\r\nnamespace " + nameSpace + "\r\n{\r\n");
			string interfaces = "";
			if (helper || events) {		
				if (helper && events) {
					interfaces = " : IObjectHelper, IObjectNotification";
				}	
				else if (helper) {
					interfaces = " : IObjectHelper";
				}
				else if (events) {
					interfaces = " : IObjectNotification";
				}
			}
			
			if (escapeKeywords)
			{
				classCode.Append("\tpublic class " + EscapeKeyword(className, true) + interfaces + "\r\n\t{\r\n");
			}
			else
			{
				classCode.Append("\tpublic class " + className + interfaces + "\r\n\t{\r\n");
			}

			int fieldRow = row + 1;
			while (fieldRow < schemaTable.Rows.Count && schemaTable.Rows[fieldRow][0].ToString().Length == 0) {
				string fieldName = schemaTable.Rows[fieldRow][1].ToString();
				string objectName = schemaTable.Rows[fieldRow][2].ToString();
				string memberName = Global.GetMemberName(objectName, prefix);
				OleDbType fieldType = (OleDbType) Global.schemaInfo[tableName + "." + fieldName];

				if (escapeKeywords)
				{
					classCode.Append(String.Format("\t\tprivate {0} {1};\r\n",
						Global.CSharpType(fieldType), EscapeKeyword(memberName, true)));
				}
				else
				{
					classCode.Append(String.Format("\t\tprivate {0} {1};\r\n",
						Global.CSharpType(fieldType), memberName));
				}
				
				fieldRow++;
			}
			if (relationships) {
				classCode.Append(relVarDeclaration);
			}

			fieldRow = row + 1;
			while (fieldRow < schemaTable.Rows.Count && schemaTable.Rows[fieldRow][0].ToString().Length == 0) {
				string fieldName = schemaTable.Rows[fieldRow][1].ToString();
				string objectName = schemaTable.Rows[fieldRow][2].ToString();
				string memberName = Global.GetMemberName(objectName, prefix);
				OleDbType fieldType = (OleDbType) Global.schemaInfo[tableName + "." + fieldName];
				classCode.Append(String.Format("\r\n\t\tpublic {0} {1}\r\n",
					Global.CSharpType(fieldType), objectName) + "\t\t{\r\n");
				classCode.Append("\t\t\tget { return this." + memberName + "; }\r\n");
				if (fieldName != keyField) {
					classCode.Append("\t\t\tset { this." + memberName + " = value; }\r\n");
				}
				classCode.Append("\t\t}\r\n");
				fieldRow++;
			}
			if (relationships) {
				classCode.Append("\r\n" + relProps);
			}

			if (helper) {
				classCode.Append("\r\n\t\t#region IObjectHelper Members\r\n");
				classCode.Append("\t\tpublic object this[string memberName]\r\n\t\t{\r\n");
				classCode.Append("\t\t\tget {\r\n");
				classCode.Append("\t\t\t\tswitch (memberName) {\r\n");
				fieldRow = row + 1;
				while (fieldRow < schemaTable.Rows.Count && schemaTable.Rows[fieldRow][0].ToString().Length == 0) {
					string objectName = schemaTable.Rows[fieldRow][2].ToString();
					string memberName = Global.GetMemberName(objectName, prefix);
					classCode.Append("\t\t\t\t\tcase \"" + memberName
						+ "\": return this." + memberName + ";\r\n");
					fieldRow++;
				}
				if (relationships) {
					classCode.Append(relObjHelper1);
				}

				classCode.Append("\t\t\t\t\tdefault: throw new Exception(\"Invalid Member\");\r\n");
				classCode.Append("\t\t\t\t}\r\n");
				classCode.Append("\t\t\t}\r\n");
				classCode.Append("\t\t\tset {\r\n");
				classCode.Append("\t\t\t\tswitch (memberName) {\r\n");
				fieldRow = row + 1;
				while (fieldRow < schemaTable.Rows.Count && schemaTable.Rows[fieldRow][0].ToString().Length == 0) {
					string fieldName = schemaTable.Rows[fieldRow][1].ToString();
					string objectName = schemaTable.Rows[fieldRow][2].ToString();
					string memberName = Global.GetMemberName(objectName, prefix);
					OleDbType fieldType = (OleDbType) Global.schemaInfo[tableName + "." + fieldName];
					classCode.Append("\t\t\t\t\tcase \"" + memberName
						+ "\": this." + memberName + " = (" + Global.CSharpType(fieldType)
						+ ") value; break;\r\n");
					fieldRow++;
				}
				if (relationships) {
					classCode.Append(relObjHelper2);
				}
				classCode.Append("\t\t\t\t\tdefault: throw new Exception(\"Invalid Member\");\r\n");
				classCode.Append("\t\t\t\t}\r\n");
				classCode.Append("\t\t\t}\r\n");
				classCode.Append("\t\t}\r\n");
				classCode.Append("\t\t#endregion\r\n");
			}

			if (events) {
				classCode.Append("\r\n\t\t#region IObjectNotification Members\r\n");
				classCode.Append("\t\tpublic void OnCreated(Transaction transaction)\r\n\t\t{\r\n");
				classCode.Append("\t\t\t// TODO\r\n\t\t}\r\n\r\n");
				classCode.Append("\t\tpublic void OnCreating(Transaction transaction)\r\n\t\t{\r\n");
				classCode.Append("\t\t\t// TODO\r\n\t\t}\r\n\r\n");
				classCode.Append("\t\tpublic void OnDeleted(Transaction transaction)\r\n\t\t{\r\n");
				classCode.Append("\t\t\t// TODO\r\n\t\t}\r\n\r\n");
				classCode.Append("\t\tpublic void OnDeleting(Transaction transaction)\r\n\t\t{\r\n");
				classCode.Append("\t\t\t// TODO\r\n\t\t}\r\n\r\n");
				classCode.Append("\t\tpublic void OnMaterialized(System.Data.IDataRecord dataRecord)\r\n\t\t{\r\n");
				classCode.Append("\t\t\t// TODO\r\n\t\t}\r\n\r\n");
				classCode.Append("\t\tpublic void OnPersistError(Transaction transaction, Exception exception)\r\n\t\t{\r\n");
				classCode.Append("\t\t\t// TODO\r\n\t\t}\r\n\r\n");
				classCode.Append("\t\tpublic void OnUpdated(Transaction transaction)\r\n\t\t{\r\n");
				classCode.Append("\t\t\t// TODO\r\n\t\t}\r\n\r\n");
				classCode.Append("\t\tpublic void OnUpdating(Transaction transaction)\r\n\t\t{\r\n");
				classCode.Append("\t\t\t// TODO\r\n\t\t}\r\n");
				classCode.Append("\t\t#endregion\r\n");
			}

			classCode.Append("\t}\r\n");
			classCode.Append("}\r\n");
			return classCode.ToString();
		}

		private static string VBNetCode(DataTable schemaTable, string nameSpace, string prefix, int row, bool helper, bool events,
				bool relationships, string connectionString, string dateTimeValue, bool includeDateTimeStamp, bool escapeKeywords, bool isLazyLoad)
		{
			string tableName = schemaTable.Rows[row][0].ToString();
			string className = schemaTable.Rows[row][2].ToString();
			string keyField = (string) Global.schemaInfo[tableName];

			// Hold strings for relationships
			string relVarDeclaration = String.Empty;
			string relProps = String.Empty;
			string relObjHelper1 = String.Empty;
			string relObjHelper2 = String.Empty;
			
			// Create relationship info
			if (relationships) {
				GetVBNetRelations(connectionString, tableName, nameSpace, prefix,
					ref relVarDeclaration, ref relProps, ref relObjHelper1, ref relObjHelper2, isLazyLoad);
			}

			StringBuilder classCode = new StringBuilder();
			if (includeDateTimeStamp)
			{
				classCode.Append("'This code was generated by a tool as of " + dateTimeValue + "." + "\r\n" + "\r\n");
			}
			else
			{
				//Continue.
			}

			classCode.Append("Option Explicit On\r\n");
			classCode.Append("Option Strict On\r\n");
			classCode.Append("\r\nImports System\r\n");
			if (relationships && relVarDeclaration.Length > 0) {
				classCode.Append("Imports System.Collections\r\n");
				classCode.Append("Imports Wilson.ORMapper\r\n");
			}
			else if (helper || events) {
				classCode.Append("Imports Wilson.ORMapper\r\n");
			}

			classCode.Append("\r\nNamespace " + nameSpace + "\r\n");

			if (escapeKeywords)
			{
				classCode.Append("\tPublic Class " + EscapeKeyword(className, false) + "\r\n");
			}
			else
			{
				classCode.Append("\tPublic Class " + className + "\r\n");
			}
			
			if (helper || events) 
			{		
				if (helper && events) {
					classCode.Append("\t\tImplements IObjectHelper, IObjectNotification\r\n\r\n");
				}	
				else if (helper) {
					classCode.Append("\t\tImplements IObjectHelper\r\n\r\n");
				}
				else if (events) {
					classCode.Append("\t\tImplements IObjectNotification\r\n\r\n");
				}
			}
			
			int fieldRow = row + 1;
			while (fieldRow < schemaTable.Rows.Count && schemaTable.Rows[fieldRow][0].ToString().Length == 0) {
				string fieldName = schemaTable.Rows[fieldRow][1].ToString();
				string objectName = schemaTable.Rows[fieldRow][2].ToString();
				string memberName = Global.GetMemberName(objectName, prefix);
				OleDbType fieldType = (OleDbType) Global.schemaInfo[tableName + "." + fieldName];
				
				if (escapeKeywords)
				{
					classCode.Append(String.Format("\t\tPrivate {0} As {1}\r\n",
						EscapeKeyword(memberName, false), Global.VBNetType(fieldType)));
				}
				else
				{
					classCode.Append(String.Format("\t\tPrivate {0} As {1}\r\n",
						memberName, Global.VBNetType(fieldType)));
				}

				fieldRow++;
			}
			if (relationships) {
				classCode.Append(relVarDeclaration);
			}
			
			fieldRow = row + 1;
			while (fieldRow < schemaTable.Rows.Count && schemaTable.Rows[fieldRow][0].ToString().Length == 0) {
				string fieldName = schemaTable.Rows[fieldRow][1].ToString();
				string objectName = schemaTable.Rows[fieldRow][2].ToString();
				string memberName = Global.GetMemberName(objectName, prefix);
				OleDbType fieldType = (OleDbType) Global.schemaInfo[tableName + "." + fieldName];

				if (escapeKeywords)
				{
					classCode.Append(String.Format("\r\n\t\tPublic {0}Property {1}() As {2}\r\n",
						(fieldName == keyField ? "ReadOnly " : ""),	EscapeKeyword(objectName, false), Global.VBNetType(fieldType)));
				}
				else
				{
					classCode.Append(String.Format("\r\n\t\tPublic {0}Property {1}() As {2}\r\n",
						(fieldName == keyField ? "ReadOnly " : ""),	objectName, Global.VBNetType(fieldType)));
				}

				classCode.Append("\t\t\tGet\r\n\t\t\t\tReturn Me." + memberName + "\r\n\t\t\tEnd Get\r\n");
				if (fieldName != keyField) {
					classCode.Append("\t\t\tSet(ByVal value As " + Global.VBNetType(fieldType)
						+")\r\n\t\t\t\tMe." + memberName + " = value\r\n\t\t\tEnd Set\r\n");
				}
				classCode.Append("\t\tEnd Property\r\n");
				fieldRow++;
			}
			if (relationships) {
				classCode.Append("\r\n" + relProps);
			}

			if (helper) {
				classCode.Append("\r\n#Region \"IObjectHelper Members\"\r\n");
				classCode.Append("\t\tDefault Public Property Item(ByVal memberName As String) As Object Implements IObjectHelper.Item\r\n");
				classCode.Append("\t\t\tGet\r\n");
				classCode.Append("\t\t\t\tSelect Case memberName\r\n");
				fieldRow = row + 1;
				while (fieldRow < schemaTable.Rows.Count && schemaTable.Rows[fieldRow][0].ToString().Length == 0) {
					string objectName = schemaTable.Rows[fieldRow][2].ToString();
					string memberName = Global.GetMemberName(objectName, prefix);
					classCode.Append("\t\t\t\t\tCase \"" + memberName
						+ "\" : Return Me." + memberName + "\r\n");
					fieldRow++;
				}
				if (relationships) {
					classCode.Append(relObjHelper1);
				}

				classCode.Append("\t\t\t\t\tCase Else : Throw New Exception(\"Invalid Member\")\r\n");
				classCode.Append("\t\t\t\tEnd Select\r\n");
				classCode.Append("\t\t\tEnd Get\r\n");
				classCode.Append("\t\t\tSet(ByVal value As Object)\r\n");
				classCode.Append("\t\t\t\tSelect Case memberName\r\n");
				fieldRow = row + 1;
				while (fieldRow < schemaTable.Rows.Count && schemaTable.Rows[fieldRow][0].ToString().Length == 0) {
					string fieldName = schemaTable.Rows[fieldRow][1].ToString();
					string objectName = schemaTable.Rows[fieldRow][2].ToString();
					string memberName = Global.GetMemberName(objectName, prefix);
					OleDbType fieldType = (OleDbType) Global.schemaInfo[tableName + "." + fieldName];
					classCode.Append("\t\t\t\t\tCase \"" + memberName	+ "\" : Me."
						+ memberName + " = CType(value, " + Global.VBNetType(fieldType) + ")\r\n");
					fieldRow++;
				}
				if (relationships) {
					classCode.Append(relObjHelper2);
				}
				classCode.Append("\t\t\t\t\tCase Else : Throw New Exception(\"Invalid Member\")\r\n");
				classCode.Append("\t\t\t\tEnd Select\r\n");
				classCode.Append("\t\t\tEnd Set\r\n");
				classCode.Append("\t\tEnd Property\r\n");
				classCode.Append("#End Region\r\n");
			}

			if (events) {
				classCode.Append("\r\n#Region \"IObjectNotification Members\"\r\n");
				classCode.Append("\t\tPublic Sub OnCreated(ByVal _transaction As Transaction) Implements IObjectNotification.OnCreated\r\n");
				classCode.Append("\t\t\t' TODO\r\n\t\tEnd Sub\r\n\r\n");
				classCode.Append("\t\tPublic Sub OnCreating(ByVal _transaction As Transaction) Implements IObjectNotification.OnCreating\r\n");
				classCode.Append("\t\t\t' TODO\r\n\t\tEnd Sub\r\n\r\n");
				classCode.Append("\t\tPublic Sub OnDeleted(ByVal _transaction As Transaction) Implements IObjectNotification.OnDeleted\r\n");
				classCode.Append("\t\t\t' TODO\r\n\t\tEnd Sub\r\n\r\n");
				classCode.Append("\t\tPublic Sub OnDeleting(ByVal _transaction As Transaction) Implements IObjectNotification.OnDeleting\r\n");
				classCode.Append("\t\t\t' TODO\r\n\t\tEnd Sub\r\n\r\n");
				classCode.Append("\t\tPublic Sub OnMaterialized(ByVal _dataRecord As System.Data.IDataRecord) Implements IObjectNotification.OnMaterialized\r\n");
				classCode.Append("\t\t\t' TODO\r\n\t\tEnd Sub\r\n\r\n");
				classCode.Append("\t\tPublic Sub OnPersistError(ByVal _transaction As Transaction, ByVal _exception As Exception) Implements IObjectNotification.OnPersistError\r\n");
				classCode.Append("\t\t\t' TODO\r\n\t\tEnd Sub\r\n\r\n");
				classCode.Append("\t\tPublic Sub OnUpdated(ByVal _transaction As Transaction) Implements IObjectNotification.OnUpdated\r\n");
				classCode.Append("\t\t\t' TODO\r\n\t\tEnd Sub\r\n\r\n");
				classCode.Append("\t\tPublic Sub OnUpdating(ByVal _transaction As Transaction) Implements IObjectNotification.OnUpdating\r\n");
				classCode.Append("\t\t\t' TODO\r\n\t\tEnd Sub\r\n");
				classCode.Append("#End Region\r\n");
			}

			classCode.Append("\tEnd Class\r\n");
			classCode.Append("End Namespace\r\n");
			return classCode.ToString();
		}

		private static string GetKeyField(string connectString, string tableName) {
			OleDbConnection connection = null;
			try {
				connection = new OleDbConnection(connectString);
				connection.Open();
				DataTable keyFields = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys,
					new object[] {null, null, tableName});
				return Global.Clean(keyFields.Rows[0][3].ToString());
			}
			catch {
				return String.Empty;
			}
			finally {
				connection.Close();
			}
		}

		// Get Relationship info
		private static string GetRelationships(string connectString, string tableName, string nameSpace, string prefix, int tabs, bool isLazyLoad) {
			StringBuilder relations = new StringBuilder();
			OleDbConnection connection = null;
			connection = new OleDbConnection(connectString);

			try {
				connection.Open();
			}
			catch {
				return String.Empty;
			}

			// Many-To-One side
			try {
				DataTable keyFields = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys,
					new object[] {null,null,null,null,null,tableName});
					
				foreach (DataRow row in keyFields.Rows) {
					string pkTable = Global.GetMemberName(Global.GetObjectName(
						Global.Clean(row["PK_TABLE_NAME"].ToString())),String.Empty);
					string fkField = Global.Clean(row["FK_COLUMN_NAME"].ToString());
					string pkName = nameSpace + "." + Global.GetObjectName(pkTable);

					string alias = Global.GetForiegnKeyObjectName(Global.GetObjectName(
						GetPropertyName(Global.Clean(row["FK_TABLE_NAME"].ToString()), 
						Global.Clean(row["FK_COLUMN_NAME"].ToString()) + "_object")), 
						Global.Clean(row["PK_TABLE_NAME"].ToString()),
						Global.Clean(row["PK_COLUMN_NAME"].ToString()));
					string member = Global.GetMemberName(alias, prefix);
					
					relations.Append(String.Format(
						"{0}<relation relationship=\"ManyToOne\" member=\"{1}\" field=\"{2}\"\r\n"
						+ "{0}\t type=\"{3}\" alias=\"{4}\" lazyLoad=\"{5}\" />\r\n",
						Tab(tabs), member, fkField, pkName, alias, isLazyLoad.ToString().ToLower()));
				}
			}
			finally {}

			// One-To-Many side
			try {
				DataTable keyFields = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys,
					new object[] {null,null,tableName});
					
				foreach (DataRow row in keyFields.Rows) {
					string fkField = Global.Clean(row["FK_COLUMN_NAME"].ToString());
					string fkTable = Global.GetMemberName(Global.GetObjectName(
						Global.Clean(row["FK_TABLE_NAME"].ToString())),String.Empty);
					string fkName = nameSpace + "." + Global.GetObjectName(fkTable);

					string pkField = Global.GetObjectName(Global.Clean(row["PK_COLUMN_NAME"].ToString()));
					fkTable = Global.GetObjectName(Global.Clean(row["FK_TABLE_NAME"].ToString()))
						+ Global.GetPropertyName(Global.Clean(row["FK_TABLE_NAME"].ToString()),
						Global.Clean(row["FK_COLUMN_NAME"].ToString()));
					if (fkTable.Length > pkField.Length && fkTable.EndsWith(pkField))
						fkTable = fkTable.Substring(0,fkTable.Length - pkField.Length);
					string alias = Global.MakePlural(fkTable);
					string member = Global.GetMemberName(alias,prefix);

					relations.Append(String.Format(
						"{0}<relation relationship=\"OneToMany\" member=\"{1}\" field=\"{2}\"\r\n"
						+ "{0}\t type=\"{3}\" alias=\"{4}\" lazyLoad=\"{5}\" cascadeDelete=\"true\" />\r\n",
						Tab(tabs), member, fkField, fkName, alias, isLazyLoad.ToString().ToLower()));
				}
			}
			finally {}

			connection.Close();

			return relations.ToString();
		}

		/// <summary>
		/// Determines if a field can contain nulls.
		/// </summary>
		private static bool GetNullable(string tableName, string fieldName) {
			return (bool)Global.schemaInfo["null:" + tableName + "." + fieldName];
		}

		// Get Relationship info
		private static void GetCSharpRelations(string connectString, string tableName, string nameSpace, string prefix,
			ref string varDecs, ref string properties, ref string helper1, ref string helper2, bool isLazyLoad) 
		{
			StringBuilder relVarDeclaration = new StringBuilder();
			StringBuilder relProps = new StringBuilder();
			StringBuilder relObjHelper1 = new StringBuilder();
			StringBuilder relObjHelper2 = new StringBuilder();

			OleDbConnection connection = null;
			connection = new OleDbConnection(connectString);

			try {
				connection.Open();
			}
			catch {
				return;
			}

			// Many-To-One side
			try {
				DataTable keyFields = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys,
					new object[] {null,null,null,null,null,tableName});
					
				foreach (DataRow row in keyFields.Rows) {
					string fkField = Global.GetForiegnKeyObjectName(Global.GetObjectName(
						GetPropertyName(row["FK_TABLE_NAME"].ToString(), row["FK_COLUMN_NAME"].ToString() + "_object")), 
						row["PK_TABLE_NAME"].ToString(), row["PK_COLUMN_NAME"].ToString());
					string fkMember = Global.GetMemberName(fkField,prefix);
					string pkTable = Global.MakeSingular(Global.GetObjectName(row["PK_TABLE_NAME"].ToString()));
					string pkMember = Global.GetMemberName(pkTable,prefix);
					string pkField = GetPropertyName(row["PK_TABLE_NAME"].ToString(), row["PK_COLUMN_NAME"].ToString());
					string pkType = Global.GetObjectName(row["PK_TABLE_NAME"].ToString());

					if (isLazyLoad)
					{
						relVarDeclaration.Append(String.Format("\t\tprivate ObjectHolder {0};"
							+ " // Strongly Type as {1} if not Lazy-Loading\r\n", fkMember, pkTable));
					}
					else
					{
						relVarDeclaration.Append(String.Format("\t\tprivate " + pkTable + " {0};"
							+ " // Strongly Type as {1} if not Lazy-Loading\r\n", fkMember, pkTable));
					}
					
					relProps.Append("\t\t// Return the primary key property from the primary key object\r\n");
					relProps.Append(String.Format("\t\tpublic {0} {1}\r\n\t\t{{\r\n", pkType, fkField));

					if (isLazyLoad)
					{
						relProps.Append(String.Format("\t\t\tget {{ return ({0})this.{1}.InnerObject; }}\r\n", pkTable, fkMember));
					}
					else
					{
						relProps.Append(String.Format("\t\t\tget {{ return this.{1}; }}\r\n", pkTable, fkMember));
					}

					relProps.Append("\t\t}\r\n\r\n");

					relObjHelper1.Append(String.Format("\t\t\t\t\tcase \"{0}\": return this.{0};\r\n", fkMember));

					if (isLazyLoad)
					{
						relObjHelper2.Append(String.Format("\t\t\t\t\tcase \"{0}\": this.{0} = (ObjectHolder) value; break;\r\n", 
							fkMember));
					}
					else
					{
						relObjHelper2.Append(String.Format("\t\t\t\t\tcase \"{0}\": this.{0} = ({1}) value; break;\r\n", 
							fkMember, pkTable));
					}
				}
			}
			finally {}

			// One-To-Many side
			try {
				DataTable keyFields = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys,
					new object[] {null,null,tableName});
					
				foreach (DataRow row in keyFields.Rows) {
					string fkTable = Global.GetObjectName(row["FK_TABLE_NAME"].ToString())
						+ Global.GetPropertyName(row["FK_TABLE_NAME"].ToString(),row["FK_COLUMN_NAME"].ToString());
					string pkField = Global.GetObjectName(row["PK_COLUMN_NAME"].ToString());
					if (fkTable.Length > pkField.Length && fkTable.EndsWith(pkField))
						fkTable = fkTable.Substring(0,fkTable.Length - pkField.Length);
					fkTable = Global.MakePlural(fkTable);
					string member = Global.GetMemberName(fkTable,prefix);

					relVarDeclaration.Append(String.Format("\t\tprivate IList {0};"
						+ " // Supports both ObjectSet and Lazy-Loaded ObjectList\r\n", member));
					
					relProps.Append(String.Format("\t\tpublic IList {0}\r\n\t\t{{\r\n", fkTable));
					relProps.Append(String.Format("\t\t\tget {{ return this.{0}; }}\r\n", member));
					relProps.Append("\t\t}\r\n\r\n");

					relObjHelper1.Append(String.Format("\t\t\t\t\tcase \"{0}\": return this.{0};\r\n", member));
					relObjHelper2.Append(String.Format("\t\t\t\t\tcase \"{0}\": this.{0} = (IList) value; break;\r\n", member));
				}
			}
			finally {}

			connection.Close();

			varDecs = relVarDeclaration.ToString();
			properties = relProps.ToString();
			helper1 = relObjHelper1.ToString();
			helper2 = relObjHelper2.ToString();
		}

		private static void GetVBNetRelations(string connectString, string tableName, string nameSpace, string prefix,
			ref string varDecs, ref string properties, ref string helper1, ref string helper2, bool isLazyLoad)
		{
			StringBuilder relVarDeclaration = new StringBuilder();
			StringBuilder relProps = new StringBuilder();
			StringBuilder relObjHelper1 = new StringBuilder();
			StringBuilder relObjHelper2 = new StringBuilder();

			OleDbConnection connection = null;
			connection = new OleDbConnection(connectString);

			try {
				connection.Open();
			}
			catch {
				return;
			}

			// Many-To-One side
			try {
				DataTable keyFields = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys,
					new object[] {null,null,null,null,null,tableName});
					
				foreach (DataRow row in keyFields.Rows) {
					string fkField = Global.GetForiegnKeyObjectName(Global.GetObjectName(
						GetPropertyName(row["FK_TABLE_NAME"].ToString(), row["FK_COLUMN_NAME"].ToString() + "_object")), 
						row["PK_TABLE_NAME"].ToString(), row["PK_COLUMN_NAME"].ToString());
					string fkMember = Global.GetMemberName(fkField,prefix);
					string pkTable = Global.MakeSingular(Global.GetObjectName(row["PK_TABLE_NAME"].ToString()));
					string pkMember = Global.GetMemberName(pkTable,prefix);
					string pkField = GetPropertyName(row["PK_TABLE_NAME"].ToString(), row["PK_COLUMN_NAME"].ToString());
					string pkType = Global.GetObjectName(row["PK_TABLE_NAME"].ToString());

					if (isLazyLoad)
					{
						relVarDeclaration.Append(String.Format("\t\tPrivate {0} As ObjectHolder"
							+ " ' Strongly Type as {1} if not Lazy-Loading\r\n", fkMember, pkTable));
					}
					else
					{
						relVarDeclaration.Append(String.Format("\t\tPrivate {0} As " + pkTable
							+ " ' Strongly Type as {1} if not Lazy-Loading\r\n", fkMember, pkTable));
					}
					
					relProps.Append("\t\t' Return the primary key property from the primary key object\r\n");
					relProps.Append(String.Format("\t\tPublic ReadOnly Property {0}() As {1}\r\n", fkField, pkType));

					if (isLazyLoad)
					{
						relProps.Append(String.Format("\t\t\tGet\r\n\t\t\t\tReturn CType(Me.{1}.InnerObject, {0})\r\n", pkTable, fkMember));
					}
					else
					{
						relProps.Append(String.Format("\t\t\tGet\r\n\t\t\t\tReturn Me.{1}\r\n", pkTable, fkMember));
					}

					relProps.Append("\t\t\tEnd Get\r\n\t\tEnd Property\r\n\r\n");

					relObjHelper1.Append(String.Format("\t\t\t\t\tCase \"{0}\" : Return Me.{0}\r\n", fkMember));
			
					if (isLazyLoad)
					{
						relObjHelper2.Append(String.Format("\t\t\t\t\tCase \"{0}\" : Me.{0} = CType(value, ObjectHolder)\r\n", 
							fkMember));
					}
					else
					{
						relObjHelper2.Append(String.Format("\t\t\t\t\tCase \"{0}\" : Me.{0} = CType(value, {1})\r\n", 
							fkMember, pkTable));
					}
				}
			}
			finally {}

			// One-To-Many side
			try {
				DataTable keyFields = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys,
					new object[] {null,null,tableName});
					
				foreach (DataRow row in keyFields.Rows) {
					string fkTable = Global.GetObjectName(row["FK_TABLE_NAME"].ToString())
						+ Global.GetPropertyName(row["FK_TABLE_NAME"].ToString(),row["FK_COLUMN_NAME"].ToString());
					string pkField = Global.GetObjectName(row["PK_COLUMN_NAME"].ToString());
					if (fkTable.Length > pkField.Length && fkTable.EndsWith(pkField))
						fkTable = fkTable.Substring(0,fkTable.Length - pkField.Length);
					fkTable = Global.MakePlural(fkTable);
					string member = Global.GetMemberName(fkTable,prefix);

					relVarDeclaration.Append(String.Format("\t\tPrivate {0} As IList"
						+ " ' Supports both ObjectSet and Lazy-Loaded ObjectList\r\n", member));
					
					relProps.Append(String.Format("\t\tPublic ReadOnly Property {0}() As IList\r\n", fkTable));
					relProps.Append(String.Format("\t\t\tGet\r\n\t\t\t\tReturn Me.{0}\r\n", member));
					relProps.Append("\t\t\tEnd Get\r\n\t\tEnd Property\r\n\r\n");

					relObjHelper1.Append(String.Format("\t\t\t\t\tCase \"{0}\" : Return Me.{0}\r\n", member));
					relObjHelper2.Append(String.Format("\t\t\t\t\tCase \"{0}\" : Me.{0} = CType(value, IList)\r\n", member));
				}
			}
			finally {}

			connection.Close();

			varDecs = relVarDeclaration.ToString();
			properties = relProps.ToString();
			helper1 = relObjHelper1.ToString();
			helper2 = relObjHelper2.ToString();
		}

		private static string GetObjectName(string fieldName) {
			string objectName = String.Empty;
			string[] wordParts = fieldName.Split('_', ' ');
			foreach (string wordPart in wordParts) {
				try {
					if (wordPart.ToUpper() == wordPart) {
						objectName += wordPart.Substring(0, 1).ToUpper() + wordPart.Substring(1).ToLower();
					}
					else {
						objectName += wordPart.Substring(0, 1).ToUpper() + wordPart.Substring(1);
					}
				}
				catch {
					objectName += wordPart;
				}
			}

			objectName = MakeSingular(objectName);

			return objectName.ToString();
		}

		private static string GetMemberName(string objectName, string prefix) {
			try {
				return prefix + objectName.Substring(0, 1).ToLower() + objectName.Substring(1);
			}
			catch {
				return prefix + objectName;
			}
		}

		private static string MakeSingular(string objectName) {
			// Plural Cases Improved by Chad Humphries (http://www.afsweb.net)
			// Added "sis" exception for medical terms (David D'Amico)
			if (!objectName.EndsWith("sis")) {
				if (objectName.EndsWith("ies")) {
					objectName = objectName.Remove(objectName.Length - 3, 3) + "y";
				}
				else if (objectName.EndsWith("ses")) {
					objectName = objectName.Remove(objectName.Length - 2, 2);
				}
				else if (objectName.EndsWith("s") && !objectName.EndsWith("ss") && !objectName.EndsWith("us")) {
					objectName = objectName.Remove(objectName.Length - 1, 1);
				}
			}
			return objectName.ToString();
		}

		private static string MakePlural(string objectName) {
			// Added "sis" exception for medical terms (David D'Amico)
			// Added additional "y" exception (David D'Amico)
			if (!objectName.EndsWith("sis")) {
				// First make singular to avoid double plurals
				objectName = Global.MakeSingular(objectName);
 
				if (objectName.EndsWith("y")) {
					if ( ((string)"aeiou").IndexOf(objectName.Substring(objectName.Length-2, 1)) > -1 ) 
						objectName += "s";
					else
						objectName = objectName.Remove(objectName.Length - 1, 1) + "ies";
				}
				else if (objectName.EndsWith("s")) {
					objectName = objectName + "es";
				}
				else {
					objectName = objectName + "s";
				}
			}
			return objectName.ToString();
		}

		private static string CSharpType(OleDbType dbType) {
			switch (dbType) {
				case OleDbType.BigInt: return "long";
				case OleDbType.Binary: return "byte[]";
				case OleDbType.Boolean: return "bool";
				case OleDbType.BSTR: return "string";
				case OleDbType.Char: return "string";
				case OleDbType.Currency: return "decimal";
				case OleDbType.Date: return "DateTime";
				case OleDbType.DBDate: return "DateTime";
				case OleDbType.DBTime: return "TimeSpan";
				case OleDbType.DBTimeStamp: return "DateTime";
				case OleDbType.Decimal: return "decimal";
				case OleDbType.Double: return "double";
				case OleDbType.Filetime: return "DateTime";
				case OleDbType.Guid: return "Guid";
				case OleDbType.Integer: return "int";
				case OleDbType.LongVarBinary: return "byte[]";
				case OleDbType.LongVarChar: return "string";
				case OleDbType.LongVarWChar: return "string";
				case OleDbType.Numeric: return "decimal";
				case OleDbType.Single: return "float";
				case OleDbType.SmallInt: return "short";
				case OleDbType.TinyInt: return "sbyte";
				case OleDbType.UnsignedBigInt: return "ulong";
				case OleDbType.UnsignedInt: return "uint";
				case OleDbType.UnsignedSmallInt: return "ushort";
				case OleDbType.UnsignedTinyInt: return "byte";
				case OleDbType.VarBinary: return "byte[]";
				case OleDbType.VarChar: return "string";
				case OleDbType.Variant: return "object";
				case OleDbType.VarNumeric: return "decimal";
				case OleDbType.VarWChar: return "string";
				case OleDbType.WChar: return "string";
				default: return "object";
			}
		}

		private static string VBNetType(OleDbType dbType) {
			switch (dbType) {
				case OleDbType.BigInt: return "Long";
				case OleDbType.Binary: return "Byte()";
				case OleDbType.Boolean: return "Boolean";
				case OleDbType.BSTR: return "String";
				case OleDbType.Char: return "String";
				case OleDbType.Currency: return "Decimal";
				case OleDbType.Date: return "Date";
				case OleDbType.DBDate: return "Date";
				case OleDbType.DBTime: return "TimeSpan";
				case OleDbType.DBTimeStamp: return "Date";
				case OleDbType.Decimal: return "Decimal";
				case OleDbType.Double: return "Double";
				case OleDbType.Filetime: return "Date";
				case OleDbType.Guid: return "Guid";
				case OleDbType.Integer: return "Integer";
				case OleDbType.LongVarBinary: return "Byte()";
				case OleDbType.LongVarChar: return "String";
				case OleDbType.LongVarWChar: return "String";
				case OleDbType.Numeric: return "Decimal";
				case OleDbType.Single: return "Single";
				case OleDbType.SmallInt: return "Short";
				case OleDbType.TinyInt: return "SByte";
				case OleDbType.UnsignedBigInt: return "UInt64";
				case OleDbType.UnsignedInt: return "UInt32";
				case OleDbType.UnsignedSmallInt: return "UInt16";
				case OleDbType.UnsignedTinyInt: return "Byte";
				case OleDbType.VarBinary: return "Byte()";
				case OleDbType.VarChar: return "String";
				case OleDbType.Variant: return "Object";
				case OleDbType.VarNumeric: return "Decimal";
				case OleDbType.VarWChar: return "String";
				case OleDbType.WChar: return "String";
				default: return "Object";
			}
		}

		private static string GetNullValue(OleDbType dbType, string registryAppKey) {
			try {
				RegistryKey registryUser = Registry.CurrentUser;
				RegistryKey registryApp = registryUser.OpenSubKey(registryAppKey);

				string nullValue = (string) registryApp.GetValue("Default_"
					+ Enum.GetName(typeof(OleDbType),dbType), string.Empty);

				if (nullValue != String.Empty)
					nullValue = "nullValue=\"" + nullValue.Replace((string)
						registryApp.GetValue("DefaultEmptyString", string.Empty), string.Empty) + "\" ";
			
				return nullValue;
			}
			catch {
				return string.Empty;
			}
		}

		// Includes better MySQL Support by Mike Mayer (http://www.mag37.com)
		// Strips off the \0 characters from the MyOleDB provider for MySQL
		static string Clean(string str) {
			str = str.Replace("\0", "");
			return str;
		}

		/// <summary>
		/// This method will escape the input value if it is a keyword.
		/// </summary>
		/// <param name="inputValue">This is the input value.</param>
		/// <param name="isCS">This is a flag indicating whether to use C#-based-escaping (true) or VB-based-escaping (false).</param>
		/// <returns>This is the escaped value.</returns>
		private static string EscapeKeyword(string inputValue, bool isCS)
		{
			if (inputValue == null)
			{
				throw new System.ArgumentNullException("The input 'inputValue' is null.");
			}
			else
			{
				//Continue.
			}

			if (inputValue.Trim() == string.Empty)
			{
				throw new System.NotSupportedException("The input 'inputValue' is an empty string and not supported in this context.");
			}
			else
			{
				//Continue.
			}

			string myEscapedValue = inputValue;

			int mySearchResult = Array.BinarySearch(keywordsArray, inputValue.ToUpper());

			bool isKeyword = false;

			if (mySearchResult < 0) 
			{
				isKeyword = false;
			}
			else
			{
				isKeyword = true;
			}

			if (isKeyword)
			{
				if (isCS)
				{
					myEscapedValue = "@" + inputValue;
				}
				else
				{
					myEscapedValue = "[" + inputValue + "]";
				}
			}
			else
			{
				myEscapedValue = inputValue;
			}

			return myEscapedValue;
		}
	}
}
