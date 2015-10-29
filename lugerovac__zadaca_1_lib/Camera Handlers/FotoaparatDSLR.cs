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

        public FotoaparatDSLR()
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
            else if (string.Equals(key, "integratedGrip"))
            {
                if (string.Equals(value.ToLower(), "yes"))
                    integratedGrip = true;
                else
                    integratedGrip = false;
            }
            else if (string.Equals(key, "integratedBlitz"))
            {
                if (string.Equals(value.ToLower(), "yes"))
                    integratedBlitz = true;
                else
                    integratedBlitz = false;
            }
            else if (string.Equals(key, "cameraStabilizationInBody"))
            {
                if (string.Equals(value.ToLower(), "yes"))
                    cameraStabilizationInBody = true;
                else
                    cameraStabilizationInBody = false;
            }

        }

        public void SetValues(string modelName, Lenses lenses,
            float scaleFactor, bool integratedGrip, bool integratedBlitz, bool cameraStabilizationInBody)
        {
            this.modelName = modelName;
            this.lenses = lenses;
            this.scaleFactor = scaleFactor;
            this.integratedGrip = integratedGrip;
            this.integratedBlitz = integratedBlitz;
            this.cameraStabilizationInBody = cameraStabilizationInBody;
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
