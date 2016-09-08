using System.Collections.Generic;

namespace Utility.Interfaces
{
    public interface IDuplicateReport
    {
        List<string> DuplicateReport(string file);
        void ExecuteReport(dynamic textBox, string inputFile);
    }
}
