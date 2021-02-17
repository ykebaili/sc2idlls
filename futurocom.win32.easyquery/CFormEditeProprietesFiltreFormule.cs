using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.easyquery;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;

namespace futurocom.win32.easyquery
{
    public partial class CFormEditeProprietesFiltreFormule : Form
    {
        private CODEQFiltre m_tableFiltre = new CODEQFiltre();
        
        public CFormEditeProprietesFiltreFormule()
        {
            InitializeComponent();
        }

        public void Init(CODEQFiltre tableFiltre)
        {
            m_tableFiltre = tableFiltre;
            m_wndAide.FournisseurProprietes = m_tableFiltre.TableSource;
            m_wndAide.ObjetInterroge = new CObjetPourSousProprietes(m_tableFiltre.TableSource);
            m_txtEditeFormule.Init(m_tableFiltre.TableSource, new CObjetPourSousProprietes(m_tableFiltre.TableSource));
            m_txtEditeFormule.Formule = tableFiltre.Formule;
            m_txtNomTable.Text = tableFiltre.NomFinal;
            m_chkUseCache.Checked = tableFiltre.UseCache;
            m_ctrlFormulesNommees.TypeFormuleNomme = typeof(CColonneEQCalculee);
            m_ctrlFormulesNommees.Init(m_tableFiltre.ColonnesCalculeesFromSource.ToArray(), typeof(CDataRowForChampCalculeODEQ), m_tableFiltre.TableSource );
            FillListeColonnes(m_wndListeColonnesFromSource, m_tableFiltre.TableSource);
            m_chkInclureTout.Checked = m_tableFiltre.InclureToutesLesColonnesSource;
            m_panelPostFilter.Init(tableFiltre);
        }

        //-----------------------------------
        private void FillListeColonnes(ListView wndListe, IObjetDeEasyQuery table)
        {
            wndListe.Items.Clear();
            if (table == null)
                return;
            foreach (IColumnDeEasyQuery col in table.Columns)
            {
                ListViewItem item = new ListViewItem(col.ColumnName);
                item.SubItems.Add(col.ColumnName);
                IColumnDeEasyQuery colFinale = m_tableFiltre.GetColonneFinaleFor(col);
                if (colFinale != null)
                {
                    item.Text = colFinale.ColumnName;
                    item.Checked = true;
                    colFinale.DataType = col.DataType;
                }
                item.Tag = col;
                wndListe.Items.Add(item);
            }
        }

        //-----------------------------------
        private void m_wndAide_OnSendCommande(string strCommande, int nPosCurseur)
        {
            m_wndAide.InsereInTextBox(m_txtEditeFormule.TextBox, nPosCurseur, strCommande);
        }

        //-----------------------------------
        private void CFormEditeProprietesFiltreFormule_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        //-----------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20004"));
                return;
            }
            CResultAErreur result = CResultAErreur.True;
            if (m_txtEditeFormule.Formule == null)
            {
                result = m_txtEditeFormule.ResultAnalyse;
                if (result == null)
                    result = CResultAErreur.True;
                result.EmpileErreur(I.T("Error in the formula|20011"));
                CFormAfficheErreur.Show(result.Erreur);
                return;
            }
            m_tableFiltre.NomFinal = m_txtNomTable.Text;
            m_tableFiltre.UseCache = m_chkUseCache.Checked;
            m_tableFiltre.Formule = m_txtEditeFormule.Formule;
            m_tableFiltre.InclureToutesLesColonnesSource = m_chkInclureTout.Checked;

            if (!m_tableFiltre.InclureToutesLesColonnesSource)
            {
                List<CColumnEQFromSource> lstColonnes = new List<CColumnEQFromSource>();
                foreach (ListViewItem item in m_wndListeColonnesFromSource.Items)
                {
                    if (item.Checked)
                    {
                        IColumnDeEasyQuery col = item.Tag as IColumnDeEasyQuery;
                        if (col != null)
                        {
                            bool bColExiste = false;
                            foreach (IColumnDeEasyQuery colEx in m_tableFiltre.ColonnesFromSource)
                            {
                                CColumnEQFromSource colExSrc = colEx as CColumnEQFromSource;
                                if (colExSrc != null && colExSrc.IdColumnSource == col.Id)
                                {
                                    colEx.ColumnName = item.Text;
                                    lstColonnes.Add(colExSrc);
                                    bColExiste = true;
                                    break;
                                }
                            }
                            if (!bColExiste)
                            {
                                CColumnEQFromSource newCol = new CColumnEQFromSource(col);
                                newCol.ColumnName = item.Text;
                                lstColonnes.Add(newCol);
                            }
                        }
                    }
                }
                m_tableFiltre.ColonnesFromSource = lstColonnes.AsReadOnly();
            }
            else
                m_tableFiltre.ColonnesFromSource = null;

            result = m_panelPostFilter.MajChamps();
            if ( !result )
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }


            List<CColonneEQCalculee> lst = new List<CColonneEQCalculee>();
            foreach (CColonneEQCalculee col in m_ctrlFormulesNommees.GetFormules())
                lst.Add(col);
            m_tableFiltre.ColonnesCalculeesFromSource = lst;
            DialogResult = DialogResult.OK;
            Close();
        }

        //-----------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //-----------------------------------
        private void m_chkInclureTout_CheckedChanged(object sender, EventArgs e)
        {
            m_wndListeColonnesFromSource.Visible = !m_chkInclureTout.Checked;
        }
    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesFiltreFormule : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQFiltre), typeof(CEditeurProprietesFiltreFormule));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQFiltre filtre = objet as CODEQFiltre;
            if (filtre == null)
                return false;
            CFormEditeProprietesFiltreFormule form = new CFormEditeProprietesFiltreFormule();
            form.Init(filtre);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }



        #endregion
    }
}
