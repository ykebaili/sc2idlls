using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.recherche;
using sc2i.common.unites;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Représente un champ calculé.
	/// </summary>
    /// <remarks>
    /// <p>Chaque champ calculé est prévu pour un type d'entité spécifique et ne pourra être appliquée qu'à lui seul.</p>
    /// Il est possible de créer des champs calculés sur tous les types d'entité de l'application. Ces champs seront disponibles
    /// en lecture seule dans les formules et dans tout élément de paramétrage permettant l'accès aux champs de l'application.
    /// <p>Il n'est pas possible de filtrer les données sur des valeurs de champs calculés</p>
    /// <p>Les champs calculés sont générallement utilisés pour éviter de répéter des formules dont on a souvent besoin sur des entités.
    /// Ils permettent également à l'utilisateur de voir une données calculé dans les listes de l'application.</p>
    /// <p><B>Attention</B>L'utilisation de champs calculés peut ralentir considérablement l'affichage des liste dans l'application.
    /// S'ils sont mals utilisés, les performances globales de l'application être affectée.</p>
    /// </remarks>
	[Table(CChampCalcule.c_nomTable, CChampCalcule.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CChampCalculeServeur")]
	[DynamicClass("Calculated field")]
	public class CChampCalcule : 
        CObjetDonneeAIdNumeriqueAuto, 
        IObjetALectureTableComplete, 
        IVariableDynamique,
        IObjetCherchable
	{
		public const string c_nomTable = "CALCULATED_FIELDS";
		public const string c_champId = "CALCFLD_ID";
		public const string c_champNom = "CALCFLD_NAME";
		public const string c_champDescription = "CALCFLD_DESC";
		public const string c_champFormule = "CALCFLD_FORMULA";
		public const string c_champTypeObjets = "CALCFLD_OBJET_TYPE";
        public const string c_champRubrique = "CALCFLD_FOLDER";

		public const string c_champCacheFormule = "CALCFLD_FORMULA_CACHE";

#if PDA
		/// ///////////////////////////////////////////////////////
		public CChampCalcule(  )
			:base()
		{
		}
#endif
		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CChampCalcule( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CChampCalcule ( DataRow row )
			:base(row)
		{
		}

		/// //////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

        // Implemente le membre IdVariable de l'interface IVariableDynamique
        public string IdVariable
        {
            get
            {
                return IdUniversel;
            }
            set { }
        }

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Calculated field @1|133",Nom);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champNom};
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Nom du champ calculé. Ce nom doit être unique.
        ///</summary>
        [TableFieldPropertyAttribute(c_champNom, 64)]
		[DynamicField("Name")]
		public string Nom
		{
			get
			{
				return (string)Row[c_champNom];
			}
			set
			{
				Row[c_champNom] = value;
			}
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Description du champ calculé
        /// </summary>
        /// <remarks>
        /// Il est recommandé à l'administrateur d'utiliser la description pour décrire le fonctionnement du champ
        /// calculé ainsi que d'indiquer dans quel contexte il est utilisé.
        /// <p>Une bonne documentation des champs calculés peut simplifier le travail de maintenance du paramétrage.</p>
        /// </remarks>
        [TableFieldProperty(c_champDescription, 256)]
        [DynamicField("Field description")]
		public string Description
		{
			get
			{
				return (string)Row[c_champDescription];
			}
			set
			{
				Row[c_champDescription] = value;
			}
		}

        /// //////////////////////////////////////////////////
        public IClasseUnite ClasseUnite
        {
            get
            {
                return null;
            }
        }

        /// //////////////////////////////////////////////////
        public string FormatAffichageUnite
        {
            get
            {
                return "";
            }
        }

		/// //////////////////////////////////////////////////
		[TableFieldProperty(c_champFormule, 3000)]
		public string FormuleString
		{
			get
			{
				return (string)Row[c_champFormule];
			}
			set
			{
				Row[c_champFormule] = value;
			}
		}

		//-------------------------------------------------------------------
		[TableFieldProperty(c_champCacheFormule, IsInDb=false)]
        [BlobDecoder]
		public C2iExpression Formule
		{
			get
			{
				if ( Row[c_champCacheFormule] == DBNull.Value )
				{
					C2iExpression expression = C2iExpression.FromPseudoCode ( FormuleString );
					if ( expression == null )
						expression = new C2iExpressionConstante ("");
					CContexteDonnee.ChangeRowSansDetectionModification ( Row.Row, c_champCacheFormule, expression );
				}
				return ( C2iExpression )Row[c_champCacheFormule];
			}
			set
			{
				if ( value == null )
					FormuleString = "";
				else
					FormuleString = C2iExpression.GetPseudoCode(value);
				CContexteDonnee.ChangeRowSansDetectionModification ( Row.Row, c_champCacheFormule, DBNull.Value );
			}
		}

		/// //////////////////////////////////////////////////
		///<summary>
        /// Nom convivial du type des entités concernés par le champ calculé
        /// </summary>
        [DynamicField("Object type")]
		public string TypeObjetsConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial(TypeObjets);
			}
		}

		/// //////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeObjets, 1024)]
		public string TypeObjetsString
		{
			get
			{
				return (string)Row[c_champTypeObjets];
			}
			set
			{
				Row[c_champTypeObjets] = value;
			}
		}

        /// //////////////////////////////////////////////////
        [DynamicField("Folder")]
        [TableFieldProperty(c_champRubrique, 64)]
        public string Categorie
        {
            get
            {
                return (string)Row[c_champRubrique];
            }
            set
            {
                Row[c_champRubrique] = value;
            }
        }

        /// //////////////////////////////////////////////////
        public override void AfterRead()
        {
            base.AfterRead();
            CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheFormule, DBNull.Value);
        }

		/// //////////////////////////////////////////////////
		public Type TypeObjets
		{
			get
			{
				return CActivatorSurChaine.GetType(TypeObjetsString);
			}
			set
			{
				if(  value == null )
					TypeObjetsString = "";
				else
					TypeObjetsString = value.ToString();
			}
		}

		/// //////////////////////////////////////////////////
		public object Calcule ( object objetEvalue, CFournisseurPropDynStd fournisseur )
		{
			if ( objetEvalue == null )
				return null;
			if ( fournisseur == null )
				fournisseur = new CFournisseurPropDynStd();
			CContexteEvaluationExpression contexte = new CContexteEvaluationExpression(objetEvalue );
			contexte.AttacheObjet(typeof(CContexteDonnee), ContexteDonnee);
			CResultAErreur result = Formule.Eval(contexte);
			contexte.DetacheObjet(typeof(CContexteDonnee));
			if ( result )
				return result.Data;
			return null;
		}
		#region Membres de IVariableDynamique

		public CTypeResultatExpression TypeDonnee
		{
			get
			{
				C2iExpression formule = Formule;
				if ( formule != null )
					return formule.TypeDonnee;
				return new CTypeResultatExpression ( typeof(string), false );
			}
		}

		public bool IsChoixParmis()
		{
			return false;
		}

		public System.Collections.IList Valeurs
		{
			get
			{
				return new ArrayList();
			}
		}

		public bool IsChoixUtilisateur()
		{
			return false;
		}

		public CResultAErreur VerifieValeur(object valeur)
		{
			return CResultAErreur.True;
		}

		#endregion

        #region IObjetCherchable Membres

        public CRequeteRechercheObjet GetRequeteRecherche()
        {
            CDefinitionProprieteDynamiqueChampCalcule defC = new CDefinitionProprieteDynamiqueChampCalcule(this);
            return new CRequeteRechercheObjet(
                I.T("Search calc field '@1'|20039",Nom),
                defC);
        }

        #endregion
    }
		
}
