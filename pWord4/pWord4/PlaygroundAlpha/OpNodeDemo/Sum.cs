using System;

namespace OpNodeDemo
{
    /// <summary>
    /// A Summation of All children of the selected node 
    /// The total goes in the SelectedNode's value field
    /// </summary>
    public class Sum : Operator
    {
        public Sum() 
        {
        }

        public Sum(string symbol) : base(symbol)
        {
        }

        #region IOperate Members

        public override PNode Operate(PNode pNode)
        {
            pNode.ErrorString = "";
            // perform a summation on only child pNode elements
            // i.e.  this.Tag = total.ToString();
            decimal total = 0.0M;  // start off with 0
            
            if (pNode.HasChangedOperations()) 
            {
                foreach (PNode node in pNode.Children)
                {
                    node.PerformOperations();  // if there are no operations it will assume this is not an operator and treat it only as a value field
                    decimal num = 0.0M;
                    // attempt to convert to decimal
                    if (Decimal.TryParse(node.Value, out num))
                    {
                        try
                        {
                            total += num; // perform the basic summation
                        }
                        catch (OverflowException ex)
                        {
                            // overflow occurred... can't go any higher
                            total = Decimal.MaxValue;
                            pNode.ErrorString = "OverflowException occurred. " + ex.ToString();
                        }
                        catch (Exception ex)
                        {
                            // max value reached
                            pNode.ErrorString = "Unknown problem occurred. " + ex.ToString();
                        }
                    }
                    else
                    {
                        pNode.ErrorString = "A Node failed to Sum";
                        Console.WriteLine("A Node failed to Sum");
                    }
                }
                pNode.Value = total.ToString();
            }
            else
            {
                // else do nothing because what is in value shouldn't have to change as nothing changed
            }
            return pNode;
        }

        #endregion
    }
}