using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public abstract class IFotoaparat
    {
        public abstract string GetCameraType();
        public abstract string GetModelName();
        public abstract Lenses GetLenses();
        public abstract void SetValue(string key, string value);
    }
}
