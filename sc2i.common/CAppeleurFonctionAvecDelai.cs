using System;
using System.Timers;
using System.Reflection;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CAppeleurFonctionAvecDelai.
	/// </summary>
	public class CAppeleurFonctionAvecDelai : IDisposable
	{
		object m_objectSource;
		string m_strNomFonction;
		object[] m_parametres;

		Timer m_timer;


		private CAppeleurFonctionAvecDelai( object objectsource, string strNomFonction, double dDelai, params object[] parametres)
		{
			m_objectSource = objectsource;
			m_strNomFonction = strNomFonction;
			m_parametres = parametres==null?new object[0]:parametres;

			m_timer = new Timer( dDelai );
			m_timer.Elapsed += new ElapsedEventHandler(OnTimerOk);
			m_timer.Start();
		}


		private void OnTimerOk ( object sender, ElapsedEventArgs args )
		{
			m_timer.Stop();
			m_timer.Dispose();
			int nLength = m_parametres == null?0:m_parametres.Length;
			Type[] types = new Type[nLength];
			for(  int nTmp = 0; nTmp < nLength; nTmp++ )
				types[nTmp] = m_parametres[nTmp].GetType();
			MethodInfo info = null;
			if ( types.Length != 0 )
				info = m_objectSource.GetType().GetMethod(
					m_strNomFonction,	
					types);
			else
				info = m_objectSource.GetType().GetMethod(m_strNomFonction);
			if ( info != null )
			{
				try
				{
					info.Invoke(m_objectSource, m_parametres);
				}
				catch
				{ }
			}
		}

		public static void CallFonctionAvecDelai ( object objetSource, string strNomFonction, double dDelai, params object[] parametres )
		{
			new CAppeleurFonctionAvecDelai(objetSource, strNomFonction, dDelai, parametres );
		}

        protected virtual void Dispose(bool bVal)
        {
            if (bVal)
                m_timer.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
	}
}
