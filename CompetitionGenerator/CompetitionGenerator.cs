using lugerovac__zadaca_1_lib;
using System;
using System.Collections.Generic;
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

            int maxNumberOfThemese = (int)arguments.GetArgument("MaxThemeNumber");            
            int numberOfThemes = rnd.GetNumber(1, maxNumberOfThemese);
            Console.WriteLine("Broj tema: " + numberOfThemes);

            int numberOfCategories = rnd.GetNumber(1, 3);
            Console.WriteLine("Broj kategorija: " + numberOfCategories);

            int maxNumberOfCompetitors = (int)arguments.GetArgument("MaxCompetitorNumber");
            int numberOfCompetitors = rnd.GetNumber(0, maxNumberOfCompetitors);
            Console.WriteLine("Broj natjecatelja: " + numberOfCompetitors);
        }
    }
}
