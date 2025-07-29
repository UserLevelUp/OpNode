using System;
using System.Collections.Generic;
using System.Linq;

namespace OpNodeDemo
{
    /// <summary>
    /// Simplified PNode class that demonstrates the IOperate interface concept
    /// </summary>
    public class PNode
    {
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public string ErrorString { get; set; } = "";
        public PNode Parent { get; set; }
        public List<PNode> Children { get; set; } = new List<PNode>();
        public List<IOperate> Operations { get; set; } = new List<IOperate>();

        public PNode(string name = "", string value = "")
        {
            Name = name;
            Value = value;
        }

        public void AddChild(PNode child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public void AddOperation(IOperate operation)
        {
            Operations.Add(operation);
            operation.Operate(this);
        }

        public void PerformOperations()
        {
            foreach (IOperate operation in Operations)
            {
                operation.Operate(this); // usually it performs the actual operation on its child elements, and stores the result in its value field.
            }
        }

        public bool HasChangedOperations()
        {
            return Operations.Where(m => m.Changed == true).Count() > 0;
        }

        public void OperationChanged()
        {
            if (Operations.Count > 0) 
            {
                Operations[0].Change(this);
            }
        }

        public void ClearOperations()
        {
            Operations.Clear();
        }

        public string ListOperations()
        {
            return string.Join(", ", Operations.Select(op => op.GetType().Name));
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }

        /// <summary>
        /// Display the tree structure
        /// </summary>
        public void DisplayTree(int indent = 0)
        {
            string indentStr = new string(' ', indent * 2);
            string ops = Operations.Count > 0 ? $" [Operations: {ListOperations()}]" : "";
            Console.WriteLine($"{indentStr}{Name}: {Value}{ops}");
            
            foreach (var child in Children)
            {
                child.DisplayTree(indent + 1);
            }
        }
    }
}