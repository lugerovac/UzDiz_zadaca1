using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class FotoaparatCompact : IFotoaparat
    {
        private const string cameraType = "Compact";
        private string modelName;
        private Lenses lenses;
        private bool eyepiece;

        public FotoaparatCompact()
        {
            lenses = new Lenses();
        }

        public override void SetValue(string key, string value)
        {
            if (string.Equals(key, "name"))
                modelName = value;
            else if (string.Equals(key, "lensesLength"))
                lenses.Length = Convert.ToSingle(value);
            else if (string.Equals(key, "lensesDiameter"))
                lenses.Diameter = Convert.ToSingle(value);
            else if (string.Equals(key, "eyepiece"))
            {
                if (string.Equals(value.ToLower(), "yes"))
                    eyepiece = true;
                else
                    eyepiece = false;
            }
        }

        public void SetValues(string modelName, Lenses lenses, bool eyepiece)
        {
            this.modelName = modelName;
            this.lenses = lenses;
            this.eyepiece = eyepiece;
        }

        public override string GetCameraType()
        {
            return cameraType;
        }

        public override Lenses GetLenses()
        {
            return lenses;
        }

        public override string GetModelName()
        {
            return modelName;
        }

        public bool HasEyepiece()
        {
            return eyepiece;
        }
    }
}
