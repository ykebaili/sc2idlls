using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common.recherche;
using sc2i.common;

namespace sc2i.process.recherche
{
    [Serializable]
    public class CNoeudRecherche_ProcessInDbc : INoeudCheminResultatRechercheObjet
    {
        private int m_nIdProcessInDb = -1;
        private string m_strLibelle = "";

        public CNoeudRecherche_ProcessInDbc(CProcessInDb processInDb)
        {
            if (processInDb != null)
            {
                m_nIdProcessInDb = processInDb.Id;
                m_strLibelle = I.T("Process @1|20025", processInDb.Libelle);
            }
        }


        public string LibelleNoeudCheminResultatRechercheObjet
        {
            get { return m_strLibelle; }
        }

        public int IdProcessInDb
        {
            get
            {
                return m_nIdProcessInDb;
            }
        }

    }
}
