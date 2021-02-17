using System;
using System.Data;

using sc2i.data;
using sc2i.common;


namespace sc2i.data.dynamic
{
	#region RelationElementToVariableAttribute
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationElementToVariableAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CValeurVariableSurObjet.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 200;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "ELT_VARIABLES";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CValeurVariableSurObjet.c_champIdElementLie;
			}
		}

		public override string ChampType
		{
			get
			{
				return CValeurVariableSurObjet.c_champTypeElementLie;
			}
		}
		
		public override bool Composition
		{
			get
			{
				return true;
			}
		}

		public override bool CanDeleteToujours
		{
			get
			{
				return true;
			}
		}

		public override string NomConvivialPourParent
		{
			get
			{
				return "Variables";
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique ));
		}
	}
	#endregion
	/// <summary>
	/// Représente une valeur pour une <see cref="CValeurVariableSurObjet">Variable sur objet</see>
	/// </summary>
	/// <remarks>
	/// Chaque valeur de variable est associée à une variable sur objet<BR/>
	/// <P>
	/// Toutes les valeurs de variables sont stockées sous forme de texte, cependant,
	/// il est possible de convertir cette valeur en entier, nombre décimal, date,
	/// booléen en utilisant les propriétés adequates.
	/// La contenu d'une valeur de variable ne peut pas dépasse 4000 caractères
	/// </P>
	/// <P>
	/// <B>ATTENTION</B>:<BR></BR>
	/// La valeur par défaut d'une variable est la chaine vide. Si une valeur
	/// de chaine vide est associée à une variable, l'entité 'valeur de variable' est
	/// automatiquement supprimée de la base de données. Il n'est donc pas possible
	/// de recherche toutes les valeurs représentant la chaine vide, car les 
	/// entités n'existent pas!
	/// </P>
	/// <P>
	/// Contrairement aux champs personnalisés, les variables sont des entités 'sans filet'.
	/// Il est possible de supprimer une variable alors qu'il existe des valeurs
	/// pour celle ci dans la base. Il n'existe pas de lien d'intégrité référentielle
	/// entre les valeurs de variable et les variables.
	/// </P>
	/// </remarks>
	[ObjetServeurURI("CValeurVariableSurObjetServeur")]
	[Table(CValeurVariableSurObjet.c_nomTable,CValeurVariableSurObjet.c_champId,true)]
	[DynamicClass("Variable value")]
	[RelationElementToVariableAttribute]
	[NoRelationTypeId]
	public class CValeurVariableSurObjet : CObjetDonneeAIdNumeriqueAuto, IObjetSansVersion
	{

		public const string c_nomTable = "VARIABLE_VALUE";
		public const string c_champId = "VAVA_ID";
		public const string c_champNomVariable = "VAVA_NAME";
		public const string c_champTypeElementLie = "VAVA_ELEMENT_TYPE";
		public const string c_champIdElementLie = "VAVA_ELEMENT_ID";
		public const string c_champValeur = "VAVA_VALUE";

		//-------------------------------------------------------------------
		public CValeurVariableSurObjet(CContexteDonnee ctx)
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CValeurVariableSurObjet(System.Data.DataRow row)
			:base(row)
		{
		}
			
		//-------------------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
			ValeurTexte = "";
		}

		//-------------------------------------------------------------------
		public override string DescriptionElement
		{
			get
			{
				return I.T("The Variable value @1|236",Nom);
			}
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{GetChampId()};
		}

		/// ///////////////////////////////////////////////////////
		[TableFieldProperty(c_champNomVariable, 256)]
		[DynamicField("Name")]
		[IndexField]
		public string Nom
		{
			get
			{
				return ( string )Row[c_champNomVariable];
			}
			set
			{
				Row[c_champNomVariable] = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Valeur texte de la variable.
		/// </summary>
		[TableFieldProperty(c_champValeur, 4000)]
		[DynamicField("Text value")]
		public string ValeurTexte
		{
			get
			{
				return(  string )Row[c_champValeur];
			}
			set
			{
				Row[c_champValeur] = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la valeur entière de la variable<BR></BR>
		/// Si la conversion entre la valeur texte et le type 'Nombre entier' échoue,
		/// la valeur est 0.
		/// </summary>
		[DynamicField("Int value")]
		public int ValeurInt
		{
			get
			{
				return new CCasteurValeurString ( Nom, ValeurTexte ).ValeurInt;
			}
		}

		/// <summary>
		/// Retourne la valeur décimale de la variable<BR></BR>
		/// Si la conversion entre la valeur texte et le type 'Nombre décimal' échoue,
		/// la valeur est 0.
		/// </summary>
		[DynamicField("Decimal value")]
		public Double ValeurDecimale
		{
			get
			{
				return new CCasteurValeurString ( Nom, ValeurTexte ).ValeurDecimale;
			}
		}

		/// <summary>
		/// Retourne la valeur de la variable convertie en date<BR></BR>
		/// Si la conversion entre la valeur texte et le type 'date' échoue,
		/// la valeur est 1/1/1900 00:00 (1er janvier 1900 à minuit).
		/// </summary>
		[DynamicField("Date value")]
		public DateTime ValeurDate
		{
			get
			{
				return new CCasteurValeurString ( Nom, ValeurTexte ).ValeurDate;
			}
		}

		/// <summary>
		/// Retourne la valeur de la variable convertie en boolean<BR></BR>
		/// </summary>
		/// <remarks>
		/// La valeur est considerée comme fausse si elle est égale à
		/// la chaine vide, 0 ou false. Dans tous les autres cas,
		/// la valeur retournée est 'vrai'.
		/// </remarks>
		public bool ValeurBooleenne
		{
			get
			{
				return new CCasteurValeurString ( Nom, ValeurTexte ).ValeurBooleenne;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[TableFieldProperty(c_champTypeElementLie, 1024)]
		[IndexField]
		public string StringTypeElementLie
		{
			get
			{
				return (string)Row[c_champTypeElementLie];
			}
			set
			{
				Row[c_champTypeElementLie] = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Type TypeElementLie
		{
			get
			{
				return CActivatorSurChaine.GetType ( StringTypeElementLie );
			}
			set
			{
				StringTypeElementLie = value.ToString();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[TableFieldPropertyAttribute(c_champIdElementLie)]
		[IndexField]
		public int IdElementLie
		{
			get
			{
				return (int)Row[c_champIdElementLie];
			}
			set
			{
				Row[c_champIdElementLie] = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[DynamicFieldAttribute("Linked element")]
		public CObjetDonneeAIdNumerique ElementLie
		{
			get
			{
				Type tp = TypeElementLie;
				if ( tp == null )
					return null;
				CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[]{ContexteDonnee});
				if ( obj.ReadIfExists ( IdElementLie) )
					return obj;
				return null;
			}
			set
			{
				if ( value == null )
				{
					TypeElementLie = null;
					IdElementLie = -1;
				}
				else
				{
					TypeElementLie = value.GetType();
					IdElementLie = value.Id;
				}
			}
		}

		/// <summary>
		/// REtourne le CValeurVariable s'il existe
		/// </summary>
		/// <param name="strNomVariable"></param>
		/// <param name="objet"></param>
		/// <returns></returns>
		public static CValeurVariableSurObjet GetValeurFor ( string strNomVariable, CObjetDonneeAIdNumerique objet )
		{
			if ( objet == null )
				return null;
			CFiltreData filtre = new CFiltreData ( 
				c_champTypeElementLie + "=@1 and "+
				c_champIdElementLie + "=@2 and "+
				c_champNomVariable+"=@3",
				objet.GetType().ToString(),
				objet.Id.ToString(),
				strNomVariable );
			CValeurVariableSurObjet valeur = new CValeurVariableSurObjet ( objet.ContexteDonnee );
			if ( valeur.ReadIfExists ( filtre ) )
				return valeur;
			return null;
		}

		/// <summary>
		/// Retourne la valeur texte de la variable.
		/// </summary>
		/// <param name="strNomVariable"></param>
		/// <param name="objet"></param>
		/// <returns></returns>
		public static string GetValeurTexte ( string strNomVariable, CObjetDonneeAIdNumerique objet )
		{
			CValeurVariableSurObjet valeur = GetValeurFor ( strNomVariable, objet );
			if ( valeur == null )
				return "";
			return valeur.ValeurTexte;
		}

		/// <summary>
		/// Définit une valeur de variable
		/// </summary>
		/// <param name="strNomVariable"></param>
		/// <param name="objet"></param>
		/// <param name="strValeur"></param>
		public static void SetValeur ( string strNomVariable, CObjetDonneeAIdNumerique objet, string strValeur )
		{
			if ( objet == null ) 
				return;
			CValeurVariableSurObjet valeur = GetValeurFor ( strNomVariable, objet );
			if ( valeur == null )
			{
				if ( strValeur.Trim() == "" )
					return;
				valeur = new CValeurVariableSurObjet ( objet.ContexteDonnee );
				valeur.CreateNewInCurrentContexte();
				valeur.Nom = strNomVariable;
				valeur.ElementLie = objet;
			}
			valeur.ValeurTexte = strValeur;
		}
	}

	/// <summary>
	/// Ajoute une méthode à CObjetDonneeAIdNumeriqueAuto, pour
	/// qu'il puisse retourner la valeur d'une variable
	/// </summary>
	[AutoExec("Autoexec")]
	public class CMethodGetValeurVariableSurObjet : CMethodeSupplementaire
	{
		protected CMethodGetValeurVariableSurObjet( )
			:base ( typeof(CObjetDonneeAIdNumerique) )
		{
		}

		public static void Autoexec()
		{
			CGestionnaireMethodesSupplementaires.RegisterMethode ( new CMethodGetValeurVariableSurObjet() );
		}

		public override string Name
		{
			get
			{
				return "GetVariable";
			}
		}

		public override string Description
		{
			get
			{
				return I.T("Return a Variable value|237");
			}
		}

		public override Type ReturnType
		{
			get
			{
				return typeof(CCasteurValeurString);
			}
		}

		public override bool ReturnArrayOfReturnType
		{
			get
			{
				return false;
			}
		}


		public override CInfoParametreMethodeDynamique[] InfosParametres
		{
			get
			{
				return new CInfoParametreMethodeDynamique[]
					{
						new CInfoParametreMethodeDynamique ( I.T("Variable|66"),
						I.T("Variable name|238"),
						typeof(string) )
					};
			}
		}

		public override object Invoke ( object objetAppelle, params object[] parametres )
		{
			if ( parametres.Length!=1 || parametres[0] == null ||
				!(objetAppelle is CObjetDonneeAIdNumerique) )
				return null;
			CValeurVariableSurObjet valeur = CValeurVariableSurObjet.GetValeurFor ( parametres[0].ToString(), (CObjetDonneeAIdNumerique)objetAppelle );
			if ( valeur == null )
				return new CCasteurValeurString ( parametres[0].ToString(), "" );
			else
				return new CCasteurValeurString ( valeur.Nom, valeur.ValeurTexte );
		}
	}


    /// <summary>
    /// Ajoute une méthode à CObjetDonneeAIdNumeriqueAuto, pour
    /// qu'il puisse affecter la valeur d'une variable
    /// </summary>
    [AutoExec("Autoexec")]
    public class CMethodSetValeurVariableSurObjet : CMethodeSupplementaire
    {
        protected CMethodSetValeurVariableSurObjet()
            : base(typeof(CObjetDonneeAIdNumerique))
        {
        }

        public static void Autoexec()
        {
            CGestionnaireMethodesSupplementaires.RegisterMethode(new CMethodSetValeurVariableSurObjet());
        }

        public override string Name
        {
            get
            {
                return "SetVariable";
            }
        }

        public override string Description
        {
            get
            {
                return I.T("Set a Variable value|10004");
            }
        }

        public override Type ReturnType
        {
            get
            {
                return typeof(int);
            }
        }

        public override bool ReturnArrayOfReturnType
        {
            get
            {
                return false;
            }
        }


        public override CInfoParametreMethodeDynamique[] InfosParametres
        {
            get
            {
                return new CInfoParametreMethodeDynamique[]
					{
						new CInfoParametreMethodeDynamique ( I.T("Variable|66"),
						I.T("Variable name|238"),
						typeof(string) ),
                        new CInfoParametreMethodeDynamique ( I.T("Value|10005"),
						I.T("Variable value|10006"),
						typeof(string) )
					};
            }
        }

        public override object Invoke(object objetAppelle, params object[] parametres)
        {
            if (parametres.Length != 2 || parametres[0] == null || parametres[1] == null ||
                !(objetAppelle is CObjetDonneeAIdNumerique))
                return -1;

            CValeurVariableSurObjet.SetValeur(
                parametres[0].ToString(),
                (CObjetDonneeAIdNumerique)objetAppelle,
                parametres[1].ToString());

            return 0;
        }
    }

}
