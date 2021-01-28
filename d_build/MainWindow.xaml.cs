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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.WindowsAPICodePack.Dialogs;


namespace d_build
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// 
    /// 
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        public string myBuildPath { get; set; }
        public string myCheckFilePath { get; set; }

        public string dumbStationsPath { get; set; } // nahrazuje stanice do kterých se bude kopírovat obsah
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult res = dialog.ShowDialog();
            if (res == CommonFileDialogResult.Ok)
            {
                changePath(dialog.FileName);
            }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            changePath(txtPath.Text);
        }

        private void changePath(string path)
        {
            myBuildPath = path;
            txtPath.Text = myBuildPath;
            showBuilds();
        }

        private void changeCheckFilePath(string path)
        {
            myCheckFilePath = path;
            txtCheckFilePath.Text = myCheckFilePath;
        }

        private void showBuilds()
        {
            comboSetBuilds.Items.Clear();
            DirWorker dw = new DirWorker();
            List<string> builds = dw.getDirectoriesNames(myBuildPath);
            foreach(string s in builds)
            {
                comboSetBuilds.Items.Add(s);
            }
        }

        private void btnChoseCheckFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = false;
            CommonFileDialogResult res = dialog.ShowDialog();
            if(res == CommonFileDialogResult.Ok)
            {
                changeCheckFilePath(dialog.FileName);
            }
        }

        private void txtCheckFilePath_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            changeCheckFilePath(txtCheckFilePath.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DirWorker dw = new DirWorker();
            List<string> dependencies = dw.getDependencyList(myCheckFilePath);
            string buildPath = System.IO.Path.Combine(myBuildPath, comboSetBuilds.Text);
            List<string> directories = new List<string>();
          
            if(lsvStations.SelectedItems.Count > 0)
            {
                foreach(var l in lsvStations.SelectedItems)
                {
                    directories.Add(System.IO.Path.Combine(dumbStationsPath,l.ToString()));
                }
                ResultsWindow resultWinow = new ResultsWindow(dependencies, directories, buildPath);
                resultWinow.Show();
            }
            else
            {
                MessageBox.Show("plese choose at least one dumb workstation");
            }


        }

        private void btnDumpStation_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult res = dialog.ShowDialog();
            if (res == CommonFileDialogResult.Ok)
            {
                dumbStationsPath = dialog.FileName;
                lsvStations.Items.Clear();
                DirWorker dw = new DirWorker();
                List<string> builds = dw.getDirectoriesNames(dialog.FileName);
                foreach (string s in builds)
                {
                    lsvStations.Items.Add(s);
                }
            }


        }
    }
}
