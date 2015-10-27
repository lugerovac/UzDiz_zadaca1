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
            MainFacade facade = new MainFacade();
            if(!facade.LoadModules())
            {
                Console.WriteLine("Aplikacija se terminira!");
                Console.ReadLine();
                return;
            }
            facade.RunAllModules();

            Console.ReadLine();
            return;
        }  //Main
    } //class Program


    class MainFacade
    {
        List<IFotoCompetitionModule> listOfModules;

        /// <summary>
        /// Učitava module iz mape Moduli
        /// </summary>
        /// <returns>True ako su moduli učitani, False ako nisu</returns>
        public bool LoadModules()
        {
            string directoryLocation = DirectoryLocator.GetDirectory("Modules", Directory.GetCurrentDirectory(), 0, 3);
            if (string.Equals(directoryLocation, "ERROR"))
            {
                Console.WriteLine("Ne može se pronaći direktorij s modulima!");
                return false;
            }

            listOfModules = FotoCompetitionModuleLoader.GetModules(directoryLocation);
            Console.WriteLine(listOfModules.Count.ToString() + " modules have been loaded");
            return true;
        }

        /// <summary>
        /// Pokreće sve učitane module
        /// </summary>
        /// <returns>True ako je sve u redu, False ako moduli ne postoje ili je došlo do pogreške</returns>
        public bool RunAllModules()
        {
            if (!(listOfModules.Count > 0))
            { 
                Console.WriteLine("No modules exist!");
                Console.ReadLine();
                return false;
            }

            foreach (IFotoCompetitionModule module in listOfModules)
            {
                module.Run();
            }

            return true;
        }
    }
}
