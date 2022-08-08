using pWordLib.mgr;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Resolvers;
using pWordLib.dat;
using System.Runtime.Serialization;
using System.Security.Permissions;
using pWordLib.dat.math;
using System.ComponentModel;


namespace pWordLib.dat.Behavior
{
    [Serializable()]
	public class Dirty : Operator
    {
	    public Dirty() 
		{
			//Symbol = new Icon(Icon, 
		}

        public Dirty(Icon symbol) : base(symbol)
        {

        }
		
		public Dictionary<pNode, bool> isDirtyDict { get; set; } = new Dictionary<pNode, bool>();

        #region IOperate Members

        public override pNode Operate(pNode _pNode)
        {
            _pNode.ErrorString = "";

            return _pNode;  // not yet implemented
        }



        #endregion


    }
}


