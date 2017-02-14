using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace LogFilterApplication
{
    public partial class MainForm : Form
    {
        #region Variables declaration
        private string DataSourceFileLocation;
        private string InputFileLocation;
        private string InputFilePath;
        private string InputFileName;
        private string OutputFileLocation;
        private string SidToFind;
        private string AbbrevSidToFind;
        private string SidNumberToFind;
        private string SidInformation;

        private SIDDefinition objectSID;

        private OrderedDictionary SID_Description = new OrderedDictionary();
        private OrderedDictionary SID_Decimal = new OrderedDictionary();
        private OrderedDictionary SID_Unit = new OrderedDictionary();

        private int DecimalDiv = -1;
        private string OutputUnit;

        private Excel.Workbook xlWorkBook;
        private Excel.Worksheet xlWorkSheet;
        private Excel.Application xlApp;

        private StringBuilder logContent;        
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            defaultSIDs.Items.Clear();  // clear first
        }

        /// <summary>
        /// Button Browse Data Source click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseDataSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel files (*.xls, *.xlsx)|*.xls; *xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DataSourceFileLocation = dialog.FileName;
                tbDataSourceLocation.Text = dialog.FileName;
                objectSID = new SIDDefinition(DataSourceFileLocation);
                SID_Description = objectSID.GetSIDMappedDescription;
                // SID in ComboBox is the value in SID_Description, we need to know its key
                foreach (DictionaryEntry de in SID_Description)
                {
                    // Add item to ComboBox in format: <Beschreibung> (SID <SID>)
                    // Abbrev_SID = de.Key = <SID, AbbreviationSID>   
                    // Abbrev_SID.Key = SID
                    KeyValuePair<string, string> Abbrev_SID = (KeyValuePair<string, string>)de.Key;
                    string sidNumber = Abbrev_SID.Key; // mapping back                    
                    defaultSIDs.Items.Add(de.Value.ToString() + " (SID " + sidNumber + ")");
                }
            }
        }

        /// <summary>
        /// Button Browse Input Log File click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseInputFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                InputFileLocation = dialog.FileName;
                InputFilePath = Directory.GetParent(InputFileLocation).ToString();
                InputFileName = Path.GetFileNameWithoutExtension(InputFileLocation);
                tbInputFileLocation.Text = InputFileLocation;
            }
        }

        /// <summary>
        /// Button Filter click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            // = 1: comboBox
            // = 2: textBox
            int filterMethod = 0;

            if (tbDataSourceLocation.Text == string.Empty)
            {
                MessageBox.Show(this, "Please choose Excel data source", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            if (tbInputFileLocation.Text == string.Empty)
            {
                MessageBox.Show(this, "Please choose log file to filter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {
                if (tbUnknownSID.Text == string.Empty && Convert.ToInt16(defaultSIDs.SelectedIndex) != -1)
                {
                    FilteringMethod("InFilterList");
                    filterMethod = 1;
                }
                else if (tbUnknownSID.Text != string.Empty && Convert.ToInt16(defaultSIDs.SelectedIndex) == -1)
                {
                    SidToFind = tbUnknownSID.Text;  // User enters SID value not listed in ComboBox
                    filterMethod = 2;
                    //FilteringMethod("NotInFilterList");
                }
                else
                {
                    MessageBox.Show(this, "Please choose filter condition!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
  

            List<string> linesLog = new List<string>();
            linesLog = File.ReadAllLines(InputFileLocation).ToList();
            string[] elements = new string[4];
            logContent = new StringBuilder();
            bool isElementFound = false;

            // Create a new Excel application
            xlApp = new Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show(this, "Excel application was not found!", "Error", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }


            
            object misValue = System.Reflection.Missing.Value;
            int rowIndex = 1;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            try
            {
                for (int i = 1; i < linesLog.Capacity; i++) // ignore first line
                {
                    elements = linesLog[i].Split(',');
                    if (filterMethod == 2)
                    {
                        AbbrevSidToFind = SidToFind; // Not abbrev anymore
                    }
                    if (elements.Contains(AbbrevSidToFind))
                    {
                        // Processing output format
                        elements[2] = ExtensionMethods.ShiftDecimalPoint(Convert.ToDouble(elements[2]), DecimalDiv);

                        // Append to log TEXT file
                        // DateTime, SID, Value, Unit
                        logContent.AppendLine(elements[0] + ',' + elements[1] + ',' + elements[2] + " " + OutputUnit);

                        // Add elements to Excel cells
                        xlWorkSheet.Cells[1, 1] = "Date";
                        xlWorkSheet.Cells[1, 2] = "SID";
                        xlWorkSheet.Cells[1, 3] = "Value";
                        xlWorkSheet.Cells[1, 4] = "Unit";
                        rowIndex++;
                        xlWorkSheet.Cells[rowIndex, 1] = elements[0];
                        xlWorkSheet.Cells[rowIndex, 2] = elements[1];
                        xlWorkSheet.Cells[rowIndex, 3] = elements[2];
                        xlWorkSheet.Cells[rowIndex, 4] = OutputUnit;

                        isElementFound = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error while reading log file: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            if (isElementFound)
            {
                try
                {
                    // Output file at same directory of input file
                    OutputFileLocation = InputFilePath + "\\" + SidToFind
                                        + "_from_" + InputFileName;

                    if (cbFileType.SelectedItem == null)
                    {
                        MessageBox.Show(this, "Please choose output file type!", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (cbFileType.SelectedItem.ToString() == ".csv")
                        OutputFileLocation += "_filtered.csv";
                    else if (cbFileType.SelectedItem.ToString() == ".xls")
                        OutputFileLocation += "_filtered.xls";

                    // Output Excel file with extension .csv
                    xlWorkBook.SaveAs(OutputFileLocation, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp);

                    using (StreamWriter file = new StreamWriter(OutputFileLocation + "_filtered.log"))  // extenstion .log
                    {
                        file.Write(logContent);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error while writing filtered file: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }

                MessageBox.Show(this, "File has been filtered successfully!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show(this, "SID is not found in this log file", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                defaultSIDs.SelectedIndex = -1;
            }

            tbUnknownSID.Text = ""; // Clear SID text box
        }

        /// <summary>
        /// Filter method
        /// </summary>
        /// <param name="method">Whether SID is in Excel data source or not</param>
        private void FilteringMethod(string method)
        {
            switch (method)
            {
                case "NotInFilterList":
                    // User enters SID in textbox
                    // It may be like this: "Karhu 658.33 abcxyz"
                    // Just ignore the invalid part

                    break;

                case "InFilterList":
                    // User chooses SID from ComboBox
                    SidToFind = defaultSIDs.SelectedItem.ToString().
                        Remove(defaultSIDs.SelectedItem.ToString().IndexOf("SID") - 2); // Just take the description, ignore the (SID...)

                    // SID in ComboBox is the value in SID_Description, we need to know its key
                    foreach (DictionaryEntry de in SID_Description)
                    {
                        if (SidToFind == de.Value.ToString())
                        {
                            KeyValuePair<string, string> Abbrev_SID = (KeyValuePair<string, string>)de.Key;
                            AbbrevSidToFind = Abbrev_SID.Key; // mapping back
                            SidInformation = Abbrev_SID.Value;
                            break;
                        }
                    }

                    SID_Decimal = objectSID.GetSIDMappedDecimal;
                    foreach (DictionaryEntry de in SID_Decimal)
                    {
                        if (SidToFind == de.Key.ToString())
                        {
                            DecimalDiv = Convert.ToInt32(de.Value);
                            break;
                        }
                    }

                    SID_Unit = objectSID.GetSIDMappedUnit;
                    foreach (DictionaryEntry de in SID_Unit)
                    {
                        if (SidToFind == de.Key.ToString())
                        {
                            OutputUnit = de.Value.ToString();
                            break;
                        }
                    }
                    break; 
            }            
        }

        /// <summary>
        /// Event happens when closing form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Make sure even if no SID was found, Excel app must be closed
            try
            {
                if (xlWorkBook != null && xlWorkSheet != null && xlApp != null)
                {
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlWorkSheet);
                    Marshal.ReleaseComObject(xlWorkBook);
                    Marshal.ReleaseComObject(xlApp); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error while trying to close application: " + ex, "Error", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }            
        }
    }
}
