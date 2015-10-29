using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class Registration
    {
        private string id;
        public string ID
        {
            get { return id; }
        }

        private Competitor competitor;
        public Competitor Competitor
        {
            get { return competitor; }
        }

        private Theme theme;
        public Theme Theme
        {
            get { return theme; }
        }

        private string category;
        public string Category
        {
            get { return category; }
        }

        private IFotoaparat camera;
        public IFotoaparat Camera
        {
            get { return camera; }
        }

        private Photo photo;
        private Photo Photo
        {
            get { return photo; }
        }

        Dictionary<string, int> scores = new Dictionary<string, int>();

        private bool disqualified;

        public Registration(Competitor competitor, Theme theme, string category, IFotoaparat camera, Photo photo)
        {
            RegistrationHandler registrationHandler = RegistrationHandler.GetInstance();
            id = "PRIJAVA " + registrationHandler.Counter.ToString();
            registrationHandler.Counter++;

            disqualified = false;
            this.competitor = competitor;
            this.theme = theme;
            this.category = category;
            this.camera = camera;
            this.photo = photo;
        }

        public void Disqualify()
        {
            disqualified = true;
        }

        public void Requalify()
        {
            disqualified = true;
        }

        public bool IsDisqualified()
        {
            return disqualified;
        }

        public void AddScore(string juryMember, int score)
        {
            scores[juryMember] = score;
        }

        public int GetScore(string juryMember)
        {
            return scores[juryMember];
        }

        public int GetTotalScore()
        {
            int totalScore = 0;
            foreach(KeyValuePair<string, int> score in scores)
                totalScore += score.Value;
            return totalScore;
        }
    }
}
