using lugerovac__zadaca_1_lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionGenerator
{
    class CompetitionGenerator : IFotoCompetitionModule
    {
        private string ID = "Competition Generator";
        private float priority = 25;

        public string getID()
        {
            return ID;
        }

        public float GetPriority()
        {
            return priority;
        }

        public void Run()
        {
            ArgumentHandler arguments = ArgumentHandler.GetInstance();
            Randomizer rnd = Randomizer.GetInstance();

            int maxNumberOfThemes = (int)arguments.GetArgument("MaxThemeNumber");            
            int numberOfThemes = rnd.GetNumber(1, maxNumberOfThemes);
            Console.WriteLine("Broj tema: " + numberOfThemes);

            int numberOfCategories = rnd.GetNumber(1, 3);
            Console.WriteLine("Broj kategorija: " + numberOfCategories);

            int maxNumberOfCompetitors = (int)arguments.GetArgument("MaxCompetitorNumber");
            int numberOfCompetitors = rnd.GetNumber(0, maxNumberOfCompetitors);
            Console.WriteLine("Broj natjecatelja: " + numberOfCompetitors);

            List<Theme> listOfThemes = new List<Theme>();
            listOfThemes = GenerateThemes(numberOfThemes);
            Console.WriteLine("Odabrane teme:");
            foreach (Theme theme in listOfThemes)
                Console.WriteLine(theme.Name);


        }

        /// <summary>
        /// Funkcija koja generira teme za natječaj
        /// </summary>
        /// <param name="numberOfThemes">Broj tema koji treba generirati</param>
        /// <returns>Lista slučajno odabranih tema</returns>
        private List<Theme> GenerateThemes(int numberOfThemes)
        {
            #region Pronalazak direktorija s temama
            string directoryLocation = DirectoryLocator.GetDirectory("Themes", Directory.GetCurrentDirectory(), 0, 3);
            if (string.Equals("ERROR", directoryLocation))
            {
                Console.WriteLine("Niti jedna tema nije učitana!");
                return null;
            }
            #endregion

            #region Učitavanje kandidata za teme
            List<Theme> listOfCandidates = new List<Theme>();
            string[] files = Directory.GetFiles(directoryLocation);

            foreach (string file in files)
            {
                Console.WriteLine("Učitavam teme iz " + file);
                string[] lines = System.IO.File.ReadAllLines(file);

                foreach(string line in lines)
                {
                    Theme newTheme = new Theme(line);
                    listOfCandidates.Add(newTheme);
                }
            }
            #endregion

            #region Odabir tema za natječaj
            List<Theme> resultList = new List<Theme>();
            Randomizer rnd = Randomizer.GetInstance();
            while (resultList.Count < numberOfThemes)
            {
                int themeIndex = rnd.GetNumber(0, listOfCandidates.Count);
                Theme randomTheme = listOfCandidates[themeIndex];
                resultList.Add(randomTheme);
                listOfCandidates.Remove(randomTheme);
            }
            #endregion

            return resultList;
        }
    }
}
