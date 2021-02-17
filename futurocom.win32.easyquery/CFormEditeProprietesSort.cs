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
    public partial class CFormEditeProprietesSort : Form
    {
        private CODEQSort m_tableSort = new CODEQSort();
        private CControleEditeSortColumn m_controleEditeSort = null;

        //-----------------------------------
        public CFormEditeProprietesSort()
        {
            InitializeComponent();
            m_controleEditeSort = new CControleEditeSortColumn();
            m_wndListeSort.ItemControl = m_controleEditeSort;
            m_controleEditeSort.LockEdition = false;
        }

        //-----------------------------------
        public void Init(CODEQSort tableSort)
        {
            m_tableSort = tableSort;
            m_controleEditeSort.SortTable = m_tableSort;
            m_txtNomTable.Text = m_tableSort.NomFinal;
            m_chkUseCache.Checked = m_tableSort.UseCache;
            m_ctrlFormulesNommees.TypeFormuleNomme = typeof(CColonneEQCalculee);
            m_ctrlFormulesNommees.Init(m_tableSort.ColonnesCalculees.ToArray(), typeof(CDataRowForChampCalculeODEQ), m_tableSort);
            FillListeColonnes(m_wndListeColonnesFromSource);
            FillSort();
            m_panelPostFilter.Init(m_tableSort);
        }

        //-----------------------------------
        private void FillListeColonnes(ListView wndListe)
        {
            wndListe.BeginUpdate();
            wndListe.Items.Clear();
            foreach (IColumnDeEasyQuery col in m_tableSort.ColonnesOrdonnees)
            {
                ListViewItem item = new ListViewItem(col.ColumnName);
                item.Tag = col.Id;
                item.SubItems.Add(col.Id);
                wndListe.Items.Add(item);
            }
            wndListe.EndUpdate();
        }

        //-----------------------------------
        private void FillSort()
        {
            List<CItemSort> lstItems = new List<CItemSort>();
            foreach (CODEQSort.CSortColonne sort in m_tableSort.ColonnesDeTri)
            {
                CItemSort item = new CItemSort(sort);
                lstItems.Add(item);
            }
            m_wndListeSort.Items = lstItems.ToArray();
            m_wndListeSort.Refresh();
        }



        //-----------------------------------
        private void CFormEditeProprietesSort_Load(object sender, EventArgs e)
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
            m_tableSort.NomFinal = m_txtNomTable.Text;
            m_tableSort.UseCache = m_chkUseCache.Checked;
            List<string> lstIdsColonnees = new List<string>();
            foreach (ListViewItem item in m_wndListeColonnesFromSource.Items)
            {
                lstIdsColonnees.Add(item.Tag.ToString());
            }
            m_tableSort.OrdreDeColonnes = lstIdsColonnees.ToArray();

            CResultAErreur result = m_wndListeSort.MajChamps();
            if (result)
                result = m_panelPostFilter.MajChamps();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            List<CItemSort> lst = new List<CItemSort>();
            foreach ( CItemSort item in m_wndListeSort.Items)
                lst.Add (item);
            lst.Sort((x, y) => x.Index.CompareTo(y.Index));
            List<CODEQSort.CSortColonne> sorts = new List<CODEQSort.CSortColonne>();
            foreach (CItemSort item in lst)
            {
                if (item.SortColonne.IdColonne != null &&
                    item.SortColonne.IdColonne.Length > 0)
                    sorts.Add(item.SortColonne);
            }
            List<CColonneEQCalculee> lstCalc = new List<CColonneEQCalculee>();
            foreach (CColonneEQCalculee col in m_ctrlFormulesNommees.GetFormules())
                lstCalc.Add(col);
            m_tableSort.ColonnesCalculees = lstCalc;
            m_tableSort.ColonnesDeTri = sorts;
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
        private void m_btnDown_Click(object sender, EventArgs e)
        {
            if (m_wndListeColonnesFromSource.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeColonnesFromSource.SelectedItems[0];
                if (item.Index < m_wndListeColonnesFromSource.Items.Count - 1)
                {
                    int nIndex = item.Index;
                    m_wndListeColonnesFromSource.Items.Remove(item);
                    m_wndListeColonnesFromSource.Items.Insert(nIndex + 1, item);
                }
            }
        }

        //-----------------------------------
        private void m_btnUp_Click(object sender, EventArgs e)
        {
            if (m_wndListeColonnesFromSource.SelectedItems.Count > 0)
            {
                ListViewItem item = m_wndListeColonnesFromSource.SelectedItems[0];
                if (item.Index > 0)
                {
                    int nIndex = item.Index;
                    m_wndListeColonnesFromSource.Items.Remove(item);
                    m_wndListeColonnesFromSource.Items.Insert(nIndex - 1, item);
                }
            }
        }

        //-----------------------------------
        private void m_lnkAddSort_LinkClicked(object sender, EventArgs e)
        {
            CODEQSort.CSortColonne sort = new CODEQSort.CSortColonne();
            CItemSort item = new CItemSort(sort);
            m_wndListeSort.AddItem(item, true);
        }

        //-----------------------------------
        private void m_tabControl_SelectionChanged(object sender, EventArgs e)
        {

        }

        //-----------------------------------
        private void m_btnDownSort_Click(object sender, EventArgs e)
        {
            List<CItemSort> lstItems = new List<CItemSort>();
            foreach (CItemSort item in m_wndListeSort.Items)
                lstItems.Add(item);
            int? nIndex = m_wndListeSort.CurrentItemIndex;
            if (nIndex != null && nIndex < lstItems.Count-1)
            {
                
                CItemSort item = lstItems[nIndex.Value];
                lstItems.Remove(item);
                lstItems.Insert(nIndex.Value + 1, item);
                m_wndListeSort.Items = lstItems.ToArray();
                m_wndListeSort.CurrentItemIndex = item.Index;
            }

        }

        private void m_btnUpSort_Click(object sender, EventArgs e)
        {
            List<CItemSort> lstItems = new List<CItemSort>();
            foreach (CItemSort item in m_wndListeSort.Items)
                lstItems.Add(item);
            int? nIndex = m_wndListeSort.CurrentItemIndex;
            if (nIndex != null && nIndex >0 )
            {

                CItemSort item = lstItems[nIndex.Value];
                lstItems.Remove(item);
                lstItems.Insert(nIndex.Value - 1, item);
                m_wndListeSort.Items = lstItems.ToArray();
                m_wndListeSort.CurrentItemIndex = item.Index;
            }
        }

        //------------------------------------------------------------------------
        private void m_lnkRemoveSort_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeSort.CurrentItemIndex != null)
            {
                if (CFormAlerte.Afficher(
                    I.T("Delete ?|20062"),
                    EFormAlerteType.Question) == DialogResult.Yes)
                {
                    int? nIndex = m_wndListeSort.CurrentItemIndex;
                    m_wndListeSort.RemoveItem(nIndex.Value, true);
                }
            }
        }
    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesSort : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQSort), typeof(CEditeurProprietesSort));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQSort sort = objet as CODEQSort;
            if (sort == null)
                return false;
            CFormEditeProprietesSort form = new CFormEditeProprietesSort();
            form.Init(sort);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }



        #endregion
    }

    
}
