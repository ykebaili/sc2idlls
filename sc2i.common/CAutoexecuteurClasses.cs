using System;
using System.Reflection;
using System.Collections.Generic;

namespace sc2i.common
{
	/// <summary>
	/// Execute le code autoexec de toutes les classes rencontrée
	/// avec l'attribut AutoExec
	/// </summary>
	public sealed class CAutoexecuteurClasses
	{
        private CAutoexecuteurClasses() { }

        private static AssemblyLoadEventHandler m_eventLoad = null;

        private static HashSet<MethodInfo> m_dicMethodesFaites = new HashSet<MethodInfo>();

        public static void RunAutoexecsIncludeOnly(params string[] strIncludes)
        {
            RunAutoexecs(strIncludes, null);
        }

        public static void RunAutoexecsWithExclude(params string[] strExcludes)
        {
            RunAutoexecs(null, strExcludes);
        }

        public static void RunAllAutoexecs()
        {
            RunAutoexecs(null, null);
        }

		private static void RunAutoexecs(string[] strIncludes, string[] strExcludes)
		{
            HashSet<string> setIncludes = null;
            HashSet<string> setExcludes = null;
            if (strIncludes != null && strIncludes.Length > 0)
            {
                setIncludes = new HashSet<string>();
                foreach (string strInclude in strIncludes)
                    setIncludes.Add(strInclude);
            }
            if (strExcludes != null && strExcludes.Length > 0)
            {
                setExcludes = new HashSet<string>();
                foreach (string strExclude in strExcludes)
                    setExcludes.Add(strExclude);
            }
            if (m_eventLoad == null)
            {
                m_eventLoad = new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
                AppDomain.CurrentDomain.AssemblyLoad += m_eventLoad;
            }
			foreach ( Assembly ass in CGestionnaireAssemblies.GetAssemblies() )
			{
				RunAutoexec ( ass, setIncludes, setExcludes );
			}
		}

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            if (args.LoadedAssembly != null )
                RunAutoexec(args.LoadedAssembly, null, null);
        }


		public static void RunAutoexec ( Assembly ass, HashSet<string> setIncludes, HashSet<string> setExcludes )
		{
            try
			{
				foreach (Type type in ass.GetTypes())
				{
                    try
                    {
                        object[] attribs = type.GetCustomAttributes(typeof(AutoExecAttribute), false);
                        if (attribs.Length == 1)
                        {
                            AutoExecAttribute attrib = (AutoExecAttribute)attribs[0];
                            bool bDémarrer = true;
                            if (attrib.Attribut.Length > 0)
                            {
                                if (setIncludes != null && setIncludes.Count > 0)
                                    bDémarrer = setIncludes.Contains(attrib.Attribut);
                                if (setExcludes != null && setExcludes.Count > 0 && bDémarrer)
                                    bDémarrer = !setExcludes.Contains(attrib.Attribut);
                            }
                            else
                                if (setIncludes != null && setIncludes.Count > 0)
                                    bDémarrer = false;
                            if (bDémarrer)
                            {
                                MethodInfo info = type.GetMethod(attrib.FonctionAutoexec);
                                if (info == null)
                                    throw new Exception(I.T(" autoexecclass  error : inexisting function '@1' in the class '@2'|30040", attrib.FonctionAutoexec, type.ToString()));
                                if (!m_dicMethodesFaites.Contains(info))
                                    info.Invoke(null, new object[0]);
                                m_dicMethodesFaites.Add(info);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(I.T(" autoexec error : class  @1|30041", type.ToString()), e);
                    }
                    
				}
			}
			catch (ReflectionTypeLoadException e)
			{
				string strInfo = "";
				foreach (Exception le in e.LoaderExceptions)
					strInfo += le.ToString() + "\r\n";
				throw new Exception(I.T("autoexe error : Assembly @1|30042", ass.FullName)+"\r\n"+strInfo);
			}
		}
	}
}
