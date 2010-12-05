using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LeftRight
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	/// 

	public class LeftRight : System.Windows.Forms.UserControl
	{
		public System.Windows.Forms.Button btnLeft;
		public System.Windows.Forms.TextBox txtMaster;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		// My vars
		public ArrayList Masters = new ArrayList();
		public ArrayList MastersValue = new ArrayList();
		public System.Windows.Forms.Button btnRight;
        private ContextMenuStrip ctxChangeName;
        private ToolStripMenuItem ctxItemChangeName;
		public int index =0;


		// An event that clients can use to be notified whenever the
		// elements of the list change:
		public event EventHandler LeftClicked;
		public event EventHandler RightClicked;

		// Invoke the Changed event; called whenever list changes:
		protected virtual void OnLeftClicked(EventArgs e) 
		{
			if (LeftClicked != null)
				LeftClicked(this,e);
		}

		protected virtual void OnRightClicked(EventArgs e)
		{
			if (RightClicked != null)
				RightClicked(this,e);
		}

		public LeftRight()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeftRight));
            this.btnLeft = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnRight = new System.Windows.Forms.Button();
            this.txtMaster = new System.Windows.Forms.TextBox();
            this.ctxChangeName = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxItemChangeName = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxChangeName.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLeft
            // 
            this.btnLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLeft.ImageIndex = 0;
            this.btnLeft.ImageList = this.imageList1;
            this.btnLeft.Location = new System.Drawing.Point(0, 0);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(24, 24);
            this.btnLeft.TabIndex = 0;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            this.btnLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.btnLeft_Paint);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // btnRight
            // 
            this.btnRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRight.ImageIndex = 1;
            this.btnRight.ImageList = this.imageList1;
            this.btnRight.Location = new System.Drawing.Point(152, 0);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(24, 24);
            this.btnRight.TabIndex = 1;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            this.btnRight.Paint += new System.Windows.Forms.PaintEventHandler(this.button1_Paint);
            // 
            // txtMaster
            // 
            this.txtMaster.ContextMenuStrip = this.ctxChangeName;
            this.txtMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaster.Location = new System.Drawing.Point(24, 0);
            this.txtMaster.Name = "txtMaster";
            this.txtMaster.Size = new System.Drawing.Size(128, 20);
            this.txtMaster.TabIndex = 2;
            this.txtMaster.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtMaster_MouseClick);
            // 
            // ctxChangeName
            // 
            this.ctxChangeName.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxItemChangeName});
            this.ctxChangeName.Name = "ctxChangeName";
            this.ctxChangeName.Size = new System.Drawing.Size(153, 48);
            // 
            // ctxItemChangeName
            // 
            this.ctxItemChangeName.Name = "ctxItemChangeName";
            this.ctxItemChangeName.Size = new System.Drawing.Size(152, 22);
            this.ctxItemChangeName.Text = "Change Name";
            this.ctxItemChangeName.Click += new System.EventHandler(this.ctxItemChangeName_Click);
            // 
            // LeftRight
            // 
            this.Controls.Add(this.txtMaster);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Name = "LeftRight";
            this.Size = new System.Drawing.Size(176, 24);
            this.ctxChangeName.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnLeft_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
		}

		private void button1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		


		}

		public void btnLeft_Click(object sender, System.EventArgs e)
		{
			if (index >0)
			{
				index--;
				this.txtMaster.Text = (string)Masters[index];
				this.txtMaster.Tag = MastersValue[index];
				// call event
				OnLeftClicked(EventArgs.Empty);
			}
			else if (index == 0)
			{
				index = 0;
				this.txtMaster.Text = (string)Masters[index];
				this.txtMaster.Tag = MastersValue[index];
			}


		}

		public void btnRight_Click(object sender, System.EventArgs e)
		{
			if (index < (Masters.Count-1))
			{
				index++;
				this.txtMaster.Text = (string)Masters[index];
				this.txtMaster.Tag = MastersValue[index];
				OnRightClicked(EventArgs.Empty);
			}
			else if (index == Masters.Count-1)
			{
	
				this.txtMaster.Text = (string)Masters[index];
				this.txtMaster.Tag = MastersValue[index];

			}
		}

        private void txtMaster_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // Bring aup a window to change the name of this Master
            }
        }

        private void ctxItemChangeName_Click(object sender, EventArgs e)
        {
            ChangeName name = new ChangeName();
            DialogResult diaglogResult = name.ShowDialog();
            if (diaglogResult == DialogResult.OK)
            {
                // set the new name
                Masters[index] = name.MasterName;
                txtMaster.Text = (String)Masters[index];
            }
            else if (diaglogResult == DialogResult.Cancel)
            {
                // do nothing because the change of the master name was not accepted by the user
            }
        }




	}
}
