using System;

namespace pWordLib.Interfaces
{
    /// <summary>
    /// Interface for managing XML namespace information for nodes.
    /// Provides functionality for prefix and URI management.
    /// </summary>
    public interface INodeNamespace
    {
        /// <summary>
        /// Gets or sets the namespace prefix.
        /// </summary>
        string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the namespace suffix.
        /// </summary>
        string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the namespace URI for the prefix.
        /// </summary>
        string PrefixUri { get; set; }

        /// <summary>
        /// Gets or sets the namespace URI for the suffix.
        /// </summary>
        string SuffixUri { get; set; }

        /// <summary>
        /// Checks if this namespace has a valid prefix.
        /// </summary>
        /// <returns>True if prefix is defined, false otherwise</returns>
        bool HasPrefix();

        /// <summary>
        /// Checks if this namespace has a valid suffix.
        /// </summary>
        /// <returns>True if suffix is defined, false otherwise</returns>
        bool HasSuffix();

        /// <summary>
        /// Creates a copy of this namespace.
        /// </summary>
        /// <returns>A cloned namespace object</returns>
        INodeNamespace Clone();
    }
}