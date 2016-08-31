using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Utility.File_Operations.TXTOperations
{
    public class TXTDuplicateReport : Interfaces.IDuplicateReport
    {
        /// <summary>
        /// Create duplicate report as string list
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<string> DuplicateReport(string file)
        {
            string fileText = FileReader.ReadFile(file);
            string[] processedText = fileText.Split("\r\n".ToCharArray());
            List<string> report = new List<string>();
            IEnumerable<string> uniqueStrings = processedText.Where(x => !x.Equals(""));
            Dictionary<string, string> duplicates = new Dictionary<string, string>();

            for (int i = 0; i < uniqueStrings.Count(); i++)
            {
                if (!duplicates.ContainsKey(uniqueStrings.ElementAt(i)) && !uniqueStrings.ElementAt(i).Equals(""))
                {
                    duplicates.Add(uniqueStrings.ElementAt(i), (i + 1).ToString());
                }
                else
                {
                    if (!uniqueStrings.ElementAt(i).Equals(""))
                    {
                        duplicates[uniqueStrings.ElementAt(i)] = duplicates[uniqueStrings.ElementAt(i)] + ", " + (i + 1);
                    }
                }
            }

            foreach (string duplicateKey in duplicates.Keys)
            {
                if (duplicates[duplicateKey].Contains(","))
                {
                    report.Add(String.Format("{0} = apare de {1} ori pe randurile {2}", duplicateKey, duplicates[duplicateKey].Split(",".ToCharArray()).Count(), duplicates[duplicateKey]));
                }
            }
            return report;
        }

        /// <summary>
        /// Execute DuplicateReport for the given files and show the report in textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="inputFiles"></param>
        public void ExecuteReport(TextBox textBox, string[] inputFiles)
        {
            textBox.Clear();
            foreach (string file in inputFiles)
            {
                textBox.AppendText(file + Environment.NewLine);
                List<string> report = new TXTDuplicateReport().DuplicateReport(file);
                textBox.AppendText(String.Join(Environment.NewLine, report));
            }
        }
    }
}
