using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Specialized;

namespace LogFilterApplication
{
    public class SIDDefinition
    {
        private List<KeyValuePair<string, string>> AvailableSIDs = new List<KeyValuePair<string, string>>();
        private OrderedDictionary SID_Mapping_Description = new OrderedDictionary();

        /// <summary>
        /// Method SID_Mapping_Description to get AvailableSIDs
        /// </summary>
        public List<KeyValuePair<string, string>> GetAvailableSIDs
        {
            get
            {
                return this.AvailableSIDs;
            }
            set
            {
                this.AvailableSIDs = value;
            }
        }

        /// <summary>
        /// Method GetSIDMappedDescription to get SID_Mapping_Description
        /// </summary>
        public OrderedDictionary GetSIDMappedDescription
        {
            get
            {
                return this.SID_Mapping_Description;
            }
            set
            {
                this.SID_Mapping_Description = value;
            }
        }

        /// <summary>
        /// Constructor with length of data as parameter
        /// </summary>
        /// <param name="dataRowLength"></param>
        public SIDDefinition(int dataRowLength)
        {
            // Data source location
            string dataSourceLocation = "D:\\Workplace\\Git Repository\\log-filter\\LogFilterApplication\\LogFilterApplication\\Data Sources\\Messstellenliste Renault ZOE.xlsx";
            int i = 1, j = 1;

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(dataSourceLocation);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            int nrPos = 0;
            int abbrPos = 0;
            int descPos = 0;
            int sidPos = 0;

            int rowIndex = 1;

            List<string> sidFromExcel = new List<string>();

            try
            {
                for (i = 1; i <= rowCount; i++)
                {
                    for (j = 1; j <= colCount; j++)
                    {
                        if (xlRange.Cells[i, j].Value2 != null)
                        {
                            if (xlRange.Cells[i, j].Value2.Contains("Abkürzung"))
                            {
                                abbrPos = j;    // Detect position index of column "Abkürzung"
                            }
                            if (xlRange.Cells[i, j].Value2.Contains("Beschreibung"))
                            {
                                descPos = j;    // Detect position index of column "Abkürzung"
                            }
                            if (xlRange.Cells[i, j].Value2.Contains("SID"))
                            {
                                sidPos = j;     // Detect position index of column "SID"
                                rowIndex = i + 1; // Detect position index of row 
                            }
                            if (xlRange.Cells[i, j].Value2.Contains("Nr"))
                            {
                                nrPos = j;    // Detect position index of column "Nr.:"
                                int rowStartData = i + 1;
                                for (i = rowStartData; i < rowCount; i++)
                                {
                                    if (xlRange.Cells[i, nrPos].Value2 == dataRowLength)
                                    {
                                        rowCount = i;
                                        i = rowStartData - 1;
                                        break; // end loop here
                                    }
                                }
                            }
                        }
                        
                        if (abbrPos > 0 && sidPos > 0 && nrPos > 0 && descPos > 0)
                            break; // end loop column when these values has been found
                    }
                    if (abbrPos > 0 && sidPos > 0 && nrPos > 0 && descPos > 0)
                        break; // end loop row when these values has been found
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while reading data source file: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

        // Abkürzung is column abbrPos, SID is column sidPos
        // Store data to KeyValuePair
        // Available SIDs is categorized as "White color" cells in Excel data file
        // For more information about ColorIndex property of Excel cell, read here: 
        // https://msdn.microsoft.com/en-us/library/cc296089%28v=office.12%29.aspx?f=255&MSPPError=-2147217396#xlDiscoveringColorIndex_ColorIndexProperty
            try
            {
                for (i = rowIndex; i < rowCount; i++)
                {
                    if (xlRange.Cells[i, sidPos].Value2 != null && xlRange.Cells[i, abbrPos].Value2 != null && xlRange.Cells[i, descPos].Value2 != null &&
                        xlRange.Cells[i, sidPos].Interior.ColorIndex == 2 && xlRange.Cells[i, abbrPos].Interior.ColorIndex == 2)
                    {
                        //AvailableSIDs.Add(xlRange.Cells[i, sidPos].Value2.ToString(), xlRange.Cells[i, abbrPos].Value2.ToString());                        
                        AvailableSIDs.Add(new KeyValuePair<string, string>(xlRange.Cells[i, sidPos].Value2.ToString(), xlRange.Cells[i, abbrPos].Value2.ToString()));                        
                    }
                }

                j = 0;
                for (i = rowIndex; i < rowCount; i++)
                {
                    if (xlRange.Cells[i, descPos].Value2 != null && xlRange.Cells[i, descPos].Interior.ColorIndex == 2)
                    {
                        SID_Mapping_Description.Add(AvailableSIDs[j], xlRange.Cells[i, descPos].Value2.ToString());
                        j++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while processing data source file: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

            try
            {
                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:
                //  never use two dots, all COM objects must be referenced and released individually
                //  ex: [somthing].[something].[something] is bad

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while closing data source file: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void Mapping()
        {

        }
    }
}
