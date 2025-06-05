// <copyright file="ICommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// ICommand abstract class that uses the ICommand pattern to execute and unexecute ICommands.
    /// </summary>
    public interface ICommand // : ICommand abstract class
    {
        /// <summary>
        /// Gets a description of the ICommand.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Executes the ICommand function.
        /// </summary>
        public void Execute();

        /// <summary>
        /// Unexecutes/redo the Icommand function.
        /// </summary>
        public void UnExecute();
    }
}
