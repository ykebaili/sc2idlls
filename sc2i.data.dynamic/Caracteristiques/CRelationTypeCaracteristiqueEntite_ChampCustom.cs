using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// OBSOLETE<br></br>
    /// Permet de connaitre tous les champs utilisées par les caractéristiques d'un type particulier
	/// </summary>
	[ObjetServeurURI("CRelationTypeCaracteristiqueEntite_ChampCustomServeur")]
	[Table(CRelationTypeCaracteristiqueEntite_ChampCustom.c_nomTable, CRelationTypeCaracteristiqueEntite_ChampCustom.c_champId, true)]
	[FullTableSync]
    [Unique(false,
        "Another association already exists for the relation Characteristic Type/Custom Field|147",
        CTypeCaracteristiqueEntite.c_champId,
        CChampCustom.c_champId)]
    [DynamicClass("Characterstic type / Field")]
    public class CRelationTypeCaracteristiqueEntite_ChampCustom : CRelationDefinisseurChamp_ChampCustom
	{
		public const string c_nomTable = "CHARAC_TYPE_CUSTOM_FIELD";
		public const string c_champId = "CHARACTYPE_CUSTFLD_ID";

		//-------------------------------------------------------------------
		public CRelationTypeCaracteristiqueEntite_ChampCustom(CContexteDonnee ctx)
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CRelationTypeCaracteristiqueEntite_ChampCustom(System.Data.DataRow row)
			:base(row)
		{
		}
		
        //-------------------------------------------
        /// <summary>
        /// Type de caractéristique définissant le champ utilisé
        /// </summary>
		[Relation(
            CTypeCaracteristiqueEntite.c_nomTable,
            CTypeCaracteristiqueEntite.c_champId,
            CTypeCaracteristiqueEntite.c_champId, 
            true, 
            false, 
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
