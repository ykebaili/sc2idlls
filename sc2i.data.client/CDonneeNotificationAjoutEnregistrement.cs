using System;
using System.Collections;

using sc2i.multitiers.client;

namespace sc2i.data
{
	/// <summary>
	/// Indique qu'un enregistrement que les enregistrements d'une table ont changé
	/// </summary>
	[Serializable]
	public class CDonneeNotificationAjoutEnregistrement : IDonneeNotification
	{
		private int m_nIdSessionEnvoyeur = -1;
		private string m_strNomTable = "";
		/// ////////////////////////////////////////////////////////////////////
		public CDonneeNotificationAjoutEnregistrement( int nIdSession, string strNomTable )
		{
			m_nIdSessionEnvoyeur = nIdSession;    			
			m_strNomTable = strNomTable;
		}

		/// ////////////////////////////////////////////////////////////////////
		public string NomTable
		{
			get
			{
				return m_strNomTable;
			}
		}

		/// ////////////////////////////////////////////////////////////////////
		public int IdSessionEnvoyeur
		{
			get
			{
				return m_nIdSessionEnvoyeur;
			}
            set
            {
                m_nIdSessionEnvoyeur = value;
            }
		}

		/// ////////////////////////////////////////////////////////////////////
		public int PrioriteNotification
		{
			get
			{
				return 500;
			}
		}
	}
}
