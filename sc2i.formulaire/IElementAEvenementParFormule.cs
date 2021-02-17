using System;
using System.Collections.Generic;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace sc2i.formulaire
{
	[Serializable]
	public class CDescriptionEvenementParFormule : I2iSerializable
	{
		//-----------------------------------------------
		private string m_strIdEvenement;
		private string m_strNomEvenement;
		private string m_strDescriptionEvenement;
		public CDescriptionEvenementParFormule( 
			string strIdEvenement, 
			string strNomEvenement,
			string strDescription)
		{
			m_strIdEvenement = strIdEvenement;
			m_strNomEvenement = strNomEvenement;
			m_strDescriptionEvenement = strDescription;
		}

		//-----------------------------------------------
		public string IdEvenement
		{
			get
			{
				return m_strIdEvenement;
			}
			set
			{
				m_strIdEvenement = value;
			}
		}

		//-----------------------------------------------
		public string DescriptionEvenement
		{
			get
			{
				return m_strDescriptionEvenement;
			}
			set
			{
				m_strDescriptionEvenement = value;
			}
		}

		//-----------------------------------------------
		public string NomEvenement
		{
			get
			{
				return m_strNomEvenement;
			}
			set
			{
				m_strNomEvenement = value;
			}
		}

        //-----------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-----------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            serializer.TraiteString ( ref m_strIdEvenement );
            serializer.TraiteString ( ref m_strNomEvenement );
            serializer.TraiteString ( ref m_strDescriptionEvenement );
            return result;

        }
    }

	[Serializable]
	public class CHandlerEvenementParFormule : I2iSerializable
	{
		private string m_strIdEvenement = "";
		private C2iExpression m_formuleEvenement = null;

		//-----------------------------------------------
		public CHandlerEvenementParFormule()
		{
		}

		//-----------------------------------------------
		public CHandlerEvenementParFormule(string strIdEvenement,
			C2iExpression formule)
		{
			m_strIdEvenement = strIdEvenement;
			m_formuleEvenement = formule;
		}

		//-----------------------------------------------
		public string IdEvenement
		{
			get
			{
				return m_strIdEvenement;
			}
			set
			{
				m_strIdEvenement = value;
			}
		}

		//-----------------------------------------------
		public C2iExpression FormuleEvenement
		{
			get
			{
				return m_formuleEvenement;
			}
			set
			{
				m_formuleEvenement = value;
			}
		}

		//-----------------------------------------------
		private int GetNumVersion()
		{
			return 0;
		}

		//-----------------------------------------------
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			serializer.TraiteString(ref m_strIdEvenement);
			result = serializer.TraiteObject<C2iExpression>(ref m_formuleEvenement);
			return result;
		}

	}

	public interface IElementAEvenementParFormule
	{
		CDescriptionEvenementParFormule[] GetDescriptionsEvenements();

		CHandlerEvenementParFormule[] GetHanlders();

		CHandlerEvenementParFormule GetHandler(string strIdEvenement);

		void SetHandler(CHandlerEvenementParFormule handler);
	}

}
