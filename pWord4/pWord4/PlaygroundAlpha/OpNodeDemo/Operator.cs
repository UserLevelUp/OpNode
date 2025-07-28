using System;

namespace OpNodeDemo
{
    public abstract class Operator : IOperate
    {
        private bool changed = false;

        protected Operator()
        {
        }

        protected Operator(string symbol)
        {
            this.symbol = symbol;
        }

        #region IOperate Members

        public abstract PNode Operate(PNode pNode);

        /// <summary>
        /// Change is used to mark a node and all of its parents as having been changed in some measure where
        /// Only those nodes marked will need to be re-processed for IOperate if needed by their particular
        /// operation
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Change(PNode node)
        {
            // find all parent pNodes with IChange and mark them as changed as well
            var pNode = node.Parent;

            if (pNode != null) 
            {
                pNode.OperationChanged();
            }

            this.changed = true;
            Console.WriteLine($"Change: {node.Name}");
                
            return this.Changed;
        }

        public void ChangeFalse(PNode node)
        {
            this.changed = false;
        }

        private string symbol;
        public string Symbol
        {
            get { return symbol; }
        }

        public bool Changed { get { return this.changed; } }

        #endregion
    }
}