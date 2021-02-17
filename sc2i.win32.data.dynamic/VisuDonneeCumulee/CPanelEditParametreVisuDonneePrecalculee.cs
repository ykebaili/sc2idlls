using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sc2i.data.dynamic;
using sc2i.win32.data;
using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.data.dynamic
{
    public partial class CPanelEditParametreVisuDonneePrecalculee : UserControl, IControlALockEdition
    {
        private CParametreVisuDonneePrecalculee m_parametre = null;
        public CPanelEditParametreVisuDonneePrecalculee()
        {
            InitializeComponent();
        }

        public void Init(CParametreVisuDonneePrecalculee parametre)
        {
            if (parametre == null)
                m_parametre = new CParametreVisuDonneePrecalculee();
            else
                m_parametre = CCloner2iSerializable.Clone ( parametre ) as CParametreVisuDonneePrecalculee;
            InitComboTypeDonnee();
            InitFromParametre();
        }

        private void InitFromParametre(  )
        {
            CTypeDonneeCumulee typeDonnee = new CTypeDonneeCumulee(CSc2iWin32DataClient.ContexteCourant);
            if (m_parametre.IdTypeDonneeCumulee != null)
                if (!typeDonnee.ReadIfExists(m_parametre.IdTypeDonneeCumulee.Value))
                    typeDonnee = null;
            if (typeDonnee == null)
            {
                m_tabControl.Visible = false;
                if ( m_cmbTypeDonnee.ElementSelectionne != null )
                    m_cmbTypeDonnee.ElementSelectionne = null;
                return;
            }
            m_tabControl.Visible = true;
            m_cmbTypeDonnee.ElementSelectionne = typeDonnee;
            DataTable table = m_parametre.GetDataTableModelePourParametrage(CSc2iWin32DataClient.ContexteCourant);
            if (table != null)
            {
                m_panelTableauCroise.InitChamps(table, m_parametre.TableauCroise);
                m_panelTableauCroise.Enabled = true;
            }
            else
                m_panelTableauCroise.Enabled = false;

            m_panelFormatHeader.Init(m_parametre.FormatHeader, null, true);
            m_panelFormatRows.Init(m_parametre.FormatRows, null, false);

            InitFormatsChamps();

            InitFiltres(m_panelFiltresDeBase, m_parametre.FiltresDeBase, false);
            InitFiltres(m_panelFiltresUser, m_parametre.FiltresUtilisateur, true);

            m_cmbOperation.ValueMember = "Valeur";
            m_cmbOperation.DisplayMember = "Libelle";
            m_cmbOperation.DataSource = CUtilSurEnum.GetCouplesFromEnum(typeof(OperationsAgregation));
            m_cmbOperation.SelectedValue = (int)m_parametre.OperationCumul;
            m_txtLibelleTotal.Text = m_parametre.LibelleTotal;
            m_chkShowExportButton.Checked = m_parametre.ShowExportButton;
            m_chkShowHeader.Checked = m_parametre.ShowHeader;
        }

        private void InitFiltres(
            Panel panelParent, 
            CFiltreDonneePrecalculee[] filtres,
            bool bAvecInterface)
        {
            panelParent.SuspendDrawing();
            foreach (Control ctrl in panelParent.Controls)
            {
                ctrl.Visible = false;
                ctrl.Dispose();
            }
            panelParent.Controls.Clear();
            foreach (CFiltreDonneePrecalculee filtre in filtres)
            {
                CPanelFiltreDonneePrecalculee panel = new CPanelFiltreDonneePrecalculee();
                panel.Init(filtre, bAvecInterface, bAvecInterface);
                panel.Parent = panelParent;
                //m_panelFiltres.Controls.Add(panel);
                panel.Dock = DockStyle.Top;
                panel.BringToFront();
            }
            panelParent.ResumeDrawing();
        }


        private void InitFormatsChamps()
        {
            m_wndListeChamps.Items.Clear();
            foreach (CChampFinalDeTableauCroise champ in m_parametre.TableauCroise.ChampsFinaux)
            {
                ListViewItem item = new ListViewItem(champ.NomChamp);
                item.Tag = champ;
                m_wndListeChamps.Items.Add(item);
            }
        }

        public void MajChamps()
        {
            m_panelFormatHeader.MajChamps();
            m_panelFormatRows.MajChamps();
            m_panelFormatChamp.MajChamps();
            foreach (Control ctrl in m_panelFiltresUser.Controls)
            {
                CPanelFiltreDonneePrecalculee panel = ctrl as CPanelFiltreDonneePrecalculee;
                if (panel != null)
                    panel.MajChamps();
            }
            if (m_cmbOperation.SelectedValue is int)
                m_parametre.OperationCumul = (OperationsAgregation)m_cmbOperation.SelectedValue;
            m_parametre.LibelleTotal = m_txtLibelleTotal.Text;
            m_parametre.ShowExportButton = m_chkShowExportButton.Checked;
            m_parametre.ShowHeader = m_chkShowHeader.Checked;
        }

        public CParametreVisuDonneePrecalculee Parametre
        {
            get
            {
                return m_parametre;
            }
        }


        private void InitComboTypeDonnee()
        {
            m_cmbTypeDonnee.Init(typeof(CTypeDonneeCumulee), "Libelle", false);
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());

            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        private void m_cmbTypeDonnee_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CTypeDonneeCumulee typeDonnee = m_cmbTypeDonnee.ElementSelectionne as CTypeDonneeCumulee;
            if (typeDonnee != null)
                m_parametre.IdTypeDonneeCumulee = typeDonnee.Id;
            else
                m_parametre.IdTypeDonneeCumulee = null;
            InitFromParametre();
        }

        private void m_lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InitFormatsChamps();
        }

        private void m_wndListeChamps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_wndListeChamps.SelectedItems.Count == 0)
                m_panelFormatChamp.Visible = false;
            else
            {
                m_panelFormatChamp.Visible = true;
                CChampFinalDeTableauCroise champ = m_wndListeChamps.SelectedItems[0].Tag as CChampFinalDeTableauCroise;
                if ( champ != null )
                    m_panelFormatChamp.Init ( 
                        m_parametre.GetParametreForchamp ( champ ),
                        m_parametre,
                        CSc2iWin32DataClient.ContexteCourant);
                else
                    m_panelFormatChamp.Visible = false;
            }
        }

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = I.T("Visualisation option (*.2IVisu)|*.2iVisu|All files (*.*)|*.*|20011");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                MajChamps();
                string strNomFichier = dlg.FileName;
                CResultAErreur result = m_parametre.SaveToFile(strNomFichier);
                if (!result)
                    CFormAlerte.Afficher(result);
                else
                    CFormAlerte.Afficher(I.T("Save successful|260"));
            }
        }

        private void m_btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = I.T("Visualisation option (*.2IVisu)|*.2iVisu|All files (*.*)|*.*|20011");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (CFormAlerte.Afficher(I.T("Current visualisation options will be replaced.  Continue?|20012"),
                    EFormAlerteType.Question) == DialogResult.No)
                    return;
                CParametreVisuDonneePrecalculee parametres = new CParametreVisuDonneePrecalculee();
                CResultAErreur result = parametres.ReadFromFile(dlg.FileName);
                if (!result)
                    CFormAlerte.Afficher(result);
                else
                {
                    m_parametre = parametres;
                    Init(parametres);
                }
            }
        }

        private void tabPage4_PropertyChanged(Crownwood.Magic.Controls.TabPage page, Crownwood.Magic.Controls.TabPage.Property prop, object oldValue)
        {

        }

        private void m_cmbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_panelLibelleTotal.Visible = m_cmbOperation.SelectedValue != null &&
                ((OperationsAgregation)m_cmbOperation.SelectedValue) != OperationsAgregation.None;
        }
    }
}
