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
using futurocom.easyquery.staticDataSet;

namespace futurocom.win32.easyquery
{
    public partial class CFormEditeProprietesTableManuelle : Form
    {
        private CODEQTableManuelle m_tableManuelle = new CODEQTableManuelle();

        //-----------------------------------
        public CFormEditeProprietesTableManuelle()
        {
            InitializeComponent();
            m_wndListeColumns.ItemControl = new CControleEditeColumnSimple();
            m_wndListeColumns.ItemControl.LockEdition = false;
        }

        //-----------------------------------
        public void Init(CODEQTableManuelle tableManuelle)
        {
            m_tableManuelle = tableManuelle;
            m_txtNomTable.Text = m_tableManuelle.NomFinal;
            FillListeColonnes();
        }

        //-----------------------------------
        private void FillListeColonnes()
        {
            List<CItemColumnSimple> lst = new List<CItemColumnSimple>();
            foreach (CColumnEQSimple col in m_tableManuelle.GetColonnesFinales())
                lst.Add(new CItemColumnSimple(col));
            m_wndListeColumns.Items = lst.ToArray();
        }

        
        //-----------------------------------
        private void CFormEditeProprietesTableManuelle_Load(object sender, EventArgs e)
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
            m_tableManuelle.NomFinal = m_txtNomTable.Text;
            m_wndListeColumns.ItemControl.MajChamps();
            List<CColumnEQSimple> lst = new List<CColumnEQSimple>();
            foreach ( CItemColumnSimple item in m_wndListeColumns.Items)
                lst.Add (item.ColumnSimple);
            DataTable table = m_tableManuelle.Table;
            table.AcceptChanges();
            HashSet<DataColumn> colsToDelete = new HashSet<DataColumn>();
            foreach (DataColumn col in table.Columns)
                colsToDelete.Add(col);

            try
            {
                foreach (CColumnEQSimple col in lst)
                {
                    //Cherche la colonne dans la datatable
                    string[] strIds = col.Id.Split('~');
                    DataColumn colTable = null;
                    if (strIds.Length == 2)
                    {
                        colTable = table.Columns[strIds[1]];
                    }
                    if (colTable == null)
                    {
                        colTable = new DataColumn(col.ColumnName, col.DataType);
                        table.Columns.Add(colTable);
                    }
                    colTable.ColumnName = col.ColumnName;
                    colTable.DataType = col.DataType;
                    colsToDelete.Remove(colTable);

                }
            }
            catch ( Exception ex )
            {
                CFormAlerte.Afficher(ex.ToString());
                return;
            }
            foreach (DataColumn col in colsToDelete)
                table.Columns.Remove(col);
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
        private void m_lnkAddColumn_LinkClicked(object sender, EventArgs e)
        {
            CColumnEQSimple col = new CColumnEQSimple();
            col.DataType = typeof(string);
            CItemColumnSimple item = new CItemColumnSimple(col);
            m_wndListeColumns.AddItem(item, true);
        }

        //-----------------------------------
        private void m_tabControl_SelectionChanged(object sender, EventArgs e)
        {

        }

        

        //------------------------------------------------------------------------
        private void m_lnkRemoveSort_LinkClicked(object sender, EventArgs e)
        {
            if (m_wndListeColumns.CurrentItemIndex != null)
            {
                if (CFormAlerte.Afficher(
                    I.T("Delete ?|20062"),
                    EFormAlerteType.Question) == DialogResult.Yes)
                {
                    int? nIndex = m_wndListeColumns.CurrentItemIndex;
                    m_wndListeColumns.RemoveItem(nIndex.Value, true);
                }
            }
        }
    }

    [AutoExec("Autoexec")]
    public class CEditeurProprietesTableManuelle : IEditeurProprietesObjetDeEasyQuery
    {

        public static void Autoexec()
        {
            CEditeurObjetsEasyQuery.RegisterEditeurProprietes(typeof(CODEQTableManuelle), typeof(CEditeurProprietesTableManuelle));
        }

        #region IEditeurProprietesObjetDeEasyQuery Membres

        public bool EditeProprietes(IObjetDeEasyQuery objet)
        {
            CODEQTableManuelle tableManuelle = objet as CODEQTableManuelle;
            if (tableManuelle == null)
                return false;
            CFormEditeProprietesTableManuelle form = new CFormEditeProprietesTableManuelle();
            form.Init(tableManuelle);
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }



        #endregion
    }

    
}
