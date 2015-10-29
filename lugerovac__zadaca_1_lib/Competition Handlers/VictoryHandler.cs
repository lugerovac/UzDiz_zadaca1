using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public static class VictoryHandler
    {
        /// <summary>
        /// Daje listu poibjednika i njihovih pozicija
        /// </summary>
        /// <param name="scores">Rječnik s bodovima</param>
        /// <returns>Rječnik s pobjednicima</returns>
        public static Dictionary<string,float> GetVictors(Dictionary<string, float> scores)
        {
            Dictionary<string, float> results = new Dictionary<string, float>();

            #region First place
            float bestScore = 0;
            List<string> FirstPlace = new List<string>();
            foreach(KeyValuePair<string, float> score in scores)
            {
                if (score.Value == bestScore)
                    FirstPlace.Add(score.Key);
                if(score.Value > bestScore)
                {
                    FirstPlace.Clear();
                    FirstPlace.Add(score.Key);
                    bestScore = score.Value;
                }
            }

            foreach(string competitor in FirstPlace)
                results.Add("1. " + competitor, bestScore);

            if (results.Count >= 3)
                return results;
            #endregion

            #region Second place
            float secondBestScore = 0;
            List<String> SecondPlace = new List<string>();
            foreach (KeyValuePair<string, float> score in scores)
            {
                if (score.Value == bestScore)
                    continue;
                if (score.Value == secondBestScore)
                    SecondPlace.Add(score.Key);
                if (score.Value > secondBestScore)
                {
                    SecondPlace.Clear();
                    SecondPlace.Add(score.Key);
                    secondBestScore = score.Value;
                }
            }

            foreach (string competitor in SecondPlace)
                results.Add("2. " + competitor, secondBestScore);

            if (results.Count >= 3)
                return results;
            #endregion

            #region Third place
            float thirdBestScore = 0;
            List<String> ThirdPlace = new List<string>();
            foreach (KeyValuePair<string, float> score in scores)
            {
                if (score.Value == bestScore || score.Value == secondBestScore)
                    continue;
                if (score.Value == thirdBestScore)
                    ThirdPlace.Add(score.Key);
                if (score.Value > thirdBestScore)
                {
                    ThirdPlace.Clear();
                    ThirdPlace.Add(score.Key);
                    thirdBestScore = score.Value;
                }
            }

            foreach (string competitor in ThirdPlace)
                results.Add("3. " + competitor, thirdBestScore);

            return results;
            #endregion
        }
    }
}
