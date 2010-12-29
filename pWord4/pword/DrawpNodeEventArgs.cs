using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using pWordLib.dat;

namespace myPword
{
    public class DrawpNodeEventArgs : DrawTreeNodeEventArgs
    {
        public DrawpNodeEventArgs(Graphics graphics, pNode node, Rectangle bounds, TreeNodeStates state)
            : base(graphics, node, bounds, state)
        {
            
        }

        private int a;

        public int A
        {
            get { return a; }
            set { a = value; }
        }

        public pNode Node {
            get
            {
                return this.Node;
            }
        

        }
    }
}
