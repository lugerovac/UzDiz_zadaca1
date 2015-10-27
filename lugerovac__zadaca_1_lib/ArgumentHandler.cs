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
        /// Adds to, or modifies in, argument in the list
        /// </summary>
        /// <param name="argName">Name of the argument</param>
        /// <param name="argType">Type of the argument</param>
        /// <param name="arg">The argument object</param>
        /// <param name="overwrite">If exists, should it overwrite the value?</param>
        /// <returns>True if everything was in order, False if not</returns>
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
        /// Allows external classes to find the argument in the list
        /// </summary>
        /// <param name="argName">Name of the argument</param>
        /// <returns>The argument, the caller will need to cast it</returns>
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
