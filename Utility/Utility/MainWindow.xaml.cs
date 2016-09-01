using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utility.Text_Operations;
using Utility.File_Operations;
using Utility.File_Operations.TXTOperations;
using Utility.File_Operations.ExcelOperations;
using System.IO;

namespace Utility
{    
    public partial class MainWindow : Window
    {
        string fileText;
        string filePath;
        string[] inputFiles;
        List<string> processedText = new List<string>();

        public MainWindow()
        {
        }
       
        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            filePath = FileOperations.GetFilePath();
            try
            {
                fileText = FileReader.ReadFile(filePath);
                saveButton.IsEnabled = true;
            }
            catch (Exception exception)
            {
                // user clicked cancel --->> do nothing
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            if(separatorTextBox.Text == "")
            {
                MessageBox.Show("Please input delimiter!", "Error", MessageBoxButton.OK);
            }
            else
            {
                processedText = TextOperations.Uniquify(TextOperations.SplitToList(fileText, separatorTextBox.Text.ToCharArray()));
                FileWriter.SaveToFile(filePath, separatorTextBox.Text, processedText);
            }            
        }

        private void chooseFilesButton_Click(object sender, RoutedEventArgs e)
        {
            reportTextBox.Clear();
            inputFiles = FileOperations.PickFiles();
            List<string> allText = FileReader.ReadFiles(inputFiles);
            foreach (string file in inputFiles)
            {
                reportTextBox.AppendText(file + "\r\n");
            }
        }

        private void mergeToButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int? fileNumber = inputFiles.Length;
                if (fileNumber != null)
                {
                    FileOperations.MergeFiles(inputFiles);
                    reportTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Please choose file(s)!", "Error", MessageBoxButton.OK);
                }
            }
            catch
            {
                MessageBox.Show("Please choose file(s)!", "Error", MessageBoxButton.OK);
            }

        }

        private void splitButton_Click(object sender, RoutedEventArgs e)
        {
            int outNumber;
            if (int.TryParse(linesNumberTextBox.Text, out outNumber))
            {
                foreach (string file in inputFiles)
                {
                    FileOperations.Split(file, int.Parse(linesNumberTextBox.Text));
                }
            }
            else
            {
                MessageBox.Show("Please input number of lines!", "Error", MessageBoxButton.OK);
            }

        }

        private void checkDuplicatesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (string file in inputFiles)
                {
                    switch (Path.GetExtension(file))
                    {
                        case (".txt"):
                            {
                                new TXTDuplicateReport().ExecuteReport(reportTextBox, new string[1] { file });
                                break;
                            }
                        case (".xls"):
                            {
                                new ExcelDuplicateReport().ExecuteReport(reportTextBox, new string[1] { file });
                                break;
                            }
                        case (".xlsx"):
                            {
                                new ExcelDuplicateReport().ExecuteReport(reportTextBox, new string[1] { file });
                                break;
                            }

                    }
                }
                /// ^ make delegate???
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Please choose file(s)!", "Error", MessageBoxButton.OK);
            }
        }
    }
}