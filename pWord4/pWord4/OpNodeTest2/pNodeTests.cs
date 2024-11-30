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
            Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
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
            Assert.AreEqual("RootValue", xmlDoc.DocumentElement.InnerText);

            var childNodes = xmlDoc.DocumentElement.ChildNodes;
            Assert.AreEqual(2, childNodes.Count);
            Assert.AreEqual("ChildNode1", childNodes[0].Name);
            Assert.AreEqual("ChildValue1", childNodes[0].InnerText);
            Assert.AreEqual("ChildNode2", childNodes[1].Name);
            Assert.AreEqual("ChildValue2", childNodes[1].InnerText);
        }

        // Add more tests as needed to cover different scenarios
    }
}
