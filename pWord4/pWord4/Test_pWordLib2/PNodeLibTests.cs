using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using pWordLib.dat;

namespace Test_pWordLib2
{
    [TestClass]
    public class PNodeLibTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

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
            Assert.AreEqual("RootValueChildValue1ChildValue2", xmlDoc.DocumentElement.InnerText);

            var childNodes = xmlDoc.DocumentElement.ChildNodes;
            Assert.AreEqual(3, childNodes.Count);
            Assert.AreEqual("ChildValue1", childNodes[1].Name);
            Assert.AreEqual("ChildValue1", childNodes[1].InnerText);
            Assert.AreEqual("ChildValue2", childNodes[2].Name);
            Assert.AreEqual("ChildValue2", childNodes[2].InnerText);
        }

        // Test valid XML names
        [TestMethod]
        public void TestValidXmlName()
        {
            Assert.IsTrue(pNode.IsValidXmlName("validName"));
            Assert.IsTrue(pNode.IsValidXmlName("valid_name"));
            Assert.IsTrue(pNode.IsValidXmlName("valid-name"));
            Assert.IsTrue(pNode.IsValidXmlName("valid.name"));
            Assert.IsTrue(pNode.IsValidXmlName("validName123"));
            Assert.IsTrue(pNode.IsValidXmlName("validName_123"));
            Assert.IsTrue(pNode.IsValidXmlName("validName-123"));
            Assert.IsTrue(pNode.IsValidXmlName("validName.123"));
            Assert.IsTrue(pNode.IsValidXmlName("ns:validName")); // Valid namespace prefix
        }

        // Test invalid XML names
        [TestMethod]
        public void TestInvalidXmlName()
        {
            // Arrange
            var pNode = new pNode();

            // Act & Assert
            Assert.IsFalse(pNode.IsValidXmlName("1invalidName"));
            Assert.IsFalse(pNode.IsValidXmlName("@invalidName"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid<name>"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid>name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid&name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid'name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid\"name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid/name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid\\name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid:name")); // Invalid namespace prefix
            Assert.IsFalse(pNode.IsValidXmlName("invalid;name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid,name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid?name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid!name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid@name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid#name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid$name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid%name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid^name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid*name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid(name)"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid+name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid,name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid=name"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid{name}"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid[name]"));
            Assert.IsFalse(pNode.IsValidXmlName("invalid|name"));
        }

        [TestMethod]
        public void TestCallRecursive_EmptyNode()
        {
            // Arrange
            var pRoot = new pNode();
            pRoot.setName("RootNode");

            // Act
            XmlDocument xmlDoc = pRoot.CallRecursive(pRoot);

            // Assert
            Assert.IsNotNull(xmlDoc);
            Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
            Assert.AreEqual(0, xmlDoc.DocumentElement.Attributes.Count);
            Assert.AreEqual(1, xmlDoc.DocumentElement.ChildNodes.Count);
        }

        [TestMethod]
        public void TestCallRecursive_NodeWithAttributes()
        {
            // Arrange
            var pRoot = new pNode();
            pRoot.setName("RootNode");
            pRoot.setValue("RootValue");
            pRoot.AddAttribute("attr1", "value1");
            pRoot.AddAttribute("attr2", "value2");

            // Act
            XmlDocument xmlDoc = pRoot.CallRecursive(pRoot);

            // Assert
            Assert.IsNotNull(xmlDoc);
            Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
            Assert.AreEqual("value1", xmlDoc.DocumentElement.Attributes["attr1"].Value);
            Assert.AreEqual("value2", xmlDoc.DocumentElement.Attributes["attr2"].Value);
        }

        [TestMethod]
        public void TestCallRecursive_NodeWithChildren()
        {
            // Arrange
            var pRoot = new pNode();
            pRoot.setName("RootNode");

            var pChild = new pNode();
            pChild.setName("ChildNode");
            pChild.setValue("ChildValue");
            pRoot.Nodes.Add(pChild);

            // Act
            XmlDocument xmlDoc = pRoot.CallRecursive(pRoot);

            // Assert
            Assert.IsNotNull(xmlDoc);
            Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
            Assert.AreEqual(2, xmlDoc.DocumentElement.ChildNodes.Count);
            Assert.AreEqual(string.Empty, xmlDoc.DocumentElement.ChildNodes[0].Value);
            Assert.AreEqual("ChildValue", xmlDoc.DocumentElement.ChildNodes[1].Name);
            Assert.AreEqual(string.Empty, xmlDoc.DocumentElement.ChildNodes[1].InnerText.Trim());
        }

        [TestMethod]
        public void TestCallRecursive_ComplexNodeStructure()
        {
            // Arrange
            var pRoot = new pNode();
            pRoot.setName("RootNode");
            pRoot.AddAttribute("rootAttr", "rootValue");

            var pChild1 = new pNode();
            pChild1.setName("ChildNode1");
            pChild1.AddAttribute("child1Attr", "child1Value");

            var pChild2 = new pNode();
            pChild2.setName("ChildNode2");
            pChild2.AddAttribute("child2Attr", "child2Value");

            pRoot.Nodes.Add(pChild1);
            pRoot.Nodes.Add(pChild2);

            // Act
            XmlDocument xmlDoc = pRoot.CallRecursive(pRoot);

            // Assert
            Assert.IsNotNull(xmlDoc);
            Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
            Assert.AreEqual("rootValue", xmlDoc.DocumentElement.Attributes["rootAttr"].Value);
            Assert.AreEqual(3, xmlDoc.DocumentElement.ChildNodes.Count);
            Assert.AreEqual(string.Empty, xmlDoc.DocumentElement.ChildNodes[0].Value);

            var firstChild = xmlDoc.DocumentElement.ChildNodes[1];
            Assert.AreEqual("child1Value", firstChild.Attributes["child1Attr"].Value);

            var secondChild = xmlDoc.DocumentElement.ChildNodes[2];
            Assert.AreEqual("child2Value", secondChild.Attributes["child2Attr"].Value);
        }

        [TestMethod]
        public void TestRemoveChildNode()
        {
            // TODO: Implement this test
            // 1. Create a root pNode and a child pNode.
            // 2. Add the child to the root's Nodes collection.
            // 3. Verify that the child was added.
            // 4. Call a method on the root node to remove the child (e.g., root.Nodes.Remove(child)).
            // 5. Assert that the child is no longer in the root's Nodes collection.
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void TestRemoveAttribute()
        {
            // TODO: Implement this test
            // 1. Create a pNode and add an attribute to it.
            // 2. Verify that the attribute was added.
            // 3. Call a method on the pNode to remove the attribute.
            // 4. Assert that the attribute is no longer present on the pNode.
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void TestFindChildNode()
        {
            // TODO: Implement this test
            // 1. Create a root pNode and several child pNodes with different names.
            // 2. Add the children to the root's Nodes collection.
            // 3. Call a method on the root node to find a specific child by name.
            // 4. Assert that the correct child pNode is returned.
            // 5. Assert that null or an empty collection is returned when searching for a non-existent child.
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void TestCloneNode()
        {
            // TODO: Implement this test
            // 1. Create a pNode with children and attributes.
            // 2. Call a method on the pNode to clone it.
            // 3. Assert that the cloned node is not the same instance as the original.
            // 4. Assert that the cloned node has the same name, value, attributes, and children as the original.
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void TestNodeValueTypes()
        {
            // TODO: Implement this test
            // 1. Create pNodes with different value types (e.g., int, double, bool).
            // 2. Verify that the value can be set and retrieved with the correct type.
            // 3. Consider how different types are handled when exporting to XML (e.g., are they all converted to strings?).
            Assert.Inconclusive("Test not implemented.");
        }
    }
}
