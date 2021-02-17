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
	/// Description résumée de CRelationEntiteOrganisationnelle_FormulaireServeur.
	/// </summary>
	public class CRelationTypeEtapeWorkflow_FormulaireServeur : CObjetDonneeServeurAvecCache
	{
		//-------------------------------------------------------------------
#if PDA
		public CRelationEntiteOrganisationnelle_FormulaireServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationTypeEtapeWorkflow_FormulaireServeur(int nIdSession)
			:base ( nIdSession )
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationTypeEtapeWorkflow_Formulaire.c_nomTable;
		}
		
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CRelationTypeEtapeWorkflow_Formulaire);
		}

		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}
	}
}
