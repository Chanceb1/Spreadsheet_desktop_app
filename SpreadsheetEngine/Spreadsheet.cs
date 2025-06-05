namespace SpreadsheetEngine
{
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Xml;

    /// <summary>
    /// spreadsheet object will serve as a container for a 2D array of cells.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// the 2d array of cells.
        /// </summary>
        private Cell[,] cells;

        /// <summary>
        /// count of columns in spreadsheet.
        /// </summary>
        private int columnCount;

        /// <summary>
        /// count of rows in spreadsheet.
        /// </summary>
        private int rowCount;

        /// <summary>
        /// Undo ICommands stack.
        /// </summary>
        private Stack<ICommand> undoStack;

        /// <summary>
        /// Redo ICommands stack.
        /// </summary>
        private Stack<ICommand> redoStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="columns">Number of columns.</param>
        /// <param name="rows">Number of rows.</param>
        public Spreadsheet(int rows, int columns)
        {
            this.rowCount = rows;
            this.columnCount = columns;

            this.cells = new Cell[rows, columns]; // initialize size of cells

            for (int row = 0; row < rows; row++) // iterate through rows and columns
            {
                for (int col = 0; col < columns; col++)
                {
                    this.cells[row, col] = new CellInstance(row, col); // Create new instance of each cell

                    this.cells[row, col].PropertyChanged += this.OnCellPropertyChanged!; // Subscribe each cell to the PropertyChanged event
                    ((CellInstance)this.cells[row, col]).DependentCellValueChanged += this.OnCellPropertyChanged!; // Subscribe each cell to the DependentCellValueChanged event
                }
            }

            // Initialize the undo/redo stacks
            this.undoStack = new Stack<ICommand>();
            this.redoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// event fired when one of the spreadsheet's cells changes.
        /// </summary>
        public event PropertyChangedEventHandler? CellPropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets returns number of columns.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
        }

        /// <summary>
        /// Gets returns number of rows.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        /// <summary>
        /// returns cell at address.
        /// </summary>
        /// <param name="row">row parameter.</param>
        /// <param name="col">column parameter.</param>
        /// <returns> cell in spreadsheet.</returns>
        public Cell? GetCell(int row, int col)
        {
            if (col > this.ColumnCount || col < 0 || row > this.RowCount || row < 0) // error handling
            {
                return null;
            }
            else if (this.columnCount == 0 && this.RowCount == 0)
            {
                return null;
            }

            return this.cells[row, col];
        }

        /// <summary>
        /// method to save the spreadsheet as an XML file.
        /// should only save cells that have been modified from deafult state.
        /// </summary>
        /// <param name="stream"> the file Stream.</param>
        public void Save(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true; // set XmlWriterSettings indentation to true

            using (XmlWriter writer = XmlWriter.Create(stream, settings)) // Create a new XmlWriter
            {
                writer.WriteStartDocument(); // start XML Document
                writer.WriteStartElement("Spreadsheet"); // Write root element

                for (int row = 0; row < this.rowCount; row++) // Iterate through the spreadsheet cells
                {
                    for (int col = 0; col < this.columnCount; col++)
                    {
                        Cell cell = this.cells[row, col]; // Get the cell at the current row and column

                        if (cell.Text != string.Empty || cell.BGColor != 0xFFFFFFFF) // If the cell has non-deafualt properties
                        {
                            string cellName = (char)(col + 'A') + (row + 1).ToString(); // Get cell name

                            writer.WriteStartElement("Cell"); // Write the Cell element
                            writer.WriteElementString("Name", cellName); // Write cell Name element

                            if (cell.Text != string.Empty)
                            {
                                writer.WriteElementString("Text", cell.Text); // Write the Text element
                            }

                            if (cell.BGColor != 0xFFFFFFFF)
                            {
                                writer.WriteElementString("BGColor", cell.BGColor.ToString()); // Write the BGColor element
                            }

                            writer.WriteEndElement(); // Close the Cell element
                        }
                    }
                }

                writer.WriteEndElement(); // Close the root element
                writer.WriteEndDocument(); // Close the document
            }
        }

        /// <summary>
        /// method to load a spreadsheet from an XML file.
        /// </summary>
        /// <param name="stream"> the file stream.</param>
        public void Load(Stream stream)
        {
            using (XmlReader reader = XmlReader.Create(stream)) // Create a new XmlReader
            {
                while (reader.Read()) // Read XML file
                {
                    if (reader.IsStartElement() && reader.Name == "Cell") // If the current element is a Cell
                    {
                        string cellName = string.Empty;
                        string cellText = string.Empty;
                        uint cellBGColor = 0xFFFFFFFF;

                        while (reader.Read()) // Read the Cell element
                        {
                            if (reader.IsStartElement()) // If the current element is a start element
                            {
                                switch (reader.Name)
                                {
                                    case "Name":
                                        reader.Read(); // Read the element
                                        cellName = reader.Value; // Get the cell name
                                        break;
                                    case "Text":
                                        reader.Read(); // Read the element
                                        cellText = reader.Value; // Get the cell text
                                        break;
                                    case "BGColor":
                                        reader.Read(); // Read the element
                                        cellBGColor = uint.Parse(reader.Value); // Get the cell BGColor
                                        break;
                                }
                            }
                            else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Cell")
                            {
                                break; // Break out of the loop
                            }
                        }

                        int column = cellName[0] - 'A'; // Get the column index
                        int row = int.Parse(cellName.Substring(1)) - 1; // Get the row index

                        if (column >= 0 && column < this.columnCount && row >= 0 && row < this.rowCount) // Check if the cell is in bounds
                        {
                            Cell cell = this.cells[row, column]; // Get the cell at the current row and column

                            if (cellText != string.Empty)
                            {
                                cell.Text = cellText; // Set the cell text
                            }

                            if (cellBGColor != 0xFFFFFFFF)
                            {
                                cell.BGColor = cellBGColor; // Set the cell BGColor
                            }
                        }
                    }
                }
            }

            // clear the undo/redo stacks
            this.undoStack.Clear();
            this.redoStack.Clear();
        }

        /// <summary>
        /// adds an undo ICommand to the undo stack and executes it.
        /// </summary>
        /// <param name="undoCommand"> undo Command.</param>
        public void AddUndo(ICommand undoCommand)
        {
            undoCommand.Execute(); // Execute the Command

            this.undoStack.Push(undoCommand); // Add Command to the undoStack
        }

        /// <summary>
        /// pops then executes undo Command from the undoStack.
        /// then adds Command to redoStack.
        /// </summary>
        public void ExecuteUndoCommand()
        {
            if (this.undoStack.Count > 0) // if the stack isnt empty
            {
                ICommand undo = this.undoStack.Pop(); // Pop the Command
                undo.UnExecute(); // Unexecute the Command

                this.redoStack.Push(undo); // Add the Command to the redoStack
            }
        }

        /// <summary>
        /// pops then executes redo Command from the redoStack.
        /// then adds the Command to redoStack.
        /// </summary>
        public void ExecuteRedoCommand()
        {
            if (this.redoStack.Count > 0) // if the stack isnt empty
            {
                ICommand redo = this.redoStack.Pop(); // Pop the Command
                redo.Execute(); // Execute the Command

                this.undoStack.Push(redo); // Add the Command to the undoStack
            }
        }

        /// <summary>
        /// Returns the count of undo Commands in the undoStack.
        /// </summary>
        /// <returns> size of undoStack. </returns>
        public int GetUndoCount()
        {
            return this.undoStack.Count;
        }

        /// <summary>
        /// Returns the count of redo Commands in the redoStack.
        /// </summary>
        /// <returns> size of redoStack.</returns>
        public int GetRedoCount()
        {
            return this.redoStack.Count;
        }

        /// <summary>
        /// Returns text dscription of the previous undo on top of stack.
        /// </summary>
        /// <returns> gets undo description.</returns>
        public string GetUndoDescription()
        {
            return this.undoStack.Peek().Description;
        }

        /// <summary>
        /// Returns text description of the previous redo on top of stack.
        /// </summary>
        /// <returns> get redo description.</returns>
        public string GetRedoDescription()
        {
            return this.redoStack.Peek().Description;
        }

        /// <summary>
        /// method checks if a cell has circular reference.
        /// checks the dependent cell list of the cell recursively.
        /// </summary>
        /// <param name="cell"> the instance cell.</param>
        /// <param name="visitedCells"> for recusrsion.</param>
        /// <returns> true or false.</returns>
        public bool HasCircularReference(Cell cell, HashSet<Cell>? visitedCells = null)
        {
            if (visitedCells == null)
            {
                visitedCells = new HashSet<Cell>(); // Initialize the visited cells set
            }

            if (visitedCells.Contains(cell))
            {
                return true; // If the cell has already been visited, it's a circular reference
            }

            visitedCells.Add(cell); // Mark the current cell as visited

            if (cell is CellInstance cellInstance) // downcast cell to CellInstance
            {
                foreach (Cell dependent in cellInstance.Dependents) // iterate through dependents
                {
                    if (this.HasCircularReference(dependent, visitedCells)) // recursively check the dependent's dependents
                    {
                        return true; // Return true if there is a circular reference
                    }
                }
            }

            return false; // Return false if there is no circular reference
        }

        /*public bool HasCircularReference(Cell cell)
        {
            if (cell is CellInstance cellInstance) // downcast cell to CellInstance
            {
                foreach (Cell dependent in cellInstance.Dependents) // iterate through dependents
                {
                    if (dependent is CellInstance dependentInstance) // downcast dependent to CellInstance
                    {
                        if (dependentInstance.IsDependentOn(cell)) // check if the dependent is dependent on the cell
                        {
                            return true; // return true if there is a circular reference
                        }
                        else if (this.HasCircularReference(dependent)) // recursively check the dependent's dependents
                        {
                            return true; // return true if there is a circular reference
                        }
                    }
                }
            }

            return false; // return false if there is no circular reference
        }*/

        /// <summary>
        /// if text value of cell has changed update its value.
        /// If bgColor property changed, update its background color.
        /// </summary>
        /// <param name="sender"> object parameter.</param>
        /// <param name="e"> argument parameter.</param>
        private void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<Cell> references = new List<Cell>(); // List that holds cell references
            int badReferences = 0; // count of bad references
            bool circularReference = false; // circular reference helper

            var cell = (CellInstance)sender; // downcast sender to CellInstance

            if (e.PropertyName == "Text" || e.PropertyName == "Value") // If the Text or Value property of cell changed
            {
                if (cell.Text.StartsWith('=')) // value must be computed based on the formula that comes after the ‘=’.
                {
                    if (!this.HasCircularReference(cell)) // Check for circular references
                    {
                        // get value of string after = and replace whitespace
                        string formula = cell.Text.Substring(1).Replace(" ", string.Empty);

                        if (formula != string.Empty) // if formula is not empty
                        {
                            ExpressionTree expression = new ExpressionTree(formula); // create new expression tree

                            foreach (string variable in expression.GetVariableNames()) // iterate through variables
                            {
                                Cell? referencedCell = this.FindCellByName(variable); // find cell by name
                                double textToDouble = 0;

                                if (referencedCell != cell) // self reference check
                                {
                                    if (referencedCell != null) // if cell is found
                                    {
                                        if (referencedCell.Text != string.Empty)
                                        {
                                            try // exception handling
                                            {
                                                textToDouble = double.Parse(referencedCell.Value); // parse value of referenced cell
                                            }
                                            catch
                                            {
                                                throw new System.Exception("Error parsing cell value to double");
                                            }
                                        }

                                        expression.SetVariable(variable, textToDouble); // set variable to value of referenced cell
                                        cell.SubToCellValueChange(referencedCell); // subscribe to the referenced cell
                                        cell.AddDependent(referencedCell);
                                    }
                                    else // when cell is not found
                                    {
                                        badReferences++; // increment badReference count
                                        expression.SetVariable(variable, textToDouble); // set variable to value of referenced cell
                                    }
                                }
                                else // if cell references itself
                                {
                                    circularReference = true; // increment slefReference count
                                    cell.SetValue("!(self reference)"); // set value to self reference
                                }
                            }

                            if (circularReference) // if there's circular reference
                            {
                                cell.SetValue("!(circular reference)"); // set value to self reference
                            }
                            else if (badReferences == 0)
                            {
                                cell.SetValue(expression.Evaluate().ToString()); // set value of cell to evaluated expression
                            }
                            else // when bad reference count is greater than 0
                            {
                                cell.SetValue("!(bad reference)"); // when cell not found
                            }
                        }
                        else// when formula is empty
                        {
                            cell.SetValue("!(empty formula)");
                        }
                    }
                    else // when circular reference is found
                    {
                        cell.SetValue("!(circular reference)"); // set value to circular reference
                    }
                }
                else // If the Text of the cell is not a formula then the value is just set to the text.
                {
                    cell.SetValue(cell.Text);
                }
            }
            else if (e.PropertyName == "BGColor") // If the BGColor property of the cell changed
            {
                // do nothing, event gets passed to form
            }

            this.CellPropertyChanged?.Invoke(sender, e); // Invoke the CellPropertyChanged event
        }

        // Method to find a cell by its name (e.g., "A1", "B2", etc.)
        private Cell? FindCellByName(string cellName)
        {
            int column = cellName[0] - 'A'; // get column Letter
            int row = 0;

            try // exception handling for invalid cell name
            {
                row = int.Parse(cellName.Substring(1)) - 1; // Parse row number
            }
            catch
            {
                return null; // Return null if row number is not a number
            }

            // Check if the cell exists
            if (column >= 0 && column < this.columnCount && row >= 0 && row < this.rowCount)
            {
                // Return the cell if found
                return this.cells[row, column];
            }
            else
            {
                return null; // Return null if cell is out of bounds
            }
        }

        /// <summary>
        /// Represents a cell containing a numeric value.
        /// </summary>
        private class CellInstance : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CellInstance"/> class.
            /// </summary>
            /// <param name="rowInd">The row index of the cell.</param>
            /// <param name="columnInd">The column index of the cell.</param>
            public CellInstance(int rowInd, int columnInd)
                : base(rowInd, columnInd)
            {
            }

            /// <summary>
            /// event fires when the cell's value property changes.
            /// </summary>
            public event PropertyChangedEventHandler CellValueChanged = (sender, e) => { };

            /// <summary>
            /// Event when a dependent cell's value property changed.
            /// </summary>
            public event PropertyChangedEventHandler DependentCellValueChanged = (sender, e) => { };

            /// <summary>
            /// Gets set of dependent cells,keeps track of cells that depends on the instance.
            /// </summary>
            public HashSet<Cell> Dependents { get; } = new HashSet<Cell>();

            public void AddDependent(Cell cell)
            {
                this.Dependents.Add(cell);
            }

            public void RemoveDependent(Cell cell)
            {
                this.Dependents.Remove(cell);
            }

            public bool HasDependents()
            {
                return this.Dependents.Count > 0;
            }

            /// <summary>
            /// checks if cell is dependant on another cell.
            /// </summary>
            /// <param name="cell"> other cell.</param>
            /// <returns> true or false. </returns>
            public bool IsDependentOn(Cell cell)
            {
                return this.Dependents.Contains(cell);
            }

            /// <summary>
            /// clears dependets list.
            /// </summary>
            public void ClearDependents()
            {
                this.Dependents.Clear();
            }

            /// <summary>
            /// sets the value of the cell in abstact base class.
            /// </summary>
            /// <param name="value">cell value.</param>
            public void SetValue(string value)
            {
                this.value = value;
                this.CellValueChanged?.Invoke(this, new PropertyChangedEventArgs("Value")); // fire the CellPropertyChanged event
            }

            /// <summary>
            /// Subscribes this cell to the propertychanged event of another cell.
            /// Used when this cell's expression is dependent on the other cell's value.
            /// </summary>
            /// <param name="cell">Dependee cell.</param>
            public void SubToCellValueChange(Cell cell)
            {
                if (cell is CellInstance cellInstance)
                {
                    cellInstance.CellValueChanged += this.OnDependentCellValueChange!; // Subscribe to the CellValueChanged event
                }
            }

            /// <summary>
            /// Updates the value of this cell if the value of another cell has changed.
            /// </summary>
            /// <param name="sender">Sender object (shoudld be dependee cell).</param>
            /// <param name="e">Event arguments.</param>
            private void OnDependentCellValueChange(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName! == "Value")
                {
                    this.DependentCellValueChanged?.Invoke(this, new PropertyChangedEventArgs("Value")); // Fire the DependentCellValueChanged event
                }
            }
        }
    }
}
