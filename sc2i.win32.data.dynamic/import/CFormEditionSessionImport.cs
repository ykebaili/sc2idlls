using sc2i.common;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.win32.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sc2i.win32.data.dynamic.import
{
    public partial class CFormEditionSessionImport : Form
    {
        private CSessionImport m_session = null;
        public CFormEditionSessionImport()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //-----------------------------------------------------------------
        public static void EditeSession ( CSessionImport session )
        {
            using ( CFormEditionSessionImport frm = new CFormEditionSessionImport())
            {
                frm.m_session = session;
                frm.ShowDialog();
            }
        }

        //-----------------------------------------------------------------
        private void CFormEditionSessionImport_Load(object sender, EventArgs e)
        {
            m_wndListeLog.Items.Clear();
            FillLog(m_wndListeLog, m_session.Logs);
            m_gridTableSource.DataSource = m_session.TableSource;
            m_chkAfficheNonImportees.Checked = true;
            m_gridMarkedRecords.DataSource = m_session.TableLignesMarquées;
        }

        //--------------------------------------------------------------------------------------------------------
        private void FillLog(
            ListView wndListe, 
            IEnumerable<CLigneLogImport> lignes)
        {
            wndListe.BeginUpdate();
            foreach (CLigneLogImport ligne in lignes)
            {
                ListViewItem item = new ListViewItem();
                while (item.SubItems.Count < wndListe.Columns.Count)
                    item.SubItems.Add("");
                switch (ligne.TypeLigne)
                {
                    case ETypeLigneLogImport.Info:
                        item.ImageIndex = 0;
                        break;
                    case ETypeLigneLogImport.Alert:
                        item.ImageIndex = 1;
                        break;
                    case ETypeLigneLogImport.Error:
                        item.ImageIndex = 2;
                        break;
                    default:
                        break;
                }
                item.StateImageIndex = item.ImageIndex;
                item.Tag = ligne.SourceRow;
                if (ligne.ProprieteDest != null)
                    item.SubItems[m_colField.Index].Text = ligne.ProprieteDest;
                if (ligne.SourceColumn != null)
                    item.SubItems[m_colCol.Index].Text = ligne.SourceColumn;
                if (ligne.Message != null)
                    item.SubItems[m_colMessage.Index].Text = ligne.Message;
                if (ligne.RowIndex != null)
                {
                    if (ligne.EndRowIndex == null)
                        item.SubItems[m_colIndex.Index].Text = (ligne.RowIndex.Value).ToString();
                    else
                        item.SubItems[m_colIndex.Index].Text = (ligne.RowIndex.Value).ToString() + "->" +
                            ligne.EndRowIndex.Value;
                }
                else
                    item.SubItems[m_colIndex.Index].Text = "";
                wndListe.Items.Add(item);
            }
            wndListe.EndUpdate();
        }

        private void m_gridTableNonImporte_SelectionChanged(object sender, EventArgs e)
        {
            ShowLogs(m_gridTableLignesTraitees, m_wndListeLogForLigne, m_lblLigneSel);
        }

        
        private List<int> GetSelectedLines(DataGridView grid)
        {
            List<int> lstNumRows = new List<int>();
            if (grid.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow gridRow in grid.SelectedRows)
                {
                    DataRowView row = gridRow.DataBoundItem as DataRowView;
                    if (row != null)
                    {
                        if (row[CSessionImport.c_colNumLigneOriginal] is int)
                            lstNumRows.Add((int)row[CSessionImport.c_colNumLigneOriginal]);

                    }
                }
            }
           /* else if (grid.SelectedCells.Count > 0)
            {
                HashSet<int> setLignes = new HashSet<int>();
                foreach (DataGridViewCell cell in grid.SelectedCells)
                {
                    DataRowView row = cell.OwningRow.DataBoundItem as DataRowView;
                    if (row != null)
                        if (row[CSessionImport.c_colNumLigneOriginal] is int)
                            setLignes.Add((int)row[CSessionImport.c_colNumLigneOriginal]);
                }
                lstNumRows.AddRange(setLignes.ToArray());
            }*/

            lstNumRows.Sort((x, y) => x.CompareTo(y));
            return lstNumRows;
        }

        private void ShowLogs ( DataGridView grid,
            ListView lstLogs,
            Label lblInfo )
        {
            lstLogs.Items.Clear();
            List<int> lstNumRows = GetSelectedLines(grid);

            foreach ( int nLigne in lstNumRows )
            {
                IEnumerable<CLigneLogImport> lstLignes = m_session.GetLogsForLine(nLigne);
                FillLog(lstLogs, lstLignes);
            }
            if (lstNumRows.Count == 1)
            {
                lblInfo.Text = I.T("Logs of line n°@1|20153", lstNumRows[0].ToString());
            }
            else if (lstNumRows.Count > 0)
                lblInfo.Text = I.T("Logs for @1 lines|20154", lstNumRows.Count.ToString());
            else
                lblInfo.Text = "";
        }

        private void m_chkAfficherImportées_CheckedChanged(object sender, EventArgs e)
        {
            m_gridTableLignesTraitees.DataSource = m_session.TableLignesImportees;
        }

        private void m_chkAfficheNonImportees_CheckedChanged(object sender, EventArgs e)
        {
            m_gridTableLignesTraitees.DataSource = m_session.TableLignesNonImportees;
        }

        private void m_btnMarkedRecordsToSourceTable_Click(object sender, EventArgs e)
        {
            m_session.ReplaceSourceParMarkedRecords();
            m_gridTableSource.DataSource = m_session.TableSource;
        }

        private void m_btnMarkLines_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in m_gridTableLignesTraitees.SelectedRows)
            {
                DataRowView row = gridRow.DataBoundItem as DataRowView;
                if (row != null && row.Row != null)
                    m_session.TableLignesMarquées.ImportRow(row.Row);
                m_gridMarkedRecords.Refresh();
            }
        }

        private void m_gridTableLignesTraitees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //ShowLogs(m_gridTableLignesTraitees, m_wndListeLogForLigne, m_lblLigneSel);
        }

        private void m_gridTableLignesTraitees_DataSourceChanged(object sender, EventArgs e)
        {
            if (m_gridTableLignesTraitees.DataSource is DataTable)
                m_lblNbLignesTableImport.Text = I.T("@1 lines|20163",
                    ((DataTable)m_gridTableLignesTraitees.DataSource).Rows.Count.ToString());
        }

        private void m_gridTableSource_DataSourceChanged(object sender, EventArgs e)
        {
            if (m_gridTableSource.DataSource is DataTable)
                m_lnkNbLignesSource.Text = I.T("@1 lines|20163",
                    ((DataTable)m_gridTableSource.DataSource).Rows.Count.ToString());
        
        }
    }
}
