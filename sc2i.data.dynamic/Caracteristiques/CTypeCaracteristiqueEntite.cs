using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Type de caractéristique
	/// </summary>
    /// <seealso cref="CCaracteristiqueEntite">Caractéristique</seealso>
	[Table(CTypeCaracteristiqueEntite.c_nomTable, CTypeCaracteristiqueEntite.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CTypeCaracteristiqueEntiteServeur")]
	[DynamicClass("Characteristic type")]
	[Unique(false, "Charcteristic label should be unique|20016", CTypeCaracteristiqueEntite.c_champLibelle)]
	public class CTypeCaracteristiqueEntite : CObjetDonneeAIdNumeriqueAuto, IObjetALectureTableComplete, IDefinisseurChampCustomRelationObjetDonnee
	{
		public const string c_nomTable = "CHARACTERISTIC_TYPE";
		public const string c_champId = "CHARTYPE_ID";
		public const string c_champLibelle = "CHARTYPE_LABEL";
		public const string c_champDescription = "CHARTYPE_DESC";
		public const string c_champCodeRole = "CHARTYPE_ROLE";

		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CTypeCaracteristiqueEntite( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CTypeCaracteristiqueEntite ( DataRow row )
			:base(row)
		{
		}

		

		/// //////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Characteritic type @1|20014",Libelle);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Libellé du type de caractéristique
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

		/// //////////////////////////////////////////////////
		///<summary>
        ///Description du type de caractéristique
        ///</summary>
        ///<remarks>
        ///Peut être utilisé par l'administrateur pour décrire l'utilisation des caractéristiques de ce type
        /// </remarks>
        [TableFieldProperty(c_champDescription, 256)]
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





		#region IDefinisseurChampCustomRelationObjetDonnee Membres
		//-----------------------------------------------------------
		[RelationFille(typeof(CRelationTypeCaracteristiqueEntite_ChampCustom), "Definisseur")]
		public CListeObjetsDonnees RelationsChampsCustomListe
		{
			get { return GetDependancesListe(CRelationTypeCaracteristiqueEntite_ChampCustom.c_nomTable, c_champId); }
		}

		//-----------------------------------------------------------
		[RelationFille(typeof(CRelationTypeCaracteristiqueEntite_Formulaire), "Definisseur")]
		public CListeObjetsDonnees RelationsFormulairesListe
		{
			get { return GetDependancesListe(CRelationTypeCaracteristiqueEntite_Formulaire.c_nomTable, c_champId); }
		}

		//-------------------------------------------------------------------

		////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champCodeRole, 20)]
		public string CodeRole
		{
			get
			{
				return (string)Row[c_champCodeRole];
			}
			set
			{
				Row[c_champCodeRole] = value;
			}
		}

		////////////////////////////////////////////////////////////
		[DynamicField("Role")]
		public CRoleChampCustom Role
		{
			get
			{
				return CRoleChampCustom.GetRole((string)Row[c_champCodeRole]);
			}
			set
			{
				if (value == null)
					Row[c_champCodeRole] = DBNull.Value;
				else
					Row[c_champCodeRole] = value.CodeRole;
			}
		}

		////////////////////////////////////////////////////////////


		#endregion

		#region IDefinisseurChampCustom Membres


		/// /////////////////////////////////////////////
		public IRelationDefinisseurChamp_ChampCustom[] RelationsChampsCustomDefinis
		{
			get
			{
				return (IRelationDefinisseurChamp_ChampCustom[])RelationsChampsCustomListe.ToArray(typeof(IRelationDefinisseurChamp_ChampCustom));
			}
		}

		/// /////////////////////////////////////////////
		public IRelationDefinisseurChamp_Formulaire[] RelationsFormulaires
		{
			get
			{
				return (IRelationDefinisseurChamp_Formulaire[])RelationsFormulairesListe.ToArray(typeof(IRelationDefinisseurChamp_Formulaire));
			}
		}

		/// /////////////////////////////////////////////
		public CRoleChampCustom RoleChampCustomDesElementsAChamp
		{
			get
			{
				return CRoleChampCustom.GetRole(CCaracteristiqueEntite.c_roleChampCustom);
			}
		}

		/// /////////////////////////////////////////////
		public CChampCustom[] TousLesChampsAssocies
		{
			get
			{
				Hashtable tableChamps = new Hashtable();
				FillHashtableChamps(tableChamps);
				CChampCustom[] liste = new CChampCustom[tableChamps.Count];
				int nChamp = 0;
				foreach (CChampCustom champ in tableChamps.Values)
					liste[nChamp++] = champ;
				return liste;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Remplit une hashtable IdChamp->Champ
		/// avec tous les champs liés.(hiérarchique)
		/// </summary>
		/// <param name="tableChamps">HAshtable à remplir</param>
		private void FillHashtableChamps(Hashtable tableChamps)
		{
			foreach (IRelationDefinisseurChamp_ChampCustom relation in RelationsChampsCustomDefinis)
				tableChamps[relation.ChampCustom.Id] = relation.ChampCustom;
			foreach (IRelationDefinisseurChamp_Formulaire relation in RelationsFormulaires)
			{
				foreach (CRelationFormulaireChampCustom relFor in relation.Formulaire.RelationsChamps)
					tableChamps[relFor.Champ.Id] = relFor.Champ;
			}
		}

		#endregion
	}
		
}
