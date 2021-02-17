using System;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.data.dynamic.loader;
using sc2i.process.workflow;

namespace sc2i.process.serveur.workflow
{
	/// <summary>
	/// Description résumée de CRelationSite_ChampCustomServeur.
	/// </summary>
	public class CRelationWorkflow_ChampCustomServeur : CRelationElementAChamp_ChampCustomServeur
	{
		//-------------------------------------------------------------------
#if PDA
		public CRelationEO_ChampCustomServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationWorkflow_ChampCustomServeur(int nIdSession)
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationWorkflow_ChampCustom.c_nomTable;
		}
		
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
            return typeof(CRelationWorkflow_ChampCustom);
		}
		//-------------------------------------------------------------------
	}
}
