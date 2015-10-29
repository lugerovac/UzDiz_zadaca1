using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    class ArgumentObject
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        Object argument;
        public Object Argument
        {
            get { return argument; }
            set { argument = value; }
        }

        public ArgumentObject(string name, string type, Object arg)
        {
            Name = name;
            Type = type;
            argument = arg;
        }
    }
}
