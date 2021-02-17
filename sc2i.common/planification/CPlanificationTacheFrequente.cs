using System;
using System.Globalization;

using sc2i.common;

namespace sc2i.common.planification
{
	/// <summary>
	/// Tâche réalisée toutes les x heures ou minutes, 
	/// durant une certaine plage horaires de certains jours de la semaine
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CPlanificationTacheFrequente : CPlanificationTache
	{
		public enum EUniteTemps
		{
			Minute = 0,
			Hour = 1
		};

		private JoursBinaires m_joursExecution = JoursBinaires.Lundi |
			JoursBinaires.Mardi |
			JoursBinaires.Mercredi |
			JoursBinaires.Jeudi |
			JoursBinaires.Vendredi |
			JoursBinaires.Samedi |
			JoursBinaires.Dimanche;

		//Heure de fin de la plage horaire (c'est Heure qui détermine l'heure de début)
		private double m_dHeureFin = 18;

		//Nombre d'unités entre chaque planification
		private int m_nEcart = 4;

		//Unité de planification
		private EUniteTemps m_nUnite = EUniteTemps.Hour;

		/// /////////////////////////////////////////
		public CPlanificationTacheFrequente()
		{
			
		}

		/// ///////////////////////////////////////////
		public static void Autoexec()
		{
			CPlanificationTache.RegisterTypePlanification ( typeof(CPlanificationTacheFrequente), I.T("Frequent|100"));
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
				if (value == JoursBinaires.Aucun)
					m_joursExecution = JoursBinaires.Lundi;
			}
		}

		//////////////////////////////////////////////
		public double HeureFin
		{
			get
			{
				return m_dHeureFin;
			}
			set
			{
				m_dHeureFin = value;
			}
		}

		//////////////////////////////////////////////
		public int Ecart
		{
			get
			{
				return m_nEcart;
			}
			set
			{
				m_nEcart = value;
			}
		}

		//////////////////////////////////////////////
		public EUniteTemps Unite
		{
			get
			{
				return m_nUnite;
			}
			set
			{
				m_nUnite = value;
			}
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// /////////////////////////////////////////
		public override CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if ( !result )
				return result;

			int nJours = (int)m_joursExecution;
			serializer.TraiteInt(ref nJours);
			m_joursExecution = (JoursBinaires)nJours;

			serializer.TraiteDouble(ref m_dHeureFin);
			serializer.TraiteInt ( ref m_nEcart );
			int nUnite = (int)m_nUnite;
			serializer.TraiteInt(ref nUnite);
			m_nUnite = (EUniteTemps)nUnite;

			return result;
		}

		/// ///////////////////////////////////////////
		protected override string GetMyLibelle()
		{
			string strLib = I.T("Each @1 @2|101", m_nEcart.ToString(), CUtilSurEnum.GetNomConvivial ( m_nUnite.ToString() ));
			return strLib;
		}

		/// ////////////////////////////////////////
		///Retourne la prochaine date correspondant à la planification suivant
		///la date de référence
		///Peut retourner null, si aucune occurence ne suit la date demandée
		public override CDateTimeEx GetNextOccurence(DateTime dtDateExecutionPrecedente, bool bPremiereExecution)
		{
			if (DateDebut != null && dtDateExecutionPrecedente < DateDebut.DateTimeValue)
				dtDateExecutionPrecedente = DateDebut.DateTimeValue;
			if (DateFin != null && dtDateExecutionPrecedente > DateFin.DateTimeValue)
				return null;
			CDateTimeEx dt = GetMyNextOccurence(dtDateExecutionPrecedente, bPremiereExecution);
			if (dt == null)
				return null;
			if (DateFin != null && dt.DateTimeValue > DateFin.DateTimeValue)
				return null;

			return dt;
		}

		/// ///////////////////////////////////////////
		protected override CDateTimeEx GetMyNextOccurence(DateTime dtDateExecutionPrecedente, bool bPremiereExecution)
		{
			if ( JoursExecution == JoursBinaires.Aucun )
				return null;
			DateTime dtVal = dtDateExecutionPrecedente;
			double fHeure = ((double)dtVal.Hour) + ((double)dtVal.Minute) / 60;
			if (fHeure < Heure)
				dtVal = dtVal.Date.AddHours(Heure);
			else
			{
				switch (m_nUnite)
				{
					case EUniteTemps.Hour:
						dtVal = dtVal.AddHours(m_nEcart);
						break;
					case EUniteTemps.Minute:
						dtVal = dtVal.AddMinutes(m_nEcart);
						break;
				}
			}
			fHeure = ((double)dtVal.Hour) + ((double)dtVal.Minute) / 60;
			if (fHeure > HeureFin || fHeure < Heure)
			{
				dtVal = dtDateExecutionPrecedente.Date.AddDays(1);
				dtVal = dtVal.AddHours(Heure);
				JoursBinaires jour = CUtilDate.GetJourBinaireFor(dtVal.DayOfWeek);
				while ((jour & JoursExecution) != jour)
				{
					dtVal = dtVal.AddDays(1);
                    jour = CUtilDate.GetJourBinaireFor(dtVal.DayOfWeek);
				}
			}
			dtVal = new DateTime(dtVal.Year, dtVal.Month, dtVal.Day, dtVal.Hour, dtVal.Minute, 0);
			return dtVal;
		}


	}
}
