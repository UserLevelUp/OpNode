using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using pWordLib.dat;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace OpNodeTest2
{
    [TestClass]
    public class UnitTest_BasicNode_ops
    {
        [TestMethod]
        public void Create_Root_OpNode()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>();
            Assert.IsNotNull(pRoot);
        }

        // assert after set name and value to root
        [TestMethod]
        public void Set_Root_Name()
        {
            var pRoot = new Mock<TestablePNode>();
            pRoot.CallBase = true; // This will call the real implementation
            pRoot.Object.SetNameForTesting("MyName");
            Assert.AreEqual("MyName", pRoot.Object.GetNameForTesting());
        }

        // when creating root set both name and value
        [TestMethod]
        public void Create_Root_Name_Value()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>("MyName", "MyValue");
            pRoot.CallBase = true;
            Assert.AreEqual("MyName", pRoot.Object.getName());
            Assert.AreEqual("MyValue", pRoot.Object.getValue());
        }

        // when creating root set both name and object value
        [TestMethod]
        public void Create_Root_Name_Object_Value()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>("MyName", 123);
            pRoot.CallBase = true;
            Assert.AreEqual("MyName", pRoot.Object.getName());
            Assert.AreEqual(123, pRoot.Object.getValueObject());
        }

        // assert after set value to an object not a string
        [TestMethod]
        public void Set_Root_Value()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>();
            pRoot.CallBase = true;
            pRoot.Object.setValueObject(123);
            Assert.AreEqual(123, pRoot.Object.getValueObject());
        }

        // assert after set name and value to a child of root
        [TestMethod]
        public void Set_Child_Name_Value()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>();
            var pChild = new Mock<pWordLib.dat.pNode>();
            pRoot.CallBase = true;
            pChild.CallBase = true;
            pRoot.Object.setName("MyName");
            pChild.Object.setName("MyChild");
            pChild.Object.setValue("MyValue");
            pRoot.Object.Nodes.Add(pChild.Object);
            Assert.AreEqual("MyChild", pRoot.Object.getChild(0).getName());
            Assert.AreEqual("MyValue", pRoot.Object.getChild(0).getValue());
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

        // Test setName with valid and invalid names
        [TestMethod]
        public void TestSetName()
        {
            var pNodeInstance = new TestablePNode();

            // Valid name
            pNodeInstance.SetNameForTesting("validName");
            Assert.AreEqual("validName", pNodeInstance.GetNameForTesting());

            // Invalid name
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => pNodeInstance.SetNameForTesting("1invalidName"));
        }

        //[TestMethod]
        //public void TestExportToXml()
        //{
        //    // Arrange
        //    var pRoot = new Mock<pWordLib.dat.pNode>();
        //    pRoot.CallBase = true;
        //    pRoot.Object.setName("RootNode");
        //    pRoot.Object.setValue("RootValue");
        //    pRoot.Object.AddAttribute("attr1", "value1");
        //    pRoot.Object.AddAttribute("attr2", "value2");

        //    var pChild = new Mock<pWordLib.dat.pNode>();
        //    pChild.CallBase = true;
        //    pChild.Object.setName("ChildNode");
        //    pChild.Object.setValue("ChildValue");
        //    pRoot.Object.Nodes.Add(pChild.Object);

        //    // Act
        //    XmlDocument xmlDoc = pRoot.Object.CallRecursive(pRoot.Object);

        //    // Assert
        //    Assert.IsNotNull(xmlDoc);
        //    Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
        //    Assert.AreEqual("RootValue", xmlDoc.DocumentElement.InnerText);
        //    Assert.AreEqual("value1", xmlDoc.DocumentElement.Attributes["attr1"].Value);
        //    Assert.AreEqual("value2", xmlDoc.DocumentElement.Attributes["attr2"].Value);
        //    Assert.AreEqual("ChildNode", xmlDoc.DocumentElement.FirstChild.Name);
        //    Assert.AreEqual("ChildValue", xmlDoc.DocumentElement.FirstChild.InnerText);
        //}
        //[TestMethod]
        //public void TestCallRecursive_EmptyNode()
        //{
        //    // Arrange
        //    var pRoot = new pNode();
        //    pRoot.setName("RootNode");

        //    // Act
        //    XmlDocument xmlDoc = pRoot.CallRecursive(pRoot);

        //    // Assert
        //    Assert.IsNotNull(xmlDoc);
        //    Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
        //    Assert.AreEqual(0, xmlDoc.DocumentElement.Attributes.Count);
        //    Assert.AreEqual(0, xmlDoc.DocumentElement.ChildNodes.Count);
        //}
        //[TestMethod]
        //public void TestCallRecursive_NodeWithAttributes()
        //{
        //    // Arrange
        //    var pRoot = new pNode();
        //    pRoot.setName("RootNode");
        //    pRoot.setValue("RootValue");
        //    pRoot.AddAttribute("attr1", "value1");
        //    pRoot.AddAttribute("attr2", "value2");

        //    // Act
        //    XmlDocument xmlDoc = pRoot.CallRecursive(pRoot);

        //    // Assert
        //    Assert.IsNotNull(xmlDoc);
        //    Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
        //    Assert.AreEqual("value1", xmlDoc.DocumentElement.Attributes["attr1"].Value);
        //    Assert.AreEqual("value2", xmlDoc.DocumentElement.Attributes["attr2"].Value);
        //}
        //[TestMethod]
        //public void TestCallRecursive_NodeWithChildren()
        //{
        //    // Arrange
        //    var pRoot = new pNode();
        //    pRoot.setName("RootNode");

        //    var pChild = new pNode();
        //    pChild.setName("ChildNode");
        //    pChild.setValue("ChildValue");
        //    pRoot.Nodes.Add(pChild);

        //    // Act
        //    XmlDocument xmlDoc = pRoot.CallRecursive(pRoot);

        //    // Assert
        //    Assert.IsNotNull(xmlDoc);
        //    Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
        //    Assert.AreEqual(1, xmlDoc.DocumentElement.ChildNodes.Count);
        //    Assert.AreEqual("ChildNode", xmlDoc.DocumentElement.FirstChild.Name);
        //    Assert.AreEqual("ChildValue", xmlDoc.DocumentElement.FirstChild.InnerText.Trim());
        //}
        //[TestMethod]
        //public void TestCallRecursive_ComplexNodeStructure()
        //{
        //    // Arrange
        //    var pRoot = new pNode();
        //    pRoot.setName("RootNode");
        //    pRoot.AddAttribute("rootAttr", "rootValue");

        //    var pChild1 = new pNode();
        //    pChild1.setName("ChildNode1");
        //    pChild1.AddAttribute("child1Attr", "child1Value");

        //    var pChild2 = new pNode();
        //    pChild2.setName("ChildNode2");
        //    pChild2.AddAttribute("child2Attr", "child2Value");

        //    pRoot.Nodes.Add(pChild1);
        //    pRoot.Nodes.Add(pChild2);

        //    // Act
        //    XmlDocument xmlDoc = pRoot.CallRecursive(pRoot);

        //    // Assert
        //    Assert.IsNotNull(xmlDoc);
        //    Assert.AreEqual("RootNode", xmlDoc.DocumentElement.Name);
        //    Assert.AreEqual("rootValue", xmlDoc.DocumentElement.Attributes["rootAttr"].Value);
        //    Assert.AreEqual(2, xmlDoc.DocumentElement.ChildNodes.Count);

        //    var firstChild = xmlDoc.DocumentElement.ChildNodes[0];
        //    Assert.AreEqual("ChildNode1", firstChild.Name);
        //    Assert.AreEqual("child1Value", firstChild.Attributes["child1Attr"].Value);

        //    var secondChild = xmlDoc.DocumentElement.ChildNodes[1];
        //    Assert.AreEqual("ChildNode2", secondChild.Name);
        //    Assert.AreEqual("child2Value", secondChild.Attributes["child2Attr"].Value);
        //}



    }
}
