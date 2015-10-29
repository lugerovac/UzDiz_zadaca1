using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class Theme
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Theme(string name)
        {
            Name = name;
        }
    }
}
