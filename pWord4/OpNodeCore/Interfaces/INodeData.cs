using System;
using System.Collections.Generic;

namespace OpNodeCore.Interfaces
{
    /// <summary>
    /// Core interface for node data management following SOLID principles.
    /// Represents a single node with key-value pair functionality independent of UI frameworks.
    /// </summary>
    public interface INodeData
    {
        /// <summary>
        /// Gets or sets the unique identifier for this node.
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Gets or sets the display text/value for this node.
        /// </summary>
        string? Value { get; set; }

        /// <summary>
        /// Gets or sets the optional object data associated with this node.
        /// </summary>
        object? Tag { get; set; }

        /// <summary>
        /// Gets a unique identifier for this node, generated if not set.
        /// </summary>
        string GetXmlName();

        /// <summary>
        /// Validates whether the given name is valid for XML/node naming conventions.
        /// </summary>
        /// <param name="name">The name to validate</param>
        /// <returns>True if the name is valid, false otherwise</returns>
        bool IsValidName(string? name);
    }
}