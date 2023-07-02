using System;
using System.Collections.Generic;
using System.Linq;
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
            this.Namespaces = (List<NameSpace>)info.GetValue("namespace", typeof(List<NameSpace>));
            this.attributes = (SortedList<String, String>)info.GetValue("attributes", typeof(SortedList<String, String>));
            base.Deserialize(info, context);
        }

		public bool HasChangedOperations()
		{
            return this.operations.Where(m => m.Changed == true).Count() > 0;
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

        public List<NameSpace> Namespaces { get; set; }

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

        /// <summary>
        /// Appears to be sluggish or slow for some reason
        /// </summary>
        public void PerformOperations()
        {
            if (operations == null)
            {
                return;
                }
            else
            {
                foreach (IOperate operation in operations) //.Where(m => m.Changed == true))
                {
                    operation.Operate(this); // usually it performs the actual operation on its child elements, and stores the result in its tag field or value field.
                    ((IChange)operation).ChangeFalse(this);
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
            info.AddValue("namespace", Namespaces);
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
            pClone.Namespaces = (Namespaces != null) ? (List<NameSpace>)Namespaces.ToList() : null;

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

        /// <summary>
        /// Find search text within the current node starting with the current node
        /// return the pNode that contains the text if any else add status that could not find 
        /// the pNode by return null
        /// </summary>
        /// <remarks>
        /// The behavior of this is that it should start to loop through all the pNodes starting with the parent pNode
        /// It should increment the index starting with 0 each time.   So if find or F3 is selected while on the prowl or find mode 
        /// it will remember the top node it stated and maybe even box it in a rectange.  It will just continue looping through each 
        /// index allong the chain of pNodes in the tree starting with the parent pNode that was selected when the process started.
        /// Since this function uses a bit of recursion it should not be expanding anything.  The calling functions should determine what gets expanded
        /// </remarks>
        /// <param name="searchText"></param>
        /// <returns>pNode</returns>
        public List<pNode> Find(string searchText, int index) {
            if (string.IsNullOrWhiteSpace(searchText)) {
                return null;
			}
            List<pNode> pNodes = new List<pNode>();
            if ((this.Text??"").Contains(searchText) || ((string)this.Tag??"").Contains(searchText) || this.attributes.ContainsKey(searchText) || this.attributes.ContainsValue(searchText) ) {
                pNodes.Add(this);
			}
            // maybe use a Dictionary<string searchInstance, List<pNode>> for each search context..
            foreach (var _pNodeInstance in this.Nodes)
            {
                index++;
                var _pNode = _pNodeInstance as pNode;
                pNodes.AddRange(_pNode.Find(searchText, 0));
            }
            return pNodes;            
		}
		
        public void OperationChanged() 
        {
            // this is called when the operation is changed if 1 or mor operatins exist on current node
            var ops = operations as List<IOperate>;
			if (ops != null && ops.Count > 0)
			{
                ops.FirstOrDefault().Change(this);
            }
			else 
			{
				// check that the node has a parent node
				if (this.Parent != null)
				{
                    if (((pNode)this.Parent).operations.Count() >= 1) {
                        ((pNode)this.Parent).operations[0].Change((pNode)this.Parent);
                    }
					else
					{
						// parent is not an IOperate so do nothing b/c we only care about parents that contain IOperate
					}
				}
			}
		}

        public XmlDocument CallRecursive(pNode node)
        {
            // MessageBox.Show("getnode:" + getNode.Name);
            // MessageBox.Show("pView top node:" + node.Text);
            // use c# 4.0 in a nut shell to construct XmlDocument for this xml stuff
            // page starts on 490

            // TODO: Move To pWordLib
            var xdoc = new XmlDocument();

            // TODO: Move To pWordLib
            xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", null, "yes"));

            // TODO: Move To pWordLib
            if (xdoc == null)  // instantiate the first time through
            {
                xdoc = new XmlDocument();
                System.Xml.NameTable nt = new NameTable();
                nt.Add(node.Text);
                XmlNameTable xnt = (XmlNameTable)nt;
                System.Xml.XmlNamespaceManager xnsm = new XmlNamespaceManager(xnt);
                node.Namespaces.ForEach(ns =>
                {
                    if (ns.Prefix != null)
                    {
                        xnsm.AddNamespace(ns.Prefix, ns.URI_PREFIX);  // prefix will be like 'xs', and url will be like 'http://www.url.com/etc/'
                    }
                    if (ns.Suffix != null)
                    {
                        xnsm.AddNamespace(ns.Suffix, ns.URI_SUFFIX);
                    }
                });
                // todo:  iterate through all nodes in pNode and place namespaces into the namespace manager
                // for now only do the first one if it exists

                // TODO: Move To pWordLib
                xdoc = new XmlDocument(xnt);

                // TODO: Move To pWordLib
                xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", null, "yes"));
                //foreach (String key in xnsm.GetNamespacesInScope(XmlNamespaceScope.All).Keys)
                //{
                //    // this inserts the namespace into the xdoc from the name space manager
                //    //xdoc.Schemas.XmlResolver resolve = 
                //    //xdoc.Schemas.Add(key, xnsm.LookupNamespace(key));
                //}
            }

            // TODO: Move To pWordLib
            node.getXmlName();  // fix node attributes and todo: eventually namespaces

            // TODO: Move To pWordLib
            XmlNode rootNode = xdoc.CreateElement(node.getXmlName());
            rootNode.InnerText = (String)node.Tag;

            // TODO: Move To pWordLib
            foreach (var attrKey in node.getKeys())
            {

                var val = node.getValue(attrKey);

                //

                XmlAttribute xmlAttribute = null;
                if (val != null)
                {
                    xmlAttribute = xdoc.CreateAttribute(attrKey);
                    xmlAttribute.Value = val;
                    rootNode.Attributes.Append(xmlAttribute);
                }
                //rootNode.Attributes.Append(new XmlAttribute(attrKey, val));

            }

            // TODO: Move To pWordLib
            if (node.Namespaces != null)
            {
                //rootNode = xdoc.CreateNode(XmlNodeType.Element, node.Namespace.Prefix, node.Name, node.Namespace.URI_PREFIX);
            }
            else
            {
                // this takes a long time as well
                // see if we can't use an existing node and clone it
                //rootNode = xdoc.CreateNode(XmlNodeType.Element, node.getXmlName(), "");  // any time getXmlName is called ... 
                // it should be necessary to now recheck to see if its got attributes at this current node
            }

            // TODO: Move To pWordLib
            foreach (String key in node.getKeys())
            {
                if (key != "")
                {
                    node.Namespaces.ForEach(ns =>
                    {
                        XmlNode attr = xdoc.CreateNode(XmlNodeType.Attribute, ns.Prefix, key, ns.URI_PREFIX);
                        attr.Value = node.getValue(key);
                        rootNode.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)

                        if (ns != null)
                        {
                            if (ns.Prefix != null)
                            {
                                attr = xdoc.CreateNode(XmlNodeType.Attribute, key, ns.URI_PREFIX);
                            }
                            else
                            {
                                attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                            }
                        }
                        else
                        {
                            attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                        }
                        attr.Value = node.getValue(key);
                        rootNode.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)

                    });
                }
            }

            // TODO: Move To pWordLib
            foreach (pNode p in node.Nodes)
            {
                p.getXmlName();  // fix any attributes (this has been completed in pNode.DetectComplexNodeName()
                                 // todo: fix any namespace ... 
            }

            // TODO: Move To pWordLib
            XmlNode xn;

            foreach (pNode p in node.Nodes)
            {
                p.Namespaces.ForEach(ns =>
                {
                    if (ns.Prefix != null)
                    {
                        xn = xdoc.CreateNode(XmlNodeType.Element, ns.Prefix, p.Text, ns.URI_PREFIX);
                    }
                    else
                    {
                        // this takes a long time as well
                        // see if we can't use an existing node and clone it
                        xn = xdoc.CreateNode(XmlNodeType.Element, p.getXmlName(), "");  // any time getXmlName is called ... 
                        xn.InnerText = (String)p.Tag;
                    }

                    foreach (String key in p.getKeys())
                    {
                        if (ns != null)
                        {
                            if (ns.Prefix != null)
                            {
                                XmlNode attr = xdoc.CreateNode(XmlNodeType.Attribute, ns.Prefix, key, ns.URI_PREFIX);
                                attr.Value = p.getValue(key);
                                xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                            }
                            else
                            {
                                XmlNode attr;
                                attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                                attr.Value = p.getValue(key);
                                xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                            }
                        }
                        else
                        {
                            XmlNode attr;
                            if (ns != null)
                            {
                                if (ns.Prefix != null)
                                {
                                    attr = xdoc.CreateNode(XmlNodeType.Attribute, key, ns.URI_PREFIX);
                                }
                                else
                                {
                                    attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                                }
                            }
                            else
                            {
                                attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                            }
                            attr.Value = p.getValue(key);
                            xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)

                            // TODO: Move To pWordLib
                            rootNode.AppendChild(RecursiveChildren(ref xn, p.Nodes, xdoc));

                        }
                    }

                });


            }
            // TODO: Move To pWordLib
            xdoc.AppendChild(rootNode);
            return xdoc;
        }

        private XmlNode RecursiveChildren(ref XmlNode node, TreeNodeCollection pNodes, XmlDocument xdoc)
        {
            // TODO: Move To pWordLib
            foreach (pNode p in pNodes)
            {
                p.getXmlName();
            }

            // TODO: Move To pWordLib
            foreach (pNode p in pNodes)
            {
                // TODO: Move To pWordLib
                XmlNode xn;
                p.Namespaces.ForEach(ns =>
                {
                    // change any p.Name to be text only
                    xn = xdoc.CreateNode(XmlNodeType.Element, ns.Prefix, p.getXmlName(), ns.URI_PREFIX);
                    // TODO: Move To pWordLib
                    xn.InnerText = (String)p.Tag;


                    // TODO: Move To pWordLib
                    foreach (String key in p.getKeys())
                    {
                        // TODO: Move To pWordLib
                        System.Xml.NameTable nt = new NameTable();
                        nt.Add(p.Text);

                        // TODO: Move To pWordLib
                        XmlNameTable xnt = (XmlNameTable)nt;
                        System.Xml.XmlNamespaceManager xnsm = new XmlNamespaceManager(xnt);
                        if (ns.Prefix != null)
                        {
                            xnsm.AddNamespace(ns.Prefix, ns.URI_PREFIX);  // prefix will be like 'xs', and url will be like 'http://www.url.com/etc/'
                        }
                        if (ns.Suffix != null)
                        {
                            xnsm.AddNamespace(ns.Suffix, ns.URI_SUFFIX);
                        }

                        // TODO: Move To pWordLib

                        if (ns.Prefix != null)
                        {
                            XmlNode attr = xdoc.CreateNode(XmlNodeType.Attribute, ns.Prefix, ns.URI_PREFIX);
                            attr.Value = p.getValue(key);
                            xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                        }
                        else
                        {
                            XmlNode attr;
                            attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                        }
                    }

                    // TODO: Move To pWordLib
                    xn.AppendChild(RecursiveChildren(ref xn, p.Nodes, xdoc));


                });

            }

            // TODO: fix reference to node?   Don't understand what I'm doing with this???
            return node;
        }



    }

    public enum nodeMode
    {
        addto = 1,
        insert = 2,
        edit = 3,
        addAttributeTo = 4,
        addNamespacePrefix = 5,
        addNamespaceSuffix = 6,
        sum = 7,
        multiply = 8,
        divide = 9,
        viewErrors = 10,
        cut = 11,
        trig,
        find,
        xmlUpdate,
        search
    }

}
