using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.common.recherche
{
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// R�sultat d'une recherche d'objet
    /// Le r�sultat est organis� dans un arbre
    /// </summary>
    [Serializable]
    public class CResultatRequeteRechercheObjet
    {
        //Chemins en cours, mis � jour pendant une recherche
        [NonSerialized]
        private Stack<CCheminResultatRechercheObjet> m_pileChemins = new Stack<CCheminResultatRechercheObjet>();

        /// <summary>
        /// Arbre stockant les r�sultats
        /// </summary>
        private CArbreResultatRechercheObjet m_arbreResultat = new CArbreResultatRechercheObjet(null);


        public CResultatRequeteRechercheObjet()
        {
        }

        public bool ObjetTrouve
        {
            get
            {
                return m_arbreResultat.Branches.Length > 0;
            }
        }

        /// <summary>
        /// Range les r�sultats suivants sous un nouveau chemin
        /// </summary>
        /// <param name="noeud"></param>
        public void PushChemin(INoeudCheminResultatRechercheObjet noeud)
        {
            if (m_pileChemins.Count > 0)
            {
                CCheminResultatRechercheObjet chemin = m_pileChemins.Peek();
                m_pileChemins.Push(new CCheminResultatRechercheObjet(noeud, chemin));
            }
            else
                m_pileChemins.Push(new CCheminResultatRechercheObjet(noeud, null));
        }

        /// <summary>
        /// D�pile un chemin pour les r�sultats qui suivent
        /// </summary>
        public void PopChemin()
        {
            m_pileChemins.Pop();
        }

        /// <summary>
        /// Ajoute un  r�sultat 
        /// </summary>
        /// <param name="consommateur"></param>
        public void AddResultat(INoeudCheminResultatRechercheObjet noeud)
        {
            CCheminResultatRechercheObjet chemin = null;
            if (m_pileChemins.Count > 0)
                chemin = m_pileChemins.Peek();
            chemin = new CCheminResultatRechercheObjet(noeud, chemin);
            m_arbreResultat.AddResultat(chemin);
        }

        /// <summary>
        /// Arbre des r�sultats
        /// </summary>
        public CArbreResultatRechercheObjet ArbreResultats
        {
            get
            {
                return m_arbreResultat;
            }
        }
    }


    




    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// G�re les chemins dans un r�sultat de recherche d'objet
    /// </summary>
    [Serializable]
    internal class CCheminResultatRechercheObjet
    {
        //Noeuds utilis�s pour arriver au chemin
        private List<INoeudCheminResultatRechercheObjet> m_listeNoeuds = new List<INoeudCheminResultatRechercheObjet>();

        private CCheminResultatRechercheObjet()
        {
        }

        public CCheminResultatRechercheObjet(INoeudCheminResultatRechercheObjet noeud, CCheminResultatRechercheObjet cheminParent)
        {
            m_listeNoeuds = new List<INoeudCheminResultatRechercheObjet>();
            if (cheminParent != null)
                m_listeNoeuds.AddRange(cheminParent.m_listeNoeuds);
            m_listeNoeuds.Add(noeud);
        }

        /// <summary>
        /// Retourne la racine du chemin
        /// </summary>
        public INoeudCheminResultatRechercheObjet NoeudsRacine
        {
            get
            {
                if (m_listeNoeuds.Count > 0)
                    return m_listeNoeuds[0];
                return null;
            }
        }

        /// <summary>
        /// Retourne la suite du chemin, apr�s la racine
        /// </summary>
        public CCheminResultatRechercheObjet CheminFils
        {
            get
            {
                List<INoeudCheminResultatRechercheObjet> lst = new List<INoeudCheminResultatRechercheObjet>();
                for (int nNoeud = 1; nNoeud < m_listeNoeuds.Count; nNoeud++)
                    lst.Add(m_listeNoeuds[nNoeud]);
                CCheminResultatRechercheObjet chemin = new CCheminResultatRechercheObjet();
                chemin.m_listeNoeuds = lst;
                return chemin;
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Arbre de r�sultat pour une recherche objet
    /// </summary>
    [Serializable]
    public class CArbreResultatRechercheObjet
    {
        /// <summary>
        /// Noeud que repr�sente cet arbre
        /// </summary>
        private INoeudCheminResultatRechercheObjet m_noeud = null;

        /// <summary>
        /// Branches de cet arbre
        /// </summary>
        private List<CArbreResultatRechercheObjet> m_branches = new List<CArbreResultatRechercheObjet>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noeudRacine"></param>
        public CArbreResultatRechercheObjet(INoeudCheminResultatRechercheObjet noeud)
        {
            m_noeud = noeud;
        }

        /// <summary>
        /// Noeud correspondant � cette branche
        /// </summary>
        public INoeudCheminResultatRechercheObjet Noeud
        {
            get
            {
                return m_noeud;
            }
        }

        /// <summary>
        /// Branches de cet arbre
        /// </summary>
        public CArbreResultatRechercheObjet[] Branches
        {
            get
            {
                return m_branches.ToArray();
            }
        }

        /// <summary>
        /// Ajoute un r�sultat � l'arbre
        /// </summary>
        /// <param name="chemin"></param>
        /// <param name="resultat"></param>
        internal void AddResultat(CCheminResultatRechercheObjet chemin)
        {
            if (chemin != null && chemin.NoeudsRacine != null)
            {
                foreach (CArbreResultatRechercheObjet arbreFils in Branches)
                {
                    if (arbreFils.Noeud != null && arbreFils.Noeud.Equals(chemin.NoeudsRacine))
                    {
                        arbreFils.AddResultat(chemin.CheminFils);
                        return;
                    }
                }
                CArbreResultatRechercheObjet branche = new CArbreResultatRechercheObjet(chemin.NoeudsRacine);
                m_branches.Add(branche);
                branche.AddResultat(chemin.CheminFils);
                return;
            }
        }

    }
}
