using System;
using System.Data;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.process;
using sc2i.multitiers.client;
using sc2i.expression;

namespace sc2i.process
{
	#region RelationToSynchronismeSource
	/// <summary>
	/// Synchronismes ayant l'élément pour destination
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationToSynchronismeSource : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CSynchronismeDonnees.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 360;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "RELSYNCHRONISME_DEST";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CSynchronismeDonnees.c_champIdDest;
			}
		}

		public override string ChampType
		{
			get
			{
				return CSynchronismeDonnees.c_champTypeDest;
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
				return I.T( "Relation with synchronism|290");
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique) );
		}
	}

	/// <summary>
	/// Synchronismes ayant l'élément pour source
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationToSynchronismeDest : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CSynchronismeDonnees.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 400;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "RELSYNCHRONISME_SRC";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CSynchronismeDonnees.c_champIdSource;
			}
		}

		public override string ChampType
		{
			get
			{
				return CSynchronismeDonnees.c_champTypeDest;
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
				return "";
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique) );
		}
	}

	#endregion
	/// <summary>
	/// Entité interne : Garantit la synchronisation entre deux champs de deux éléments.<br/>
    /// Il y a un champ source de la synchronisation et un champ destinataire de la synchronisation.<br/>
    /// Lorsque le champ source est modifié, le champ destinataire est modifié automatiquement.
	/// </summary>
	[DynamicClass("Synchronism")]
	[Table(CSynchronismeDonnees.c_nomTable, CSynchronismeDonnees.c_champId, true)]
	[ObjetServeurURI("CSynchronismeDonneesServeur")]
	[FullTableSync]
	[RelationToSynchronismeDest]
	[RelationToSynchronismeSource]
	public class CSynchronismeDonnees : CObjetDonneeAIdNumeriqueAuto
	{
		//Indique que le champ surveillé est un champ custom.
		//le nom est suivi de l'id du champ
		public const string c_idChampCustom = "CUSTOM#";

		public const string c_nomTable = "DATA_SYNCHRO";
		public const string c_champId = "DATA_SYNCID";
		public const string c_champTypeSource = "SYNC_SOURCE_TYPE";
		public const string c_champIdSource = "DATA_SYNCID_SOURCE";
		public const string c_champChampSource = "DATA_SYNCFIELD_SOURCE";
		public const string c_champTypeDest = "DATA_SYNCTYPE_DEST";
		public const string c_champIdDest = "DATA_SYNCID_DEST";
		public const string c_champChampDest = "DATA_SYNCFIELD_DEST";
		public const string c_champConditionSurDest = "DATA_SYNCCONDITION_DEST";
		public const string c_champConditionSurSource = "DATA_SYNCCONDITION_DEST";
		
#if PDA
		public CSynchronismeDonnees( )
		:base()
		{
		}
#endif
		/// ////////////////////////////////////////////////
		public CSynchronismeDonnees ( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// ////////////////////////////////////////////////
		public CSynchronismeDonnees ( DataRow row )
			:base ( row )
		{
		}

		/// ////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The @1 synchronism|289",Id.ToString());
			}
		}

		/// ////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}

		/// ////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// ////////////////////////////////////////////////
		/// <summary>
		/// Type de l'élément source de la synchronisation
		/// </summary>
		[TableFieldProperty(c_champTypeSource,255)]
		[DynamicField("Source type")]
		[IndexField]
		public string TypeSourceString
		{
			get
			{
				return ( string )Row[c_champTypeSource];
			}
			set
			{
				Row[c_champTypeSource] = value;
			}
		}

		/// ////////////////////////////////////////////////
		/// <summary>
        /// Identifiant (Id) de l'élément source de la synchronisation
		/// </summary>
		[TableFieldProperty ( c_champIdSource )]
		[DynamicField("Source id")]
		[IndexField]
		public int IdSource
		{
			get
			{
				return ( int )Row[c_champIdSource];
			}
			set
			{
				Row[c_champIdSource] = value;
			}
		}

		/// ////////////////////////////////////////////////
		public Type TypeSource
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeSourceString );
			}
			set
			{
				if ( value == null )
					TypeSourceString = "";
				else
					TypeSourceString = value.ToString();
			}
		}

		/// ////////////////////////////////////////////////
		public CObjetDonneeAIdNumerique ObjetSource
		{
			get
			{
				if ( TypeSource == null )
					return null;
				CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance ( TypeSource,new object[]{ContexteDonnee} );
				if ( objet.ReadIfExists ( IdSource ) )
					return objet;
				return null;
			}
			set
			{
				if ( value ==  null )
				{
					TypeSource = null;
				}
				else
				{
					TypeSource = value.GetType();
					IdSource = value.Id;
				}
			}
		}

		/// ////////////////////////////////////////////////
		/// <summary>
		/// Type de l'élément destination de la synchronisation
		/// </summary>
		[TableFieldProperty(c_champTypeDest,255)]
		[DynamicField("Destination element type")]
		[IndexField]
		public string TypeDestString
		{
			get
			{
				return ( string )Row[c_champTypeDest];
			}
			set
			{
				Row[c_champTypeDest] = value;
			}
		}

		/// ////////////////////////////////////////////////
		/// <summary>
		/// Identifiant (Id) de l'élément destination de la synchronisation
		/// </summary>
		[TableFieldProperty ( c_champIdDest )]
		[DynamicField("Destination element Id")]
		[IndexField]
		public int IdDest
		{
			get
			{
				return ( int )Row[c_champIdDest];
			}
			set
			{
				Row[c_champIdDest] = value;
			}
		}

		/// ////////////////////////////////////////////////
		/// <summary>
		/// Champ source de la synchronisation. Si ce champ est modifié,
		/// le champ destination sera modifié automatiquement
		/// </summary>
		[TableFieldProperty(c_champChampSource, 128)]
		[DynamicField("Source field")]
		public string ChampSource
		{
			get
			{
				return ( string )Row[c_champChampSource];
			}
			set
			{
				Row[c_champChampSource] = value;
			}
		}

		/// ////////////////////////////////////////////////
		public Type TypeDest
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeDestString );
			}
			set
			{
				if ( value == null )
					TypeDestString = "";
				else
					TypeDestString = value.ToString();
			}
		}

		/// ////////////////////////////////////////////////
		public CObjetDonneeAIdNumerique ObjetDest
		{
			get
			{
				if ( TypeDest == null )
					return null;
				CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance ( TypeDest,new object[]{ContexteDonnee} );
				if ( objet.ReadIfExists ( IdDest ) )
					return objet;
				return null;
			}
			set
			{
				if ( value ==  null )
				{
					TypeDest = null;
				}
				else
				{
					TypeDest = value.GetType();
					IdDest = value.Id;
				}
			}
		}

		/// ////////////////////////////////////////////////
		
		/// <summary>
		/// Champ destination de la synchronisation. Si le champ source
		/// est modifié, le champ destination est modifié en conséquence
		/// </summary>
		[TableFieldProperty(c_champChampDest, 128)]
		[DynamicField("Destination field")]
		public string ChampDest
		{
			get
			{
				return (string)Row[c_champChampDest];
			}
			set
			{
				Row[c_champChampDest] = value;
			}
		}

		/// ////////////////////////////////////////////////
		[TableFieldProperty(c_champConditionSurSource, 3000)]
		public string FormuleConditionSourceString
		{
			get
			{
				return ( string )Row[c_champConditionSurSource];
			}
			set
			{
				Row[c_champConditionSurSource] = value;
			}
		}

		/// ////////////////////////////////////////////////
		public C2iExpression ConditionSurSource
		{
			get
			{
				C2iExpression exp = C2iExpression.FromPseudoCode ( FormuleConditionSourceString );
				if ( exp == null )
					exp = new C2iExpressionVrai();
				return exp;
			}
			set
			{
				if ( value == null )
					FormuleConditionSourceString = "";
				else
					FormuleConditionSourceString = C2iExpression.GetPseudoCode(value);
			}
		}

		/// ////////////////////////////////////////////////
		[TableFieldProperty(c_champConditionSurDest, 3000)]
		public string FormuleConditionDestString
		{
			get
			{
				return ( string )Row[c_champConditionSurDest];
			}
			set
			{
				Row[c_champConditionSurDest] = value;
			}
		}

		/// ////////////////////////////////////////////////
		public C2iExpression ConditionSurDest
		{
			get
			{
				C2iExpression exp = C2iExpression.FromPseudoCode ( FormuleConditionDestString );
				if ( exp == null )
					exp = new C2iExpressionVrai();
				return exp;
			}
			set
			{
				if ( value == null )
					FormuleConditionDestString = "";
				else
					FormuleConditionDestString = C2iExpression.GetPseudoCode(value);
			}
		}

		/// ////////////////////////////////////////////////
		public static CSynchronismeDonnees CreateSynchronisme (
			CContexteDonnee contexteCreation,
			CObjetDonneeAIdNumerique objetSource,
			CObjetDonneeAIdNumerique objetDest,
			string strChampSource,
			string strChampDest,
			C2iExpression expressionSurSource,
			C2iExpression expressionSurDest )
		{
			CSynchronismeDonnees synchro = new CSynchronismeDonnees ( contexteCreation );
			CFiltreData filtre = new CFiltreData ( 
				c_champTypeSource+"=@1 and "+
				c_champTypeDest+"=@2 and "+
				c_champIdSource+"=@3 and "+
				c_champIdDest+"=@4 and "+
				c_champChampSource+"=@5 and "+
				c_champChampDest+"=@6",
				objetSource.GetType().ToString(),
				objetDest.GetType().ToString(),
				objetSource.Id,
				objetDest.Id,
				strChampSource,
				strChampDest );
			if ( !synchro.ReadIfExists ( filtre ) )
			{
				synchro.CreateNewInCurrentContexte();
				synchro.ObjetSource = objetSource;
				synchro.ObjetDest = objetDest;
				synchro.ChampSource = strChampSource;
				synchro.ChampDest = strChampDest;
			}
			synchro.ConditionSurSource = expressionSurSource;
			synchro.ConditionSurDest = expressionSurDest;
			return synchro;
		}

		
	}
}
