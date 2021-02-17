using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.IO;


namespace sc2i.common
{
    /// <summary>
    /// Chargeur / Déchargeur de plugins
    /// Un plugin est un assembly ayant l'extension 2iPlugin
    /// </summary>
    public class CGestionnairePlugins
    {
        //tables des assemblies déjà chargés
        private static Dictionary<string, bool> m_tableFaits = new Dictionary<string, bool>();

        /// <summary>
        /// Charge explicitement une liste donnée de plugins
        /// </summary>
        /// <param name="strFiles"></param>
        /// <returns></returns>
        public static CResultAErreur LoadPlugins(string[] strFiles)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                foreach (string strFile in strFiles)
                {
                    if (!m_tableFaits.ContainsKey(strFile))
                    {
                        if (File.Exists(strFile))
                        {
                            Assembly ass = new CGestionnairePlugins().LoadAssembly(strFile);
                        }
                        else
                        {
                            C2iEventLog.WriteErreur("Error in LoadPlugins, file doesn't exist : " + strFile);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                C2iEventLog.WriteErreur("Error in LoadPlugins : " + e.Message);
            }
            return result;
        }

        /// <summary>
        /// Charge tous les plugins spécifiques d'un répertoire
        /// </summary>
        /// <param name="strPluginsPath"></param>
        /// <param name="strPrefixe"></param>
        /// <returns></returns>
        public static CResultAErreur InitPlugins(string strPluginsPath, string strPrefixe)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                if (strPluginsPath[strPluginsPath.Length - 1] != '\\')
                    strPluginsPath += "\\";
                string[] strFiles = Directory.GetFiles(strPluginsPath, strPrefixe + "*.dll");
                foreach (string strFile in strFiles)
                {
                    if (!m_tableFaits.ContainsKey(strFile))
                    {
                        Assembly ass = new CGestionnairePlugins().LoadAssembly(strFile);
                        AppDomain.CurrentDomain.AppendPrivatePath(strPluginsPath);
                        CAutoexecuteurClasses.RunAutoexec(ass, null, null);
                    }
                }
                foreach (string strRepertoire in Directory.GetDirectories(strPluginsPath))
                {
                    result = InitPlugins(strRepertoire, strPrefixe);
                    if (!result)
                        return result;
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }



        private Assembly LoadAssembly(string strFile)
        {
            Assembly ass = Assembly.LoadFrom(strFile);
            //return AppDomain.CurrentDomain.Load(strFile);
            return ass;
        }

        private Assembly LoadAssembly(AssemblyName name)
        {
            Assembly ass = Assembly.Load(name);

            return AppDomain.CurrentDomain.Load(name);
        }
    }
}
