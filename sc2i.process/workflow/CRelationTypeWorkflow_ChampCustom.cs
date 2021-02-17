using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.process.workflow
{
    /// <summary>
    /// Relation entre un <see cref="CTypeWorkflow">Type de workflow</see> et un 
    /// <see cref="sc2i.data.dynamic.CChampCustom">Champ personnalisé</see>.
    /// </summary>
    [DynamicClass("Workflow type / Custom field")]
    [ObjetServeurURI("CRelationTypeWorkflow_ChampCustomServeur")]
	[Table(CRelationTypeWorkflow_ChampCustom.c_nomTable, CRelationTypeWorkflow_ChampCustom.c_champId, true)]
	[FullTableSync]
    public class CRelationTypeWorkflow_ChampCustom : CRelationDefinisseurChamp_ChampCustom
	{
		public const string c_nomTable = "WKFTP_CUSTOM_FIELD";
		public const string c_champId = "WKFTP_CUSTFLD_ID";

		//-------------------------------------------------------------------
#if PDA
		public CRelationTypeEO_ChampCustom()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationTypeWorkflow_ChampCustom(CContexteDonnee ctx)
			: base(ctx)
		{
		}

		//-------------------------------------------------------------------
		public CRelationTypeWorkflow_ChampCustom(System.Data.DataRow row)
			: base(row)
		{
		}

        //-------------------------------------------------------------------
        /// <summary>
        /// Type de workflow objet de la relation
        /// </summary>
		[Relation(
			CTypeWorkflow.c_nomTable,
		   CTypeWorkflow.c_champId,
		   CTypeWorkflow.c_champId,
			true,
			false,
			true)]
        [DynamicField("Workflow type")]
		public override IDefinisseurChampCustom Definisseur
		{
			get
			{
				return (IDefinisseurChampCustom)GetParent(CTypeWorkflow.c_champId, typeof(CTypeWorkflow));
			}
			set
			{
				SetParent(CTypeWorkflow.c_champId, (CTypeWorkflow)value);
			}
		}
	}
}
