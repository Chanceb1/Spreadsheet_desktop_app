namespace ExpressionTreeCodeDemo
{
    using System;
    using SpreadsheetEngine;

    /// <summary>
    /// demo for expression tree functionality.
    /// </summary>
    internal class ETDemo
    {
        /// <summary>
        /// main function.
        /// </summary>
        /// <param name="args"> input parameters.</param>
        public static void Main()
        {
            bool exit = true;

            string menuInput = string.Empty; // for user menu input
            string exppressionInput = "1+5+A1"; // for expression input, default val is 1+5
            ExpressionTree expression = new ExpressionTree(exppressionInput); // create expression tree

            while (exit)
            {
                Console.WriteLine("Menu (Current epxression = \"{0}\") ", exppressionInput);
                Console.WriteLine("  1 = Enter a new Expression");
                Console.WriteLine("  2 = set a variable value");
                Console.WriteLine("  3 = Evaluate Tree");
                Console.WriteLine("  4 = Quit");

                menuInput = Console.ReadLine() !;

                // switch handles menu output.
                switch (menuInput)
                {
                    case "1":
                        Console.Write("Enter a new Expression: ");
                        exppressionInput = Console.ReadLine() !;
                        expression = new ExpressionTree(exppressionInput);
                        break;
                    case "2":
                        Console.Write("Enter Variable name: ");
                        string variableName = Console.ReadLine() !;
                        Console.Write("Enter variable value: ");
                        string varialbeValue = Console.ReadLine() !;
                        expression.SetVariable(variableName, Convert.ToDouble(varialbeValue)); // set variable value
                        break;
                    case "3":
                        Console.WriteLine(expression.Evaluate().ToString()); // evaluate expression
                        break;
                    case "4":
                        Console.WriteLine("Exiting the program...");
                        exit = false; // exit program loop
                        break;
                    default:
                        Console.WriteLine("Invalid Input\n");
                        break;
                }
            }
        }
    }
}
