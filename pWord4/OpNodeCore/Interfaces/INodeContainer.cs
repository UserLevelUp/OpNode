using System;
using System.Collections.Generic;

namespace OpNodeCore.Interfaces
{
    /// <summary>
    /// Interface for managing hierarchical node relationships.
    /// Provides functionality for parent-child node management.
    /// </summary>
    public interface INodeContainer
    {
        /// <summary>
        /// Gets the parent node of this node, if any.
        /// </summary>
        INodeContainer Parent { get; }

        /// <summary>
        /// Gets the collection of child nodes.
        /// </summary>
        IReadOnlyList<INodeContainer> Children { get; }

        /// <summary>
        /// Checks if this node has any child nodes.
        /// </summary>
        /// <returns>True if the node has children, false otherwise</returns>
        bool HasChildren();

        /// <summary>
        /// Adds a child node to this node.
        /// </summary>
        /// <param name="child">The child node to add</param>
        void AddChild(INodeContainer child);

        /// <summary>
        /// Removes a child node from this node.
        /// </summary>
        /// <param name="child">The child node to remove</param>
        /// <returns>True if the child was removed, false if not found</returns>
        bool RemoveChild(INodeContainer child);

        /// <summary>
        /// Gets a child node by name.
        /// </summary>
        /// <param name="name">The name of the child to find</param>
        /// <returns>The child node if found, null otherwise</returns>
        INodeContainer GetChild(string name);

        /// <summary>
        /// Gets a child node by index.
        /// </summary>
        /// <param name="index">The index of the child</param>
        /// <returns>The child node if found, null otherwise</returns>
        INodeContainer GetChild(int index);

        /// <summary>
        /// Checks if a child with the given name exists.
        /// </summary>
        /// <param name="name">The name to check</param>
        /// <returns>True if a child with that name exists, false otherwise</returns>
        bool HasChild(string name);
    }
}