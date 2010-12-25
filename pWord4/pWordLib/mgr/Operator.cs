using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pWordLib.dat;
using System.Drawing;
using myPword.dat;
using System.Diagnostics;

namespace pWordLib.mgr
{
    [Serializable()]
    public abstract class Operator : IOperate
    {

        public Operator()
        {
            //Symbol = new Icon(Icon, 
        }

        public Operator(Icon symbol)
        {
            this.symbol = symbol;
        }

        #region IOperate Members

        abstract public pNode Operate(pNode _pNode);

        private Icon symbol;
        public  Icon Symbol
        {
            get { return symbol; }
        }

        #endregion

    }
}
