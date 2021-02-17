using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data.dynamic
{
	[Serializable]
	public class C2iChampFusion : IChampDeTable
	{
		private string m_strNomChamp = "";
		private Type m_typeDonnee = typeof(string);

		public C2iChampFusion(string strChamp, Type typeDonnee)
		{
			m_strNomChamp = strChamp;
			m_typeDonnee = typeDonnee;
		}

		//-----------------------------------
		public string NomChamp
		{
			get
			{
				return m_strNomChamp;
			}
			set
			{
				m_strNomChamp = value;
			}
		}

		//-----------------------------------
		public Type TypeDonnee
		{
			get
			{

				return m_typeDonnee;
			}
			set
			{
			m_typeDonnee = value;
			}
		}

		//-----------------------------------
		private int GetNumVersion()
		{
			return 0;
		}

		//-----------------------------------
		public CResultAErreur Serialize(C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			serializer.TraiteString(ref m_strNomChamp);
			serializer.TraiteType(ref m_typeDonnee);
			return result;
		}
		
	}
}
