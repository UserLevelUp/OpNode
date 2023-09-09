using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpNodeTest2
{
    public class TestableLeftRightWrapper : TestableLeftRight
    {
        public virtual new Control.ControlCollection Controls
        {
            get { return base.Controls; }
        }
    }
}
