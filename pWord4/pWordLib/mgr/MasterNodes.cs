using pWordLib.dat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pWordLib.mgr
{
    public class MasterNodesMgr
    {
        public List<pNode> MasterNodes { get; private set; } = new List<pNode>();
        public List<string> MasterNames { get; private set; } = new List<string>();
        public int CurrentIndex { get; private set; } = 0;

        public void AddMasterNode(pNode node, string name)
        {
            MasterNodes.Add(node);
            MasterNames.Add(name);
        }

        public (pNode, string) GetCurrentMasterNodeAndName()
        {
            return (MasterNodes[CurrentIndex], MasterNames[CurrentIndex]);
        }

        public void MoveLeft()
        {
            if (CurrentIndex > 0)
            {
                CurrentIndex--;
            }
        }

        public void MoveRight()
        {
            if (CurrentIndex < MasterNodes.Count - 1)
            {
                CurrentIndex++;
            }
        }

        // ... other methods for managing master nodes and their state
    }
}
