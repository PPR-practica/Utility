﻿using Microsoft.Win32;
using System.IO;

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
    }
}
