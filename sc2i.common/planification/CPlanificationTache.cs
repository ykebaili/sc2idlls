using System;
using System.Collections;
using System.Globalization;

using sc2i.common;

namespace sc2i.common.planification
{
	public enum UniteTemps
	{
		Seconde = 0,
		Minute,
		Heure,
		Jour,
		Semaine,
		Mois,
		Annee
	}
	/// <summary>
	/// Classe de base pour la planification de tâches
	/// </summary>
	[Serializable]
	public abstract class CPlanificationTache : I2iSerializable
	{
		public class CInfoTypePlanification
		{
			private Type m_typePlanification;
			private string m_strLibelle;

			public CInfoTypePlanification ( Type tp, string strLibelle )
			{
				TypePlanification = tp;
				m_strLibelle = strLibelle;
			}

            public Type TypePlanification
            {
                get
                {
                    return m_typePlanification;
                }
                set
                {
                    m_typePlanification = value;
                }
            }

			[DescriptionField]
			public string Libelle
			{
				get
				{
					return m_strLibelle;
				}
			}

			public override bool Equals(object obj)
			{
                CInfoTypePlanification info = (obj as CInfoTypePlanification);
				if ( info != null )
					return TypePlanification == info.TypePlanification;
				return false;
			}

			public override int GetHashCode()
			{
				return TypePlanification.GetHashCode();
			}


		}

		//Liste de CinfoTypePlanification
		private static ArrayList m_lstTypesPlanifications = new ArrayList();
		private double m_dHeure;//en heures

		private CDateTimeEx m_dateDebut;
		private CDateTimeEx m_dateFin;

		/// ////////////////////////////////////////
		protected CPlanificationTache()
		{
			m_dHeure = 9;
		}

		/// ////////////////////////////////////////
		public double Heure
		{
			get
			{
				return m_dHeure;
			}
			set
			{
				m_dHeure = value;
			}
		}

		/// ////////////////////////////////////////
		public CDateTimeEx DateDebut
		{
			get
			{
				return m_dateDebut;
			}
			set
			{
				m_dateDebut = value;
			}
		}

		/// ////////////////////////////////////////
		public CDateTimeEx DateFin
		{
			get
			{
				return m_dateFin;
			}
			set
			{
				m_dateFin = value;
			}
		}
		
		/// ////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
			//Version 1: stockage de l'heure en heures (flottant et non plus en 2.15 pour 2:15)
		}


		/// ////////////////////////////////////////
		public virtual CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			
			serializer.TraiteDouble ( ref m_dHeure );
			if (nVersion < 1)
			{
				int nHeure = (int)m_dHeure;
				double fMin = m_dHeure - nHeure;
				fMin = fMin * 100.0 / 60.0;
				m_dHeure = nHeure + fMin;
			}

			I2iSerializable objet = m_dateDebut;
			serializer.TraiteObject ( ref objet );
			m_dateDebut = (CDateTimeEx)objet;

			objet = m_dateFin;
			serializer.TraiteObject ( ref objet );
			m_dateFin = (CDateTimeEx)objet;

			return result;
		}

		//-------------------------------------------------------------------------
		public string GetHeureFormattee ( double dVal )
		{
			int nHeure = (int)dVal;
			int nMin = (int)Math.Round((dVal-nHeure)*60, 0);
			return nHeure.ToString(CultureInfo.CurrentCulture).PadLeft(2,'0')+":"+
                nMin.ToString(CultureInfo.CurrentCulture).PadLeft(2,'0');
		}

		/// ////////////////////////////////////////
		public string GetLibelle()
		{
			string strLib = "A "+GetHeureFormattee(m_dHeure)+" "+GetMyLibelle();
			return strLib;
		}

		/// ////////////////////////////////////////
		protected abstract string GetMyLibelle();


		/// ////////////////////////////////////////
		
		/// <summary>
		/// retourne la prochaine date correspondant à la planification
		/// Sans tenir compte de l'heure ni des dates min et max.
		/// </summary>
		/// <param name="dateExecutionPrecedente"></param>
		/// <returns></returns>
		protected abstract CDateTimeEx GetMyNextOccurence( DateTime dtDateExecutionPrecedente, bool bPremiereExecution);

		/// ////////////////////////////////////////
		///Retourne la prochaine date correspondant à la planification suivant
		///la date de référence
		///Peut retourner null, si aucune occurence ne suit la date demandée
		public virtual CDateTimeEx GetNextOccurence ( DateTime dtDateExecutionPrecedente, bool bPremiereExecution )
		{
			if ( DateDebut != null && dtDateExecutionPrecedente < DateDebut.DateTimeValue )
				dtDateExecutionPrecedente = DateDebut.DateTimeValue;
			if ( DateFin != null && dtDateExecutionPrecedente > DateFin.DateTimeValue )
				return null;
			double dHeure = dtDateExecutionPrecedente.Hour+(double)dtDateExecutionPrecedente.Minute/100;
			if ( dHeure > m_dHeure )
				dtDateExecutionPrecedente = dtDateExecutionPrecedente.AddDays(1);


			CDateTimeEx dt = GetMyNextOccurence ( dtDateExecutionPrecedente, bPremiereExecution );
			if ( dt == null )
				return null;
			if(  DateFin != null && dt.DateTimeValue > DateFin.DateTimeValue )
				return null;

			return dt;
		}

		/// ////////////////////////////////////////
		public static void RegisterTypePlanification(Type tp, string strLibelle )
		{
			if ( typeof(CPlanificationTache).IsAssignableFrom(tp ) )
				m_lstTypesPlanifications.Add ( new CInfoTypePlanification ( tp, strLibelle ) );
		}

		/// ////////////////////////////////////////
		public static CInfoTypePlanification[] GetTypesPlanificationsTaches
		{
			get
			{
				return ( CInfoTypePlanification[] )m_lstTypesPlanifications.ToArray ( typeof(CInfoTypePlanification) );
			}
		}

	}
}
