using Microsoft.VisualStudio.TestTools.UnitTesting;
using pWordLib.dat;
using System.Xml;

namespace pWordTests
{
    [TestClass]
    public class pNodeTests
    {
        [TestMethod]
        public void TestCallRecursive_SingleNode()
        {
            // Arrange
            var rootNode = new pNode("RootNode", "RootValue");

            // Act
            var xmlDoc = rootNode.CallRecursive(rootNode);

            // Assert
            Assert.IsNotNull(xmlDoc);
            Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name); // assuming string
            Assert.AreEqual("RootValue", xmlDoc.DocumentElement.InnerText);
        }

        [TestMethod]
        public void TestCallRecursive_MultipleNodes()
        {
            // Arrange
            var rootNode = new pNode("RootNode", "RootValue");
            var childNode1 = new pNode("ChildNode1", "ChildValue1");
            var childNode2 = new pNode("ChildNode2", "ChildValue2");

            rootNode.Nodes.Add(childNode1);
            rootNode.Nodes.Add(childNode2);

            // Act
            var xmlDoc = rootNode.CallRecursive(rootNode);

            // Assert
            Assert.IsNotNull(xmlDoc);
            Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
            
            // In XML, when a node has children, InnerText includes all descendant text content
            // This is expected XML behavior - the root's InnerText includes child text
            Assert.AreEqual("RootValueChildValue1ChildValue2", xmlDoc.DocumentElement.InnerText);

            // The issue: ChildNodes includes ALL nodes (text nodes + element nodes)
            // We need to count only element nodes, not text nodes
            var elementNodes = new System.Collections.Generic.List<XmlNode>();
            foreach (XmlNode child in xmlDoc.DocumentElement.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element)
                {
                    elementNodes.Add(child);
                }
            }
            
            Assert.AreEqual(2, elementNodes.Count);
            Assert.AreEqual("ChildNode1", elementNodes[0].Name);
            Assert.AreEqual("ChildValue1", elementNodes[0].InnerText);
            Assert.AreEqual("ChildNode2", elementNodes[1].Name);
            Assert.AreEqual("ChildValue2", elementNodes[1].InnerText);
        }

        // Add more tests as needed to cover different scenarios
    }
}
