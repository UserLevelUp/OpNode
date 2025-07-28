using OpNodeDemo;
using System;

namespace PlaygroundAlpha
{
    public static class OpNodeDemonstration
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== IOperate Interface Demonstration ===");
            Console.WriteLine();

            // Step 1: Create a root "sum" node (as described in the issue)
            Console.WriteLine("Step 1: Creating a 'sum' node with no initial value");
            var sumNode = new PNode("sum", "");
            Console.WriteLine($"Created: {sumNode}");
            Console.WriteLine();

            // Step 2: Add two child "item" nodes with numeric values (as described in the issue)
            Console.WriteLine("Step 2: Adding two child 'item' nodes with numeric values");
            var item1 = new PNode("item1", "25");
            var item2 = new PNode("item2", "75");
            
            sumNode.AddChild(item1);
            sumNode.AddChild(item2);
            
            Console.WriteLine($"Added child: {item1}");
            Console.WriteLine($"Added child: {item2}");
            Console.WriteLine();

            // Step 3: Display the tree before adding operation
            Console.WriteLine("Step 3: Tree structure before adding Sum operation:");
            sumNode.DisplayTree();
            Console.WriteLine();

            // Step 4: Add Sum operation to the sum node (as described in the issue)
            Console.WriteLine("Step 4: Adding Sum operation to the 'sum' node");
            var sumOperation = new Sum();
            sumNode.AddOperation(sumOperation);
            
            Console.WriteLine($"Sum operation added. Result: {sumNode}");
            Console.WriteLine();

            // Step 5: Display the tree after adding operation
            Console.WriteLine("Step 5: Tree structure after adding Sum operation:");
            sumNode.DisplayTree();
            Console.WriteLine();

            // Step 6: Demonstrate automatic recalculation when child values change
            Console.WriteLine("Step 6: Demonstrating automatic recalculation when child values change");
            Console.WriteLine("Changing item1 value from 25 to 50...");
            item1.Value = "50";
            item1.OperationChanged(); // Notify parent that a change occurred
            
            Console.WriteLine("Performing operations to recalculate sum...");
            sumNode.PerformOperations();
            Console.WriteLine($"New result: {sumNode}");
            Console.WriteLine();

            // Step 7: Add a third child and show automatic calculation
            Console.WriteLine("Step 7: Adding a third child node and recalculating");
            var item3 = new PNode("item3", "30");
            sumNode.AddChild(item3);
            Console.WriteLine($"Added child: {item3}");
            
            // Mark as changed and recalculate
            sumNode.OperationChanged();
            sumNode.PerformOperations();
            Console.WriteLine($"Updated result: {sumNode}");
            Console.WriteLine();

            // Step 8: Final tree display
            Console.WriteLine("Step 8: Final tree structure:");
            sumNode.DisplayTree();
            Console.WriteLine();

            // Step 9: Demonstrate nested operations
            Console.WriteLine("Step 9: Demonstrating nested operations with a sub-sum");
            var subSumNode = new PNode("subSum", "");
            var subItem1 = new PNode("subItem1", "10");
            var subItem2 = new PNode("subItem2", "20");
            
            subSumNode.AddChild(subItem1);
            subSumNode.AddChild(subItem2);
            subSumNode.AddOperation(new Sum());
            
            // Add the sub-sum as a child to the main sum
            sumNode.AddChild(subSumNode);
            sumNode.OperationChanged();
            sumNode.PerformOperations();
            
            Console.WriteLine("Added nested sum node with children 10 + 20 = 30");
            Console.WriteLine($"Main sum now includes nested result: {sumNode}");
            Console.WriteLine();
            
            Console.WriteLine("Final nested tree structure:");
            sumNode.DisplayTree();
            Console.WriteLine();

            Console.WriteLine("=== Demo Complete ===");
            Console.WriteLine();
            Console.WriteLine("Key Concepts Demonstrated:");
            Console.WriteLine("1. IOperate interface allows operations to be attached to nodes");
            Console.WriteLine("2. Sum operation automatically calculates total of child node values");
            Console.WriteLine("3. Change tracking enables efficient recalculation only when needed");
            Console.WriteLine("4. Operations can be nested (sums within sums)");
            Console.WriteLine("5. Tree structure maintains parent-child relationships");
            Console.WriteLine("6. Operations store results in the node's Value property");
        }
    }
}