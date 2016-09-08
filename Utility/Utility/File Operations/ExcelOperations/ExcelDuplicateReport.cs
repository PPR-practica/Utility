using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
<<<<<<< HEAD
using System.Text.RegularExpressions;
=======
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
>>>>>>> parent of 756b508... stuff

namespace Utility.File_Operations.ExcelOperations
{
    public class ExcelDuplicateReport : Interfaces.IDuplicateReport
    {
        ExcelReader excelReader = new ExcelReader();
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
            DataTable table = excelReader.ReadExcelFile(sheetName, file);
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

            // create duplicate report
            foreach (object[] duplicateObject in duplicates.Keys)
            {
                if (duplicates[duplicateObject].Contains(","))
                {
                    report.Add(String.Format("{0} = apare de {1} ori pe randurile {2}", String.Join(" | ", duplicateObject.Select(o => o.ToString())), duplicates[duplicateObject].Split(",".ToCharArray()).Count(), duplicates[duplicateObject]));
                }
            }
            }

    */
            return report;
        }

<<<<<<< HEAD
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

        /// <summary>
        /// Create duplicate report as table
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public DataTable TableDuplicateReport(string file, string sheetName)
=======
        public void ExecuteReport(TextBox textBox, string[] inputFiles)
>>>>>>> parent of 756b508... stuff
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
=======
                    sheetName = sheetName;
                    comm.CommandText = "Select * from [" + sheetName + "]";
                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
>>>>>>> parent of 756b508... stuff
                    {
                        duplicates[(duplicates.Keys.Where(x => x.SequenceEqual(rowsList.ElementAt(i)))).ElementAt(0)] = duplicates[(object[])(duplicates.Keys.Where(x => x.SequenceEqual(rowsList.ElementAt(i)))).ElementAt(0)] + ", " + (i + 1);
                }
            }
        }
            DataTable reportTable = new DataTable("DuplicateReport");
            reportTable.Columns.Add("Line");
            reportTable.Columns.Add("Value");
            reportTable.Columns.AddRange(new DataColumn[duplicates.Keys.Count + 2]); // 1 extra cell for line number and other extra cell for checkbox 

            //create duplicate report as table
            foreach (object[] duplicateRow in duplicates.Keys)
            {
<<<<<<< HEAD
                if (duplicates[duplicateRow].Contains(","))
                {
                    Regex digitRegex = new Regex(@"([\d]+)");
                    foreach (Match duplicateMatch in digitRegex.Matches(duplicates[duplicateRow]))
=======
                String connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
            
            DataTable reportTable = new DataTable("DuplicateReport");
            reportTable.Columns.Add("Value");
            reportTable.Columns.Add("Line");
            reportTable.Columns.AddRange(new DataColumn[duplicates.Keys.Count + 2]); // 1 extra cell for line number and other extra cell for checkbox 

        /// <summary>
        /// Create duplicate report as table
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public DataTable TableDuplicateReport(string file, string sheetName)
        {
            DataTable table = excelReader.ReadExcelFile(sheetName, file);
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
            reportTable.Columns.Add("Line");
            reportTable.Columns.Add("Value");
            reportTable.Columns.AddRange(new DataColumn[duplicates.Keys.Count + 2]); // 1 extra cell for line number and other extra cell for checkbox 

            //create duplicate report as table
            foreach (object[] duplicateRow in duplicates.Keys)
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

        public void ExecuteReport(dynamic excelGridView, string inputFile)
                {
            var excelReport = new ExcelDuplicateReport();
            foreach (string sheet in excelReader.GetExcelSheetNames(inputFile))
                {
                DataTable report = excelReport.TableDuplicateReport(inputFile, sheet);
                excelGridView.ItemsSource = report.DefaultView;
                excelGridView.DataContext = report.DefaultView;

                // will overwrite previous sheet report ^^^^^^^^^^^^^^^^^^^
            }
        }
    }
}
