using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.common.recherche;

namespace sc2i.win32.common.recherche
{
    public class CWndArbreResultatRequete : TreeView
    {
        private ImageList m_imagesArbre;
        private System.ComponentModel.IContainer components;
        private CResultatRequeteRechercheObjet m_resultat;

        public CWndArbreResultatRequete()
            : base()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------------------------------
        public void Init(CResultatRequeteRechercheObjet resultat)
        {
            m_resultat = resultat;
            Nodes.Clear();
            FillNodes(Nodes, resultat.ArbreResultats);
        }

        //-----------------------------------------------------------------------------
        private void FillNodes(TreeNodeCollection nodes, CArbreResultatRechercheObjet arbre)
        {
            //Ajoute les branches de l'arbre
            foreach (CArbreResultatRechercheObjet branche in arbre.Branches)
            {
                nodes.Add(CreateNodeBranche(branche));
            }
        }

        //-----------------------------------------------------------------------------
        private TreeNode CreateNodeBranche(CArbreResultatRechercheObjet branche)
        {
            TreeNode node = new TreeNode(branche.Noeud.LibelleNoeudCheminResultatRechercheObjet);
            node.Tag = branche;
            if (branche.Branches.Length > 0)
                node.Nodes.Add(new TreeNode(""));
            int nIndex = 0;
            if (branche.Branches.Count() == 0)
                nIndex = 1;
            node.ImageIndex = nIndex;
            node.SelectedImageIndex = nIndex;
            return node;
        }



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CWndArbreResultatRequete));
            this.m_imagesArbre = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // m_imagesArbre
            // 
            this.m_imagesArbre.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesArbre.ImageStream")));
            this.m_imagesArbre.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagesArbre.Images.SetKeyName(0, "Folder.png");
            this.m_imagesArbre.Images.SetKeyName(1, "1256575237_resultset_next.png");
            // 
            // CWndArbreResultatRequete
            // 
            this.ImageIndex = 0;
            this.ImageList = this.m_imagesArbre;
            this.LineColor = System.Drawing.Color.Black;
            this.SelectedImageIndex = 0;
            this.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.CWndArbreResultatRequete_BeforeExpand);
            this.ResumeLayout(false);

        }



        private void CWndArbreResultatRequete_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if ( node.Nodes.Count == 1 && node.Nodes[0].Tag == null )
            {
                CArbreResultatRechercheObjet arbre = node.Tag as CArbreResultatRechercheObjet;
                node.Nodes.Clear();
                if ( arbre != null )
                    FillNodes ( node.Nodes, arbre );
            }
        }

        private void m_menuRightClic_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
