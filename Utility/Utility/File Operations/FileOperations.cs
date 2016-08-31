using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utility.File_Operations;

namespace Utility.Text_Operations
{
    public static class FileOperations
    {
        /// <summary>
        /// Gets the file path of the file selected in dialog
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }

        /// <summary>
        /// Select multiple files and return their path as string[]
        /// </summary>
        /// <returns></returns>
        public static string[] PickFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            bool? dialogResult = openFileDialog.ShowDialog();
            return openFileDialog.FileNames;
        }

        ///
        public static void MergeFiles(string[] inputFiles)
        {
            SaveFileDialog mergeFileDialog = new SaveFileDialog();
            bool? dialogResult = mergeFileDialog.ShowDialog();
            string outputFile = mergeFileDialog.FileName;

            const int chunkSize = 2 * 1024;

            using (var output = File.Create(outputFile))
            {
                foreach (var file in inputFiles)
                {
                    using (var input = File.OpenRead(file))
                    {
                        var buffer = new byte[chunkSize];
                        int bytesRead;
                        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Split the file to multiple files(by lines number)
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="lines"></param>
        public static void Split(string inputFile, int lines)
        {
            int fileNumber = 1;
            int processedLines = 0;
            string filePath = Path.GetPathRoot(inputFile);
            string outputFileName = Path.GetFileNameWithoutExtension(inputFile);
            string extension = Path.GetExtension(inputFile);
            string directory = Path.GetDirectoryName(inputFile);
            string fileText = FileReader.ReadFile(inputFile);
            string outputFile = directory + "\\" + outputFileName + fileNumber + extension;
            string currentLine = "";
            StreamWriter fileWriter;
            StreamReader fileReader = new StreamReader(inputFile);

            File.Create(outputFile).Dispose();

            fileWriter = new StreamWriter(outputFile);
            fileWriter.AutoFlush = true;
            while ((currentLine = fileReader.ReadLine()) != null)
            {
                if (processedLines < lines)
                {
                    fileWriter.WriteLine(currentLine);
                    processedLines++;
                }
                else
                {
                    fileNumber++;
                    outputFile = directory + "\\" + outputFileName + fileNumber + extension;
                    File.Create(outputFile).Dispose();
                    fileWriter = new StreamWriter(outputFile);
                    fileWriter.AutoFlush = true;
                    processedLines = 0;
                    fileWriter.WriteLine(currentLine);
                    processedLines++;
                }
            }
            fileWriter.Dispose();
        }
    }
}
