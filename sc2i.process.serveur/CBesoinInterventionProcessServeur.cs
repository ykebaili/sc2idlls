using System;
using System.Collections;
using System.Threading;
using System.Data;

using sc2i.data.dynamic;
using sc2i.data.serveur;
using sc2i.multitiers.server;
using sc2i.common;
using sc2i.data;
using sc2i.process;
using sc2i.multitiers.client;
using System.Collections.Generic;



namespace sc2i.ProcessEnExecution.serveur
{
	/// <summary>
	/// Description résumée de CFormulaireServeur.
	/// </summary>
	public class CBesoinInterventionProcessServeur : CObjetServeurAvecBlob, IBesoinInterventionProcessServeur
	{
#if PDA
		public CBesoinInterventionProcessServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CBesoinInterventionProcessServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CBesoinInterventionProcess.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CBesoinInterventionProcess);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CBesoinInterventionProcess besoin = (CBesoinInterventionProcess)objet;
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur SaveAll(CContexteSauvegardeObjetsDonnees contexteSauvegarde, System.Data.DataRowState etatsAPrendreEnCompte)
		{
			DataTable table = contexteSauvegarde.ContexteDonnee.Tables[GetNomTable()];
            List<DataRow> rowsAdded = new List<DataRow>();
			if(  table != null )
			{
                
				foreach ( DataRow row in new ArrayList(table.Rows) )
				{
					if ( row.RowState == DataRowState.Added || (row.RowState == DataRowState.Modified && !row[CSc2iDataConst.c_champIsDeleted].Equals(true)))
					{
                        rowsAdded.Add(row);
					}
					if ( row.RowState == DataRowState.Deleted || (row.RowState == DataRowState.Modified && row[CSc2iDataConst.c_champIsDeleted].Equals(true)))
					{
                        //TESTDBKEYTODO
						CDonneeNotificationBesoinIntervention notif = new CDonneeNotificationBesoinIntervention(
                            IdSession,
                            CDbKey.CreateFromStringValue((string)row[CBesoinInterventionProcess.c_champKeyUtilisateur, 
                            DataRowVersion.Original]),
                            (int)row[CBesoinInterventionProcess.c_champId,
                            DataRowVersion.Original],
                            (string)row[CBesoinInterventionProcess.c_champLibelle,
                            DataRowVersion.Original],
                            true);
						CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[] { notif });
					}
				}
			}
			CResultAErreur result = base.SaveAll(contexteSauvegarde, etatsAPrendreEnCompte );
            if (result)
            {
                foreach (DataRow row in rowsAdded)
                {
                    //TESTDBKEYTODO
                    CDonneeNotificationBesoinIntervention notif = new CDonneeNotificationBesoinIntervention(
                        IdSession, 
                        CDbKey.CreateFromStringValue((string)row[CBesoinInterventionProcess.c_champKeyUtilisateur]),
                        (int)row[CBesoinInterventionProcess.c_champId],
                        (string)row[CBesoinInterventionProcess.c_champLibelle],
                        false);
                    CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[] { notif });
                }
            }
            return result;
		}

		private class CLockRechercheInterventions
		{
		}

		private static CSessionClient m_sessionPourRechercheInterventions =null;
		public bool HasInterventions ( CDbKey keyUtilisateur )
		{
            if (keyUtilisateur == null)
                return false;
			lock ( typeof ( CLockRechercheInterventions ) )
			{
				if ( m_sessionPourRechercheInterventions == null )
				{
					m_sessionPourRechercheInterventions = CSessionClient.CreateInstance();
					CResultAErreur result = m_sessionPourRechercheInterventions.OpenSession(new sc2i.multitiers.server.CAuthentificationSessionServer(),
						I.T("Search for interventions|101"),
						ETypeApplicationCliente.Service );
					if ( !result )
						return false;
				}
				using ( CContexteDonnee contexte = new CContexteDonnee ( m_sessionPourRechercheInterventions.IdSession, true, false ) )
				{
					CListeObjetsDonnees listeInterventions = new CListeObjetsDonnees ( contexte, typeof (CBesoinInterventionProcess ) );
                    //TESTDBKEYTODO
					listeInterventions.Filtre = new CFiltreData ( 
						CBesoinInterventionProcess.c_champKeyUtilisateur+"=@1",
						keyUtilisateur.StringValue );
					return listeInterventions.CountNoLoad > 0;
				}
			}
		}


	}
}
