using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace pWordLib.dat
{
    
    public interface IOperate
    {
        Icon Symbol { get; }
        pNode Operate(pNode node);
    }
}
