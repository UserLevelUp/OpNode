using System;
using System.Collections.Generic;

namespace OpNodeCore.Interfaces
{
    /// <summary>
    /// Interface for managing node attributes (key-value pairs).
    /// Provides functionality for storing and retrieving metadata associated with nodes.
    /// </summary>
    public interface INodeAttributes
    {
        /// <summary>
        /// Gets all attribute keys for this node.
        /// </summary>
        IReadOnlyList<string> AttributeKeys { get; }

        /// <summary>
        /// Gets the value of an attribute by key.
        /// </summary>
        /// <param name="key">The attribute key</param>
        /// <returns>The attribute value if found, null otherwise</returns>
        string GetAttribute(string key);

        /// <summary>
        /// Sets the value of an attribute.
        /// </summary>
        /// <param name="key">The attribute key</param>
        /// <param name="value">The attribute value</param>
        void SetAttribute(string key, string value);

        /// <summary>
        /// Removes an attribute by key.
        /// </summary>
        /// <param name="key">The attribute key to remove</param>
        /// <returns>True if the attribute was removed, false if not found</returns>
        bool RemoveAttribute(string key);

        /// <summary>
        /// Checks if an attribute with the given key exists.
        /// </summary>
        /// <param name="key">The attribute key to check</param>
        /// <returns>True if the attribute exists, false otherwise</returns>
        bool HasAttribute(string key);

        /// <summary>
        /// Checks if any attribute has the given value.
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <returns>True if any attribute has that value, false otherwise</returns>
        bool HasAttributeValue(string value);

        /// <summary>
        /// Gets all attributes as key-value pairs.
        /// </summary>
        /// <returns>Dictionary of all attributes</returns>
        IReadOnlyDictionary<string, string> GetAllAttributes();
    }
}