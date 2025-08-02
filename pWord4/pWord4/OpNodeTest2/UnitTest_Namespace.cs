using Microsoft.VisualStudio.TestTools.UnitTesting;
using pWordLib.dat;
using System;
using System.Xml;
using System.IO;
using Newtonsoft.Json;

namespace OpNodeTest2
{
    [TestClass]
    public class UnitTest_Namespace
    {
        [TestMethod]
        public void TestNamespaceCreation()
        {
            // Test basic namespace creation
            var ns = new NameSpace();
            ns.Prefix = "math";
            ns.URI_PREFIX = "http://opnode.org/math";
            
            Assert.IsNotNull(ns);
            Assert.AreEqual("math", ns.Prefix);
            Assert.AreEqual("http://opnode.org/math", ns.URI_PREFIX);
        }

        [TestMethod]
        public void TestNamespaceCloning()
        {
            // Test namespace cloning functionality
            var original = new NameSpace();
            original.Prefix = "data";
            original.Suffix = "v1";
            original.URI_PREFIX = "http://opnode.org/data";
            original.URI_SUFFIX = "http://opnode.org/data/v1";
            
            var clone = (NameSpace)original.Clone();
            
            Assert.IsNotNull(clone);
            Assert.AreEqual(original.Prefix, clone.Prefix);
            Assert.AreEqual(original.Suffix, clone.Suffix);
            Assert.AreEqual(original.URI_PREFIX, clone.URI_PREFIX);
            Assert.AreEqual(original.URI_SUFFIX, clone.URI_SUFFIX);
            
            // Verify they are separate objects
            clone.Prefix = "modified";
            Assert.AreNotEqual(original.Prefix, clone.Prefix);
        }

        [TestMethod]
        public void TestValidNamespacePrefixes()
        {
            // Test valid namespace prefixes using XML naming conventions
            var node1 = new pNode();
            node1.Text = "math:sum";
            Assert.IsTrue(node1.IsValidXmlName(node1.Text), "math:sum should be valid");
            
            var node2 = new pNode();
            node2.Text = "data:content";
            Assert.IsTrue(node2.IsValidXmlName(node2.Text), "data:content should be valid");
            
            var node3 = new pNode();
            node3.Text = "ui_controls:button";
            Assert.IsTrue(node3.IsValidXmlName(node3.Text), "ui_controls:button should be valid");
        }

        [TestMethod]
        public void TestInvalidNamespacePrefixes()
        {
            // Test invalid namespace prefixes
            var node1 = new pNode();
            node1.Text = "123invalid:test";
            Assert.IsFalse(node1.IsValidXmlName(node1.Text), "Prefix starting with number should be invalid");
            
            var node2 = new pNode();
            node2.Text = "xml:reserved";
            Assert.IsFalse(node2.IsValidXmlName(node2.Text), "xml prefix should be reserved/invalid");
            
            var node3 = new pNode();
            node3.Text = "invalid@prefix:test";
            Assert.IsFalse(node3.IsValidXmlName(node3.Text), "Prefix with @ symbol should be invalid");
        }

        [TestMethod]
        public void TestNodeWithNamespace()
        {
            // Test pNode with namespace assignment
            var node = new pNode();
            node.Text = "TestNode";
            node.Tag = "TestValue";
            
            var ns = new NameSpace();
            ns.Prefix = "test";
            ns.URI_PREFIX = "http://opnode.org/test";
            
            node.Namespace = ns;
            
            Assert.IsNotNull(node.Namespace);
            Assert.AreEqual("test", node.Namespace.Prefix);
            Assert.AreEqual("http://opnode.org/test", node.Namespace.URI_PREFIX);
        }

        [TestMethod]
        public void TestXmlExportWithNamespace()
        {
            // Test XML export functionality with namespace
            var rootNode = new pNode();
            rootNode.Text = "root";
            rootNode.Tag = "root value";
            
            var childNode = new pNode();
            childNode.Text = "child";
            childNode.Tag = "test value";
            
            var ns = new NameSpace();
            ns.Prefix = "test";
            ns.URI_PREFIX = "http://opnode.org/test";
            childNode.Namespace = ns;
            
            rootNode.Nodes.Add(childNode);
            
            try
            {
                var xmlDoc = rootNode.CallRecursive(rootNode);
                
                // Verify XML generation doesn't throw exception
                Assert.IsNotNull(xmlDoc);
                Assert.IsNotNull(xmlDoc.DocumentElement);
                Assert.AreEqual("root", xmlDoc.DocumentElement.Name);
            }
            catch (Exception ex)
            {
                Assert.Fail($"XML export with namespace failed: {ex.Message}");
            }
        }

        [TestMethod]
        public void TestJsonSerializationWithNamespace()
        {
            // Test JSON serialization with namespace information
            var node = new pNode();
            node.Text = "TestNode";
            node.Tag = "TestValue";
            
            var ns = new NameSpace();
            ns.Prefix = "json_test";
            ns.URI_PREFIX = "http://opnode.org/json";
            node.Namespace = ns;
            
            try
            {
                // Test that namespace info can be included in JSON serialization
                var jsonString = JsonConvert.SerializeObject(node.Namespace, Formatting.Indented);
                Assert.IsNotNull(jsonString);
                Assert.IsTrue(jsonString.Contains("json_test"));
                Assert.IsTrue(jsonString.Contains("http://opnode.org/json"));
                
                // Test deserialization
                var deserializedNs = JsonConvert.DeserializeObject<NameSpace>(jsonString);
                Assert.IsNotNull(deserializedNs);
                Assert.AreEqual(ns.Prefix, deserializedNs.Prefix);
                Assert.AreEqual(ns.URI_PREFIX, deserializedNs.URI_PREFIX);
            }
            catch (Exception ex)
            {
                Assert.Fail($"JSON serialization with namespace failed: {ex.Message}");
            }
        }

        [TestMethod]
        public void TestNamespaceWithSuffix()
        {
            // Test namespace with suffix functionality
            var ns = new NameSpace();
            ns.Prefix = "data";
            ns.Suffix = "v2";
            ns.URI_PREFIX = "http://opnode.org/data";
            ns.URI_SUFFIX = "http://opnode.org/data/v2";
            
            var node = new pNode();
            node.Text = "testNode";
            node.Namespace = ns;
            
            Assert.IsNotNull(node.Namespace);
            Assert.AreEqual("data", node.Namespace.Prefix);
            Assert.AreEqual("v2", node.Namespace.Suffix);
            Assert.AreEqual("http://opnode.org/data", node.Namespace.URI_PREFIX);
            Assert.AreEqual("http://opnode.org/data/v2", node.Namespace.URI_SUFFIX);
        }

        [TestMethod]
        public void TestEmptyNamespaceHandling()
        {
            // Test handling of empty/null namespace values
            var node = new pNode();
            node.Text = "testNode";
            node.Namespace = null;
            
            Assert.IsNull(node.Namespace);
            
            // Test with empty namespace
            var emptyNs = new NameSpace();
            node.Namespace = emptyNs;
            
            Assert.IsNotNull(node.Namespace);
            Assert.IsNull(node.Namespace.Prefix);
            Assert.IsNull(node.Namespace.Suffix);
        }
    }
}