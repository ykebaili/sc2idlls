using System;

using sc2i.win32.navigation;
using System.Collections.Generic;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CSc2iWin32DataNavigation.
	/// </summary>
	public class CSc2iWin32DataNavigation
	{
        private static Stack<CFormNavigateur> m_stackNavigateurs = new Stack<CFormNavigateur>();

		public static void Init (
			CFormNavigateur navigateur,
			string strCleRegistre )
		{
            m_stackNavigateurs.Push(navigateur);
			CSc2iWin32DataNavigationRegistre.Init(strCleRegistre);
		}

        public static bool IsInit()
        {
            return m_stackNavigateurs.Count > 0;
        }

		public static CFormNavigateur Navigateur
		{
			get
			{
				try
				{
					if ( m_stackNavigateurs.Count == 0 )
						throw new Exception("sc2i.win32.data.navigation has not been initialized|30013 ");
				}
				catch ( Exception e )
				{
					throw new Exception(e.ToString()+"\r\n"+e.StackTrace.ToString() );
				}
                return m_stackNavigateurs.Peek();
			}
		}

        public static void PushNavigateur(CFormNavigateur form)
        {
            m_stackNavigateurs.Push(form);
        }

        public static void PopNavigateur()
        {
            m_stackNavigateurs.Pop();
        }

	}
}
