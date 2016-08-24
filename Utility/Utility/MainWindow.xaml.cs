using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utility.Text_Operations;
using Utility.File_Operations;

namespace Utility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileText;
        string filePath;
        string[] inputFiles;
        List<string> processedText = new List<string>();

        public MainWindow()
        {
        }
        //start, run, services.msc, TabletInputService

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            filePath = FileOperations.GetFilePath();
            try
            {
                fileText = FileReader.ReadFile(filePath);
                saveButton.IsEnabled = true;
            }
            catch(Exception exception)
            {
                // user clicked cancel --->> do nothing
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Please input delimiter!", "Error", MessageBoxButton.OK);
            }
            else
            {
                processedText = TextOperations.Uniquify(TextOperations.SplitToList(fileText, textBox1.Text.ToCharArray()));
                FileWriter.SaveToFile(filePath, textBox1.Text, processedText);
            }
            
        }

        private void chooseFilesButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Clear();
            inputFiles = FileOperations.PickFiles();
            List<string> allText = FileReader.ReadFiles(inputFiles);
            foreach(string file in inputFiles)
            {
                textBox.AppendText(file + "\r\n");
            }
        }

        private void mergeToButton_Click(object sender, RoutedEventArgs e)
        {
            FileOperations.MergeFiles(inputFiles);
            textBox.Clear();
        }

        private void splitButton_Click(object sender, RoutedEventArgs e)
        {
            int outNumber;
            if(inputFiles.Length == 0)
            {
                MessageBox.Show("Please choose file(s)!", "Error", MessageBoxButton.OK);
            }
            else
            {
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
        }
    }
}