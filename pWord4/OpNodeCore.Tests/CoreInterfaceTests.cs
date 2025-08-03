using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpNodeCore.Interfaces;
using OpNodeCore.Implementations;
using System.Linq;

namespace OpNodeCore.Tests
{
    [TestClass]
    public class INodeDataTests
    {
        [TestMethod]
        public void NodeData_SetName_ValidName_SetsCorrectly()
        {
            // Arrange
            var node = new BasicOpNode();
            var validName = "ValidNodeName";

            // Act
            node.Name = validName;

            // Assert
            Assert.AreEqual(validName, node.Name);
        }

        [TestMethod]
        public void NodeData_SetName_InvalidName_ThrowsException()
        {
            // Arrange
            var node = new BasicOpNode();
            var invalidName = "123InvalidName"; // starts with digit

            // Act & Assert
            Assert.ThrowsException<System.ArgumentException>(() => node.Name = invalidName);
        }

        [TestMethod]
        public void NodeData_SetValue_SetsCorrectly()
        {
            // Arrange
            var node = new BasicOpNode();
            var value = "Test Value";

            // Act
            node.Value = value;

            // Assert
            Assert.AreEqual(value, node.Value);
        }

        [TestMethod]
        public void NodeData_SetTag_SetsCorrectly()
        {
            // Arrange
            var node = new BasicOpNode();
            var tag = new { Key = "Value" };

            // Act
            node.Tag = tag;

            // Assert
            Assert.AreEqual(tag, node.Tag);
        }

        [TestMethod]
        public void NodeData_GetXmlName_EmptyName_ReturnsGuid()
        {
            // Arrange
            var node = new BasicOpNode();

            // Act
            var xmlName = node.GetXmlName();

            // Assert
            Assert.IsNotNull(xmlName);
            Assert.IsTrue(xmlName.Length > 0);
        }

        [TestMethod]
        public void NodeData_IsValidName_ValidNames_ReturnsTrue()
        {
            // Arrange
            var node = new BasicOpNode();
            var validNames = new[] { "ValidName", "valid_name", "node123" };

            // Act & Assert
            foreach (var name in validNames)
            {
                Assert.IsTrue(node.IsValidName(name), $"Expected '{name}' to be valid");
            }
        }

        [TestMethod]
        public void NodeData_IsValidName_InvalidNames_ReturnsFalse()
        {
            // Arrange
            var node = new BasicOpNode();
            var invalidNames = new[] { "123InvalidName", "Invalid Name", "", null, "name@invalid" };

            // Act & Assert
            foreach (var name in invalidNames)
            {
                Assert.IsFalse(node.IsValidName(name), $"Expected '{name}' to be invalid");
            }
        }
    }

    [TestClass]
    public class INodeContainerTests
    {
        [TestMethod]
        public void NodeContainer_AddChild_AddsCorrectly()
        {
            // Arrange
            var parent = new BasicOpNode("Parent");
            var child = new BasicOpNode("Child");

            // Act
            parent.AddChild(child);

            // Assert
            Assert.AreEqual(1, parent.Children.Count);
            Assert.AreEqual(child, parent.Children[0]);
            Assert.AreEqual(parent, child.Parent);
        }

        [TestMethod]
        public void NodeContainer_HasChildren_WithChildren_ReturnsTrue()
        {
            // Arrange
            var parent = new BasicOpNode("Parent");
            var child = new BasicOpNode("Child");
            parent.AddChild(child);

            // Act & Assert
            Assert.IsTrue(parent.HasChildren());
        }

        [TestMethod]
        public void NodeContainer_HasChildren_WithoutChildren_ReturnsFalse()
        {
            // Arrange
            var parent = new BasicOpNode("Parent");

            // Act & Assert
            Assert.IsFalse(parent.HasChildren());
        }

        [TestMethod]
        public void NodeContainer_RemoveChild_RemovesCorrectly()
        {
            // Arrange
            var parent = new BasicOpNode("Parent");
            var child = new BasicOpNode("Child");
            parent.AddChild(child);

            // Act
            var removed = parent.RemoveChild(child);

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(0, parent.Children.Count);
            Assert.IsNull(child.Parent);
        }

        [TestMethod]
        public void NodeContainer_GetChildByName_FindsCorrectChild()
        {
            // Arrange
            var parent = new BasicOpNode("Parent");
            var child1 = new BasicOpNode("Child1");
            var child2 = new BasicOpNode("Child2");
            parent.AddChild(child1);
            parent.AddChild(child2);

            // Act
            var found = parent.GetChild("Child2");

            // Assert
            Assert.AreEqual(child2, found);
        }

        [TestMethod]
        public void NodeContainer_GetChildByIndex_FindsCorrectChild()
        {
            // Arrange
            var parent = new BasicOpNode("Parent");
            var child1 = new BasicOpNode("Child1");
            var child2 = new BasicOpNode("Child2");
            parent.AddChild(child1);
            parent.AddChild(child2);

            // Act
            var found = parent.GetChild(1);

            // Assert
            Assert.AreEqual(child2, found);
        }

        [TestMethod]
        public void NodeContainer_HasChild_ExistingChild_ReturnsTrue()
        {
            // Arrange
            var parent = new BasicOpNode("Parent");
            var child = new BasicOpNode("Child");
            parent.AddChild(child);

            // Act & Assert
            Assert.IsTrue(parent.HasChild("Child"));
        }
    }

    [TestClass]
    public class INodeAttributesTests
    {
        [TestMethod]
        public void NodeAttributes_SetAttribute_SetsCorrectly()
        {
            // Arrange
            var node = new BasicOpNode();
            var key = "TestKey";
            var value = "TestValue";

            // Act
            node.SetAttribute(key, value);

            // Assert
            Assert.AreEqual(value, node.GetAttribute(key));
            Assert.IsTrue(node.HasAttribute(key));
        }

        [TestMethod]
        public void NodeAttributes_RemoveAttribute_RemovesCorrectly()
        {
            // Arrange
            var node = new BasicOpNode();
            var key = "TestKey";
            var value = "TestValue";
            node.SetAttribute(key, value);

            // Act
            var removed = node.RemoveAttribute(key);

            // Assert
            Assert.IsTrue(removed);
            Assert.IsFalse(node.HasAttribute(key));
            Assert.IsNull(node.GetAttribute(key));
        }

        [TestMethod]
        public void NodeAttributes_HasAttributeValue_FindsValue()
        {
            // Arrange
            var node = new BasicOpNode();
            var key = "TestKey";
            var value = "TestValue";
            node.SetAttribute(key, value);

            // Act & Assert
            Assert.IsTrue(node.HasAttributeValue(value));
        }

        [TestMethod]
        public void NodeAttributes_GetAllAttributes_ReturnsAllAttributes()
        {
            // Arrange
            var node = new BasicOpNode();
            node.SetAttribute("Key1", "Value1");
            node.SetAttribute("Key2", "Value2");

            // Act
            var allAttributes = node.GetAllAttributes();

            // Assert
            Assert.AreEqual(2, allAttributes.Count);
            Assert.AreEqual("Value1", allAttributes["Key1"]);
            Assert.AreEqual("Value2", allAttributes["Key2"]);
        }
    }

    [TestClass]
    public class INodeOperationsTests
    {
        [TestMethod]
        public void NodeOperations_AddOperation_AddsCorrectly()
        {
            // Arrange
            var node = new BasicOpNode("TestNode", "TestValue");
            var operation = new BasicNodeOperation("TestOperation");

            // Act
            node.AddOperation(operation);

            // Assert
            Assert.AreEqual(1, node.OperationCount);
            Assert.IsTrue(node.ListOperations().Contains("TestOperation"));
        }

        [TestMethod]
        public void NodeOperations_PerformOperations_ExecutesOperations()
        {
            // Arrange
            var node = new BasicOpNode("TestNode", "TestValue");
            var operation = new BasicNodeOperation("TestOperation");
            node.AddOperation(operation);
            operation.MarkChanged(node);

            // Act
            node.PerformOperations();

            // Assert
            Assert.IsTrue(node.Value.Contains("TestOperation"));
        }

        [TestMethod]
        public void NodeOperations_ClearOperations_ClearsAllOperations()
        {
            // Arrange
            var node = new BasicOpNode("TestNode", "TestValue");
            var operation = new BasicNodeOperation("TestOperation");
            node.AddOperation(operation);

            // Act
            node.ClearOperations();

            // Assert
            Assert.AreEqual(0, node.OperationCount);
        }

        [TestMethod]
        public void NodeOperations_SumOperation_CalculatesSum()
        {
            // Arrange
            var parent = new BasicOpNode("Parent", "0");
            var child1 = new BasicOpNode("Child1", "10");
            var child2 = new BasicOpNode("Child2", "20");
            parent.AddChild(child1);
            parent.AddChild(child2);
            
            var sumOperation = new SumOperation();

            // Act
            parent.AddOperation(sumOperation);
            parent.PerformOperations();

            // Assert
            Assert.AreEqual("30", parent.Value);
        }
    }

    [TestClass]
    public class INodeSearchableTests
    {
        [TestMethod]
        public void NodeSearchable_Find_FindsMatchingNodes()
        {
            // Arrange
            var root = new BasicOpNode("Root", "RootValue");
            var child1 = new BasicOpNode("Child1", "TestValue");
            var child2 = new BasicOpNode("Child2", "AnotherValue");
            root.AddChild(child1);
            root.AddChild(child2);

            // Act
            var results = root.Find("TestValue");

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(child1, results[0]);
        }

        [TestMethod]
        public void NodeSearchable_Matches_MatchesCorrectly()
        {
            // Arrange
            var node = new BasicOpNode("TestNode", "TestValue");
            node.SetAttribute("TestAttr", "TestAttrValue");

            // Act & Assert
            Assert.IsTrue(node.Matches("TestValue"));
            Assert.IsTrue(node.Matches("TestAttr"));
            Assert.IsTrue(node.Matches("TestAttrValue"));
            Assert.IsFalse(node.Matches("NotFound"));
        }
    }

    [TestClass]
    public class IOpNodeBehaviorTests
    {
        [TestMethod]
        public void OpNodeBehavior_Clone_CreatesDeepCopy()
        {
            // Arrange
            var original = new BasicOpNode("Original", "OriginalValue");
            original.SetAttribute("TestAttr", "TestValue");
            var child = new BasicOpNode("Child", "ChildValue");
            original.AddChild(child);

            // Act
            var clone = original.Clone();

            // Assert
            Assert.AreNotSame(original, clone);
            Assert.AreEqual(original.Name, clone.Name);
            Assert.AreEqual(original.Value, clone.Value);
            Assert.AreEqual(original.GetAttribute("TestAttr"), clone.GetAttribute("TestAttr"));
            Assert.AreEqual(1, clone.Children.Count);
            Assert.AreEqual("Child", ((INodeData)clone.Children[0]).Name);
        }

        [TestMethod]
        public void OpNodeBehavior_DoOperationOnChildren_CallsChildOperations()
        {
            // Arrange
            var parent = new BasicOpNode("Parent", "0") as IOpNodeBehavior;
            var child = new BasicOpNode("Child", "10");
            ((INodeContainer)parent).AddChild(child);

            // Act & Assert (no exception should be thrown)
            parent.DoOperationOnChildren();
        }

        [TestMethod]
        public void OpNodeBehavior_FollowsSequencePattern()
        {
            // Arrange
            var parent = new BasicOpNode("Parent", "0") as IOpNodeBehavior;
            var child1 = new BasicOpNode("Child1", "10");
            var child2 = new BasicOpNode("Child2", "20");
            ((INodeContainer)parent).AddChild(child1);
            ((INodeContainer)parent).AddChild(child2);

            // Act - Follow the sequence: HasChildren -> DoOperation -> CalculateChildrenResults -> InformParent
            bool hasChildren = parent.HasChildren();
            object? results = null;
            if (hasChildren)
            {
                parent.DoOperationOnChildren();
                results = parent.CalculateChildrenResults();
                parent.InformParentIfContainsSameOperation();
            }

            // Assert
            Assert.IsTrue(hasChildren);
            Assert.IsNotNull(results);
        }
    }

    [TestClass]
    public class SOLIDPrinciplesTests
    {
        [TestMethod]
        public void SingleResponsibilityPrinciple_EachInterfaceHasSingleResponsibility()
        {
            // INodeData - manages basic node data
            // INodeContainer - manages hierarchical relationships
            // INodeAttributes - manages attribute key-value pairs
            // INodeOperations - manages operations on nodes
            // INodeNamespace - manages XML namespace information
            // INodeSearchable - manages search functionality

            var node = new BasicOpNode("Test");

            // Each interface can be used independently
            Assert.IsTrue(node is INodeData);
            Assert.IsTrue(node is INodeContainer);
            Assert.IsTrue(node is INodeAttributes);
            Assert.IsTrue(node is INodeOperations);
            Assert.IsTrue(node is INodeNamespace);
            Assert.IsTrue(node is INodeSearchable);
        }

        [TestMethod]
        public void OpenClosedPrinciple_InterfacesAreOpenForExtension()
        {
            // The interfaces are designed to be extended without modification
            // Custom implementations can be created without changing existing interfaces
            
            var customNode = new CustomOpNode("Custom");
            Assert.IsTrue(customNode is IOpNode);
            Assert.AreEqual("Custom", customNode.Name);
        }

        [TestMethod]
        public void InterfaceSegregationPrinciple_ClientsNotForcedToDependOnUnusedInterfaces()
        {
            // Clients can depend only on the interfaces they need
            INodeData dataOnly = new BasicOpNode("Test");
            INodeContainer containerOnly = new BasicOpNode("Test");
            INodeAttributes attributesOnly = new BasicOpNode("Test");

            // Each interface can be used independently
            Assert.IsNotNull(dataOnly);
            Assert.IsNotNull(containerOnly);
            Assert.IsNotNull(attributesOnly);
        }

        [TestMethod]
        public void DependencyInversionPrinciple_DependsOnAbstraction()
        {
            // High-level modules depend on abstractions, not concretions
            ProcessNodeData(new BasicOpNode("Test"));
            ProcessNodeData(new CustomOpNode("Custom"));
        }

        private void ProcessNodeData(INodeData node)
        {
            // This method depends on the INodeData abstraction, not concrete implementations
            Assert.IsNotNull(node.Name);
        }
    }

    // Custom implementation to test extensibility
    public class CustomOpNode : BasicOpNode
    {
        public CustomOpNode(string name) : base(name)
        {
        }

        public new virtual string GetXmlName()
        {
            return $"Custom_{base.GetXmlName()}";
        }
    }
}