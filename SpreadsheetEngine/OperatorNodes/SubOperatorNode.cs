// <copyright file="SubOperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.OperatorNodes
{
    /// <summary>
    /// operator node that represents subtraction operator.
    /// </summary>
    public class SubOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubOperatorNode"/> class.
        /// operator node that represents subtraction operator.
        /// </summary>
        public SubOperatorNode()
        {
        }

        /// <summary>
        /// Gets the subtraction operator.
        /// </summary>
        public static char Operator => '-';

        /// <summary>
        /// Gets operator associativity value.
        /// </summary>
        public override Associative Associativity => Associative.Left;

        /// <summary>
        /// Gets operator precedence value.
        /// </summary>
        public override int Precedence => 0; // `+` and `-` have the same precedence

        /// <summary>
        /// Evaluates and returns the subtraction of the evaluated child nodes values.
        /// </summary>
        /// <returns> evaluated double value.</returns>
        public override double Evaluate()
        {
            return this.LeftChild.Evaluate() - this.RightChild.Evaluate();
        }
    }
}
