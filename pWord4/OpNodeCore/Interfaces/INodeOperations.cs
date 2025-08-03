using System;
using System.Collections.Generic;

namespace OpNodeCore.Interfaces
{
    /// <summary>
    /// Interface for managing operations on nodes.
    /// Provides functionality for adding, executing and managing operations.
    /// </summary>
    public interface INodeOperations
    {
        /// <summary>
        /// Gets the count of operations associated with this node.
        /// </summary>
        int OperationCount { get; }

        /// <summary>
        /// Gets information about operations that have pending changes.
        /// </summary>
        bool HasChangedOperations { get; }

        /// <summary>
        /// Adds an operation to this node.
        /// </summary>
        /// <param name="operation">The operation to add</param>
        void AddOperation(INodeOperation operation);

        /// <summary>
        /// Removes all operations from this node.
        /// </summary>
        void ClearOperations();

        /// <summary>
        /// Executes all operations on this node.
        /// </summary>
        void PerformOperations();

        /// <summary>
        /// Gets a string representation of all operations.
        /// </summary>
        /// <returns>String listing all operations</returns>
        string ListOperations();

        /// <summary>
        /// Notifies that an operation has changed and needs recalculation.
        /// </summary>
        void OperationChanged();
    }

    /// <summary>
    /// Interface for individual operations that can be performed on nodes.
    /// Extends the existing IOperate interface for compatibility.
    /// </summary>
    public interface INodeOperation
    {
        /// <summary>
        /// Gets whether this operation has pending changes.
        /// </summary>
        bool HasChanges { get; }

        /// <summary>
        /// Performs the operation on the given node.
        /// </summary>
        /// <param name="node">The node to operate on</param>
        /// <returns>The result node after operation</returns>
        object Operate(object node);

        /// <summary>
        /// Marks the operation as having changes.
        /// </summary>
        /// <param name="node">The node that triggered the change</param>
        /// <returns>True if change was successfully marked</returns>
        bool MarkChanged(object node);

        /// <summary>
        /// Clears the changed flag for this operation.
        /// </summary>
        /// <param name="node">The node to clear changes for</param>
        void ClearChanged(object node);

        /// <summary>
        /// Gets the display name or type of this operation.
        /// </summary>
        string OperationType { get; }
    }
}