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
using pWordLib.dat.math;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace pWordLib.dat
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
        public pNode() : base()
        {
            if (this.attributes == null)
            {
                this.attributes = new SortedList<string, string>();
            }
            if (this.operations == null)
            {
                this.operations = new List<IOperate>();
            }

        }

        //public pNode Parent {

        //    get {
        //        base.Parent;
        //        return this.Parent; 
        //    }

        // }

        
        protected pNode(SerializationInfo info, StreamingContext context) : this()
        {
            // my deserialization code ... don't forget that the info returns something
            // battled this for a little while hoping that the info would do it all on its own
            this.operations = (List<IOperate>)info.GetValue("operations", typeof(List<IOperate>));
            this.Namespace = (NameSpace)info.GetValue("namespace", typeof(NameSpace));
            this.attributes = (SortedList<String, String>)info.GetValue("attributes", typeof(SortedList<String, String>));
            base.Deserialize(info, context);
        }

        public String getXmlName()
        {
            DetectComplexNodeName();

            if ((base.Text == null) || (base.Text == ""))
            {
                return Guid.NewGuid().ToString();
            }
            else
            {
                // exceptions here
                // try and figure out why these cause the system to export so slowly
                //base.Text = base.Text.Replace(@"#", "");
                //base.Text = base.Text.Replace(@"/", "");
                //base.Text = base.Text.Replace(@"\", "");
                //base.Text = base.Text.Replace(@"@", "");
                //base.Text = base.Text.Replace(@"(", "");
                //base.Text = base.Text.Replace(@")", "");
                //base.Text = base.Text.Replace(@"*", "");
                //base.Text = base.Text.Replace(@"%", "");
                //base.Text = base.Text.Replace(@" ", "");
                //base.Text = base.Text.Replace(@"+", "");
                //base.Text = base.Text.Replace(@";", "");
                //base.Text = base.Text.Replace(@":", "");
                //base.Text = base.Text.Replace(@"&", "");
                if (base.Text == "")
                {
                    return Guid.NewGuid().ToString();
                }
            }
            return base.Text;
        }

        public pNode(String name) : this()
        {

            this.Name = name;
            this.Text = name;
        }

        public pNode(String name, int img1, int img2) : this()
        {

            this.Name = name;
            this.ImageIndex = img1;
            this.SelectedImageIndex = img2;
        }

        public NameSpace Namespace { get; set; }

        private SortedList<String, String> attributes = null;


        public IList<String> getKeys()
        {
            return attributes.Keys;
        }

        public String getValue(String key)
        {
            String value;
            if (attributes.TryGetValue(key, out value) == true)
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public void AddAttribute(String key, String value)
        {
            attributes.Add(key, value);
        }

        private List<IOperate> operations = null;

        public void AddOperation(IOperate operation)
        {
            this.ClearOperations();  //  clearing for now ...
                                     //  as more operations are added, make this optional
            operations.Add(operation);
            operation.Operate(this);
        }

        public String ListOperations()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IOperate operation in operations)
            {
                sb.Append(operation.GetType().ToString() + " ");
            }
            return sb.ToString();
        }

        public void PerformOperations()
        {
            if (operations == null)
            {
                return;
                }
            else
            {
                foreach (IOperate operation in operations)
                {
                    operation.Operate(this); // usually it performs the actual operation on its child elements, and stores the result in its tag field or value field.
                }
            }
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
            info.AddValue("operations", operations);
            info.AddValue("namespace", Namespace);
            info.AddValue("attributes", attributes);
            base.Serialize(info, context);
        }

        #endregion

        public void ClearOperations()
        {
            if (operations != null)
            {
                operations.Clear();
            }
        }

        public int OperationsCount()
        {
            if (operations != null)
            {
                return operations.Count;
            }
            else
            {
                return 0;
            }
        }

        public List<Icon> OperationIcons()
        {
            List<Icon> icons = new List<Icon>();
            foreach (IOperate operation in operations)
            {
                icons.Add(operation.Symbol);
            }
            return icons;
        }

        public String ErrorString { get; set; }

        public override object Clone()
        {
            pNode pClone = new pNode();

            pClone.Name = this.Name;
            pClone.Text = this.Text;
            pClone.Tag = this.Tag;

            pClone.operations = new List<IOperate>();
            foreach (IOperate operation in operations)
            {
                pClone.operations.Add(operation);
            }
            pClone.attributes = new SortedList<string, string>();
            foreach (KeyValuePair<string, string> attribute in attributes)
            {
                pClone.attributes.Add(attribute.Key, attribute.Value);
            }
            pClone.Namespace = (Namespace != null) ? (NameSpace)Namespace.Clone() : null;

            foreach(pNode pn in this.Nodes)
            {
                pClone.Nodes.Add((pNode)pn.Clone());
            }

            return pClone;
        }

        public void DetectComplexNodeName()
        {

            // get attributes from node.Text from a name
            // node.Text deterimne if node.Text is name followed by attr1="value1" attr2="value2" ...
            Regex rx = new Regex("(?<name>[A-z]{1,1}\\w*)\\s(\\s*(?<attr>[A-z]{1,1}\\w*)=['\"](?<value>\\w*)\\s*['\"])*", RegexOptions.Singleline);
            if (rx.IsMatch(this.Text))
            {
                MatchCollection mc = rx.Matches(this.Text);
                int matchCount = mc.Count;
                foreach (Match m in mc)
                {
                    // first get the name
                    Debug.WriteLine("{0} is group count.", m.Groups.Count);
                    this.Text = m.Groups["name"].ToString();
                    Regex rxattr = new Regex("(\\s*(?<attr>[A-z]{1,1}\\w*)=['\"](?<value>[^'\"]*)\\s*['\"])", RegexOptions.Multiline);
                    if (rxattr.IsMatch(m.Value))
                    {
                        // then get the attributes
                        MatchCollection mca = rxattr.Matches(m.Value);
                        int matchAttrCount = mca.Count;
                        foreach (Match ma in mca)
                        {
                            this.AddAttribute(ma.Groups["attr"].ToString(), ma.Groups["value"].ToString());
                            Debug.WriteLine("{0} is group count.", ma.Groups.Count);
                        }
                    }
                }
            }
        }

    }
}
