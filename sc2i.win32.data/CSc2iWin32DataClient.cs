using System;
using System.Collections;
using System.Threading;
using sc2i.data;
using sc2i.expression;
using sc2i.common;

namespace sc2i.win32.data
{
	/// <summary>
	/// Description résumée de CSc2iDataClient.
	/// </summary>
    [AutoExec("Autoexec")]
	public class CSc2iWin32DataClient
	{
	
		//Si pas initialisé générallement, initialisé pour chaque thread
		private static Hashtable m_tableContexteParThread = new Hashtable();

		private static CSc2iWin32DataClient m_instance = null;

		private IFournisseurContexteDonnee m_fournisseur;

        /// ////////////////////////////////////////////////////////////
        private static bool m_bAutoexecDone = false;
        public static void Autoexec()
        {
            if ( !m_bAutoexecDone )
            {
                CContexteEvaluationExpression.OnNewContexteEvaluation += new CContexteEvaluationExpression.OnNewContexteEvaluationExpressionDelegate( OnNewContexteEvaluationExpression );
            }
        }

        /// ////////////////////////////////////////////////////////////
        public static void OnNewContexteEvaluationExpression(CContexteEvaluationExpression ctx)
        {
            ctx.AttacheObjet(typeof(CContexteDonnee), ContexteCourant);
        }
		
		/// ////////////////////////////////////////////////////////////
		public CSc2iWin32DataClient()
		{
			
		}

		///////////////////////////////////////////////////////////////
		public static void Init ( IFournisseurContexteDonnee fournisseur )
		{
			m_instance = new CSc2iWin32DataClient();
			m_instance.m_fournisseur = fournisseur;
		}

		///////////////////////////////////////////////////////////////
		public static void InitForThread ( CContexteDonnee contexte )
		{
			m_tableContexteParThread[Thread.CurrentThread] = contexte;
		}

		///////////////////////////////////////////////////////////////
		public static CContexteDonnee ContexteCourant
		{
			get
			{
				if ( m_instance != null )
					return m_instance.m_fournisseur.ContexteCourant;
				return (CContexteDonnee)m_tableContexteParThread[Thread.CurrentThread];
			}
		}

        ///////////////////////////////////////////////////////////////
        public static void PushContexteCourant(CContexteDonnee contexte)
        {
            if (m_instance != null)
                m_instance.m_fournisseur.PushContexteCourant(contexte);
        }

        ///////////////////////////////////////////////////////////////
        public static void PopContexteCourant(CContexteDonnee contexte)
        {
            if (m_instance != null)
                m_instance.m_fournisseur.PopContexteCourant();
        }

	}
}
