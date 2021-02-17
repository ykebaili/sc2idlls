using System;


using sc2i.common;

namespace sc2i.common.planification
{
	/// <summary>
	/// Paramètre d'une planification basée sur le mois
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CPlanificationTacheMensuelleJourMois : CPlanificationTache
	{
		
		//Numéro de jour dans le mois
		//31 signifie le dernier jour du mois
		private int m_nNumJour = 1;

		/// <summary>
		/// s'execute tous les m_nEcartMois
		/// </summary>
		private int m_nEcartMois = 1;

		/// ////////////////////////////////////////
		public CPlanificationTacheMensuelleJourMois()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

		/// ///////////////////////////////////////////
		public static void Autoexec()
		{
			CPlanificationTache.RegisterTypePlanification ( typeof(CPlanificationTacheMensuelleJourMois), "Mensuelle (jour)");
		}

		/// ////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize(serializer);
			if ( !result )
				return result;

			serializer.TraiteInt ( ref m_nNumJour );

			serializer.TraiteInt ( ref m_nEcartMois );

			return result;
		}

		/// ///////////////////////////////////////////
		protected override string GetMyLibelle()
		{
			string strLib = I.T("Every @1 |30001",NumJour.ToString());
			if ( EcartMois == 1 )
				strLib += I.T("of the month|30002");
			else strLib += I.T("Every  @1 months|30003",EcartMois.ToString());
			return strLib;
		}

		/// ///////////////////////////////////////////
		protected override CDateTimeEx GetMyNextOccurence(DateTime dtDateExecutionPrecedente, bool bPremiereExecution)
		{
			DateTime dtVal = dtDateExecutionPrecedente;
			//Récupère le dernier jour du mois de la date d'execution
			dtVal = new DateTime ( dtVal.Year, dtVal.Month, 1, dtVal.Hour, dtVal.Minute, dtVal.Second );
			dtVal = dtVal.AddMonths ( 1 );
			dtVal = dtVal.AddDays ( -1 );
			int nJour = Math.Min ( dtVal.Day, m_nNumJour );

			dtVal = dtDateExecutionPrecedente;
			if ( dtVal.Day > nJour )
			{
				dtVal = new DateTime ( dtVal.Year, dtVal.Month, 1, 0, 0, 0 );
				dtVal = dtVal.AddMonths ( 1 );
			}
			dtVal = new DateTime ( dtVal.Year, dtVal.Month, nJour, dtVal.Hour, dtVal.Minute, dtVal.Second );
			if ( !bPremiereExecution )
				dtVal = dtVal.AddMonths(m_nEcartMois);
			int nHeure = (int)Heure;
			int nMinute = (int)Math.Round((Heure - nHeure) * 60.0, 0);
			dtVal = new DateTime(dtVal.Year, dtVal.Month, dtVal.Day, nHeure, nMinute, 0);
			return new CDateTimeEx(dtVal);
		}

		

		/// ///////////////////////////////////////////
		public int NumJour
		{
			get
			{
				return m_nNumJour;
			}
			set
			{
				m_nNumJour = value;
			}
		}

		/// ///////////////////////////////////////////
		public int EcartMois
		{
			get
			{
				return m_nEcartMois;
			}
			set
			{
				m_nEcartMois = value;
			}
		}


	}
}
