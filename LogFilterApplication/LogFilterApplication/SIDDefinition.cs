﻿using System;
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
        private OrderedDictionary SID_Mapping_Decimal = new OrderedDictionary();
        private OrderedDictionary SID_Mapping_Unit = new OrderedDictionary();

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
        /// Method GetSIDMappedDecimal to get SID_Mapping_Decimal
        /// </summary>
        public OrderedDictionary GetSIDMappedDecimal
        {
            get
            {
                return SID_Mapping_Decimal;
            }

            set
            {
                SID_Mapping_Decimal = value;
            }
        }

        /// <summary>
        /// Method GetSIDMappedUnit to get SID_Mapping_Unit
        /// </summary>
        public OrderedDictionary GetSIDMappedUnit
        {
            get
            {
                return SID_Mapping_Unit;
            }

            set
            {
                SID_Mapping_Unit = value;
            }
        }

        /// <summary>
        /// Constructor with length of data as parameter
        /// </summary>
        /// <param name="dataRowLength"></param>
        public SIDDefinition()
        {
            // Data source location
            //string dataSourceLocation = "D:\\Workplace\\Git Repository\\log-filter\\LogFilterApplication\\LogFilterApplication\\Data Sources\\Messstellenliste Renault ZOE.xlsx";
            string dataSourceLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Data Sources\\Messstellenliste Renault ZOE.xlsx";
            int i = 1, j = 1;

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(dataSourceLocation);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            // Total number of rows
            int dataRowLength = xlRange.Cells.Find("*", System.Reflection.Missing.Value,
            System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

            // Total number of columns
            int dataColumnLength = xlRange.Cells.Find("*", System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSearchOrder.xlByColumns, Excel.XlSearchDirection.xlPrevious, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;

            // Variables for position of each data column
            int nrPos = 0;
            int abbrPos = 0;
            int descPos = 0, desPosIndex = 0;
            int sidPos = 0;
            int decPos = 0, decPosIndex = 0;
            int unitPos = 0, unitPosIndex = 0;

            int rowStartData = 1;

            List<string> sidFromExcel = new List<string>();

            try
            {
                for (i = 1; i <= dataRowLength; i++)
                {
                    for (j = 1; j <= dataColumnLength; j++)
                    {
                        if (xlRange.Cells[i, j].Value2 != null)
                        {
                            if (xlRange.Cells[i, j].Value2.Contains("Nr"))
                            {
                                nrPos = j;    // Detect position index of column "Nr.:"
                                for (int k = i; k <= dataRowLength; k++)
                                {
                                    if (xlRange.Cells[k, nrPos].Value2 != null)
                                    {
                                        if (xlRange.Cells[k, nrPos].Value2.ToString().Contains("1"))
                                        {
                                            // Found the first row of data
                                            rowStartData = k;
                                            break; 
                                        }
                                    }
                                }
                            }
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
                            }
                            if (xlRange.Cells[i, j].Value2.Contains("Decimal"))
                            {
                                decPos = j;    // Detect position index of column "Abkürzung"
                            }
                            if (xlRange.Cells[i, j].Value2.Contains("Unit"))
                            {
                                unitPos = j;    // Detect position index of column "Abkürzung"
                            }
                        }
                        
                        if (abbrPos > 0 && sidPos > 0 && nrPos > 0 && descPos > 0 &&
                            decPos > 0 && unitPos > 0)
                            break; // end loop column when these values has been found
                    }
                    if (abbrPos > 0 && sidPos > 0 && nrPos > 0 && descPos > 0 &&
                        decPos > 0 && unitPos > 0)
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
                for (i = rowStartData; i < dataRowLength; i++)
                {
                    if (xlRange.Cells[i, sidPos].Value2 != null && xlRange.Cells[i, abbrPos].Value2 != null && xlRange.Cells[i, descPos].Value2 != null &&
                        xlRange.Cells[i, sidPos].Interior.ColorIndex == 2 && xlRange.Cells[i, abbrPos].Interior.ColorIndex == 2)
                    {
                        // AvailableSIDs = <SID, AbbreviationSID>                     
                        AvailableSIDs.Add(new KeyValuePair<string, string>(xlRange.Cells[i, sidPos].Value2.ToString(), xlRange.Cells[i, abbrPos].Value2.ToString()));                        
                    }
                }

                for (i = rowStartData; i < dataRowLength; i++)
                {
                    if (xlRange.Cells[i, descPos].Value2 != null && xlRange.Cells[i, descPos].Interior.ColorIndex == 2)
                    {
                        // SID_Mapping_Description = <AvailableSIDs, Description>
                        SID_Mapping_Description.Add(AvailableSIDs[desPosIndex], xlRange.Cells[i, descPos].Value2.ToString());
                        desPosIndex++;
                    }
                    if (xlRange.Cells[i, decPos].Value2 != null && xlRange.Cells[i, decPos].Interior.ColorIndex == 2)
                    {
                        // SID_Mapping_Decimal = <Description, Decimal>
                        SID_Mapping_Decimal.Add(xlRange.Cells[i, descPos].Value2.ToString(), xlRange.Cells[i, decPos].Value2.ToString());
                        decPosIndex++;
                    }
                    if (xlRange.Cells[i, unitPos].Value2 != null && xlRange.Cells[i, unitPos].Interior.ColorIndex == 2)
                    {
                        // SID_Mapping_Unit = <Description, Unit>
                        SID_Mapping_Unit.Add(xlRange.Cells[i, descPos].Value2.ToString(), xlRange.Cells[i, unitPos].Value2.ToString());
                        unitPosIndex++;
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
    }
}
