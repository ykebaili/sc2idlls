using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// OBSOLETE<br></br>
    /// Permet de connaitre tous les formulaires associés aux caractéristiques d'un type particulier
	/// </summary>
	[ObjetServeurURI("CRelationTypeCaracteristiqueEntite_FormulaireServeur")]
	[Table(CRelationTypeCaracteristiqueEntite_Formulaire.c_nomTable, CRelationTypeCaracteristiqueEntite_Formulaire.c_champId,true)]
	[FullTableSync]
    [Unique(false,
        "Another association already exist for the relation Characteristic Type/Custom Form|148",
        CTypeCaracteristiqueEntite.c_champId,
        CFormulaire.c_champId)]
    [DynamicClass("Characteristic type / Form")]
    public class CRelationTypeCaracteristiqueEntite_Formulaire : CRelationDefinisseurChamp_Formulaire
	{
		public const string c_nomTable = "CHARAC_TYPE_FORM";
		public const string c_champId = "CHARAC_TYPE_FORM_ID";
		
        
		//-------------------------------------------------------------------
		public CRelationTypeCaracteristiqueEntite_Formulaire(CContexteDonnee ctx)
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CRelationTypeCaracteristiqueEntite_Formulaire(System.Data.DataRow row)
			:base(row)
		{
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Type de caractéristique
		/// </summary>
        [Relation(
            CTypeCaracteristiqueEntite.c_nomTable,
            CTypeCaracteristiqueEntite.c_champId,
            CTypeCaracteristiqueEntite.c_champId,
            true,
            true,
            true)]
        [DynamicField("Characteristic type")]
		public override IDefinisseurChampCustom Definisseur
		{
			get
			{
                return (IDefinisseurChampCustom)GetParent(CTypeCaracteristiqueEntite.c_champId, typeof(CTypeCaracteristiqueEntite));
			}
			set
			{
                SetParent(CTypeCaracteristiqueEntite.c_champId, (CTypeCaracteristiqueEntite)value);
			}
		}
	}
}
