﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public interface IFotoCompetitionModule
    {
        string getID();
        float GetPriority();
        void Run();
    }
}
