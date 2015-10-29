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
        }

        public void Disqualify(List<Registration> listOfRegistrations)
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

        public List<Jury> GenerateJury()
        {
            ArgumentHandler arguments = ArgumentHandler.GetInstance();
            int juryNumber = (int)arguments.GetArgument("JuryNumber");

            #region Pronalazaz direktorija s imenima i prezimenima
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

        public void GenerateScores(List<Registration> listOfRegistrations, List<Jury> jury)
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

        public void GenerateFinalScores(List<Registration> listOfRegistrations, Dictionary<Registration, float> finalScores)
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
                Console.WriteLine(registration.ID + " by " + registration.Competitor.Name + ": " + finalScore.ToString());
            }
        }
    }
}
