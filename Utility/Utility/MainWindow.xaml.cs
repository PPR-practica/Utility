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

        OpenFileDialog ofd = new OpenFileDialog();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ofd.ShowDialog();
            Stream fileInputStream = ofd.OpenFile();
            StreamReader fileReader = new StreamReader(fileInputStream);
            string fileText = fileReader.ReadToEnd();
            List<string> processedText = TextOperations.Uniquify(TextOperations.SplitToList(fileText, ";"));
            foreach(string line in processedText)
            {
                textBox1.AppendText(line + "\n");
            }

        }

        //start, run, services.msc, TabletInputService

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}