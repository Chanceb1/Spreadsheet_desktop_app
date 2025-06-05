// <copyright file="DivideOperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.OperatorNodes
{
    /// <summary>
    /// Operator node that represents division operator.
    /// </summary>
    public class DivideOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivideOperatorNode"/> class.
        /// Operator node that represents division operator.
        /// </summary>
        public DivideOperatorNode()
        {
        }

        /// <summary>
        /// Gets the mulitplication operator.
        /// </summary>
        public static char Operator => '/';

        /// <summary>
        /// Gets operator associativity value.
        /// </summary>
        public override Associative Associativity => Associative.Left;

        /// <summary>
        /// Gets operator precedence value.
        /// </summary>
        public override int Precedence => 1; // `*` and `/` have the same precedence

        /// <summary>
        /// Evaluates and returns the division of the evaluated child nodes values.
        /// </summary>
        /// <returns> evaluated double value.</returns>
        public override double Evaluate()
        {
            return this.LeftChild.Evaluate() / this.RightChild.Evaluate();
        }
    }
}
