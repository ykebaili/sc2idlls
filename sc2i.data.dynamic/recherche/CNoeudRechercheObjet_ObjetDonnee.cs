using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using sc2i.common.recherche;

namespace sc2i.data.dynamic.recherche
{
    public class CNoeudRechercheObjet_ObjetDonnee : INoeudCheminResultatRechercheObjet
    {
        private CReferenceObjetDonnee m_referenceObjet = null;
        private string m_strLibelle;


        //*-------------------------------------------------------------------
        public CNoeudRechercheObjet_ObjetDonnee(CObjetDonnee objet)
        {
            if (objet != null)
                m_referenceObjet = new CReferenceObjetDonnee(objet);
            m_strLibelle = objet.DescriptionElement;
        }

        //*-------------------------------------------------------------------
        public CNoeudRechercheObjet_ObjetDonnee(CObjetDonnee objet,
            string strLibelle)
        {
            if (objet != null)
                m_referenceObjet = new CReferenceObjetDonnee(objet);
            m_strLibelle = strLibelle;
        }

        //*-------------------------------------------------------------------
        public string LibelleNoeudCheminResultatRechercheObjet
        {

            get
            {
                return m_strLibelle;
            }
        }

        public CObjetDonnee GetObjet(CContexteDonnee contexte)
        {
            if (m_referenceObjet != null)
                return m_referenceObjet.GetObjet(contexte);
            return null;
        }

    }
}
