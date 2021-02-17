using System;
using System.Collections;
using System.Data;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CContexteDonneesSynchroMainServeur.
	/// </summary>
	public class CContexteDonneesSynchroMainServeur : CContexteDonneeServeur
	{
		public CContexteDonneesSynchroMainServeur( int nIdSession )
			:base ( nIdSession )
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

		///////////////////////////////////////////////////////////////////////////////
		protected override CResultAErreur MySaveAll(CContexteSauvegardeObjetsDonnees contexteSauvegarde)
		{
			CResultAErreur result = CResultAErreur.True;
			result = DoAddAndUpdateInDB( contexteSauvegarde );
			if ( result )
				result = DoDeleteInDB(contexteSauvegarde);
			/*if ( result )
				result = SaveBlobs();*/
			return result;
		}
	}
}
