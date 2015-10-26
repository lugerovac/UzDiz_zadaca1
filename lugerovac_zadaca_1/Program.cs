using lugerovac__zadaca_1_lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryLocation = GetDirectory(Directory.GetCurrentDirectory(), 0);
            if(string.Equals(directoryLocation, "ERROR"))
            {
                Console.WriteLine("Ne može se pronaći direktorij s modulima!");
                Console.ReadLine();
                return;
            }

            List<IFotoCompetitionModule> listOfModules = FotoCompetitionModuleLoader.GetModules(directoryLocation);
            if(listOfModules.Count > 0)
                RunModules(listOfModules);
            else
            {
                Console.WriteLine("No modules exist!");
                Console.ReadLine();
                return;
            }

            Console.ReadLine();
            return;
        }  //Main

        /// <summary>
        /// Rukuje učitanim modulima
        /// </summary>
        /// <param name="listOfModules">Lista učitanih modula</param>
        static void RunModules(List<IFotoCompetitionModule> listOfModules)
        {
            foreach(IFotoCompetitionModule module in listOfModules)
            {
                module.Run();
            }
        }  //RunModules

        /// <summary>
        /// Ova funkcija vraće nam putanju do direktorija Module. 
        /// Ukoliko ju ne nađe, rekurzivno ju traži do maksimalno 3 koraka
        /// </summary>
        /// <param name="directory">Trenutni direktorij koji se provjerava</param>
        /// <param name="step">Korak rekurzije</param>
        /// <returns>Putanju do male Modules ili pogrešku</returns>
        static string GetDirectory(string directory, int step)
        {
            if(Directory.Exists(directory+"\\Modules"))
            {
                return directory + "\\Modules";
            }
            else if(step == 2)
            {
                return "ERROR";
            }
            else
            {
                char delimiter = '\\';
                string[] directories = directory.Split(delimiter);

                string newDirectory = "";
                string lastDir = "";
                foreach(string dir in directories)
                {
                    if (lastDir.Length != 0)
                    {
                        if (newDirectory.Length != 0)
                            newDirectory += "\\";
                        newDirectory += lastDir;
                    }
                    lastDir = dir;
                }
                return GetDirectory(newDirectory, step+1);
            }
        }  //GetDirectory
    }
}
