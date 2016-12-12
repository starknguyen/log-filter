using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        private string SidInformation;

        private SIDDefinition objectSID;

        private OrderedDictionary SID_Description = new OrderedDictionary();
        private OrderedDictionary SID_Decimal = new OrderedDictionary();
        private OrderedDictionary SID_Unit = new OrderedDictionary();

        private int DecimalDiv = 1;
        private string OutputUnit;

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
            dialog.Filter = "xls files (*.xls)|*.xls| xlsx files (*.xlsx)|*.xlsx";
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
                }
                else if (tbUnknownSID.Text != string.Empty && Convert.ToInt16(defaultSIDs.SelectedIndex) == -1)
                    SidToFind = tbUnknownSID.Text;  // User enters SID value not listed in ComboBox
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

            try
            {
                for (int i = 1; i < linesLog.Capacity; i++) // ignore first line
                {
                    elements = linesLog[i].Split(',');
                    if (elements.Contains(AbbrevSidToFind))
                    {
                        // Processing output format
                        elements[2] = ExtensionMethods.ShiftDecimalPoint(Convert.ToDouble(elements[2]), DecimalDiv);
                                         
                        logContent.AppendLine(elements[0] + ',' + elements[1] + ',' + elements[2] + " " + OutputUnit);
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
                                        + "_from_" + InputFileName + "_filtered.log";
                    using (StreamWriter file = new StreamWriter(OutputFileLocation))
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
        }
    }
}
