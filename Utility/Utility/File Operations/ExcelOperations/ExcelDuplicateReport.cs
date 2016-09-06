﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Utility.File_Operations.ExcelOperations
{
    public class ExcelDuplicateReport : Interfaces.IDuplicateReport
    {
        public List<string> DuplicateReport(string file)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Create duplicate report as string list
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<string> DuplicateReport(string file, string sheetName)
        {
            DataTable table = ReadExcelFile(sheetName, file);
            List<object[]> rowsList = new List<object[]>();
            List<string> report = new List<string>(); // toate liniile (in afara de prima fmm)
            Dictionary<object[], string> duplicates = new Dictionary<object[], string>();

            // get first row of table and add it to rowsList
            object[] firstRow = new object[3];
            for(int columnItemIndex = 0; columnItemIndex < table.Columns.Count; columnItemIndex++)
            {
                firstRow[columnItemIndex] =  table.Columns[columnItemIndex].ToString();
            }
            rowsList.Add(firstRow);

            // add the remaining rows to rowsList
            foreach (DataRow row in table.Rows)
            {
                rowsList.Add(row.ItemArray);
            }

            // iterate over rowsList and get duplicates
            for (int i = 0; i < rowsList.Count(); i++)
            {
                if (!duplicates.Keys.Any(x => x.SequenceEqual(rowsList.ElementAt(i))))
                {
                    duplicates.Add(rowsList.ElementAt(i), (i + 1).ToString());
                }
                else
                {
                    if (!rowsList.ElementAt(i).Equals(null))
                    {
                        duplicates[(duplicates.Keys.Where(x => x.SequenceEqual(rowsList.ElementAt(i)))).ElementAt(0)] = duplicates[(object[])(duplicates.Keys.Where(x => x.SequenceEqual(rowsList.ElementAt(i)))).ElementAt(0)] + ", " + (i + 1);
                    }
                }
            }

            
            // create duplicate report
            foreach (object[] duplicateObject in duplicates.Keys)
            {
                if (duplicates[duplicateObject].Contains(","))
                {
                    report.Add(String.Format("{0} = apare de {1} ori pe randurile {2}", String.Join(" | ", duplicateObject.Select(o => o.ToString())), duplicates[duplicateObject].Split(",".ToCharArray()).Count(), duplicates[duplicateObject]));
                }
            }
            
            /*
            DataTable reportTable = new DataTable("DuplicateReport");
            reportTable.Columns.Add("Value");
            reportTable.Columns.Add("Line");
            reportTable.Columns.AddRange((DataColumn)String.Join(" | ", ));



            //create duplicate report as table
            foreach(object[] duplicateRow in duplicates.Keys)
            {
                if (duplicates[duplicateRow].Contains(","))
                {
                    Regex digitRegex = new Regex(@"([\d]+)");
                    foreach (Match duplicateMatch in digitRegex.Matches(duplicates[duplicateRow]))
                    {
                        //add row to results table
                        // match | duplicateText | checkbox
                        reportTable.Rows.Add(duplicateMatch.Value, duplicateRow);

                    }
                }
            }

    */
            return report;
        }

        /// <summary>
        /// Create duplicate report as string list
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public DataTable TableDuplicateReport(string file, string sheetName)
        {
            DataTable table = ReadExcelFile(sheetName, file);
            List<object[]> rowsList = new List<object[]>();
            List<string> report = new List<string>(); // toate liniile (in afara de prima fmm)
            Dictionary<object[], string> duplicates = new Dictionary<object[], string>();

            // get first row of table and add it to rowsList
            object[] firstRow = new object[3];
            for (int columnItemIndex = 0; columnItemIndex < table.Columns.Count; columnItemIndex++)
            {
                firstRow[columnItemIndex] = table.Columns[columnItemIndex].ToString();
            }
            rowsList.Add(firstRow);

            // add the remaining rows to rowsList
            foreach (DataRow row in table.Rows)
            {
                rowsList.Add(row.ItemArray);
            }

            // iterate over rowsList and get duplicates
            for (int i = 0; i < rowsList.Count(); i++)
            {
                if (!duplicates.Keys.Any(x => x.SequenceEqual(rowsList.ElementAt(i))))
                {
                    duplicates.Add(rowsList.ElementAt(i), (i + 1).ToString());
                }
                else
                {
                    if (!rowsList.ElementAt(i).Equals(null))
                    {
                        duplicates[(duplicates.Keys.Where(x => x.SequenceEqual(rowsList.ElementAt(i)))).ElementAt(0)] = duplicates[(object[])(duplicates.Keys.Where(x => x.SequenceEqual(rowsList.ElementAt(i)))).ElementAt(0)] + ", " + (i + 1);
                    }
                }
            }
            
            DataTable reportTable = new DataTable("DuplicateReport");
            reportTable.Columns.Add("Value");
            reportTable.Columns.Add("Line");
            reportTable.Columns.AddRange(new DataColumn[duplicates.Keys.Count + 2]); // 1 extra cell for line number and other extra cell for checkbox 


            //create duplicate report as table
            foreach(object[] duplicateRow in duplicates.Keys)
            {
                if (duplicates[duplicateRow].Contains(","))
                {
                    Regex digitRegex = new Regex(@"([\d]+)");
                    foreach (Match duplicateMatch in digitRegex.Matches(duplicates[duplicateRow]))
                    {
                        //add row to results table
                        // match | duplicateText | checkbox
                        reportTable.Rows.Add(duplicateMatch.Value, String.Join(" | ", duplicateRow));

                    }
                }
            }
            return reportTable;
        }

        public void ExecuteReport(TextBox textBox, string[] inputFiles)
        {
            textBox.Clear();
            var excelReport = new ExcelDuplicateReport();
            
            foreach (string file in inputFiles)
            {
                foreach (string sheet in GetExcelSheetNames(file))
                {
                    //textBox.AppendText(Environment.NewLine + file + Environment.NewLine);
                    //textBox.AppendText("WorkSheet: " + sheet.TrimEnd('$') + Environment.NewLine);
                    //List<string> report = excelReport.DuplicateReport(file, sheet);
                    //textBox.AppendText(String.Join(Environment.NewLine, report));

                    DataTable report = excelReport.TableDuplicateReport(file, sheet); 
                    // put report to dataview


                }
            }
        }

        private DataTable ReadExcelFile(string sheetName, string path)
        {

            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = path;
                string fileExtension = Path.GetExtension(Import_FileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "]";
                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        dt.Columns[0].ToString();
                        dt.Rows[0].ToString();

                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// This method retrieves the excel sheet names from 
        /// an excel workbook.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <returns>String[]</returns>
        private String[] GetExcelSheetNames(string excelFile)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                String xlsConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
                String xlsxConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFile + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                string connectionString;
                if(Path.GetExtension(excelFile).Equals(".xls"))
                {
                    connectionString = xlsConnectionString;
                }
                else
                {
                    connectionString = xlsxConnectionString;
                }

                objConn = new OleDbConnection(connectionString);

                objConn.Open();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                return excelSheets;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
    }
}
