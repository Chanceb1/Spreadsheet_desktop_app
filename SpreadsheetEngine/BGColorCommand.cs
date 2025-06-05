namespace SpreadsheetEngine
{
    /// <summary>
    /// command object to change the background color of spreadsheet cells.
    /// </summary>
    public class BGColorCommand : ICommand
    {
        private List<Cell> cells;

        /// <summary>
        /// The new color to change the cell to.
        /// </summary>
        private uint newColor;

        /// <summary>
        /// The old colors of the cells.
        /// </summary>
        private Dictionary<Cell, uint> oldColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BGColorCommand"/> class.
        /// constuctor for BGColorcommand.
        /// </summary>
        /// <param name="sheet"> list of cells.</param>
        /// <param name="color"> new color of cells.</param>
        public BGColorCommand(List<Cell> sheet, uint color)
        {
            this.cells = sheet;
            this.newColor = color;

            this.oldColor = new Dictionary<Cell, uint>(); // initialize oldColor dictionary

            foreach (Cell cell in this.cells)
            {
                this.oldColor.Add(cell, cell.BGColor);
            }
        }

        /// <summary>
        /// gets description of the command.
        /// </summary>
        public string Description => "changing cell background color";

        /// <summary>
        /// changes the color of the cells to their newColor.
        /// </summary>
        public void Execute()
        {
            foreach (Cell cell in this.cells)
            {
                cell.BGColor = this.newColor;
            }
        }

        /// <summary>
        /// undo's the color change of the cells.
        /// </summary>
        public void UnExecute()
        {
            foreach (Cell cell in this.cells)
            {
                cell.BGColor = this.oldColor[cell];
            }
        }
    }
}
