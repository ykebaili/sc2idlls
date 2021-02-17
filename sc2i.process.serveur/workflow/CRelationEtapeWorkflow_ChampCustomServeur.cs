using System;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.data.dynamic.loader;
using sc2i.process.workflow;


namespace sc2i.process.serveur.EtapeWorkflow
{
	/// <summary>
	/// Description résumée de CRelationSite_ChampCustomServeur.
	/// </summary>
	public class CRelationEtapeWorkflow_ChampCustomServeur : CRelationElementAChamp_ChampCustomServeur
	{
		//-------------------------------------------------------------------
#if PDA
		public CRelationEO_ChampCustomServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationEtapeWorkflow_ChampCustomServeur(int nIdSession)
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationEtapeWorkflow_ChampCustom.c_nomTable;
		}
		
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
            return typeof(CRelationEtapeWorkflow_ChampCustom);
		}
		//-------------------------------------------------------------------
	}
}
