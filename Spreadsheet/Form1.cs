namespace Spreadsheet
{
    #pragma warning disable SA1300 // ElementMustBeginWithUpperCaseLetter
    // warning disabled because the auto generated event method names cannot be changed or program will not run.
    #pragma warning disable SA1009 // ClosingParenthesisMustBeSpacedCorrectly
    // warning disabled because when copying code the nullability character (!) does not keep formating correctly.
    #pragma warning disable CA1416 // reandom issue after merging with branches.
    using System;
    using System.ComponentModel;
    using System.Diagnostics.Metrics;
    using System.Security.Policy;
    using System.Windows.Forms;
    using Microsoft.VisualBasic.Logging;
    using SpreadsheetEngine;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

    /// <summary>
    /// unnecessary commenting.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// spreadhsheet data.
        /// </summary>
        private Spreadsheet? spreadsheet;

        /// <summary>
        /// Color dialog menu for cell color change.
        /// </summary>
        private ColorDialog? colorDialog;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeSpreadsheet(); // initialize spreadsheet
        }

        /// <summary>
        /// Initializes the spreadsheet for the form.
        /// </summary>
        public void InitializeSpreadsheet()
        {
            this.spreadsheet = new Spreadsheet(50, 26); // (row, column)

            // Clear the columns and rows in the dataGridView.
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();

            // Add columns to the DataGridView
            for (int col = 0; col < this.spreadsheet.ColumnCount; col++)
            {
                this.dataGridView1.Columns.Add("Column" + (col + 1), ((char)('A' + col)).ToString());
            }

            // Add rows to the DataGridView and set row numbers in row headers
            for (int row = 0; row < this.spreadsheet.RowCount; row++)
            {
                this.dataGridView1.Rows.Add(); // Add a new row
                this.dataGridView1.Rows[row].HeaderCell.Value = (row + 1).ToString(); // Set row number in row header
            }

            this.colorDialog = new ColorDialog(); // Initialize the color dialog box
            this.UpdateEditButtons(); // Update the undo and redo buttons

            this.dataGridView1.CellBeginEdit += this.dataGridView1_CellBeginEdit!; // subscribe to the CellBeginEdit event
            this.dataGridView1.CellEndEdit += this.dataGridView1_CellEndEdit!; // Subscribe to CellEndEdit event

            this.spreadsheet.CellPropertyChanged += this.OnSpreadSheetCellPropertyChanged!; // Subscribe to CellPropertyChanged event
        }

        /// <summary>
        /// method to initialize or reset the spreadsheet.
        /// </summary>
        /// <param name="sender">sender.</param> // (object sender, PropertyChangedEventArgs e)
        /// <param name="args">events.</param>
        public void ResetDataGrid()
        {
            this.spreadsheet = new Spreadsheet(50, 26); // (row, column)

            // Clear the columns and rows in the dataGridView.
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();

            // Add columns to the DataGridView
            for (int col = 0; col < this.spreadsheet.ColumnCount; col++)
            {
                this.dataGridView1.Columns.Add("Column" + (col + 1), ((char)('A' + col)).ToString());
            }

            // Add rows to the DataGridView and set row numbers in row headers
            for (int row = 0; row < this.spreadsheet.RowCount; row++)
            {
                this.dataGridView1.Rows.Add(); // Add a new row
                this.dataGridView1.Rows[row].HeaderCell.Value = (row + 1).ToString(); // Set row number in row header
            }

            this.spreadsheet.CellPropertyChanged += this.OnSpreadSheetCellPropertyChanged!;

            this.UpdateEditButtons(); // Update the undo and redo buttons
        }

        /// <summary>
        /// If the value of the SpreadsheetCell changed, updates dataGridView1 cell value.
        /// </summary>
        /// <param name="sender"> spreadsheet cell instance.</param>
        /// <param name="e"> property of the cell.</param>
        private void OnSpreadSheetCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell spreadSheetCell = (Cell)sender; // Get the spreadsheet cell
            DataGridViewCell dataGridCell = this.dataGridView1.Rows[spreadSheetCell.RowIndex].Cells[spreadSheetCell.ColumnIndex]; // Get the corresponding cell in the DataGridView

            if (e.PropertyName == "Text")
            {
                dataGridCell.Value = spreadSheetCell.Value; // set the datagrid value to the spreadsheet cell's value
            }
            else if (e.PropertyName == "Value")
            {
                dataGridCell.Value = spreadSheetCell.Value; // set the datagrid value to the spreadsheet cell's value
            }
            else if (e.PropertyName == "BGColor")
            {
                Color newColor = Color.FromArgb((int)spreadSheetCell.BGColor); // Get the new color

                dataGridCell.Style.BackColor = newColor; // Set the datagrid cell's background color to the new color
            }
        }

        /// <summary>
        /// When a dataGrid cell begins to be edited, set datagrid value to
        /// its corresponding spreadsheet cell's text value.
        /// </summary>
        /// <param name="sender"> instance that raises the event.</param>
        /// <param name="e"> holds the event data.</param>
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell dataGridCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex]; // Get the editing cell
            Cell spreadSheetCell = this.spreadsheet!.GetCell(e.RowIndex, e.ColumnIndex)!; // Get the spreadsheet cell

            dataGridCell.Value = spreadSheetCell.Text; // Set the cell's value to its spreadsheet cell's text value
        }

        /// <summary>
        /// When a dataGrid cell ends editing, set the datagrids value to spreadsheet cells value.
        /// creates a new TextCommand object and adds it to the undo stack.
        /// </summary>
        /// <param name="sender"> instance that raises the event.</param>
        /// <param name="e"> holds the event data.</param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // get the datagrid cell and corresponding spreadsheet cell
            DataGridViewCell dataGridCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Cell spreadSheetCell = this.spreadsheet!.GetCell(e.RowIndex, e.ColumnIndex)!;

            if (dataGridCell.Value == null) // If the cell is null, set the spreadsheet cell's text to empty
            {
                // do nothing
            }
            else
            {
                // Create a new TextCommand object and add it to the undo stack
                ICommand command = new TextCommand(spreadSheetCell, dataGridCell.Value?.ToString()!);
                this.spreadsheet!.AddUndo(command);

                // Set the spreadsheet cell's Text to the edited cell's value
                // spreadSheetCell.Text = dataGridCell.Value?.ToString()!;
                dataGridCell.Value = spreadSheetCell.Value;

                this.UpdateEditButtons(); // Update the undo and redo buttons
            }
        }

        /// <summary>
        /// Saves current spreadsheet as am XML file.
        /// </summary>
        /// <param name="sender">sender instance object.</param>
        /// <param name="e">event arguments.</param>
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog(); // Create SaveFileDialog instance

            saveFileDialog.Filter = "XML files (*.xml)|*.xml"; // Filter for XML files

            if (saveFileDialog.ShowDialog() == DialogResult.OK) // If the user selects a file
            {
                Stream fileStream = saveFileDialog.OpenFile();
                this.spreadsheet!.Save(fileStream);
                fileStream.Close();
            }
        }

        /// <summary>
        /// Loads a spreadsheet from an XML file.
        /// </summary>
        /// <param name="sender">sender instance object.</param>
        /// <param name="e">event arguments.</param>
        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // Create OpenFileDialog instance

            openFileDialog.Filter = "XML files (*.xml)|*.xml"; // filter for XML files

            if (openFileDialog.ShowDialog() == DialogResult.OK) // If the user selects a file
            {
                Stream fileStream = openFileDialog.OpenFile(); // Get the file stream

                this.ResetDataGrid(); // clear spreadsheet and datagrid data before loading file.
                this.spreadsheet!.Load(fileStream); // Load the file
                fileStream.Close(); // Close the file stream
            }
        }

        /// <summary>
        /// changes background color of currently selected cell.
        /// </summary>
        /// <param name="sender"> sender instance. </param>
        /// <param name="e"> event params. </param>
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.LightGoldenrodYellow;
        }

        /// <summary>
        /// executes the undo command.
        /// </summary>
        /// <param name="sender">sender object.</param>
        /// <param name="e">event arguments.</param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet!.ExecuteUndoCommand(); // execute the undo command

            this.UpdateEditButtons(); // update the undo and redo button texts
        }

        /// <summary>
        /// executes the redo command.
        /// </summary>
        /// <param name="sender"> sender object.</param>
        /// <param name="e"> event arguments.</param>
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet!.ExecuteRedoCommand(); // execute the redo command

            this.UpdateEditButtons(); // update the undo and redo button texts
        }

        /// <summary>
        /// method to update the undo and redo button displayed texts.
        /// </summary>
        private void UpdateEditButtons()
        {
            if (this.spreadsheet!.GetUndoCount() > 0) // if there are undos
            {
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo " + this.spreadsheet.GetUndoDescription();
            }
            else // if there are no undos available
            {
                this.undoToolStripMenuItem.Enabled = false;
                this.undoToolStripMenuItem.Text = "Undo";
            }

            if (this.spreadsheet.GetRedoCount() > 0) // if there are redos
            {
                this.redoToolStripMenuItem.Enabled = true;
                this.redoToolStripMenuItem.Text = "Redo " + this.spreadsheet.GetRedoDescription();
            }
            else // if there are no redos available
            {
                this.redoToolStripMenuItem.Enabled = false;
                this.redoToolStripMenuItem.Text = "Redo";
            }
        }

        /// <summary>
        /// event handler for changing the background color of the selected cells.
        /// user selects color from color dialog box menu.
        /// </summary>
        /// <param name="sender"> sender instance.</param>
        /// <param name="e"> event arguments.</param>
        private void changeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.colorDialog!.ShowDialog() == DialogResult.OK) // if the color dialog box is shown
            {
                uint newColor = (uint)this.colorDialog.Color.ToArgb();
                List<Cell> changedCells = new List<Cell>();

                foreach (DataGridViewCell gridCell in this.dataGridView1.SelectedCells) // for each selected cell
                {
                    Cell spreadsheetCell = this.spreadsheet!.GetCell(gridCell.RowIndex, gridCell.ColumnIndex)!;
                    changedCells.Add(spreadsheetCell);
                }

                BGColorCommand command = new BGColorCommand(changedCells, newColor);

                this.spreadsheet!.AddUndo(command); // add undo bgcolor command to the undo stack

                this.UpdateEditButtons(); // update the undo and redo buttons
            }
        }

        /// <summary>
        /// Button performs Spreadsheet Demo.
        /// </summary>
        /// <param name="sender"> instance that raises the event.</param>
        /// <param name="e"> holds the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random(); // random num

            for (int i = 0; i < 50; i++) // 50 random cells
            {
                int rowIndex = random.Next(this.spreadsheet!.RowCount);
                int colIndex = random.Next(this.spreadsheet.ColumnCount);

                this.spreadsheet.GetCell(rowIndex, colIndex)!.Text = "Hello World!";
            }

            // every cell text in column B is set to �This is cell B#�
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet!.GetCell(i, 1)!.Text = "This is cell B" + (i + 1);
            }

            // every cell text in column A is set to �=B#�
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet!.GetCell(i, 0)!.Text = this.spreadsheet!.GetCell(i, 1)!.Text;
            }
        }
    }
}
