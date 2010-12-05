using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using LL;

namespace myPword
{
	/// <summary>
	/// Summary description for AddItem.
	/// </summary>
	public class AddItem : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public LL.List group = new LL.List();
		private System.Windows.Forms.Button btnSubmit;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ComboBox cboGroupType;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.Button button1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddItem()
		{
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AddItem));
			this.cboGroupType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.btnSubmit = new System.Windows.Forms.Button();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cboGroupType
			// 
			this.cboGroupType.BackColor = System.Drawing.Color.LightGreen;
			this.cboGroupType.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.cboGroupType.Items.AddRange(new object[] {
															  "Username",
															  "Password",
															  "GROUP",
															  "SCRIPT",
															  "ROBOT"});
			this.cboGroupType.Location = new System.Drawing.Point(88, 24);
			this.cboGroupType.Name = "cboGroupType";
			this.cboGroupType.Size = new System.Drawing.Size(168, 21);
			this.cboGroupType.TabIndex = 0;
			this.cboGroupType.Visible = false;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightGreen;
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(8, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select Group";
			this.label1.Visible = false;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.LightGreen;
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 21);
			this.label2.TabIndex = 3;
			this.label2.Text = "Enter Name";
			// 
			// txtName
			// 
			this.txtName.AllowDrop = true;
			this.txtName.Location = new System.Drawing.Point(88, 64);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(168, 20);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			// 
			// btnSubmit
			// 
			this.btnSubmit.BackColor = System.Drawing.Color.LightGreen;
			this.btnSubmit.Location = new System.Drawing.Point(88, 256);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.TabIndex = 3;
			this.btnSubmit.Text = "Add";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// txtValue
			// 
			this.txtValue.AcceptsReturn = ((bool)(configurationAppSettings.GetValue("txtValue.AcceptsReturn", typeof(bool))));
			this.txtValue.AcceptsTab = ((bool)(configurationAppSettings.GetValue("txtValue.AcceptsTab", typeof(bool))));
			this.txtValue.AllowDrop = ((bool)(configurationAppSettings.GetValue("txtValue.AllowDrop", typeof(bool))));
			this.txtValue.Enabled = ((bool)(configurationAppSettings.GetValue("txtValue.Enabled", typeof(bool))));
			this.txtValue.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtValue.Location = new System.Drawing.Point(88, 96);
			this.txtValue.Multiline = ((bool)(configurationAppSettings.GetValue("txtValue.Multiline", typeof(bool))));
			this.txtValue.Name = "txtValue";
			this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtValue.Size = new System.Drawing.Size(432, 136);
			this.txtValue.TabIndex = 2;
			this.txtValue.Text = "";
			this.txtValue.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtValue_DragDrop);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.LightGreen;
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(8, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 24);
			this.label3.TabIndex = 7;
			this.label3.Text = "value";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.LightGreen;
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(408, 256);
			this.button1.Name = "button1";
			this.button1.TabIndex = 4;
			this.button1.Text = "Cancel";
			// 
			// AddItem
			// 
			this.AcceptButton = this.btnSubmit;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.CancelButton = this.button1;
			this.ClientSize = new System.Drawing.Size(536, 293);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.btnSubmit);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboGroupType);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddItem";
			this.Text = "Add Item Form";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			// verify if applicable
			
			// add group name and value
			group.InsertAtBack(this.txtName.Text);
			group.InsertAtBack(this.txtValue.Text);
			// add group type
		//	group.InsertAtBack(this.cboGroupType.SelectedItem.ToString());
			this.DialogResult = DialogResult.OK;
			this.Close();




		}

		private void txtValue_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			

		}
	}
}
