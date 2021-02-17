using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common.recherche;
using sc2i.common;

namespace sc2i.win32.common.recherche
{
    public partial class CFormResultatRechercheObjet : Form
    {
        private CRequeteRechercheObjet m_requete = null;
        private CResultatRequeteRechercheObjet m_resultat = null;

        public delegate void OnDemandeAffichageNoeudEventHandler(CNoeudCheminResultatRechercheObjetAvecParents noeuds);

        public static OnDemandeAffichageNoeudEventHandler OnDemandeAffichageNoeud;

        public CFormResultatRechercheObjet()
        {
            InitializeComponent();
        }

        private delegate void GenericDelegate();

        private void CFormResultatRechercheObjet_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
            m_arbreResultat.Nodes.Clear();
            m_arbreResultat.Nodes.Add(I.T("Searching...|20002"));
            GenericDelegate del = new GenericDelegate(StartRecherche);
            Cursor = Cursors.WaitCursor;
            del.BeginInvoke(null, null);
        }

        private void StartRecherche()
        {
            m_resultat = new CResultatRequeteRechercheObjet();
            CMoteurRechercheObjetCherchable.ChercheObjet(m_requete, m_resultat);
            EventHandler handler = new EventHandler(OnEndRecherche);
            this.Invoke(handler, null, null);
        }

        public static void RechercheInThread(CRequeteRechercheObjet requete, string strLibelleRecherche)
        {
            CFormResultatRechercheObjet form = new CFormResultatRechercheObjet();
            form.Text = strLibelleRecherche;
            form.m_lblElementRecherché.Text = strLibelleRecherche;
            form.ShowInTaskbar = true;
            form.m_requete = requete;
            form.Show();
        }


        public void OnEndRecherche(object sender, EventArgs args )
        {
            Cursor = Cursors.Arrow;
            m_arbreResultat.Init(m_resultat);
        }

        private void m_arbreResultat_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if ( OnDemandeAffichageNoeud != null && e.Node != null )
            {
                TreeNode node = e.Node;
                CNoeudCheminResultatRechercheObjetAvecParents noeudEnCours = null;
                while ( node != null )
                {
                    CArbreResultatRechercheObjet arbre = node.Tag as CArbreResultatRechercheObjet;
                    if (arbre == null)//L'item n'est pas un arbre, stop la remontée
                        break;
                    INoeudCheminResultatRechercheObjet noeud = null;
                    if (arbre != null)
                        noeud = arbre.Noeud;
                    if ( noeud != null )
                        noeudEnCours = new CNoeudCheminResultatRechercheObjetAvecParents ( noeud, noeudEnCours);
                    node = node.Parent;
                }
                if ( noeudEnCours != null )
                    OnDemandeAffichageNoeud ( noeudEnCours );
            }
        }


        
    }
}
