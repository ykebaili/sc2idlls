using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.multitiers.client;
using sc2i.common;
using System.Data;
using System.Collections;

namespace sc2i.data.synchronisation
{
    public interface IUtilSynchronisation
    {
        /// <summary>
        /// Retourne l'id de syncSession en cours
        /// </summary>
        int IdSyncSession { get; }

        /// <summary>
        /// Incrémente l'id de sync session en cours
        /// </summary>
        void IncrementeSyncSession();

        /// <summary>
        /// Indique le dernier id de sync session envoyé vers la base principale
        /// </summary>
        int LastSyncIdPutInBasePrincipale { get; set; }

        /// <summary>
        /// Inidique le dernier id de sync session vue de la base principale
        /// </summary>
        int LastSyncIdVueDeBasePrincipale { get; set; }

        
    }

    public class CUtilSynchronisation : IUtilSynchronisation
    {
        public const string c_strEvenementAfterSyncSurMain = "AFTER_SYNC_ON_MAIN";
        
        private int m_nIdSession;
        //-----------------------------------------
        public CUtilSynchronisation(int nIdSession)
        {
            m_nIdSession = nIdSession;
        }

        //-----------------------------------------
        private static IUtilSynchronisation GetUtilSurServeur( int nIdSession )
        {
            return (IUtilSynchronisation)C2iFactory.GetNewObjetForSession("CUtilSynchronisationServeur", typeof(IUtilSynchronisation), nIdSession);
		}


        //-----------------------------------------
        public int IdSyncSession
        {
            get 
            {
                return GetUtilSurServeur(m_nIdSession).IdSyncSession;
            }
        }

        //-----------------------------------------
        public void IncrementeSyncSession()
        {
            GetUtilSurServeur(m_nIdSession).IncrementeSyncSession();
        }

        //-----------------------------------------
        public int LastSyncIdPutInBasePrincipale 
        {
            get
            {
                return GetUtilSurServeur(m_nIdSession).LastSyncIdPutInBasePrincipale;
            }
            set
            {
                GetUtilSurServeur(m_nIdSession).LastSyncIdPutInBasePrincipale = value;
            }
        }

        //-----------------------------------------
        public int LastSyncIdVueDeBasePrincipale
        {
            get
            {
                return GetUtilSurServeur(m_nIdSession).LastSyncIdVueDeBasePrincipale;
            }
            set
            {
                GetUtilSurServeur(m_nIdSession).LastSyncIdVueDeBasePrincipale = value;
            }
        }

        

    }

}
