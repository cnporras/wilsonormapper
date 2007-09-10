//**************************************************************//
// Paul Wilson -- www.WilsonDotNet.com -- Paul@WilsonDotNet.com //
// Feel free to use and modify -- just leave these credit lines //
// I also always appreciate any other public credit you provide //
//**************************************************************//
// Includes Relationships for Mappings and Entities and Default Null Values by
// David D'Amico (ORMapper@datasolinc.com) and Craig Tucker (ORMapper@hrzdata.com)
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Win32;

namespace Wilson.ORHelper
{
	/// <summary>
	/// Summary description for DefaultsForm.
	/// </summary>
	public class DefaultsForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtDefaultEmpty;

		private static string registryAppKey;

		public static void Show(string regLocation) {
			registryAppKey = regLocation;

			DefaultsForm form = new DefaultsForm();
			form.ShowDialog();
		}

		public DefaultsForm() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtDefaultEmpty = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(44, 248);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(108, 36);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "&Close";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(172, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Character to signify empty string:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtDefaultEmpty
			// 
			this.txtDefaultEmpty.Location = new System.Drawing.Point(164, 0);
			this.txtDefaultEmpty.Name = "txtDefaultEmpty";
			this.txtDefaultEmpty.Size = new System.Drawing.Size(48, 20);
			this.txtDefaultEmpty.TabIndex = 2;
			this.txtDefaultEmpty.Text = "";
			// 
			// DefaultsForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 293);
			this.Controls.Add(this.txtDefaultEmpty);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DefaultsForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Set Null Defaults";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DefaultsForm_Closing);
			this.Load += new System.EventHandler(this.DefaultsForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void DefaultsForm_Load(object sender, System.EventArgs e) {
			System.Drawing.Point curLoc = new Point(0,24);
			int cnt = 0;
			string[] types = {"BigInt", "Binary", "Boolean", "BSTR", "Char", "Currency", "Date", "DBDate", "DBTime",
												 "DBTimeStamp", "Decimal", "Double", "Filetime", "Guid", "Integer", "LongVarBinary", "LongVarChar",
												 "LongVarWChar", "Numeric", "Single", "SmallInt", "TinyInt", "UnsignedBigInt", "UnsignedInt",
												 "UnsignedSmallInt", "UnsignedTinyInt", "VarBinary", "VarChar", "Variant", "VarNumeric", "VarWChar", "WChar"};
			int height = (int)Math.Floor((types.Length / 2) + .99);

			RegistryKey registryUser = null;
			RegistryKey registryApp = null;

			try {
				registryUser = Registry.CurrentUser;
				registryApp = registryUser.OpenSubKey(registryAppKey);
				txtDefaultEmpty.Text = (string) registryApp.GetValue("DefaultEmptyString", string.Empty);
			}
			catch {
				// Do Nothing
			}

			foreach (string s in types) {
				// Create the label
				System.Windows.Forms.Label lb = new Label();
				lb.Location = curLoc;
				lb.Name = "enuLabel" + cnt;
				lb.Size = new System.Drawing.Size(100, 20);
				lb.TabIndex = cnt*2;
				lb.Text = s;
				lb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

				// Create the text box
				System.Windows.Forms.TextBox tb = new TextBox();
				tb.Location = new System.Drawing.Point(lb.Location.X + lb.Size.Width, lb.Location.Y);
				tb.Name = "enum" + cnt.ToString();
				tb.TabIndex = cnt*2+1;
				tb.Tag = "Default_" + s;
				try {
					tb.Text = (string) registryApp.GetValue(tb.Tag.ToString(), string.Empty);
				}
				catch {
					// Do Nothing
				}

				cnt++;
				curLoc.X = (int)Math.Floor((double)(cnt / height)) * 250;
				curLoc.Y = ((cnt % height)+1) * 24;

				this.Controls.Add(lb);
				this.Controls.Add(tb);
			}
			this.btnOK.Location=new System.Drawing.Point(196, 30 + height * 24);

			this.Width= 500;
			this.Height = 100 + height * 24;

			if (registryApp != null) registryApp.Close();
			if (registryUser != null) registryUser.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void DefaultsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			switch (MessageBox.Show("Save Changes?","Save Changes",MessageBoxButtons.YesNoCancel)) {
				case DialogResult.Cancel:
					e.Cancel = true;
					break;
				
				case DialogResult.No:
					this.Dispose();
					break;

				case DialogResult.Yes:
					// Save the settings
					RegistryKey registryUser = Registry.CurrentUser;
					RegistryKey registryApp = registryUser.CreateSubKey(registryAppKey);

					registryApp.SetValue("DefaultEmptyString", txtDefaultEmpty.Text);

					foreach (Control control in this.Controls) {
						if (control is TextBox && control.Name.StartsWith("enum")) {
							registryApp.SetValue(control.Tag.ToString(), control.Text);
						}
					}
					registryApp.Close();
					registryUser.Close();
					this.Dispose();
					break;
			}
		}
	}
}