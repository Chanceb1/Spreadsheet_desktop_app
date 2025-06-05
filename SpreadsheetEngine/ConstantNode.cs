// <copyright file="ConstantNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// expression tree node that hold a constant value.
    /// </summary>
    public class ConstantNode : Node
    {
        /// <summary>
        /// Constant value member variable.
        /// </summary>
        private readonly double nodeValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// constructor for constant node.
        /// </summary>
        /// <param name="value"> double value of node.</param>
        public ConstantNode(double value)
        {
            this.nodeValue = value;

            // this.nodeValue = Convert.ToDouble(value);
        }

        /// <summary>
        /// Gets constant node value.
        /// </summary>
        public double NodeValue
        {
            get { return this.nodeValue; }
        }

        /// <summary>
        /// evaluates the node expression.
        /// </summary>
        /// <returns> returns constant value.</returns>
        public override double Evaluate()
        {
            return this.nodeValue;
        }
    }
}
