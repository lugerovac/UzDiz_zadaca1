using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class Competitor : Person
    {
        private List<CompetitionGroup> listOfCompetitionGroups;
        public List<CompetitionGroup> ListOfCompetitionGroups
        {
            get { return listOfCompetitionGroups; }
        }

        private IFotoaparat camera;
        public IFotoaparat Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public Competitor(string name) : base(name)
        {
            listOfCompetitionGroups = new List<CompetitionGroup>();
        }

        public bool AddCompetitionGroup(CompetitionGroup competitionGroup)
        {
            foreach(CompetitionGroup compGroup in listOfCompetitionGroups)
            {
                if (compGroup.Theme.Name == competitionGroup.Theme.Name)
                    return false;
            }

            listOfCompetitionGroups.Add(competitionGroup);
            return true;
        }
    }
}
