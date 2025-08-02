using Microsoft.VisualStudio.TestTools.UnitTesting;
using pWordLib.dat;
using System;
using System.Xml;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest_PrefixSuffix
    {
        [TestMethod]
        public void TestPrefixFunctionality()
        {
            // Test basic prefix functionality
            var node = new pNode();
            node.Text = "testNode";
            node.Tag = "test value";
            
            var ns = new NameSpace();
            ns.Prefix = "data";
            ns.URI_PREFIX = "http://opnode.org/data";
            node.Namespace = ns;
            
            Assert.IsNotNull(node.Namespace);
            Assert.AreEqual("data", node.Namespace.Prefix);
            Assert.AreEqual("http://opnode.org/data", node.Namespace.URI_PREFIX);
            Assert.IsNull(node.Namespace.Suffix);
        }

        [TestMethod]
        public void TestSuffixFunctionality()
        {
            // Test basic suffix functionality  
            var node = new pNode();
            node.Text = "testNode";
            node.Tag = "test value";
            
            var ns = new NameSpace();
            ns.Suffix = "v1";
            ns.URI_SUFFIX = "http://opnode.org/version/v1";
            node.Namespace = ns;
            
            Assert.IsNotNull(node.Namespace);
            Assert.AreEqual("v1", node.Namespace.Suffix);
            Assert.AreEqual("http://opnode.org/version/v1", node.Namespace.URI_SUFFIX);
            Assert.IsNull(node.Namespace.Prefix);
        }

        [TestMethod]
        public void TestPrefixAndSuffixCombination()
        {
            // Test prefix and suffix working together
            var node = new pNode();
            node.Text = "apiEndpoint";
            node.Tag = "REST API endpoint";
            
            var ns = new NameSpace();
            ns.Prefix = "api";
            ns.Suffix = "v2";
            ns.URI_PREFIX = "http://company.com/api";
            ns.URI_SUFFIX = "http://company.com/api/v2";
            node.Namespace = ns;
            
            Assert.IsNotNull(node.Namespace);
            Assert.AreEqual("api", node.Namespace.Prefix);
            Assert.AreEqual("v2", node.Namespace.Suffix);
            Assert.AreEqual("http://company.com/api", node.Namespace.URI_PREFIX);
            Assert.AreEqual("http://company.com/api/v2", node.Namespace.URI_SUFFIX);
        }

        [TestMethod]
        public void TestPrefixValidation()
        {
            // Test various prefix patterns
            var validPrefixes = new[]
            {
                "api",
                "data_store", 
                "ui-controls",
                "math.calc",
                "_private"
            };

            foreach (var prefix in validPrefixes)
            {
                var node = new pNode();
                node.Text = $"{prefix}:element";
                Assert.IsTrue(node.IsValidXmlName(node.Text), $"Prefix '{prefix}' should be valid");
            }

            var invalidPrefixes = new[]
            {
                "123api",      // starts with number
                "xml-reserved", // starts with xml
                "api@company"   // contains invalid character
            };

            foreach (var prefix in invalidPrefixes)
            {
                var node = new pNode();
                node.Text = $"{prefix}:element";
                Assert.IsFalse(node.IsValidXmlName(node.Text), $"Prefix '{prefix}' should be invalid");
            }
        }

        [TestMethod]
        public void TestXmlExportWithPrefixAndSuffix()
        {
            // Test XML export preserves prefix and suffix information
            var root = new pNode();
            root.Text = "application";
            root.Tag = "Application root";

            var childNode = new pNode();
            childNode.Text = "service";
            childNode.Tag = "Service configuration";
            
            var ns = new NameSpace();
            ns.Prefix = "config";
            ns.Suffix = "beta";
            ns.URI_PREFIX = "http://app.com/config";
            ns.URI_SUFFIX = "http://app.com/config/beta";
            childNode.Namespace = ns;
            
            root.Nodes.Add(childNode);

            try
            {
                var xmlDoc = root.CallRecursive(root);
                Assert.IsNotNull(xmlDoc);
                Assert.IsNotNull(xmlDoc.DocumentElement);
                
                // The XML should be generated without errors
                Assert.AreEqual("application", xmlDoc.DocumentElement.Name);
            }
            catch (Exception ex)
            {
                Assert.Fail($"XML export with prefix and suffix failed: {ex.Message}");
            }
        }

        [TestMethod]
        public void TestSuffixUsageScenarios()
        {
            // Test common suffix usage patterns
            var versioningTestCases = new[]
            {
                new { prefix = "api", suffix = "v1", description = "API versioning" },
                new { prefix = "data", suffix = "beta", description = "Beta release" },
                new { prefix = "ui", suffix = "mobile", description = "Platform variant" },
                new { prefix = "auth", suffix = "oauth2", description = "Authentication method" }
            };

            foreach (var testCase in versioningTestCases)
            {
                var node = new pNode();
                node.Text = "element";
                node.Tag = testCase.description;
                
                var ns = new NameSpace();
                ns.Prefix = testCase.prefix;
                ns.Suffix = testCase.suffix;
                ns.URI_PREFIX = $"http://example.com/{testCase.prefix}";
                ns.URI_SUFFIX = $"http://example.com/{testCase.prefix}/{testCase.suffix}";
                node.Namespace = ns;
                
                Assert.IsNotNull(node.Namespace);
                Assert.AreEqual(testCase.prefix, node.Namespace.Prefix);
                Assert.AreEqual(testCase.suffix, node.Namespace.Suffix);
            }
        }

        [TestMethod]
        public void TestPrefixSuffixCloning()
        {
            // Test that prefix and suffix are properly cloned
            var original = new NameSpace();
            original.Prefix = "original_prefix";
            original.Suffix = "original_suffix";
            original.URI_PREFIX = "http://original.com/prefix";
            original.URI_SUFFIX = "http://original.com/suffix";
            
            var cloned = (NameSpace)original.Clone();
            
            Assert.IsNotNull(cloned);
            Assert.AreEqual(original.Prefix, cloned.Prefix);
            Assert.AreEqual(original.Suffix, cloned.Suffix);
            Assert.AreEqual(original.URI_PREFIX, cloned.URI_PREFIX);
            Assert.AreEqual(original.URI_SUFFIX, cloned.URI_SUFFIX);
            
            // Verify they are separate objects
            cloned.Prefix = "modified";
            cloned.Suffix = "modified";
            Assert.AreNotEqual(original.Prefix, cloned.Prefix);
            Assert.AreNotEqual(original.Suffix, cloned.Suffix);
        }

        [TestMethod]
        public void TestEmptyPrefixSuffixHandling()
        {
            // Test handling of empty/null prefix and suffix values
            var testCases = new[]
            {
                new { prefix = (string)null, suffix = "v1", shouldWork = true },
                new { prefix = "api", suffix = (string)null, shouldWork = true },
                new { prefix = "", suffix = "v1", shouldWork = true },
                new { prefix = "api", suffix = "", shouldWork = true },
                new { prefix = (string)null, suffix = (string)null, shouldWork = true }
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    var node = new pNode();
                    node.Text = "element";
                    
                    var ns = new NameSpace();
                    ns.Prefix = testCase.prefix;
                    ns.Suffix = testCase.suffix;
                    node.Namespace = ns;
                    
                    Assert.IsNotNull(node.Namespace);
                    Assert.AreEqual(testCase.prefix, node.Namespace.Prefix);
                    Assert.AreEqual(testCase.suffix, node.Namespace.Suffix);
                }
                catch (Exception ex)
                {
                    if (testCase.shouldWork)
                    {
                        Assert.Fail($"Unexpected exception for prefix='{testCase.prefix}', suffix='{testCase.suffix}': {ex.Message}");
                    }
                }
            }
        }
    }
}