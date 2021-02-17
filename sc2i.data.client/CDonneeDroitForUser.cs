using System;
using System.Collections;

using sc2i.data;
using sc2i.multitiers.client;

#if !PDA_DATA

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CDonneeDroitForUser.
	/// </summary>
	[Serializable]
	public class CDonneeDroitForUser : IDonneeDroitUtilisateur
	{
		private string m_strCodeDroit = "";
		private OptionsDroit m_options = OptionsDroit.All;
		//Hashtable avec des CReferenceObjetDonnee en clé
		private Hashtable m_tableIdObjets = new Hashtable();
		
		/// ///////////////////////////////////////////////
		public CDonneeDroitForUser( string strCodeDroit, OptionsDroit options )
		{
			m_strCodeDroit = strCodeDroit;
			m_options = options;
		}

		/// ///////////////////////////////////////////////
		public string CodeDroit
		{
			get
			{
				return m_strCodeDroit;
			}
		}

		/// ///////////////////////////////////////////////
		public void AddObjetDonneeOption ( CObjetDonnee objet )
		{
			CReferenceObjetDonnee refobj = new CReferenceObjetDonnee ( objet );
			m_tableIdObjets[refobj] = true;
		}

		/// ///////////////////////////////////////////////
		public CObjetDonnee[] GetListeObjetsDonneeOption ( CContexteDonnee contexte )
		{
			ArrayList lst = new ArrayList();
			foreach ( CReferenceObjetDonnee refObj in m_tableIdObjets )
			{
				lst.Add(refObj.GetObjet ( contexte ));
			}
			return (CObjetDonnee[])lst.ToArray(typeof(CObjetDonnee));
		}

		/// ///////////////////////////////////////////////
		public void CombineOptions ( OptionsDroit options )
		{
			m_options |= options;
		}

		/// ///////////////////////////////////////////////
		public bool HasOption ( OptionsDroit option )
		{
			return (m_options & option) == option;
		}

	}
}
#endif