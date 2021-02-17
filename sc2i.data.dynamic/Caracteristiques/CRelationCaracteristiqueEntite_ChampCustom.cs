using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Valeurs de champs personnalisés pour une caractéristique
	/// </summary>
	[ObjetServeurURI("CRelationCaracteristiqueEntite_ChampCustomServeur")]
	[Table(CRelationCaracteristiqueEntite_ChampCustom.c_nomTable, CRelationCaracteristiqueEntite_ChampCustom.c_champId,true)]
	[FullTableSync]
    [DynamicClass("Characterstic / Custom field")]
	public class CRelationCaracteristiqueEntite_ChampCustom : CRelationElementAChamp_ChampCustom
	{
		public const string c_nomTable = "CHARAC_CUSTOM_FIELD";
		public const string c_champId = "CHARAC_CUSTFLD_ID";

		//-------------------------------------------------------------------
#if PDA
		public CRelationCaracteristiqueEntite_ChampCustom()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationCaracteristiqueEntite_ChampCustom(CContexteDonnee ctx)
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CRelationCaracteristiqueEntite_ChampCustom(System.Data.DataRow row)
			:base(row)
		{
		}

		//-------------------------------------------------------------------
		public override Type GetTypeElementAChamps()
		{
			return typeof(CCaracteristiqueEntite);
		}

		//-------------------------------------------------------------------
		public override string GetNomTable()
		{
			return c_nomTable;
		}
		//-------------------------------------------------------------------
		public override string GetChampId()
		{
			return c_champId;
		}

		
		//-------------------------------------------------------------------
		/// <summary>
		/// Caractéristique concernée par cette valeur de champ
		/// </summary>
        [Relation(
			CCaracteristiqueEntite.c_nomTable,
            CCaracteristiqueEntite.c_champId,
            CCaracteristiqueEntite.c_champId, 
			true, 
			true, 
			true)]
		[DynamicField("Characteristic")]
		public override IElementAChamps ElementAChamps
		{
			get
			{
				return (IElementAChamps)GetParent(CCaracteristiqueEntite.c_champId, typeof(CCaracteristiqueEntite));
			}
			set
			{
				SetParent(CCaracteristiqueEntite.c_champId, (CCaracteristiqueEntite)value);
			}
		}

		//-------------------------------------------------------------------
	}
}
