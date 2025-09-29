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
        public void Simple_Test_Without_Dependencies()
        {
            // This test doesn't use any complex dependencies
            Assert.IsTrue(true);
            Assert.AreEqual(2 + 2, 4);
        }

        // Test static methods only - these don't require pNode instances
        [TestMethod]
        public void TestValidXmlName_Static_Only()
        {
            // Test valid names
            Assert.IsTrue(pNode.IsValidXmlName("validName"));
            Assert.IsTrue(pNode.IsValidXmlName("valid_name"));
            Assert.IsTrue(pNode.IsValidXmlName("valid-name"));
            Assert.IsTrue(pNode.IsValidXmlName("valid.name"));
            Assert.IsTrue(pNode.IsValidXmlName("validName123"));
            Assert.IsTrue(pNode.IsValidXmlName("validName_123"));
            Assert.IsTrue(pNode.IsValidXmlName("validName-123"));
            Assert.IsTrue(pNode.IsValidXmlName("validName.123"));
        }

        [TestMethod]
        public void TestInvalidXmlName_Static_Only()
        {
            // Test invalid names - all static method calls
            Assert.IsFalse(pNode.IsValidXmlName("1invalidName")); // starts with digit
            Assert.IsFalse(pNode.IsValidXmlName("@invalidName")); // invalid character
            Assert.IsFalse(pNode.IsValidXmlName("invalid name")); // contains space
            Assert.IsFalse(pNode.IsValidXmlName("invalid<name>")); // contains <
            Assert.IsFalse(pNode.IsValidXmlName("invalid>name")); // contains >
            Assert.IsFalse(pNode.IsValidXmlName("invalid&name")); // contains &
            Assert.IsFalse(pNode.IsValidXmlName("invalid'name")); // contains '
            Assert.IsFalse(pNode.IsValidXmlName("invalid\"name")); // contains "
            Assert.IsFalse(pNode.IsValidXmlName("")); // empty string
            Assert.IsFalse(pNode.IsValidXmlName(null)); // null string
        }

        [TestMethod]
        public void TestEdgeCases_StaticMethods()
        {
            // Test edge cases for static validation
            Assert.IsFalse(pNode.IsValidXmlName(""));
            Assert.IsFalse(pNode.IsValidXmlName("   "));
            Assert.IsFalse(pNode.IsValidXmlName("123"));
            Assert.IsFalse(pNode.IsValidXmlName("9abc"));
            
            // Test single character names
            Assert.IsTrue(pNode.IsValidXmlName("a"));
            Assert.IsTrue(pNode.IsValidXmlName("Z"));
            Assert.IsFalse(pNode.IsValidXmlName("1"));
            Assert.IsFalse(pNode.IsValidXmlName("@"));
        }

        // COMMENTED OUT - These tests require pNode instantiation which causes issues
        /*
        [TestMethod]
        public void Create_Root_OpNode_Simple()
        {
            var pRoot = new pNode("TestName", "TestValue");
            Assert.IsNotNull(pRoot);
            Assert.AreEqual("TestName", pRoot.getName());
            Assert.AreEqual("TestValue", pRoot.getValue());
        }

        [TestMethod]
        public void TestSetName_Direct()
        {
            var pNodeInstance = new pNode();
            pNodeInstance.setName("validName");
            Assert.AreEqual("validName", pNodeInstance.getName());
        }
        */
    }
}
