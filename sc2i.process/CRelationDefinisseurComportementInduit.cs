using System;
using System.Data;


using sc2i.common;
using sc2i.data;
using sc2i.expression;

namespace sc2i.process
{
	#region RelationDefinisseurToComportementInduitAttriubte
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationDefinisseurToComportementInduitAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CRelationDefinisseurComportementInduit.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 450;
			}
		}


		protected override string MyIdRelation
		{
			get
			{
				return "DEF_COMPORT_INDUIT";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CRelationDefinisseurComportementInduit.c_champIdDefinisseur;
			}
		}

		public override string ChampType
		{
			get
			{
				return CRelationDefinisseurComportementInduit.c_champTypeDefinisseur;
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
				return I.T("Induced behaviors relations|285");
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return typeof(IDefinisseurEvenements).IsAssignableFrom ( tp );
		}
	}
	#endregion
	/// <summary>
    /// Association entre un définisseur d'événements et un <see cref="CComportementGenerique">comportement</see>
	/// </summary>
	/// <remarks>
	/// Pour chaque comportement induit pas un définisseur d'événements,
	/// il y a un objet de ce type.
	/// </remarks>
	[Table(CRelationDefinisseurComportementInduit.c_nomTable, CRelationDefinisseurComportementInduit.c_champId, false)]
	[ObjetServeurURI("CRelationDefinisseurComportementInduitServeur")]
	[DynamicClass("Event definer / behavior")]
	[RelationDefinisseurToComportementInduit]
	public class CRelationDefinisseurComportementInduit : CObjetDonneeAIdNumeriqueAuto
	{

		public const string c_nomTable = "INDUCED_BEHAVIOR_DEF";
		public const string c_champId = "INDBEHDEF_ID";
		public const string c_champTypeDefinisseur = "INDBEHDEF_ELT_TYPE";
		public const string c_champIdDefinisseur = "INDBEHDEF_ELT_ID";

		/// <summary>
		/// /////////////////////////////////////////////////////////////
		/// </summary>
		public CRelationDefinisseurComportementInduit( CContexteDonnee contexte)
			:base ( contexte )
		{
		}

		/// /////////////////////////////////////////////////////////////
		public CRelationDefinisseurComportementInduit ( DataRow row )
			:base( row )
		{
		}

		/// /////////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The relation between the definer and the induced behavior|286");
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
		[TableFieldProperty(c_champTypeDefinisseur, 1024)] 
		[IndexField]
		public string TypeDefinisseurString
		{
			get
			{
				return (string)Row[c_champTypeDefinisseur];
			}
			set
			{
				Row[c_champTypeDefinisseur] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public Type TypeDefinisseur 
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeDefinisseurString );
			}
			set
			{
				if ( value != null )
					TypeDefinisseurString = value.ToString();
			}
		}

		/// /////////////////////////////////////////////////////////
		/// <summary>
		/// Type de l'élément objet de la relation
		/// </summary>
		[DynamicField("Element type")]
		public string TypeDefinisseurConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial(TypeDefinisseur);
			}
		}

		//-------------------------------------------------------------
        /// <summary>
        /// Identifiant (ID) de l'élément, objet de la relation
        /// </summary>
		[TableFieldPropertyAttribute(c_champIdDefinisseur)]
		[DynamicField("Element id")]
		[IndexField]
		public int IdDefinisseur
		{
			get
			{
				return ( int )Row[c_champIdDefinisseur];
			}
			set
			{
				Row[c_champIdDefinisseur] = value;
			}
		}


		//---------------------------------------------------------
        /// <summary>
        /// L'élément, objet de la relation
        /// </summary>
		[DynamicField("Element")]
        public CObjetDonneeAIdNumerique DefinisseurAssocie
		{
			get
			{
				try
				{
					Type tp = TypeDefinisseur;
					if ( tp == null )
						return null;
#if PDA
					CObjetDonneeAIdNumeriqueAuto obj = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance ( tp );
					obj.ContexteDonnee =ContexteDonnee;
#else
					CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new Object[]{ContexteDonnee});
#endif
					if ( obj.ReadIfExists ( IdDefinisseur ) )
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
				IdDefinisseur = value.Id;
				TypeDefinisseur = value.GetType();
			}
		}

		/// /////////////////////////////////////////////////////////////
		/// <summary>
		/// Le Comportement lié à l'élément
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
