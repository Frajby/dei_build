using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace d_build
{
    class DirWorker
    {
   
        /// <summary>
        /// Práce se složkami a soubory
        /// </summary>
        public DirWorker()
        {

        }

        public List<string> getDirectoriesNames(string path)
        {
            var dirs = Directory.GetDirectories(path);
            List<string> ret = new List<string>();
            foreach(var d in dirs)
            {
                ret.Add(new DirectoryInfo(d).Name);
            }
            return ret;
        }
        
        public List<string> getFilesInDir(string dirpath)
        {   
            return Directory.GetFiles(dirpath).ToList();   
        }

        public List<string> getDependencyList(string checkFilePath)
        {
            return File.ReadAllLines(checkFilePath).ToList();
        }

        public async Task copyFilesAsync(List<string> files, string source, string destinantion,IProgress<ProgressStatus> progress)
        {
            await Task.Run(() =>
            {
                float numOfFiles = files.Count;
                float current = 0;
                float percentage = 0;
                string commentary = string.Empty;

                foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(source, destinantion));
                }

                foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                {

                    if (!File.Exists(newPath.Replace(source, destinantion)))
                    {
                        try
                        {
                            File.Copy(newPath, newPath.Replace(source, destinantion), true);
                            commentary = $"Copy of {Path.GetFileName(newPath)} done";
                        }
                        catch(Exception e)
                        {
                            commentary = $"Error in {Path.GetFileName(newPath)} - {e.Message} ";
                        }
                        

                    }
                    current++;
                    percentage = (float)(current / numOfFiles) * 100;
                    progress.Report(new ProgressStatus(percentage, commentary));

                }

                //foreach (string file in files)
                //{
                //    string src = Path.Combine(source, file); 
                //    string dst = Path.Combine(destinantion, file);

                //    string commentary = $"Copy {file}..."; 
                //    progress.Report(new ProgressStatus(percentage, commentary));

                //    File.Copy(src, dst, false);

                //    current++;
                //    percentage = (float)(current / numOfFiles) * 100;

                //    commentary = $"Copy of {file} done";
                //    progress.Report(new ProgressStatus(percentage, commentary));

                //}
            });
        }

    }
}
