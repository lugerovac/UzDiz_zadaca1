using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class Competition
    {
        private static Competition instance;

        List<Registration> listOfRegistrations;
        List<Theme> listOfThemes;
        List<string> listOfCategories;
        List<Competitor> listOfCompetitors;

        protected Competition()
        {
        }

        public static Competition GetInstance()
        {
            if (instance == null)
                instance = new Competition();

            return instance;
        }

        public void UploadCompetitionData(List<Registration> listOfRegistrations, List<Theme> listOfThemes, List<string> listOfCategories, List<Competitor> listOfCompetitors)
        {
            this.listOfRegistrations = listOfRegistrations;
            this.listOfThemes = listOfThemes;
            this.listOfCategories = listOfCategories;
            this.listOfCompetitors = listOfCompetitors;
        }

        public List<Registration> DownloadRegistrations()
        {
            return listOfRegistrations;
        }

        public List<Theme> DownloadThemess()
        {
            return listOfThemes;
        }

        public List<string> DownloadCategories()
        {
            return listOfCategories;
        }

        public List<Competitor> DownloadCompetitors()
        {
            return listOfCompetitors;
        }
    }
}
