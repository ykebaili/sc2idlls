using System;
using System.Collections;
using System.Data;

using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Informations sur une relation entre des tables standard,
	/// C'est une relation qui se fait entre des champs de table
	/// relation classique SQL
	/// </summary>
	[Serializable]
	public class CInfoRelation : CInfoRelationBase
	{
		private string m_strTableParente;
		private string m_strTableFille;
		private string[] m_strChampsParent;
		private string[] m_strChampsFille;
		private bool m_bObligatoire;
		private bool m_bComposition;
		private string m_strPropriete="";
		private string m_strNomConvivial="";
		private bool m_bIndexed = false;
		private bool m_bPasserLesFilsANullLorsDeLaSuppression = false;
        private bool m_bDeleteCascadeManuel = false;
        private bool m_bIsInDb = true;
        private bool m_bIsClustered = false;
        private bool m_bNePasClonerLesFils = false;

		public CInfoRelation(
			string strTableParente, 
			string strTableFille, 
			string[] strChampsParent, 
			string[] strChampsFille,
			bool bObligatoire,
			bool bComposition)
		{
			m_strTableParente = strTableParente;
			m_strTableFille = strTableFille;
			m_strChampsParent = strChampsParent;
			m_strChampsFille = strChampsFille;
			m_bObligatoire = bObligatoire;
			m_bComposition = bComposition;
		}

        

		public CInfoRelation(
			string strTableParente, 
			string strTableFille, 
			string[] strChampsParent, 
			string[] strChampsFille,
			bool bObligatoire,
			bool bComposition,
			bool bIndexed,
			bool bPasserLesFilsANullLorsDeLaSuppression,
            bool bDeleteCascadeManuel)
		{
			m_strTableParente = strTableParente;
			m_strTableFille = strTableFille;
			m_strChampsParent = strChampsParent;
			m_strChampsFille = strChampsFille;
			m_bObligatoire = bObligatoire;
			m_bComposition = bComposition;
			m_bIndexed = bIndexed;
			m_bPasserLesFilsANullLorsDeLaSuppression = bPasserLesFilsANullLorsDeLaSuppression;
            m_bDeleteCascadeManuel = bDeleteCascadeManuel;
		}

		/// /////////////////////////////////////////////////
		public CInfoRelation()
		{
			m_strTableParente = "";
			m_strTableFille = "";
			m_strChampsParent = new string[0];
			m_strChampsFille = new string[0];
			m_bObligatoire = false;
			m_bComposition = false;
		}

        /// /////////////////////////////////////////////////
        public bool IsInDb
        {
            get
            {
                return m_bIsInDb;
            }
            set
            {
                m_bIsInDb = value;

            }
        }

        /// /////////////////////////////////////////////////
        public bool IsClustered
        {
            get
            {
                return m_bIsClustered;
            }
            set
            {
                m_bIsClustered = value;
            }
        }

        /// /////////////////////////////////////////////////
        public bool NePasClonerLesFils
        {
            get
            {
                return m_bNePasClonerLesFils;
            }
            set
            {
                m_bNePasClonerLesFils = value;
            }
        }


		/// /////////////////////////////////////////////////
		public override string TableParente
		{
			get
			{
				return m_strTableParente;
			}
		}

		/// /////////////////////////////////////////////////
		public override string TableFille
		{
			get
			{
				return m_strTableFille;
			}
		}

		/// /////////////////////////////////////////////////
		public string[] ChampsParent
		{
			get
			{
				return m_strChampsParent;
			}
		}

		/// /////////////////////////////////////////////////
		public string[] ChampsFille
		{
			get
			{
				return m_strChampsFille;
			}
		}

		/// /////////////////////////////////////////////////
		public bool Obligatoire
		{
			get
			{
				return m_bObligatoire;
			}
			set
			{
				m_bObligatoire = value;
			}
		}

		/// /////////////////////////////////////////////////
		public bool Composition
		{
			get
			{
				return m_bComposition;
			}
		}

		/// <summary>
		/// /////////////////////////////////////////////////
		/// </summary>
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

		/// <summary>
		/// /////////////////////////////////////////////////
		/// </summary>
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

		/// /////////////////////////////////////////////////
		public bool Indexed
		{
			get
			{
				return m_bIndexed;
			}
			set
			{
				m_bIndexed = value;
			}
		}

		/// /////////////////////////////////////////////////
		public bool PasserLesFilsANullLorsDeLaSuppression
		{
			get
			{
				return m_bPasserLesFilsANullLorsDeLaSuppression;
			}
			set
			{
				m_bPasserLesFilsANullLorsDeLaSuppression = value;
			}
		}

        /// /////////////////////////////////////////////////
        public bool DeleteCascadeManuel
        {
            get
            {
                return m_bDeleteCascadeManuel;
            }
            set
            {
                m_bDeleteCascadeManuel = value;
            }
        }





		public override int GetHashCode()
		{
			return RelationKey.GetHashCode();
		}

		/// <summary>
		/// Retourne true si les colonnes parentes correspondent à la liste des colonnes passées
		/// </summary>
		/// <param name="colonnes"></param>
		/// <returns></returns>
		public bool MatchParentsCols ( DataColumn[] colonnes )
		{
			if ( colonnes.Length != ChampsParent.Length )
				return false;
			foreach ( DataColumn col in colonnes )
			{
				bool bTrouve = false;
				foreach ( string strChamp in ChampsParent )
					if ( strChamp == col.ColumnName )
					{
						bTrouve = true;
						break;
					}
				if ( !bTrouve )
					return false;
			}
			return true;
		}



		/// /////////////////////////////////////////////////
		public override string RelationKey
		{
			get
			{
				string strRet = "FK_";
				strRet += TableFille+"_"+TableParente;
				foreach ( string strChamp in ChampsFille )
					strRet += "_"+strChamp;
				return strRet;
			}
		}

		/// /////////////////////////////////////////////////
		public override bool Equals ( object obj )
		{
			if ( !(obj is CInfoRelation ) )
				return false;
			return ((CInfoRelation)obj).RelationKey.Equals ( RelationKey );
		}
											 

		/// /////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
		}


		/// /////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			serializer.TraiteString	( ref m_strTableParente );
			serializer.TraiteString ( ref m_strTableFille );
			serializer.TraiteBool ( ref m_bObligatoire );
			serializer.TraiteBool ( ref m_bComposition );
			serializer.TraiteString ( ref m_strPropriete );
			serializer.TraiteString ( ref m_strNomConvivial );
			
			int nNb = 0;
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					nNb = ChampsParent.Length;
					serializer.TraiteInt ( ref nNb );
					for ( int n = 0; n< nNb; n++ )
					{
						string str = ChampsParent[n];
						serializer.TraiteString ( ref str );
					}
					nNb = ChampsFille.Length;
					serializer.TraiteInt ( ref nNb );
					for ( int n = 0; n <nNb; n++ )
					{
						string str = ChampsFille[n];
						serializer.TraiteString ( ref str );
					}
					break;
				case ModeSerialisation.Lecture :
					serializer.TraiteInt ( ref nNb );
					m_strChampsParent = new string[nNb];
					for ( int n = 0; n < nNb; n++ )
					{
						string str = "";
						serializer.TraiteString ( ref str );
						ChampsParent[n] = str;
					}
					serializer.TraiteInt ( ref nNb );
					m_strChampsFille = new string[nNb];
					for ( int n = 0; n < nNb; n++ )
					{
						string str = "";
						serializer.TraiteString ( ref str );
						ChampsFille[n] = str;
					}
					break;
			}
			if ( nVersion > 0 )
				serializer.TraiteBool ( ref m_bIndexed );
			return result;
		}
						

	}
}
