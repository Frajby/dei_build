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

    }
}
