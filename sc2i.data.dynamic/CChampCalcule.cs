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
	/// Repr�sente un champ calcul�.
	/// </summary>
    /// <remarks>
    /// <p>Chaque champ calcul� est pr�vu pour un type d'entit� sp�cifique et ne pourra �tre appliqu�e qu'� lui seul.</p>
    /// Il est possible de cr�er des champs calcul�s sur tous les types d'entit� de l'application. Ces champs seront disponibles
    /// en lecture seule dans les formules et dans tout �l�ment de param�trage permettant l'acc�s aux champs de l'application.
    /// <p>Il n'est pas possible de filtrer les donn�es sur des valeurs de champs calcul�s</p>
    /// <p>Les champs calcul�s sont g�n�rallement utilis�s pour �viter de r�p�ter des formules dont on a souvent besoin sur des entit�s.
    /// Ils permettent �galement � l'utilisateur de voir une donn�es calcul� dans les listes de l'application.</p>
    /// <p><B>Attention</B>L'utilisation de champs calcul�s peut ralentir consid�rablement l'affichage des liste dans l'application.
    /// S'ils sont mals utilis�s, les performances globales de l'application �tre affect�e.</p>
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
        ///Nom du champ calcul�. Ce nom doit �tre unique.
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
        ///Description du champ calcul�
        /// </summary>
        /// <remarks>
        /// Il est recommand� � l'administrateur d'utiliser la description pour d�crire le fonctionnement du champ
        /// calcul� ainsi que d'indiquer dans quel contexte il est utilis�.
        /// <p>Une bonne documentation des champs calcul�s peut simplifier le travail de maintenance du param�trage.</p>
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
        /// Nom convivial du type des entit�s concern�s par le champ calcul�
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
