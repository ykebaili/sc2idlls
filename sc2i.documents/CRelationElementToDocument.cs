using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.common;


namespace sc2i.documents
{
	#region RelationElementToDocumentAttribute
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationToDocumentAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CRelationElementToDocument.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 600;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "RELDOC";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CRelationElementToDocument.c_champIdElement;
			}
		}

		public override string ChampType
		{
			get
			{
				return CRelationElementToDocument.c_champTypeElement;
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
				return "Relations documents";
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique) );
		}
	}
	#endregion

    /// <summary>
    /// Relation entre un élément TIMOS et <see cref="CDocumentGED">un document GED</see>
    /// </summary>
	[DynamicClass("EDM document / Entity")]
	[ObjetServeurURI("CRelationElementToDocumentServeur")]
	[Table(CRelationElementToDocument.c_nomTable, CRelationElementToDocument.c_champId,true)]
	[RelationToDocumentAttribute]
	public class CRelationElementToDocument : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "ELEMENT_TO_DOCUMENT";

		public const string c_champId = "RETE_ID";
		public const string c_champTypeElement = "RETE_ELEMENT_TYPE";
		public const string c_champIdElement = "RETE_ELEMENT_ID";
		
		
		/// ////////////////////////////////////////////////////
		public CRelationElementToDocument( CContexteDonnee contexte)
			:base ( contexte )
		{
			
		}

		/// ////////////////////////////////////////////////////
		public CRelationElementToDocument ( DataRow row )
			:base ( row )
		{
		}

		/// ////////////////////////////////////////////////////
		/// <summary>
		/// 
		/// </summary>
		public override string DescriptionElement
		{
			get
			{
				string strInfo = I.T("Relation between document @1|116",DocumentGed.Id.ToString());
				IObjetDonneeAIdNumerique elt = ElementLie;
				if ( elt != null )
					strInfo += I.T(" and @1|115",ElementLie.DescriptionElement);
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
		/// Document GED, objet de la relation
		/// </summary>
		[Relation(CDocumentGED.c_nomTable, CDocumentGED.c_champId, CDocumentGED.c_champId, true, true)]
		[DynamicField("EDM document")]
		public CDocumentGED DocumentGed
		{
			get
			{
				return (CDocumentGED)GetParent ( CDocumentGED.c_champId, typeof(CDocumentGED));
			}
			set
			{
				SetParent ( CDocumentGED.c_champId, value );
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Type TypeElementLie
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
		/// Type de l'élément TIMOS, objet de la relation
		/// </summary>
		[TableFieldProperty(c_champTypeElement, 1024)]
		[IndexField]
        [DynamicField("Element Type")]
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
		/// Identifiant (ID) de l'élément TIMOS, objet de la relation
		/// </summary>
		[TableFieldPropertyAttribute(c_champIdElement)]
		[IndexField]
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
		/// Elément TIMOS, objet de la relation
		/// </summary>
		[DynamicFieldAttribute("Linked element")]
		public IObjetDonneeAIdNumerique ElementLie
		{
			get
			{
				Type tp = TypeElementLie;
				if ( tp == null )
					return null;

				IObjetDonneeAIdNumerique obj = (IObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[]{ContexteDonnee});
				if ( obj.ReadIfExists ( IdElement ) )
					return obj;
				return null;
			}
			set
			{
				if ( value == null )
				{
					TypeElementLie = null;
					IdElement = -1;
				}
				else
				{
					TypeElementLie = value.GetType();
					IdElement = value.Id;
				}
			}
		}

		

	}
}
