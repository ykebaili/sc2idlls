using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
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
    public partial class CFormResultatSmartImport : Form
    {
        private CContexteDonnee m_contexteDonnee = null;
        private CContexteImportDonnee m_contexteImport = null;

        private HashSet<string> m_dicComparaisonsColsToHide = new HashSet<string>();

        //--------------------------------------------------------------------------------------------------------
        public CFormResultatSmartImport()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
            m_wndListeResumée.Columns[0].Width = m_wndListeResumée.ClientSize.Width;
            m_wndListeItems.Columns[0].Width = m_wndListeItems.ClientSize.Width;
        }

        //--------------------------------------------------------------------------------------------------------
        public static bool ShowImportResult ( 
            CContexteDonnee contexteDonnee, 
            CContexteImportDonnee contexteImport,
            CConfigMappagesSmartImport config,
            CValeursProprietes valeurs,
            DataTable tableSource)
        {
            using ( CFormResultatSmartImport form = new CFormResultatSmartImport())
            {
                form.m_contexteDonnee = contexteDonnee;
                form.m_contexteImport = contexteImport;
                form.FillListeRésumée();
                form.FillLog();
                form.FillComparaisons();
                form.m_ctrlSetup.Fill(valeurs, config, tableSource);
                if (form.ShowDialog() == DialogResult.OK)
                    return true;
                return false;
            }
        }

        //--------------------------------------------------------------------------------------------------------
        private void m_wndListeResumée_SizeChanged(object sender, EventArgs e)
        {
            m_wndListeResumée.Columns[0].Width = m_wndListeResumée.ClientSize.Width;
        }

        //--------------------------------------------------------------------------------------------------------
        private void m_wndListeItems_SizeChanged(object sender, EventArgs e)
        {
            m_wndListeItems.Columns[0].Width = m_wndListeItems.ClientSize.Width;
        }

        //--------------------------------------------------------------------------------------------------------
        private void FillLog()
        {
            m_wndListeLog.BeginUpdate();
            m_wndListeLog.Items.Clear();
            foreach ( CLigneLogImport ligne in m_contexteImport.Logs)
            {
                ListViewItem item = new ListViewItem();
                while ( item.SubItems.Count < m_wndListeLog.Columns.Count )
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
                item.SubItems[m_colIndex.Index].Text = (ligne.RowIndex + 1).ToString();
                m_wndListeLog.Items.Add(item);
            }
            m_wndListeLog.EndUpdate();
        }

        //--------------------------------------------------------------------------------------------------------
        private void FillListeRésumée()
        {
            //Note les rows comme modifiées si des champs custom ont été modifiés
            foreach (DataTable table in m_contexteDonnee.Tables)
            {
                if (table.Rows.Count > 0)
                {
                    Type tp = CContexteDonnee.GetTypeForTable(table.TableName);
                    if (typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tp))
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            CRelationElementAChamp_ChampCustom rel = m_contexteDonnee.GetNewObjetForRow(row) as CRelationElementAChamp_ChampCustom;
                            if (row.RowState != DataRowState.Unchanged)
                                if (rel.ElementAChamps != null && rel.ElementAChamps.Row.RowState == DataRowState.Unchanged)
                                {
                                    rel.ElementAChamps.Row.Row.SetModified();
                                }
                        }
                    }
                }
            }
            m_wndListeResumée.BeginUpdate();
            m_wndListeResumée.Items.Clear();
            List<DataTable> lst = new List<DataTable>();
            foreach ( DataTable table in m_contexteDonnee.Tables)
                lst.Add ( table );
            lst.Sort((x,y)=>DynamicClassAttribute.GetNomConvivial(CContexteDonnee.GetTypeForTable(x.TableName))
                .CompareTo(DynamicClassAttribute.GetNomConvivial(CContexteDonnee.GetTypeForTable(y.TableName))));
            foreach ( DataTable table in lst )
            {
                if ( table.Rows.Count > 0 )
                {
                    StringBuilder bl = new StringBuilder();
                    bl.Append ( DynamicClassAttribute.GetNomConvivial ( CContexteDonnee.GetTypeForTable(table.TableName)));
                    bl.Append(" (");
                    bl.Append(table.Rows.Count);
                    bl.Append(" / ");
                    int nNbModified = 0;
                    int nNbAdded = 0;
                    foreach ( DataRow row in table.Rows )
                    {
                        if ( row.RowState == DataRowState.Modified )
                            nNbModified++;
                        if ( row.RowState == DataRowState.Added )
                            nNbAdded++;
                    }
                    bl.Append ( nNbModified);
                    bl.Append(" / ");
                    bl.Append(nNbAdded);
                    bl.Append(")");
                    ListViewItem item = new ListViewItem(bl.ToString());
                    item.Tag = table;
                    m_wndListeResumée.Items.Add ( item );
                }
            }
            m_wndListeResumée.EndUpdate();
        }

        //-------------------------------------------------------------------------------
        private void m_wndListeResumée_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListeEntites();
        }

        //-------------------------------------------------------------------------------
        private void FillListeEntites()
        {
            ListViewItem item = m_wndListeResumée.SelectedItems.Count == 1 ?
            m_wndListeResumée.SelectedItems[0] : null;
            m_wndListeItems.BeginUpdate();
            m_wndListeItems.Items.Clear();
            if ( item != null && item.Tag is DataTable )
            {
                DataTable table = item.Tag as DataTable;
                m_lblTypeEntite.Text = item.Text;
                foreach ( DataRow row in table.Rows )
                {
                    CObjetDonnee objet = Activator.CreateInstance(CContexteDonnee.GetTypeForTable(table.TableName), new object[]{row}) as CObjetDonnee;
                    ListViewItem listItem = new ListViewItem(DescriptionFieldAttribute.GetDescription(objet));
                    listItem.Tag = objet;
                    listItem.ImageIndex =
                        row.RowState == DataRowState.Added ? 0 :
                        (row.RowState == DataRowState.Modified ? 1 : 2);
                    m_wndListeItems.Items.Add ( listItem );
                }
            }
            m_wndListeItems.EndUpdate();


        }

        //----------------------------------------------------------------------------
        private void m_wndListeItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( m_wndListeItems.SelectedItems.Count != 1)
            {
                m_wndViewItem.Visible = false;
                return;
            }
            ListViewItem item = m_wndListeItems.SelectedItems[0];
            CObjetDonnee objet = item != null?item.Tag as CObjetDonnee:null;
            if (objet != null)
            {
                m_wndViewItem.Visible = true;
                CValeursProprietes v = new CValeursProprietes(objet, true);
                m_wndViewItem.Fill(v);
            }
            else
                m_wndViewItem.Visible = false;

        }


        //----------------------------------------------------------------------------
        private void m_btnValider_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        //----------------------------------------------------------------------------
        private void FillComparaisons ( )
        {
            m_wndListeComparaison.BeginUpdate();
            m_wndListeComparaison.Items.Clear();
            if (m_contexteImport.TableSource == null)
                return;
            int nIndexLigne = 0;
            List<DataColumn> lstColonnes = new List<DataColumn>();
            foreach (DataColumn col in m_contexteImport.TableSource.Columns)
                if ( !m_dicComparaisonsColsToHide.Contains (col.ColumnName ))
                lstColonnes.Add(col);
            lstColonnes.Sort((x, y) => x.ColumnName.CompareTo(y.ColumnName));
            foreach ( DataRow rowSource in m_contexteImport.TableSource.Rows )
            {
                nIndexLigne++;
                DataRow rowDest = m_contexteImport.GetRowTemoin(rowSource);
                if (rowDest != null)
                {

                    foreach (DataColumn col in lstColonnes)
                    {
                        if (!rowDest[col].Equals(rowSource[col]) && (rowDest[col] != DBNull.Value || rowSource[col] != ""))
                        {
                            ListViewItem itemCompare = new ListViewItem();
                            while (itemCompare.SubItems.Count < m_wndListeComparaison.Columns.Count)
                                itemCompare.SubItems.Add("");
                            itemCompare.SubItems[m_colNumLigneCompare.Index].Text = nIndexLigne.ToString();
                            itemCompare.SubItems[m_colColumnCompare.Index].Text = col.ColumnName;
                            itemCompare.SubItems[m_colSourceValue.Index].Text = rowSource[col].ToString();
                            itemCompare.SubItems[m_colDestValue.Index].Text = rowDest[col].ToString();
                            m_wndListeComparaison.Items.Add(itemCompare);
                        }
                    }
                }
            }
            m_wndListeComparaison.EndUpdate();
        }

        //---------------------------------------------------------------------------------
        private void m_wndListeComparaison_MouseUp(object sender, MouseEventArgs e)
        {
            if ( e.Button == System.Windows.Forms.MouseButtons.Right )
            {
                Point pt = new Point(e.X, e.Y);
                ListViewHitTestInfo test = m_wndListeComparaison.HitTest(pt);
                if ( test.Item != null )
                {
                    string strCol = test.Item.SubItems[m_colColumnCompare.Index].Text;
                    ContextMenuStrip menu = new ContextMenuStrip();
                    ToolStripMenuItem itemHideCol = new ToolStripMenuItem(I.T("Hide difference on '@1'|20236", strCol));
                    itemHideCol.Tag = strCol;
                    menu.Items.Add(itemHideCol);
                    itemHideCol.Click += itemHideCol_Click;
                    menu.Show(m_wndListeComparaison, pt);
                }
            }
        }

        //---------------------------------------------------------------------------------
        void itemHideCol_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string strCol = item.Tag.ToString();
            m_dicComparaisonsColsToHide.Add(strCol);
            FillComparaisons();
        }
    }
}
