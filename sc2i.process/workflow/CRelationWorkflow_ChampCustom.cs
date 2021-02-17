using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;


namespace sc2i.process.workflow
{
    /// <summary>
    /// Relation entre un <see cref="CWorkflow">Workflow</see> et un 
    /// <see cref="sc2i.data.dynamic.CChampCustom">Champ personnalisé</see>.
    /// </summary>
    [DynamicClass("Workflow / Custom field")]
	[ObjetServeurURI("CRelationWorkflow_ChampCustomServeur")]
	[Table(CRelationWorkflow_ChampCustom.c_nomTable, CRelationWorkflow_ChampCustom.c_champId, true)]
	[FullTableSync]
    public class CRelationWorkflow_ChampCustom : CRelationElementAChamp_ChampCustom
	{
		public const string c_nomTable = "WKF_CUSTOM_FIELD";
		public const string c_champId = "WKF_CUSTFLD_ID";

		//-------------------------------------------------------------------
#if PDA
		public CRelationWorkflow_ChampCustom()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationWorkflow_ChampCustom(CContexteDonnee ctx)
			: base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CRelationWorkflow_ChampCustom(System.Data.DataRow row)
			: base(row)
		{
		}

		//-------------------------------------------------------------------
		public override Type GetTypeElementAChamps()
		{
			return typeof(CWorkflow);
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
        /// Workflow objet de la relation
        /// </summary>
		[Relation(
			CWorkflow.c_nomTable,
		   CWorkflow.c_champId,
		   CWorkflow.c_champId,
			true,
			true,
			true)]
		[DynamicField("Workflow")]
		public override IElementAChamps ElementAChamps
		{
			get
			{
				return (IElementAChamps)GetParent(CWorkflow.c_champId, typeof(CWorkflow));
			}
			set
			{
				SetParent(CWorkflow.c_champId, (CWorkflow)value);
			}
		}

		//-------------------------------------------------------------------
	}
}
