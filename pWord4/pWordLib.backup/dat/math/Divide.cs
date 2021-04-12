using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pWordLib.dat;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using pWordLib.mgr;


namespace pWordLib.dat.math
{
    [Serializable()]
    public class Divide : Operator
    {
        public Divide(Icon symbol) : base(symbol)
        {

        }

        #region IOperate Members

        public override pNode Operate(pNode _pNode)
        {
            // perform a summation on only child pNode elements
            // i.e.  this.Tag = total.ToString();
            decimal total = 0.0M;  // start off with 0
            int index = 0;
            foreach (pNode node in _pNode.Nodes)
            {
                node.PerformOperations();  // if there are no operations it will assume this is not an operator and treat it only as a value field

                // first time around get the total
                decimal num = 0.0M;
                if (index++ == 0)
                {
                    if (Decimal.TryParse((String)node.Tag, out total))
                    {
                        continue;
                    }
                    else
                    {
                        _pNode.Tag = "Undefined";
                        continue;
                    }
                }

                // note: to make this a little more clear, if it is an operations field this current pNode has child nodes under it, it
                // will then process all child nodes under it based on whatever type of operaiton it is performing

                // attempt to convert to decimal and place it in num and perform the multiplication operation
                if (Decimal.TryParse((String)node.Tag, out num))
                {
                    _pNode.ErrorString = "";
                    if (num == 0)
                    {
                        _pNode.Tag = "Infinity";
                        return _pNode;
                    }

                    try
                    {
                        total /= num; // perform the basic division; 
                    }
                    catch (OverflowException ex)
                    {
                        // overflow occurred... can't go any hire
                        total = Decimal.MaxValue;
                        _pNode.ErrorString = "OverflowException occurred. " + ex.ToString();
                    }
                    catch (DivideByZeroException ex)
                    {
                        // overflow occurred... can't go any hire
                        total = Decimal.MaxValue;
                        _pNode.ErrorString = "DivideByZeroException occurred. " + ex.ToString();
                    }
                    catch (Exception ex)
                    {
                        // max value reached
                        _pNode.ErrorString = "Unknown problem occurred. " + ex.ToString();
                    }
                    
                }
                else
                {
                    _pNode.ErrorString = "A Node failed to Divide.";
                    Debug.WriteLine("A Node failed to Divide.");
                }

                //note: eventially I want to add advanced summation on (n^2+n)/2 with i=1 etc... but for now it just totallys up the values
            }
            _pNode.Tag = total.ToString();
            return _pNode;  // not yet implemented
        }

        #endregion
    }
}

