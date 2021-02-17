using System;
using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Indique les configurations d'impression favorites pour une session
	/// </summary>
	[Serializable]
	public class CConfigurationsImpression : I2iSerializable
	{
		//"" = par défaut
		private string m_strNomImprimanteSurClient = "";
		
		//"" = par défaut
		private string m_strNomImprimanteSurServeur = "";

		public CConfigurationsImpression()
		{
		}

		/// /////////////////////////////////////////////
		public string NomImprimanteSurClient
		{
			get
			{
				return ( m_strNomImprimanteSurClient );
			}
			set
			{
				m_strNomImprimanteSurClient = value;
			}
		}

		/// /////////////////////////////////////////////
		public string NomImprimanteSurServeur
		{
			get
			{
				return m_strNomImprimanteSurServeur;
			}
			set
			{
				m_strNomImprimanteSurServeur = value;
			}
		}
		#region Membres de I2iSerializable
		private int GetNumVersion()
		{
			return 0;
		}

		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strNomImprimanteSurClient );
			serializer.TraiteString ( ref m_strNomImprimanteSurServeur ) ;
			return result;
		}

		#endregion
	}
}
