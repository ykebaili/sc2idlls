using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.multitiers.client;
using sc2i.common;
using sc2i.common.unites;

namespace sc2i.data.dynamic.unite
{

    [Serializable]
    public class CDonneeNotificationModificationSystemeUnites : IDonneeNotification
    {
        private int m_nIdSession;

        public CDonneeNotificationModificationSystemeUnites(int nIdSessionEnvoyeur)
        {
            m_nIdSession = nIdSessionEnvoyeur;
        }


        //-----------------------------------------------
        public int IdSessionEnvoyeur
        {
            get
            {
                return m_nIdSession;
            }
            set
            {
                m_nIdSession = value;
            }
        }

        //-----------------------------------------------
        public int PrioriteNotification
        {
            get { return 0; }
        }
    }

    //------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CGestionnaireNotificationsSystemUnite
    {
        private static bool m_bIsInit = false;

        //------------------------------------------------------
        public static void Autoexec()
        {
            if (!m_bIsInit)
            {
                m_bIsInit = true;
                CGestionnaireUnites.OnInitGestionnaireUnites += new EventHandler(OnInitGestionnaireUnites);
                CSessionClient.AfterOpenSession += new SessionEventHandler(CSessionClient_AfterOpenSession);
                CGestionnaireUnites.Refresh();
            }
            

        }

        static void CSessionClient_AfterOpenSession(CSessionClient session)
        {
            CRecepteurNotification recepteur = new CRecepteurNotification(session.IdSession, typeof(CDonneeNotificationModificationSystemeUnites));
            recepteur.OnReceiveNotification += new NotificationEventHandler(recepteur_OnReceiveNotification);
        }

        //------------------------------------------------------
        public static void  recepteur_OnReceiveNotification(IDonneeNotification donnee)
        {
 	        CGestionnaireUnites.Refresh();
        }

        //------------------------------------------------------
        public static void OnInitGestionnaireUnites(object sender, EventArgs args)
        {

            CListeObjetDonneeGenerique<CClasseUniteInDb> classes = new CListeObjetDonneeGenerique<CClasseUniteInDb>(CContexteDonneeSysteme.GetInstance());
            classes.Refresh();
            foreach (CClasseUniteInDb classe in classes)
            {
                CGestionnaireUnites.AddClasseUnite(classe);
            }
            CListeObjetDonneeGenerique<CUniteInDb> unites = new CListeObjetDonneeGenerique<CUniteInDb>(CContexteDonneeSysteme.GetInstance());
            unites.Refresh();
            foreach (CUniteInDb unite in unites)
            {
                CGestionnaireUnites.AddUnite(unite);
            }
        }
    }
}
