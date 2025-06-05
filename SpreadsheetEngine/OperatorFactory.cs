// <copyright file="OperatorFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Reflection;

    /// <summary>
    /// class factory pattern for creating operator nodes.
    /// </summary>
    internal class OperatorFactory
    {
        /// <summary>
        /// dictionary of operators and their corresponding types.
        /// </summary>
        private Dictionary<char, Type> operators;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorFactory"/> class.
        /// creates an operator node based on the operator character.
        /// </summary>
        public OperatorFactory()
        {
            this.operators = new Dictionary<char, Type>();
            this.FindAndRegisterOperators((op, type) => this.operators.Add(op, type));
        }

        /// <summary>
        /// delegate used for operator creation.
        /// </summary>
        /// <param name="operatorSymbol"> represents the operator char.</param>
        /// <param name="type"> type of operator node that is the symbol.</param>
        private delegate void OperatorHandler(char operatorSymbol, Type type);

        /// <summary>
        /// Takes an operator charachter and returns it's coresponding operator node.
        /// </summary>
        /// <param name="operatorSymbol"> charachter of operator.</param>
        /// <returns> operator node type.</returns>
        public OperatorNode CreateOperatorNode(char operatorSymbol)
        {
            if (!this.operators.ContainsKey(operatorSymbol)) // if operator is not in dictionary
            {
                return null !;
            }

            return (OperatorNode)Activator.CreateInstance(this.operators[operatorSymbol]) !;
        }

        /// <summary>
        /// method checks if operator char is a valid type.
        /// </summary>
        /// <param name="op"> operator character.</param>
        /// <returns> true of false.</returns>
        internal bool ValidOperator(char op)
        {
            return this.operators.ContainsKey(op);
        }

        /// <summary>
        /// searches loaded assemblies and adds operator nodes to the dictionary.
        /// </summary>
        /// <param name="handler"> delegate of operator signature type.</param>
        private void FindAndRegisterOperators(OperatorHandler handler)
        {
            Type operatorNodeType = typeof(OperatorNode); // get operator node type

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) // iterate through assemblies
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                foreach (var type in operatorTypes) // iterate through operator types
                {
                    PropertyInfo operatorType = type.GetProperty("Operator") !; // get operator field

                    if (operatorType != null)
                    {
                        var value = operatorType.GetValue(type);

                        if (value is char)
                        {
                            char operatorSymbol = (char)value; // get operator symbol

                            handler(operatorSymbol, type); // call delegate
                        }
                    }
                }
            }
        }
    }
}
