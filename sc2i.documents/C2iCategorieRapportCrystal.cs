using System;
using System.Data;

using sc2i.common;
using sc2i.data;

#if !PDA_DATA

namespace sc2i.documents
{
	/// <summary>
	/// Une catégorie de rapport est un organe de classement pour les rapports.
	/// </summary>
	[Table(C2iCategorieRapportCrystal.c_nomTable, C2iCategorieRapportCrystal.c_champId, true)]
	[ObjetServeurURI("C2iCategorieRapportCrystalServeur")]
	[FullTableSync]
	[DynamicClass("Crystal report category")]
	public class C2iCategorieRapportCrystal : CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "REPORT_CATEGORY";

		public const string c_champId = "REPCAT_ID";
		public const string c_champLibelle = "REPCAT_LABEL";

		//--------------------------------------------------------
		public C2iCategorieRapportCrystal( CContexteDonnee contexte)
			:base (contexte)
		{
		}

		//--------------------------------------------------------
		public C2iCategorieRapportCrystal ( DataRow row )
			:base ( row) 
		{
		}

		//--------------------------------------------------------
		public override string DescriptionElement
		{
			get
			{
				return I.T("The crystal report category @1|30001",Libelle);
			}
		}

		//--------------------------------------------------------
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		//--------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
		}
		//--------------------------------------------------------
        /// <summary>
        /// Libellé de la catégorie
        /// </summary>
		[TableFieldPropertyAttribute(c_champLibelle, 255)]
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
		//--------------------------------------------------------
	}
}
#endif