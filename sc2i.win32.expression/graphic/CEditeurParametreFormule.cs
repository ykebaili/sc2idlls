using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using System.Collections;
using sc2i.win32.common;
using sc2i.common;

namespace sc2i.win32.expression
{
    public partial class CEditeurParametreFormule : UserControl
    {
        private int m_nNumParametre = 0;
        private CRepresentationExpressionGraphique m_expressionGraphique = null;
        private IFournisseurProprietesDynamiques m_fournisseur = null;

        //------------------------------------
        public CEditeurParametreFormule()
        {
            InitializeComponent();
        }

        //------------------------------------
        public int NumParametre
        {
            get
            {
                return m_nNumParametre;
            }
        }

        //------------------------------------
        public C2iExpression Formule
        {
            get
            {
                return m_txtFormule.Formule;
            }
        }

        //------------------------------------
        private bool m_bIsInitializing = false;
        public void Init(
            CRepresentationExpressionGraphique expressionGraphique,
            int nNumeroParametre,
            IFournisseurProprietesDynamiques fournisseur,
            CObjetPourSousProprietes objetAnalyse)
        {
            m_fournisseur = fournisseur;
            m_bIsInitializing = true;
            m_lblNomParametre.Text = I.T("Parametre @1|20000)",(nNumeroParametre+1).ToString());
            C2iExpressionAnalysable expAn = expressionGraphique.Formule as C2iExpressionAnalysable;
            if (expAn != null)
            {
                CInfo2iExpression info = expAn.GetInfos();
                m_lblNomParametre.Text = info.GetNomParametre(nNumeroParametre);
                m_toolTip.SetToolTip(m_lblNomParametre, m_lblNomParametre.Text);
            }
            m_expressionGraphique = expressionGraphique;
            m_nNumParametre = nNumeroParametre;
            m_txtFormule.Init(fournisseur, objetAnalyse);
            CRepresentationExpressionGraphique graf = expressionGraphique.GetExterne ( m_nNumParametre );
            if ( graf != null )
            {
                m_chkVisible.Checked=  true;
            }
            else
            {
                m_chkVisible.Checked = false;
            }
            UpdateWindow();
            m_bIsInitializing = false;
        }

        //------------------------------------
        public void ChangeObjetAnalyse(CObjetPourSousProprietes objetAnalyse)
        {
            m_txtFormule.Init(m_fournisseur, objetAnalyse);
        }

        //------------------------------------
        public void ReloadParametre()
        {
            if (m_expressionGraphique != null && m_expressionGraphique.Formule != null)
            {
                C2iExpression formule = m_expressionGraphique.Formule;
                C2iExpression[] parametres = formule.Parametres2i;
                if (parametres.Length > m_nNumParametre)
                {
                    m_bIsInitializing = true;
                    m_txtFormule.Formule = parametres[m_nNumParametre];
                    m_bIsInitializing = false;
                }
            }
        }
        
        //------------------------------------
        public event EventHandler OnChangeDessin;

        //------------------------------------
        private void m_chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            C2iExpressionGraphique rep = m_expressionGraphique.RepresentationRacine;
            if (!m_bIsInitializing)
            {
                if (m_chkVisible.Checked)
                {

                    if (rep == null)
                    {
                        m_chkVisible.Checked = false;
                        return;
                    }

                    if (rep != null)
                    {
                        CRepresentationExpressionGraphique exp = null;
                        if (exp == null)
                        {
                            C2iExpression formule = m_txtFormule.Formule;
                            if (formule == null)
                                formule = new C2iExpressionNull();
                            exp = new CRepresentationExpressionGraphique();
                            rep.AddChild(exp);
                            exp.Parent = rep;
                            Point pt = m_expressionGraphique.Position;
                            pt.Offset(2 * m_expressionGraphique.Size.Width, m_nNumParametre * (m_expressionGraphique.Size.Height + 10));
                            exp.Position = pt;
                            exp.Formule = formule;
                        }
                        m_expressionGraphique.SetExterne(m_nNumParametre, exp);
                        UpdateWindow();
                        if (OnChangeDessin != null)
                            OnChangeDessin(this, null);
                    }
                }
                else
                {
                    //Not checked
                    CRepresentationExpressionGraphique ext = m_expressionGraphique.GetExterne(m_nNumParametre);
                    if (ext != null)
                    {
                        m_expressionGraphique.SetExterne(m_nNumParametre, null);
                    }
                    UpdateWindow();
                    if (OnChangeDessin != null)
                        OnChangeDessin(this, null);
                }
            }
        }

        private void m_txtFormule_OnChangeTexteFormule(object sender, EventArgs e)
        {
            if (!m_bIsInitializing )
            {
                if (m_txtFormule.Formule != null)
                {
                    CRepresentationExpressionGraphique externe = m_expressionGraphique.GetExterne(m_nNumParametre);
                    if (externe == null)
                    {
                        m_expressionGraphique.Formule.SetParametre(m_nNumParametre, m_txtFormule.Formule);
                        m_expressionGraphique.Formule = m_expressionGraphique.Formule;
                    }
                    else
                        externe.Formule = m_txtFormule.Formule;
                    if (OnChangeDessin != null)
                        OnChangeDessin(this, null);
                }
            }
            UpdateCouleurLabel();
        }

        //--------------------------------------------------------------------
        private void UpdateCouleurLabel()
        {
            if (m_txtFormule.Formule == null)
                m_lblNomParametre.BackColor = Color.Red;
            else
                m_lblNomParametre.BackColor = Color.Green;
        }


        //--------------------------------------------------------------------
        private void UpdateWindow()
        {
            if (m_expressionGraphique != null)
            {
                if (m_chkVisible.Checked)
                {
                    CRepresentationExpressionGraphique exp = m_expressionGraphique.GetExterne(m_nNumParametre);
                    m_txtFormule.Formule = exp.FormuleFinale;
                    //m_txtFormule.LockEdition = true;
                }
                else
                {
                    if (m_expressionGraphique.Formule != null)
                    {
                        C2iExpression[] parametres = m_expressionGraphique.Formule.Parametres2i;
                        if (parametres != null && m_nNumParametre >= 0 && m_nNumParametre < parametres.Length)
                            m_txtFormule.Formule = parametres[m_nNumParametre];
                        else
                            m_txtFormule.Formule = null;
                        //m_txtFormule.LockEdition = false;
                        
                    }
                }
            }
            UpdateCouleurLabel();
        }

        private void CEditeurParametreFormule_DragEnter(object sender, DragEventArgs e)
        {
        }

        //--------------------------------------------------------------------
        private CRepresentationExpressionGraphique GetExpressionDragDrop(DragEventArgs e)
        {
            CDonneeDragDropObjetGraphique data = e.Data.GetData(typeof(CDonneeDragDropObjetGraphique)) as CDonneeDragDropObjetGraphique;
            if (data == null)
            {
                List<CDonneeDragDropObjetGraphique> lst = e.Data.GetData(typeof(List<CDonneeDragDropObjetGraphique>)) as List<CDonneeDragDropObjetGraphique>;
                if (lst != null && lst.Count == 1)
                    data = lst[0];
            }

            if (data != null)
            {
                if (data.ObjetDragDrop is CRepresentationExpressionGraphique)
                    return data.ObjetDragDrop as CRepresentationExpressionGraphique;
            }
            return null;
        }

        private void CEditeurParametreFormule_DragOver(object sender, DragEventArgs e)
        {
            if (GetExpressionDragDrop(e) != null)
                e.Effect = DragDropEffects.Link;
        }

        private void CEditeurParametreFormule_DragDrop(object sender, DragEventArgs e)
        {
            CRepresentationExpressionGraphique exp = GetExpressionDragDrop(e);
            if (exp != null)
            {
                m_bIsInitializing = true;
                m_chkVisible.Checked = true;
                m_expressionGraphique.SetExterne(m_nNumParametre, exp);
                UpdateWindow();
                if (OnChangeDessin != null)
                    OnChangeDessin(this, null);
                m_bIsInitializing = false;
                
            }
        }

        






    }
}
