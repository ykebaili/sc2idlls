using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common.unites;
using sc2i.data.dynamic.unite;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.data.dynamic.unites
{
    public partial class CControleGestionUnites : UserControl
    {
        private object m_objetEdite = null;

        public CControleGestionUnites()
        {
            InitializeComponent();
        }

        //------------------------------------------------------------
        public void Init()
        {
            m_arbreUnits.BeginUpdate();
            m_arbreUnits.Nodes.Clear();
            foreach (IClasseUnite classe in CGestionnaireUnites.Classes)
            {
                TreeNode node = new TreeNode(classe.Libelle);
                node.Tag = classe;
                m_arbreUnits.Nodes.Add(node);
                foreach (IUnite unite in CGestionnaireUnites.Unites)
                {
                    if (unite.Classe != null && unite.Classe.GlobalId == classe.GlobalId)
                    {
                        TreeNode nodeU = new TreeNode(unite.LibelleCourt);
                        nodeU.Tag = unite;
                        node.Nodes.Add(nodeU);
                    }
                }
            }
            m_arbreUnits.EndUpdate();
        }

        private void DisplayClasse(IClasseUnite classe)
        {
            m_panelUnite.Visible = false;
            m_panelClasse.Visible = true;
            m_txtLibelleClasse.Text = classe.Libelle;
            m_txtIdClasse.Text = classe.GlobalId;
            m_txtUniteDeBase.Text = classe.UniteBase;
            m_objetEdite = classe;
            m_panelEditeClasse.Visible = classe is CClasseUniteInDb;
        }

        private void DisplayUnite(IUnite unite)
        {
            m_panelUnite.Visible = true;
            m_panelClasse.Visible = false;
            if (unite.Classe != null)
                m_txtClasseDeUnite.Text = unite.Classe.Libelle;
            else
                m_txtClasseDeUnite.Text = "???";

            m_txtIdUnite.Text = unite.GlobalId;
            m_txtLibellCourtUnite.Text = unite.LibelleCourt;
            m_txtLibelleLongUnite.Text = unite.LibelleLong;
            m_txtFacteurConversion.DoubleValue = unite.FacteurVersBase;
            m_txtOffsetConversion.DoubleValue = unite.OffsetVersBase;
            m_objetEdite = unite;
            RefreshLibelleFormule();
            m_panelEditeUnite.Visible = unite is CUniteInDb;
        }

        //------------------------------------------------------------
        private void RefreshLibelleFormule()
        {
            IUnite unite = m_objetEdite as IUnite;
            if (unite != null)
            {
                IClasseUnite classe = unite.Classe;
                if (classe != null)
                {
                    m_lblConversion.Text = 1+m_txtLibellCourtUnite.Text + " = " +
                        "A" + classe.Libelle + "+B";
                    return;
                }
            }
            m_lblConversion.Text = "";
        }

        

        //--------------------------------------------------------------
        private void m_lnkModifierUnite_LinkClicked(object sender, EventArgs e)
        {
            CUniteInDb unite = m_objetEdite as CUniteInDb;
            if (unite != null)
            {
                if (CFormEditeUniteInDb.EditeUnite(unite))
                {
                    RefreshGestionnaire();
                }
            }
        }

        //-------------------------------------------------------------
        private void RefreshGestionnaire()
        {
            CGestionnaireUnites.Refresh();
            CTreeViewNodeKeeper keeper = new CTreeViewNodeKeeper(m_arbreUnits);
            Init();
            keeper.Apply(m_arbreUnits.Nodes);
        }

        //-------------------------------------------------------------
        private void m_lnkAddUnite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CFormEditeUniteInDb.EditeUnite(null))
            {
                RefreshGestionnaire();
            }
        }

        //-------------------------------------------------------------
        private void m_lnkSupprimerUnite_LinkClicked(object sender, EventArgs e)
        {
            CUniteInDb unite = m_objetEdite as CUniteInDb;
            if (unite == null)
            {
                MessageBox.Show(I.T("Can not remove this unit|20074"));
                return;
            }
            if ( MessageBox.Show(I.T("Delete unit @1 ?|20075", unite.Libelle),
                "",
                MessageBoxButtons.YesNo ) == DialogResult.No )
                return;
            CResultAErreur result = unite.Delete();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            RefreshGestionnaire();
        }

        //------------------------------------------------------------------------
        private void m_lnkSupprimerClasse_LinkClicked(object sender, EventArgs e)
        {
            CClasseUniteInDb classe = m_objetEdite as CClasseUniteInDb;
            if (classe == null)
            {
                MessageBox.Show(I.T("Can not delete that unit class|20076"));
                return;
            }
            if (MessageBox.Show(I.T("Delete unit class @1 ?|20077", classe.Libelle),
                "",
                MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            CResultAErreur result = classe.Delete();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            RefreshGestionnaire();
        }

        //-------------------------------------------------------------------
        private void m_lnkModifierClasse_LinkClicked(object sender, EventArgs e)
        {
            CClasseUniteInDb classe = m_objetEdite as CClasseUniteInDb;
            if (classe!= null)
            {
                if (CFormEditeClasseUniteInDb.EditeClasse(classe))
                {
                    RefreshGestionnaire();
                }
            }
        }

        //-------------------------------------------------------------------
        private void m_lnkAddClasse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CFormEditeClasseUniteInDb.EditeClasse(null))
            {
                RefreshGestionnaire();
            }
        }

        private void m_arbreUnits_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IClasseUnite classe = e.Node.Tag as IClasseUnite;
            if (classe != null)
            {
                DisplayClasse(classe);
                return;
            }
            IUnite unite = e.Node.Tag as IUnite;
            if (unite != null)
            {
                DisplayUnite(unite);
                return;
            }
            m_panelClasse.Visible = false;
            m_panelUnite.Visible = false;
        }

    }
}


       
