using System;

namespace sc2i.data
{
	/// <summary>
	/// Description r�sum�e de TableFieldPropertyAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class TableFieldPropertyAttribute : Attribute
	{
		public readonly string NomChamp;
		private  int m_nLongueur;
		private bool m_bIsLongString = false;
		//Indique que la valeur null est autoris�e ou non
		public bool NullAutorise = false;
		
		//Indique que ce champ sera exclu des commandes d'update
		//(mais pas des commandes insert)
		public bool ExclureUpdateStandard = false;

		//Indique que la colonne ne va pas dans la base de donn�es, elle est g�n�rallement
		//un cache local ( si false )
		public bool IsInDb = true;

		

		/// ////////////////////////////////////////////////////////////////////
		public TableFieldPropertyAttribute( string strNomChamp)
		{
			NomChamp = strNomChamp;
			if ( !IsInDb )
				ExclureUpdateStandard = true;
		}

		/// ////////////////////////////////////////////////////////////////////
		public TableFieldPropertyAttribute( string strNomChamp, int nLongueur )
		{
			NomChamp = strNomChamp;
			Longueur = nLongueur;
			if ( !IsInDb )
				ExclureUpdateStandard = true;
		}

		/// ////////////////////////////////////////////////////////////////////
		public TableFieldPropertyAttribute( string strNomChamp, bool bNullAuthorized)
		{
			NomChamp = strNomChamp;
			NullAutorise = bNullAuthorized;
			if ( !IsInDb )
				ExclureUpdateStandard = true;
		}

		/// ////////////////////////////////////////////////////////////////////
		public bool IsLongString
		{
			get
			{
				return m_bIsLongString;
			}
			set
			{
				m_bIsLongString = value;
				if ( m_bIsLongString )
					Longueur = 4096;
			}
		}

		/// ////////////////////////////////////////////////////////////////////
		public int Longueur
		{
			get
			{
				return m_nLongueur;
			}
			set
			{
				m_nLongueur = value;
			}
		}
	}

	/// <summary>
	/// Indique que le champ ne doit pas �tre acced� en direct, mais toujours en appelant la propri�t�
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class AccesDirectInterditAttribute : Attribute
	{
	}



}
