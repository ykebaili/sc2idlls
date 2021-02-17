using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.chart;
using sc2i.win32.common;
using sc2i.common;
using System.Collections;

namespace futurocom.win32.chart.series
{
    public partial class CFormEditeFournisseurValeur : Form
    {
        private CChartSetup m_chartSetup = null;
        private IFournisseurValeursSerie m_fournisseurValeurs = null;
        private IEditeurFournisseurValeursSerieDeTypeConnu m_editeurEnCours = null;

        //-------------------------------------------------------------------
        public CFormEditeFournisseurValeur()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //-------------------------------------------------------------------
        public static bool EditeFournisseur(
            CChartSetup chart,
            ref IFournisseurValeursSerie fournisseur)
        {
            using (CFormEditeFournisseurValeur form = new CFormEditeFournisseurValeur())
            {
                form.m_chartSetup = chart;
                form.m_fournisseurValeurs = fournisseur;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    fournisseur = form.m_fournisseurValeurs;
                    return true;
                }
                return false;
            }
        }


        //-------------------------------------------------------------------
        private void CFormEditeFournisseurValeur_Load(object sender, EventArgs e)
        {
            m_cmbSource.BeginUpdate();
            m_cmbSource.Items.Clear();
            m_cmbSource.ListDonnees = m_chartSetup.ParametresDonnees.ParametresSourceFV;
            m_cmbSource.ProprieteAffichee = "SourceName";
            m_cmbSource.EndUpdate();
            if (m_fournisseurValeurs != null && m_fournisseurValeurs.SourceId != null)
                m_cmbSource.SelectedValue = m_chartSetup.ParametresDonnees.GetSourceFV(m_fournisseurValeurs.SourceId);

            FillListeTypesFournisseursPossibles();
            
            ShowDetailFournisseur();
        }

        //-------------------------------------------------------------------
        private class CDescriptionFournisseurValeurs
        {
            private string m_strLibelle = "";
            private Type m_type = null;

            public CDescriptionFournisseurValeurs(string strLibelle, Type type)
            {
                m_strLibelle = strLibelle;
                m_type = type;
            }
            public string Libelle
            {
                get
                {
                    return m_strLibelle;
                }
            }
            public Type Type
            {
                get
                {
                    return m_type;
                }
            }
        }
        
        //-------------------------------------------------------------------
        private void FillListeTypesFournisseursPossibles()
        {
            m_cmbTypeExtracteur.BeginUpdate();
            m_cmbTypeExtracteur.Items.Clear();
            string strIdSource = GetIdSourceSelectionnee();

            if (strIdSource != null)
            {
                CParametreSourceChart p = m_chartSetup.ParametresDonnees.GetSourceFV(strIdSource);
                if (p != null)
                {
                    foreach (Type tp in CGestionnaireFournisseursValeursSerie.GetTypesFournisseursConnus())
                    {
                        IFournisseurValeursSerie f = Activator.CreateInstance(tp) as IFournisseurValeursSerie;
                        if (f != null && f.IsApplicableToSource(p))
                        {
                            CDescriptionFournisseurValeurs desc = new CDescriptionFournisseurValeurs(f.LabelType, tp);
                            m_cmbTypeExtracteur.Items.Add(desc);
                        }
                    }
                }
            }
            m_cmbTypeExtracteur.DisplayMember = "Libelle";
            m_cmbTypeExtracteur.ValueMember = "Type";
            m_cmbTypeExtracteur.EndUpdate();
            if (m_fournisseurValeurs != null)
            {
                for (int n = 0; n < m_cmbTypeExtracteur.Items.Count; n++)
                {
                    CDescriptionFournisseurValeurs desc = m_cmbTypeExtracteur.Items[n] as CDescriptionFournisseurValeurs;
                    if (desc != null && desc.Type == m_fournisseurValeurs.GetType())
                    {
                        m_cmbTypeExtracteur.SelectedIndex = n;
                        break;
                    }
                }
            }
               // m_cmbTypeExtracteur.SelectedValue = m_fournisseurValeurs.GetType();
            ShowDetailFournisseur();
        }


        //-------------------------------------------------------------------
        private string GetIdSourceSelectionnee()
        {
            if (m_cmbSource.SelectedIndex >= 0)
            {

                CParametreSourceChart p = m_cmbSource.SelectedValue as CParametreSourceChart;
                if (p != null)
                    return p.SourceId;
            }
            return null;
        }

        //-------------------------------------------------------------------
        private void ShowDetailFournisseur()
        {
            CDescriptionFournisseurValeurs desc = null;
            if (m_cmbTypeExtracteur.SelectedIndex >= 0)
                desc = m_cmbTypeExtracteur.Items[m_cmbTypeExtracteur.SelectedIndex] as CDescriptionFournisseurValeurs;
            if ( desc == null )
            {
                if (m_editeurEnCours != null)
                {
                    ((Control)m_editeurEnCours).Visible = false;
                    m_panelDetailExtracteur.Controls.Remove((Control)m_editeurEnCours);
                    ((Control)m_editeurEnCours).Dispose();
                    m_editeurEnCours = null;
                }
                return;
            }
                    
            if (m_fournisseurValeurs == null || m_fournisseurValeurs.GetType() != desc.Type)
            {
                m_fournisseurValeurs = Activator.CreateInstance(desc.Type) as IFournisseurValeursSerie;
            }

            m_panelDetailExtracteur.SuspendDrawing();
            if (m_fournisseurValeurs != null)
            {
                string strId = GetIdSourceSelectionnee();
                if (strId == null)
                    m_fournisseurValeurs = null;
                else
                    m_fournisseurValeurs.SourceId = strId;
                Type tp = CGestionnaireEditeursFournisseursValeurs.GetTypeEditeur(m_fournisseurValeurs.GetType());
                if ( tp != null && (m_editeurEnCours == null || m_editeurEnCours.GetType() != tp ))
                {
                    if ( m_editeurEnCours != null )
                    {
                        ((Control)m_editeurEnCours).Visible = false;
                        m_panelDetailExtracteur.Controls.Remove ( ((Control)m_editeurEnCours));
                        ((Control)m_editeurEnCours).Dispose();
                        m_editeurEnCours = null;
                    }
                    m_editeurEnCours = Activator.CreateInstance ( tp, new object[0] ) as IEditeurFournisseurValeursSerieDeTypeConnu;
                    if ( m_editeurEnCours != null )
                    {
                        Control ctrl = m_editeurEnCours as Control;
                        m_panelDetailExtracteur.Controls.Add ( ctrl );
                        ctrl.Dock = DockStyle.Fill;
                    }
                }
                if ( m_editeurEnCours != null )
                {
                    m_editeurEnCours.InitChamps ( m_chartSetup, m_fournisseurValeurs );
                }
            }
            m_panelDetailExtracteur.ResumeDrawing();

        }

        //-------------------------------------------------------------------
        private void m_cmbTypeExtracteur_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowDetailFournisseur();
        }

        //-------------------------------------------------------------------
        private void m_cmbSource_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillListeTypesFournisseursPossibles();
        }

        //-------------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //-------------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (GetIdSourceSelectionnee() == null)
                m_fournisseurValeurs = null;
            else
            {
                if (m_editeurEnCours != null)
                {
                    CParametreSourceChart p = m_cmbSource.SelectedValue as CParametreSourceChart;
                    if (p != null)
                        m_fournisseurValeurs.SourceId = p.SourceId;
                    CResultAErreur result = m_editeurEnCours.MajChamps();
                    if (!result)
                    {
                        CFormAlerte.Afficher(result.Erreur);
                        return;
                    }
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        //-------------------------------------------------------------------
        private void m_lnkVoirDonnees_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_editeurEnCours != null)
            {
                CResultAErreur result = m_editeurEnCours.MajChamps();
                if (!result)
                {
                    CFormAlerte.Afficher(result.Erreur);
                    return;
                }
                object[] valeurs = m_fournisseurValeurs.GetValues(m_chartSetup);
            }
        }

            
    }

    //-------------------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurFournisseurValeursSerie : IEditeurFournisseurValeursSerie
    {

        //------------------------------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CFournisseurValeursSerieEditor.SetTypeEditeur(typeof(CEditeurFournisseurValeursSerie));
        }

        //------------------------------------------------------------------------------------------------------
        public IFournisseurValeursSerie EditeFournisseur(CChartSetup chartSetup, IFournisseurValeursSerie fournisseur)
        {
            IFournisseurValeursSerie copie = fournisseur;
            if (CFormEditeFournisseurValeur.EditeFournisseur(chartSetup, ref copie))
                return copie;
            return fournisseur;
        }
    }

}
