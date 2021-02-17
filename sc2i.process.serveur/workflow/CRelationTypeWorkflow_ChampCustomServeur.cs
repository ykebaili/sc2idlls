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
	/// Description résumée de CRelationEntiteOrganisationnelle_ChampCustomServeur.
	/// </summary>
	public class CRelationTypeWorkflow_ChampCustomServeur : CObjetDonneeServeurAvecCache
	{
		//-------------------------------------------------------------------
#if PDA
		public CRelationEntiteOrganisationnelle_ChampCustomServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationTypeWorkflow_ChampCustomServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationTypeWorkflow_ChampCustom.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CRelationTypeWorkflow_ChampCustom);
		}
		
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}
	}
}
