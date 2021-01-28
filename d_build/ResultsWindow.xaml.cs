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

using System.IO;

namespace d_build
{
    /// <summary>
    /// Interakční logika pro ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public List<string> Dependencies { get; set; }
        public List<string> Directories { get; set; }
        public string BuildPath { get; set; }

        public ResultsWindow(List<string> depencencies, List<string> directories, string buildPath)
        {
            InitializeComponent();
            Dependencies = depencencies;
            Directories = directories;
            BuildPath = buildPath;
        }

     

        private async void winResults_Loaded(object sender, RoutedEventArgs e)
        {

            //Tohle bych do budoucna rád chtěl udělat více přehlednější

            Checker checker = new Checker();
            Dictionary<string,bool> results = checker.getCheckedFiles(Dependencies, Directories[0]);

            List<string> filesToCopy = new List<string>();
            foreach(KeyValuePair<string,bool> item in results)
            {
                if(item.Value == false)
                {
                    filesToCopy.Add(item.Key);
                }
            }

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


            listboxSummary.Items.Add("Copying new files now");

            var CopyProgres = new Progress<ProgressStatus>(progress =>
            {
                ProgresBar1.Value = progress.percent;
                listboxSummary.Items.Add(progress.commentary);
            });

            DirWorker dr = new DirWorker();
            foreach(string dir in Directories)
            {
                await dr.copyFilesAsync(filesToCopy, BuildPath, dir, CopyProgres);
            }
        }

        

       
    }
}
