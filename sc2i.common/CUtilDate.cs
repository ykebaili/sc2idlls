using System;
using System.Globalization;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CUtilDate.
	/// </summary>
	[Flags]
	public enum JoursBinaires
	{
		Aucun = 0,
		Lundi = 1,
		Mardi = 2,
		Mercredi = 4,
		Jeudi = 8,
		Vendredi = 16,
		Samedi = 32,
		Dimanche = 64
	}

	public sealed class CUtilDate
	{
		private CUtilDate()
		{
		}

		public static DateTime SetTime0(DateTime dtVal)
		{
			DateTime dtTempDate = new DateTime(
				dtVal.Year,
				dtVal.Month,
				dtVal.Day,
				0,0,0,0);
			return dtTempDate;
		}

		//Retourne le jour de la semaine Lundi = 0
		public static int FrenchDayOfWeek(DayOfWeek dw)
		{
			int nVal = (int)dw;
			return (nVal+6)%7;
		}

		public static int GetWeekNum(DateTime dtVal)
		{
			//La semaine d'une date est la semaine du jeudi de la semaine de cette date
			DateTime dtTempDate = dtVal.AddDays ( 3-FrenchDayOfWeek(dtVal.DayOfWeek) );

			//La première semaine est la semaine qui contient le premier jeudi de l'année
			int nNbJoursToAdd = FrenchDayOfWeek((new DateTime(dtTempDate.Year,1,1)).DayOfWeek);
			if ( nNbJoursToAdd > 3 )
				nNbJoursToAdd = nNbJoursToAdd-7;
			
			int nVal = (dtTempDate.DayOfYear + nNbJoursToAdd -1)/7;
			return nVal+1;
		}

		public static int GetYearOfWeek ( DateTime dtVal )
		{
			int nSem = GetWeekNum ( dtVal );
			if ( nSem == 1 && dtVal.Month == 12 )
				return dtVal.Year +1;
			else if ( nSem >= 50 && dtVal.Month == 1 )
				return dtVal.Year-1;
			else
				return dtVal.Year;
		}

		public static DateTime LundiDeSemaine ( int nSemaine, int nAnnee )
		{
			DateTime dtVal = new DateTime(nAnnee,1,1);
			int nNbJoursToAdd = FrenchDayOfWeek((new DateTime(nAnnee,1,1)).DayOfWeek);
			if ( nNbJoursToAdd > 3 )
				nNbJoursToAdd = nNbJoursToAdd-7;
			dtVal = dtVal.AddDays ( -nNbJoursToAdd );
            if (nSemaine == int.MinValue)
                throw new ArgumentOutOfRangeException("nSemaine", I.T("The week number must be higher than Int32.MinValue|30066"));
			dtVal = dtVal.AddDays ( (nSemaine-1)*7 );
			return dtVal;
		}

		public static DateTime LundiDeSemaine ( DateTime dtVal )
		{
			return LundiDeSemaine ( GetWeekNum ( dtVal ), GetYearOfWeek ( dtVal ) );
		}
		

		public static string GetUniversalString ( DateTime dtVal )
		{
			string strChaine = dtVal.Year.ToString(CultureInfo.CurrentCulture).PadLeft(4,'0');
            strChaine += dtVal.Month.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0');
            strChaine += dtVal.Day.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0');
            strChaine += dtVal.Hour.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0');
            strChaine += dtVal.Minute.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0');
            strChaine += dtVal.Second.ToString(CultureInfo.CurrentCulture).PadLeft(2, '0');
			return strChaine;
		}

		public static DateTime FromUniversalString ( string strChaine )
		{
			if ( strChaine.Length !=14 )
				throw new Exception(I.T("Date conversion errror from Universal string|30067"));
			int nYear, nMonth, nDay, nHour, nMin, nSec;
			nYear = Int32.Parse(strChaine.Substring(0, 4), CultureInfo.CurrentCulture);
            nMonth = Int32.Parse(strChaine.Substring(4, 2), CultureInfo.CurrentCulture);
            nDay = Int32.Parse(strChaine.Substring(6, 2), CultureInfo.CurrentCulture);
            nHour = Int32.Parse(strChaine.Substring(8, 2), CultureInfo.CurrentCulture);
            nMin = Int32.Parse(strChaine.Substring(10, 2), CultureInfo.CurrentCulture);
            nSec = Int32.Parse(strChaine.Substring(12, 2), CultureInfo.CurrentCulture);
			return new DateTime(nYear, nMonth, nDay, nHour, nMin, nSec);
		}

		/// <summary>
		/// Retourne le nom d'un jour
		/// </summary>
		/// <param name="day"></param>
		/// <param name="bCourt">
		/// Indique s'il faut utiliser un format court ou non
		/// </param>
		/// <returns></returns>
		public static string GetNomJour ( DayOfWeek day, bool bCourt )
		{
			switch ( day )
			{
				case DayOfWeek.Sunday :
					if ( bCourt )
						return I.T("Sun|30068");
					else
						return I.T("Sunday|30069");						
				case DayOfWeek.Monday:
					if ( bCourt )
						return I.T("Mon|30070");
					else
						return I.T("Monday|30071");
				case DayOfWeek.Tuesday:
					if ( bCourt )
						return I.T("Tue|30072");
					else
						return I.T("Tuesday|30073");
				case DayOfWeek.Wednesday:
					if ( bCourt )
						return I.T("Wed|30074");
					else
						return I.T("Wednesday|30075");
				case DayOfWeek.Thursday:
					if ( bCourt )
						return I.T("Thu|30076");
					else
						return I.T("Thursday|30077");
				case DayOfWeek.Friday:
					if ( bCourt )
						return I.T("Fri|30078");
					else
						return I.T("Friday|30079");
				case DayOfWeek.Saturday :
					if ( bCourt )
						return I.T("Sat|30080");
					else
						return I.T("Saturday|30081");
			}
			return "";
		}

		/// <summary>
		/// Retourn le jours binaire correspondant au day of week
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		public static JoursBinaires GetJourBinaireFor ( DayOfWeek day )
		{
			switch ( day )
			{
				case DayOfWeek.Sunday :
					return JoursBinaires.Dimanche;
				case DayOfWeek.Monday:
					return JoursBinaires.Lundi;
				case DayOfWeek.Tuesday:
					return JoursBinaires.Mardi;
				case DayOfWeek.Wednesday:
					return JoursBinaires.Mercredi;
				case DayOfWeek.Thursday:
					return JoursBinaires.Jeudi;
				case DayOfWeek.Friday:
					return JoursBinaires.Vendredi;
				case DayOfWeek.Saturday :
					return JoursBinaires.Samedi;
			}
			return JoursBinaires.Dimanche;
		}

		/// <summary>
		/// Retourn le jours binaire correspondant au day of week
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		public static JoursBinaires GetJourBinaireForBaseLundi ( int nJour )
		{
			switch ( nJour )
			{
				case 6 :
					return JoursBinaires.Dimanche;
				case 0:
					return JoursBinaires.Lundi;
				case 1:
					return JoursBinaires.Mardi;
				case 2:
					return JoursBinaires.Mercredi;
				case 3:
					return JoursBinaires.Jeudi;
				case 4:
					return JoursBinaires.Vendredi;
				case 5 :
					return JoursBinaires.Samedi;
			}
			return JoursBinaires.Dimanche;
		}

		/// <summary>
		/// Retourne le dayOf week correspondant à un jour (lundi = 0 )
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		public static DayOfWeek GetDayOfWeekForBaseLundi ( int nJour )
		{
			switch ( nJour )
			{
				case 6 :
					return DayOfWeek.Sunday;
				case 0:
					return DayOfWeek.Monday;
				case 1:
					return DayOfWeek.Tuesday;
				case 2:
					return DayOfWeek.Wednesday;
				case 3:
					return DayOfWeek.Thursday;
				case 4:
					return DayOfWeek.Friday;
				case 5:
					return DayOfWeek.Saturday;
			}
			return DayOfWeek.Sunday;
		}

		/// <summary>
		/// Retourne le dayOf week correspondant à un jour binaire
		/// </summary>
		/// <param name="day"></param>
		/// <returns></returns>
		public static DayOfWeek GetDayOfWeekFor ( JoursBinaires jour )
		{
			switch ( jour )
			{
				case JoursBinaires.Dimanche :
					return DayOfWeek.Sunday;
				case JoursBinaires.Lundi:
					return DayOfWeek.Monday;
				case JoursBinaires.Mardi:
					return DayOfWeek.Tuesday;
				case JoursBinaires.Mercredi:
					return DayOfWeek.Wednesday;
				case JoursBinaires.Jeudi:
					return DayOfWeek.Thursday;
				case JoursBinaires.Vendredi:
					return DayOfWeek.Friday;
				case  JoursBinaires.Samedi:
					return DayOfWeek.Saturday;
			}
			return DayOfWeek.Sunday;
		}

		//Retourne true si le DayOfWeek fait partie de la liste de jours binaires
		public static bool IsDayInJourBinaire ( DayOfWeek day, JoursBinaires jours )
		{
			return (jours & GetJourBinaireFor(day)) != 0;
		}

		/// <summary>
		/// Retourne la liste des dayofWeek dans l'ordre de la semaine 
		/// </summary>
		public static DayOfWeek[] JoursSemaineOrdonnees
		{
			get
			{
				DayOfWeek[] lst = new DayOfWeek[7];
				lst[0] = DayOfWeek.Monday;
				lst[1] = DayOfWeek.Tuesday;
				lst[2] = DayOfWeek.Wednesday;
				lst[3] = DayOfWeek.Thursday;
				lst[4] = DayOfWeek.Friday;
				lst[5] = DayOfWeek.Saturday;
				lst[6] = DayOfWeek.Sunday;
				return lst;	
			}
		}


		/////////////////////////////////////////////////////////
		public static DateTime GetDateFromString ( string strDate, DateTime dtValeurSiImpossible )
		{
			try
			{
				return DateTime.Parse ( strDate, CultureInfo.CurrentCulture );
			}
			catch
			{
				//Ca ne marche pas avec le parse, il faut tenter autre chose :
				return dtValeurSiImpossible;
			}
		}

		/// <summary>
		/// Retourne le nom du mois
		/// </summary>
		/// <param name="nMois"></param>
		/// <param name="bCourt">Indique s'il faut utiliser le format court</param>
		/// <returns></returns>
		public static string GetNomMois ( int nMois, bool bCourt )
		{
			switch ( nMois )
			{
				case 1 :
					if ( bCourt )
						return I.T("Jan|30082");
					else
                        return I.T("January|30083");
				case 2 :
					if ( bCourt )
                        return I.T("Feb|30084");
					else
                        return I.T("February|30085");
				case 3 :
					if ( bCourt )
                        return I.T("Mar|30086");
					else
                        return I.T("March|30087");
				case 4 :
					if ( bCourt )
                        return I.T("Apr|30088");
					else
                        return I.T("April|30089");
				case 5 :
                    if (bCourt)
                        return I.T("May|30090");
                    else
                        return I.T("May|30091");
				case 6 :
					if ( bCourt )
                        return I.T("Jun|30092");
					else
                        return I.T("June|30093");
				case 7 :
					if ( bCourt )
                        return I.T("Jul|30094");
					else
                        return I.T("July|30095");
				case 8 :
					if ( bCourt )
                        return I.T("Aug|30096");
					else
                        return I.T("August|30097");
				case 09 :
					if ( bCourt )
                        return I.T("Sep|30098");
					else
                        return I.T("September|30099");
				case 10 :
					if ( bCourt )
                        return I.T("Oct|30100");
					else
                        return I.T("October|30101");
				case 11 :
					if ( bCourt )
                        return I.T("Nov|30102");
					else
						return I.T("November|30103");
				case 12 :
					if ( bCourt )
                        return I.T("Dec|30104");
					else
						return I.T("December|30105");
			}
			return "";
		}

		public static DateTime DateFromString( string strDate )
		{
			try
			{
				DateTime dtVal = DateTime.Parse ( strDate, CultureInfo.CurrentCulture);
				return dtVal;
			}
			catch ( Exception e )
			{
				throw (e);
			}
		}

        //---------------------------------
        public static string gFormat
        {
            get
            {
                return I.T("dd/MM/yyyy HH:mm|200");
            }
        }

        //---------------------------------
        public static string GFormat
        {
            get
            {
                return I.T("dd/MM/yyyy HH:mm:ss|201");
            }
        }

        //---------------------------------
        public static string dFormat
        {
            get
            {
                return I.T("dd/MM/yyyy|202");
            }
        }

	}
}
