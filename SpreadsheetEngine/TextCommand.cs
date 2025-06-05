// <copyright file="TextCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// command object to change the text of spreadsheet cells.
    /// </summary>
    public class TextCommand : ICommand
    {
        /// <summary>
        /// the cell to change the text of.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// the new text to change the cell to.
        /// </summary>
        private string newText;

        /// <summary>
        /// the old text of the cell.
        /// </summary>
        private string oldText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCommand"/> class.
        /// creates a new CellTextcommand object.
        /// </summary>
        /// <param name="cell"> the cell that text changes.</param>
        /// <param name="newText"> the new text of cell.</param>
        public TextCommand(Cell cell, string newText)
        {
            this.cell = cell;
            this.newText = newText; // set newText to text parameter argument
            this.oldText = cell.Text; // set old text to text of the cell
        }

        /// <summary>
        /// gets the description of the command.
        /// </summary>
        public string Description => "changing cell text";

        /// <summary>
        /// changes the text of the cell to newText.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.newText;
        }

        /// <summary>
        /// changes the text of the cell back to oldText.
        /// </summary>
        public void UnExecute()
        {
            this.cell.Text = this.oldText;
        }
    }
}
