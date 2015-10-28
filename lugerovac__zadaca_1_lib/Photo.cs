using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class Photo
    {
        private bool disqualified;

        private float aperture;
        public float Aperture
        {
            get { return aperture; }
            set { aperture = value; }
        }

        private float exposure;
        public float Exposure
        {
            get { return exposure; }
            set { exposure = value; }
        }

        public Photo()
        {
            disqualified = true;
        }

        public Photo(Photo photo)
        {
            Aperture = photo.Aperture;
            Exposure = photo.Exposure;
            disqualified = photo.IsDisqualified();
        }

        public Photo(float aperture, float exposure)
        {
            Create(aperture, exposure);
        }

        public void Create(float aperture, float exposure)
        {
            Aperture = aperture;
            Exposure = exposure;
            disqualified = false;
        }

        public void Disqualify()
        {
            disqualified = true;
        }

        public bool IsDisqualified()
        {
            return disqualified;
        }
    }
}
