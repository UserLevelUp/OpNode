using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace pWordLib.dat
{
    
    public interface IOperate: IChange
    {
        Icon Symbol { get; }
        pNode Operate(pNode node);
	}

    public interface IChange
    {
	    bool Changed { get; }
        bool Change(pNode node);
        void ChangeFalse(pNode pNode);
    }
}
