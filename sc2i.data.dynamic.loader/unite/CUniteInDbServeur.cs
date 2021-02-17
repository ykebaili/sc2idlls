using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.multitiers.server;

namespace sc2i.data.dynamic.unite
{
	/// <summary>
	/// Description résumée de CUniteInDbServeur.
	/// </summary>
	public class CUniteInDbServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CUniteInDbServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CUniteInDbServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CUniteInDb.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CUniteInDb unite = (CUniteInDb)objet;

				if ( unite.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("Indicate a label for unit |20004"));

                if (unite.Classe == null)
                    result.EmpileErreur(I.T("Unit class @1 doesn't exists|20005", unite.ClassId));

			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CUniteInDb);
		}
		
		//-------------------------------------------------------------------
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			DataTable table = contexte.Tables[GetNomTable()];
            bool bHasChanges = false;
			foreach ( DataRow row in new ArrayList(table.Rows) )
			{
                if (row.RowState != DataRowState.Unchanged)
                {
                    bHasChanges = true;
                }
			}
            if ( bHasChanges )
            {
                CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[]{
                    new CDonneeNotificationModificationSystemeUnites(contexte.IdSession)});
            }
			return result;
		}

	}
}
