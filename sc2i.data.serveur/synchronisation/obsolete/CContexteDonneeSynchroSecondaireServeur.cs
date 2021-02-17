using System;
using System.Collections;
using System.Data;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CContexteDonneeSynchroSecondaireServeur.
	/// </summary>
	public class CContexteDonneeSynchroSecondaireServeur : CContexteDonneeServeur
	{
		///////////////////////////////////////////////////////////////////////////////
		public CContexteDonneeSynchroSecondaireServeur( int nIdSession )
			:base ( nIdSession )
		{
		}

		///////////////////////////////////////////////////////////////////////////////
		protected override void PrepareLoader ( IObjetServeur loader )
		{
			loader.DesactiverIdentifiantAutomatique = true;
			loader.DesactiverContraintes = true;
		}

		///////////////////////////////////////////////////////////////////////////////
		/// PAs de traitement avant sauvegarde pour le contexte secondaire, à priori, ils ont déjà
		/// été faits sur le contexte principal

	/*	///////////////////////////////////////////////////////////////////////////////
		///Delete avant de modifier. C'est possible car on a désactivé les contraintes
		protected override CResultAErreur MySaveAll(CContexteSauvegardeObjetsDonnees contexteSauvegarde)
		{
			CResultAErreur result = CResultAErreur.True;

			//Garde le contexte avant add et update
			DataSet ds = null;
			if ( contexteSauvegarde.DataSet.HasChanges(DataRowState.Deleted) )
#if PDA
				ds = contexte.GetChanges(DataRowState.Deleted);
#else
				ds = ((DataSet)contexteSauvegarde.DataSet).GetChanges(DataRowState.Deleted);
#endif
			//Fait le delete avant !!
			if ( result )
				result = DoDeleteInDB(ds, donneeNotification);
			result = DoAddAndUpdateInDB(contexte, donneeNotification);
			
			return result;
		}*/
	}
}
