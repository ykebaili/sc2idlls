using System;
using System.Globalization;

using sc2i.common;

namespace sc2i.common.planification
{
	/// <summary>
	/// Description résumée de CPlanificationTacheQuotidienne.
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class CPlanificationTacheQuotidienne : CPlanificationTache
	{
		private int m_nEcartJours = 1;

		/// /////////////////////////////////////////
		public CPlanificationTacheQuotidienne()
		{
			
		}

		/// ///////////////////////////////////////////
		public static void Autoexec()
		{
			CPlanificationTache.RegisterTypePlanification ( typeof(CPlanificationTacheQuotidienne), "Quotidienne");
		}

		/// /////////////////////////////////////////
		public int EcartJours
		{
			get
			{
				return m_nEcartJours;
			}
			set
			{
				m_nEcartJours = value;
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

			serializer.TraiteInt ( ref m_nEcartJours );

			return result;
		}

		/// ///////////////////////////////////////////
		protected override string GetMyLibelle()
		{
            string strLib = I.T("Every|30001"); 
			if ( EcartJours != 1 )
				strLib += EcartJours.ToString(CultureInfo.CurrentCulture)+" ";
			strLib += "day|30009";
			return strLib;
		}

		/// ///////////////////////////////////////////
		protected override CDateTimeEx GetMyNextOccurence(DateTime dtDateExecutionPrecedente, bool bPremiereExecution)
		{
			DateTime dtVal = dtDateExecutionPrecedente;
			if ( !bPremiereExecution )
				dtVal = dtVal.AddDays(m_nEcartJours);
			int nHeure = (int)Heure;
			int nMinute = (int)Math.Round((Heure - nHeure) * 60.0, 0);
			dtVal = new DateTime(dtVal.Year, dtVal.Month, dtVal.Day, nHeure, nMinute, 0);
			return new CDateTimeEx(dtVal);
		}


	}
}
