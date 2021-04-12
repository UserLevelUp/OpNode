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
    public class Sin : Operator
    {
        public Sin(Icon symbol) : base(symbol)
        {

        }

        #region IOperate Members

        public override pNode Operate(pNode _pNode)
        {
            // perform a summation on only child pNode elements
            // i.e.  this.Tag = total.ToString();
            double total = 0.0;  // start off with 0
            int index = 0;
            foreach (pNode node in _pNode.Nodes)
            {
                double number = 0.0;
                node.PerformOperations();  // if there are no operations it will assume this is not an operator and treat it only as a value field

                // first time around get the total
                
                if (index++ == 0) // first node just perform the sin funciton on the first child member convert to radians... ie ... 3.141.. ~= 180 degrees and any multiple thereof...
                {
                    if (Double.TryParse((String)node.Tag, out number))
                    {
                        total = System.Math.Sin(number);
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
                else if (Double.TryParse((String)node.Tag, out number))
                {
                    _pNode.ErrorString = "";

                    try
                    {
                        //total = total num; // perform the sin function;   //  for each additional child multiple the previous sin to the current sin
                        total = total * System.Math.Sin(number);                      
                    }
                    catch (OverflowException ex)
                    {
                        // overflow occurred... can't go any hire
                        total = Double.MaxValue;
                        _pNode.ErrorString = "OverflowException occurred. " + ex.ToString();
                    }
                    catch (Exception ex)
                    {
                        // max value reached
                        _pNode.ErrorString = "Unknown problem occurred. " + ex.ToString();
                    }
                    
                }
                else
                {
                    _pNode.ErrorString = "A Node failed Sin.";
                    Debug.WriteLine("A Node failed to Sin.");
                }

                //note: eventially I want to add advanced summation on (n^2+n)/2 with i=1 etc... but for now it just totallys up the values
            }
            _pNode.Tag = total.ToString();
            return _pNode;  // not yet implemented
        }

        #endregion
    }
}

