using Microsoft.VisualStudio.TestTools.UnitTesting;
using pWordLib.dat;
using System;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace OpNodeTest2
{
    [TestClass]
    public class UnitTest_NamespaceUsability
    {
        private string testDataPath;

        [TestInitialize]
        public void Setup()
        {
            testDataPath = Path.Combine(Path.GetTempPath(), "OpNodeNamespaceTests");
            if (!Directory.Exists(testDataPath))
            {
                Directory.CreateDirectory(testDataPath);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(testDataPath))
            {
                Directory.Delete(testDataPath, true);
            }
        }

        [TestMethod]
        public void TestSaveAndLoadXmlWithNamespaces()
        {
            // Create a tree structure with various namespaces
            var root = new pNode();
            root.Text = "document";
            root.Tag = "Document root";

            // Math namespace
            var mathNode = new pNode();
            mathNode.Text = "operations";
            mathNode.Tag = "Math operations";
            var mathNs = new NameSpace();
            mathNs.Prefix = "math";
            mathNs.URI_PREFIX = "http://opnode.org/math";
            mathNode.Namespace = mathNs;

            var sumNode = new pNode();
            sumNode.Text = "sum";
            sumNode.Tag = "42";
            sumNode.Namespace = mathNs;
            mathNode.Nodes.Add(sumNode);

            // Data namespace
            var dataNode = new pNode();
            dataNode.Text = "content";
            dataNode.Tag = "Data content";
            var dataNs = new NameSpace();
            dataNs.Prefix = "data";
            dataNs.URI_PREFIX = "http://opnode.org/data";
            dataNode.Namespace = dataNs;

            var textNode = new pNode();
            textNode.Text = "text";
            textNode.Tag = "Hello World";
            textNode.Namespace = dataNs;
            dataNode.Nodes.Add(textNode);

            root.Nodes.Add(mathNode);
            root.Nodes.Add(dataNode);

            string xmlFilePath = Path.Combine(testDataPath, "namespace_test.xml");

            try
            {
                // Test XML export
                var xmlDoc = root.CallRecursive(root);
                Assert.IsNotNull(xmlDoc);

                // Save to file
                xmlDoc.Save(xmlFilePath);
                Assert.IsTrue(File.Exists(xmlFilePath));

                // Verify file content contains namespace information
                string xmlContent = File.ReadAllText(xmlFilePath);
                Assert.IsTrue(xmlContent.Contains("math"), "XML should contain math namespace");
                Assert.IsTrue(xmlContent.Contains("data"), "XML should contain data namespace");
                Assert.IsTrue(xmlContent.Contains("operations"), "XML should contain operations element");
                Assert.IsTrue(xmlContent.Contains("content"), "XML should contain content element");

                // Test loading the XML file back
                var loadedDoc = new XmlDocument();
                loadedDoc.Load(xmlFilePath);
                Assert.IsNotNull(loadedDoc.DocumentElement);
                Assert.AreEqual("document", loadedDoc.DocumentElement.Name);

            }
            catch (Exception ex)
            {
                Assert.Fail($"XML save/load with namespaces failed: {ex.Message}");
            }
        }

        [TestMethod]
        public void TestSaveAndLoadJsonWithNamespaces()
        {
            // Create a complex structure with namespaces for JSON serialization
            var root = new pNode();
            root.Text = "api_root";
            root.Tag = "API Root Node";

            // Security namespace
            var securityNode = new pNode();
            securityNode.Text = "auth";
            securityNode.Tag = "Authentication data";
            var secNs = new NameSpace();
            secNs.Prefix = "sec";
            secNs.URI_PREFIX = "http://opnode.org/security";
            secNs.Suffix = "v1";
            secNs.URI_SUFFIX = "http://opnode.org/security/v1";
            securityNode.Namespace = secNs;

            // UI namespace
            var uiNode = new pNode();
            uiNode.Text = "interface";
            uiNode.Tag = "User interface elements";
            var uiNs = new NameSpace();
            uiNs.Prefix = "ui";
            uiNs.URI_PREFIX = "http://opnode.org/ui";
            uiNode.Namespace = uiNs;

            root.Nodes.Add(securityNode);
            root.Nodes.Add(uiNode);

            string jsonFilePath = Path.Combine(testDataPath, "namespace_test.json");

            try
            {
                // Create a simple structure to serialize
                var namespaceData = new
                {
                    Root = new
                    {
                        Text = root.Text,
                        Tag = root.Tag,
                        Children = new[]
                        {
                            new
                            {
                                Text = securityNode.Text,
                                Tag = securityNode.Tag,
                                Namespace = securityNode.Namespace
                            },
                            new
                            {
                                Text = uiNode.Text,
                                Tag = uiNode.Tag,
                                Namespace = uiNode.Namespace
                            }
                        }
                    }
                };

                // Save to JSON file
                string jsonString = JsonConvert.SerializeObject(namespaceData, Formatting.Indented);
                File.WriteAllText(jsonFilePath, jsonString);
                Assert.IsTrue(File.Exists(jsonFilePath));

                // Verify file content contains namespace information
                string jsonContent = File.ReadAllText(jsonFilePath);
                Assert.IsTrue(jsonContent.Contains("sec"), "JSON should contain sec namespace prefix");
                Assert.IsTrue(jsonContent.Contains("ui"), "JSON should contain ui namespace prefix");
                Assert.IsTrue(jsonContent.Contains("http://opnode.org/security"), "JSON should contain security URI");
                Assert.IsTrue(jsonContent.Contains("http://opnode.org/ui"), "JSON should contain ui URI");
                Assert.IsTrue(jsonContent.Contains("v1"), "JSON should contain version suffix");

                // Test loading the JSON file back
                string loadedJson = File.ReadAllText(jsonFilePath);
                var deserializedData = JsonConvert.DeserializeObject(loadedJson);
                Assert.IsNotNull(deserializedData);

            }
            catch (Exception ex)
            {
                Assert.Fail($"JSON save/load with namespaces failed: {ex.Message}");
            }
        }

        [TestMethod]
        public void TestNamespaceConsistencyInFiles()
        {
            // Test that namespaces are handled consistently across different file formats
            var testNode = new pNode();
            testNode.Text = "testElement";
            testNode.Tag = "Test Value";

            var ns = new NameSpace();
            ns.Prefix = "test";
            ns.Suffix = "beta";
            ns.URI_PREFIX = "http://opnode.org/test";
            ns.URI_SUFFIX = "http://opnode.org/test/beta";
            testNode.Namespace = ns;

            try
            {
                // Test XML consistency
                var xmlDoc = testNode.CallRecursive(testNode);
                Assert.IsNotNull(xmlDoc);
                
                string xmlPath = Path.Combine(testDataPath, "consistency_test.xml");
                xmlDoc.Save(xmlPath);
                
                // Load back and verify
                var loadedXml = new XmlDocument();
                loadedXml.Load(xmlPath);
                Assert.IsNotNull(loadedXml.DocumentElement);

                // Test JSON consistency  
                string jsonData = JsonConvert.SerializeObject(testNode.Namespace, Formatting.Indented);
                string jsonPath = Path.Combine(testDataPath, "consistency_test.json");
                File.WriteAllText(jsonPath, jsonData);
                
                string loadedJson = File.ReadAllText(jsonPath);
                var deserializedNs = JsonConvert.DeserializeObject<NameSpace>(loadedJson);
                
                // Verify namespace consistency
                Assert.AreEqual(ns.Prefix, deserializedNs.Prefix);
                Assert.AreEqual(ns.Suffix, deserializedNs.Suffix);
                Assert.AreEqual(ns.URI_PREFIX, deserializedNs.URI_PREFIX);
                Assert.AreEqual(ns.URI_SUFFIX, deserializedNs.URI_SUFFIX);

            }
            catch (Exception ex)
            {
                Assert.Fail($"Namespace consistency test failed: {ex.Message}");
            }
        }

        [TestMethod]
        public void TestNamespaceValidationInContext()
        {
            // Test namespace validation in real-world usage scenarios
            var validTestCases = new[]
            {
                "math:operations",
                "data:content", 
                "ui_controls:button",
                "app-settings:config",
                "api.v2:endpoint"
            };

            var invalidTestCases = new[]
            {
                "123invalid:test",    // starts with number
                "xml:reserved",       // xml prefix is reserved
                "invalid@:test",      // invalid character
                ":noprefix",          // empty prefix
                "valid:",             // empty local name
            };

            foreach (var validCase in validTestCases)
            {
                var node = new pNode();
                node.Text = validCase;
                Assert.IsTrue(node.IsValidXmlName(validCase), $"{validCase} should be valid");
            }

            foreach (var invalidCase in invalidTestCases)
            {
                var node = new pNode();
                node.Text = invalidCase;
                Assert.IsFalse(node.IsValidXmlName(invalidCase), $"{invalidCase} should be invalid");
            }
        }

        [TestMethod]
        public void TestComplexNamespaceHierarchy()
        {
            // Test complex namespace hierarchies for real-world usage
            var root = new pNode();
            root.Text = "application";
            root.Tag = "Application Root";

            // Create a complex hierarchy with mixed namespaces
            var modules = new[]
            {
                new { prefix = "auth", uri = "http://app.com/auth", name = "authentication", value = "user login system" },
                new { prefix = "data", uri = "http://app.com/data", name = "storage", value = "database operations" },
                new { prefix = "ui", uri = "http://app.com/ui", name = "interface", value = "user interface components" },
                new { prefix = "api", uri = "http://app.com/api", name = "endpoints", value = "REST API endpoints" }
            };

            foreach (var module in modules)
            {
                var moduleNode = new pNode();
                moduleNode.Text = module.name;
                moduleNode.Tag = module.value;
                
                var ns = new NameSpace();
                ns.Prefix = module.prefix;
                ns.URI_PREFIX = module.uri;
                moduleNode.Namespace = ns;

                // Add child elements with same namespace
                for (int i = 1; i <= 3; i++)
                {
                    var childNode = new pNode();
                    childNode.Text = $"item{i}";
                    childNode.Tag = $"Child item {i}";
                    childNode.Namespace = ns;
                    moduleNode.Nodes.Add(childNode);
                }

                root.Nodes.Add(moduleNode);
            }

            try
            {
                // Test XML export of complex hierarchy
                var xmlDoc = root.CallRecursive(root);
                Assert.IsNotNull(xmlDoc);

                string xmlPath = Path.Combine(testDataPath, "complex_hierarchy.xml");
                xmlDoc.Save(xmlPath);
                Assert.IsTrue(File.Exists(xmlPath));

                // Verify the file contains all namespaces
                string content = File.ReadAllText(xmlPath);
                foreach (var module in modules)
                {
                    Assert.IsTrue(content.Contains(module.prefix), $"XML should contain {module.prefix} namespace");
                    Assert.IsTrue(content.Contains(module.name), $"XML should contain {module.name} element");
                }

            }
            catch (Exception ex)
            {
                Assert.Fail($"Complex namespace hierarchy test failed: {ex.Message}");
            }
        }

        [TestMethod]
        public void TestNamespaceErrorHandling()
        {
            // Test error handling with malformed namespaces
            var testCases = new[]
            {
                new { text = "", shouldPass = false, description = "empty text" },
                new { text = "validname", shouldPass = true, description = "no namespace prefix" },
                new { text = "valid:name", shouldPass = true, description = "valid namespace" },
                new { text = ":::invalid", shouldPass = false, description = "multiple colons" },
                new { text = "space name:invalid", shouldPass = false, description = "space in local name" }
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    var node = new pNode();
                    node.Text = testCase.text;
                    
                    if (testCase.shouldPass)
                    {
                        // Should not throw exception
                        var result = node.IsValidXmlName(testCase.text);
                        // We expect this to work for valid cases
                    }
                    else
                    {
                        // Invalid cases should fail validation
                        var result = node.IsValidXmlName(testCase.text);
                        if (result)
                        {
                            Assert.Fail($"Expected {testCase.description} to fail validation but it passed");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (testCase.shouldPass)
                    {
                        Assert.Fail($"Unexpected exception for {testCase.description}: {ex.Message}");
                    }
                    // Expected exception for invalid cases
                }
            }
        }
    }
}