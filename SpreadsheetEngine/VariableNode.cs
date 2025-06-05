// <copyright file="VariableNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// expression tree node represents a node that holds a variable.
    /// </summary>
    public class VariableNode : Node
    {
        /// <summary>
        /// dictionary to hold variables and their values.
        /// </summary>
        private Dictionary<string, double> variables = new ();

        private string variableName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// expression tree node constructor for variable.
        /// </summary>
        /// <param name="variableName"> string variable.</param>
        /// <param name="variables"> reference to variable dictionary.</param>
        public VariableNode(string variableName, ref Dictionary<string, double> variables)
        {
            this.variableName = variableName;

            if (variables != null)
            {
                this.variables = variables;
            }
            else
            {
                this.variables = new Dictionary<string, double>();
            }
        }

        /// <summary>
        /// Gets variable name.
        /// </summary>
        /// <returns> variable text.</returns>
        public string VarName
        {
            get { return this.variableName; }
        }

        /// <summary>
        /// evaluates the node expression. if variable not found returns 0.0.
        /// </summary>
        /// <returns> value of variable in dictionary.</returns>
        public override double Evaluate()
        {
            this.variables.TryGetValue(this.variableName, out var value);
            return value;
        }
    }
}
