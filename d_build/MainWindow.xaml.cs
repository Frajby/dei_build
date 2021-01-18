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
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Path { get; set; }
        public string CheckFilePath { get; set; }
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
            Path = path;
            txtPath.Text = Path;
            showBuilds();
        }

        private void changeCheckFilePath(string path)
        {
            CheckFilePath = path;
            txtCheckFilePath.Text = CheckFilePath;
        }

        private void showBuilds()
        {
            comboSetBuilds.Items.Clear();
            DirWorker dw = new DirWorker();
            List<string> builds = dw.getDirectoriesNames(Path);
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
            List<string> dependencies = dw.getDependencyList(CheckFilePath);
            Checker checker = new Checker();
            string folderPath = Path + "\\" + comboSetBuilds.Text;
            List<string> directories = new List<string>();
            directories.Add(folderPath);

            ResultsWindow resultWinow = new ResultsWindow(dependencies, directories);
            resultWinow.Show();

        }


    }
}
