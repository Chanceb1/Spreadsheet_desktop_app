// <copyright file="AddOperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.OperatorNodes
{
    /// <summary>
    /// Operator node that represents addition operator.
    /// </summary>
    public class AddOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOperatorNode"/> class.
        /// operator node that represents addition operator.
        /// </summary>
        public AddOperatorNode()
        {
        }

        /// <summary>
        /// Gets the addition operator.
        /// </summary>
        public static char Operator => '+';

        /// <summary>
        /// Gets operator associativity value.
        /// </summary>
        public override Associative Associativity => Associative.Left;

        /// <summary>
        /// gets operator precedence value.
        /// </summary>
        public override int Precedence => 0; // `+` and `-` have the same precedence.

        /// <summary>
        /// Evaluates and returns the addition of the evaluated child nodes values.
        /// </summary>
        /// <returns> double value sum of its children.</returns>
        public override double Evaluate()
        {
            return this.LeftChild.Evaluate() + this.RightChild.Evaluate();
        }
    }
}
