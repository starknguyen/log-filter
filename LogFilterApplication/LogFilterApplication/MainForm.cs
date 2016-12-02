using System;
using System.Collections.Generic;
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
        private string InputFileLocation;
        private string InputFilePath;
        private string InputFileName;
        private string OutputFileLocation;
        private string SidToFind;
        private string SidInformation;

        private StringBuilder logContent;
        #endregion

        SIDDefinition objectSID = new SIDDefinition();

        public MainForm()
        {
            InitializeComponent();            
        }

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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SidToFind = defaultSIDs.SelectedItem.ToString();

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
                    if (elements.Contains(SidToFind))
                    {
                        //Console.WriteLine("Found");                        
                        logContent.AppendLine(elements[0] + ',' + elements[1] + ',' + elements[2]);
                        isElementFound = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while reading log file: " + ex.Message);
            }

            if (isElementFound)
            {
                try
                {
                    objectSID.AvailableSIDs.TryGetValue(SidToFind, out SidInformation);

                    // Output file at same directory of input file
                    OutputFileLocation = InputFilePath + "\\" + SidInformation
                                        + "_from_" + InputFileName + "_filtered.log";
                    using (StreamWriter file = new StreamWriter(OutputFileLocation))
                    {
                        file.Write(logContent);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while writing filtered file: " + ex.Message);
                }

                MessageBox.Show(this, "File has been filtered successfully!", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("SID is not found in this log file");
            }
        }
    }
}
