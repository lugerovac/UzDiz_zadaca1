using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class ScoreByAverageFactory : ScoringFactory
    {
        public override ScoreAlgorhitm Create()
        {
            return new ScoreByAverage();
        }
    }
}
