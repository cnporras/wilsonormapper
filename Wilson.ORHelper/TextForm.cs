//**************************************************************//
// Paul Wilson -- www.WilsonDotNet.com -- Paul@WilsonDotNet.com //
// Feel free to use and modify -- just leave these credit lines //
// I also always appreciate any other public credit you provide //
//**************************************************************//
// Includes Several General Improvements by Chad Humphries (http://www.afsweb.net)
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace Wilson.ORHelper
{
	public class TextForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox TextFile;
		private System.Windows.Forms.Button GetDBObjects;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnSaveAndQuit;

		#region Windows Form Designer generated code
		private System.ComponentModel.Container components = null;

		public TextForm()	{
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
			this.TextFile = new System.Windows.Forms.TextBox();
			this.GetDBObjects = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.btnSaveAndQuit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TextFile
			// 
			this.TextFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextFile.Location = new System.Drawing.Point(10, 10);
			this.TextFile.Multiline = true;
			this.TextFile.Name = "TextFile";
			this.TextFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TextFile.Size = new System.Drawing.Size(480, 425);
			this.TextFile.TabIndex = 0;
			this.TextFile.Text = "";
			this.TextFile.WordWrap = false;
			// 
			// GetDBObjects
			// 
			this.GetDBObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.GetDBObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GetDBObjects.Location = new System.Drawing.Point(500, 10);
			this.GetDBObjects.Name = "GetDBObjects";
			this.GetDBObjects.Size = new System.Drawing.Size(120, 40);
			this.GetDBObjects.TabIndex = 15;
			this.GetDBObjects.Text = "&Save to File";
			this.GetDBObjects.Click += new System.EventHandler(this.GetDBObjects_Click);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(495, 360);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(130, 75);
			this.label6.TabIndex = 17;
			this.label6.Text = "WilsonORMapper WilsonORHelper ©2003-2005               By: Paul Wilson WilsonDotN" +
				"et.com";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// btnSaveAndQuit
			// 
			this.btnSaveAndQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveAndQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnSaveAndQuit.Location = new System.Drawing.Point(500, 60);
			this.btnSaveAndQuit.Name = "btnSaveAndQuit";
			this.btnSaveAndQuit.Size = new System.Drawing.Size(120, 40);
			this.btnSaveAndQuit.TabIndex = 18;
			this.btnSaveAndQuit.Text = "Save to File and &Close Window";
			this.btnSaveAndQuit.Click += new System.EventHandler(this.btnSaveAndQuit_Click);
			// 
			// TextForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 446);
			this.Controls.Add(this.btnSaveAndQuit);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.GetDBObjects);
			this.Controls.Add(this.TextFile);
			this.DockPadding.All = 10;
			this.MinimizeBox = false;
			this.Name = "TextForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Wilson ORMapper Helper";
			this.ResumeLayout(false);

		}
		#endregion

		private static string finalFileName = String.Empty;

		public static string Show(string fileName, string textFile) {
			TextForm form = new TextForm();
			form.Text = fileName;
			form.TextFile.Text = textFile;
			form.ShowDialog();
			return finalFileName;
		}

		private void GetDBObjects_Click(object sender, System.EventArgs e) {
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.FileName = this.Text;
			dialog.Title = "Save " + dialog.FileName;
			dialog.DefaultExt = new FileInfo(dialog.FileName).Extension.ToLower();
			switch (dialog.DefaultExt) {
				case "config": dialog.Filter = "Config Files (*.config)|*.config|All Files (*.*)|*.*"; break;
				case "cs": dialog.Filter = "C# Files (*.cs)|*.cs|All Files (*.*)|*.*"; break;
				case "vb": dialog.Filter = "VB Files (*.vb)|*.vb|All Files (*.*)|*.*"; break;
				default: dialog.Filter = "All Files (*.*)|*.*"; break;
			}
			dialog.FilterIndex = 0;
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK) {
				finalFileName = dialog.FileName;
			}
		}

		private void btnSaveAndQuit_Click(object sender, System.EventArgs e) {
			this.GetDBObjects_Click(this,new System.EventArgs());
			this.Close();
		}

	}
}
