using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.dynamic;
using sc2i.data;
using System.Data;
using sc2i.common;

namespace sc2i.process.workflow
{
    [DynamicClass("Workflow Step Type / Custom field value")]
    [ObjetServeurURI("CRelationTypeEtapeWorkflow_ChampCustomValeurServeur")]
    [Table(CRelationTypeEtapeWorkflow_ChampCustomValeur.c_nomTable, CRelationTypeEtapeWorkflow_ChampCustomValeur.c_champId, true)]
    public class CRelationTypeEtapeWorkflow_ChampCustomValeur : CRelationElementAChamp_ChampCustom
    {
        public const string c_nomTable = "STEP_TYPE_CUSTFIELD_VAL";
        public const string c_champId = "STEPTYP_CUSTFLD_VAL_ID";

        public CRelationTypeEtapeWorkflow_ChampCustomValeur(CContexteDonnee ctx)
            :base(ctx)
        {

        }

        public CRelationTypeEtapeWorkflow_ChampCustomValeur(DataRow row)
            :base(row)
        {

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

        /// <summary>
        /// Type d'Etape de la relation
        /// </summary>
        [Relation(
        CTypeEtapeWorkflow.c_nomTable,
        CTypeEtapeWorkflow.c_champId,
        CTypeEtapeWorkflow.c_champId,
        true,
        true,
        true)]
        [DynamicField("Step Type")]
        public override IElementAChamps ElementAChamps
        {
            get
            {
                return (IElementAChamps)GetParent(CTypeEtapeWorkflow.c_champId, typeof(CTypeEtapeWorkflow));
            }
            set
            {
                SetParent(CTypeEtapeWorkflow.c_champId, (CTypeEtapeWorkflow)value);
            }
        }

        public override Type GetTypeElementAChamps()
        {
            return typeof(CTypeEtapeWorkflow);
        }
    }
}
