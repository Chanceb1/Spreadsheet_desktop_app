// <copyright file="SpreadSheetTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetTests
{
    using System.Reflection; // MethodInfo, Type
    using System.Xml;
    using System.Xml.Serialization;
    using SpreadsheetEngine;

    /// <summary>
    /// tests spreadsheet class.
    /// </summary>
    public class SpreadSheetTests
    {
        /// <summary>
        /// setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// tests spreadhsheet creation for normal case.
        /// </summary>
        [Test]
        public void TestSpreadSheetCreation()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 3);

            Assert.That(spreadsheet.RowCount, Is.EqualTo(5)); // check rows are equal to expected
            Assert.That(spreadsheet.ColumnCount, Is.EqualTo(3));

            // Check that the cells are created
            for (int row = 0; row < spreadsheet.RowCount; row++)
            {
                for (int col = 0; col < spreadsheet.ColumnCount; col++)
                {
                    Assert.That(spreadsheet.GetCell(row, col), Is.Not.Null);
                }
            }
        }

        /// <summary>
        /// Tests that the spreadsheet is being created with the correct number of rows and columns.
        /// covers boundry case.
        /// </summary>
        [Test]
        public void TestSpreadsheetEmpty()
        {
            // Boundary condition
            // returns null for empty spreadsheet
            {
                Spreadsheet spreadsheet = new Spreadsheet(0, 0);

                Assert.That(spreadsheet.RowCount, Is.EqualTo(0));
                Assert.That(spreadsheet.ColumnCount, Is.EqualTo(0));
                Assert.That(spreadsheet.GetCell(0, 0), Is.Null);
            }
        }

        /// <summary>
        /// tests for unexpected out of bounds behaviour.
        /// </summary>
        [Test]
        public void TestInvalidCellAddress()
        {
            Spreadsheet spreadsheet = new Spreadsheet(3, 3);
            Cell outofBoundscell = spreadsheet.GetCell(5, 5) !;
            Cell negativeCell = spreadsheet.GetCell(-3, -5) !;

            // Assert
            Assert.IsNull(outofBoundscell);
            Assert.IsNull(negativeCell);
        }

        /// <summary>
        /// Tests for boundry case of getCell function.
        /// </summary>
        [Test]
        public void TestSpreadsheetBoundry()
        {
            Spreadsheet spreadsheet = new Spreadsheet(4, 5);

            // Boundary condition
            spreadsheet.GetCell(3, 0) !.Text = "A3"; // B3 is GetCell(1, 2)
            Assert.That(spreadsheet.GetCell(3, 1) !.Value, Is.EqualTo(spreadsheet.GetCell(1, 2) !.Value));
        }

        /// <summary>
        /// tests the BGColorCommand excecute for normal case.
        /// </summary>
        [Test]
        public void TestbgColorCommandExecute()
        {
            Spreadsheet spreadsheet = new Spreadsheet(2, 2);

            // Get the list of cells from the spreadsheet
            List<Cell> cells = new List<Cell>
            {
                spreadsheet.GetCell(0, 0) !, // Cell A1
                spreadsheet.GetCell(0, 1) !,  // Cell B1
                spreadsheet.GetCell(1, 0) !, // Cell A2
                spreadsheet.GetCell(1, 1) !,  // Cell B2
            };

            ICommand command = new BGColorCommand(cells, 0xFF0000);
            command.Execute();

            foreach (Cell cell in cells)
            {
                Assert.That(cell.BGColor, Is.EqualTo(0xFF0000)); // Assert that bgcolor of cells is red
            }
        }

        /// <summary>
        /// tests the BGColorCommand unexcecute for normal case.
        /// </summary>
        [Test]
        public void TestbgColorCommandUnexecute()
        {
            Spreadsheet spreadsheet = new Spreadsheet(2, 2);

            // Get the list of cells from the spreadsheet
            List<Cell> cells = new List<Cell>
            {
                spreadsheet.GetCell(0, 0) !, // Cell A1
                spreadsheet.GetCell(0, 1) !,  // Cell B1
                spreadsheet.GetCell(1, 0) !, // Cell A2
                spreadsheet.GetCell(1, 1) !,  // Cell B2
            };

            ICommand command = new BGColorCommand(cells, 0xFF0000); // bgcolor to Red

            command.Execute(); // Execute the command
            command.UnExecute(); // Undo the command

            foreach (Cell cell in cells)
            {
                Assert.That(cell.BGColor, Is.EqualTo(0xFFFFFFFF)); // check background color is now white
            }
        }

        /// <summary>
        /// tests the BGColorCommand for a single cell Boundry case.
        /// </summary>
        [Test]
        public void TestbgColorCommandBoundary()
        {
            Spreadsheet spreadsheet = new Spreadsheet(1, 1);

            Cell singleCell = spreadsheet.GetCell(0, 0) !; // Cell A1

            // Create a BGColorCommand instance with the single cell w/new color
            ICommand command = new BGColorCommand(new List<Cell> { singleCell }, 0x00FF00); //  set bgcolor to Green

            command.Execute();

            Assert.That(singleCell.BGColor, Is.EqualTo(0x00FF00)); // check that the cell's background color is green
        }

        /// <summary>
        /// tests the TextCommand excecute for normal case.
        /// </summary>
        [Test]
        public void TestTextCommandExecute()
        {
            Spreadsheet spreadsheet = new Spreadsheet(2, 2); // Create a spreadsheet

            Cell singleCell = spreadsheet.GetCell(0, 0) !; // Cell A1

            ICommand command = new TextCommand(singleCell, "Hello World!"); // Set the cell's text to "Hello World!"

            command.Execute();

            Assert.That(singleCell.Text, Is.EqualTo("Hello World!")); // check that the cell's text is "Hello World!"
        }

        /// <summary>
        /// tests the TextCommand unexcecute for normal case.
        /// </summary>
        [Test]
        public void TestTextCommandUnexecute()
        {
            Spreadsheet spreadsheet = new Spreadsheet(1, 1); // Create a spreadsheet

            Cell singleCell = spreadsheet.GetCell(0, 0) !; // Cell A1

            ICommand command = new TextCommand(singleCell, "Hello World!"); // Set the cell's text to "Hello World!"

            command.Execute();
            command.UnExecute();

            Assert.That(singleCell.Text, Is.EqualTo(string.Empty)); // check that the cell's text is "Hello World!"
        }

        /// <summary>
        /// Tests for spreadhseet cell bad(self) reference error handling.
        /// </summary>
        [Test]
        public void TestCellBadReference()
        {
            Spreadsheet spreadsheet = new Spreadsheet(2, 2); // Create a spreadsheet

            Cell testCell = spreadsheet.GetCell(0, 0) !; // Cell A1

            testCell.Text = "=A1"; // Set the cell's text to "=A1"

            Assert.That(testCell.Value, Is.EqualTo("!(self reference)")); // check that the cell's value is "!(self reference)"
        }

        /// <summary>
        /// Tests for spreadhseet cell bad reference Boundry error handling.
        /// </summary>
        [Test]
        public void TestCellBadReferenceBoundry()
        {
            Spreadsheet spreadsheet = new Spreadsheet(2, 2); // Create a spreadsheet

            Cell testCellA1 = spreadsheet.GetCell(0, 0) !; // Cell A1
            Cell testCellA2 = spreadsheet.GetCell(0, 1) !; // Cell A2
            Cell testCellB1 = spreadsheet.GetCell(1, 0) !; // Cell B1
            Cell testCellB2 = spreadsheet.GetCell(1, 1) !; // Cell B2

            /*testCellA1.Text = "=B1*2"; // Set the cell's text to eachother
            testCellB1.Text = "=B2*3";
            testCellB2.Text = "=A2*4";
            testCellA2.Text = "=A1*5";*/

            testCellA1.Text = "=B1*2"; // Set the cell's text to eachother
            testCellB1.Text = "=A1+3";

            // Assert.That(testCellA2.Value, Is.EqualTo("!(circular reference)"));
            Assert.That(testCellB1.Value, Is.EqualTo("!(circular reference)")); // check for circular reference error message
        }

        /// <summary>
        /// Tests for spreadhseet cell bad reference Error error handling.
        /// </summary>
        [Test]
        public void TestCellBadReferenceError()
        {
            Spreadsheet spreadsheet = new Spreadsheet(2, 2); // Create a spreadsheet

            Cell testCell = spreadsheet.GetCell(0, 1) !; // Cell B1

            testCell.Text = "=6+Cell*27"; // Set the cell's text to invalid cell reference

            Assert.That(testCell.Value, Is.EqualTo("!(bad reference)")); // check that the cell's value is "!(bad reference)"
        }

        /// <summary>
        /// Tests for spreadhseet cell empty formula Error error handling.
        /// </summary>
        [Test]
        public void TestCellBadReferenceEmptyFormula()
        {
            Spreadsheet spreadsheet = new Spreadsheet(2, 2); // Create a spreadsheet

            Cell testCell = spreadsheet.GetCell(1, 0) !; // Cell A2

            testCell.Text = "="; // Set the cell's text to invalid cell reference

            // check that the cell's value is the correct error message
            Assert.That(testCell.Value, Is.EqualTo("!(empty formula)"));
        }

        // XML Tests /////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Tests the spreadsheet save method for normal case.
        /// </summary>
        [Test]
        public void TestSpreadsheetSave()
        {
            Spreadsheet spreadsheet = new Spreadsheet(50, 26); // create spreadsheet

            // set 2 cells in the spreadsheet to non deault values
            spreadsheet.GetCell(1, 1) !.Text = "save test";
            spreadsheet.GetCell(1, 1) !.BGColor = 0x0000FF;

            spreadsheet.GetCell(2, 3) !.Text = "save test";
            spreadsheet.GetCell(2, 3) !.BGColor = 0x0000FF;

            using (MemoryStream stream = new MemoryStream()) // Create a MemoryStream to hold the XML output
            {
                spreadsheet.Save(stream);

                stream.Position = 0; // read stream from beginning

                using (XmlReader reader = XmlReader.Create(stream))
                {
                    Assert.IsTrue(reader.ReadToFollowing("Cell")); // Check if there is at least one Cell element

                    do // Loop through XML elements
                    {
                        // Check the content of each Cell element
                        Assert.IsTrue(reader.ReadToFollowing("Name"));
                        string cellName = reader.ReadElementContentAsString();
                        Assert.IsTrue(reader.ReadToFollowing("Text") || reader.ReadToFollowing("BGColor"));
                    }
                    while (reader.ReadToFollowing("Cell"));
                }
            }
        }

        /// <summary>
        /// Tests the spreadsheet save method for uninitialized spreadsheet boundry case.
        /// </summary>
        [Test]
        public void TestSpreadsheetSaveBoundry()
        {
            Spreadsheet spreadsheet = new Spreadsheet(50, 26); // create spreadsheet

            // don't set any cells in the spreadsheet to test for unititialized spreadsheet save
            using (MemoryStream stream = new MemoryStream())
            {
                spreadsheet.Save(stream);

                // Convert the MemoryStream to a string for inspection
                string xmlContent = System.Text.Encoding.UTF8.GetString(stream.ToArray());

                // for debugging
                Console.WriteLine("Generated XML content:");
                Console.WriteLine(xmlContent);

                stream.Position = 0; // Reset the stream position to read from the beginning

                using (XmlReader reader = XmlReader.Create(stream))
                {
                    reader.MoveToContent(); // Move to the root element

                    Assert.That(reader.LocalName, Is.EqualTo("Spreadsheet"));

                    Assert.IsFalse(reader.Read()); // check that root has no children
                }
            }
        }

        /// <summary>
        /// Tests the spreadsheet save method for invalid stream parameter error case.
        /// </summary>
        [Test]
        public void TestSpreadsheetSaveError()
        {
            Spreadsheet spreadsheet = new Spreadsheet(50, 26); // create spreadsheet

            try // Call the Save null stream, should throw exception
            {
                spreadsheet.Save(null!);

                Assert.Fail("Expected ArgumentException was not thrown."); // If no exception is thrown, fail the test
            }
            catch (ArgumentException ex) // Assert that the exception message contains the expected error message
            {
                string message = "String containing Value cannot be null. (Parameter 'output')";
                StringAssert.Contains(ex.Message, message);
            }
        }

        /// <summary>
        /// Tests the spreadsheet Load method for normal case.
        /// </summary>
        [Test]
        public void TestSpreadsheetLoad()
        {
            Spreadsheet spreadsheet = new Spreadsheet(50, 26); // create a spreadsheet from the stream
            string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                    <Spreadsheet>
                                        <Cell>
                                            <Name>A1</Name>
                                            <Text>Test Load</Text>
                                            <BGColor>4294902015</BGColor>
                                        </Cell>
                                    </Spreadsheet>";

            // Convert the XML content to a MemoryStream
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlContent);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                spreadsheet.Load(stream);

                // Verify that cell A1 has been updated with the correct text and BGColor
                Assert.That(spreadsheet.GetCell(0, 0) !.Text, Is.EqualTo("Test Load"));
                Assert.That(spreadsheet.GetCell(0, 0) !.BGColor, Is.EqualTo(4294902015));
            }
        }

        /// <summary>
        /// Tests the spreadsheet Load method for boundry case where the xml spreadsheet is empty.
        /// </summary>
        [Test]
        public void TestTestSpreadsheetLoadBoundry()
        {
            Spreadsheet spreadsheet = new Spreadsheet(50, 26); // create a spreadsheet from the stream
            string emptyXmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                <Spreadsheet></Spreadsheet>"; // Empty XML content

            // Convert the XML content to a MemoryStream
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(emptyXmlContent);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                spreadsheet.Load(stream);

                // check that no cells have been updated
                for (int row = 0; row < spreadsheet.RowCount; row++)
                {
                    for (int col = 0; col < spreadsheet.ColumnCount; col++)
                    {
                        Assert.That(spreadsheet.GetCell(row, col) !.Text, Is.EqualTo(string.Empty));
                        Assert.That(spreadsheet.GetCell(row, col) !.BGColor, Is.EqualTo(0xFFFFFFFF));
                    }
                }
            }
        }

        /// <summary>
        /// Tests the spreadsheet Load method for error case when the XML file has invalid elements.
        /// </summary>
        [Test]
        public void TestTestSpreadsheetLoadError()
        {
            Spreadsheet spreadsheet = new Spreadsheet(50, 26); // Create a spreadsheet instance
            string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
                            <Spreadsheet>
                                <Cell>
                                    <Name>A1</Name>
                                    <Text>Test Load</Text>
                                    <BGColor>4294902015</BGColor>
                                </Cell>
                                <InvalidElement>InvalidValue</InvalidElement> <!-- Invalid XML element -->
                            </Spreadsheet>";

            // Convert the XML content to a MemoryStream
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlContent);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                spreadsheet.Load(stream);

                // Verify that the loaded spreadsheet contains only the valid cell
                Assert.That(spreadsheet.GetCell(0, 0)?.Text, Is.EqualTo("Test Load"));
                Assert.That(spreadsheet.GetCell(0, 0)?.BGColor, Is.EqualTo(4294902015U));
            }
        }
    }
}