# OpNode Core Interfaces

This library provides SOLID-compliant, framework-independent interfaces for implementing tree-based node structures similar to the original pNode but without Windows Forms dependencies.

## Overview

The OpNode Core interfaces are designed to enable cross-platform compatibility while maintaining the same operational capabilities as the original pNode implementation. These interfaces follow SOLID principles to ensure maintainable, extensible, and testable code.

## Core Interfaces

### INodeData
Core interface for node data management following SOLID principles.
```csharp
public interface INodeData
{
    string? Name { get; set; }
    string? Value { get; set; }
    object? Tag { get; set; }
    string GetXmlName();
    bool IsValidName(string? name);
}
```

**Responsibilities:**
- Basic node identification and value management
- XML name validation and generation
- Tag-based data storage

### INodeContainer
Interface for managing hierarchical node relationships.
```csharp
public interface INodeContainer
{
    INodeContainer? Parent { get; }
    IReadOnlyList<INodeContainer> Children { get; }
    bool HasChildren();
    void AddChild(INodeContainer child);
    bool RemoveChild(INodeContainer child);
    INodeContainer? GetChild(string name);
    INodeContainer? GetChild(int index);
    bool HasChild(string name);
}
```

**Responsibilities:**
- Parent-child relationships
- Child node management
- Hierarchical structure navigation

### INodeAttributes
Interface for managing node attributes (key-value pairs).
```csharp
public interface INodeAttributes
{
    IReadOnlyList<string> AttributeKeys { get; }
    string? GetAttribute(string key);
    void SetAttribute(string key, string value);
    bool RemoveAttribute(string key);
    bool HasAttribute(string key);
    bool HasAttributeValue(string value);
    IReadOnlyDictionary<string, string> GetAllAttributes();
}
```

**Responsibilities:**
- Metadata management through attributes
- Key-value pair storage and retrieval
- Attribute querying and validation

### INodeOperations
Interface for managing operations on nodes.
```csharp
public interface INodeOperations
{
    int OperationCount { get; }
    bool HasChangedOperations { get; }
    void AddOperation(INodeOperation operation);
    void ClearOperations();
    void PerformOperations();
    string ListOperations();
    void OperationChanged();
}
```

**Responsibilities:**
- Operation lifecycle management
- Change tracking for operations
- Operation execution and coordination

### INodeNamespace
Interface for managing XML namespace information.
```csharp
public interface INodeNamespace
{
    string? Prefix { get; set; }
    string? Suffix { get; set; }
    string? PrefixUri { get; set; }
    string? SuffixUri { get; set; }
    bool HasPrefix();
    bool HasSuffix();
    INodeNamespace Clone();
}
```

**Responsibilities:**
- XML namespace management
- Prefix and URI handling
- Namespace validation

### INodeSearchable
Interface for searching capabilities within node structures.
```csharp
public interface INodeSearchable
{
    IList<INodeSearchable> Find(string searchText);
    IList<INodeSearchable> Find(string searchText, int startIndex);
    bool Matches(string searchText);
}
```

**Responsibilities:**
- Node search functionality
- Pattern matching within node hierarchies
- Iterative search support

### IOpNode
Main composite interface that combines all SOLID interfaces.
```csharp
public interface IOpNode : INodeData, INodeContainer, INodeAttributes, 
                          INodeOperations, INodeNamespace, INodeSearchable
{
    IOpNode Clone();
    string? ErrorMessage { get; set; }
}
```

**Responsibilities:**
- Complete node functionality
- Deep cloning capabilities
- Error state management

### IOpNodeBehavior
Interface for core OpNode operational behavior following the documented sequence pattern.
```csharp
public interface IOpNodeBehavior
{
    bool HasChildren();
    void DoOperationOnChildren();
    object CalculateChildrenResults();
    void InformParentIfContainsSameOperation();
}
```

**Responsibilities:**
- Implements the core OpNode sequence: HasChildren() → DoOperation() → CalculateChildrenResults() → InformParent()
- Change detection and parent notification
- Result calculation and bubbling

## SOLID Principles Compliance

### Single Responsibility Principle (SRP)
Each interface has a single, well-defined responsibility:
- `INodeData` - Basic data management
- `INodeContainer` - Hierarchical relationships
- `INodeAttributes` - Attribute management
- `INodeOperations` - Operation management
- `INodeNamespace` - Namespace handling
- `INodeSearchable` - Search functionality

### Open/Closed Principle (OCP)
Interfaces are open for extension but closed for modification. New implementations can be created without changing existing interfaces.

### Liskov Substitution Principle (LSP)
All implementations of the interfaces can be substituted for each other without breaking functionality.

### Interface Segregation Principle (ISP)
Clients depend only on the interfaces they need. They are not forced to implement unused functionality.

### Dependency Inversion Principle (DIP)
High-level modules depend on abstractions (interfaces) rather than concrete implementations.

## Implementation

The `BasicOpNode` class provides a complete implementation of all interfaces, demonstrating how they work together:

```csharp
var node = new BasicOpNode("MyNode", "MyValue");
node.SetAttribute("type", "example");
node.AddOperation(new SumOperation());

var child = new BasicOpNode("Child", "10");
node.AddChild(child);

var results = node.Find("example");
```

## Usage Examples

### Creating a Basic Node
```csharp
var node = new BasicOpNode("RootNode", "RootValue");
node.SetAttribute("category", "root");
```

### Building a Hierarchy
```csharp
var parent = new BasicOpNode("Parent");
var child1 = new BasicOpNode("Child1", "10");
var child2 = new BasicOpNode("Child2", "20");

parent.AddChild(child1);
parent.AddChild(child2);
```

### Adding Operations
```csharp
var sumOperation = new SumOperation();
parent.AddOperation(sumOperation);
parent.PerformOperations(); // Calculates sum of children
```

### Searching
```csharp
var results = parent.Find("Child1");
foreach (var result in results)
{
    Console.WriteLine($"Found: {((INodeData)result).Name}");
}
```

### Following OpNode Behavior Pattern
```csharp
var behaviorNode = parent as IOpNodeBehavior;
if (behaviorNode.HasChildren())
{
    behaviorNode.DoOperationOnChildren();
    var results = behaviorNode.CalculateChildrenResults();
    behaviorNode.InformParentIfContainsSameOperation();
}
```

## Testing

The library includes comprehensive unit tests that validate:
- Individual interface behaviors
- SOLID principles compliance
- Integration between interfaces
- Error handling and edge cases

Run tests with:
```bash
dotnet test
```

## Benefits

1. **Platform Independence**: No dependency on Windows Forms or any specific UI framework
2. **Testability**: Clean interfaces enable easy unit testing and mocking
3. **Extensibility**: New implementations can be added without modifying existing code
4. **Maintainability**: SOLID principles ensure clean, maintainable code
5. **Flexibility**: Clients can depend only on the functionality they need
6. **Compatibility**: Maintains the same operational semantics as the original pNode

## Future Enhancements

- Additional operation implementations (mathematical, string processing, etc.)
- Serialization support for different formats (JSON, XML, Binary)
- Async operation support
- Performance optimizations
- Plugin architecture for custom operations