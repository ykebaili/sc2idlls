using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common.recherche;
using sc2i.common;

namespace sc2i.process.recherche
{
    [Serializable]
    public class CNoeudRecherche_Evenementc : INoeudCheminResultatRechercheObjet
    {
        private int m_nIdEvenement = -1;
        private string m_strLibelle = "";

        public CNoeudRecherche_Evenementc(CEvenement evenement)
        {
            if (evenement != null)
            {
                m_nIdEvenement = evenement.Id;
                m_strLibelle = I.T("Event @1|20025", evenement.Libelle);
            }
        }


        public string LibelleNoeudCheminResultatRechercheObjet
        {
            get { return m_strLibelle; }
        }

        public int IdEvenement
        {
            get
            {
                return m_nIdEvenement;
            }
        }

    }
}
