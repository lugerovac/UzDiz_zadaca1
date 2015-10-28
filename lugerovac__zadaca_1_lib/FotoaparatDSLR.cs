using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class FotoaparatDSLR : IFotoaparat
    {
        private const string cameraType = "DSLR";
        private Lenses lenses;
        private string modelName;
        private float scaleFactor;
        private bool integratedGrip;
        private bool integratedBlitz;
        private bool cameraStabilizationInBody;

        public FotoaparatDSLR(string modelName, Lenses lenses, 
            float scaleFactor, bool integratedGrip, bool integratedBlitz, bool cameraStabilizationInBody)
        {
            this.modelName = modelName;
            this.lenses = lenses;
            this.scaleFactor = scaleFactor;
            this.integratedGrip = integratedGrip;
            this.integratedBlitz = integratedBlitz;
            this.cameraStabilizationInBody = cameraStabilizationInBody;
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

        public bool HasIntegratedGrip()
        {
            return integratedGrip;
        }

        public bool HasIntegratedBlitz()
        {
            return integratedBlitz;
        }

        public bool HasCameraStabilizationInBody()
        {
            return cameraStabilizationInBody;
        }
    }
}
