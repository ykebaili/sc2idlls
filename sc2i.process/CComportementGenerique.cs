using System;
using System.Collections;

using sc2i.data;
using sc2i.common;

using sc2i.expression;
using sc2i.process;


namespace sc2i.process
{
	
	/// <summary>
	/// D�finit un comportement (jeu d'�v�nements)
	/// </summary>
	/// <remarks>
	/// <P>
	/// Le comportement consiste � associer des �v�nements
	/// sur un �l�ment.
    /// <BR/>
    /// <BR/>
	/// Un comportement est associ� � un type cible auquel il pourra 
	/// �tre appliqu�. Un �l�ment li� � un comportement se verra attribuer
	/// tous les �v�nements de ce comportement.
	/// </P>
	/// <P>
	/// <B>Attention : </B>Lorsqu'un �l�ment est associ� � un comportement,
	/// les �v�nements du comportement s'activent sur l'objet. Si ces �v�nements
	/// programment des actions (sur modification ou sur date par l'action de cr�ation
	/// de handle d'�v�nement) explicitement
	/// sur un �l�ment, le fait de ne plus associer l'�l�ment au comportement ne 
	/// supprimera pas ces actions.
	/// </P>
	/// 
	/// </remarks>
	[DynamicClass("Behavior")]
	[ObjetServeurURI("CComportementGeneriqueServeur")]
	[Table(CComportementGenerique.c_nomTable, CComportementGenerique.c_champId,true)]
	[FullTableSync]
	public class CComportementGenerique : CObjetDonneeAIdNumeriqueAuto, IDefinisseurEvenements
	{
		#region D�claration des constantes
		public const string c_nomTable = "GENERIC_BEHAVIOR";
		public const string c_champId = "GENBEH_ID";
		public const string c_champLibelle = "GENBEH_LABEL";
		public const string c_champTypeElementCible = "GENBEH_TARGET_TYPE";
		#endregion
		//-------------------------------------------------------------------
#if PDA
		public CComportementGenerique()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CComportementGenerique( CContexteDonnee ctx )
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CComportementGenerique( System.Data.DataRow row )
			:base(row)
		{
		}
		//-------------------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
		}
		//-------------------------------------------------------------------
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}
		//-------------------------------------------------------------------
		public override string DescriptionElement
		{
			get
			{
				return I.T("The @1 behavior|30001",Libelle);
			}
		}
		//-------------------------------------------------------------------
		/// <summary>
		/// Libell� du comportement.
		/// </summary>
		[
		TableFieldProperty(c_champLibelle, 255),
		DynamicField("Label")
		]
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return (string)Row[c_champLibelle];
			}
			set
			{
				Row[c_champLibelle] = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Type des �l�ments associ�s
		/// </summary>
		[TableFieldProperty ( c_champTypeElementCible, 1024)]
		public string TypeCibleString
		{
			get
			{
				return ( string )Row[c_champTypeElementCible];
			}
			set
			{
				Row[c_champTypeElementCible] = value;
			}
		}

		//-------------------------------------------------------------------
		public Type TypeCible
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeCibleString );
			}
			set
			{
				if ( value == null )
					TypeCibleString = "";
				else
					TypeCibleString = value.ToString();
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Type des �l�ments qui peuvent appliquer ce comportement
		/// </summary>
		public Type[] TypesCibleEvenement
		{
			get
			{
				Type tp = TypeCible;
				if ( tp != null )
					return new Type[]{tp};
				return new Type[0];
			}
			set
			{
				if ( value == null )
					TypeCibleString = "";
				else
					TypeCibleString = value.ToString();
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Type des �l�ments qui peuvent appliquer ce comportement
		/// </summary>
		[DynamicField("Target type")]
		public string TypeCibleConvivial
		{
			get
			{
				Type[] tps = TypesCibleEvenement;
				if ( tps.Length > 0 )
					return DynamicClassAttribute.GetNomConvivial ( tps[0] );
				return "";
			}
		}

		//-------------------------------------------------------------------
		public CListeObjetsDonnees Evenements
		{
			get
			{
				return CUtilDefinisseurEvenement.GetEvenementsFor ( this );
			}
		}

		//-------------------------------------------------------------------
		public CComportementGenerique[] ComportementsInduits
		{
			get
			{
				return CUtilDefinisseurEvenement.GetComportementsInduits ( this );
			}
		}

		//-------------------------------------------------------------------
		private CRelationElementComportement GetRelationForObjet ( CObjetDonneeAIdNumerique objet )
		{
			if ( objet == null )
				return null;
			CRelationElementComportement relation = new CRelationElementComportement ( objet.ContexteDonnee );
			if ( relation.ReadIfExists ( 
				new CFiltreData (
				CRelationElementComportement.c_champTypeElement+"=@1 and "+
				CRelationElementComportement.c_champIdElement+"=@2 and "+
				c_champId+"=@3",
				objet.GetType().ToString(),
				objet.Id,
				Id ) ) )
				return relation;
			return null;
		}
		//-------------------------------------------------------------------
		public void AddComportementToObjet ( CObjetDonneeAIdNumerique objet )
		{
			CRelationElementComportement relation = GetRelationForObjet ( objet );
			if ( relation != null )
				return ;
			relation = new CRelationElementComportement ( objet.ContexteDonnee );
			relation.CreateNewInCurrentContexte();
			relation.Comportement = this;
			relation.ElementAssocie = objet;
		}

		//-------------------------------------------------------------------
		public void RemoveComportementFromObjet ( CObjetDonneeAIdNumerique objet )
		{
			CRelationElementComportement relation = GetRelationForObjet ( objet );
			if ( relation != null )
				relation.Delete();
		}

		//-------------------------------------------------------------------
		public bool IsAppliqueAObjet ( CObjetDonneeAIdNumerique objet )
		{
			if ( objet is IElementAEvenementsDefinis )
				return CUtilIsElementDefiniPar.IsDefiniPar ( objet, this );
			else
				return GetRelationForObjet(objet)!=null;
		}
	}
}
