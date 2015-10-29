using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class BalancedScoring : ScoreAlgorhitm
    {
        public override float GenerateScore(Registration registration)
        {
            if (registration.IsDisqualified())
                return 0;

            Dictionary<string, int> scores = registration.GetAllScores();
            int minimumScore = int.MaxValue;
            int maximumScore = -1;
            int totalScore = 0;

            foreach(KeyValuePair<string, int> score in scores)
            {
                totalScore += score.Value;
                if (score.Value < minimumScore)
                    minimumScore = score.Value;
                if (score.Value > maximumScore)
                    maximumScore = score.Value;
            }

            if (scores.Count <= 2)
                return totalScore / scores.Count;

            totalScore -= minimumScore;
            totalScore -= maximumScore;

            return Convert.ToSingle(totalScore)/(Convert.ToSingle(scores.Count) - 2f);
        }
    }
}
