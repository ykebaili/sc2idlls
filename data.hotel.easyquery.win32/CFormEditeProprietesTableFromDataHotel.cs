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
using futurocom.easyquery.CAML;
using data.hotel.easyquery;
using data.hotel.easyquery.win32.calcul;

namespace futurocom.win32.easyquery
{
    public partial class CFormEditeProprietesTableFromDataHotel : Form
    {
        private CODEQTableFromDataHotel m_tableFromDataHotel = null;

        //---------------------------------------------------------------
        public CFormEditeProprietesTableFromDataHotel()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------
        public void Init(CODEQTableFromDataHotel obj)
        {
            m_tableFromDataHotel = obj;
            if (m_tableFromDataHotel.TableDefinition != null)
                m_lblSource.Text = m_tableFromDataHotel.TableDefinition.TableName;
            else
                m_lblSource.Text = "?";
            m_txtNomTable.Text = m_tableFromDataHotel.NomFinal;
            m_chkUseCache.Checked = m_tableFromDataHotel.UseCache;
            FillListeColonnes();
            FillListeFormulesNommees();
            m_panelOptions.Init(m_tableFromDataHotel);
        }

        //---------------------------------------------------------------
        private void FillListeFormulesNommees()
        {
            m_ctrlFormulesNommees.TypeFormuleNomme = typeof(CColonneEQCalculee);
            m_ctrlFormulesNommees.Init(m_tableFromDataHotel.ColonnesCalculees.ToArray(), typeof(CDataRowForChampCalculeODEQ), m_tableFromDataHotel);
        }

        //---------------------------------------------------------------
        private void FillListeColonnes()
        {
            if (m_tableFromDataHotel.TableDefinition != null)
            {
                foreach (IColumnDefinition col in m_tableFromDataHotel.TableDefinition.Columns)
                {
                    ListViewItem item = new ListViewItem(col.ColumnName);
                    IColumnDeEasyQuery colFromDataHotel = m_tableFromDataHotel.GetColonneFor(col);
                    if (colFromDataHotel != null)
                    {
                        item.Text = colFromDataHotel.ColumnName;
                        item.Checked = true;
                    }
                    item.SubItems.Add(col.ColumnName);
                    item.Tag = col;
                    m_wndListeColonnes.Items.Add(item);
                }
            }
            foreach (IColumnDeEasyQuery col in m_tableFromDataHotel.Columns)
            {
                if (col is CColonneCalculeeDataHotel)
                {
                    ListViewItem item = new ListViewItem(col.ColumnName);
                    FillItemCalcul(item, (CColonneCalculeeDataHotel)col);
                    m_wndListeColonnes.Items.Add(item);
                }
            }
        }

        //--------------------------------------------------------------
        private void FillItemCalcul(ListViewItem item, CColonneCalculeeDataHotel col)
        {
            item.Text = col.ColumnName;
            item.Tag = col;
            item.SubItems.Add(I.T("Calculation|20050"));
            item.Checked = true;
            item.BackColor = Color.LightGray;
        }

        //---------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            if (m_txtNomTable.Text.Length == 0)
            {
                MessageBox.Show(I.T("Please enter a table name|20004"));
                return;
            }



            m_tableFromDataHotel.NomFinal = m_txtNomTable.Text;
            m_tableFromDataHotel.UseCache = m_chkUseCache.Checked;
            List<IColumnDeEasyQuery> lst = new List<IColumnDeEasyQuery>();
            foreach (ListViewItem item in m_wndListeColonnes.Items)
                if (item.Checked)
                {
                    IColumnDefinition colFromSource = item.Tag as IColumnDefinition;
                    if (colFromSource != null)
                    {
                        IColumnDeEasyQuery newCol = m_tableFromDataHotel.GetColonneFor(colFromSource);
                        if (newCol == null)
                            newCol = new CColumnEQFromSource(colFromSource);
                        newCol.ColumnName = item.Text;
                        lst.Add(newCol);
                    }
                    CColonneCalculeeDataHotel colCalc = item.Tag as CColonneCalculeeDataHotel;
                    if ( colCalc != null )
                    {
                        colCalc.ColumnName = item.Text;
                        lst.Add(colCalc);
                    }
                }
            m_tableFromDataHotel.SetColonnesOrCalculees(lst);

            List<CColonneEQCalculee> colsCalc = new List<CColonneEQCalculee>();
            foreach (CColonneEQCalculee col in m_ctrlFormulesNommees.GetFormules())
                colsCalc.Add(col);

            m_tableFromDataHotel.ColonnesCalculees = colsCalc;

            CResultAErreur result = m_panelOptions.MajChamps(m_tableFromDataHotel);
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        //---------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //---------------------------------------------------------------
        private void CFormEditeProprietesTableFromDataHotel_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        //---------------------------------------------------------------
        private void m_menuChamp_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if ( m_wndListeColonnes.SelectedItems.Count == 1 )
            {
                ListViewItem item = m_wndListeColonnes.SelectedItems[0];
                CColonneCalculeeDataHotel col = item != null ? item.Tag as CColonneCalculeeDataHotel : null;
                if (col != null)
                    e.Cancel = false;
            }
        }


        //------------------------------------------------------------------
        private void m_btnAddField_LinkClicked(object sender, EventArgs e)
        {
            CColonneCalculeeDataHotel col = new CColonneCalculeeDataHotel();
            if (CFormEditeColonneCalculeeDataHotel.EditeColonne(col, m_tableFromDataHotel, m_tableFromDataHotel.Query))
            {
                ListViewItem item = new ListViewItem();
                FillItemCalcul(item, col);
                m_wndListeColonnes.Items.Add(item);
            }
        }

        private void m_tabControl_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void m_menuSupprimerChamp_Click(object sender, EventArgs e)
        {
            if ( m_wndListeColonnes.SelectedItems.Count == 1 )
            {
                ListViewItem item = m_wndListeColonnes.SelectedItems[0];
                if (item.Tag is CColonneCalculeeDataHotel)
                    m_wndListeColonnes.Items.Remove(item);
            }
        }

        private void m_menuProprietes_Click(object sender, EventArgs e)
        {
            if (m_wndListeColonnes.SelectedItems.Count == 1)
            {
                ListViewItem item = m_wndListeColonnes.SelectedItems[0];
                CColonneCalculeeDataHotel col = item.Tag as CColonneCalculeeDataHotel;
                if (col != null && CFormEditeColonneCalculeeDataHotel.EditeColonne(col, m_tableFromDataHotel, m_tableFromDataHotel.Query))
                {
                    FillItemCalcul(item, col);
                }
            }
        }


    }


    [AutoExec("Autoexec")]
    public class CEditeurProprietesTableFromDataHotel : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQTableFromDataHotel), typeof(CEditeurProprietesTableFromDataHotel));
        }


        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQTableFromDataHotel tableFromDataHotel = objet as CODEQTableFromDataHotel;
            if (tableFromDataHotel == null)
                return false;
            CFormEditeProprietesTableFromDataHotel form = new CFormEditeProprietesTableFromDataHotel();
            form.Init(tableFromDataHotel);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }
    }
}
