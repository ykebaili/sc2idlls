using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common.recherche;
using sc2i.common;

namespace sc2i.process.recherche
{
    [Serializable]
    public class CNoeudRecherche_Action : INoeudCheminResultatRechercheObjet
    {
        private int m_nIdAction = -1;
        private string m_strLibelle = "";

        public CNoeudRecherche_Action(CAction action)
        {
            m_nIdAction = action.IdObjetProcess;
            m_strLibelle = I.T("Action @1|20026",action.Libelle);
        }

        //---------------------------------------
        public string LibelleNoeudCheminResultatRechercheObjet
        {
            get
            {
                return m_strLibelle;
            }
        }

        //---------------------------------------
        public int IdAction
        {
            get
            {
                return m_nIdAction;
            }
        }


    }
}
