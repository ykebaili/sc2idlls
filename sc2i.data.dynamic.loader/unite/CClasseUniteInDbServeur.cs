using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.data.dynamic.unite
{
	/// <summary>
	/// Description résumée de CClasseUniteInDbServeur.
	/// </summary>
	public class CClasseUniteInDbServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CClasseUniteInDbServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CClasseUniteInDbServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CClasseUniteInDb.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CClasseUniteInDb classe = (CClasseUniteInDb)objet;

				if ( classe.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("Indicate a label for unit class|20002"));

                if (classe.GlobalId.Trim() == "")
                    result.EmpileErreur(I.T("Indicate a globalId for unit class|20003"));

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
			return typeof(CClasseUniteInDb);
		}
		
		//-------------------------------------------------------------------
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
            CResultAErreur result = CResultAErreur.True;
            DataTable table = contexte.Tables[GetNomTable()];
            bool bHasChanges = false;
            foreach (DataRow row in new ArrayList(table.Rows))
            {
                if (row.RowState != DataRowState.Unchanged)
                {
                    bHasChanges = true;
                }
            }
            if (bHasChanges)
            {
                CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[]{
                    new CDonneeNotificationModificationSystemeUnites(contexte.IdSession)});
            }
            return result;
		}

	}
}
