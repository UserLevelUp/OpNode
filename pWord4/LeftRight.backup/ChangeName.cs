using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LeftRight
{
    public partial class ChangeName : Form
    {
        public ChangeName()
        {
            InitializeComponent();
        }

        private String masterName;

        public String MasterName
        {
          get { return masterName; }
        }



        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.TextLength == 0)
            {
                btnAccept.Enabled = false;
            }
            else
            {
                btnAccept.Enabled = true;
            }
        }

        private Keys lastKeyPressed;
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            lastKeyPressed = e.KeyCode;
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lastKeyPressed == Keys.Back)
            {

            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            masterName = txtName.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void txtName_SizeChanged(object sender, EventArgs e)
        {

        }





    }
}
