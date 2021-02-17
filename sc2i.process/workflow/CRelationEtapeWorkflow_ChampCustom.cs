using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;


namespace sc2i.process.workflow
{
    /// <summary>
    /// Relation entre une <see cref="CEtapeWorkflow">Etape de workflow</see> et un 
    /// <see cref="sc2i.data.dynamic.CChampCustom">Champ personnalisé</see>.
    /// </summary>
    [DynamicClass("Workflow step/ Custom field")]
	[ObjetServeurURI("CRelationEtapeWorkflow_ChampCustomServeur")]
	[Table(CRelationEtapeWorkflow_ChampCustom.c_nomTable, CRelationEtapeWorkflow_ChampCustom.c_champId, true)]
	[FullTableSync]
    public class CRelationEtapeWorkflow_ChampCustom : CRelationElementAChamp_ChampCustom
	{
		public const string c_nomTable = "WKFSTP_CUSTOM_FIELD";
		public const string c_champId = "WKFSTP_CUSTFLD_ID";

		//-------------------------------------------------------------------
#if PDA
		public CRelationEtapeWorkflow_ChampCustom()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationEtapeWorkflow_ChampCustom(CContexteDonnee ctx)
			: base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CRelationEtapeWorkflow_ChampCustom(System.Data.DataRow row)
			: base(row)
		{
		}

		//-------------------------------------------------------------------
		public override Type GetTypeElementAChamps()
		{
			return typeof(CEtapeWorkflow);
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
        /// Etape de Workflow objet de la relation
        /// </summary>
		[Relation(
			CEtapeWorkflow.c_nomTable,
		   CEtapeWorkflow.c_champId,
		   CEtapeWorkflow.c_champId,
			true,
			true,
			true)]
		[DynamicField("Workflow step")]
		public override IElementAChamps ElementAChamps
		{
			get
			{
				return (IElementAChamps)GetParent(CEtapeWorkflow.c_champId, typeof(CEtapeWorkflow));
			}
			set
			{
				SetParent(CEtapeWorkflow.c_champId, (CEtapeWorkflow)value);
			}
		}

		//-------------------------------------------------------------------
	}
}
