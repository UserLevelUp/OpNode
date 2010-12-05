using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration.Internal;

namespace myPword
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
	{

		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAbout()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAbout));
			this.label5 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(168, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 23);
			this.label5.TabIndex = 4;
			this.label5.Text = "The Power of pWord.";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 496);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(72, 32);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(280, 296);
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.pictureBox2);
			this.panel1.Location = new System.Drawing.Point(0, 328);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(472, 200);
			this.panel1.TabIndex = 9;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(472, 200);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 11;
			this.pictureBox2.TabStop = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(144, 496);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(296, 23);
			this.label3.TabIndex = 10;
			this.label3.Text = "This project is licensed under the GNU Public License.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(360, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 304);
			this.label2.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 328);
			this.label1.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(179, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 24);
			this.label4.TabIndex = 11;
			this.label4.Text = "Copyright 2006";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// frmAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 534);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmAbout";
			this.Text = "About pWord";
			this.Load += new System.EventHandler(this.frmAbout_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
		//	System.Diagnostics.Process.Start(linkLabel1.Tag.ToString());
		}

		private void frmAbout_Load(object sender, System.EventArgs e)
		{
			this.label1.Text = "Developers:\nMarc Noon - mnoon\n\nPeter Twiggs - petertt\n\nQuenten - qgriffith\n\nCodEjunKi\n\n\nConsultant:\nCoppertronian";
			this.label2.Text ="Beta Testers:\nChuck Moore - vizion.";
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
			// go to website baby
			try
			{
				System.Diagnostics.Process.Start("http://pword.sourceforge.net/");
				this.Close();
			}
			catch(Exception f)
			{
				MessageBox.Show(f.Message);
			}
			

		}


	}
}
