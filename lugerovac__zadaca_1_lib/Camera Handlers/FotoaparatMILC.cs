using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class FotoaparatMILC : IFotoaparat
    {
        private const string cameraType = "MILC";
        private Lenses lenses;
        private string modelName;
        private float scaleFactor;
        private bool additionalGrip;

        public FotoaparatMILC()
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
            else if (string.Equals(key, "scaleFactor"))
                scaleFactor = Convert.ToSingle(value);
            else if (string.Equals(key, "additionalGrip"))
            {
                if (string.Equals(value.ToLower(), "yes"))
                    additionalGrip = true;
                else
                    additionalGrip = false;
            }
        }

        public void SetValues(string modelName, Lenses lenses, float scaleFactor, bool additionalGrip)
        {
            this.modelName = modelName;
            this.lenses = lenses;
            this.scaleFactor = scaleFactor;
            this.additionalGrip = additionalGrip;
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

        public float GetScaleFactor()
        {
            return scaleFactor;
        }

        public bool HadAdditionalGrip()
        {
            return additionalGrip;
        }
    }
}
