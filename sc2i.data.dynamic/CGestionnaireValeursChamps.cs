using System;
using System.Collections;

using sc2i.common;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CGestionnaireValeursChamps.
	/// </summary>
	public class CGestionnaireValeursChamps
	{
		Hashtable m_hashtableValeurs = new Hashtable();
		//-------------------------------------------------------------------
		public CGestionnaireValeursChamps()
		{
		}
		//-------------------------------------------------------------------
		public object GetValeurChamp(CChampCustom champ)
		{
			return m_hashtableValeurs[champ];
		}
		//-------------------------------------------------------------------
		public void SetValeurChamp(CChampCustom champ, object valeur)
		{
			m_hashtableValeurs[champ] = valeur;
		}
		//-------------------------------------------------------------------
		public CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			/*
			foreach(object obj in m_hashtableValeurs)
			{
				
			}
			*/
			return result;
		}
		//-------------------------------------------------------------------
	}
}
