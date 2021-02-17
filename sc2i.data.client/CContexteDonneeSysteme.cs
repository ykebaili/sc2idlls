using System;
using System.Collections.Generic;
using System.Text;
using sc2i.multitiers.client;

namespace sc2i.data
{
	/// <summary>
	/// Fournit un contexte de donnée pour la session,
	/// contexte utilisé pour récuperer des données systèmes 
	/// telles des données de paramétrage ou autre
	/// SURTOUT NE PAS CREER D'OBJET DANS CE CONTEXTE, IL EST FAIT POUR 
	/// CONSULTATION DE DONNEES DE PARAMETRAGE UNIQUEMENT
	/// </summary>
	public class CContexteDonneeSysteme
	{
		private static Dictionary<int, CContexteDonnee> m_tableContextesDonneesParSessions = new Dictionary<int, CContexteDonnee>();

		/// ///////////////////////////////////////////////////////////
		public static CContexteDonnee GetInstance()
		{
			CSessionClient session = CSessionClient.GetSessionUnique();
            while (session is CSousSessionClient)
                session = ((CSousSessionClient)session).RootSession;
			CContexteDonnee contexte = null;
            int nIdSession = 0;
			if (session != null)
			{
				nIdSession = session.IdSession;
				bool bIsLocale = CSessionClient.IsSessionLocale(session.IdSession);
				if (!bIsLocale)
					nIdSession = 0;
				contexte = null;
            }
			if (!m_tableContextesDonneesParSessions.TryGetValue ( nIdSession, out contexte ) )
			{
				contexte = new CContexteDonnee(nIdSession, true, true);
				contexte.GestionParTablesCompletes = true;
				if (nIdSession != 0)
					((CSessionClient)session).OnCloseSession += new EventHandler(session_OnCloseSession);
				m_tableContextesDonneesParSessions[nIdSession] = contexte;
			}
			return contexte;
		}

		/// ///////////////////////////////////////////////////////////
		public static void session_OnCloseSession(object sender, EventArgs e)
		{
			if (sender is CSessionClient)
			{
				CSessionClient session = (CSessionClient)sender;
				try
				{
					CContexteDonnee contexte = null;
					if ( m_tableContextesDonneesParSessions.TryGetValue ( session.IdSession, out contexte ) )
						contexte.Dispose();
				}
				catch { }
				if (m_tableContextesDonneesParSessions.ContainsKey(session.IdSession))
					m_tableContextesDonneesParSessions.Remove(session.IdSession);
			}
		}
	}
}
