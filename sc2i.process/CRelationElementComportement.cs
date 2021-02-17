using System;
using System.Data;


using sc2i.common;
using sc2i.data;
using sc2i.expression;

namespace sc2i.process
{
	#region RelationElementToComportementAttribute
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationElementToComportementAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CRelationElementComportement.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 500;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "ELT_COMPT";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CRelationElementComportement.c_champIdElement;
			}
		}

		public override string ChampType
		{
			get
			{
				return CRelationElementComportement.c_champTypeElement;
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
				return I.T("Behaviors relations|287");
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return tp.IsSubclassOf ( typeof(CObjetDonneeAIdNumerique) );
		}
	}
	#endregion
	/// <summary>
	/// Association entre un élément et un comportement
	/// </summary>
	[Table(CRelationElementComportement.c_nomTable, CRelationElementComportement.c_champId, false)]
	[ObjetServeurURI("CRelationElementComportementServeur")]
	[DynamicClass("Entity / Behavior")]
	[RelationElementToComportement]
	public class CRelationElementComportement : CObjetDonneeAIdNumeriqueAuto
	{

		public const string c_nomTable = "GENERIC_BEHAVIOR_ELEMENT";
		public const string c_champId = "GEBEHELT_ID";
		public const string c_champTypeElement = "GEBHELT_ELT_TYPE";
		public const string c_champIdElement = "GEBHELT_ELT_ID";

		/// <summary>
		/// /////////////////////////////////////////////////////////////
		/// </summary>
		public CRelationElementComportement( CContexteDonnee contexte)
			:base ( contexte )
		{
		}

		/// /////////////////////////////////////////////////////////////
		public CRelationElementComportement ( DataRow row )
			:base( row )
		{
		}

		/// /////////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The relation between the element and the behavior|288");
			}
		}

		/// /////////////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// /////////////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}

		


		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeElement, 1024)] 
		[IndexField]
		public string TypeElementString
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

		/// /////////////////////////////////////////////////////////
		public Type TypeElement 
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeElementString );
			}
			set
			{
				if ( value != null )
					TypeElementString = value.ToString();
			}
		}

		/// /////////////////////////////////////////////////////////
		/// <summary>
		/// Type de l'élément objet de la relation
		/// </summary>
		[DynamicField("Element type")]
		public string TypeElementConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial(TypeElement);
			}
		}

		//------------------------------------------------------------
        /// <summary>
        /// Identifiant (ID) de l'élément, objet de la relation
        /// </summary>
		[TableFieldPropertyAttribute(c_champIdElement)]
		[DynamicField("Element id")]
		[IndexField]
		public int IdElement
		{
			get
			{
				return ( int )Row[c_champIdElement];
			}
			set
			{
				Row[c_champIdElement] = value;
			}
		}


		/// /////////////////////////////////////////////////////////////
		public CObjetDonneeAIdNumerique ElementAssocie
		{
			get
			{
				try
				{
					Type tp = TypeElement;
					if ( tp == null )
						return null;
#if PDA
					CObjetDonneeAIdNumeriqueAuto obj = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance ( tp );
					obj.ContexteDonnee =ContexteDonnee;
#else
					CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new Object[]{ContexteDonnee});
#endif
					if ( obj.ReadIfExists ( IdElement ) )
						return obj;
					return null;
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if ( value == null )
					return;
				IdElement = value.Id;
				TypeElement = value.GetType();
			}
		}

		/// /////////////////////////////////////////////////////////////
		/// <summary>
		/// Comportement lié à l'élément
		/// </summary>
		[Relation(CComportementGenerique.c_nomTable,
			 CComportementGenerique.c_champId,
			 CComportementGenerique.c_champId,
			 true,
			 true,
			 false)]
		[DynamicField("Behavior")]
		public CComportementGenerique Comportement
		{
			get
			{
				return ( CComportementGenerique )GetParent ( CComportementGenerique.c_champId, typeof(CComportementGenerique) );
			}
			set
			{
				SetParent ( CComportementGenerique.c_champId, value );
			}
		}


		
	}
}
