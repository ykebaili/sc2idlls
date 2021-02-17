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
	/// Description résumée de CRelationEntiteOrganisationnelle_ChampCustomServeur.
	/// </summary>
	public class CRelationTypeEtapeWorkflow_ChampCustomServeur : CObjetDonneeServeurAvecCache
	{
		//-------------------------------------------------------------------
#if PDA
		public CRelationEntiteOrganisationnelle_ChampCustomServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationTypeEtapeWorkflow_ChampCustomServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationTypeEtapeWorkflow_ChampCustom.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CRelationTypeEtapeWorkflow_ChampCustom);
		}
		
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}
	}
}
