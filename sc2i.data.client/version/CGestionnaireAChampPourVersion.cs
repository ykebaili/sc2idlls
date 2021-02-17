using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using sc2i.common;
using System.Data;

namespace sc2i.data
{
	public static class CGestionnaireAChampPourVersion
	{
		private static Dictionary<string, IJournaliseurDonneesChamp> m_tableTypeChampToJournaliseur = new Dictionary<string, IJournaliseurDonneesChamp>();
		public static List<IJournaliseurDonneesChamp> GetJournaliseurs()
		{
			List<IJournaliseurDonneesChamp> journaliseurs = new List<IJournaliseurDonneesChamp>();
			foreach (string k in m_tableTypeChampToJournaliseur.Keys)
				journaliseurs.Add(m_tableTypeChampToJournaliseur[k]);
			return journaliseurs;
		}
		public static void RegisterJournaliseur(IJournaliseurDonneesChamp journaliseur)
		{
			m_tableTypeChampToJournaliseur[journaliseur.TypeChamp] = journaliseur;
		}
		public static IJournaliseurDonneesChamp GetJournaliseur(string strTypeChamp)
		{
			if (m_tableTypeChampToJournaliseur.ContainsKey(strTypeChamp))
				return m_tableTypeChampToJournaliseur[strTypeChamp];
			return null;
		}

	}


	
}
