using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class RegistrationHandler
    {
        #region Singleton
        private static RegistrationHandler instance;

        protected RegistrationHandler()
        {
        }

        public static RegistrationHandler GetInstance()
        {
            if (instance == null)
                instance = new RegistrationHandler();

            return instance;
        }
        #endregion

        private int counter = 1;
        public int Counter
        {
            get { return counter; }
            set { counter++; }
        }

        public List<Registration> Registrations;
    }
}
