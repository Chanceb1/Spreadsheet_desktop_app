namespace Spreadsheet
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            A = new DataGridViewTextBoxColumn();
            C = new DataGridViewTextBoxColumn();
            D = new DataGridViewTextBoxColumn();
            B = new DataGridViewTextBoxColumn();
            button1 = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveFileToolStripMenuItem = new ToolStripMenuItem();
            loadFileToolStripMenuItem = new ToolStripMenuItem();
            EdittoolStripMenuItem1 = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            CellToolStripMenuItem = new ToolStripMenuItem();
            changeBackgroundColorToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { A, C, D, B });
            dataGridView1.Location = new Point(0, 27);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(800, 424);
            dataGridView1.TabIndex = 0;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            // 
            // A
            // 
            A.HeaderText = "A";
            A.Name = "A";
            // 
            // C
            // 
            C.HeaderText = "C";
            C.Name = "C";
            // 
            // D
            // 
            D.HeaderText = "D";
            D.Name = "D";
            // 
            // B
            // 
            B.HeaderText = "B";
            B.Name = "B";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(707, 0);
            button1.Name = "button1";
            button1.Size = new Size(81, 24);
            button1.TabIndex = 1;
            button1.Text = "Do Demo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, EdittoolStripMenuItem1, CellToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveFileToolStripMenuItem, loadFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveFileToolStripMenuItem
            // 
            saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            saveFileToolStripMenuItem.Size = new Size(121, 22);
            saveFileToolStripMenuItem.Text = "Save File";
            saveFileToolStripMenuItem.Click += saveFileToolStripMenuItem_Click;
            // 
            // loadFileToolStripMenuItem
            // 
            loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            loadFileToolStripMenuItem.Size = new Size(121, 22);
            loadFileToolStripMenuItem.Text = "Load File";
            loadFileToolStripMenuItem.Click += loadFileToolStripMenuItem_Click;
            // 
            // EdittoolStripMenuItem1
            // 
            EdittoolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            EdittoolStripMenuItem1.Name = "EdittoolStripMenuItem1";
            EdittoolStripMenuItem1.Size = new Size(39, 20);
            EdittoolStripMenuItem1.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(103, 22);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(103, 22);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // CellToolStripMenuItem
            // 
            CellToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeBackgroundColorToolStripMenuItem });
            CellToolStripMenuItem.Name = "CellToolStripMenuItem";
            CellToolStripMenuItem.Size = new Size(39, 20);
            CellToolStripMenuItem.Text = "Cell";
            // 
            // changeBackgroundColorToolStripMenuItem
            // 
            changeBackgroundColorToolStripMenuItem.Name = "changeBackgroundColorToolStripMenuItem";
            changeBackgroundColorToolStripMenuItem.Size = new Size(212, 22);
            changeBackgroundColorToolStripMenuItem.Text = "Change background color";
            changeBackgroundColorToolStripMenuItem.Click += changeBackgroundColorToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Spreadsheet App";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn A;
        private DataGridViewTextBoxColumn C;
        private DataGridViewTextBoxColumn D;
        private DataGridViewTextBoxColumn B;
        protected Button button1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem EdittoolStripMenuItem1;
        private ToolStripMenuItem CellToolStripMenuItem;
        private ToolStripMenuItem changeBackgroundColorToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveFileToolStripMenuItem;
        private ToolStripMenuItem loadFileToolStripMenuItem;
    }
}
