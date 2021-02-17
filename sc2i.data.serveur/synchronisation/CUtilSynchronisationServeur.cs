using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.synchronisation;
using sc2i.multitiers.server;
using sc2i.common;

namespace sc2i.data.serveur.synchronisation
{
    public class CUtilSynchronisationServeur : C2iObjetServeur, IUtilSynchronisation
    {
        public CUtilSynchronisationServeur(int nIdSession)
            : base(nIdSession)
        {
        }

        private IDatabaseConnexionSynchronisable GetDbSynchronisable()
        {
            IDatabaseConnexionSynchronisable db = (IDatabaseConnexionSynchronisable)CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, "") as IDatabaseConnexionSynchronisable;
            return db;
        }


        public int IdSyncSession
        {
            get 
            {
                IDatabaseConnexionSynchronisable db = GetDbSynchronisable();
                if (db != null)
                    return db.IdSyncSession;
                return -1;                
            }
        }

        public void IncrementeSyncSession()
        {
            IDatabaseConnexionSynchronisable db = GetDbSynchronisable();
            if (db != null)
                db.IncrementeSyncSession();
        }

        public int LastSyncIdPutInBasePrincipale
        {
            get
            {
                IDatabaseConnexionSynchronisable db = GetDbSynchronisable();
                if (db != null)
                    return db.LastSyncIdPutInBasePrincipale;
                return -1;
            }
            set
            {
                IDatabaseConnexionSynchronisable db = GetDbSynchronisable();
                if (db != null)
                    db.LastSyncIdPutInBasePrincipale = value;
            }
        }

        public int LastSyncIdVueDeBasePrincipale
        {
            get
            {
                IDatabaseConnexionSynchronisable db = GetDbSynchronisable();
                if (db != null)
                    return db.LastSyncIdVueDeBasePrincipale;
                return -1;
            }
            set
            {
                IDatabaseConnexionSynchronisable db = GetDbSynchronisable();
                if (db != null)
                    db.LastSyncIdVueDeBasePrincipale = value;
            }
        }

    }
}
