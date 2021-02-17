using System;
using System.IO;
using System.Collections;
using System.Threading;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.documents;
using sc2i.data.dynamic.loader;

using sc2i.multitiers.server;


namespace sc2i.documents.serveur
{
	/// <summary>
    /// Description résumée de CRelationCategorieGED_FormulaireServeur.
	/// </summary>
    public class CRelationCategorieGED_FormulaireServeur : CObjetDonneeServeurAvecCache
	{
   		//-------------------------------------------------------------------
		public CRelationCategorieGED_FormulaireServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationCategorieGED_Formulaire.c_nomTable;
		}
		
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CRelationCategorieGED_Formulaire);
		}

        //-------------------------------------------------------------------
        public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

	}
}
