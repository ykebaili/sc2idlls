using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.multitiers.client;
using sc2i.data.serveur;
using sc2i.data;
using sc2i.process.workflow;
using sc2i.common;
using System.Data;
using System.Timers;

namespace sc2i.process.serveur.workflow
{
    //-----------------------------------------------------------------------------------------
    public class CParametresStartEtapeInPile
    {
        public int IdEtape;
        public int? IdSessionMain;
        public CDbKey KeyUtilisateur;
        public DateTime DateStart;

        //-----------------------------------------------------------------------------------------
        public CParametresStartEtapeInPile(int? nIdSessionMain, int nIdEtape, CDbKey keyUtilisateur)
        {
            IdSessionMain = nIdSessionMain;
            IdEtape = nIdEtape;
            //TESTDBKEYTODO
            KeyUtilisateur = keyUtilisateur;
            DateStart = DateTime.Now;
        }

        //-----------------------------------------------------------------------------------------
        public CParametresStartEtapeInPile(
            int? nIdSessionMain,
            int nIdEtape,
            CDbKey keyUtilisateur,
            DateTime dtDemarrage)
            : this(nIdSessionMain, nIdEtape, keyUtilisateur)
        {
            DateStart = dtDemarrage;
        }
    }

    //-----------------------------------------------------------------------------------------
    [AutoExec("Autoexec", AutoExecAttribute.BackGroundService)]
    public class CPileExecutionEtapes
    {
        private CSessionClient m_session = null;
        private List<CParametresStartEtapeInPile> m_listeEtapes = new List<CParametresStartEtapeInPile>();


        private static CPileExecutionEtapes m_instance = null;

        //Le timer est utilisé en cas de gros pépin, si jamais la boucle d'étapes s'arrête,
        //le timer se charge de la redémarrer
        private const int c_nDelaiTimerLooper = 1 * 10 * 1000; // 10 secondes
        private Timer m_timerLooper = null;

        private bool m_bLoopEtapesEnCours = false;

        private DateTime? m_lastDateLookInDb = null;
        private const int c_nDelailLookInDb = 10 * 60 * 1000; // Vérifie dans la base toutes les 10 minutes

        private class CLockerEtapes { }

        //------------------------------------------------------------------------
        public static void Autoexec()
        {
            CPileExecutionEtapes pile = Instance;
        }

        //------------------------------------------------------------------------
        private CPileExecutionEtapes()
        {
            m_timerLooper = new Timer(c_nDelaiTimerLooper);
            m_timerLooper.Elapsed += new ElapsedEventHandler(m_timerSecours_Elapsed);
            m_timerLooper.Start();
        }

        //------------------------------------------------------------------------
        private void m_timerSecours_Elapsed(object sender, ElapsedEventArgs e)
        {
            C2iEventLog.WriteInfo("DEBUG_TODO - m_timerSecours_Elapsed - m_bLoopEtapesEnCours" + m_bLoopEtapesEnCours);
            if (!m_bLoopEtapesEnCours)
                LoopEtapes();
        }

        //------------------------------------------------------------------------
        public static CPileExecutionEtapes Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new CPileExecutionEtapes();
                return m_instance;
            }
        }

        //------------------------------------------------------------------------
        private static bool IsTransactionTerminee(int nIdSession)
        {

            //S'assure que la transaction est terminée pour cette session
            CSessionClient session = CSessionClient.GetSessionForIdSession(nIdSession);
            IDatabaseConnexion cnx = null;
            if (session != null && session.IsConnected)
            {
                try
                {
                    cnx = CSc2iDataServer.GetInstance().GetDatabaseConnexion(nIdSession, typeof(CEtapeWorkflow));
                }
                catch //En cas d'erreur, c'est probablement que la session a été fermée, du coup, on peut y aller !
                {
                    cnx = null;
                }
            }
            else
                cnx = null;
            return cnx == null || !cnx.IsInTrans();
        }



        //------------------------------------------------------------------------
        //Démarre la prochaine étape de la liste
        private void StartNextEtape()
        {
            C2iEventLog.WriteInfo("DEBUG_TODO - StartNextEtape()");
            CParametresStartEtapeInPile parametreToRun = null;
            lock (typeof(CLockerEtapes))
            {
                if (m_listeEtapes.Count == 0)
                    return;
                m_listeEtapes.Sort((x, y) => x.DateStart.CompareTo(y.DateStart));
                Dictionary<int, bool?> dicSessionsPrets = new Dictionary<int, bool?>();
                foreach (CParametresStartEtapeInPile parametre in m_listeEtapes)
                {
                    bool? bIsPrete = null;
                    if (parametre.IdSessionMain != null)
                    {
                        if (!dicSessionsPrets.TryGetValue(parametre.IdSessionMain.Value, out bIsPrete))
                        {
                            bIsPrete = IsTransactionTerminee(parametre.IdSessionMain.Value);
                            dicSessionsPrets[parametre.IdSessionMain.Value] = bIsPrete;
                        }
                    }
                    else
                        bIsPrete = true;
                    if (bIsPrete.Value)
                    {
                        //Vérifie que l'étape existe
                        C2iRequeteAvancee req = new C2iRequeteAvancee();
                        req.TableInterrogee = CEtapeWorkflow.c_nomTable;
                        req.ListeChamps.Add(new C2iChampDeRequete("ID",
                            new CSourceDeChampDeRequete(CEtapeWorkflow.c_champId),
                            typeof(int),
                            OperationsAgregation.None,
                            true));
                        req.FiltreAAppliquer = new CFiltreData(CEtapeWorkflow.c_champId + "=@1",
                            parametre.IdEtape);
                        C2iEventLog.WriteInfo("DEBUG_TODO - StartNextEtape() - Vérifie que l'étape existe Id = " + parametre.IdEtape);

                        CResultAErreur result = req.ExecuteRequete(m_session.IdSession);
                        if (result && result.Data is DataTable && ((DataTable)result.Data).Rows.Count > 0)
                        {
                            C2iEventLog.WriteInfo("DEBUG_TODO - StartNextEtape() - OK l'étape existe Id = " + parametre.IdEtape);
                            parametreToRun = parametre;
                            break;
                        }
                    }
                }
                if (parametreToRun != null)
                    m_listeEtapes.Remove(parametreToRun);
            }
            if (parametreToRun != null)
                ExecuteEtape(parametreToRun);
        }

        //------------------------------------------------------------------------
        private void ExecuteEtape(CParametresStartEtapeInPile parametreStart)
        {
            C2iEventLog.WriteInfo("DEBUG_TODO - ExecuteEtape()");
            List<CDonneeNotificationWorkflow> lstNotifications = new List<CDonneeNotificationWorkflow>();

            //TESTDBKEYTODO
            if (parametreStart.KeyUtilisateur != null)
                m_session.ChangeUtilisateur(parametreStart.KeyUtilisateur);
            try
            {
                using (CContexteDonnee ctx = new CContexteDonnee(m_session.IdSession, true, false))
                {
                    CResultAErreur result = CResultAErreur.True;
                    CEtapeWorkflow etape = new CEtapeWorkflow(ctx);
                    C2iEventLog.WriteInfo("DEBUG_TODO - ExecuteEtape() - etape.ReadIfExists(" + parametreStart.IdEtape + ")");
                    if (etape.ReadIfExists(parametreStart.IdEtape))
                    {
                        C2iEventLog.WriteInfo("DEBUG_TODO - ExecuteEtape() OK étape existe Id = " + parametreStart.IdEtape);
                        result = etape.InternalSetInfosDemarrageInCurrentContext();
                        C2iEventLog.WriteInfo("DEBUG_TODO - etape.InternalSetInfosDemarrageInCurrentContext() Id = " + parametreStart.IdEtape);
                        etape.EtatCode = (int)EEtatEtapeWorkflow.Démarrée;
                        result = ctx.SaveAll(true);
                        C2iEventLog.WriteInfo("DEBUG_TODO - ctx.SaveAll(true) Id = " + parametreStart.IdEtape + " - result = " + result.Result);

                        if (result)
                        {
                            C2iEventLog.WriteInfo("DEBUG_TODO - before InternalRunAndSaveifOk() Id = " + parametreStart.IdEtape);
                            result = etape.InternalRunAndSaveifOk();
                            C2iEventLog.WriteInfo("DEBUG_TODO - after InternalRunAndSaveifOk() Id = " + parametreStart.IdEtape);
                        }
                        else
                            C2iEventLog.WriteInfo("DEBUG_TODO - InternalRunAndSaveifOk() - Erreur : " + result.MessageErreur);

                        if (result && etape.CodeAffectations.Length > 0 && etape.DateFin == null)
                        {
                            CDonneeNotificationWorkflow donneeWorkflow = new CDonneeNotificationWorkflow(
                                        m_session.IdSession,
                                        etape.Id,
                                        etape.Libelle,
                                        etape.CodeAffectations,
                                        etape.TypeEtape.ExecutionAutomatique);
                            lstNotifications.Add(donneeWorkflow);
                            // Déclenche l'evenement spécifique au démarrage de l'étape
                            result = etape.EnregistreEvenement(CEtapeWorkflow.c_codeEvenementOnRunStep, true);
                            C2iEventLog.WriteInfo("DEBUG_TODO - ExecuteEtape() - etape.EnregistreEvenement()");
                        }
                        if (!result)
                        {
                            NoteErreurSurEtape(etape, result.MessageErreur);
                            return;
                        }
                        C2iEventLog.WriteInfo("DEBUG_TODO - ExecuteEtape() - Fin traitement étape Id = " + parametreStart.IdEtape);
                    }
                }
            }
            catch (Exception e)
            {
                C2iEventLog.WriteErreur("DEBUG_TODO - ExecuteEtape() - Exception executing step Id  = " + parametreStart.IdEtape + Environment.NewLine + e.Message);
            }
            if (lstNotifications != null)
                CEnvoyeurNotification.EnvoieNotifications(lstNotifications.ToArray());
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

        //------------------------------------------------------------------------
        private void LoopEtapes()
        {
            C2iEventLog.WriteInfo("DEBUG_TODO - LoopEtapes() - m_bLoopEtapesEnCours = " + m_bLoopEtapesEnCours);
            if (m_bLoopEtapesEnCours)
                return;

            int nNbEtapes = 0;
            LoadEtapesDepuisBase();
            lock (typeof(CLockerEtapes))
            {
                m_bLoopEtapesEnCours = true;
                m_timerLooper.Stop();
                C2iEventLog.WriteInfo("DEBUG_TODO - LoopEtapes() - m_timerLooper.Stop()");
                nNbEtapes = m_listeEtapes.Count;
            }
            try
            {
                while (nNbEtapes > 0)
                {
                    AssureSession();
                    if (m_session != null)
                    {
                        StartNextEtape();
                    }

                    lock (typeof(CLockerEtapes))
                    {
                        nNbEtapes = m_listeEtapes.Count;
                    }
                    System.Threading.Thread.Sleep(500);
                }
            }
            catch (Exception e)
            {
                C2iEventLog.WriteErreur("DEBUG_TODO - LoopEtapes() - Step loop failed : " + e.Message);
            }

            lock (typeof(CLockerEtapes))
            {
                m_bLoopEtapesEnCours = false;
                m_timerLooper.Interval = c_nDelaiTimerLooper;
                m_timerLooper.Start();
                C2iEventLog.WriteInfo("DEBUG_TODO - LoopEtapes() - m_timerLooper.Start()");
            }

        }

        //------------------------------------------------------------------------
        private void AssureSession()
        {
            //S'assure de la validité de la session
            if (m_session != null && !m_session.IsConnected)
            {
                m_session.CloseSession();
                m_session.Dispose();
                m_session = null;
            }
            if (m_session == null)
            {
                m_session = CSessionClient.CreateInstance();
                CResultAErreur result = m_session.OpenSession(new CAuthentificationSessionProcess(), "Steps looper",
                    ETypeApplicationCliente.Service);
                if (!result)
                {
                    C2iEventLog.WriteErreur("DEBUG_TODO - AssureSession() - Failed to create Step session looper" + Environment.NewLine + result.MessageErreur);
                    m_session = null;
                }
            }
        }

        //-------------------------------------------------------------------------
        private void LoadEtapesDepuisBase()
        {
            if (m_lastDateLookInDb == null ||
                ((TimeSpan)(DateTime.Now - m_lastDateLookInDb.Value)).TotalMilliseconds > c_nDelailLookInDb)
            {
                m_lastDateLookInDb = DateTime.Now;

                AssureSession();
                if (m_session != null)
                {
                    using (CContexteDonnee ctx = new CContexteDonnee(m_session.IdSession, true, false))
                    {
                        CFiltreData filtre = new CFiltreData(
                            CEtapeWorkflow.c_champEtat + "=@1",
                            (int)EEtatEtapeWorkflow.ADemarrer);
                        CListeObjetsDonnees lst = new CListeObjetsDonnees(ctx, typeof(CEtapeWorkflow));
                        lst.Filtre = filtre;
                        lst.AssureLectureFaite();
                        List<CParametresStartEtapeInPile> lstToStart = new List<CParametresStartEtapeInPile>();
                        foreach (CEtapeWorkflow etape in lst)
                        {
                            //TESTDBKEYTODO
                            CParametresStartEtapeInPile parametre = new CParametresStartEtapeInPile(
                                m_session.IdSession,
                                etape.Id,
                                etape.KeyDémarreur,
                                etape.DateDebut != null ? etape.DateDebut.Value : DateTime.Now);
                            lstToStart.Add(parametre);
                        }
                        AddEtapesADemarrer(lstToStart.ToArray());
                    }
                }
            }
        }


        //-------------------------------------------------------------------------
        public static void AddEtapesADemarrer(params CParametresStartEtapeInPile[] parametres)
        {
            lock (typeof(CLockerEtapes))
            {
                foreach (CParametresStartEtapeInPile parametre in parametres)
                {
                    if (parametre != null)
                    {
                        if (Instance.m_listeEtapes.Count(x => x.IdEtape == parametre.IdEtape) == 0)
                            Instance.m_listeEtapes.Add(parametre);
                    }
                }
            }

            if (!Instance.m_bLoopEtapesEnCours)
            {
                Instance.m_timerLooper.Stop();
                Instance.m_timerLooper.Interval = c_nDelaiTimerLooper;
                Instance.m_timerLooper.Start();
                C2iEventLog.WriteInfo("DEBUG_TODO - AddEtapesADemarrer() - m_timerLooper.Start()");
            }

        }

    }
}
