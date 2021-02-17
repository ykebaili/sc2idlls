using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	#region RelationElementToCaracteristique
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationElementToCaracteristiqueAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CCaracteristiqueEntite.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 1300;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "CHARCTERISTIC";
			}
		}


		public override string ChampId
		{
			get
			{
				return CCaracteristiqueEntite.c_champIdElementLie;
			}
		}

		public override string ChampType
		{
			get
			{
				return CCaracteristiqueEntite.c_champTypeElement;
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
				//A besoin de v�rifier les champs customs
				return false;
			}
		}

		public override string NomConvivialPourParent
		{
            get
            {
                return "Characteristics";
            }
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tp);
		}
	}
	#endregion
	/// <summary>
	/// Une caract�ristique correspond � un sous �l�ment g�n�rique d'un �l�ment. 
	/// </summary>
    /// <remarks>
    /// Les caract�ristiques sont g�n�rallement utilis�es lorsqu'un champ personnalis� ne suffit pas � stocker des donn�es sp�cifique.
    /// <p>Il est utile de cr�er des caract�ristiques lorsqu'une entit� pouvant accueillir des champs personnalis�s poss�de des donn�es
    /// multiples pour un m�me �l�ment<br></br>
    /// Par exemple si l'on souhaite stocker sur un site une liste de noms de gardiens, plut�t que de cr�er 3 champs "Gardien 1",
    /// "gardien 2" et "Gardien 3", il sera pr�f�rable de cr�er une caract�ristique ayant un champ personnalis� "Nom" qui sera
    /// appliqu�e aux sites.<br></br>
    /// PAr ce biais, il sera possible de stocker autant de nom de gardien que d�sir� sur un m�me site.</p>
    /// <p>Chaque caract�ristique appartient � un <see cref="CTypeCaracteristique">type de caract�ristique</see> qui 
    /// permet de retrouver et de qualifier la caract�ristique.</p>
    /// <p>Toute caract�ristique est syst�matiquement associ�e � une et une seule entit�.</p>
    ///
    /// </remarks>
	[DynamicClass("Characteristic")]
	[ObjetServeurURI("CCaracteristiqueEntiteServeur")]
	[Table(CCaracteristiqueEntite.c_nomTable, CCaracteristiqueEntite.c_champId, true)]
	[AutoExec("Autoexec")]
	[RelationElementToCaracteristique]
    [Index(CCaracteristiqueEntite.c_champTypeElement, CCaracteristiqueEntite.c_champIdElementLie, IsCluster=true)]
    [RelationsToReadAlways("RelationsChampsCustom")]
	public class CCaracteristiqueEntite : CElementAChamp
	{
		public const string c_roleChampCustom = "CHARACTERISTIC";

		public const string c_nomTable = "CHARACTERITIC";
		public const string c_champId = "CHAR_ID";
		public const string c_champLibelle = "CHAR_LABEL";
		public const string c_champIdElementLie = "CHAR_LINKED_ELEMENT_ID";
		public const string c_champTypeElement = "CHAR_ELEMENT_TYPE";

		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CCaracteristiqueEntite(CContexteDonnee ctx)
			: base(ctx)
		
        {
		}

		/// //////////////////////////////////////////////////
		public CCaracteristiqueEntite(DataRow row)
			: base(row)
		{
		}

		//-------------------------------------------------------------------
		public static void Autoexec()
		{
			CRoleChampCustom.RegisterRole(c_roleChampCustom, I.T("Characteristic|20049"), typeof(CCaracteristiqueEntite), typeof(CTypeCaracteristiqueEntite));
		}

        /// //////////////////////////////////////////////////
        public override CRoleChampCustom RoleChampCustomAssocie
        {
            get { return CRoleChampCustom.GetRole(c_roleChampCustom); }
        }

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
                return I.T("Characteristic @1|20015", Libelle);
			}
		}


		/// //////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[] { c_champLibelle, c_champId };
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Libell� de la caract�ristique. Ce libell� est d�termin� par le param�trage de l'application
        ///qui peut indiquer un libell� � toute caract�ristique lors de sa cr�ation ou de sa modification.
        /// </summary>
        [TableFieldPropertyAttribute(c_champLibelle, 255)]
		[DynamicField("Label")]
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

		/// <summary>
		/// Type de la caract�ristique
		/// </summary>
		/// <returns></returns>
		[Relation(
			CTypeCaracteristiqueEntite.c_nomTable,
			CTypeCaracteristiqueEntite.c_champId,
			CTypeCaracteristiqueEntite.c_champId,
			true,
			false,
			true)]
		[DynamicField("Charteristic type")]
		public CTypeCaracteristiqueEntite TypeCaracteristique
		{
			get
			{
				return (CTypeCaracteristiqueEntite)GetParent(CTypeCaracteristiqueEntite.c_champId, typeof(CTypeCaracteristiqueEntite));
			}
			set
			{
				SetParent(CTypeCaracteristiqueEntite.c_champId, value);
			}
		}


		//-------------------------------------------------------------------
		public override CRelationElementAChamp_ChampCustom GetNewRelationToChamp()
		{
			return new CRelationCaracteristiqueEntite_ChampCustom(ContexteDonnee);
		}

		//-------------------------------------------------------------------
		public override IDefinisseurChampCustom[] DefinisseursDeChamps
		{
			get
			{
				return new IDefinisseurChampCustom[] { TypeCaracteristique };
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Liens vers les valeurs de champs personnalis�s pour cette caract�ristique.
		/// </summary>
        [RelationFille(typeof(CRelationCaracteristiqueEntite_ChampCustom), "ElementAChamps")]
		[DynamicChilds("Custom fields relations", typeof(CRelationCaracteristiqueEntite_ChampCustom))]
		public override CListeObjetsDonnees RelationsChampsCustom
		{
			get
			{
				return GetDependancesListe(CRelationCaracteristiqueEntite_ChampCustom.c_nomTable, GetChampId());
			}
		}

       
		/// <summary>
		/// Interne : Type de l'�l�ment suivi. Le type est stock� sous forme d'un codage interne.
		/// </summary>
		[TableFieldProperty(c_champTypeElement, 1024)]
		[DynamicField("Element type")]
		public string StringTypeElementLie
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
		/// 
		/// </summary>
		public Type TypeElementLie
		{
			get
			{
				return CActivatorSurChaine.GetType(StringTypeElementLie);
			}
			set
			{
				if (value != null)
					StringTypeElementLie = value.ToString();
				else
					StringTypeElementLie = "";
			}
		}

		/// <summary>
		/// Interne : Id de l'�l�ment associ� � la caract�ristique
		/// </summary>
		[TableFieldPropertyAttribute(c_champIdElementLie)]
		[DynamicField("Element Id")]
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
		/// Element li� � la caract�ristique
		/// </summary>
		[DynamicFieldAttribute("Element")]
		public CObjetDonneeAIdNumerique ElementSuivi
		{
			get
			{
				Type tp = TypeElementLie;
				if (tp == null)
					return null;
				CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[] { ContexteDonnee });
				if (obj.ReadIfExists(IdElementLie))
					return obj;
				return null;
			}
			set
			{
				if (value == null)
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
		/// Retourne les caract�ristiques d'un �l�ment
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static CListeObjetsDonnees GetCaracteristiques ( IObjetDonneeAIdNumerique element )
		{
			CFiltreData filtre = null;
			if (element == null)
				filtre = new CFiltreDataImpossible();
			else
			{
				filtre = new CFiltreData(CCaracteristiqueEntite.c_champTypeElement + "=@1 and " +
					CCaracteristiqueEntite.c_champIdElementLie + "=@2",
					element.GetType().ToString(),
					((IObjetDonneeAIdNumerique)element).Id);
			}
			return new CListeObjetsDonnees(element.ContexteDonnee, typeof(CCaracteristiqueEntite), filtre);
		}

	}
		
}
