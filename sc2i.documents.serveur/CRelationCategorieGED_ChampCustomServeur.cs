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
    /// Description résumée de CRelationCategorieGED_ChampCustomServeur.
	/// </summary>
    public class CRelationCategorieGED_ChampCustomServeur : CObjetDonneeServeurAvecCache
	{
   		//-------------------------------------------------------------------
		public CRelationCategorieGED_ChampCustomServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationCategorieGED_ChampCustom.c_nomTable;
		}
		
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CRelationCategorieGED_ChampCustom);
		}

        //-------------------------------------------------------------------
        public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            return result;
        }

	}
}
