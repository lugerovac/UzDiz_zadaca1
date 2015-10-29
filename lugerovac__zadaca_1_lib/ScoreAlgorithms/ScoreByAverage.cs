using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class ScoreByAverage : ScoreAlgorhitm
    {
        public override float GenerateScore(Registration registration)
        {
            if (registration.IsDisqualified())
                return 0;

            int totalScore = 0;
            Dictionary<string, int> scores = registration.GetAllScores();
            foreach (KeyValuePair<string, int> score in scores)
                totalScore += score.Value;

            return Convert.ToSingle(totalScore) / Convert.ToSingle(scores.Count);
        }
    }
}
