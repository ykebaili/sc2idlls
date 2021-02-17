using System;
using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de COptionDroitUtilisateur.
	/// </summary>
	[Flags]
	public enum OptionsDroit
	{
		All = 0xFFFF,
		Aucune = 0,
		Consultation = 1,
		Creation = 2,
		Modification = 4,
		Suppression = 8
	}

	public class COptionDroitUtilisateur
	{
		private OptionsDroit m_option;
		
		/// //////////////////////////////////////////////////////////////////////////////
		public COptionDroitUtilisateur ( OptionsDroit option )
		{
			m_option = option;
		}

		/// //////////////////////////////////////////////////////////////////////////////
		public OptionsDroit Option
		{
			get
			{
				return m_option;
			}
			set
			{
				m_option = value;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////
		public string Libelle
		{
			get
			{
				return GetLibelleOption ( m_option );
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////
		public override string ToString()
		{
			return Libelle;
		}

		public static string GetLibelleOption ( OptionsDroit opt )
		{
			switch ( opt )
			{
				case OptionsDroit.All : 
					return "";
				case OptionsDroit.Consultation :
					return I.T("Consultation|90");
				case OptionsDroit.Creation :
					return I.T("Creation|91");
				case OptionsDroit.Modification :
					return I.T("Modification|92");
				case OptionsDroit.Suppression :
					return I.T("Suppression|93");
				default :
					return I.T("Unknown option|94");
			}
		}
	}
}
