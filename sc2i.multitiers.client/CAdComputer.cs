using System;
using System.Collections;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CAdComputer.
	/// </summary>
#if PDA
#else
	[Serializable]
#endif
	public class CAdComputer : IComparable
	{

		private string m_strNom;

		//////////////////////////////////////
		public CAdComputer( string strNom)
		{
			m_strNom = strNom;
		}


		//////////////////////////////////////
		public string Nom
		{
			get
			{
				return m_strNom;
			}
			set
			{
				m_strNom = value;
			}

		}

		//////////////////////////////////////
		public int CompareTo ( object obj )
		{
			if ( !(obj is CAdComputer) )
				return 1;
			CAdComputer Computer = (CAdComputer)obj;
			return Nom.CompareTo(Computer.Nom);
		}
		//////////////////////////////////////
		public override string ToString()
		{
			return Nom;
		}

		//////////////////////////////////////
		public static CAdComputer[] GetComputers( int nIdSession )
		{
			IAdComputersServer adComputers = (IAdComputersServer)C2iFactory.GetNewObjetForSession("CAdComputersServeur", typeof(IAdComputersServer), nIdSession );
			if ( adComputers != null )
				return adComputers.GetComputers();
			return null;
		}

		//////////////////////////////////////
		public static CAdComputer GetComputer ( int nIdSession, string strNom )
		{
			IAdComputersServer adComputers = (IAdComputersServer)C2iFactory.GetNewObjetForSession("CAdComputersServeur", typeof(IAdComputersServer), nIdSession );
			if ( adComputers != null )
				return adComputers.GetComputer ( strNom );
			return null;
		}
	}
}
