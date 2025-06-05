// <copyright file="Node.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// expression tree base class node, abstract class that concrete nodes inherit from.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Evaluates the node expression.
        /// </summary>
        /// <returns>The node's evaluation result.</returns>
        public abstract double Evaluate();
    }
}
