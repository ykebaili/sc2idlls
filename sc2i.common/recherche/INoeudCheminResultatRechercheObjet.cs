using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.recherche
{
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Noeud de recherche dans un résultat de recherche
    /// </summary>
    public interface INoeudCheminResultatRechercheObjet
    {
        string LibelleNoeudCheminResultatRechercheObjet { get; }
    }

    public class CNoeudCheminResultatRechercheObjetLibelleSimple :INoeudCheminResultatRechercheObjet
    {
        private string m_strLibelle = "";

        public CNoeudCheminResultatRechercheObjetLibelleSimple(string strLibelle)
        {
            m_strLibelle = strLibelle;

        }

        public string LibelleNoeudCheminResultatRechercheObjet
        {
            get
            {
                return m_strLibelle;
            }
        }
    }

    public class CNoeudCheminResultatRechercheObjetAvecParents
    {
        private INoeudCheminResultatRechercheObjet m_noeud;
        private CNoeudCheminResultatRechercheObjetAvecParents m_noeudParent = null;
        private CNoeudCheminResultatRechercheObjetAvecParents m_noeudFils = null;

        public CNoeudCheminResultatRechercheObjetAvecParents(INoeudCheminResultatRechercheObjet noeud,
            CNoeudCheminResultatRechercheObjetAvecParents noeudFils)
        {
            m_noeud = noeud;
            m_noeudFils = noeudFils;
            if (m_noeudFils != null)
                m_noeudFils.m_noeudParent = this;
        }

        public INoeudCheminResultatRechercheObjet Noeud
        {
            get
            {
                return m_noeud;
            }
        }

        public CNoeudCheminResultatRechercheObjetAvecParents NoeudParent
        {
            get{
                return m_noeudParent;
            }
        }

        public CNoeudCheminResultatRechercheObjetAvecParents NoeudFils
        {
            get
            {
                return m_noeudFils;
            }
        }
    }


}
