using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pWordLib.dat;
using System.Drawing;
using pWordLib.dat;
using System.Diagnostics;


namespace pWordLib.mgr
{
    [Serializable()]
    public abstract class Operator : IOperate
    {
        private bool changed = false;

        protected Operator()
        {
            //Symbol = new Icon(Icon, 
        }

        protected Operator(Icon symbol)
        {
            this.symbol = symbol;
        }

        #region IOperate Members

        abstract public pNode Operate(pNode _pNode);

		/// <summary>
        /// Change is used to mark a node and all of its parents as having been changed in some measure where
        /// Only those nodes marked will need to be re-processed for IOperate if needed by their particular
        /// operation
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Change(pNode node)
        {
            // find all parent pNodes with IChange and mark them as changed as well
            var _pNode = (pNode)node.Parent;  // this is a clever test to see if this parent node is an IOperate interface

            if (_pNode != null) {
                _pNode.OperationChanged();
            }

            this.changed = true;
            // TODO:  recursions infinity problem --->
            //node.OperationChanged();
            Debugger.Log(1, "test", $"Change: {node.Name}");
				
            return this.Changed ;
        }

		public void ChangeFalse(pNode node)
        {
            this.changed = false;
		}

        private Icon symbol;
        public Icon Symbol
        {
            get { return symbol; }
        }

        public bool Changed { get { return this.changed; } }

		#endregion

	}
}
