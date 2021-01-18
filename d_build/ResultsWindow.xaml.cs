using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace d_build
{
    /// <summary>
    /// Interakční logika pro ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public List<string> Dependencies { get; set; }
        public List<string> Directories { get; set; }

        public ResultsWindow(List<string> depencencies, List<string> directories)
        {
            InitializeComponent();
            Dependencies = depencencies;
            Directories = directories;

        }

        private void winResults_Loaded(object sender, RoutedEventArgs e)
        {
            Checker checker = new Checker();
            Dictionary<string,bool> results = checker.getCheckedFiles(Dependencies, Directories[0]);

            int fileNotExist = 0;
            int fileExist = 0;

            listboxSummary.Items.Add("\r\n-----Summary-----\r\n");
            foreach (KeyValuePair<string,bool> res in results)
            {
                if (res.Value == true)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Foreground = new SolidColorBrush(Colors.Blue);
                    item.Content = res.Key + "\t\t -OK";
                    listboxSummary.Items.Add(item);
                    fileExist++;
                }
                else
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Foreground = new SolidColorBrush(Colors.Red);
                    item.Content = res.Key + "\t\t -not OK";
                    listboxSummary.Items.Add(item);
                    fileNotExist++;
                }
               
            }
            listboxSummary.Items.Add("-----------------");
            listboxSummary.Items.Add("\r\n");
            listboxSummary.Items.Add("Exist:\t\t" + fileExist);
             listboxSummary.Items.Add("Not Exist:\t\t" + fileNotExist);
        }

        private void txtRichSummary_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
