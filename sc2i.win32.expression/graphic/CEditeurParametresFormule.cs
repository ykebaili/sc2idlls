using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.win32.expression.graphic;
using sc2i.win32.common;

namespace sc2i.win32.expression
{
    public partial class CEditeurParametresFormule : UserControl, IEditeurParametresFormule
    {
        private List<CEditeurParametreFormule> m_reserve = new List<CEditeurParametreFormule>();

        private CRepresentationExpressionGraphique m_expressionEditee = null;
        private IFournisseurProprietesDynamiques m_fournisseur;
        private CObjetPourSousProprietes m_objetAnalyse = null;

        public CEditeurParametresFormule()
        {
            InitializeComponent();
        }

        public void Init(CRepresentationExpressionGraphique exp, IFournisseurProprietesDynamiques fournisseur,
            CObjetPourSousProprietes objetAnalyse)
        {
            m_fournisseur = fournisseur;
            m_objetAnalyse = objetAnalyse;
            m_panelControls.SuspendDrawing();
            try
            {
                m_bIsInitialising = true;
                m_expressionEditee = exp;

                m_txtFormule.Init(fournisseur, objetAnalyse);

                m_txtFormule.Formule = exp.Formule;


                foreach (Control ctrl in m_panelControls.Controls)
                {
                    CEditeurParametreFormule editeur = ctrl as CEditeurParametreFormule;
                    if (editeur != null && !m_reserve.Contains(editeur))
                    {
                        editeur.Visible = false;
                        m_reserve.Add(editeur);
                    }
                }
                if (exp.Formule == null)
                {
                    m_panelControls.Visible = false;
                    return;
                }
                m_panelControls.Visible = true;

                
                C2iExpressionAnalysable formule = exp.Formule as C2iExpressionAnalysable;
                if (formule != null)
                {
                    int nNbParametres = 0;
                    CInfo2iExpression info = formule.GetInfos();
                    foreach (CInfo2iDefinitionParametres par in info.InfosParametres)
                    {
                        nNbParametres = Math.Max(par.TypesDonnees.Length, nNbParametres);
                    }
                    for (int nParam = 0; nParam < nNbParametres; nParam++)
                    {
                        CEditeurParametreFormule editeur = null;
                        editeur = GetNewEditeur();
                        editeur.Dock = DockStyle.Top;
                        editeur.BringToFront();
                        editeur.Init(exp, nParam, fournisseur, objetAnalyse);
                        editeur.Visible = true;
                    }
                }
                else
                {
                    int nParam = 0;
                    foreach (C2iExpression f in exp.Formule.Parametres2i)
                    {
                        CEditeurParametreFormule editeur = null;
                        editeur = GetNewEditeur();
                        editeur.Dock = DockStyle.Top;
                        editeur.BringToFront();
                        editeur.Init(exp, nParam, fournisseur, objetAnalyse);
                        editeur.Visible = true;
                        nParam++;
                    }
                }
            }
            catch { }
            finally
            {
                m_bIsInitialising = false;
                m_panelControls.ResumeDrawing();
            }
            RecalcObjetsAnalyse();
        }

        private CEditeurParametreFormule GetNewEditeur()
        {
            CEditeurParametreFormule editeur;
            if (m_reserve.Count > 0)
            {
                editeur = m_reserve[0];
                m_reserve.RemoveAt(0);
            }
            else
            {
                editeur = new CEditeurParametreFormule();
                m_panelControls.Controls.Add(editeur);
                editeur.Parent = m_panelControls;
                editeur.OnChangeDessin += new EventHandler(editeur_OnChangeDessin);
            }
            return editeur;
        }

        public event EventHandler OnChangeDessin;

        private bool m_bIsInitialising = false;
        void editeur_OnChangeDessin(object sender, EventArgs e)
        {
            m_timerDelayUpdate.Stop();
            m_timerDelayUpdate.Start();
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void m_txtFormule_OnChangeTexteFormule(object sender, EventArgs e)
        {
            if (!m_bIsInitialising )
            {
                m_timerDelayInterprete.Stop();
                m_timerDelayInterprete.Start();
            }
            



        }

        private void m_timerDelayUpdate_Tick(object sender, EventArgs e)
        {
            m_timerDelayUpdate.Stop();
            if (OnChangeDessin != null)
            {
                OnChangeDessin(sender, e);
                m_bIsInitialising = true;
                m_txtFormule.Formule = m_expressionEditee.Formule;
                m_bIsInitialising = false;
            }
        }

        private void m_timerDelayInterprete_Tick(object sender, EventArgs e)
        {
            m_timerDelayInterprete.Stop();
            if (m_txtFormule.Formule != null)
            {
                m_expressionEditee.Formule = m_txtFormule.Formule;
                m_expressionEditee.LastErreur = "";
                C2iExpression[] parametres = m_txtFormule.Formule.Parametres2i;
                Dictionary<int, CEditeurParametreFormule> parametresVus = new Dictionary<int, CEditeurParametreFormule>();
                List<CEditeurParametreFormule> lstEditeurs = new List<CEditeurParametreFormule>();
                foreach (Control ctrl in m_panelControls.Controls)
                {
                    CEditeurParametreFormule editeur = ctrl as CEditeurParametreFormule;
                    if (editeur != null && !m_reserve.Contains(editeur))//Ce n'est pas un éditeur de reserve !
                    {
                        parametresVus[editeur.NumParametre] = editeur;
                        editeur.ReloadParametre();
                        lstEditeurs.Add(editeur);
                    }
                }
                m_panelControls.Visible = true;
                for (int n = 0; n < parametres.Length; n++)
                {
                    if (!parametresVus.ContainsKey(n))
                    {
                        CEditeurParametreFormule editeur = GetNewEditeur();
                        editeur.Dock = DockStyle.Top;
                        editeur.BringToFront();
                        editeur.Init(m_expressionEditee, n, m_fournisseur, m_objetAnalyse);
                        editeur.Visible = true;
                        parametresVus[n] = editeur;
                        lstEditeurs.Add(editeur);
                    }
                }
                RecalcObjetsAnalyse();
                //Supprime les éditeurs superflus
                //tri les éditeurs
                lstEditeurs.Sort((x, y) => x.NumParametre.CompareTo(y.NumParametre));
                foreach (CEditeurParametreFormule editeur in lstEditeurs)
                    editeur.BringToFront();
                if (lstEditeurs.Count > 0)
                {
                    CEditeurParametreFormule editeur = lstEditeurs[lstEditeurs.Count - 1];
                    while (editeur != null && editeur.NumParametre >= parametres.Length && editeur.Formule == null)
                    {
                        editeur.Visible = false;
                        m_reserve.Add(editeur);
                        lstEditeurs.Remove(editeur);
                        if (lstEditeurs.Count > 0)
                            editeur = lstEditeurs[lstEditeurs.Count - 1];
                        else
                            editeur = null;
                    }
                }

                if (OnChangeDessin != null)
                    OnChangeDessin(this, null);
            }
        }

        private void RecalcObjetsAnalyse ( )
        {
            C2iExpressionObjet expObjet = m_expressionEditee.Formule as C2iExpressionObjet;
            foreach ( Control ctrl in m_panelControls.Controls )
            {
                CEditeurParametreFormule editeur = ctrl as CEditeurParametreFormule;
                if ( editeur != null )
                    editeur.ChangeObjetAnalyse ( m_objetAnalyse );
                if ( expObjet != null && expObjet.Parametres.Count >= 2)
                {
                    C2iExpression exp1 = expObjet.Parametres2i[0];
                    C2iExpressionObjetNeedTypeParent exp2 = expObjet.Parametres2i[1] as C2iExpressionObjetNeedTypeParent;
                    if ( exp2 != null && exp1 != null )
                        editeur.ChangeObjetAnalyse ( exp1.GetObjetPourSousProprietes());
                }
            }
        }
    }
}
