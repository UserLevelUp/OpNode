using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace myPword
{

    // I want to be able to go between the treenode in the treeview and the pNode which implements XmlNode
    // seemlessly.  The way to do this, is to also add an attribute control which is just a label for each 
    // attribute inside of the treeview site.  

    // I think the attributes may turn out to be the hardest part of this endeavor because it will need a 
    // pretty sweet interface.

    // Also I want to find a way to use the treenode to utilize xpath and xslt commands to easily parse data 
    // inside the tree node, and then quickly switch between any given treenode to xml utilizing the pNode.
    // this will be quite the endeavor

    // but I think this can be done quickly as long as it is created one step at a time.
    public class pNode : XmlDocument
    {

    }
}
