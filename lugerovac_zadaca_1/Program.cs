using lugerovac__zadaca_1_lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac_zadaca_1
{
    #region MainApp
    class Program
    {
        static void Main(string[] args)
        {
            MainFacade facade = new MainFacade();
            if(!facade.HandleArguments(args))
            {
                Console.WriteLine("Aplikacija se terminira!");
                return;
            }

            if(!facade.LoadModules())
            {
                Console.WriteLine("Aplikacija se terminira!");
                return;
            }
            facade.RunAllModules();
        }  //Main
    } //class Program
    #endregion

    #region Facade
    class MainFacade
    {
        List<IFotoCompetitionModule> listOfModules;

        /// <summary>
        /// Ova funkcija učitava argumente iz komandne linije
        /// </summary>
        /// <param name="args">Polje argumenata</param>
        /// <returns>True ako je sve dobro učitano, inače False</returns>
        public bool HandleArguments(string[] args)
        {
            return ArgReader.ReadArgs(args);
        }

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
    #endregion

    static class ArgReader
    {
        /// <summary>
        /// Ova funkcija učitava argumente iz komandne linije
        /// </summary>
        /// <param name="args">Polje argumenata</param>
        /// <returns>True ako je sve dobro učitano, inače False</returns>
        public static bool ReadArgs(string[] args)
        {
            ArgumentHandler arguments = ArgumentHandler.GetInstance();

            try
            {
                if(args.Length < 8)
                {
                    Console.WriteLine("Nisu upisani svi argumenti!");
                    return false;
                }

                #region  Učitaj generator slučajnog broja
                int randomSeed = Int32.Parse(args[0]);
                if (randomSeed < 100)
                {
                    Console.WriteLine("Generator slučajnog broja mora imati bar 3 znamenke!");
                    return false;
                }
                arguments.AddArgument("RandomSeed", "int", randomSeed, true);
                #endregion

                #region  Učitaj maksimalni broj tema
                int maxThemeNumber = Int32.Parse(args[1]);
                if(maxThemeNumber <= 0 || maxThemeNumber > 10)
                {
                    Console.WriteLine("Maksimalni broj tema mora biti u rangu 1-10!");
                    return false;
                }
                arguments.AddArgument("MaxThemeNumber", "int", maxThemeNumber, true);
                #endregion

                #region Učitaj maksimalni broj tema po natjecatelju
                int maxThemeNumberPerCompetitor = Int32.Parse(args[2]);
                if (maxThemeNumberPerCompetitor <= 0 || maxThemeNumberPerCompetitor > maxThemeNumber)
                {
                    Console.WriteLine("Maksimalni broj tema po natjecatelju mora biti veći od 0 i manji ili jedna maksimalnom broju tema!");
                    return false;
                }
                arguments.AddArgument("MaxThemeNumberPerCompetitor", "int", maxThemeNumberPerCompetitor, true);
                #endregion

                #region  Učitaj maksimalni broj kategorija po natjecatelju
                int maxCategoryNumberPerCompetitor = Int32.Parse(args[3]);
                if (maxCategoryNumberPerCompetitor <= 0 || maxCategoryNumberPerCompetitor > 3)
                {
                    Console.WriteLine("Maksimalni broj kategorija po natjecatelju mora biti u rangu 1-3!");
                    return false;
                }
                arguments.AddArgument("MaxCategoryNumberPerCompetitor", "int", maxCategoryNumberPerCompetitor, true);
                #endregion

                #region  Učitaj maksimalni broj natjecatelja
                int maxCompetitorNumber = Int32.Parse(args[4]);
                if (maxCompetitorNumber < 0)
                {
                    Console.WriteLine("Maksimalni broj natjecatelja ne smije biti manji od 0");
                    return false;
                }
                arguments.AddArgument("MaxCompetitorNumber", "int", maxCompetitorNumber, true);
                #endregion

                #region  Učitaj broj članova žiria
                int juryNumber = Int32.Parse(args[5]);
                if (maxCompetitorNumber <= 0)
                {
                    Console.WriteLine("Maksimalni broj kategorija po natjecatelju mora biti veći od 0!");
                    return false;
                }
                arguments.AddArgument("JuryNumber", "int", juryNumber, true);
                #endregion

                #region  Učitaj naziv klase bodovanja
                string scoringClass = args[6];
                arguments.AddArgument("ScoringClass", "string", scoringClass, true);
                #endregion

                #region učitaj naziv datoteke u koju se spremaju svi rezultati i tablice bodova
                string resultFile = args[7];
                arguments.AddArgument("ResultFile", "string", resultFile, true);
                #endregion
            }
            catch
            {
                Console.WriteLine("Došlo je do nepoznate pogreške");
                return false;
            }

            return true;
        }
    }
}
