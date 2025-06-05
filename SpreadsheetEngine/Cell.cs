namespace SpreadsheetEngine
{
    #pragma warning disable SA1401 // FieldsMustBePrivate
    using System;
    using System.ComponentModel;

    /// <summary>
    /// This class represents one cell in the worksheet.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// class constructor.
    /// </remarks>
    /// <param name="rowInd">sets read only value.</param>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// text value of cell.
        /// </summary>
        protected string text = string.Empty;

        /// <summary>
        /// represents the “evaluated” value of the cell.
        /// </summary>
        protected string value = string.Empty;

        /// <summary>
        /// Background color of the cell.
        /// </summary>
        protected uint bgColor = 0xFFFFFFFF;

        private readonly int rowIndex;
        private readonly int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// constructor.
        /// </summary>
        /// <param name="rowInd"> rowindex parameter.</param>
        /// <param name="columnInd">columnindex paramter.</param>
        protected Cell(int rowInd, int columnInd)
        {
            this.text = string.Empty;
            this.value = string.Empty;
            this.bgColor = 0xFFFFFFFF; // white for Default color
            this.rowIndex = rowInd;
            this.columnIndex = columnInd;
        }

        /// <summary>
        /// property event that is fired when the cell's property changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets or sets and sets protected text string.
        /// </summary>
        /// <returns> this.text value.</returns>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text != value) // ignore if the same text
                {
                    this.text = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets protected value string.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// gets or
        /// sets cell background color.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.bgColor;
            }

            set
            {
                if (this.bgColor != value)
                {
                    this.bgColor = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BGColor"));
                }
            }
        }

        /// <summary>
        /// Gets rowIndex.
        /// </summary>
        /// <returns>returns rowIndex.</returns>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets ColumnIndex.
        /// </summary>
        /// <returns>returns columnIndex.</returns>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }
    }
}
