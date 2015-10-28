using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    class FotoaparatMILC : IFotoaparat
    {
        private const string cameraType = "MILC";
        private Lenses lenses;
        private string modelName;
        private float scaleFactor;
        private bool additionalGrip;

        public FotoaparatMILC(string modelName, Lenses lenses, float scaleFactor, bool additionalGrip)
        {
            this.modelName = modelName;
            this.lenses = lenses;
            this.scaleFactor = scaleFactor;
            this.additionalGrip = additionalGrip;
        }

        public string GetCameraType()
        {
            return cameraType;
        }

        public Lenses GetLenses()
        {
            return lenses;
        }

        public string GetModelName()
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
