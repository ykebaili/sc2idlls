using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using sc2i.common;
using sc2i.win32.common;
using sc2i.win32.data;
using sc2i.data;


namespace sc2i.win32.data.navigation
{
    public partial class CFormReaffecteObjetHierarchique : Form
    {
		Type m_typeObjets = null;
        private CObjetHierarchique m_elementAReaffecter;

        public CFormReaffecteObjetHierarchique()
        {
            InitializeComponent();
        }

        public static bool ReaffecteObjet(CObjetHierarchique element)
        {
            CFormReaffecteObjetHierarchique form = new CFormReaffecteObjetHierarchique();
			form.m_typeObjets = element.GetType();
            form.m_elementAReaffecter = element;
            bool bResult = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                TreeNode node = form.m_arbre.SelectedNode;
                if (node != null)
                {
					CObjetHierarchique parentDemande = (CObjetHierarchique)node.Tag;
                    element.ObjetParent = (CObjetHierarchique)node.Tag;
					if (parentDemande != null && !parentDemande.Equals ( element.ObjetParent ))
                    {
                        CFormAlerte.Afficher(I.T("The system cannot reaffect the element|30115"), EFormAlerteType.Erreur);
                    }
                    else
                        bResult = true;
                }
            }
            return bResult;
        }

        private void CFormReaffecteObjetHierarchique_Load(object sender, EventArgs e)
        {
			CWin32Traducteur.Translate(this);
            CListeObjetsDonnees liste = new CListeObjetsDonnees(CSc2iWin32DataClient.ContexteCourant, m_typeObjets );
			liste.Filtre = new CFiltreData(m_elementAReaffecter.ChampNiveau + "=@1", 0);
            TreeNode nodeAucun = m_arbre.Nodes.Add(I.T(" (None)|30031"));
            foreach (CObjetHierarchique objet in liste)
            {
                TreeNode node = new TreeNode(objet.Libelle);
                node.Tag = objet;
                m_arbre.Nodes.Add(node);
                node.Nodes.Add("");
                if (m_elementAReaffecter != null)
                    node.Expand();
            }
        }

        //----------------------------------------------------------------------------
        private TreeNode CreateNode(CObjetHierarchique objet)
        {
            TreeNode node = new TreeNode(objet.Libelle);
            node.Tag = objet;
            if (objet.ObjetsFils.Count != 0)
            {
                TreeNode dummy = new TreeNode("");
                dummy.Tag = null;
                node.Nodes.Add(dummy);
            }
            return node;
        }


        //----------------------------------------------------------------------------
        private void m_arbre_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Nodes.Count > 0 && node.Nodes[0].Tag == null)
            {
                //il faut reremplir ce noeud
                node.Nodes.Clear();
                CListeObjetsDonnees listeObjets = null;
                listeObjets = ((CObjetHierarchique)node.Tag).ObjetsFils;
                if (listeObjets != null)
                {
                    foreach (CObjetHierarchique objet in listeObjets)
                    {
                        TreeNode newNode = CreateNode(objet);
                        node.Nodes.Add(newNode);
                        CObjetHierarchique objetThis = null;
						objetThis = m_elementAReaffecter;
                        if (objet.CodeSystemeComplet.Length > objetThis.CodeSystemeComplet.Length &&
                            objet.CodeSystemeComplet.Substring(0, objetThis.CodeSystemeComplet.Length) ==
                            objetThis.CodeSystemeComplet)
                            node.Expand();
                    }
                }
            }

        }

        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_arbre.SelectedNode != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

		private void m_btnAnnuler_Click(object sender, EventArgs e)
		{

		}

		
    }
}