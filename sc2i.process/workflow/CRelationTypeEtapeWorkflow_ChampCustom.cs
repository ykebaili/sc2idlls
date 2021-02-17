using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.process.workflow
{
    /// <summary>
    /// Relation entre un <see cref="CTypeEtapeWorkflow">Type d'étape de workflow</see> et un 
    /// <see cref="sc2i.data.dynamic.CChampCustom">Champ personnalisé</see>.
    /// </summary>
    [DynamicClass("Workflow type step / Custom field")]
    [ObjetServeurURI("CRelationTypeEtapeWorkflow_ChampCustomServeur")]
	[Table(CRelationTypeEtapeWorkflow_ChampCustom.c_nomTable, CRelationTypeEtapeWorkflow_ChampCustom.c_champId, true)]
	[FullTableSync]
    public class CRelationTypeEtapeWorkflow_ChampCustom : CRelationDefinisseurChamp_ChampCustom
	{
		public const string c_nomTable = "WKFSTPTP_CUSTOM_FIELD";
		public const string c_champId = "WKFSTPTP_CUSTFLD_ID";

		//-------------------------------------------------------------------
#if PDA
		public CRelationTypeEO_ChampCustom()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationTypeEtapeWorkflow_ChampCustom(CContexteDonnee ctx)
			: base(ctx)
		{
		}

		//-------------------------------------------------------------------
		public CRelationTypeEtapeWorkflow_ChampCustom(System.Data.DataRow row)
			: base(row)
		{
		}

        //-------------------------------------------------------------------
        /// <summary>
        /// Type d'étape de workflow objet de la relation
        /// </summary>
        [Relation(
            CTypeEtapeWorkflow.c_nomTable,
           CTypeEtapeWorkflow.c_champId,
           CTypeEtapeWorkflow.c_champId,
            true,
            false,
            true)]
        [DynamicField("Workflow step type")]
        public override IDefinisseurChampCustom Definisseur
        {
            get
            {
                return (IDefinisseurChampCustom)GetParent(CTypeEtapeWorkflow.c_champId, typeof(CTypeEtapeWorkflow));
            }
            set
            {
                SetParent(CTypeEtapeWorkflow.c_champId, (CTypeEtapeWorkflow)value);
            }
        }
	}
}
