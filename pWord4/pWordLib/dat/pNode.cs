using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Resolvers;
using pWordLib.dat;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace myPword
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
    [Serializable()]
    public class pNode : TreeNode, ISerializable
    {
        public pNode()
        {
            
        }

        public pNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // my deserialization code
            info.FullTypeName = "pNode";
        }
        

        public pNode(String name)
        {
            this.Name = name;
            this.Text = name;
        }

        public pNode(String name, int img1, int img2)
        {
            this.Name = name;
            this.ImageIndex = img1;
            this.SelectedImageIndex = img2;
        }

        public NameSpace Namespace { get; set; }

        private SortedList<String, String> attributes = new SortedList<string, string>();

        public void AddAttribute(String key, String value)
        {
            attributes.Add(key, value);
        }

        public void DeleteAttribute(String key)
        {
            attributes.Remove(key);
        }

        // Recursive class to convert a TreeNode into a pNode
        // Don't have to worry about attributes or namespaces because going from a treeNode to a pNode
        // if I was going the other way I would have to worry about attr and namespaces
        public static pNode TreeNode2pNode(TreeNode treeNode)
        {
            // use a form of recursion to delve into the bowels of the treeNode and construct a pNode
            pNode pNodeA = new pNode(treeNode.Name, treeNode.ImageIndex, treeNode.SelectedImageIndex);
            pNodeA.Text = treeNode.Text;
            pNodeA.Tag = treeNode.Tag;
            foreach (TreeNode tni in treeNode.Nodes)
            {
                pNodeA.Nodes.Add(TreeNode2pNode(tni));
            }
            return pNodeA;
        }

        public static TreeNode pNode2TreeNode(pNode _pNode)
        {

            return null;
        }



        #region ISerializable Members
 
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.Serialize(info, context);
        }

        #endregion
    }
}
