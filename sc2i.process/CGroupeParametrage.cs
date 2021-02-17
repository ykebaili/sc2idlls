using System;
using System.Data;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.process;
using sc2i.multitiers.client;

namespace sc2i.process
{
    /// <summary>
    /// Un Groupe de paramétrage est un objet permettant de classer, regrouper,
    /// les <see cref="CAction">actions</see> et les <see cref="CEvenement">événements</see> par affinités
    /// </summary>
	[DynamicClass("parameter setting group")]
	[Table(CGroupeParametrage.c_nomTable, CGroupeParametrage.c_champId, false)]
	[ObjetServeurURI("CGroupeParametrageServeur")]
	public class CGroupeParametrage : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "SETTING_GROUPE";
		public const string c_champId = "SETGRP_ID";
		public const string c_champLibelle = "SETGRP_LABEL";
		
#if PDA
		public CGroupeParametrage( )
		:base()
		{
		}
#endif
		/// ////////////////////////////////////////////////
		public CGroupeParametrage ( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// ////////////////////////////////////////////////
		public CGroupeParametrage ( DataRow row )
			:base ( row )
		{
		}

		/// ////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T( "The @1 parameter setting group|268", Libelle);
			}
		}

		/// ////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// ////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		//-------------------------------------------------
        /// <summary>
        /// Libellé du groupe de paramétrage
        /// </summary>
		[TableFieldProperty(c_champLibelle, 255)]
		[DynamicField("Label")]
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

		/// ////////////////////////////////////////////////
		/// <summary>
		/// Retourne la liste des Actions appartenant à ce groupe
		/// </summary>
		[RelationFille ( typeof ( CProcessInDb ), "GroupeParametrage")]
		[DynamicChilds ("Actions", typeof ( CProcessInDb ) )]
		public CListeObjetsDonnees Actions
		{
			get
			{
				return GetDependancesListe ( CProcessInDb.c_nomTable, c_champId );
			}
		}

		/// ////////////////////////////////////////////////
		/// <summary>
		/// Retourne la liste des Evénements appartenant à ce groupe
		/// </summary>
		[RelationFille ( typeof ( CEvenement ), "GroupeParametrage")]
		[DynamicChilds("Evenements", typeof ( CEvenement  ))]
		public CListeObjetsDonnees Evenements
		{
			get
			{
				return GetDependancesListe ( CEvenement.c_nomTable, c_champId );
			}
		}
		
	}
}
