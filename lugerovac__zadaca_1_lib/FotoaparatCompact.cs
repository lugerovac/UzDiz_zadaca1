using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    class FotoaparatCompact : IFotoaparat
    {
        private const string cameraType = "Compact";
        private string modelName;
        private Lenses lenses;
        private bool eyepiece;

        public FotoaparatCompact(string modelName, Lenses lenses, bool eyepiece)
        {
            this.modelName = modelName;
            this.lenses = lenses;
            this.eyepiece = eyepiece;
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

        public bool HasEyepiece()
        {
            return eyepiece;
        }
    }
}
