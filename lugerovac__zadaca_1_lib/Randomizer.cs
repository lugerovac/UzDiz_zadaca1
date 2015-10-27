using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class Randomizer
    {
        ArgumentHandler arguments = ArgumentHandler.GetInstance();
        Random rnd;

        #region Singleton
        private static Randomizer instance;

        protected Randomizer()
        {
            int randomSeed = (int) arguments.GetArgument("RandomSeed");
            rnd = new Random(randomSeed);
        }

        public static Randomizer GetInstance()
        {
            if (instance == null)
            {
                instance = new Randomizer();
            }

            return instance;
        }
        #endregion

        public int GetNumber(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }
}
