using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.serveur;
using sc2i.common;
using sc2i.process.workflow;
using System.Data;
using System.Collections;
using sc2i.multitiers.client;
using sc2i.data;
using System.Threading;
using sc2i.process.serveur.workflow;

namespace sc2i.process.serveur.EtapeWorkflow
{
    public class CEtapeWorkflowServeur : CObjetServeur
    {
        private const string c_constEtapesADemarre = "START_STEPS";
        private const string c_constEtapesTraitees = "ETAPES_TRAITEES";
        /// //////////////////////////////////////////////////
        public CEtapeWorkflowServeur(int nIdSession)
			:base(nIdSession)
		{
		}

        /// //////////////////////////////////////////////////
        public override string GetNomTable()
        {
            return CEtapeWorkflow.c_nomTable;
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur VerifieDonnees(sc2i.data.CObjetDonnee objet)
        {
            CResultAErreur result = CResultAErreur.True;
            CEtapeWorkflow etapeWorkflow = objet as CEtapeWorkflow;
            if ( etapeWorkflow != null )
            {
                
            }
            return result;
        }

        /// //////////////////////////////////////////////////
        public override Type GetTypeObjets()
        {
            return typeof(CEtapeWorkflow);
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur TraitementAvantSauvegarde(sc2i.data.CContexteDonnee contexte)
        {
            CResultAErreur result =  base.TraitementAvantSauvegarde(contexte);
            if (!result)
                return result;



            DataTable table = contexte.Tables[GetNomTable()];
            if (table == null)
                return result;

            //Stocke la liste des étapes pour lesquelles le traitement a déjà été fait dans cette sauvegarde
            //Pour éviter que plusieurs appels à TraitementAvantSauvegarde ne déclenche plusieurs fois la même étape !
            HashSet<int> lstEtapesTraitees = table.ExtendedProperties[c_constEtapesTraitees] as HashSet<int>;
            if (lstEtapesTraitees == null)
            {
                lstEtapesTraitees = new HashSet<int>();
                table.ExtendedProperties[c_constEtapesTraitees] = lstEtapesTraitees;
            }

            CEtapeWorkflow etape = null;

            bool bHasLancéDesEtapes = true;
            
            ArrayList lst;
            do
            {
                bHasLancéDesEtapes = false;
                lst = new ArrayList(table.Rows);
                //Identifie les étapes terminées et lance la suite
                foreach (DataRow rowTerminee in lst)
                {
                    if (rowTerminee.RowState == DataRowState.Modified)
                    {
                        etape = new CEtapeWorkflow(rowTerminee);
                        if (!lstEtapesTraitees.Contains(etape.Id))
                        {
                            etape.VersionToReturn = DataRowVersion.Original;
                            EEtatEtapeWorkflow oldEtat = (EEtatEtapeWorkflow)etape.EtatCode;
                            etape.VersionToReturn = DataRowVersion.Current;
                            EEtatEtapeWorkflow etat = (EEtatEtapeWorkflow)etape.EtatCode;
                            if (etat == EEtatEtapeWorkflow.Terminée && oldEtat != EEtatEtapeWorkflow.Terminée)
                            {
                                lstEtapesTraitees.Add(etape.Id);
                                //Il faut lancer la suite de cette étape
                                result = etape.Workflow.PrépareSuiteEtapeInCurrentContext(etape);
                                bHasLancéDesEtapes = true;
                            }
                        }
                    }
                }
            }
            while (bHasLancéDesEtapes);

            lst = new ArrayList(table.Rows);
            List<DataRow> lstEtapesADemarrer = table.ExtendedProperties[c_constEtapesADemarre] as List<DataRow>;
            if ( lstEtapesADemarrer == null )
                lstEtapesADemarrer = new List<DataRow>();
            foreach (DataRow row in lst)
            {
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    etape = new CEtapeWorkflow(row);
                    
                    if (etape.DateDebut != null && etape.DateFin == null &&
                        etape.EtatCode == (int)EEtatEtapeWorkflow.ADemarrer)
                    {
                        if (!lstEtapesTraitees.Contains ( etape.Id ) )
                        {
                            lstEtapesTraitees.Add ( etape.Id );
                            //etape.EtatCode = (int)EEtatEtapeWorkflow.Démarrée;
                            lstEtapesADemarrer.Add( row );
                        }
                    }
                }
            }
            if ( lstEtapesADemarrer.Count > 0 )
                table.ExtendedProperties[c_constEtapesADemarre] = lstEtapesADemarrer;
            
            return result;
        }

        /// //////////////////////////////////////////////////
        public override CResultAErreur TraitementApresSauvegarde(sc2i.data.CContexteDonnee contexte, bool bOperationReussie)
        {
            CResultAErreur result = CResultAErreur.True;
            result =  base.TraitementApresSauvegarde(contexte, bOperationReussie);
            if (!result)
                return result;
            DataTable table = contexte.Tables[GetNomTable()];
            List<DataRow> lstRowsEtapesADemarrer = table == null ? null : table.ExtendedProperties[c_constEtapesADemarre] as List<DataRow>;
                
            if (lstRowsEtapesADemarrer != null && lstRowsEtapesADemarrer.Count > 0)
            {
                int? nIdSessionMain = IdSession;
                CDbKey keyUtilisateur = null;
                CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
                if (session != null)
                {
                    IInfoUtilisateur info = session.GetInfoUtilisateur();
                    //TESTDBKEYOK
                    if (info != null)
                        keyUtilisateur = info.KeyUtilisateur;
                    CSousSessionClient sousSession = session as CSousSessionClient;
                    if (sousSession != null)
                    {
                        nIdSessionMain = sousSession.RootSession.IdSession;
                    }
                }
                List<CParametresStartEtapeInPile> lstToStart = new List<CParametresStartEtapeInPile>();
                foreach (DataRow row in lstRowsEtapesADemarrer)
                {
                    //Ancien mode, avant 13/9/2013
                    //RunEtapeInThread ( (int)row[CEtapeWorkflow.c_champId]);
                    //TESTDBKEYOK
                    lstToStart.Add(new CParametresStartEtapeInPile(
                        nIdSessionMain, (int)row[CEtapeWorkflow.c_champId], keyUtilisateur));
                }
                CPileExecutionEtapes.AddEtapesADemarrer(lstToStart.ToArray());
            }
            return result;
        }

        private class CParametresStartEtape
        {
            public int IdEtape;
            public int IdSessionMain;
            public CParametresStartEtape(int nIdSessionMain, int nIdEtape)
            {
                IdSessionMain = nIdSessionMain;
                IdEtape = nIdEtape;
            }
        }
        /// //////////////////////////////////////////////////
        ///Démarre une étape dans un thread séparé
        public void RunEtapeInThread  (int nIdEtape )
        {
            Thread th = new Thread(StartEtapeInThread);
            int nIdSession = IdSession;
            //Lance l'étape dans la session root, car la sousSEssion peut être fermée avant que
            //L'étape ne soit lancée !
            CSousSessionClient session = CSessionClient.GetSessionForIdSession(IdSession) as CSousSessionClient;
            if (session != null)
                nIdSession = session.RootSession.IdSession;

            th.Start ( new CParametresStartEtape ( nIdSession, nIdEtape  ));
        }

        /// //////////////////////////////////////////////////
        private void StartEtapeInThread(object objParam)
        {
            CParametresStartEtape parametre = objParam as CParametresStartEtape;
            if (parametre != null)
                RunEtape(parametre.IdSessionMain, parametre.IdEtape);
        }

        private class CLockerStartEtape { }

        /// //////////////////////////////////////////////////
        ///Démarre une étape. 
        ///Attention, un étape ne peut démarrer que si elle n'est pas déjà démarrée
        public void RunEtape(int nIdSessionMain, int nIdEtape)
        {
            CResultAErreur result = CResultAErreur.True;
            CDbKey keyUser = null;
            //Attend la fin des transactions en cours pour la session principale
            IDatabaseConnexion cnx = null;
            do
            {
                CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
                if (session != null && session.IsConnected)
                {
                    IInfoUtilisateur info = session.GetInfoUtilisateur();
                    //TESTDBKEYTODO
                    if ( info != null )
                        keyUser = info.KeyUtilisateur;
                    try
                    {
                        cnx = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, typeof(CEtapeWorkflow));
                    }
                    catch //En cas d'erreur, c'est probablement que la session a été fermée, du coup, on peut y aller !
                    {
                        cnx = null;
                    }
                    System.Threading.Thread.Sleep(50);
                }
                else
                    cnx = null;
            }
            while (cnx != null && cnx.IsInTrans());
            lock (typeof(CLockerStartEtape))//S'assure que deux étapes ne démarrent pas en même temps !
            {
                List<CDonneeNotificationWorkflow> lstNotifications = new List<CDonneeNotificationWorkflow>();

                CAuthentificationSessionProcess auth = new CAuthentificationSessionProcess();

                using (CSessionClient sousSession = CSessionClient.CreateInstance())
                {
                    try
                    {
                        Console.WriteLine("Thread : " + System.Diagnostics.Process.GetCurrentProcess().Threads.Count);
                        sousSession.OpenSession(auth, "Workflow step " + nIdEtape, ETypeApplicationCliente.Process);
                        //TESTDBKEYTODO
                        if (keyUser != null)
                            sousSession.ChangeUtilisateur(keyUser);
                        using (CContexteDonnee ctx = new CContexteDonnee(sousSession.IdSession, true, true))
                        {
                            CEtapeWorkflow etape = new CEtapeWorkflow(ctx);
                            int nWaiter = 10;
                            while (!etape.ReadIfExists(nIdEtape) && nWaiter > 0)
                            {
                                //On ne trouve pas l'étape, c'est peut être que l'écriture en base n'est pas completement terminée
                                //On va retenter toutes les 2 secondes pendant 20 secondes, si elle n'existe jamais,
                                //c'est qu'il y a eu suppression (ou au moins non commit).
                                nWaiter--;
                                Thread.Sleep(2000);
                            }
                            if (etape.ReadIfExists(nIdEtape))
                            {
                                result = etape.InternalSetInfosDemarrageInCurrentContext();
                                etape.EtatCode = (int)EEtatEtapeWorkflow.Démarrée;
                                result = ctx.SaveAll(true);
                                if (result)
                                    result = etape.InternalRunAndSaveifOk();
                                if (result && etape.CodeAffectations.Length > 0 && etape.DateFin == null)
                                {
                                    CDonneeNotificationWorkflow donneeWorkflow = new CDonneeNotificationWorkflow(
                                        nIdSessionMain,
                                        etape.Id,
                                        etape.Libelle,
                                        etape.CodeAffectations,
                                        etape.TypeEtape.ExecutionAutomatique);
                                    lstNotifications.Add(donneeWorkflow);
                                    // Déclenche l'evenement spécifique au démarrage de l'étape
                                    result = etape.EnregistreEvenement(CEtapeWorkflow.c_codeEvenementOnRunStep, true);

                                }
                                if (!result)
                                {
                                    NoteErreurSurEtape(etape, result.Erreur.ToString());
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    finally
                    {
                        sousSession.CloseSession();
                    }
                }
                if (lstNotifications != null)
                    CEnvoyeurNotification.EnvoieNotifications(lstNotifications.ToArray());
            }
        }

        //------------------------------------------------------------------------
        private void NoteErreurSurEtape(CEtapeWorkflow etape, string strMessage)
        {
            using (CContexteDonnee ctx = new CContexteDonnee(etape.ContexteDonnee.IdSession, true, false))
            {
                etape = etape.GetObjetInContexte(ctx) as CEtapeWorkflow;
                etape.EtatCode = (int)EEtatEtapeWorkflow.Erreur;
                etape.LastError = strMessage;
                etape.DateFin = null;
                ctx.SaveAll(false);
            }
        }

                



    }
}
