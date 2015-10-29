using lugerovac__zadaca_1_lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

            #region Generiranje tema
            int maxNumberOfThemes = (int)arguments.GetArgument("MaxThemeNumber");            
            int numberOfThemes = rnd.GetNumber(1, maxNumberOfThemes);
            Console.WriteLine("\nBroj tema: " + numberOfThemes);

            List<Theme> listOfThemes = new List<Theme>();
            listOfThemes = GenerateThemes(numberOfThemes);
            Console.WriteLine("\nOdabrane teme:");
            foreach (Theme theme in listOfThemes)
                Console.WriteLine(theme.Name);
            #endregion

            #region Generiranje kategorija
            int numberOfCategories = rnd.GetNumber(1, 3);
            Console.WriteLine("\nBroj kategorija: " + numberOfCategories);

            List<string> listOfCategories = new List<string>();
            listOfCategories = GenerateCategories(numberOfCategories);
            Console.WriteLine("Odabrane kategorije:");
            foreach (string category in listOfCategories)
                Console.WriteLine(category);
            #endregion
            
            #region Generiranje natjecatelja
            int maxNumberOfCompetitors = (int)arguments.GetArgument("MaxCompetitorNumber");
            int numberOfCompetitors = rnd.GetNumber(0, maxNumberOfCompetitors);
            Console.WriteLine("\nBroj natjecatelja: " + numberOfCompetitors);

            List<Competitor> listOfCompetitors = GenerateCompetitors(numberOfCompetitors);
            Console.WriteLine("\nNatjecatelji:");
            foreach(Competitor competitor in listOfCompetitors)
                Console.WriteLine(competitor.Name);
            #endregion

            #region Generiranje prijava
            int maxThemeNumberPerCompetitor = (int)arguments.GetArgument("MaxThemeNumberPerCompetitor");
            int maxCategoryNumberPerCompetitor = (int)arguments.GetArgument("MaxCategoryNumberPerCompetitor");
            List<Registration> listOfRegistrations = GenerateRegistrations(listOfCompetitors, maxThemeNumberPerCompetitor, maxCategoryNumberPerCompetitor, numberOfThemes, listOfThemes, numberOfCategories, listOfCategories);
            #endregion

            Competition competition = Competition.GetInstance();
            competition.UploadCompetitionData(listOfRegistrations, listOfThemes, listOfCategories, listOfCompetitors);
            Console.WriteLine("Prihvaćeno " + listOfRegistrations.Count.ToString() + " prijava");
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
            List<string> listOfThemeNames = new List<string>();
            string[] files = Directory.GetFiles(directoryLocation);

            foreach (string file in files)
            {
                Console.WriteLine("Učitavam teme iz " + file);
                string[] lines = System.IO.File.ReadAllLines(file);

                foreach(string line in lines)
                {
                    if (listOfThemeNames.Contains(line) || string.Equals(line, ""))
                        continue;

                    Theme newTheme = new Theme(line);
                    listOfCandidates.Add(newTheme);
                    listOfThemeNames.Add(line);
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

        /// <summary>
        /// Funkcija koja određuje u kojim kategorijama se može natjecati
        /// </summary>
        /// <param name="numberOfCategories">Broj kategorija</param>
        /// <returns>Lista kategorija</returns>
        private List<String> GenerateCategories(int numberOfCategories)
        {
            #region Generiraj kandidate
            List<string> listOfCandidates = new List<string>();
            listOfCandidates.Add("DSLR");
            listOfCandidates.Add("MILC");
            listOfCandidates.Add("Compact");
            #endregion

            #region Odaber kategorija za natječaj
            List<string> resultList = new List<string>();
            Randomizer rnd = Randomizer.GetInstance();
            while (resultList.Count < numberOfCategories)
            {
                int categoryIndex = rnd.GetNumber(1, listOfCandidates.Count);
                string randomCategory = listOfCandidates[categoryIndex];
                resultList.Add(randomCategory);
                listOfCandidates.Remove(randomCategory);
            }
            #endregion

            return resultList;
        }

        /// <summary>
        /// Funkcija koja generira natjecatelje,e zajedno sn jihovim imenima i prezimenima
        /// </summary>
        /// <param name="numberOfCompetitors">Broj natjecatelja</param>
        /// <returns>Lista natjecatelja</returns>
        private List<Competitor> GenerateCompetitors(int numberOfCompetitors)
        {
            #region Pronalazaz direktorija s imenima i prezimenima
            string namesLocation = DirectoryLocator.GetDirectory("Person Names", Directory.GetCurrentDirectory(), 0, 3);
            string surnamesLocation = DirectoryLocator.GetDirectory("Person Surnames", Directory.GetCurrentDirectory(), 0, 3);
            if (string.Equals("ERROR", namesLocation) || string.Equals("ERROR", surnamesLocation))
            {
                return null;
            }
            #endregion

            #region Učitavanje listi imena i perzimena
            List<string> listOfNames = new List<string>();
            string[] files1 = Directory.GetFiles(namesLocation);
            foreach (string file in files1)
            {
                Console.WriteLine("Učitavam imena iz " + file);
                string[] lines = System.IO.File.ReadAllLines(file);

                foreach (string line in lines)
                {
                    string name = line;
                    if (listOfNames.Contains(name) || string.Equals(name, ""))
                        continue;
                    listOfNames.Add(name);
                }
            }

            List<string> listOfSurnames = new List<string>();
            string[] files2 = Directory.GetFiles(surnamesLocation);
            foreach (string file in files2)
            {
                Console.WriteLine("Učitavam prezimena iz " + file);
                string[] lines = System.IO.File.ReadAllLines(file);

                foreach (string line in lines)
                {
                    string surname = line;
                    if (listOfSurnames.Contains(surname) || string.Equals(surname, ""))
                        continue;
                    listOfSurnames.Add(surname);
                }
            }
            #endregion

            #region Generiranje natjecatelja
            List<Competitor> competitors = new List<Competitor>();
            List<string> listOfCompetitorNames = new List<string>();
            Randomizer rnd = Randomizer.GetInstance();
            while(competitors.Count < numberOfCompetitors)
            {
                int nameIndex = rnd.GetNumber(0, listOfNames.Count);
                string name = listOfNames[nameIndex];

                int surnameIndex = rnd.GetNumber(0, listOfSurnames.Count);
                string surname = listOfSurnames[surnameIndex];

                string competitorName = name + " " + surname;
                if(rnd.GetNumber(0, 7) == 4)
                {
                    //Dodajemo i drugo prezime
                    int secondSurnameIndex = rnd.GetNumber(0, listOfSurnames.Count);
                    string secondSurname = listOfSurnames[secondSurnameIndex];
                    if(!string.Equals(surname, secondSurname))
                    {
                        competitorName = competitorName + "-" + secondSurname;
                    }
                }

                if (!listOfCompetitorNames.Contains(competitorName))
                {
                    listOfCompetitorNames.Add(competitorName);
                    Competitor competitor = new Competitor(competitorName);
                    competitors.Add(competitor);
                }
            }
            #endregion

            return competitors;
        }

        private List<Registration> GenerateRegistrations(List<Competitor> listOfCompetitors, int maxThemeNumberPerCompetitor, int maxCategoryNumberPerCompetitor, int numberOfThemes, List<Theme> listOfThemes, int numberOfCategories, List<string> listOfCategories)
        {
            Randomizer rnd = Randomizer.GetInstance();
            List<Registration> listOfRegistrations = new List<Registration>();

            Console.WriteLine("\nPrijave natjecatelja:");
            foreach (Competitor competitor in listOfCompetitors)
            {
                Console.WriteLine("\nNatjecatelj: " + competitor.Name);
                int numberOfRegistratedThemes = rnd.GetNumber(1, maxThemeNumberPerCompetitor);
                List<Theme> listOfRegistratedThemes = new List<Theme>();
                Console.WriteLine("Registrirane teme:");
                while (listOfRegistratedThemes.Count < numberOfRegistratedThemes)
                {
                    int themeIndex = rnd.GetNumber(0, numberOfThemes);
                    Theme registratedTheme = listOfThemes[themeIndex];
                    if (listOfRegistratedThemes.Contains(registratedTheme))
                        continue;
                    listOfRegistratedThemes.Add(registratedTheme);
                    Console.WriteLine(registratedTheme.Name);

                    int numberOfRegistratedCategories = rnd.GetNumber(1, maxCategoryNumberPerCompetitor);
                    List<string> listOfregistratedCategories = new List<string>();
                    while (listOfregistratedCategories.Count < numberOfRegistratedCategories)
                    {
                        int categoryIndex = rnd.GetNumber(0, numberOfCategories);
                        string registratedCategory = listOfCategories[categoryIndex];
                        if (listOfregistratedCategories.Contains(registratedCategory))
                            continue;
                        listOfregistratedCategories.Add(registratedCategory);
                        Console.WriteLine("Kategorija: " + registratedCategory);

                        IFotoaparat camera = GetCamera(registratedCategory);
                        Console.WriteLine("Koristi fotoaparat: " + camera.GetModelName());

                        Registration registration = new Registration(competitor, registratedTheme, registratedCategory, camera, new Photo());
                        listOfRegistrations.Add(registration);
                    }
                }
            }

            return listOfRegistrations;
        }

        /// <summary>
        /// Funkcija koja daje određeni model kamere
        /// </summary>
        /// <param name="category">Kategorija kamere</param>
        /// <returns>Objekt IFotoaparat-a</returns>
        private IFotoaparat GetCamera(string category)
        {
            string directoryLocation = DirectoryLocator.GetDirectory("Cameras", Directory.GetCurrentDirectory(), 0, 3);
            if (string.Equals(directoryLocation, "ERROR"))
            {
                Console.WriteLine("Ne može se pronaći direktorij s kamerama!");
            }

            string[] files = Directory.GetFiles(directoryLocation);
            List<IFotoaparat> cameraList = new List<IFotoaparat>();

            foreach (string file in files)
            {
                XmlTextReader reader = new XmlTextReader(file);
                while (reader.Read())
                {
                    if (XmlNodeType.Element == reader.NodeType)
                    {
                        reader.MoveToAttribute("type");
                        string elementType = reader.Value;
                        if (string.Equals(elementType, category))
                        {
                            CameraFactory cameraFactory;
                            if (string.Equals(category, "DSLR"))
                            {
                                cameraFactory = new CreatorDSLR();
                            }
                            else if (string.Equals(category, "MILC"))
                            {
                                cameraFactory = new CreatorMILC();
                            }
                            else
                            {
                                cameraFactory = new CreatorCompact();
                            }

                            IFotoaparat newCamera = cameraFactory.Create();
                            for (int attInd = 0; attInd < reader.AttributeCount; attInd++)
                            {
                                reader.MoveToAttribute(attInd);
                                newCamera.SetValue(reader.Name, reader.Value);
                            }
                            cameraList.Add(newCamera);
                        }
                    }
                }
            }

            Randomizer rnd = Randomizer.GetInstance();
            int cameraIndex = rnd.GetNumber(0, cameraList.Count);
            return cameraList[cameraIndex];
        }
    }

    #region Factory Method
    abstract class CameraFactory
    {
        public abstract IFotoaparat Create();
    }

    class CreatorDSLR : CameraFactory
    {
        public override IFotoaparat Create()
        {
            return new FotoaparatDSLR();
        }
    }

    class CreatorMILC : CameraFactory
    {
        public override IFotoaparat Create()
        {
            return new FotoaparatMILC();
        }
    }

    class CreatorCompact : CameraFactory
    {
        public override IFotoaparat Create()
        {
            return new FotoaparatCompact();
        }
    }
    #endregion
}
