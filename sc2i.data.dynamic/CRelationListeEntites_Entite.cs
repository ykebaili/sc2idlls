using System;
using System.Data;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.dynamic
{
	#region RelationListeEntites_Entite
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationListeEntites_EntiteAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CRelationListeEntites_Entite.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 300;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "REL_LISTE_ENTITE_ENTITE";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CRelationListeEntites_Entite.c_champIdElement;
			}
		}

		public override string ChampType
		{
			get
			{
				return CRelationListeEntites_Entite.c_champTypeElement;
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
				return I.T("Lists relations|195");
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique) );
		}
	}

	#endregion

	/// <summary>
	/// Lien entre une entité et une liste
	/// </summary>
    /// <remarks>
    /// Lorsqu'une <see cref="CListeEntites">Liste d'entités</see> est statique,
    /// chaque entité faisant partie de la liste génère une entité "Entity list / Entity" correspondant à 
    /// l'appartenance de l'entité à la liste.<br></br>
    /// Pour les listes dynamiques, cette entité n'est pas utilisée.
    /// </remarks>
	[Table(CRelationListeEntites_Entite.c_nomTable, 
		 CRelationListeEntites_Entite.c_champId,
		 true)]
	[ObjetServeurURI("CRelationListeEntites_EntiteServeur")]
	[DynamicClass("Entity list / Entity")]
	[RelationListeEntites_Entite]
	public class CRelationListeEntites_Entite : CObjetDonneeAIdNumeriqueAuto, IObjetALectureTableComplete
	{
		public const string c_nomTable = "ENTITY_LIST_ENTITY";

		public const string c_champId = "ENTLSTENTID";
		public const string c_champTypeElement = "ENTLSTENTELEMENT_TYPE";
		public const string c_champIdElement = "ENTLSTENTELEMENT_ID";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ctx"></param>
		public CRelationListeEntites_Entite( CContexteDonnee ctx)
			:base(ctx)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		public CRelationListeEntites_Entite ( DataRow row )
			:base(row)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public override string DescriptionElement
		{
			get
			{
				string strInfo = I.T("Relation between the list @1|194",ListeEntites.Libelle);
				CObjetDonneeAIdNumerique elt = ElementLie;
				if ( elt != null )
					strInfo += I.T(" and @1|193",ElementLie.DescriptionElement);
				return strInfo;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void MyInitValeurDefaut()
		{
		}

		/// <summary>
		/// Liste d'entités concernée
		/// </summary>
		[Relation(CListeEntites.c_nomTable, CListeEntites.c_champId, CListeEntites.c_champId, true, true, true)]
        [DynamicField("Entity list")]
		public CListeEntites ListeEntites
		{
			get
			{
				return (CListeEntites)GetParent ( CListeEntites.c_champId, typeof(CListeEntites));
			}
			set
			{
				SetParent ( CListeEntites.c_champId, value );
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Type TypeElement
		{
			get
			{
				return CActivatorSurChaine.GetType ( StringTypeElement );
			}
			set
			{
				StringTypeElement = value.ToString();
			}
		}

		/// <summary>
		/// Type de l'élément associé à la liste. Le type est stocké sous la forme d'un codage interne.
		/// </summary>
		[TableFieldProperty(c_champTypeElement, 1024)]
        [DynamicField("Element type string")]
		public string StringTypeElement
		{
			get
			{
				return (string)Row[c_champTypeElement];
			}
			set
			{
				Row[c_champTypeElement] = value;
			}
		}

		/// <summary>
		/// Identifiant de l'entité liée à la liste
		/// </summary>
		[TableFieldPropertyAttribute(c_champIdElement)]
        [DynamicField("Element Id")]
		public int IdElement
		{
			get
			{
				return (int)Row[c_champIdElement];
			}
			set
			{
				Row[c_champIdElement] = value;
			}
		}

		/// <summary>
		/// Element lié à la liste
		/// </summary>
		[DynamicFieldAttribute("Linked element")]
		public CObjetDonneeAIdNumerique ElementLie
		{
			get
			{
				Type tp = TypeElement;
				if ( tp == null )
					return null;
#if PDA
				IElement obj = (IElement)Activator.CreateInstance(tp);
				obj.ContexteDonnee = ContexteDonnee;
#else
				CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[]{ContexteDonnee});
#endif
				if ( obj.ReadIfExists ( IdElement) )
					return obj;
				return null;
			}
			set
			{
				if ( value == null )
				{
					TypeElement = null;
					IdElement = -1;
				}
				else
				{
					TypeElement = value.GetType();
					IdElement = value.Id;
				}
			}
		}

	}
}
