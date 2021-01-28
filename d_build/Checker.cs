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
        /// <summary>
        /// Třída by měla být zodpovědná za kontrolování souborů nebo např. porovnávaní Hashů atd.
        /// </summary>
        public Checker()
        {

        }

        public Dictionary<string,bool> getCheckedFiles(List<string> dependencies, string folderPath)
        {
            Dictionary<string, bool> retVal = new Dictionary<string, bool>();
            foreach(string dependecy in dependencies)
            {
                if(!retVal.ContainsKey(dependecy))
                    retVal.Add(dependecy, File.Exists(Path.Combine(folderPath,dependecy))); 
            }
            return retVal;
        }

    }
}
