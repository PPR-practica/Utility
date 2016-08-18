using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using Utility.Text_Operations;

namespace Utility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //string input = "Igor;Ivan;Albert;Sofia;Mary;Salmonela;Igor;Igor";
            //InitializeComponent();            
        }
        //start, run, services.msc, TabletInputService
        OpenFileDialog ofd = new OpenFileDialog();
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ofd.ShowDialog();
            Stream fileInputStream = ofd.OpenFile();
            StreamReader fileReader = new StreamReader(fileInputStream);
            string fileText = fileReader.ReadToEnd();
            List<string> processedText = TextOperations.Uniquify(TextOperations.SplitToList(fileText, ";"));
                        
            foreach (string line in processedText)
            {
                textBox1.AppendText(line + "\n");
            }
            ///  replace this (put lines in a list or something)
        }
               
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        SaveFileDialog sdf = new SaveFileDialog();       
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool? dialogResult = sdf.ShowDialog();   // true if user chosen file  - null if cancelled 
            if (dialogResult == true)
            {
                List<string> processedText = TextOperations.Uniquify(TextOperations.SplitToList(fileText, ";"));
                foreach(string line in processedText)
                {

                }
            }
            else
            {
                Stream fileInputStream = ofd.OpenFile();
                StreamReader fileReader = new StreamReader(fileInputStream);
                string fileText = fileReader.ReadToEnd();
                if (File.Exists(fileText))
                    File.Delete(fileText);
                FileStream fs = File.Create(fileText);
            }
        }
    }
}