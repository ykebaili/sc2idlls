using System;

using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CConteneurProgressionAvecSponsor.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CConteneurProgressionAvecSponsor : CConteneurIndicateurProgression
	{
		private C2iSponsor m_sponsor = new C2iSponsor();
		
		protected CConteneurProgressionAvecSponsor( )
		{
		}

		public override IIndicateurProgression Indicateur
		{
			get
			{
				return m_indicateur;
			}
			set
			{
				try
				{
					if ( m_indicateur != null && value != m_indicateur )
						m_sponsor.Unregister ( m_indicateur );

				}
				catch
				{
				}
				m_indicateur = value;
				m_sponsor.Register ( m_indicateur );
			}
		}
				

		~CConteneurProgressionAvecSponsor()
		{
			try
			{
				m_sponsor.Unregister ( m_indicateur );
			}
			catch
			{}
		}

		public static void Autoexec()
		{
			CConteneurIndicateurProgression.m_typeConteneurParDefaut =  typeof ( CConteneurProgressionAvecSponsor );
		}
	}
}
