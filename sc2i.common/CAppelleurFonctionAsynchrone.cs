using System;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CAppelleurFonctionAvecCallback.
	/// </summary>
	public class CAppelleurFonctionAsynchrone
	{
        private static Type m_typeMultiThreader = null;

        public static void SetTypeMultiThreader(Type tp)
        {
            if (typeof(IMultiThreader).IsAssignableFrom(tp))
                m_typeMultiThreader = tp;
        }

		private Type m_typeObjetAppele;
		private object m_objetAppele;
		private string m_strMethode;
		private object[] m_parametres;
		private object m_retour;
		
		private void StartThread ( )
		{
			//Trouve la méthode à appeler
			Type tp = m_typeObjetAppele;
			/*ArrayList lstTypes = new ArrayList();
			foreach ( object parametre in m_parametres )
				if(  parametre == null )
					lstTypes.Add ( typeof(object));
				else
					lstTypes.Add ( parametre.GetType() );*/
            MethodInfo info = CUtilType.FindMethodFromParametres(m_typeObjetAppele, m_strMethode, m_parametres);
			if ( info == null )
			{
				foreach ( Type tpInt in tp.GetInterfaces() )
				{
					info = tpInt.GetMethod(m_strMethode);
					if ( info != null )
						break;
				}
			}
			if ( info == null )
			{
				m_retour = new Exception(I.T("Impossible to find the method @1|30039",m_strMethode));
				return;
			}
			try
			{
				m_retour = info.Invoke ( m_objetAppele, m_parametres );
			}
			catch ( Exception e )
			{
				m_retour = e;
			}
		}
 




		public object StartFonctionAndWaitAvecCallback ( 
			Type typeObjetAppele,
			object objetAppele,
			string strMethode,
            string strLibelle,
            object defaultResult,
			params object[] parametres )
		{
            if (m_typeMultiThreader != null)
            {
                IMultiThreader multiThreader = Activator.CreateInstance(m_typeMultiThreader) as IMultiThreader;
                return multiThreader.StartFonctionAndWaitAvecCallback(
                    typeObjetAppele,
                    objetAppele,
                    strMethode,
                    strLibelle,
                    defaultResult,
                    parametres);
            }
            try
            {
                Thread th = new Thread(new ThreadStart(StartThread));
                m_typeObjetAppele = typeObjetAppele;
                m_objetAppele = objetAppele;
                m_strMethode = strMethode;
                m_parametres = parametres;
                m_retour = null;
                th.Start();
                th.Join();
                /*while (th.IsAlive)
                {
                    Thread.Sleep(100);
                }*/
                if (m_retour is Exception)
                {
                    throw (Exception)m_retour;
                }
            }
            catch 
            {
                return defaultResult;
            }
			return m_retour;
		}
	}

    public interface IMultiThreader
    {
        object StartFonctionAndWaitAvecCallback(Type typeObjetAppele,
            object objetAppele,
            string strMethode,
            string strLibelle,
            object defaultResult,
            params object[] parametres);
    }
}
