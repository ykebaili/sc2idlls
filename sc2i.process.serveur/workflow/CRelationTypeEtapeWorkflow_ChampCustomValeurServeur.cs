using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.dynamic.loader;
using sc2i.process.workflow;

namespace sc2i.process.serveur.workflow
{
    /// <summary>
    /// CRelationTypeEtapeWorkflow_ChampCustomValeurServeur
    /// </summary>
    public class CRelationTypeEtapeWorkflow_ChampCustomValeurServeur : CRelationElementAChamp_ChampCustomServeur
    {
        public CRelationTypeEtapeWorkflow_ChampCustomValeurServeur(int nIdSession)
            : base(nIdSession)
        { }

        //-------------------------------------------------------------------
        public override string GetNomTable()
        {
            return CRelationTypeEtapeWorkflow_ChampCustomValeur.c_nomTable;
        }

        //-------------------------------------------------------------------
        public override Type GetTypeObjets()
        {
            return typeof(CRelationTypeEtapeWorkflow_ChampCustomValeur);
        }

    }
}
