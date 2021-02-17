using System;

using sc2i.common;


namespace sc2i.common.planification
{

	/// <summary>
	/// Paramètre d'une planification basée sur le mois
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CPlanificationTacheMensuelleJourSemaine : CPlanificationTache
	{
		
		//Numéro d'apparition du jour dans le mois
		//1 = le premier jour de semaine demandé (1er lundi par exemple)
		//2 ...
		//5 : Le dernier du mois
		private int m_nNumeroJour = 1;

		private DayOfWeek m_jour;

		/// <summary>
		/// s'execute tous les m_nEcartMois
		/// </summary>
		private int m_nEcartMois = 1;

		/// ////////////////////////////////////////
		public CPlanificationTacheMensuelleJourSemaine()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}


		/// ///////////////////////////////////////////
		public static void Autoexec()
		{
			CPlanificationTache.RegisterTypePlanification ( typeof(CPlanificationTacheMensuelleJourSemaine), "Mensuelle (jour semaine)");
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
			result = base.Serialize ( serializer );

			serializer.TraiteInt ( ref m_nEcartMois );

			serializer.TraiteInt ( ref m_nNumeroJour );

			int nTmp = (int)m_jour;
			serializer.TraiteInt ( ref nTmp );
			m_jour = (DayOfWeek)nTmp;

			return result;
		}

		/// ////////////////////////////////////////
		public int NumeroJour
		{
			get
			{
				return m_nNumeroJour;
			}
			set
			{
				m_nNumeroJour = value;
			}
		}

		/// ////////////////////////////////////////
		public DayOfWeek JourSemaine
		{
			get
			{
				return m_jour;
			}
			set
			{
				m_jour = value;
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

		/// ///////////////////////////////////////////
		protected override string GetMyLibelle()
		{
			string strLib = I.T("Every|30001 ");
			switch ( NumeroJour )
			{
				case 1 :
					strLib +=I.T("first|30004 ");
					break;
				case 2 :
                    strLib += I.T("second|30005 ");
					break;
				case 3 :
                    strLib += I.T("third|30006 ");
					break;
				case 4 :
                    strLib += I.T("fourth|30007 ");
					break;
				case 5 :
                    strLib += I.T("last|30008 "); 
					break;
			}
			strLib += CUtilDate.GetNomJour ( JourSemaine, false )+"s ";
			if ( EcartMois == 1 )
				strLib += I.T("of the month|30002");
			else
				strLib += I.T("Every @1 months|30003",EcartMois.ToString());
			return strLib;
		}

		/// ///////////////////////////////////////////
		protected override CDateTimeEx GetMyNextOccurence(DateTime dtDateExecutionPrecedente, bool bPremiereExecution)
		{
			DateTime dtVal = dtDateExecutionPrecedente;
			if ( !bPremiereExecution )
			{
				dtVal =  dtVal.AddMonths(m_nEcartMois);
				dtVal = new DateTime ( dtVal.Year, dtVal.Month, 1, dtVal.Hour, dtVal.Minute, dtVal.Second );
			}
			if ( m_nNumeroJour != 5 )
			{
				//Récupère le premier jour du mois
				dtVal = new DateTime ( dtVal.Year, dtVal.Month, 1 );
				DayOfWeek jourPremier = dtVal.DayOfWeek;
				int nEcart = (int)m_jour-(int)jourPremier;
				if ( nEcart < 0 )
					nEcart = 7+nEcart;
				dtVal = dtVal.AddDays ( nEcart );
				dtVal = dtVal.AddDays( 7*(m_nNumeroJour-1) );
			}
			else
			{
				//Compte à partir de la fin
				dtVal = new DateTime ( dtVal.Year, dtVal.Month, 1 );
				dtVal = dtVal.AddMonths(1).AddDays(-1);//Dernier jour du mois
				DayOfWeek jourDernier = dtVal.DayOfWeek;
				int nEcart = (int)m_jour - (int)jourDernier;
				if ( nEcart > 0 )
					nEcart = - 7 + nEcart;
				dtVal = dtVal.AddDays(nEcart);
			}
			if ( dtVal < dtDateExecutionPrecedente )
			{
				dtVal = new DateTime ( dtVal.Year, dtVal.Month, 1, 0, 0, 0 );
				dtVal = dtVal.AddMonths ( 1 );
				dtVal = new DateTime ( dtVal.Year, dtVal.Month, 1, dtVal.Hour, dtVal.Minute, dtVal.Second );
				return GetMyNextOccurence ( dtVal, true );
			}
			int nHeure = (int)Heure;
			int nMinute = (int)Math.Round((Heure - nHeure) * 60.0, 0);
			dtVal = new DateTime(dtVal.Year, dtVal.Month, dtVal.Day, nHeure, nMinute, 0);
			return new CDateTimeEx(dtVal);

		}

	}
}
