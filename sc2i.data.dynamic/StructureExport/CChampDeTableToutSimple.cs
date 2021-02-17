using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// C'est un IChamp de table qui a juste un nom 
	/// et un type. C'est pour ça qu'il est tout simple
	/// </summary>
	[Serializable]
	public class CChampDeTableToutSimple : IChampDeTable
	{
		private Type m_typeDonnees=typeof(string);
		private string m_strNom = "";

		//------------------------------------
		public CChampDeTableToutSimple()
		{
		}

		//------------------------------------
		public CChampDeTableToutSimple ( string strNom, Type type )
		{
			m_strNom = strNom;
			m_typeDonnees = type;
		}

		//------------------------------------
		public string NomChamp
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

		//------------------------------------
		public Type TypeDonnee
		{
			get
			{
				return m_typeDonnees;
			}
			set
			{
				m_typeDonnees = value;
			}
		}

		#region I2iSerializable Membres

		private int GetNumVersion()
		{
			return 0;
		}
		public sc2i.common.CResultAErreur Serialize(sc2i.common.C2iSerializer serializer)
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion(ref nVersion);
			if (!result)
				return result;
			serializer.TraiteType(ref m_typeDonnees);
			serializer.TraiteString(ref m_strNom);
			return result;
		}

		#endregion
	}
}
