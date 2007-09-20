using System;
using System.Configuration;
using System.IO;
using System.Web;
using Wilson.ORMapper;
using WilsonORWrapper.Logging;

namespace WilsonORWrapper.Providers
{
	/// <summary>
	/// Singleton class which exposes internal classes and methods for interacting
	/// with the O/R mapper.
	/// </summary>
	public static class DataProvider
	{
		// singleton instances
		private static volatile ObjectSpace _objectSpace;
		private static volatile  ConnectionStringSettings _connectionStringSettings;

		// lock objects, each singleton has own to prevent deadlock
		private static readonly object _objectSpaceLock = new Object();
		private static readonly object _connectionStringSettingsLock = new Object();

		// extra features
		private static readonly Guid CONTEXT_KEY = Guid.NewGuid();

		#region Public Properties
		/// <summary>The singleton instance of an ObjectSpace Class</summary>
		public static ObjectSpace ObjectSpace
		{
			get
			{
				if (_objectSpace == null)
				{
					lock (_objectSpaceLock)
					{
						if (_objectSpace == null)
						{
							_objectSpace = GetObjectSpace();
						}
					}
				}

				// Utilize HttpContext where appropriate
				HttpContext context = HttpContext.Current;
				if (context != null)
				{
					ObjectSpace isolatedContext = context.Items[CONTEXT_KEY] as ObjectSpace;
					if (isolatedContext == null)
					{
						// Cache the isolated context in the current request so that it is used
						// throughout the entire request
						isolatedContext = _objectSpace.IsolatedContext;
						context.Items[CONTEXT_KEY] = isolatedContext;
					}

					return isolatedContext;
				}
				else
				{
					return _objectSpace;
				}
			}
		}
		/// <summary>The application connection string provider name read from the configuration file.
		/// The DataProvider will automatically recognize the following provider names, and map to 
		/// the specified O/R mapper provider.
		/// 
		/// Build-in providers include:
		/// 
		/// System.Data.SqlClient -> Provider.Sql2005
		/// System.Data.OleDb -> Provider.OleDb
		/// System.Data.Odbc -> Provider.Odbc
		/// System.Data.OracleClient -> Provider.Oracle
		/// 
		/// Custom providers include:
		/// 
		/// MySql.Data -> MySql (http://dev.mysql.com/tech-resources/articles/dotnet/)
		/// ByteFX.MySqlClient -> MySql (http://sourceforge.net/projects/mysqlnet/)
		/// CoreLab.MySql -> MySql (http://crlab.com/mysqlnet/)
		/// Npgsql -> PostgreSQL (http://pgfoundry.org/projects/npgsql)
		/// Finisar.SQLite -> SQLite (http://adodotnetsqlite.sourceforge.net/)
		/// FirebirdSql.Data.Firebird -> Firebird (http://www.dotnetfirebird.org/)
		/// IBM.Data.DB2 -> DB2 (http://publib.boulder.ibm.com/infocenter/db2luw/v8/index.jsp?topic=/com.ibm.db2.udb.dndp.doc/htm/frlrfIBMDataDB2.htm)
		/// VistaDB.Provider -> VistaDB (http://www.vistadb.net/vistadb3.asp)
		/// Sybase.Data.AseClient -> Sybase (http://manuals.sybase.com/onlinebooks/group-adonet/@Generic__CollectionView)
		/// Mono.Data.SybaseClient -> Sybase (http://www.mono-project.com/TDS_Providers)
		/// Wilson.XmlDbClient -> Paul Wilson's XmlDbClient (http://workspaces.gotdotnet.com/xmldbclient)
		/// System.Data.SqlServerCe -> SqlCE
		/// 
		/// Note that not all providers have been tested. If you find any problems please contact
		/// the developers of this library.
		/// </summary>
		public static ConnectionStringSettings ConnectionStringSettings
		{
			get
			{
				if (_connectionStringSettings == null)
				{
					lock (_connectionStringSettingsLock)
					{
						if (_connectionStringSettings == null)
						{
							_connectionStringSettings = GetConnectionStringSettings();
						}
					}
				}

				return _connectionStringSettings;
			}
		}
		#endregion

		#region Public Methods
		#endregion

		#region Private Methods
		private static ObjectSpace GetObjectSpace()
		{
			using (Stream mappingsFile = MappingsProvider.GetMappingsFile())
			{
				if (mappingsFile == null)
				{
					throw new DataProviderException("Could not load mappings file");
				}
				else
				{
					CustomProvider provider;
					string providerName = ConnectionStringSettings.ProviderName;
					string connectionString = ConnectionStringSettings.ConnectionString;

					#region Parse providername to get _objectSpace
					switch (providerName)
					{
						case "System.Data.SqlClient":
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, Provider.Sql2005, 20, 5);
							break;
						case "System.Data.OleDb":
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, Provider.OleDb, 20, 5);
							break;
						case "System.Data.Odbc":
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, Provider.Odbc, 20, 5);
							break;
						case "System.Data.OracleClient":
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, Provider.Oracle, 20, 5);
							break;
						case "MySql.Data":
							provider = new CustomProvider(
								"MySql.Data",
								"MySql.Data.MySqlClient.MySqlConnection",
								"MySql.Data.MySqlClient.MySqlDataAdapter");
							provider.StartDelimiter = "`";
							provider.EndDelimiter = "`";
							provider.ParameterPrefix = "?";
							provider.IdentityQuery = "SELECT LAST_INSERT_ID()";
							provider.SelectPageQuery = "SELECT * LIMIT {0} OFFSET {1}";
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "ByteFX.MySqlClient":
							provider = new CustomProvider(
								"ByteFX.MySqlClient",
								"ByteFX.Data.MySqlClient.MySqlConnection",
								"ByteFX.Data.MySqlClient.MySqlDataAdapter");
							provider.StartDelimiter = "`";
							provider.EndDelimiter = "`";
							provider.IdentityQuery = "SELECT LAST_INSERT_ID()";
							provider.SelectPageQuery = "SELECT * LIMIT {0} OFFSET {1}";
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "CoreLab.MySql":
							provider = new CustomProvider(
								"CoreLab.MySql",
								"CoreLab.MySql.MySqlConnection",
								"CoreLab.MySql.MySqlDataAdapter");
							provider.StartDelimiter = "`";
							provider.EndDelimiter = "`";
							provider.IdentityQuery = "SELECT LAST_INSERT_ID()";
							provider.SelectPageQuery = "SELECT * LIMIT {0} OFFSET {1}";
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "Npgsql":
							provider = new CustomProvider(
								"Npgsql",
								"Npgsql.NpgsqlConnection",
								"Npgsql.NpgsqlDataAdapter");
							provider.StartDelimiter = "\"";
							provider.EndDelimiter = "\"";
							provider.ParameterPrefix = ":";
							provider.IdentityQuery = "SELECT currval('{1}_{0}_seq')";
							provider.SelectPageQuery = "SELECT * LIMIT {0} OFFSET {1}";
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "Finisar.SQLite":
							provider = new CustomProvider(
								"Finisar.SQLite",
								"Finisar.SQLite.SQLiteConnection",
								"Finisar.SQLite.SQLiteDataAdapter");
							provider.StartDelimiter = "[";
							provider.EndDelimiter = "]";
							provider.IdentityQuery = "SELECT last_insert_rowid()";
							provider.SelectPageQuery = "SELECT * LIMIT {0} OFFSET {1}";
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "FirebirdSql.Data.Firebird":
							provider = new CustomProvider(
								"FirebirdSql.Data.Firebird",
								"FirebirdSql.Data.Firebird.FbConnection",
								"FirebirdSql.Data.Firebird.FbDataAdapter");
							provider.StartDelimiter = "\"";
							provider.EndDelimiter = "\"";
							provider.IdentityQuery = "SELECT gen_id(gen_{1}_id, 0) FROM RDB$DATABASE";
							provider.SelectPageQuery = "SELECT FIRST {0} SKIP {1} *";
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "IBM.Data.DB2":
							provider = new CustomProvider(
								"IBM.Data.DB2",
								"IBM.Data.DB2.DB2Connection",
								"IBM.Data.DB2.DB2DataAdapter");
							provider.StartDelimiter = "`";
							provider.EndDelimiter = "`";
							provider.IdentityQuery = "VALUES IDENTITY_VAL_LOCAL()";
							provider.SelectPageQuery = null;
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "VistaDB":
							provider = new CustomProvider(
								"VistaDB.Provider",
								"VistaDB.VistaDBConnection",
								"VistaDB.VistaDBDataAdapter");
							provider.StartDelimiter = "[";
							provider.EndDelimiter = "]";
							provider.IdentityQuery = "SELECT LastIdentity({0}) FROM {1}";
							provider.SelectPageQuery = "SELECT TOP {2}, {0} *";
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "Sybase.Data.AseClient":
							provider = new CustomProvider(
								"Sybase.Data.AseClient",
								"Sybase.Data.AseClient.AseConnection",
								"Sybase.Data.AseClient.AseDataAdapter");
							provider.StartDelimiter = "[";
							provider.EndDelimiter = "]";
							provider.IdentityQuery = "SELECT @@IDENTITY";
							provider.SelectPageQuery = null;
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "Mono.Data.SybaseClient":
							provider = new CustomProvider(
								"Mono.Data.SybaseClient",
								"Mono.Data.SybaseClient.SybaseConnection",
								"Mono.Data.SybaseClient.SybaseDataAdapter");
							provider.StartDelimiter = "[";
							provider.EndDelimiter = "]";
							provider.IdentityQuery = "SELECT @@IDENTITY";
							provider.SelectPageQuery = null;
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "Wilson.XmlDbClient":
							provider = new CustomProvider(
								"WilsonXmlDbClient",
								"Wilson.XmlDbClient.XmlDbConnection",
								"Wilson.XmlDbClient.XmlDbDataAdapter");
							provider.StartDelimiter = "[";
							provider.EndDelimiter = "]";
							provider.IdentityQuery = "SELECT @@Identity;";
							provider.SelectPageQuery = "SELECT * LIMIT {0} OFFSET {1}";
							_objectSpace = new ObjectSpace(mappingsFile, connectionString, provider, 20, 5);
							break;
						case "System.Data.SqlServerCe":
							provider = new CustomProvider(
								"System.Data.SqlServerCe",
								"System.Data.SqlServerCe.SqlCeConnection",
								"System.Data.SqlServerCe.SqlCeDataAdapter");
							provider.StartDelimiter = "[";
							provider.EndDelimiter = "]";
							provider.IdentityQuery = "SELECT SCOPE_IDENTITY()";
							provider.SelectPageQuery = null;
							break;
						default:
							throw new DataProviderException("ProviderName is invalid");
					}
					#endregion

					// Create the log interceptor
					IInterceptCommand interceptor = new ORMapperInterceptor();
					_objectSpace.SetInterceptor(interceptor);

					return _objectSpace;
				}
			}
		}
		private static ConnectionStringSettings GetConnectionStringSettings()
		{
			ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[ConfigurationSettings.Settings.ConnectionString];

			if (settings == null)
			{
				throw new DataProviderException("Could not find connection string");
			}

			return settings;
		}
		#endregion
	}
}
