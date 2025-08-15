using System;
using System.Collections.Generic;

namespace OpNodeCore.Interfaces
{
    /// <summary>
    /// Main composite interface that represents a complete OpNode.
    /// Combines all SOLID interfaces for comprehensive node functionality.
    /// This interface is designed to be platform-independent and framework-agnostic.
    /// </summary>
    public interface IOpNode : INodeData, INodeContainer, INodeAttributes, INodeOperations, INodeNamespace, INodeSearchable
    {
        /// <summary>
        /// Creates a deep copy of this node and all its children.
        /// </summary>
        /// <returns>A cloned copy of this node</returns>
        IOpNode Clone();

        /// <summary>
        /// Gets or sets an error message associated with this node.
        /// </summary>
        string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Interface for operations specific to OpNode functionality.
    /// Provides the core operational behavior following the sequence pattern described in the documentation.
    /// </summary>
    public interface IOpNodeBehavior
    {
        /// <summary>
        /// Checks if this node has children that can participate in operations.
        /// Part of the core OpNode operation sequence: hasChildren() -> doOperation() -> CalculateChildrenResults() -> InformParent()
        /// </summary>
        /// <returns>True if the node has children, false otherwise</returns>
        bool HasChildren();

        /// <summary>
        /// Performs the operation on all child nodes.
        /// Part of the core OpNode operation sequence.
        /// </summary>
        void DoOperationOnChildren();

        /// <summary>
        /// Calculates the results from all child operations.
        /// Part of the core OpNode operation sequence.
        /// </summary>
        /// <returns>The calculated result from child operations</returns>
        object CalculateChildrenResults();

        /// <summary>
        /// Informs the parent node if it contains the same operation.
        /// Part of the core OpNode operation sequence.
        /// </summary>
        void InformParentIfContainsSameOperation();
    }
}