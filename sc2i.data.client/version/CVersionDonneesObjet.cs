using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sc2i.common;


namespace sc2i.data
{
	/// <summary>
	/// Contient le détail des modification d'une entité particuli-re au sein d'une <see cref="CVersionDonnees">version de données</see>.
	/// </summary>
	[ObjetServeurURI("CVersionDonneesObjetServeur")]
	[Table(CVersionDonneesObjet.c_nomTable, CVersionDonneesObjet.c_champId, true)]
	[RelationVersionDonneesObjet]
	[NoRelationTypeId]
	[DynamicClass("Data Version object")]
    [NoIdUniversel]
	public class CVersionDonneesObjet : CObjetDonneeAIdNumeriqueAuto, IObjetSansVersion
	{
		private static bool m_bEnableJournalisation = false;

		public const string c_nomTable = "DATA_VERSION_OBJECT";

		public const string c_champId = "DVOB_ID";
		public const string c_champIdElement = "DVOB_ELT_ID";
		public const string c_champTypeElement = "DVOB_ELT_TYPE";
		public const string c_champTypeOperation = "DVOB_OPERATION_TYPE";

		public CVersionDonneesObjet(CContexteDonnee contexte)
			: base(contexte)
		{
		}

		public CVersionDonneesObjet(DataRow row)
			: base(row)
		{
		}

		/// <summary>
		/// Initialise le système pour activer la journalisation des données
		/// </summary>
		/// <returns></returns>
		public static bool EnableJournalisation
		{
			get
			{
				return m_bEnableJournalisation;
			}
			set
			{
				m_bEnableJournalisation = value;
			}
		}

		public override string[] GetChampsTriParDefaut()
		{
			return new string[] { c_champId };
		}

		protected override void MyInitValeurDefaut()
		{
			CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Aucune;
		}

		public override string DescriptionElement
		{
			get { return I.T("Object data version @1|176", Element == null?"":Element.DescriptionElement); }
		}

        /// <summary>
        /// Id de l'entité concernée par la modification.
        /// </summary>
		[TableFieldProperty(CVersionDonneesObjet.c_champIdElement)]
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
        /// Type de l'entité concernée par la modification.
        /// </summary>
        /// <remarks>
        /// Le type de l'entité est stocké sous la forme d'une chaine de caractère correspondant au type interne
        /// de l'entité.
        /// </remarks>
		[TableFieldProperty(CVersionDonneesObjet.c_champTypeElement, 1024)]
		[DynamicField("System element type")]
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
        /// Libellé du type de l'élément concerné par la modification. Cette propriété
        /// peut être utilisée pour présenter à l'utilisateur le type de l'entité concernée.
        /// </summary>
		[DynamicField("Element type name")]
		public string TypeElementConvivial
		{
			get
			{
				Type tp = TypeElement;
				if (tp != null)
					return DynamicClassAttribute.GetNomConvivial(tp);
				return "";
			}
		}

		public Type TypeElement
		{
			get
			{
				return CActivatorSurChaine.GetType(StringTypeElement);
			}
			set
			{
				if (value != null)
					StringTypeElement = value.ToString();
				else
					StringTypeElement = "";
			}
		}

        /// <summary>
        /// Type de l'opération de modification
        /// </summary>
        /// <remarks>
        /// Les types de modifications possibles sont  :<BR></BR>
        /// <LI>0  : Création de l'entité</LI>
        /// <LI>10 : Suppression de l'entité</LI>
        /// <LI>20 : Modification</LI>
        /// <LI>30 : Aucune modification</LI>
        /// </remarks>
		[TableFieldProperty(CVersionDonneesObjet.c_champTypeOperation)]
		[DynamicField("Operation type code")]
		public int CodeTypeOperation
		{
			get
			{
				return (int)Row[c_champTypeOperation];
			}
			set
			{
				Row[c_champTypeOperation] = value;
			}
		}

		[DynamicField("Operation type")]
		public CTypeOperationSurObjet TypeOperation
		{
			get
			{
				return new CTypeOperationSurObjet((CTypeOperationSurObjet.TypeOperation)CodeTypeOperation);
			}
			set
			{
				if (value != null)
					CodeTypeOperation = value.CodeInt;
			}
		}

		/// <summary>
		/// Retourne le descriptif de l'élément alteré par la version
		/// </summary>
		[DynamicField("Element information")]
		public string InfoElement
		{
			get
			{
				CObjetDonneeAIdNumerique element = Element;
				if (element == null || !element.IsValide())
				{
					return DynamicClassAttribute.GetNomConvivial(TypeElement);
				}
				return element.DescriptionElement;
			}
		}

		/// <summary>
		/// Retourne l'élément correspondant à cette version.
		/// </summary>
        /// <remarks>
        /// Attention si la modification correspond à un élément créé dans une version prévisionnelle ou supprimé
        /// dans une version archive, il est possible que cette propriété soit nulle suivant le contexte.
        /// </remarks>
		[DynamicField("Element")]
		public CObjetDonneeAIdNumerique Element
		{
			get
			{
				Type tp = CActivatorSurChaine.GetType ( StringTypeElement );
				if ( tp != null )
				{
					try
					{
						CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance ( tp, new object[]{ContexteDonnee} );
						if ( objet.ReadIfExists ( IdElement ) )
							return objet;
					}
					catch
					{}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					StringTypeElement = "";
					IdElement = -1;
				}
				else
				{
					StringTypeElement = value.GetType().ToString();
					IdElement = value.Id;
				}
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
        /// Retourne la version auquel appartient cette version d'objet
		/// </summary>
		[Relation(
			CVersionDonnees.c_nomTable,
			CVersionDonnees.c_champId,
			CVersionDonnees.c_champId,
			true,
			true,
			true)]
		[DynamicField("Data version")]
		public CVersionDonnees VersionDonnees
		{
			get
			{
				return (CVersionDonnees)GetParent(CVersionDonnees.c_champId, typeof(CVersionDonnees));
			}
			set
			{
				SetParent(CVersionDonnees.c_champId, value);
			}
		}

		//---------------------------------------------
		protected override CResultAErreur MyCanDelete()
		{
			CResultAErreur result = base.MyCanDelete();
			if (!result)
				return result;
            if (VersionDonnees.CodeTypeVersion != (int)CTypeVersion.TypeVersion.Previsionnelle)
                return result;
			//Vérifie qu'il n'y a pas d'objet dans la base associé à la version et
			//à cet objet
			CObjetDonneeAIdNumerique objet = Element;
			if (objet != null)
			{
				CListeObjetsDonnees liste = new CListeObjetsDonnees(ContexteDonnee, Element.GetType());
				liste.Filtre = new CFiltreData(
					"(" + Element.GetChampId() + "=@1 or " +
					CSc2iDataConst.c_champOriginalId + "=@1) and " +
					CSc2iDataConst.c_champIdVersion + "=@2",
					IdElement,
					VersionDonnees.Id);
				liste.Filtre.IgnorerVersionDeContexte = true;
				liste.PreserveChanges = true;
				if (liste.Count > 0)
				{
					result.EmpileErreur(I.T("You should cancel the modifications before deleting this version|188"));
					return result;
				}
			}
			return result;
		}

		
		//---------------------------------------------
		/// <summary>
		/// Retourne toutes les données modifiées liées à cet objet (uniquement
		/// les données de modification)
		/// </summary>
        /// <remarks>
        /// Lors d'une modification de type "Ajout" ou "Suppression", la liste
        /// est vide, les données n'ayant pas été modifiées, mais simplement
        /// créées ou supprimées.
        /// </remarks>
		[DynamicChilds("Modifications",typeof(CVersionDonneesObjetOperation))]
		public CListeObjetsDonnees Modifications
		{
			get
			{
				CListeObjetsDonnees liste = ToutesLesOperations;
				liste.FiltrePrincipal = CFiltreData.GetAndFiltre ( liste.FiltrePrincipal, 
					new CFiltreData ( CVersionDonneesObjetOperation.c_champOperation+"=@1",
					(int)CTypeOperationSurObjet.TypeOperation.Modification ));
				return liste;
			}
		}

		//---------------------------------------------
	    /// <summary>
	    /// Retourne la liste des données alterées par la version
	    /// </summary>
        [RelationFille(typeof(CVersionDonneesObjetOperation), "VersionObjet")]
		[DynamicChilds("All operations", typeof(CVersionDonneesObjetOperation))]
		public CListeObjetsDonnees ToutesLesOperations
		{
			get
			{
				return GetDependancesListe(CVersionDonneesObjetOperation.c_nomTable, c_champId);
			}
		}

		//---------------------------------------------
		/// <summary>
		/// Annule toutes les modifications pour l'objet associé
		/// </summary>
		/// <returns></returns>
		public CResultAErreur AnnuleModificationsPrevisionnelles()
		{
			IVersionDonneesObjetServeur objServeur = (IVersionDonneesObjetServeur)GetLoader();
			return objServeur.AnnuleModificationsPrevisionnelles(Id);
		}


		public CResultAErreur AppliqueModifications(CObjetDonneeAIdNumerique objetDest)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (CVersionDonneesObjetOperation data in Modifications)
			{
				result = data.AppliqueModification(objetDest);
				if (!result)
					return result;
			}
			return result;
		}
	}

}
