using System;

namespace sc2i.data
{
	[Serializable]
	public class CInfoChampTable
	{
		private string m_strNomChamp;
		public readonly Type TypeDonnee;
		public readonly int Longueur;
		public readonly bool IsLongString = false;
		public readonly bool IsId;
		private bool m_bNullAuthorized;
		public readonly bool ExclureFormStandardUpdate=false;
		private  bool m_bIsAutoId = false;
		private string m_strPropriete = "";
		private string m_strNomConvivial = "";
		private bool m_bReadOnly = false;
		private bool m_bIsIndex = false;
		
		//Indique que le champ n'est pas un champ de la base de données (si false)
		public readonly bool m_bIsInDB = true;

		public CInfoChampTable ( 
			string strNomChamp, 
			Type typeDonnee, 
			int nLongueur, 
			bool bIsLongString, 
			bool bIsId , 
			bool bm_bNullAuthorized,
			bool bExclureFromStandardUpdate,
			bool bIsInDB )
		{
			NomChamp = strNomChamp;
			TypeDonnee = typeDonnee;
			IsId = bIsId;
			Longueur = nLongueur;
			m_bNullAuthorized = bm_bNullAuthorized;
			ExclureFormStandardUpdate = bExclureFromStandardUpdate;
			m_bIsInDB = bIsInDB;
			IsLongString = bIsLongString;
		}

		/// <summary>
		/// A utiliser pour les identifiants automatiques
		/// </summary>
		/// <param name="nomChamp"></param>
		public static CInfoChampTable CreateIdAuto( string strNomChamp )
		{
			CInfoChampTable info = new CInfoChampTable(
				strNomChamp,
				typeof(int),
				0,
				false,
				true,
				false,
				false,
				true);
			info.m_bIsAutoId = true;
			info.NomConvivial = "Id";
            info.IsIndex = true;
			return info;
		}

		/// ////////////////////////////////////////////////////////
		public string Propriete
		{
			get
			{
				return m_strPropriete;
			}
			set
			{
				m_strPropriete = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public bool NullAuthorized
		{
			get
			{
				return m_bNullAuthorized;
			}
			set
			{
				m_bNullAuthorized = value;
			}
		}



		/// ////////////////////////////////////////////////////////
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

		/// ////////////////////////////////////////////////////////
		public bool IsIndex
		{
			get
			{
				return m_bIsIndex;
			}
			set
			{
				m_bIsIndex = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public string NomConvivial
		{
			get
			{
				return m_strNomConvivial;
			}
			set
			{
				m_strNomConvivial = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public bool IsAutoId
		{
			get
			{
				return m_bIsAutoId;
			}
			set
			{
				m_bIsAutoId = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public bool ReadOnly
		{
			get
			{
				return m_bReadOnly;
			}
			set
			{
				m_bReadOnly = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne le nom convivial, de propriété ou de champ 
		/// </summary>
		/// <returns></returns>
		public string NomConvivialPropOuChamp
		{
			get
			{
				if ( NomConvivial != "" )
					return NomConvivial;
				if ( Propriete != "" )
					return Propriete;
				return NomChamp;
			}
		}

		/// ////////////////////////////////////////////////////////
		public override bool Equals(object obj)
		{
			if ( obj is string )
				return ((string)obj).Equals(NomChamp);
			if ( obj is CInfoChampTable )
				return ((CInfoChampTable)obj).NomChamp.Equals(NomChamp);
			return false;
		}

		/// ////////////////////////////////////////////////////////
		public override int GetHashCode()
		{
			return NomChamp.GetHashCode();
		}


				



	}
}