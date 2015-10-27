using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public static class DirectoryLocator
    {
        /// <summary>
        /// Ova funkcija rekurzivno traži željeni direktoriji u zadanom direktoriju i najbližim roditeljima
        /// </summary>
        /// <param name="directory">Direktorij koji tražimo</param>
        /// <param name="path">Putanja u kojoj se traži direktorij</param>
        /// <param name="step">Korak rekurzije</param>
        /// <param name="maxSteps">Maksimalni dopušteni korak rekurzije</param>
        /// <returns>Putanja do direktorija ili kod pogreške</returns>
        public static string GetDirectory(string directory, string path, int step, int maxSteps)
        {
            if (Directory.Exists(path + "\\" + directory))
            {
                return path + "\\" + directory;
            }
            else if (step == maxSteps)
            {
                return "ERROR";
            }
            else
            {
                char delimiter = '\\';
                string[] directories = path.Split(delimiter);

                string newPath = "";
                string lastDir = "";
                foreach (string dir in directories)
                {
                    if (lastDir.Length != 0)
                    {
                        if (newPath.Length != 0)
                            newPath += "\\";
                        newPath += lastDir;
                    }
                    lastDir = dir;
                }
                return GetDirectory(directory, newPath, step + 1, maxSteps);
            }
        }  //GetDirectory
    }
}
