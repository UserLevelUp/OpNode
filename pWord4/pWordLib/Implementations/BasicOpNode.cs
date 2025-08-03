using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using pWordLib.Interfaces;

namespace pWordLib.Implementations
{
    /// <summary>
    /// Basic implementation of IOpNode interfaces for testing and demonstration purposes.
    /// This implementation is framework-independent and follows SOLID principles.
    /// </summary>
    public class BasicOpNode : IOpNode, IOpNodeBehavior
    {
        private string _name;
        private string _value;
        private object _tag;
        private readonly Dictionary<string, string> _attributes;
        private readonly List<INodeOperation> _operations;
        private readonly List<INodeContainer> _children;
        private INodeContainer _parent;

        public BasicOpNode()
        {
            _attributes = new Dictionary<string, string>();
            _operations = new List<INodeOperation>();
            _children = new List<INodeContainer>();
        }

        public BasicOpNode(string name) : this()
        {
            Name = name;
            Value = name;
        }

        public BasicOpNode(string name, string value) : this()
        {
            Name = name;
            Value = value;
        }

        public BasicOpNode(string name, object tag) : this()
        {
            Name = name;
            Value = name;
            Tag = tag;
        }

        #region INodeData Implementation

        public string Name
        {
            get => _name;
            set
            {
                if (IsValidName(value))
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentException($"Invalid node name: {value}");
                }
            }
        }

        public string Value
        {
            get => _value;
            set => _value = value;
        }

        public object Tag
        {
            get => _tag;
            set => _tag = value;
        }

        public string GetXmlName()
        {
            if (string.IsNullOrEmpty(_name))
            {
                return Guid.NewGuid().ToString();
            }
            return _name;
        }

        public bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            // Check if name starts with a digit
            if (char.IsDigit(name[0]))
                return false;

            // Check if name contains spaces
            if (name.Contains(' '))
                return false;

            // Check for invalid XML characters
            return !name.Any(c => "!@#$%^&*()+=[]{}|\\;:'\",<>/?".Contains(c));
        }

        #endregion

        #region INodeContainer Implementation

        public INodeContainer Parent => _parent;

        public IReadOnlyList<INodeContainer> Children => _children.AsReadOnly();

        public bool HasChildren()
        {
            return _children.Count > 0;
        }

        public void AddChild(INodeContainer child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            
            _children.Add(child);
            if (child is BasicOpNode basicChild)
            {
                basicChild._parent = this;
            }
        }

        public bool RemoveChild(INodeContainer child)
        {
            if (child == null) return false;
            
            bool removed = _children.Remove(child);
            if (removed && child is BasicOpNode basicChild)
            {
                basicChild._parent = null;
            }
            return removed;
        }

        public INodeContainer GetChild(string name)
        {
            return _children.FirstOrDefault(c => 
                c is INodeData nodeData && nodeData.Name == name);
        }

        public INodeContainer GetChild(int index)
        {
            if (index < 0 || index >= _children.Count)
                return null;
            return _children[index];
        }

        public bool HasChild(string name)
        {
            return GetChild(name) != null;
        }

        #endregion

        #region INodeAttributes Implementation

        public IReadOnlyList<string> AttributeKeys => _attributes.Keys.ToList().AsReadOnly();

        public string GetAttribute(string key)
        {
            return _attributes.TryGetValue(key, out string value) ? value : null;
        }

        public void SetAttribute(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Attribute key cannot be null or empty");
            
            _attributes[key] = value;
        }

        public bool RemoveAttribute(string key)
        {
            return _attributes.Remove(key);
        }

        public bool HasAttribute(string key)
        {
            return _attributes.ContainsKey(key);
        }

        public bool HasAttributeValue(string value)
        {
            return _attributes.ContainsValue(value);
        }

        public IReadOnlyDictionary<string, string> GetAllAttributes()
        {
            return _attributes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        #endregion

        #region INodeOperations Implementation

        public int OperationCount => _operations.Count;

        public bool HasChangedOperations => _operations.Any(op => op.HasChanges);

        public void AddOperation(INodeOperation operation)
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));
            
            ClearOperations(); // For now, clear existing operations as per original pNode behavior
            _operations.Add(operation);
            operation.Operate(this);
        }

        public void ClearOperations()
        {
            _operations.Clear();
        }

        public void PerformOperations()
        {
            foreach (var operation in _operations.Where(op => op.HasChanges))
            {
                operation.Operate(this);
                operation.ClearChanged(this);
            }
        }

        public string ListOperations()
        {
            return string.Join(" ", _operations.Select(op => op.OperationType));
        }

        public void OperationChanged()
        {
            var operation = _operations.FirstOrDefault();
            if (operation != null)
            {
                operation.MarkChanged(this);
            }
            else if (_parent is BasicOpNode parentNode && parentNode._operations.Count > 0)
            {
                parentNode._operations[0].MarkChanged(parentNode);
            }
        }

        #endregion

        #region INodeNamespace Implementation

        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string PrefixUri { get; set; }
        public string SuffixUri { get; set; }

        public bool HasPrefix()
        {
            return !string.IsNullOrEmpty(Prefix);
        }

        public bool HasSuffix()
        {
            return !string.IsNullOrEmpty(Suffix);
        }

        INodeNamespace INodeNamespace.Clone()
        {
            return new BasicOpNode
            {
                Prefix = this.Prefix,
                Suffix = this.Suffix,
                PrefixUri = this.PrefixUri,
                SuffixUri = this.SuffixUri
            };
        }

        #endregion

        #region INodeSearchable Implementation

        public IList<INodeSearchable> Find(string searchText)
        {
            return Find(searchText, 0);
        }

        public IList<INodeSearchable> Find(string searchText, int startIndex)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<INodeSearchable>();

            var results = new List<INodeSearchable>();
            
            if (Matches(searchText))
            {
                results.Add(this);
            }

            foreach (var child in _children.OfType<INodeSearchable>())
            {
                results.AddRange(child.Find(searchText, 0));
            }

            return results;
        }

        public bool Matches(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return false;

            return (_value?.Contains(searchText) == true) ||
                   (_tag?.ToString()?.Contains(searchText) == true) ||
                   _attributes.ContainsKey(searchText) ||
                   _attributes.ContainsValue(searchText);
        }

        #endregion

        #region IOpNode Implementation

        public IOpNode Clone()
        {
            var clone = new BasicOpNode
            {
                Name = this.Name,
                Value = this.Value,
                Tag = this.Tag,
                Prefix = this.Prefix,
                Suffix = this.Suffix,
                PrefixUri = this.PrefixUri,
                SuffixUri = this.SuffixUri,
                ErrorMessage = this.ErrorMessage
            };

            // Copy attributes
            foreach (var attr in _attributes)
            {
                clone._attributes[attr.Key] = attr.Value;
            }

            // Copy operations
            foreach (var operation in _operations)
            {
                clone._operations.Add(operation);
            }

            // Recursively clone children
            foreach (var child in _children.OfType<IOpNode>())
            {
                clone.AddChild(child.Clone());
            }

            return clone;
        }

        public string ErrorMessage { get; set; }

        #endregion

        #region IOpNodeBehavior Implementation

        bool IOpNodeBehavior.HasChildren()
        {
            return HasChildren();
        }

        public void DoOperationOnChildren()
        {
            foreach (var child in _children.OfType<IOpNodeBehavior>())
            {
                child.DoOperationOnChildren();
            }
        }

        public object CalculateChildrenResults()
        {
            var results = new List<object>();
            foreach (var child in _children.OfType<IOpNodeBehavior>())
            {
                results.Add(child.CalculateChildrenResults());
            }
            return results;
        }

        public void InformParentIfContainsSameOperation()
        {
            if (_parent is IOpNodeBehavior parentBehavior && _operations.Count > 0)
            {
                // Check if parent has same operation type
                if (_parent is BasicOpNode parentNode && 
                    parentNode._operations.Count > 0 && 
                    parentNode._operations[0].OperationType == _operations[0].OperationType)
                {
                    parentNode.OperationChanged();
                }
            }
        }

        #endregion
    }
}