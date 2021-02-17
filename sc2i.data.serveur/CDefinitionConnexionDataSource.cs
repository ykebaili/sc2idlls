using System;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CDefinitionConnexionDataSource.
	/// </summary>
	public class CDefinitionConnexionDataSource
	{
		private Type m_typeConnexion;
		private string m_strConnexionString;
		private string m_strIdConnexion;
        private string m_strPrefixeTables;

		////////////////////////////////////////////////////////////////////////
		public CDefinitionConnexionDataSource( string strIdConnexion, Type typeConnexion, string strConnexionString )
		{
			m_typeConnexion = typeConnexion;
			m_strConnexionString = strConnexionString;
			m_strIdConnexion = strIdConnexion;
            m_strPrefixeTables = "";
		}

        ////////////////////////////////////////////////////////////////////////
        public CDefinitionConnexionDataSource(string strIdConnexion, Type typeConnexion, string strConnexionString,
            string strPrefixeTables)
        {
            m_typeConnexion = typeConnexion;
            m_strConnexionString = strConnexionString;
            m_strIdConnexion = strIdConnexion;
            m_strPrefixeTables = strPrefixeTables;
        }


		////////////////////////////////////////////////////////////////////////
		public string IdConnexion
		{
			get
			{
				return m_strIdConnexion;
			}
		}

        ////////////////////////////////////////////////////////////////////////
        public string PrefixeTables
        {
            get
            {
                return m_strPrefixeTables;
            }
        }

		////////////////////////////////////////////////////////////////////////
		public Type TypeConnexion
		{
			get
			{
				return m_typeConnexion;
			}
		}

		////////////////////////////////////////////////////////////////////////
		public string ConnexionString
		{
			get
			{
				return m_strConnexionString;
			}
		}
		
	}
}
