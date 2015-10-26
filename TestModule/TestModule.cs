using lugerovac__zadaca_1_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModule
{
    class TestModule : IFotoCompetitionModule
    {
        private string ID = "TestModule";
        private float priority = 1;

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
            Console.WriteLine("Test works!");
        }
    }
}
