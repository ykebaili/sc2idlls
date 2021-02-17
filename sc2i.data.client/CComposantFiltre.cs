using System;
using System.Collections;

using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CComposantFiltre.
	/// </summary>
	[Serializable]
	public abstract class CComposantFiltre : IExpression, I2iSerializable
	{
		/// ///////////////////////////////////////////////////////////
		public CComposantFiltre()
		{
		}

		/// ///////////////////////////////////////////////////////////
		public string CaracteresControleAvant
		{
			get
			{

				return "";
			}
			set
			{
			}
		}

				
        public virtual void RenumerotteParameters ( int nNumDebut )
        {
            foreach ( CComposantFiltre composant in Parametres )
                composant.RenumerotteParameters ( nNumDebut );
        }


		/// ///////////////////////////////////////////////////////////
		public abstract int GetNbParametresNecessaires();

		/// ///////////////////////////////////////////////////////////
		public abstract string GetString();

		/// ///////////////////////////////////////////////////////////
		/// Utilisé si un composant génère d'autres composants
		public virtual CComposantFiltre GetComposantFiltreFinal ( CFiltreData filtre )
		{
			return this;
		}

		/// ///////////////////////////////////////////////////////////
		public abstract ArrayList Parametres{get;}

		/// ///////////////////////////////////////////////////////////
		public bool CanBeArgumentExpressionObjet
		{
			get 
			{
				return false;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////////
		public virtual CResultAErreur AfterAnalyse ( CAnalyseurSyntaxique analyseur )
		{
			return CResultAErreur.True;
		}


		/// ///////////////////////////////////////////////////////////
		public ArrayList ExtractExpressionsType ( Type tp )
		{
			ArrayList lst = new ArrayList();
			if ( GetType().IsSubclassOf ( tp ) || GetType() == tp )
				lst.Add ( this );
			FillListeWithParametresType ( tp, lst );
			return lst;
		}

		/// ///////////////////////////////////////////////////////////
		private void FillListeWithParametresType ( Type tp, ArrayList lst )
		{
			if ( Parametres == null )
				return;
			foreach ( IExpression exp in Parametres )
			{
				if ( exp.GetType().IsSubclassOf ( tp ) || exp.GetType() == tp )
					lst.Add ( exp );
				if ( exp is CComposantFiltre )
					((CComposantFiltre)exp).FillListeWithParametresType ( tp, lst );
			}
		}

		/// ///////////////////////////////////////////////////////////
		public abstract CResultAErreur VerifieParametres();

		/// ///////////////////////////////////////////////////////////
		public void RemplaceVariableParConstante ( string strNomVariable, object constante )
		{
			if ( Parametres == null )
				return;
			for ( int nParametre = 0; nParametre < Parametres.Count; nParametre++ )
			{
				IExpression expression = (IExpression)Parametres[nParametre];
				if ( expression is CComposantFiltreVariable )
				{
					CComposantFiltreVariable variable = (CComposantFiltreVariable)expression;
					if ( variable.NomVariable == strNomVariable )
					{
						Parametres[nParametre] = new CComposantFiltreConstante ( constante );
					}
				}
				else
					((CComposantFiltre)expression).RemplaceVariableParConstante(strNomVariable, constante);
			}
		}

		/// ///////////////////////////////////////////////////////////
		public virtual void DefinitAlias ( CInfoRelationComposantFiltre[] cheminDeRelations, string strAlias )
		{
			foreach ( CComposantFiltre composant in Parametres )
				composant.DefinitAlias ( cheminDeRelations, strAlias );
		}

		/// ///////////////////////////////////////////////////////////
		public void RenommeVariable ( string strAncienNom, string strNouveauNom )
		{
			if ( Parametres == null )
				return;
			for ( int nParametre = 0; nParametre < Parametres.Count; nParametre++ )
			{
				IExpression expression = (IExpression)Parametres[nParametre];
				if ( expression is CComposantFiltreVariable )
				{
					CComposantFiltreVariable variable = (CComposantFiltreVariable)expression;
					if ( variable.NomVariable == strAncienNom )
					{
						Parametres[nParametre] = new CComposantFiltreVariable(strNouveauNom);
					}
				}
				else
					((CComposantFiltre)expression).RenommeVariable(strAncienNom, strNouveauNom);
			}
			OnRenommeVariable ( strAncienNom, strNouveauNom );
		}

		/// ///////////////////////////////////////////////////////////
		protected virtual void OnRenommeVariable ( string strOldNom, string strNewNom )
		{
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected abstract CResultAErreur MySerialize ( C2iSerializer serializer );	

		/// ///////////////////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = MySerialize ( serializer );
			return result;
		}
	}

	/// ////////////////////////////////////////////////////////////////
	[Serializable]
	public class CComposantFiltreOperateur : CComposantFiltre
	{
		public const string c_IdOperateurOu = "0";
		public const string c_IdOperateurEt = "1";
		public const string c_IdOperateurEgal = "7";
		public const string c_IdOperateurLike = "8";
		public const string c_IdOperateurNotIn = "17";
		public const string c_IdOperateurNotLike = "18";
		public const string c_IdOperateurIsNotNull = "20";
		public const string c_IdOperateurDifferent = "4";
		public const string c_IdOperateurIs = "22";
		public const string c_IdOperateurNull = "23";
		public const string c_IdOperateurParentheses = "1000";
		public const string c_IdOperateurSuperieurOuEgal = "3"; 
		public const string c_IdOperateurIn = "14";
		public const string c_IdOperateurContains="25";
		public const string c_IdOperateurWithout="26";
		public const string c_IdOperateurNot = "24";
		public const string c_IdOperateurInf = "5";
		public const string c_IdOperateurInfEgal = "2";
		public const string c_IdOperateurSup = "6";
        public const string c_IdOperateurModulo = "13";
        public const string c_IdOperateurEtBinaire = "15";
        public const string c_IdOperateurOuBinaire = "16";

		public static COperateurAnalysable[] m_operateurs =
		{
			new COperateurAnalysable(7,"OR",c_IdOperateurOu,false),
			new COperateurAnalysable(6,"AND",c_IdOperateurEt,false),
			
			new COperateurAnalysable(3,"<=",c_IdOperateurInfEgal, false),
			new COperateurAnalysable(3,">=",c_IdOperateurSuperieurOuEgal, false),
			new COperateurAnalysable(3,"<>",c_IdOperateurDifferent, false),
			new COperateurAnalysable(3,"<",c_IdOperateurInf,false),
			new COperateurAnalysable(3,">",c_IdOperateurSup,false),
			new COperateurAnalysable(3,"=",c_IdOperateurEgal,false),
			new COperateurAnalysable(3,"Like",c_IdOperateurLike,false),
			new COperateurAnalysable(2,"+","9", false),
			new COperateurAnalysable(2,"-","10", false),
			new COperateurAnalysable(1,"*","11",false),
			new COperateurAnalysable(1,"/","12", false),
			new COperateurAnalysable(1,"%",c_IdOperateurModulo, false),
			new COperateurAnalysable(3,"IN","14", false),
			new COperateurAnalysable(1,"&",c_IdOperateurEtBinaire,  false),
			new COperateurAnalysable(2,"|",c_IdOperateurOuBinaire, false),
			new COperateurAnalysable(3,"NotIn",c_IdOperateurNotIn, false),
			new COperateurAnalysable(3,"NotLike",c_IdOperateurNotLike, false),
			new COperateurAnalysable(5,"is", c_IdOperateurIs, false),
			new COperateurAnalysable(4,"not", c_IdOperateurNot, false),
			new COperateurAnalysable(3,"Contains",c_IdOperateurContains,false),
			new COperateurAnalysable(3,"Without",c_IdOperateurWithout,false),
			
			//A laisser en dernier
			new COperateurAnalysable(0,"",c_IdOperateurParentheses, false)
		};

		COperateurAnalysable m_operateur = null;
		private ArrayList m_parametres = new ArrayList();

		/// //////////////////////////////////////////////////
		public CComposantFiltreOperateur(  )
		{
		}
		/// //////////////////////////////////////////////////
		public CComposantFiltreOperateur( string strId )
		{
			SetIdOperateur ( strId );
		}

		/// //////////////////////////////////////////////////
		protected void SetIdOperateur ( string strId )
		{
			foreach ( COperateurAnalysable operateur in m_operateurs )
				if ( operateur.Id == strId )
				{
					m_operateur = operateur;
					break;
				}
		}

		/// //////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			if ( Int32.Parse(m_operateur.Id) < 15 )
				return 0;
			if ( m_operateur.Id =="15" || m_operateur.Id == "16" )
				return 1;
			else if (Int32.Parse(m_operateur.Id) == 1000)
				return -1;
			return 0;
		}

		//////////////////////////////////////////////////////////////
		public override string GetString()
		{
			string strChaine;
			if ( m_operateur.Niveau > 0 )
			{
				if ( Parametres.Count == 2 )
					strChaine = "("+((IExpression)Parametres[0]).GetString()+
						" "+m_operateur.Texte+
						" "+((IExpression)Parametres[1]).GetString()+")";
				else
					strChaine = m_operateur.Texte+
						" "+((IExpression)Parametres[0]).GetString();
			}
			else
			{
				strChaine = m_operateur.Texte+"(";
				foreach ( IExpression exp in Parametres )
					strChaine += exp.GetString()+";";
				if ( Parametres.Count > 0 )
					strChaine = strChaine.Substring(0, strChaine.Length-1);
				strChaine +=")";
			}
			return strChaine;
		}

		//////////////////////////////////////////////////////////////
		public override ArrayList Parametres
		{
			get
			{
				return m_parametres;
			}
		}

		//////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			return CResultAErreur.True;
		}
		

		/////////////////////////////////////////////////////
		public COperateurAnalysable Operateur
		{
			get
			{
				return m_operateur;
			}
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			string strId = "";
			if ( m_operateur != null )
				strId = m_operateur.Id;
			serializer.TraiteString ( ref strId );
			if ( serializer.Mode == ModeSerialisation.Lecture )
				SetIdOperateur ( strId );

			result = serializer.TraiteArrayListOf2iSerializable ( m_parametres );
			return result;
		}


	}

	[Serializable]
	public abstract class CInfoRelationComposantFiltre : I2iSerializable
	{
		public abstract bool IsRelationFille{get;}

		public abstract CResultAErreur Serialize ( C2iSerializer serializer );

		public abstract string RelationKey{get;}


		public abstract string TableFille{get;}

		public abstract string TableParente{get;}

		//Retourne le texte de la clause join à mettre dans SQL
		public abstract string GetJoinClause ( string strAliasTableParente, string strSuffixeParent, string strAliasTableFille, string strSuffixeFils )   ;

	}

	/// <summary>
	/// Relation classique de base de données
	/// </summary>
	[Serializable]
	public class CInfoRelationComposantFiltreStd : CInfoRelationComposantFiltre
	{
		private CInfoRelation m_infoRelation = null;
		private bool m_bIsRelationFille = false;

		//Permet de distinguer deux relations différentes à la même table
		private int m_nIdGroupeRelation = -1;
		
		public CInfoRelationComposantFiltreStd ( CInfoRelation info,
			bool bIsRelationFille,
			int nIdGroupeRelation )
		{
			m_infoRelation = (CInfoRelation)CCloner2iSerializable.Clone ( info );
			m_bIsRelationFille = bIsRelationFille;
			m_nIdGroupeRelation = nIdGroupeRelation;
		}

        /// //////////////////////////////////////////////////////
        protected int IdGroupeRelation
        {
            get
            {
                return m_nIdGroupeRelation;
            }
        }


		/// //////////////////////////////////////////////////////
		public CInfoRelationComposantFiltreStd()
			:base()
		{
		}

		


		/// //////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
			//2 : ajout de IdGroupeRelation
		}

		/// //////////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			if ( serializer.Mode == ModeSerialisation.Lecture )
				m_infoRelation = new CInfoRelation();
			result = m_infoRelation.Serialize ( serializer );
			if ( !result )
				return result;
			if ( nVersion < 1 )//Variable mise dans le CArbreTable
			{
				bool bTmp=false;
				serializer.TraiteBool ( ref bTmp );
			}
			serializer.TraiteBool( ref m_bIsRelationFille );
			if ( nVersion >= 2 )
				serializer.TraiteInt ( ref m_nIdGroupeRelation );
			else
				m_nIdGroupeRelation = -1;
			return result;
		}

		/// //////////////////////////////////////////////////////
		public override bool IsRelationFille
		{
			get
			{
				return m_bIsRelationFille;
			}
		}

		/// //////////////////////////////////////////////////////
		public CInfoRelation InfoRelation
		{
			get
			{
				return m_infoRelation;
			}
		}

		/// //////////////////////////////////////////////////////
		public override string RelationKey
		{
			get
			{
				return m_infoRelation.RelationKey+
					(m_nIdGroupeRelation>=0?m_nIdGroupeRelation.ToString():"");
			}
		}


		/// //////////////////////////////////////////////////////
		public override string TableFille
		{
			get
			{
				return m_infoRelation.TableFille;
			}
		}

		/// //////////////////////////////////////////////////////
		public override string TableParente
		{
			get
			{
				return m_infoRelation.TableParente;
			}
		}
		
		/// //////////////////////////////////////////////////////
		public override string GetJoinClause ( 
			string strAliasTableParente, 
			string strSuffixeParent,
			string strAliasTableFille,
			string strSuffixeFils)
		{
			string strRetour = "";
			for ( int nChamp = 0; nChamp < m_infoRelation.ChampsFille.Length; nChamp++ )
			{
				strRetour += strAliasTableParente+"."+m_infoRelation.ChampsParent[nChamp]+strSuffixeParent+"="+
					strAliasTableFille+"."+m_infoRelation.ChampsFille[nChamp]+strSuffixeFils+" and ";
			}
			//Supprime le And de la fin
			strRetour = strRetour.Substring(0, strRetour.Length-5);
			return strRetour;
		}
	}

    /// //////////////////////////////////////////////////////
    //Idem à une relation stda, mais contient l'id de champ custom dans l'idDegroupe
    [Serializable]
    public class CInfoRelationComposantFiltreChampCustom : CInfoRelationComposantFiltreStd
    {

        public CInfoRelationComposantFiltreChampCustom(CInfoRelation info,
            bool bIsRelationFille,
            int nIdChampCustom)
            : base(info, bIsRelationFille, nIdChampCustom)
        {
        }

        public CInfoRelationComposantFiltreChampCustom()
            : base()
        {
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
            if ( !result )
                return result;
            result = base.Serialize(serializer);
            return result;
        }

        public override string GetJoinClause(string strAliasTableParente, string strSuffixeParent, string strAliasTableFille, string strSuffixeFils)
        {
            string strRetour = base.GetJoinClause(strAliasTableParente, strSuffixeParent, strAliasTableFille, strSuffixeFils);
            if (IdGroupeRelation >= 0)
                strRetour += " and "+strAliasTableFille+".CUSTFLD_ID"+strSuffixeFils+"=" + IdGroupeRelation;
            return strRetour;
        }
    }

	
	/// ////////////////////////////////////////////////////////////////
	/// <summary>
	/// Relation TypeId
	/// </summary>
    [Serializable]
	public class CInfoRelationComposantFiltreTypeId : CInfoRelationComposantFiltre
	{
		private RelationTypeIdAttribute m_attribut;
		private string m_strTableParente = "";

		public CInfoRelationComposantFiltreTypeId (  )
		{
		}

		public CInfoRelationComposantFiltreTypeId ( string strTableParente, RelationTypeIdAttribute attrib )
		{
			m_attribut = attrib;
			m_strTableParente = strTableParente;
		}

		/// ////////////////////////////////////////////////////////////////
		public override bool IsRelationFille
		{
			get
			{
				return true;
			}
		}

		/// ////////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;

			serializer.TraiteString ( ref m_strTableParente );

			I2iSerializable objet = m_attribut;
			result = serializer.TraiteObject ( ref objet );
			m_attribut = (RelationTypeIdAttribute)objet;

			return result;
		}


		/// ////////////////////////////////////////////////////////////////
		public override string RelationKey
		{
			get
			{
				return "TYPEID_"+m_attribut.IdRelation;
			}
		}


		/// ////////////////////////////////////////////////////////////////
		public override string TableFille
		{
			get
			{
				return m_attribut.TableFille;
			}
		}

		/// ////////////////////////////////////////////////////////////////
		public override string TableParente
		{
			get
			{
				return m_strTableParente;
			}
		}


		/// ////////////////////////////////////////////////////////////////
		//Retourne le texte de la clause join à mettre dans SQL
		public override string GetJoinClause ( 
			string strAliasTableParente, 
			string strSuffixeParent,
			string strAliasTableFille,
			string strSuffixeFils) 
		{
			//Identifie le type parent
			Type tp = CContexteDonnee.GetTypeForTable ( m_strTableParente );
			if ( !tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique) ) )
				throw new Exception (I.T("A TypeId Link on a table is not managed by a numerical identifier|112"));
			CStructureTable structure = CStructureTable.GetStructure(tp);
			string strJoin = strAliasTableParente+"."+structure.ChampsId[0].NomChamp+strSuffixeParent+"="+
				strAliasTableFille+"."+m_attribut.ChampId+strSuffixeFils+" and "+
                strAliasTableFille + "." + m_attribut.ChampType + strSuffixeFils + "='" +
				tp.ToString()+"'";
			return strJoin;
		}
	}


    public delegate void FindRelationDelegate(string strTable, Type tp, ref CInfoRelationComposantFiltre relationTrouvee);
	/// ////////////////////////////////////////////////////////////////
	[Serializable]
	public class CComposantFiltreChamp: CComposantFiltre
	{
		private CInfoRelationComposantFiltre[] m_relations;
		private string m_strNomChamp;
		private string m_strChaineInitiale;
		private string m_strTableDeBase;
		private string m_strAlias = "";
		
		//Id du champ custom si ce composant est lié à un traitement de champ custom
		private int m_nIdChampCustom = -1;
		
		/// ///////////////////////////////////////////////////////////
		public CComposantFiltreChamp ()
		{

		}

		/// ///////////////////////////////////////////////////////////
		public CComposantFiltreChamp ( string strChamp, string strTableDeBase )
		{
			//Si le nom de champ commence par #~, c'est qu'il contient un id de champ
			//custom
			//#~5~#
			if ( strChamp.Length > 4 && strChamp.Substring(0, 2) == "#~" )
			{
				//Cherche le ~# fermant
				int nPos = strChamp.IndexOf("~#");
				if ( nPos > 1 )
				{
					string strIdChamp = strChamp.Substring ( 2, nPos-2 );
					try
					{
						m_nIdChampCustom = Int32.Parse ( strIdChamp );
					}
					catch
					{
						m_nIdChampCustom = -1;
					}
				}
				strChamp = strChamp.Substring(nPos+2);
			}
			m_strChaineInitiale = strChamp;
			m_strTableDeBase = strTableDeBase;
			CalculeRelations();
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
			//2 : ajout de IdChampCustom
		}

		/// ///////////////////////////////////////////////////////////
		public int IdChampCustom
		{
			get
			{
				return m_nIdChampCustom;
			}
			set
			{
				m_nIdChampCustom = value;
                CalculeRelations();
			}
		}

		/// ///////////////////////////////////////////////////////////
		public override void DefinitAlias ( CInfoRelationComposantFiltre[] cheminDeRelations, string strAlias )
		{
			if ( cheminDeRelations.Length != Relations.Length )
				return;
			for ( int nRelation = 0; nRelation < m_relations.Length; nRelation++ )
			{
				if ( m_relations[nRelation].RelationKey != cheminDeRelations[nRelation].RelationKey )
					return;
			}
			m_strAlias = strAlias;
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strNomChamp );
			serializer.TraiteString ( ref m_strChaineInitiale );
			serializer.TraiteString ( ref m_strTableDeBase );
			int nNb = 0;
			if ( serializer.Mode == ModeSerialisation.Ecriture )
				nNb = m_relations.Length;
			serializer.TraiteInt ( ref nNb );
			switch(  serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach ( CInfoRelationComposantFiltre info in m_relations )
					{
						if ( nVersion < 1 )
							result = info.Serialize(serializer);
						else
						{
							I2iSerializable objet = info;
							result = serializer.TraiteObject ( ref objet );
						}
						if ( !result )
							return result;
					}
					break;
				case ModeSerialisation.Lecture :
					m_relations = new CInfoRelationComposantFiltre[nNb];
					for ( int n = 0; n< nNb; n++ )
					{
						CInfoRelationComposantFiltre info;
						if ( nVersion < 1 )
						{
							info = new CInfoRelationComposantFiltreStd();
							result = info.Serialize ( serializer );
						}
						else
						{
							I2iSerializable objet = null;
							result = serializer.TraiteObject ( ref objet );
							info = (CInfoRelationComposantFiltre)objet;
						}
						m_relations[n] = info;
						if ( !result )
							return result;
					}
					break;
			}
			if ( nVersion > 0 )
				serializer.TraiteString ( ref m_strAlias );
			if ( nVersion >= 2 )
                //NOTE CDbKey : ne pas changer en CDbKey car en principe,
                //les filtres ne sont pas serializés
				serializer.TraiteInt ( ref m_nIdChampCustom );
			else
				m_nIdChampCustom = -1;

			return result;
		}

		/// ///////////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected void CalculeRelations()
		{
			ArrayList lst = new ArrayList();
			CalculeRelations ( m_strChaineInitiale, lst, null );
			m_relations = (CInfoRelationComposantFiltre[])lst.ToArray(typeof(CInfoRelationComposantFiltre));
		}

		/// ///////////////////////////////////////////////////////////
		public static FindRelationDelegate FindRelationComplementaire; 

		/// ///////////////////////////////////////////////////////////
        protected void CalculeRelations(string strChaine, ArrayList listRelations, Type lastType)
        {
            if (m_strTableDeBase.Trim() == "")
                return;
            string[] strChemins = strChaine.Split('.');
            if (strChemins.Length == 0)
                throw new Exception(I.T("Incorrect field name ''|113"));
            if (lastType == null)
                lastType = CContexteDonnee.GetTypeForTable(m_strTableDeBase);
            if (lastType == null)
                throw new Exception(I.T("Table '@1' is not associated with a data type|114", m_strTableDeBase));
            string strTableParente = CContexteDonnee.GetNomTableForType(lastType);
            CStructureTable structureLast = CStructureTable.GetStructure(lastType);
            string strTable = strChemins[0];
            string strNomTableNue = strTable;
            if (strTable.Length > 0)
            {
                if (strTable[0] == '#')//ça vient d'un CDefinitionProprietedynamique
                {
                    int nPos = strTable.IndexOf('|');
                    if (nPos > 0)
                        strNomTableNue = strTable.Substring(nPos + 1);
                }
            }


            //le nouveau type doit être une relation parente de l'ancien
            ArrayList lstRelations = new ArrayList();
            CInfoRelationComposantFiltre relationTrouvee = null;

            foreach (CInfoRelation info in structureLast.RelationsParentes)
            {
                if (info.TableParente == strNomTableNue || info.NomConvivial == strNomTableNue || info.Propriete == strNomTableNue || info.RelationKey == strNomTableNue)
                {
                    //Ok, on ajoute la relation et on continue
                    relationTrouvee = GetRelation(info, false);
                    //new CInfoRelationComposantFiltreStd(info, false, m_nIdChampCustom);
                    break;
                }
            }
            if (relationTrouvee == null)
            {
                foreach (CInfoRelation info in structureLast.RelationsFilles)
                {
                    if (info.TableFille == strNomTableNue || info.NomConvivial == strNomTableNue || info.Propriete == strNomTableNue || info.RelationKey == strNomTableNue)
                    {
                        //Ok, on ajoute la relation et on continue
                        relationTrouvee = GetRelation(info, true);
                        //new CInfoRelationComposantFiltreStd(info,true, m_nIdChampCustom);
                        break;
                    }
                }
            }


            if (relationTrouvee == null)
            {
                //Si on est là c'est que la relation n'a pas été trouvée
                //Serais-ce une relation fille qui n'a pas de propriété équivalente dans la classe parente ??
                foreach (CInfoRelation info in CContexteDonnee.GetListeRelationsTable(strNomTableNue))
                {
                    if (info.TableFille == strNomTableNue && info.TableParente == strTableParente || info.RelationKey == strNomTableNue)
                    {
                        //Ok, on ajoute la relation et on continue
                        relationTrouvee = GetRelation(info, true);
                        //new CInfoRelationComposantFiltreStd(info,true, m_nIdChampCustom);
                        break;
                    }
                }
            }


            if (relationTrouvee == null)
            {
                //Peut être le champ est il un nom d'attribut de relationTypeId
                foreach (RelationTypeIdAttribute relation in structureLast.RelationsTypeId)
                {
                    if (relation.IdRelation == strNomTableNue)
                    {
                        //Yes, c'est ça !!!
                        relationTrouvee = new CInfoRelationComposantFiltreTypeId(strTableParente, relation);
                        break;
                    }
                }
            }


            if (relationTrouvee == null)
            {
                //autre espoir : c'est un nom de relation de la table parente
                //Si on est là c'est que la relation n'a pas été trouvée
                //Le nom serait-il le nom d'une relation ???
                //
                foreach (CInfoRelation info in CContexteDonnee.GetListeRelationsTable(strTableParente))
                {
                    if (info.RelationKey == strNomTableNue)
                    {
                        //Ok, on ajoute la relation et on continue
                        relationTrouvee = GetRelation(info, true);
                        //relationTrouvee = new CInfoRelationComposantFiltreStd(info,true, m_nIdChampCustom);
                        break;
                    }
                }
            }

            if (relationTrouvee == null && FindRelationComplementaire != null)
            {
                FindRelationComplementaire(strTable, lastType, ref relationTrouvee);
            }


            if (relationTrouvee != null)
            {
                listRelations.Add(relationTrouvee);
                if (strChemins.Length == 1)
                    return;
                string strSuite = strChaine.Substring(strTable.Length + 1);
                Type tp = CContexteDonnee.GetTypeForTable(relationTrouvee.IsRelationFille ? relationTrouvee.TableFille : relationTrouvee.TableParente);
                if (tp == null)
                    throw new Exception(I.T("Table '@1' is not associated with a data type|114", strTable));
                CalculeRelations(strSuite, listRelations, tp);
                return;
            }

            if (strChemins.Length > 1)//Pas de relation trouvée !!!
            {
                throw new Exception(I.T("Cannot establish a Parent/Child link for tables @1 / @2|115", structureLast.NomTable, strTable));
            }
            //On est sur le champ. Est-ce bien un champ de la table demandée ???
            foreach (CInfoChampTable info in structureLast.Champs)
            {
                if (info.NomChamp == strNomTableNue ||
                    info.NomConvivial == strNomTableNue ||
                    info.Propriete == strNomTableNue)
                {
                    m_strNomChamp = info.NomChamp;
                    return;
                }
            }

            //Pas trouvé, peut être un champ qui a changé de nom
            if (lastType != null)
            {
                System.Reflection.PropertyInfo prop = ReplaceFieldAttribute.FindFieldObsolete(lastType, strNomTableNue);
                if (prop != null)
                {
                    object[] atts = prop.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
                    if (atts.Length > 0)
                    {
                        m_strNomChamp = ((TableFieldPropertyAttribute)atts[0]).NomChamp;
                        return;
                    }
                }
            }

            if (strNomTableNue.StartsWith("#SQL#"))
            {
                m_strNomChamp = strNomTableNue.Substring("#SQL#".Length);
                return;
            }

            //Si on est là c'est que le champ n'existe pas
            throw new Exception(I.T("The field '@1' does not exist|116", strChemins[0]));
        }

        /// ///////////////////////////////////////////////////////////
        private CInfoRelationComposantFiltreStd GetRelation(CInfoRelation info, bool bIsFille)
        {
            if (m_nIdChampCustom >= 0 && info.Propriete == "RelationsChampsCustom")
                return new CInfoRelationComposantFiltreChampCustom(info, bIsFille, m_nIdChampCustom);
            else
                return new CInfoRelationComposantFiltreStd(info, bIsFille, m_nIdChampCustom);
        }

		/// ///////////////////////////////////////////////////////////
		public string Alias
		{
			get
			{
				return m_strAlias;
			}
		}


		/// ///////////////////////////////////////////////////////////
		public override string GetString()
		{
			string strRetour = "[";
			if ( m_nIdChampCustom >= 0 )
				strRetour += "#~"+m_nIdChampCustom.ToString()+"~#";
			strRetour += m_strChaineInitiale+"]";
			return strRetour;
		}
			

		/// ///////////////////////////////////////////////////////////
		public override ArrayList Parametres
		{
			get
			{
				return new ArrayList();
			}
		}

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			return CResultAErreur.True;
		}

		/// ///////////////////////////////////////////////////////////
		public CInfoRelationComposantFiltre[] Relations
		{
			get
			{
				return m_relations;
			}
		}

		/// ///////////////////////////////////////////////////////////
		public void EmpileRelation(string strNouvelleTableDeBase, string strRelation)
		{
			m_strTableDeBase = strNouvelleTableDeBase;
			m_strChaineInitiale = strRelation + "." + m_strChaineInitiale;
			CalculeRelations();
		}

        /// ///////////////////////////////////////////////////////////
        public string ChaineInitiale
        {
            get
            {
                return m_strChaineInitiale;
            }
        }

		/// ///////////////////////////////////////////////////////////
		public string NomChamp
		{
			get
			{
				return m_strNomChamp;
			}
		}

		/// ///////////////////////////////////////////////////////////
		public string TableDeBase
		{
			get
			{
				return m_strTableDeBase;
			}
		}
	}

	/// ////////////////////////////////////////////////////////////////
	[Serializable]
	public class CComposantFiltreConstante : CComposantFiltre
	{
		private object m_valeur;

		public CComposantFiltreConstante(  )
		{
		}

		public CComposantFiltreConstante( object valeur )
		{
			m_valeur = valeur;
		}

		/// ///////////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		public override string GetString()
		{
			if ( m_valeur == null )
				return "null";


			if ( m_valeur is string )
			{
				if ( m_valeur.ToString().IndexOf('"') >= 0 )
					return "'"+m_valeur+"'";
				return "\""+m_valeur.ToString()+"\"";
			}
			else if ( m_valeur is DateTime )
			{
				DateTime dt = (DateTime)m_valeur;
				string strChaine = "#";
				strChaine +=dt.Year+"/"+dt.Month+"/"+dt.Day+"#";
				return strChaine;
			}
			//else if (m_valeur is double) 
			//{
			//    return ((double)m_valeur).ToString("e");
			//}
			//else if (m_valeur is decimal)
			//{
			//    return ((decimal)m_valeur).ToString("e");
			//}
			//else if (m_valeur is float)
			//{
			//    return ((float)m_valeur).ToString("e");
			//}
			return m_valeur.ToString();
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
			//Version 1 : les inforelation peuvent avoir des types différents
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			if ( !result )
				return result;
			result = serializer.TraiteObjetSimple ( ref m_valeur );
			return result;
		}			

		/// ///////////////////////////////////////////////////////////
		public override ArrayList Parametres
		{
			get
			{
				return new ArrayList();
			}
		}

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			return CResultAErreur.True;
		}

		/// ///////////////////////////////////////////////////////////
		public object Valeur
		{
			get
			{
				return m_valeur;
			}
		}
	}

	/// ////////////////////////////////////////////////////////////////
	[Serializable]
	public class CComposantFiltreVariable : CComposantFiltre
	{
		private string m_strVariable;


		public CComposantFiltreVariable( )
		{
		}

		public CComposantFiltreVariable( string strVariable )
		{
			m_strVariable = strVariable;
		}

        /// ///////////////////////////////////////////////////////////
        public override void RenumerotteParameters(int nNumDebut)
        {
            base.RenumerotteParameters(nNumDebut);
            if (m_strVariable.Length > 1)
            {
                string strVal = m_strVariable.Substring(1);
                try
                {
                    int nVal = Int32.Parse(strVal);
                    nVal += nNumDebut-1;
                    m_strVariable = m_strVariable[0] + nVal.ToString().Trim();
                }
                catch
                {
                }
            }
        }

		/// ///////////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		public override string GetString()
		{
			return m_strVariable;
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if (!result)
				return result;
			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strVariable );
			return result;
		}

		/// ///////////////////////////////////////////////////////////
		public string NomVariable
		{
			get
			{
				return m_strVariable;
			}
		}
			

		/// ///////////////////////////////////////////////////////////
		public override ArrayList Parametres
		{
			get
			{
				return new ArrayList();
			}
		}

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			return CResultAErreur.True;
		}
	}


	/// ///////////////////////////////////////////////////////////
	[Serializable]
	public abstract class CComposantFiltreFonction : CComposantFiltre
	{
		private ArrayList m_listeParametres = new ArrayList();//Un seul paramètre : le champ

		public abstract COperateurAnalysable GetOperateur();

		/// ///////////////////////////////////////////////////////////
		public override ArrayList Parametres
		{
			get
			{
				return m_listeParametres;
			}
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = serializer.TraiteArrayListOf2iSerializable ( m_listeParametres );
			return result;
		}
	}

	/// ///////////////////////////////////////////////////////////
	[Serializable]
	public class CComposantFiltreHasNo : CComposantFiltreFonction
	{
		/// //////////////////////////////////////
		public CComposantFiltreHasNo()
			:base()
		{
		}

		/// ///////////////////////////////////////////////////////////
		public override COperateurAnalysable GetOperateur()
		{
			return new COperateurAnalysable ( 0, "HasNo","HASNO",false);
		}


		/// ///////////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 1;
		}

		/// ///////////////////////////////////////////////////////////
		public override string GetString()
		{
			return " HasNo ("+((CComposantFiltre)Parametres[0]).GetString()+") ";
		}

		

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( Parametres.Count == 0 || Parametres[0] == null || !(Parametres[0] is CComposantFiltreChamp))
			{
				result.EmpileErreur(I.T("HasNo expects a field as a parameter|117"));
			}
			else
				result = ((CComposantFiltre)Parametres[0]).VerifieParametres();
			return result;
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize(serializer );
			return result;
		}
	}

	/// ///////////////////////////////////////////////////////////
	[Serializable]
	public class CComposantFiltreHas : CComposantFiltreFonction
	{
		/// //////////////////////////////////////
		public CComposantFiltreHas()
			:base()
		{
		}

		/// ///////////////////////////////////////////////////////////
		public override COperateurAnalysable GetOperateur()
		{
			return new COperateurAnalysable ( 0, "Has","HAS",false);
		}


		/// ///////////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 1;
		}

		/// ///////////////////////////////////////////////////////////
		public override string GetString()
		{
			return " Has ("+((CComposantFiltre)Parametres[0]).GetString()+") ";
		}

		

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( Parametres.Count == 0 || Parametres[0] == null || !(Parametres[0] is CComposantFiltreChamp))
			{
				result.EmpileErreur(I.T("Has expects a field as a parameter|118"));
			}
			else
				result = ((CComposantFiltre)Parametres[0]).VerifieParametres();
			return result;
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize(serializer );
			return result;
		}
	}

	/// ///////////////////////////////////////////////////////////
	[Serializable]
	public class CComposantFiltreListe : CComposantFiltre
	{
		private ArrayList m_listeValeurs = new ArrayList();

		/// ///////////////////////////////////////////////////////////
		public CComposantFiltreListe(  )
		{
		}

		/// ///////////////////////////////////////////////////////////
		public CComposantFiltreListe( IExpression[] listes )
		{
			foreach ( IExpression expression in listes )
			{
				if ( expression != null )
					m_listeValeurs.Add ( expression );
			}
		}

		/// ///////////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		public override string GetString()
		{
			string strValeur = "";
			foreach ( IExpression exp in m_listeValeurs )
				strValeur += exp.GetString()+";";
			if ( strValeur != "" )
				strValeur = strValeur.Substring(0, strValeur.Length-1);
			strValeur = "{"+strValeur+"}";
			return strValeur;
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if (!result)
				return result;
			if ( !result )
				return result;
			result = serializer.TraiteArrayListOf2iSerializable( m_listeValeurs );
			return result;
		}

		/// ///////////////////////////////////////////////////////////
		public override ArrayList Parametres
		{
			get
			{
				return new ArrayList();
			}
		}

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			return CResultAErreur.True;
		}

		/// ///////////////////////////////////////////////////////////
		public ArrayList Liste
		{
			get
			{
				return m_listeValeurs;
			}
		}
	
	}


	
}
