// <copyright file="MultiplyOperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine.OperatorNodes
{
    /// <summary>
    /// Operator node that represents multiplication operator.
    /// </summary>
    public class MultiplyOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplyOperatorNode"/> class.
        /// operator node that represents multiplication operator.
        /// </summary>
        public MultiplyOperatorNode()
        {
        }

        /// <summary>
        /// Gets the mulitplication operator.
        /// </summary>
        public static char Operator => '*';

        /// <summary>
        /// Gets operator associativity value.
        /// </summary>
        public override Associative Associativity => Associative.Left;

        /// <summary>
        /// Gets operator precedence value.
        /// </summary>
        public override int Precedence => 1; // `*` and `/` have the same precedence

        /// <summary>
        /// Evaluates and returns the multiplication of the evaluated child nodes values.
        /// </summary>
        /// <returns> evaluation of children nodes.</returns>
        public override double Evaluate()
        {
            return this.LeftChild.Evaluate() * this.RightChild.Evaluate();
        }
    }
}
