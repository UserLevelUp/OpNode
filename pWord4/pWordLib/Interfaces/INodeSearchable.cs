using System;
using System.Collections.Generic;

namespace pWordLib.Interfaces
{
    /// <summary>
    /// Interface for searching capabilities within node structures.
    /// Provides functionality for finding nodes based on various criteria.
    /// </summary>
    public interface INodeSearchable
    {
        /// <summary>
        /// Finds all nodes that match the given search text.
        /// Searches in node values, tags, and attributes.
        /// </summary>
        /// <param name="searchText">The text to search for</param>
        /// <returns>List of nodes that match the search criteria</returns>
        IList<INodeSearchable> Find(string searchText);

        /// <summary>
        /// Finds all nodes that match the given search text starting from a specific index.
        /// Used for iterative searching through results.
        /// </summary>
        /// <param name="searchText">The text to search for</param>
        /// <param name="startIndex">The index to start searching from</param>
        /// <returns>List of nodes that match the search criteria</returns>
        IList<INodeSearchable> Find(string searchText, int startIndex);

        /// <summary>
        /// Checks if this node matches the given search criteria.
        /// Includes checking name, value, tag, and attributes.
        /// </summary>
        /// <param name="searchText">The text to match against</param>
        /// <returns>True if this node matches the search criteria</returns>
        bool Matches(string searchText);
    }
}