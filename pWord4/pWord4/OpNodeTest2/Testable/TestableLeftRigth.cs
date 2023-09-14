using pWordLib.dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpNodeTest2
{
    public class TestableLeftRight : LeftRight.LeftRight
    {
        public void SetInitialIndex(int value)
        {
            this.index = value;
        }

        public int GetIndex()
        {
            return this.index;
        }

        public void AddMasterItem(string item)
        {
            this.MasterNames.Add(item);
        }
        public void AddMasterValueItem(object item)
        {
            this.MasterNodes.Add(item as pNode);
        }

        public void SimulateLeftClick()
        {
            this.btnLeft_Click(null, null);
        }

        public void SimulateRightClick()
        {
            this.btnRight_Click(null, null);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TestableLeftRight
            // 
            this.Name = "TestableLeftRight";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
