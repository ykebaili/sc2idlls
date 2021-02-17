using System;
using System.Collections;
using System.Text.RegularExpressions;

using sc2i.common;


namespace sc2i.data
{

	//----------------------------------------
	public interface IChampDeTable : I2iSerializable
	{
		string NomChamp{get;}
		Type TypeDonnee{get;}
	}

	[Serializable]
	public class CSourceDeChampDeRequete : I2iSerializable
	{
		//Source du champ ( nom de champ (utilise la syntaxe des composantFiltreDynamique )
		private string m_strSource = "";

		//Source transformée sur la table destination, 
		private string m_strChampDeTable = "";

		//Alias permettant d'accéder à la table destination
		private string m_strAlias = "";


		public CSourceDeChampDeRequete()
		{
		}

		public CSourceDeChampDeRequete ( string strSource )
		{
			m_strSource = strSource;
		}

		//--------------------------------------
		public override bool Equals(object obj)
		{
			if (!(obj is CSourceDeChampDeRequete))
				return false;
			CSourceDeChampDeRequete source = (CSourceDeChampDeRequete)obj;
			if (source.m_strSource != m_strSource)
				return false;
			if (source.m_strChampDeTable != m_strChampDeTable)
				return false;
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		
		//--------------------------------------------------
		public string Source
		{
			get
			{
				return (this.m_strSource);
			}
			set
			{
				this.m_strSource = value;
			}
		}

		
		//--------------------------------------------------
		public string ChampDeTable
		{
			get
			{
				return (this.m_strChampDeTable);
			}
			set
			{
				this.m_strChampDeTable = value;
			}
		}

		
		//--------------------------------------------------
		public string Alias
		{
			get
			{
				return (this.m_strAlias);
			}
			set
			{
				this.m_strAlias = value;
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
			if  (! result )
				return result;

			serializer.TraiteString ( ref m_strSource );

			serializer.TraiteString ( ref m_strChampDeTable );

			serializer.TraiteString ( ref m_strAlias );

			return result;
		}

		#endregion
	}

	[Serializable]
	public class C2iChampDeRequete : I2iSerializable, IChampDeTable
	{
		//Nom du champ final
		private string m_strNomChampFinal="";

		/// <summary>
		/// Liste de CSourceDeChampDeRequete
		/// </summary>
		private ArrayList m_listeSources = new ArrayList();

		/// <summary>
		/// fonction SQL à appliquer à la donnée
		/// La fonction SQL remplace @1, @2, ... par les sources
		/// </summary>
		private string m_strFonctionSql = "";

		private Type m_typeDonneeAvantAgregation = typeof(string);

		private Type m_typeDonneeFinalForce = null;

		private OperationsAgregation m_operationAgregation = OperationsAgregation.Sum;

		private bool m_bGroupBy = false;

		//----------------------------------------
		public C2iChampDeRequete()
		{
		}

		//----------------------------------------
		public C2iChampDeRequete ( 
			string strNomChampFinal,
			CSourceDeChampDeRequete source,
			Type typeDonneeAvantAgregation,
			OperationsAgregation operation,
			bool bGroupBy )
		{
			NomChamp = strNomChampFinal;
			m_listeSources = new ArrayList();
			m_listeSources.Add ( source );
			TypeDonneeAvantAgregation = typeDonneeAvantAgregation;
			OperationAgregation = operation;
			GroupBy = bGroupBy;
		}

		//----------------------------------------
		public C2iChampDeRequete ( 
			string strNomChampFinal,
			CSourceDeChampDeRequete[] sources,
			Type typeDonneeAvantAgregation,
			OperationsAgregation operation,
			bool bGroupBy )
		{
			NomChamp = strNomChampFinal;
			m_listeSources = new ArrayList ( sources );
			TypeDonneeAvantAgregation = typeDonneeAvantAgregation;
			OperationAgregation = operation;
			GroupBy = bGroupBy;
		}

		//----------------------------------------
		public override int GetHashCode()
		{
			return (NomChamp + "_" + m_operationAgregation.ToString() + "_" + m_strFonctionSql).GetHashCode();
		}

		//----------------------------------------
		public string NomChamp
		{
			get
			{
				return m_strNomChampFinal;
			}
			set
			{
				string strAutorises = "0987654321_ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
				string strVal = "";
				foreach ( char strCar in value )
					if ( strAutorises.IndexOf ( strCar) >= 0 )
						strVal += strCar;				
				m_strNomChampFinal = strVal;
			}
		}

		//----------------------------------------
		public override bool Equals(object obj)
		{
			if (!(obj is C2iChampDeRequete))
				return false;
			C2iChampDeRequete champ = (C2iChampDeRequete)obj;
			if (champ.NomChamp != NomChamp)
				return false;
			if (champ.FonctionSql != FonctionSql)
				return false;
			if (champ.TypeDonneeAvantAgregation != TypeDonneeAvantAgregation)
				return false;
			if (champ.OperationAgregation != this.OperationAgregation)
				return false;
			if (champ.GroupBy != GroupBy)
				return false;
			if (champ.Sources.Length != Sources.Length)
				return false;
			for ( int nSource =0; nSource < champ.Sources.Length; nSource++ )
				if ( !champ.Sources[nSource].Equals(Sources[nSource] ))
					return false;
			return true;

		}

		//----------------------------------------
		public Type TypeDonneeAvantAgregation

		{
			get
			{
				return m_typeDonneeAvantAgregation;
			}
			set
			{
				m_typeDonneeAvantAgregation = value;
			}
		}

		//----------------------------------------
		/// <summary>
		/// Type de donné final, si null, c'est le type calculé par l'opération d'agrégation
		/// </summary>
		public Type TypeDonneeFinalForce
		{
			get
			{
				return m_typeDonneeFinalForce;
			}
			set
			{
				m_typeDonneeFinalForce = value;
			}
		}

		//----------------------------------------
		public string FonctionSql
		{
			get
			{
				return m_strFonctionSql;
			}
			set
			{
				m_strFonctionSql = value;
			}
		}

		//----------------------------------------
		public Type TypeDonnee
		{
			get
			{
				switch ( OperationAgregation )
				{
					case OperationsAgregation.Average :
					case OperationsAgregation.Sum :
						return typeof ( double );
					case OperationsAgregation.Number :
					case OperationsAgregation.DistinctNumber :
						return typeof ( int ) ;
					default :
						if (TypeDonneeFinalForce != null)
							return TypeDonneeFinalForce;
						return TypeDonneeAvantAgregation;
				}
			}
		}

		
		//----------------------------------------
		public CSourceDeChampDeRequete[] Sources
		{
			get
			{
				return ( CSourceDeChampDeRequete[])m_listeSources.ToArray ( typeof ( CSourceDeChampDeRequete ) );
			}
			set
			{
				m_listeSources = new ArrayList ( value );
			}
		}

		//----------------------------------------
		public OperationsAgregation OperationAgregation
		{
			get
			{
				return m_operationAgregation;
			}
			set
			{
				m_operationAgregation = value;
				if ( m_operationAgregation == OperationsAgregation.None )
					GroupBy = true;
			}
		}

		//----------------------------------------
		public bool GroupBy
		{
			get
			{
				return m_bGroupBy;
			}
			set
			{
				m_bGroupBy = value;
			}
		}

		//------------------------------------------------------------
		public CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		//------------------------------------------------------------
		private int GetNumVersion()
		{
			return 3;
			//V2 : Ajout multi sources
			//3 : Ajout de TypeDonneeFinalForce
		}

		//------------------------------------------------------------
		public virtual CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion	 ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strNomChampFinal );
			
			int nOp = (int)m_operationAgregation;
			serializer.TraiteInt ( ref nOp );
			m_operationAgregation = (OperationsAgregation)nOp;

			if ( nVersion < 2 )
			{
				string strTmp = "";
				serializer.TraiteString ( ref strTmp );
				CSourceDeChampDeRequete source = new CSourceDeChampDeRequete ( strTmp );
				serializer.TraiteString ( ref strTmp );
				source.ChampDeTable = strTmp;
				serializer.TraiteString ( ref strTmp );
				source.Alias = strTmp ;
				m_listeSources = new ArrayList();
				m_listeSources.Add ( source );
			}
			else
			{
				result = serializer.TraiteArrayListOf2iSerializable ( m_listeSources );
				if ( !result )
					return result;
			}
			serializer.TraiteBool  (ref m_bGroupBy );

			serializer.TraiteType ( ref m_typeDonneeAvantAgregation );

			if ( nVersion >= 1 )
				serializer.TraiteString ( ref m_strFonctionSql );

			if (nVersion >= 3)
				serializer.TraiteType(ref m_typeDonneeFinalForce);
			else
				m_typeDonneeFinalForce = null;

			return result;
		}

		/// <summary>
		/// Le data du result contient la chaine à mettre dans SQL
		/// </summary>
		/// <returns></returns>
		public CResultAErreur GetStringSql()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_listeSources.Count < 1 )
			{
				result.EmpileErreur(I.T("No source for the field @1|104",NomChamp));
				return result;
			}
			string strMonoSource = Sources[0].Alias +"."+Sources[0].ChampDeTable;
			if ( m_strFonctionSql.Trim() == "" )
			{
				result.Data = strMonoSource;
				return result;
			}
			/*if ( m_strFonctionSql.IndexOf("@") <= 0 )
			{
				result.Data = m_strFonctionSql+"("+strMonoSource+")";
				return result;
			}*/

			//fonction complexe avec @
			int nIndex = 1;
			string strFonc = m_strFonctionSql;
			foreach ( CSourceDeChampDeRequete source in m_listeSources )
			{
				string strReplace = source.Alias+"."+source.ChampDeTable;
				Regex ex = new Regex ( "(@"+nIndex.ToString()+")(?<SUITE>[^0123456789]{1})" );
				strFonc = ex.Replace ( strFonc+" ", strReplace+"${SUITE}" );
				nIndex++;
			}
			result.Data = strFonc;
			return result;
		}
	}
}