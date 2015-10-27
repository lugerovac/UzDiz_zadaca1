using lugerovac__zadaca_1_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModule2
{
    class TestModule2 : IFotoCompetitionModule
    {
        private string ID = "TestModule2";
        private float priority = 29;

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
            string resultFile = (string)arguments.GetArgument("ResultFile");
            Console.WriteLine(resultFile);
        }
    }
}
