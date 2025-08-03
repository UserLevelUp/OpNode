using OpNodeCore.Interfaces;
using OpNodeCore.Implementations;

namespace OpNodeCore.Demo
{
    /// <summary>
    /// Demonstration of the SOLID Core Interfaces for OpNode.
    /// Shows how the interfaces work without Windows Forms dependencies.
    /// </summary>
    public class OpNodeDemo
    {
        public static void Main()
        {
            Console.WriteLine("=== OpNode Core Interfaces Demo ===\n");

            // Demonstrate basic node creation and data management
            DemonstrateBasicNodeOperations();

            // Demonstrate hierarchical relationships
            DemonstrateHierarchicalOperations();

            // Demonstrate attributes management
            DemonstrateAttributeOperations();

            // Demonstrate operations and calculations
            DemonstrateNodeOperations();

            // Demonstrate search functionality
            DemonstrateSearchOperations();

            // Demonstrate the core OpNode behavior pattern
            DemonstrateOpNodeBehaviorPattern();

            // Demonstrate SOLID principles
            DemonstrateSOLIDPrinciples();

            Console.WriteLine("\n=== Demo completed successfully! ===");
        }

        private static void DemonstrateBasicNodeOperations()
        {
            Console.WriteLine("1. Basic Node Operations (INodeData):");
            
            var node = new BasicOpNode("MyNode", "Hello World");
            node.Tag = new { Type = "Demo", Created = DateTime.Now };

            Console.WriteLine($"   Name: {node.Name}");
            Console.WriteLine($"   Value: {node.Value}");
            Console.WriteLine($"   XML Name: {node.GetXmlName()}");
            Console.WriteLine($"   Is Valid Name: {node.IsValidName("ValidName")}");
            Console.WriteLine($"   Is Invalid Name: {node.IsValidName("123Invalid")}");
            Console.WriteLine();
        }

        private static void DemonstrateHierarchicalOperations()
        {
            Console.WriteLine("2. Hierarchical Operations (INodeContainer):");
            
            var parent = new BasicOpNode("Parent");
            var child1 = new BasicOpNode("Child1", "10");
            var child2 = new BasicOpNode("Child2", "20");
            var child3 = new BasicOpNode("Child3", "30");

            parent.AddChild(child1);
            parent.AddChild(child2);
            parent.AddChild(child3);

            Console.WriteLine($"   Parent has children: {parent.HasChildren()}");
            Console.WriteLine($"   Number of children: {parent.Children.Count}");
            Console.WriteLine($"   Child by name 'Child2': {parent.GetChild("Child2")?.Name}");
            Console.WriteLine($"   Child by index 1: {((INodeData?)parent.GetChild(1))?.Name}");
            Console.WriteLine();
        }

        private static void DemonstrateAttributeOperations()
        {
            Console.WriteLine("3. Attribute Operations (INodeAttributes):");
            
            var node = new BasicOpNode("ConfigNode");
            node.SetAttribute("version", "1.0");
            node.SetAttribute("author", "OpNode Team");
            node.SetAttribute("category", "configuration");

            Console.WriteLine($"   Version attribute: {node.GetAttribute("version")}");
            Console.WriteLine($"   Has author attribute: {node.HasAttribute("author")}");
            Console.WriteLine($"   Has value 'configuration': {node.HasAttributeValue("configuration")}");
            Console.WriteLine($"   All attributes: {string.Join(", ", node.AttributeKeys)}");
            Console.WriteLine();
        }

        private static void DemonstrateNodeOperations()
        {
            Console.WriteLine("4. Node Operations (INodeOperations):");
            
            var parent = new BasicOpNode("Calculator", "0");
            var child1 = new BasicOpNode("Value1", "15");
            var child2 = new BasicOpNode("Value2", "25");
            var child3 = new BasicOpNode("Value3", "10");

            parent.AddChild(child1);
            parent.AddChild(child2);
            parent.AddChild(child3);

            var sumOperation = new SumOperation();
            parent.AddOperation(sumOperation);

            Console.WriteLine($"   Before operation - Parent value: {parent.Value}");
            Console.WriteLine($"   Operation count: {parent.OperationCount}");
            Console.WriteLine($"   Operations: {parent.ListOperations()}");
            
            parent.PerformOperations();
            Console.WriteLine($"   After operation - Parent value: {parent.Value}");
            Console.WriteLine();
        }

        private static void DemonstrateSearchOperations()
        {
            Console.WriteLine("5. Search Operations (INodeSearchable):");
            
            var root = new BasicOpNode("Root", "root data");
            var branch1 = new BasicOpNode("Branch1", "important data");
            var branch2 = new BasicOpNode("Branch2", "other info");
            var leaf = new BasicOpNode("Leaf", "important data too");

            root.AddChild(branch1);
            root.AddChild(branch2);
            branch1.AddChild(leaf);

            branch1.SetAttribute("type", "important");

            var results = root.Find("important");
            Console.WriteLine($"   Search for 'important' found {results.Count} results:");
            foreach (var result in results)
            {
                var resultData = (INodeData)result;
                Console.WriteLine($"     - {resultData.Name}: {resultData.Value}");
            }
            Console.WriteLine();
        }

        private static void DemonstrateOpNodeBehaviorPattern()
        {
            Console.WriteLine("6. OpNode Behavior Pattern (IOpNodeBehavior):");
            
            var parent = new BasicOpNode("WorkflowNode", "0") as IOpNodeBehavior;
            var child1 = new BasicOpNode("Step1", "5");
            var child2 = new BasicOpNode("Step2", "3");

            ((INodeContainer)parent).AddChild(child1);
            ((INodeContainer)parent).AddChild(child2);

            Console.WriteLine("   Following OpNode sequence pattern:");
            
            // Step 1: Check if has children
            bool hasChildren = parent.HasChildren();
            Console.WriteLine($"     1. HasChildren(): {hasChildren}");

            if (hasChildren)
            {
                // Step 2: Do operation on children
                Console.WriteLine("     2. DoOperationOnChildren()");
                parent.DoOperationOnChildren();

                // Step 3: Calculate children results
                Console.WriteLine("     3. CalculateChildrenResults()");
                var results = parent.CalculateChildrenResults();

                // Step 4: Inform parent if contains same operation
                Console.WriteLine("     4. InformParentIfContainsSameOperation()");
                parent.InformParentIfContainsSameOperation();
            }
            Console.WriteLine();
        }

        private static void DemonstrateSOLIDPrinciples()
        {
            Console.WriteLine("7. SOLID Principles Demonstration:");
            
            // Single Responsibility - each interface has one job
            INodeData dataNode = new BasicOpNode("Data");
            INodeContainer containerNode = new BasicOpNode("Container");
            INodeAttributes attributeNode = new BasicOpNode("Attributes");
            
            Console.WriteLine("   ✓ Single Responsibility: Each interface has one clear purpose");

            // Open/Closed - can extend without modifying
            IOpNode customNode = new CustomDemoNode("Custom");
            Console.WriteLine("   ✓ Open/Closed: Extended with CustomDemoNode without modification");

            // Liskov Substitution - can substitute implementations
            ProcessNode(new BasicOpNode("Basic"));
            ProcessNode(customNode);
            Console.WriteLine("   ✓ Liskov Substitution: Different implementations work interchangeably");

            // Interface Segregation - clients use only what they need
            UseOnlyDataInterface(dataNode);
            UseOnlyContainerInterface(containerNode);
            Console.WriteLine("   ✓ Interface Segregation: Clients depend only on needed interfaces");

            // Dependency Inversion - depend on abstractions
            var processor = new NodeProcessor();
            processor.Process(new BasicOpNode("Test"));
            Console.WriteLine("   ✓ Dependency Inversion: Depends on abstractions, not concretions");
            Console.WriteLine();
        }

        private static void ProcessNode(IOpNode node)
        {
            // This method works with any IOpNode implementation
            node.Value = $"Processed: {node.Value}";
        }

        private static void UseOnlyDataInterface(INodeData node)
        {
            // This method only needs INodeData functionality
            Console.WriteLine($"     Data interface used: {node.Name}");
        }

        private static void UseOnlyContainerInterface(INodeContainer node)
        {
            // This method only needs INodeContainer functionality
            Console.WriteLine($"     Container interface used: Children count = {node.Children.Count}");
        }
    }

    // Example of extending the interfaces (Open/Closed Principle)
    public class CustomDemoNode : BasicOpNode
    {
        public CustomDemoNode(string name) : base(name)
        {
            Value = $"Custom: {name}";
        }

        public new virtual string GetXmlName()
        {
            return $"demo_{base.GetXmlName()}";
        }
    }

    // Example of depending on abstractions (Dependency Inversion Principle)
    public class NodeProcessor
    {
        public void Process(INodeData node)
        {
            // Depends on INodeData abstraction, not concrete implementation
            node.Value = $"Processed by NodeProcessor: {node.Value}";
        }
    }
}