// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// class to create an expression tree from a parsed string and evaluate it.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// root node of the expression tree.
        /// </summary>
        private Node? root = null;

        /// <summary>
        /// for storing variables and their values.
        /// </summary>
        private Dictionary<string, double> variables = new ();

        /// <summary>
        /// for storing variable names.
        /// </summary>
        private List<string> variableNames = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// expression tree class used for evaluating expressions.
        /// </summary>
        /// <param name="expression"> the expression to be parsed and evaluated.</param>
        public ExpressionTree(string expression)
        {
            this.root = this.CreateExpressionTree(expression); // cretes the expression tree
        }

        /// <summary>
        /// Sets the specified variable within the ExpressionTree variables dictionary.
        /// </summary>
        /// <param name="variableName"> name of variable.</param>
        /// <param name="variableValue"> value of variable.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables[variableName] = variableValue; // sets the variable value in the dictionary
        }

        /// <summary>
        /// Gets the variable value from the ExpressionTree variables dictionary.
        /// </summary>
        /// <returns> list of variables.</returns>
        public List<string> GetVariableNames()
        {
            return this.variableNames;
        }

        /// <summary>
        /// evaluates the expression to a double value.
        /// </summary>
        /// <returns> returns the evaluated expression as a float.</returns>
        public double Evaluate()
        {
            double result = 0.0;
            if (this.root != null)
            {
                result = this.root.Evaluate();
            }

            return result;
        }

        /// <summary>
        /// checks if the node is a constant node.
        /// </summary>
        /// <param name="expressionNode"> expression node.</param>
        /// <returns> true or false.</returns>
        private bool IfConstantNode(Node expressionNode) => expressionNode.GetType() == typeof(ConstantNode);

        /// <summary>
        /// checks if node is a variable node.
        /// </summary>
        /// <param name="node"> expression node.</param>
        /// <returns> true or false.</returns>
        private bool IfVariableNode(Node node) => node.GetType() == typeof(VariableNode);

        /// <summary>
        /// creates the expression tree from the postfixed parsesd expression string.
        /// </summary>
        /// <param name="expression"> string expression.</param>
        /// <returns> expression node. </returns>
        private Node CreateExpressionTree(string expression)
        {
            List<Node> postfixList = this.TranslateToPostFix(expression); // gets the postfix ordered list of nodes

            Stack<Node> expressionStack = new Stack<Node>(); // stack used for creating the expression tree

            foreach (Node current in postfixList) // iterate over the postfix list
            {
                if (this.IfConstantNode(current) || this.IfVariableNode(current)) // if the node is a constant or variable node
                {
                    expressionStack.Push(current); // push node onto stack
                }
                else
                {
                    OperatorNode? currentOperator = current as OperatorNode; // cast current node to operator node

                    currentOperator!.RightChild = expressionStack.Pop(); // set rigth child
                    currentOperator!.LeftChild = expressionStack.Pop(); // set left child

                    expressionStack.Push(currentOperator);
                }
            }

            Node node = expressionStack.Pop(); // pop the root
            return node; // return the root node
        }

        /// <summary>
        /// method uses Dijkstra's Shunting Yard algorithm to parse and
        /// convert infix expression to postfix expression.
        /// </summary>
        /// <param name="expression"> expression string.</param>
        /// <returns> postfix ordered expression node list.</returns>
        private List<Node> TranslateToPostFix(string expression)
        {
            // data collections used for the shunting yard algorithm
            Stack<char> expressionStack = new Stack<char>();
            List<Node> postfixList = new List<Node>();

            OperatorFactory operatorHandler = new OperatorFactory();

            char[] expressionString = expression.ToArray<char>(); // parse the expression string to a char array

            // loops over each char in expression string
            for (int i = 0; i < expressionString.Length; i++)
            {
                char currentCharacter = expressionString[i]; // current char ptr

                if (currentCharacter.Equals('(')) // left parenthesis
                {
                    expressionStack.Push(currentCharacter);
                }
                else if (currentCharacter.Equals(')')) // right parenthesis
                {
                    while (!expressionStack.Peek().Equals('('))
                    {
                        OperatorNode newOpNode = operatorHandler.CreateOperatorNode(expressionStack.Pop());
                        postfixList.Add(newOpNode);
                    }

                    expressionStack.Pop(); // pop parenthesis
                }
                else if (operatorHandler.ValidOperator(currentCharacter)) // char is an operator
                {
                    if (expressionStack.Count <= 0 || expressionStack.Peek().Equals('(')) // handles parenthesis
                    {
                        expressionStack.Push(currentCharacter);
                    }
                    else // next char is an operator
                    {
                        OperatorNode currentOperator = operatorHandler.CreateOperatorNode(currentCharacter);

                        OperatorNode nextOperator = operatorHandler.CreateOperatorNode(expressionStack.Peek());

                        // if curr operator has > precedence and is right associative
                        if (currentOperator.Precedence > nextOperator.Precedence ||
                           (currentOperator.Precedence == nextOperator.Precedence && currentOperator.Associativity == OperatorNode.Associative.Right))
                        {
                            expressionStack.Push(currentCharacter); // push operator
                        }
                        else // otherwise next operator has < precedence and is left associative
                        {
                            expressionStack.Pop(); // pop operator from the stack

                            postfixList.Add(nextOperator); // add next operator to the list
                            i--;
                        }
                    }
                }
                else if (char.IsDigit(currentCharacter)) // if char is a number
                {
                    string constantString = currentCharacter.ToString(); // create a string to store the number

                    for (int j = i + 1; j < expressionString.Length; j++)
                    {
                        currentCharacter = expressionString[j];

                        if (char.IsDigit(currentCharacter)) // if the next char is a number add it to the string
                        {
                            constantString += currentCharacter.ToString(); // add the number to the string
                            i++;
                        }
                        else // discard as next char is not a number
                        {
                            break;
                        }
                    }

                    postfixList.Add(new ConstantNode(double.Parse(constantString))); // add number to the list
                }
                else // if the current char is a variable
                {
                    string variable = currentCharacter.ToString();
                    for (int j = i + 1; j < expressionString.Length; j++)
                    {
                        currentCharacter = expressionString[j];

                        if (!operatorHandler.ValidOperator(currentCharacter)) // if the char is not an operator
                        {
                            variable += expressionString[j].ToString();
                            i++;
                        }
                        else // discard the rest of the variable
                        {
                            break;
                        }
                    }

                    postfixList.Add(new VariableNode(variable, ref this.variables)); // add the variable to the list

                    if (!this.variableNames.Contains(variable)) // if the variable is not already in the list add it
                    {
                        this.variableNames.Add(variable);
                    }
                }
            }

            while (expressionStack.Count > 0) // while the expressionStack is not empty
            {
                postfixList.Add((OperatorNode)operatorHandler.CreateOperatorNode(expressionStack.Pop())); // add the operator to the list
            }

            return postfixList;
        }
    }
}
