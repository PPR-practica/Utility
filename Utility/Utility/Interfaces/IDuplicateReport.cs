using System.Collections.Generic;
using System.Windows.Controls;

namespace Utility.Interfaces
{
    public interface IDuplicateReport
    {
        List<string> DuplicateReport(string file);
        void ExecuteReport(TextBox textBox, string[] inputFiles);
    }
}
