using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace d_build
{
    class Checker
    {
        public Checker()
        {

        }

        public Dictionary<string,bool> getCheckedFiles(List<string> dependencies, string folderPath)
        {
            Dictionary<string, bool> retVal = new Dictionary<string, bool>();
            foreach(string s in dependencies)
            {
                    retVal.Add(s, isFileExist(s, folderPath)); 
            }
            return retVal;
        }

        private bool isFileExist(string fileToCheck, string dirPath)
        {
            bool retVal = false;
            DirWorker dw = new DirWorker();
            List<string> files = dw.getFilesInDir(dirPath);
            foreach(string s in files)
            {
                string fullFilePath = dirPath + "\\" + fileToCheck;
                if (File.Exists(fullFilePath))
                {
                    retVal = true;
                }
            }
            return retVal;
        }

    }
}
