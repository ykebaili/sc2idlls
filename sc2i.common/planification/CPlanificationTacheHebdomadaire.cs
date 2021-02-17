using System;

using sc2i.common;

namespace sc2i.common.planification
{
	/// <summary>
	/// Description résumée de CPlanificationTacheHebdomadaire.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CPlanificationTacheHebdomadaire : CPlanificationTache
	{
		private int m_nEcartSemaine = 1;
		private JoursBinaires m_joursExecution = JoursBinaires.Lundi |
			JoursBinaires.Mardi |
			JoursBinaires.Mercredi |
			JoursBinaires.Jeudi |
			JoursBinaires.Vendredi |
			JoursBinaires.Samedi |
			JoursBinaires.Dimanche;

		/// ///////////////////////////////////////////
		public CPlanificationTacheHebdomadaire()
		{
			
		}

		/// ///////////////////////////////////////////
		public static void Autoexec()
		{
			CPlanificationTache.RegisterTypePlanification ( typeof(CPlanificationTacheHebdomadaire), "Hebdomadaire");
		}

		/// ///////////////////////////////////////////
		public int EcartSemaine
		{
			get
			{
				return m_nEcartSemaine;
			}
			set
			{
				m_nEcartSemaine = value;
			}
		}

		/// ///////////////////////////////////////////
		public JoursBinaires JoursExecution
		{
			get
			{
				return m_joursExecution;
			}
			set
			{
				m_joursExecution = value;
				if ( value == JoursBinaires.Aucun )
					m_joursExecution = JoursBinaires.Lundi;
			}
		}

		/// ///////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////
		public override CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if(  !result )
				return result;

			serializer.TraiteInt ( ref m_nEcartSemaine );

			int nJours = (int)m_joursExecution;
			serializer.TraiteInt ( ref nJours );
			m_joursExecution = (JoursBinaires)nJours;

			return result;
		}

		/// ///////////////////////////////////////////
		protected override string GetMyLibelle()
		{
			if ( JoursExecution == JoursBinaires.Aucun )
				return I.T("Never|30000");
			string strLib = " Every|30001 ";
			if ( EcartSemaine != 1 )
				strLib+= EcartSemaine+" ";
			for ( int nTmp = 0; nTmp < 7; nTmp++ )
			{
				if ( (JoursExecution & CUtilDate.GetJourBinaireForBaseLundi ( nTmp ) ) != 0 )
					strLib += CUtilDate.GetNomJour ( (DayOfWeek)((nTmp+8)%7), true )+",";
			}
			strLib = strLib.Substring(0, strLib.Length-1);
			return strLib;
			
		}

		/// ///////////////////////////////////////////
		protected override CDateTimeEx GetMyNextOccurence(DateTime dtDateExecutionPrecedente, bool bPremiereExecution)
		{
			return GetMyNextOccurence ( dtDateExecutionPrecedente, bPremiereExecution, true );
		}

		/// ///////////////////////////////////////////
		protected CDateTimeEx GetMyNextOccurence(DateTime dtDateExecutionPrecedente, bool bPremiereExecution, bool bSuperieur)
		{
			DateTime dtVal = dtDateExecutionPrecedente;
			if ( JoursExecution == JoursBinaires.Aucun )
				JoursExecution = JoursBinaires.Lundi;
			JoursBinaires jour = CUtilDate.GetJourBinaireFor ( dtVal.DayOfWeek );
			int nInc = 0;
			if ( bPremiereExecution )
			{
				while ( ( (jour & JoursExecution) != jour) && nInc < 7 )
				{
					nInc++;
					dtVal = dtVal.AddDays ( 1 );
					jour = CUtilDate.GetJourBinaireFor ( dtVal.DayOfWeek );
				}
			}
			else
			{
				if ( bSuperieur )
				{
					if ( dtVal.DayOfWeek == DayOfWeek.Sunday )
						dtVal=dtVal.AddDays(7*(m_nEcartSemaine-1)+1);
					else
						dtVal=dtVal.AddDays(1);
					
				}
				jour = CUtilDate.GetJourBinaireFor ( dtVal.DayOfWeek );
				//Regarde les jours suivants de la même semaine
				while ( (jour & JoursExecution) != jour && dtVal.DayOfWeek!=DayOfWeek.Sunday )
				{
					dtVal=dtVal.AddDays(1);
					jour = CUtilDate.GetJourBinaireFor ( dtVal.DayOfWeek );
				}
				if ( (jour & JoursExecution) != jour )
				{
					//Il faut passer à la semaine d'après
					return GetMyNextOccurence ( CUtilDate.LundiDeSemaine(dtDateExecutionPrecedente.AddDays(7*m_nEcartSemaine)), false, false );
				}
			}
			int nHeure = (int)Heure;
			int nMinute = (int)Math.Round((Heure - nHeure) * 60, 0);
			dtVal = new DateTime(dtVal.Year, dtVal.Month, dtVal.Day, nHeure, nMinute, 0);
			return new CDateTimeEx(dtVal);
		}



	}
}
