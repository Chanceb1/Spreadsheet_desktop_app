// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// represents a operator node.
    /// </summary>
    public abstract class OperatorNode : Node
    {
        /// <summary>
        /// pointer for the left child node.
        /// </summary>
        private Node leftChild = null!;

        /// <summary>
        /// pointer for the right child node.
        /// </summary>
        private Node rightChild = null!;

        /// <summary>
        /// Associativity of the operator.
        /// </summary>
        public enum Associative
        {
            /// <summary>
            /// if node has no associativity.
            /// </summary>
            None = 0,

            /// <summary>
            /// Left operator associativity.
            /// </summary>
            Left,

            /// <summary>
            /// Right operator associativity.
            /// </summary>
            Right,
        }

        /// <summary>
        /// Gets the operator node value.
        /// </summary>
        public static char OperatorValue { get; }

        /// <summary>
        /// Gets operator associativity.
        /// </summary>
        public abstract Associative Associativity { get; }

        /// <summary>
        /// Gets operator precedence.
        /// </summary>
        public abstract int Precedence { get; }

        /// <summary>
        /// gets or sets the left child node.
        /// </summary>
        public Node LeftChild
        {
            get { return this.leftChild; }
            set { this.leftChild = value; }
        }

        /// <summary>
        /// gets or sets the right child node.
        /// </summary>
        public Node RightChild
        {
            get { return this.rightChild; }
            set { this.rightChild = value; }
        }
    }
}
