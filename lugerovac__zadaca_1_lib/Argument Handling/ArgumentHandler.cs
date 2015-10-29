using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public class ArgumentHandler
    {
        #region Singleton
        private static ArgumentHandler instance;

        protected ArgumentHandler()
        {
            listOfArguments = new List<ArgumentObject>();
        }

        public static ArgumentHandler GetInstance()
        {
            if(instance == null)
                instance = new ArgumentHandler();

            return instance;
        }
        #endregion


        List<ArgumentObject> listOfArguments;

        /// <summary>
        /// Dodaje ili modificra argument u listi
        /// </summary>
        /// <param name="argName">Naziv argumenta</param>
        /// <param name="argType">Tip argumenta</param>
        /// <param name="arg">Objekt argumenta</param>
        /// <param name="overwrite">Ako argument postoji, overwritati?</param>
        /// <returns>True ako ej sve u redu, False ako nije</returns>
        public bool AddArgument(string argName, string argType, Object arg, bool overwrite)
        {
            foreach(ArgumentObject argObject in listOfArguments)
            {
                if(string.Equals(argObject.Name, argName))
                {
                    if (!overwrite)
                        return false;

                    argObject.Argument = arg;
                    argObject.Type = argType;
                    return true;
                }
            }

            ArgumentObject newArgument = new ArgumentObject(argName, argType, arg);
            listOfArguments.Add(newArgument);

            return true;
        }

        /// <summary>
        /// Omogućuje vanjskim klasam da dohvate argument
        /// </summary>
        /// <param name="argName">Naziv argumenta</param>
        /// <returns>Argument koji će pozivatelj morati castati</returns>
        public Object GetArgument(string argName)
        {
            foreach (ArgumentObject argObject in listOfArguments)
            {
                if (string.Equals(argObject.Name, argName))
                {
                    return argObject.Argument;
                }
            }
            return null;
        }
    }
}
