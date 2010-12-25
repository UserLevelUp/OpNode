﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Resolvers;
using pWordLib.dat;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Diagnostics;
using pWordLib.dat.math;
using System.Drawing;
using System.ComponentModel;
using pWordLib.mgr;

namespace myPword.dat.Math
{
    // I want to be able to go between the treenode in the treeview and the pNode which implements XmlNode
    // and XmlDocument seemlessly.  The way to do this, is to also add an attribute control which is just a label for each 
    // attribute inside of the TreeNode structor.  Implementing this into the pNode implementation.

    // I think the attributes may turn out to be the hardest part of this endeavor because it will need a 
    // pretty sweet interface.

    // Also I want to find a way to use the treenode to utilize xpath and xslt commands to easily parse data 
    // inside the tree node, and then quickly switch between any given treenode to xml utilizing the pNode.
    // this will be quite the endeavor

    // but I think this can be done quickly as long as it is created one step at a time.

    /// <summary>
    /// A Summation of All children of the selected node 
    /// The total goes in the SelectedNode's value field
    /// </summary>
    [Serializable()]
    public class Subtract : Operator
    {

        public Subtract()
        {
            //Symbol = new Icon(Icon, 
        }

        public Subtract(Icon symbol) : base(symbol)
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
                decimal num = 0.0M;
                // attempt to convert to decimal
                if (Decimal.TryParse((String)node.Tag, out num))
                {
                    total -= num; // perform the basic summation
                }
                else
                {
                    Debug.WriteLine("A Node failed to Sum");
                }

                //note: eventially I want to add advanced summation on (n^2+n)/2 with i=1 etc... but for now it just totallys up the values
            }
            _pNode.Tag = total.ToString();
            return _pNode;  // not yet implemented
        }



        #endregion
    }
}
