using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;

namespace Utility.File_Operations
{
    public static class FileWriter
    {
        /// <summary>
        /// Save text to selected file or to given default file
        /// </summary>
        /// <param name="defaultFile"></param>
        /// <param name="text"></param>
        public static void SaveToFile(string defaultFile, string delimiter, List<string> text)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            bool? dialogResult = saveFileDialog.ShowDialog();   // true if user chosen file  - null if cancelled 
            string filePath = defaultFile;
            if (dialogResult == true)
            {
                filePath = saveFileDialog.FileName;
                //File.WriteAllLines(filePath, text);

                StreamWriter streamWriter = File.AppendText(filePath);
                foreach (string line in text)
                {
                    streamWriter.Write(line + delimiter);
                }
                streamWriter.Dispose();

            }
            else
            {
                //File.WriteAllLines(filePath, text);

                StreamWriter streamWriter = File.AppendText(filePath);
                foreach (string line in text)
                {
                    streamWriter.Write(line + delimiter);
                }
                streamWriter.Dispose();
            }
        }
    }
}
