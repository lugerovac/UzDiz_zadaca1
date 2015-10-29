using lugerovac__zadaca_1_lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoringProcess
{
    class ScoringProcess : IFotoCompetitionModule
    {
        string ID = "Scoring Process";
        private float priority = 55;

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
            Console.WriteLine("\n\nZapočinje proces bodovanja");
            Competition competition = Competition.GetInstance();
            List<Registration> listOfRegistrations = competition.DownloadRegistrations();
            Disqualify(listOfRegistrations);
            List<Jury> jury = GenerateJury();
            GenerateScores(listOfRegistrations, jury);
            Dictionary<Registration, float> finalScores = new Dictionary<Registration, float>();
            GenerateFinalScores(listOfRegistrations, finalScores);
            VictoryProclamation(competition);
        }

        /// <summary>
        /// Diskvalificira slučajno odabrane prijave
        /// </summary>
        /// <param name="listOfRegistrations">Lista prijavi</param>
        void Disqualify(List<Registration> listOfRegistrations)
        {
            Randomizer rnd = Randomizer.GetInstance();
            int counter = 0;
            foreach(Registration registration in listOfRegistrations)
            {
                int disqualificationHandler = rnd.GetNumber(0, 21);
                if (disqualificationHandler < 2)
                {
                    registration.Disqualify();
                    counter++;
                    Console.WriteLine("Prijava " + registration.ID + " od " + registration.Competitor.Name + " je diskvalificirana!");
                }
            }
            Console.WriteLine(counter.ToString() + " prijavi je diskvalificirano");
        }

        /// <summary>
        /// Generira članove žiria
        /// </summary>
        /// <returns>Lista članova žiria</returns>
        List<Jury> GenerateJury()
        {
            ArgumentHandler arguments = ArgumentHandler.GetInstance();
            int juryNumber = (int)arguments.GetArgument("JuryNumber");

            #region Pronalazak direktorija s imenima i prezimenima
            string namesLocation = DirectoryLocator.GetDirectory("Person Names", Directory.GetCurrentDirectory(), 0, 3);
            string surnamesLocation = DirectoryLocator.GetDirectory("Person Surnames", Directory.GetCurrentDirectory(), 0, 3);
            #endregion

            #region Učitavanje listi imena i perzimena
            List<string> listOfNames = new List<string>();
            string[] files1 = Directory.GetFiles(namesLocation);
            foreach (string file in files1)
            {
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

            #region Generiranje članova žiria
            List<Jury> jury = new List<Jury>();
            List<string> listOfJuryNames = new List<string>();
            Randomizer rnd = Randomizer.GetInstance();

            while(jury.Count < juryNumber)
            {
                int nameIndex = rnd.GetNumber(0, listOfNames.Count);
                string name = listOfNames[nameIndex];

                int surnameIndex = rnd.GetNumber(0, listOfSurnames.Count);
                string surname = listOfSurnames[surnameIndex];

                string juryMemberName = name + " " + surname;
                if (rnd.GetNumber(0, 7) == 4)
                {
                    int secondSurnameIndex = rnd.GetNumber(0, listOfSurnames.Count);
                    string secondSurname = listOfSurnames[secondSurnameIndex];
                    if (!string.Equals(surname, secondSurname))
                    {
                        juryMemberName = juryMemberName + "-" + secondSurname;
                    }
                }

                if (!listOfJuryNames.Contains(juryMemberName))
                {
                    listOfJuryNames.Add(juryMemberName);
                    Jury juryMember = new Jury(juryMemberName);
                    jury.Add(juryMember);
                }
            }
            #endregion

            #region Ispis članova žiria
            Console.WriteLine("\nČlanovi žiria:");
            foreach (Jury juryMember in jury)
                Console.WriteLine(juryMember.Name);
            #endregion
            return jury;
        }

        /// <summary>
        /// Boduje svaku prijavu
        /// </summary>
        /// <param name="listOfRegistrations">Lista prijava</param>
        /// <param name="jury">Lista članova žiria</param>
        void GenerateScores(List<Registration> listOfRegistrations, List<Jury> jury)
        {
            Randomizer rnd = Randomizer.GetInstance();
            Console.WriteLine("\nIspis bodova po prijavi:");
            foreach (Registration registration in listOfRegistrations)
            {
                if (registration.IsDisqualified())
                    continue;

                Console.WriteLine("\nPrijava " + registration.ID + " od " + registration.Competitor.Name + ":");
                foreach(Jury juryMember in jury)
                {
                    int score = rnd.GetNumber(0, 10);
                    registration.AddScore(juryMember.Name, score);
                    Console.WriteLine(score.ToString() + "/10 od " + juryMember.Name);
                }
                Console.WriteLine("Ukupno: " + registration.GetTotalScore().ToString() + " bodova");
            }
        }

        /// <summary>
        /// Generira bodove ovisno o odabranom algoritmu bodovanja
        /// </summary>
        /// <param name="listOfRegistrations">Lista prijava</param>
        /// <param name="finalScores">Rječnik u koji se spremaju finalni bodovi</param>
        void GenerateFinalScores(List<Registration> listOfRegistrations, Dictionary<Registration, float> finalScores)
        {
            ArgumentHandler arguments = ArgumentHandler.GetInstance();
            string scoringClass = (string)arguments.GetArgument("ScoringClass");

            ScoringFactory scoringFactory;
            if (string.Equals(scoringClass, "SumAllScores"))
                scoringFactory = new SumAllScoresFactory();
            else if (string.Equals(scoringClass, "BalancedScoring"))
                scoringFactory = new BalancedScoringFactory();
            else
                scoringFactory = new ScoreByAverageFactory();

            ScoreAlgorhitm scoringAlgorhitm = scoringFactory.Create();

            Console.WriteLine("\n\nFinal Scores:");
            foreach (Registration registration in listOfRegistrations)
            {
                float finalScore = scoringAlgorhitm.GenerateScore(registration);
                finalScores[registration] = finalScore;
                Console.WriteLine(registration.ID + " od " + registration.Competitor.Name + ": " + finalScore.ToString());
                registration.FinalScore = finalScore;
            }
        }

        /// <summary>
        /// Funkcija koja proglašava pobjednike i stvara datoteku rezultata
        /// </summary>
        /// <param name="competition">Singleton natječaja iz kojeg se vuku podaci</param>
        void VictoryProclamation(Competition competition)
        {
            ArgumentHandler arguments = ArgumentHandler.GetInstance();
            string resultFileName = (string)arguments.GetArgument("ResultFile");
            string directoryLocation = DirectoryLocator.GetDirectory("Results", Directory.GetCurrentDirectory(), 0, 3);
            string resultFile = directoryLocation + "\\" + resultFileName;

            List<string> listOfCategories = competition.DownloadCategories();
            List<Theme> listOfThemes = competition.DownloadThemess();
            List<Registration> listOfRegistrations = competition.DownloadRegistrations();
            List<Competitor> listOfCompetitors = competition.DownloadCompetitors();

            using (StreamWriter sw = new StreamWriter(resultFile))
            {
                #region Pobjednici po temama unutar kategorija
                sw.WriteLine("Pobjednici po temama unutar kategorija".ToUpper());
                foreach (string category in listOfCategories)
                {
                    sw.WriteLine(Indent(0) + "KATEGORIJA " + category + ":");
                    foreach(Theme theme in listOfThemes)
                    {
                        sw.WriteLine(Indent(1) + "TEMA " + theme.Name + ":");
                        List<Registration> disqualifiedRegistrations = new List<Registration>();
                        Dictionary<string, float> victoryCandidatesPerTheme = new Dictionary<string, float>();
                        foreach(Registration registration in listOfRegistrations)
                        {
                            if (!string.Equals(registration.Category, category) || !string.Equals(registration.Theme.Name, theme.Name))
                                continue;

                            if (registration.IsDisqualified())
                            {
                                disqualifiedRegistrations.Add(registration);
                                continue;
                            }

                            sw.Write(Indent(2) + "NATJECATELJ " + registration.Competitor.Name);
                            sw.Write(Indent(1) + registration.ID);
                            foreach(KeyValuePair<string, int> score in registration.GetAllScores())
                                sw.Write(Indent(1) + score.Key + ": " + score.Value.ToString());
                            sw.WriteLine(Indent(1) + "UKUPNO: " + registration.FinalScore.ToString());

                            victoryCandidatesPerTheme.Add(registration.Competitor.Name, registration.FinalScore);
                        }
                        Dictionary<string, float> victorsPerTheme = VictoryHandler.GetVictors(victoryCandidatesPerTheme);
                        Console.WriteLine("\nPobjednici u temi " + theme.Name + ", kategorija " + category + ": ");
                        foreach (KeyValuePair<string, float> score in victorsPerTheme)
                            Console.WriteLine(score.Key + ": " + score.Value.ToString());

                        sw.WriteLine(Indent(3) + "DISKVALIFICIRANE PRIJAVE");
                        foreach (Registration disqualifiedRegistration in disqualifiedRegistrations)
                        {
                            sw.Write(Indent(4) + disqualifiedRegistration.Competitor.Name);
                            sw.WriteLine(Indent(1) + disqualifiedRegistration.ID);
                        }
                    }
                }
                #endregion

                #region Pobjednici po kategorijama
                sw.WriteLine();
                sw.WriteLine("Pobjednici po kategorijama".ToUpper());
                foreach (string category in listOfCategories)
                {
                    sw.WriteLine(Indent(0) + "KATEGORIJA " + category + ":");
                    Dictionary<string, float> victoryCandidatesPerCategory = new Dictionary<string, float>();
                    foreach (Competitor competitor in listOfCompetitors)
                    {
                        sw.WriteLine();
                        sw.WriteLine(Indent(1) + "NATJECATELJ " + competitor.Name + ":");
                        float totalScore = 0;
                        foreach (Registration registration in listOfRegistrations)
                        {
                            if (registration.IsDisqualified() || !string.Equals(registration.Category, category) || !string.Equals(registration.Competitor.Name, competitor.Name))
                                continue;

                            sw.Write(Indent(2) + "TEMA " + registration.Theme.Name);
                            sw.Write(Indent(2) + "PRIJAVA " + registration.ID);
                            sw.WriteLine(Indent(2) + "UKUPNI BODOVI " + registration.GetFinalScore().ToString());
                            totalScore += registration.GetFinalScore();
                        }
                        sw.WriteLine(Indent(1) + "UKUPNI BODOVI SVIH PRIJAVA: " + totalScore.ToString());
                        victoryCandidatesPerCategory.Add(competitor.Name, totalScore);
                    }

                    Dictionary<string, float> victorsPerCategory = VictoryHandler.GetVictors(victoryCandidatesPerCategory);
                    Console.WriteLine("\nPobjednici u kategoriji " + category + ": ");
                    foreach (KeyValuePair<string, float> score in victorsPerCategory)
                        Console.WriteLine(score.Key + ": " + score.Value.ToString());
                }
                #endregion

                #region Ukupni pobjednik
                sw.WriteLine();
                sw.WriteLine("Ukupno".ToUpper());
                Dictionary<string, float> victoryCandidates = new Dictionary<string, float>();
                foreach (Competitor competitor in listOfCompetitors)
                {
                    sw.WriteLine();
                    sw.WriteLine(Indent(0) + "NATJECATELJ " + competitor.Name + ":");
                    float globalScore = 0;
                    foreach(string category in listOfCategories)
                    {
                        float totalScore = 0;
                        foreach (Registration registration in listOfRegistrations)
                        {
                            if (!registration.IsDisqualified() && string.Equals(registration.Category, category) && string.Equals(registration.Competitor.Name, competitor.Name))
                                totalScore += registration.GetFinalScore();
                        }

                        sw.Write(Indent(2) + category);
                        sw.WriteLine(Indent(2) + "UKUPNI BODOVI: " + totalScore.ToString());
                        globalScore += totalScore;
                    }
                    sw.WriteLine(Indent(1) + "UKUPNI BODOVI NATJECATELJA: " + globalScore.ToString());
                    victoryCandidates.Add(competitor.Name, globalScore);
                }

                Dictionary<string, float> victors = VictoryHandler.GetVictors(victoryCandidates);
                Console.WriteLine("\nUkupni pobjednici: ");
                foreach (KeyValuePair<string, float> score in victors)
                    Console.WriteLine(score.Key + ": " + score.Value.ToString());
                #endregion
            }
        }

        /// <summary>
        /// Funkcija za uređivanje uvlačenja u datoteci
        /// </summary>
        /// <param name="count">Razina uvlake</param>
        /// <returns></returns>
        public static string Indent(int count)
        {
            return "".PadLeft(count*3);
        }
    }
}
