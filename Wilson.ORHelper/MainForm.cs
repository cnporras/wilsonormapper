//**************************************************************//
// Paul Wilson -- www.WilsonDotNet.com -- Paul@WilsonDotNet.com //
// Feel free to use and modify -- just leave these credit lines //
// I also always appreciate any other public credit you provide //
//**************************************************************//
// Includes Relationships for Mappings and Entities and Default Null Values by
// David D'Amico (ORMapper@datasolinc.com) and Craig Tucker (ORMapper@hrzdata.com)
// Includes Several General Improvements by Chad Humphries (http://www.afsweb.net)
// Thanks to Sam Smoot for the ability to create Mappings for just one Entity
// Thanks to Mark Kamoski (MKamoski@WebLogicArts.com - http://www.WebLogicArts.com)
//   for options for Data Types, LazyLoad False, DateTime Stamps, Escape Keywords
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;
using System.IO;

namespace Wilson.ORHelper
{
	public enum Provider {
		MsSql,
		Access,
		Oracle,
		OleDb
	}

	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid Mappings;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox DBType;
		private System.Windows.Forms.ComboBox Language;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox Prefix;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox ConnectString;
		private System.Windows.Forms.TextBox Namespace;
		private System.Windows.Forms.Button CreateMappings;
		private System.Windows.Forms.Button GetDBObjects;
		private System.Windows.Forms.Button CreateClass;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox Helper;
		private new System.Windows.Forms.CheckBox Events;
		private System.Windows.Forms.Button btnCreateAllClasses;
		private System.Windows.Forms.CheckBox Relationships;
		private System.Windows.Forms.Button btnEditDefaults;
		private System.Windows.Forms.Button CreateMapping;
		private System.Windows.Forms.Button CreateObjectSpace;
		private System.Windows.Forms.CheckBox DataTypes;
		private System.Windows.Forms.CheckBox LazyLoad;
		private System.Windows.Forms.CheckBox DateTimeStamp;
		private System.Windows.Forms.CheckBox EscapeKeywords;
		#region Windows Form Designer generated code
		private System.ComponentModel.Container components = null;

		public MainForm()	{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing ) {
			if ( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.Mappings = new System.Windows.Forms.DataGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.DBType = new System.Windows.Forms.ComboBox();
			this.Language = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ConnectString = new System.Windows.Forms.TextBox();
			this.Prefix = new System.Windows.Forms.TextBox();
			this.Namespace = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.CreateMappings = new System.Windows.Forms.Button();
			this.GetDBObjects = new System.Windows.Forms.Button();
			this.CreateClass = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.Helper = new System.Windows.Forms.CheckBox();
			this.Events = new System.Windows.Forms.CheckBox();
			this.btnCreateAllClasses = new System.Windows.Forms.Button();
			this.Relationships = new System.Windows.Forms.CheckBox();
			this.btnEditDefaults = new System.Windows.Forms.Button();
			this.CreateMapping = new System.Windows.Forms.Button();
			this.CreateObjectSpace = new System.Windows.Forms.Button();
			this.DataTypes = new System.Windows.Forms.CheckBox();
			this.LazyLoad = new System.Windows.Forms.CheckBox();
			this.DateTimeStamp = new System.Windows.Forms.CheckBox();
			this.EscapeKeywords = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.Mappings)).BeginInit();
			this.SuspendLayout();
			// 
			// Mappings
			// 
			this.Mappings.AllowNavigation = false;
			this.Mappings.AllowSorting = false;
			this.Mappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.Mappings.CaptionText = "Edit Object Names for Tables and Fields";
			this.Mappings.DataMember = "";
			this.Mappings.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.Mappings.Location = new System.Drawing.Point(10, 70);
			this.Mappings.Name = "Mappings";
			this.Mappings.Size = new System.Drawing.Size(480, 722);
			this.Mappings.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(10, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 20);
			this.label1.TabIndex = 2;
			this.label1.Text = "DB Type:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DBType
			// 
			this.DBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DBType.Location = new System.Drawing.Point(80, 10);
			this.DBType.Name = "DBType";
			this.DBType.Size = new System.Drawing.Size(70, 21);
			this.DBType.TabIndex = 4;
			this.DBType.SelectedIndexChanged += new System.EventHandler(this.DBType_SelectedIndexChanged);
			// 
			// Language
			// 
			this.Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Language.Location = new System.Drawing.Point(80, 40);
			this.Language.Name = "Language";
			this.Language.Size = new System.Drawing.Size(70, 21);
			this.Language.TabIndex = 6;
			this.Language.SelectedIndexChanged += new System.EventHandler(this.Language_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(10, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 20);
			this.label2.TabIndex = 5;
			this.label2.Text = "Language:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(160, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 20);
			this.label3.TabIndex = 8;
			this.label3.Text = "Private Field Prefix:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Cursor = System.Windows.Forms.Cursors.Default;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(160, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(110, 20);
			this.label4.TabIndex = 7;
			this.label4.Text = "Connection String:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ConnectString
			// 
			this.ConnectString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ConnectString.Location = new System.Drawing.Point(280, 10);
			this.ConnectString.Name = "ConnectString";
			this.ConnectString.Size = new System.Drawing.Size(340, 20);
			this.ConnectString.TabIndex = 9;
			this.ConnectString.Text = "";
			// 
			// Prefix
			// 
			this.Prefix.Location = new System.Drawing.Point(280, 40);
			this.Prefix.MaxLength = 5;
			this.Prefix.Name = "Prefix";
			this.Prefix.Size = new System.Drawing.Size(50, 20);
			this.Prefix.TabIndex = 10;
			this.Prefix.Text = "";
			// 
			// Namespace
			// 
			this.Namespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.Namespace.Location = new System.Drawing.Point(420, 40);
			this.Namespace.Name = "Namespace";
			this.Namespace.Size = new System.Drawing.Size(200, 20);
			this.Namespace.TabIndex = 12;
			this.Namespace.Text = "CompanyName.BusinessObjects";
			// 
			// label5
			// 
			this.label5.Cursor = System.Windows.Forms.Cursors.Default;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(340, 40);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 20);
			this.label5.TabIndex = 11;
			this.label5.Text = "Namespace:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CreateMappings
			// 
			this.CreateMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CreateMappings.BackColor = System.Drawing.SystemColors.Control;
			this.CreateMappings.Enabled = false;
			this.CreateMappings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CreateMappings.Image = ((System.Drawing.Image)(resources.GetObject("CreateMappings.Image")));
			this.CreateMappings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CreateMappings.Location = new System.Drawing.Point(500, 120);
			this.CreateMappings.Name = "CreateMappings";
			this.CreateMappings.Size = new System.Drawing.Size(120, 40);
			this.CreateMappings.TabIndex = 13;
			this.CreateMappings.Text = "     &Create All     Mappings";
			this.CreateMappings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.CreateMappings.Click += new System.EventHandler(this.CreateMappings_Click);
			// 
			// GetDBObjects
			// 
			this.GetDBObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.GetDBObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GetDBObjects.Location = new System.Drawing.Point(500, 70);
			this.GetDBObjects.Name = "GetDBObjects";
			this.GetDBObjects.Size = new System.Drawing.Size(120, 40);
			this.GetDBObjects.TabIndex = 14;
			this.GetDBObjects.Text = "&Get all Database Tables and Fields";
			this.GetDBObjects.Click += new System.EventHandler(this.GetDBObjects_Click);
			// 
			// CreateClass
			// 
			this.CreateClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CreateClass.BackColor = System.Drawing.SystemColors.Control;
			this.CreateClass.Enabled = false;
			this.CreateClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CreateClass.Image = ((System.Drawing.Image)(resources.GetObject("CreateClass.Image")));
			this.CreateClass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CreateClass.Location = new System.Drawing.Point(500, 220);
			this.CreateClass.Name = "CreateClass";
			this.CreateClass.Size = new System.Drawing.Size(120, 40);
			this.CreateClass.TabIndex = 15;
			this.CreateClass.Text = "     Class for only     &Selected Table";
			this.CreateClass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.CreateClass.Click += new System.EventHandler(this.CreateClass_Click);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(495, 722);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(130, 70);
			this.label6.TabIndex = 18;
			this.label6.Text = "WilsonORMapper WilsonORHelper ©2003-2005               By: Paul Wilson WilsonDotN" +
				"et.com";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// Helper
			// 
			this.Helper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Helper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Helper.Location = new System.Drawing.Point(500, 370);
			this.Helper.Name = "Helper";
			this.Helper.Size = new System.Drawing.Size(120, 40);
			this.Helper.TabIndex = 16;
			this.Helper.Text = "Implement IObjectHelper for Performance";
			// 
			// Events
			// 
			this.Events.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Events.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Events.Location = new System.Drawing.Point(500, 420);
			this.Events.Name = "Events";
			this.Events.Size = new System.Drawing.Size(120, 40);
			this.Events.TabIndex = 17;
			this.Events.Text = "Implement IObjectNotification for Events";
			// 
			// btnCreateAllClasses
			// 
			this.btnCreateAllClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreateAllClasses.BackColor = System.Drawing.SystemColors.Control;
			this.btnCreateAllClasses.Enabled = false;
			this.btnCreateAllClasses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnCreateAllClasses.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateAllClasses.Image")));
			this.btnCreateAllClasses.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCreateAllClasses.Location = new System.Drawing.Point(500, 270);
			this.btnCreateAllClasses.Name = "btnCreateAllClasses";
			this.btnCreateAllClasses.Size = new System.Drawing.Size(120, 40);
			this.btnCreateAllClasses.TabIndex = 19;
			this.btnCreateAllClasses.Text = "     Create &All     Classes";
			this.btnCreateAllClasses.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCreateAllClasses.Click += new System.EventHandler(this.btnCreateAllClasses_Click);
			// 
			// Relationships
			// 
			this.Relationships.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Relationships.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Relationships.Location = new System.Drawing.Point(500, 470);
			this.Relationships.Name = "Relationships";
			this.Relationships.Size = new System.Drawing.Size(120, 30);
			this.Relationships.TabIndex = 20;
			this.Relationships.Text = "Create Relationships";
			// 
			// btnEditDefaults
			// 
			this.btnEditDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEditDefaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnEditDefaults.Location = new System.Drawing.Point(500, 680);
			this.btnEditDefaults.Name = "btnEditDefaults";
			this.btnEditDefaults.Size = new System.Drawing.Size(120, 30);
			this.btnEditDefaults.TabIndex = 21;
			this.btnEditDefaults.Text = "Edit &Defaults";
			this.btnEditDefaults.Click += new System.EventHandler(this.btnEditDefaults_Click);
			// 
			// CreateMapping
			// 
			this.CreateMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CreateMapping.BackColor = System.Drawing.SystemColors.Control;
			this.CreateMapping.Enabled = false;
			this.CreateMapping.Image = ((System.Drawing.Image)(resources.GetObject("CreateMapping.Image")));
			this.CreateMapping.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CreateMapping.Location = new System.Drawing.Point(500, 170);
			this.CreateMapping.Name = "CreateMapping";
			this.CreateMapping.Size = new System.Drawing.Size(120, 40);
			this.CreateMapping.TabIndex = 22;
			this.CreateMapping.Text = "     &Mappings for     Selected Table";
			this.CreateMapping.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.CreateMapping.Click += new System.EventHandler(this.CreateMapping_Click);
			// 
			// CreateObjectSpace
			// 
			this.CreateObjectSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CreateObjectSpace.Enabled = false;
			this.CreateObjectSpace.Location = new System.Drawing.Point(500, 320);
			this.CreateObjectSpace.Name = "CreateObjectSpace";
			this.CreateObjectSpace.Size = new System.Drawing.Size(120, 40);
			this.CreateObjectSpace.TabIndex = 23;
			this.CreateObjectSpace.Text = "Create &ObjectSpace";
			this.CreateObjectSpace.Click += new System.EventHandler(this.CreateObjectSpace_Click);
			// 
			// DataTypes
			// 
			this.DataTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DataTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DataTypes.Location = new System.Drawing.Point(500, 510);
			this.DataTypes.Name = "DataTypes";
			this.DataTypes.Size = new System.Drawing.Size(120, 30);
			this.DataTypes.TabIndex = 24;
			this.DataTypes.Text = "Include Data Types In Mappings";
			// 
			// LazyLoad
			// 
			this.LazyLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LazyLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LazyLoad.Location = new System.Drawing.Point(500, 550);
			this.LazyLoad.Name = "LazyLoad";
			this.LazyLoad.Size = new System.Drawing.Size(120, 30);
			this.LazyLoad.TabIndex = 25;
			this.LazyLoad.Text = "Set LazyLoad = False In Mappings";
			// 
			// DateTimeStamp
			// 
			this.DateTimeStamp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DateTimeStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.DateTimeStamp.Location = new System.Drawing.Point(500, 590);
			this.DateTimeStamp.Name = "DateTimeStamp";
			this.DateTimeStamp.Size = new System.Drawing.Size(120, 30);
			this.DateTimeStamp.TabIndex = 26;
			this.DateTimeStamp.Text = "Include DateTime Stamp In Output";
			// 
			// EscapeKeywords
			// 
			this.EscapeKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EscapeKeywords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.EscapeKeywords.Location = new System.Drawing.Point(500, 630);
			this.EscapeKeywords.Name = "EscapeKeywords";
			this.EscapeKeywords.Size = new System.Drawing.Size(120, 30);
			this.EscapeKeywords.TabIndex = 27;
			this.EscapeKeywords.Text = "Escape Keywords In Output";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 803);
			this.Controls.Add(this.EscapeKeywords);
			this.Controls.Add(this.DateTimeStamp);
			this.Controls.Add(this.LazyLoad);
			this.Controls.Add(this.DataTypes);
			this.Controls.Add(this.CreateObjectSpace);
			this.Controls.Add(this.CreateMapping);
			this.Controls.Add(this.btnEditDefaults);
			this.Controls.Add(this.Relationships);
			this.Controls.Add(this.btnCreateAllClasses);
			this.Controls.Add(this.Events);
			this.Controls.Add(this.Helper);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.CreateClass);
			this.Controls.Add(this.GetDBObjects);
			this.Controls.Add(this.CreateMappings);
			this.Controls.Add(this.Namespace);
			this.Controls.Add(this.Prefix);
			this.Controls.Add(this.ConnectString);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Language);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.DBType);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Mappings);
			this.DockPadding.All = 10;
			this.Name = "MainForm";
			this.Text = "Wilson ORMapper Helper";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.Mappings)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private const string registryAppKey = @"Software\Wilson\ORHelper";
		private const string registryAppProvider = @"Provider";
		private const string registryAppConnect = @"Connect";
		private const string registryAppLanguage = @"Language";
		private const string registryAppPrefix = @"Prefix";
		private const string registryAppNamespace = @"Namespace";
		private const string registryAppIHelper = @"IHelper";
		private const string registryAppINotify = @"INotify";
		private const string registryAppIRelationship = @"IRelationship";
		private const string registryDefaultDateTime = @"DefaultNullDateTime";
		private const string registryAppIDataTypes = @"IDataTypes";
		private const string registryAppILazyLoad = @"ILazyLoad";
		private const string registryAppIDateTimeStamp = @"IDateTimeStamp";
		private const string registryAppIEscapeKeywords = @"IEscapeKeywords";

		private string destDirectory = String.Empty;

		private void Application_Error(object sender, System.Threading.ThreadExceptionEventArgs e) {
			MessageBox.Show(e.Exception.Message, "ORHelper Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void MainForm_Load(object sender, System.EventArgs e) {
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(this.Application_Error);
			
			this.DBType.Items.Add(Provider.MsSql.ToString());
			this.DBType.Items.Add(Provider.Access.ToString());
			this.DBType.Items.Add(Provider.Oracle.ToString());
			this.DBType.Items.Add(Provider.OleDb.ToString());

			this.Language.Items.Add("C#");
			this.Language.Items.Add("VB");

			try {
				RegistryKey registryUser = Registry.CurrentUser;
				RegistryKey registryApp = registryUser.OpenSubKey(registryAppKey);
				this.DBType.SelectedIndex = (int) registryApp.GetValue(registryAppProvider, 0);
				string connect = (string) registryApp.GetValue(registryAppConnect, null);
				if (connect != null) {
					this.ConnectString.Text = connect;
				}

				this.Language.SelectedIndex = (int) registryApp.GetValue(registryAppLanguage, 0);
				string prefix = (string) registryApp.GetValue(registryAppPrefix, null);
				if (prefix != null) {
					this.Prefix.Text = prefix;
				}
				
				this.Namespace.Text = (string) registryApp.GetValue(registryAppNamespace, "CompanyName.BusinessObjects");
				this.Helper.Checked = (int) registryApp.GetValue(registryAppIHelper, 0) == 1;
				this.Events.Checked = (int) registryApp.GetValue(registryAppINotify, 0) == 1;
				this.Relationships.Checked = (int) registryApp.GetValue(registryAppIRelationship, 0) == 1;
				this.DataTypes.Checked = (int) registryApp.GetValue(registryAppIDataTypes, 0) == 1;
				this.LazyLoad.Checked = (int) registryApp.GetValue(registryAppILazyLoad, 0) == 1;
				this.DateTimeStamp.Checked = (int) registryApp.GetValue(registryAppIDateTimeStamp, 0) == 1;
				this.EscapeKeywords.Checked = (int) registryApp.GetValue(registryAppIEscapeKeywords, 0) == 1;
			}
			catch {
				this.DBType.SelectedIndex = (int) Provider.MsSql;
				this.Language.SelectedIndex = 0;
			}

			this.Mappings.PreferredColumnWidth = 140;
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			try {
				RegistryKey registryUser = Registry.CurrentUser;
				RegistryKey registryApp = registryUser.CreateSubKey(registryAppKey);
				registryApp.SetValue(registryAppProvider, this.DBType.SelectedIndex);
				registryApp.SetValue(registryAppConnect, this.ConnectString.Text);
				registryApp.SetValue(registryAppLanguage, this.Language.SelectedIndex);
				registryApp.SetValue(registryAppPrefix, this.Prefix.Text);
				registryApp.SetValue(registryAppNamespace, this.Namespace.Text);
				registryApp.SetValue(registryAppIHelper, this.Helper.Checked ? 1 : 0);
				registryApp.SetValue(registryAppINotify, this.Events.Checked ? 1 : 0);
				registryApp.SetValue(registryAppIRelationship, this.Relationships.Checked ? 1 : 0);
				registryApp.SetValue(registryAppIDataTypes, this.DataTypes.Checked ? 1 : 0);
				registryApp.SetValue(registryAppILazyLoad, this.LazyLoad.Checked ? 1 : 0);
				registryApp.SetValue(registryAppIDateTimeStamp, this.DateTimeStamp.Checked ? 1 : 0);
				registryApp.SetValue(registryAppIEscapeKeywords, this.EscapeKeywords.Checked ? 1 : 0);
			}
			catch {
				// Do Nothing
			}
		}

		private void DBType_SelectedIndexChanged(object sender, System.EventArgs e) {
			switch ((Provider) this.DBType.SelectedIndex) {
				case Provider.MsSql: this.ConnectString.Text = "Server=(local);DataBase=?;UID=?;PWD=?;"; break;
				case Provider.Access: this.ConnectString.Text = @"C:\Path\FileName.mdb"; break;
				case Provider.Oracle: this.ConnectString.Text = "Server=(local);User ID=?;Password=?;"; break;
				default: this.ConnectString.Text = "Provider=?;Other=?;User ID=?;Password=?;"; break;
			}
		}

		private void Language_SelectedIndexChanged(object sender, System.EventArgs e) {
			if (this.Language.SelectedIndex == 1) {
				this.Prefix.Text = "_";
			}
			else {
				this.Prefix.Text = "";
			}
		}

		private void GetDBObjects_Click(object sender, System.EventArgs e) {
			string connectString = ConnectionString();

			if (connectString.Length == 0) {
				throw new ArgumentException("ORHelper: Connection String was Empty");
			}

			this.Mappings.DataSource = Global.GetSchema(connectString);
			this.CreateMappings.Enabled = true;
			this.CreateClass.Enabled = true;
			this.btnCreateAllClasses.Enabled = true;
			this.CreateMapping.Enabled = true;
			this.CreateObjectSpace.Enabled = true;
		}

		private string ConnectionString() {
			if (this.ConnectString.Text.Length == 0) {
				return "";
			}
			string connectString = this.ConnectString.Text;
			if (connectString.ToUpper().IndexOf("PROVIDER") < 0) {
				switch ((Provider) this.DBType.SelectedIndex) {
					case Provider.MsSql: connectString = "Provider=SQLOLEDB;" + connectString; break;
					case Provider.Access: connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + connectString; break;
					case Provider.Oracle: connectString = "Provider=MSDAORA;" + connectString; break;
					default: break;
				}
			}
			return connectString;
		}

		private void CreateMappings_Click(object sender, System.EventArgs e) {
			string connectString = ConnectionString();

			if (connectString.Length == 0) {
				throw new ArgumentException("ORHelper: Connection String was Empty");
			}

			if (this.Namespace.Text.Length == 0) {
				throw new ArgumentException("ORHelper: Namespace was Empty");
			}

			string mappings = Global.GetMappings((DataTable) this.Mappings.DataSource,
				this.Namespace.Text, this.Prefix.Text,connectString, this.Relationships.Checked, registryAppKey, true, this.DataTypes.Checked, !this.LazyLoad.Checked, this.DateTimeStamp.Checked);

			string fileName = TextForm.Show("Mappings.config", mappings);
			if (fileName.Length > 0) {
				SaveFile(fileName, mappings);
			}
		}

		private void CreateClass_Click(object sender, System.EventArgs e) {
			createClass(this.Mappings.CurrentRowIndex, DateTime.Now.ToString(Global.defaultDateTimeFormatString));
		}

		private void createClass(int row, string dateTimeValue){
			if (this.Namespace.Text.Length == 0) {
				throw new ArgumentException("ORHelper: Namespace was Empty");
			}
			string destFile = String.Empty;

			bool isCSharp = (this.Language.SelectedIndex == 0);
			while (this.Mappings[row, 0].ToString().Length == 0) { row--; }
			string classFile = this.Mappings[row, 2].ToString() + (isCSharp ? ".cs" : ".vb");

			string classCode = Global.GetClassCode((DataTable) this.Mappings.DataSource,
				this.Namespace.Text, this.Prefix.Text, isCSharp, row, this.Helper.Checked, this.Events.Checked, 
				this.Relationships.Checked, ConnectionString(), dateTimeValue, this.DateTimeStamp.Checked, this.EscapeKeywords.Checked, !this.LazyLoad.Checked);
			
			if (destDirectory.Length == 0) 
			{
				destFile = TextForm.Show(classFile,classCode);
				if (destFile.Length > 0) {
					destDirectory = Path.GetDirectoryName(destFile) + @"\";
				}
			}
			else {
				destFile = destDirectory + classFile;
			}

			if (destFile.Length > 0) {
				SaveFile(destFile, classCode);
			}
		}
		
		private void SaveFile(string fileName, string contents) {
			StreamWriter writer = null;
			try {
				writer = File.CreateText(fileName);
				writer.Write(contents);
			}
			finally {
				if (writer != null) writer.Close();
			}
		}

		private void btnCreateAllClasses_Click(object sender, System.EventArgs e){
			bool unattended = false;
			
			switch (MessageBox.Show("Automatically save ALL classes to the same directory?\n\n(No dialog boxes)",
					"Automatically Save All?",MessageBoxButtons.YesNoCancel)) {
				case DialogResult.No:
					unattended = false;
					break;
				case DialogResult.Yes:
					unattended = true;
					break;
				case DialogResult.Cancel:
					return;
			}

			int rowCount=((DataTable)(this.Mappings.DataSource)).Rows.Count;

			string dateTimeValue = DateTime.Now.ToString(Global.defaultDateTimeFormatString);

			for (int row=0;row<rowCount;row++) {
				if (this.Mappings[row,0].ToString().Length > 0) {
					createClass(row, dateTimeValue);

					if (!unattended) {
						destDirectory = String.Empty;
					}
				}
			}	
			destDirectory = String.Empty;
		}

		private void btnEditDefaults_Click(object sender, System.EventArgs e) {
			DefaultsForm.Show(registryAppKey);
		}

		private void CreateMapping_Click(object sender, System.EventArgs e)
		{
			string connectString = ConnectionString();
		
			int startrow = this.Mappings.CurrentRowIndex;
			while (this.Mappings[startrow, 0].ToString().Length == 0) { startrow--; }
		
			int endrow = startrow;
			while (this.Mappings[endrow + 1, 0].ToString().Length == 0) {
				endrow++;
				if (endrow >= (this.Mappings.DataSource as DataTable).Rows.Count - 1) break;
			}

			DataTable currentclass = new DataTable();	
			DataTable tables = (DataTable) this.Mappings.DataSource;

			foreach(DataColumn column in tables.Columns)
				currentclass.Columns.Add(new DataColumn(column.ColumnName, column.DataType));

			for(int x = startrow; x <= endrow; x++)
			{
				DataRow dr = currentclass.NewRow();

				for(int index = 0; index < tables.Columns.Count; index++)
					dr[index] = tables.Rows[x][index];

				currentclass.Rows.Add(dr);
			}

			if (connectString.Length == 0) 
			{
				throw new ArgumentException("ORHelper: Connection String was Empty");
			}

			if (this.Namespace.Text.Length == 0) 
			{
				throw new ArgumentException("ORHelper: Namespace was Empty");
			}

			string mappings = Global.GetMappings(currentclass, this.Namespace.Text, this.Prefix.Text, connectString,
				this.Relationships.Checked, registryAppKey, false, this.DataTypes.Checked, !this.LazyLoad.Checked, this.DateTimeStamp.Checked);

			string fileName = TextForm.Show("Mappings.config", mappings);

			if (fileName.Length > 0) 
			{
				SaveFile(fileName, mappings);
			}
		}

		private void CreateObjectSpace_Click(object sender, System.EventArgs e)
		{
			bool isCSharp = (this.Language.SelectedIndex == 0);
			Provider provider = (Provider) DBType.SelectedIndex;

			string objectspace = Global.GetObjectSpace(this.Namespace.Text, isCSharp,
				this.ConnectString.Text, provider.ToString(), this.DateTimeStamp.Checked);

			string fileName = TextForm.Show("Manager" + (isCSharp ? ".cs" : ".vb"), objectspace);

			if(fileName.Length > 0)
			{
				SaveFile(fileName, objectspace);
			}
		}
	}
}

