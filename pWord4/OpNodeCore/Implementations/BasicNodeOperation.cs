using System;
using OpNodeCore.Interfaces;

namespace OpNodeCore.Implementations
{
    /// <summary>
    /// Basic implementation of INodeOperation for testing purposes.
    /// Provides a simple operation that can be used to validate the interface behavior.
    /// </summary>
    public class BasicNodeOperation : INodeOperation
    {
        private bool _hasChanges;
        
        public BasicNodeOperation(string operationType)
        {
            OperationType = operationType ?? "BasicOperation";
        }

        public bool HasChanges => _hasChanges;

        public string OperationType { get; }

        public object Operate(object node)
        {
            // Basic operation implementation for testing
            if (node is INodeData nodeData)
            {
                // Example: Append operation type to the value
                nodeData.Value = $"{nodeData.Value}_{OperationType}";
            }
            
            _hasChanges = false;
            return node;
        }

        public bool MarkChanged(object node)
        {
            _hasChanges = true;
            return true;
        }

        public void ClearChanged(object node)
        {
            _hasChanges = false;
        }
    }

    /// <summary>
    /// Basic sum operation for testing mathematical operations on nodes.
    /// </summary>
    public class SumOperation : INodeOperation
    {
        private bool _hasChanges;

        public bool HasChanges => _hasChanges;

        public string OperationType => "Sum";

        public object Operate(object node)
        {
            if (node is INodeContainer container)
            {
                double sum = 0;
                foreach (var child in container.Children)
                {
                    if (child is INodeData childData && 
                        double.TryParse(childData.Value, out double value))
                    {
                        sum += value;
                    }
                }

                if (container is INodeData nodeData)
                {
                    nodeData.Value = sum.ToString();
                }
            }

            _hasChanges = false;
            return node;
        }

        public bool MarkChanged(object node)
        {
            _hasChanges = true;
            return true;
        }

        public void ClearChanged(object node)
        {
            _hasChanges = false;
        }
    }
}