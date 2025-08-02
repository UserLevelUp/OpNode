using pWordLib.dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpNodeTest2
{
    public partial class TestablePNode : pNode
    {
        public void SetNameForTesting(string key)
        {
            // Call the setName method from the base class
            setName(key);
        }

        public string GetNameForTesting()
        {
            return getName();
        }
    }
}
