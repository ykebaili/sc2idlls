using System;
using System.Collections;

using sc2i.multitiers.client;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CDonneeNotificationModificationContexteDonnee.
	/// </summary>
	[Serializable]
	public class CDonneeNotificationModificationContexteDonnee : IDonneeNotification 
	{
		[Serializable]
		public class CInfoEnregistrementModifie
		{
			public readonly string NomTable;
			public readonly object[] ValeursCle;
			public readonly bool Deleted = false;
			
			public CInfoEnregistrementModifie( string strNomTable, bool bDeleted, object[] cles )
			{
				NomTable = strNomTable;
				ValeursCle = cles;
				Deleted = bDeleted;
			}

			
		}

		private ArrayList m_listeModifications = new ArrayList();
		private int m_nIdSession ;

		/// /////////////////////////////////////////////////////////
		public CDonneeNotificationModificationContexteDonnee( int nIdSession )
		{
			m_nIdSession = nIdSession;
		}

		public int IdSessionEnvoyeur
		{
			get
			{
				return m_nIdSession;
			}
            set
            {
                m_nIdSession = value;
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

		/// /////////////////////////////////////////////////////////
		public void AddModifiedRecord ( string strNomTable, bool bDeleted, object[] cles )
		{
			m_listeModifications.Add ( new CInfoEnregistrementModifie(strNomTable,bDeleted, cles) );
		}

		/// /////////////////////////////////////////////////////////
		public ArrayList ListeModifications
		{
			get
			{
				return m_listeModifications;
			}
		}
	}
}
