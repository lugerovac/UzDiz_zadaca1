using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lugerovac__zadaca_1_lib
{
    public static class FotoCompetitionModuleLoader
    {
        /// <summary>
        /// Učitava .dll datoteke, tj. module, te ih sortira po prioritetu i vraće pozivatelju
        /// </summary>
        /// <param name="moduleFolder">Direktorij u kojem se nalaze moduli</param>
        /// <returns>Sortirana lista modula</returns>
        public static List<IFotoCompetitionModule> GetModules(string moduleFolder)
        {
            //Load the modules
            List<string> listOfFiles = new List<string>();
            List<IFotoCompetitionModule> listOfModules = new List<IFotoCompetitionModule>();
            string[] files = Directory.GetFiles(moduleFolder);
            foreach (string file in files)
            {
                if (!file.EndsWith(".dll"))
                    continue;

                var moduleAssembly = System.Reflection.Assembly.LoadFrom(file);
                var moduleTypes = moduleAssembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IFotoCompetitionModule)));
                IEnumerable<IFotoCompetitionModule> listOfModulesEnum = moduleTypes.Select(type =>
                {
                    return (IFotoCompetitionModule)Activator.CreateInstance(type);
                });

                foreach(IFotoCompetitionModule mod in listOfModulesEnum)
                {
                    listOfModules.Add(mod);
                }
            }
            
            //Sort the modules
            List<IFotoCompetitionModule> listOfModulestoReturn = new List<IFotoCompetitionModule>();
            while(listOfModules.Count > 0)
            {
                bool notInstanced = true;
                float highestPriority = 0;
                IFotoCompetitionModule highestPriorityModule = listOfModules.First();
                foreach (IFotoCompetitionModule module in listOfModules)
                {
                    if(notInstanced)
                    {
                        highestPriorityModule = module;
                        highestPriority = module.GetPriority();
                        notInstanced = false;
                    }
                    else if(module.GetPriority() < highestPriority)
                    {
                        highestPriorityModule = module;
                        highestPriority = module.GetPriority();
                    }
                }

                listOfModulestoReturn.Add(highestPriorityModule);
                listOfModules.Remove(highestPriorityModule);
            }

            return listOfModulestoReturn;
        }
    }
}
