using System.Collections.Generic;
using System.IO;
using Utility.File_Operations.ExcelOperations;
using Utility.File_Operations.TXTOperations;

namespace Utility.File_Operations
{
    public class DuplicateChecker
    {
        delegate void CheckDuplicates(dynamic resultBox, string inputFile);
        private static Dictionary<string, CheckDuplicates> DuplicateCheckers = new Dictionary<string, CheckDuplicates>()
        {
            {".txt", new TXTDuplicateReport().ExecuteReport },
            {".xls", new ExcelDuplicateReport().ExecuteReport },
            {".xlsx", new ExcelDuplicateReport().ExecuteReport }
        };

        public void Check(dynamic resultBox, dynamic gridView, string[] inputFiles)
        {
            foreach(string file in inputFiles)
            {
                string extension = Path.GetExtension(file);
                if(extension.Equals(".txt"))
                {
                    DuplicateCheckers[extension].Invoke(resultBox, file);
                }
                else
                {
                    DuplicateCheckers[extension].Invoke(gridView, file);
                }
            }
        }
    }
}
