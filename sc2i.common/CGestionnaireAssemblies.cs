using System;
using System.Collections;
using System.Reflection;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CGestionnaireAssemblies.
	/// </summary>
	public sealed class CGestionnaireAssemblies
	{
		private static Hashtable m_tableAssemblies = new Hashtable();

        private CGestionnaireAssemblies() { }

		////////////////////////////////////////////////////////
		public static Assembly LoadAssembly ( string strAssembly )
		{
			Assembly ass = Assembly.LoadFrom(strAssembly);
			m_tableAssemblies[strAssembly] = ass;
			return ass;
		}

		////////////////////////////////////////////////////////
		public static void RegisterAssembly ( Assembly ass )
		{
			m_tableAssemblies[ass.FullName] = ass;
		}

		////////////////////////////////////////////////////////
		public static Assembly[] GetAssemblies()
		{
#if PDA
			Assembly[] liste = new Assembly[m_tableAssemblies.Count];
			int nAssembly = 0;
			foreach ( Assembly ass in m_tableAssemblies.Values )
			{
				liste[nAssembly] = ass;
				nAssembly++;
			}
			return liste;
#else
			return AppDomain.CurrentDomain.GetAssemblies();
#endif
		}

	}
}
