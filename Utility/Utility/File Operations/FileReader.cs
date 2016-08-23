using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utility.File_Operations
{
    public class FileReader
    {
        /// <summary>
        /// Reads the text from the specified file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            string fileText = "";

            StreamReader fileReader = new StreamReader(filePath);
            fileText = fileReader.ReadToEnd();
            fileReader.Dispose();


            return fileText;
        }

        /// <summary>
        /// Reads all the text from the given files
        /// </summary>
        /// <param name="files"></param>
        /// <returns>List of all string across all files</returns>
        public static List<string> ReadFiles(string[] files)
        {
            List<string> allText = new List<string>();

            for (int i = 0; i < files.Count(); i++)
            {
                StreamReader fileReader = new StreamReader(files[i]);
                string fileText = fileReader.ReadToEnd();
                allText.Add(fileText);
                fileReader.Dispose();
            }
            return allText;
        }

    }
}
