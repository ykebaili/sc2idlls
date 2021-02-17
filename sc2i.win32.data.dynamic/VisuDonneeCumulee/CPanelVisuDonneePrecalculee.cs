using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.formulaire;
using sc2i.win32.common;
using sc2i.formulaire.win32;
using System.Collections;
using sc2i.formulaire.win32.editor;

namespace sc2i.win32.data.dynamic
{
    public partial class CPanelVisuDonneePrecalculee : UserControl
    {
        private CParametreVisuDonneePrecalculee m_parametre = null;
        private CContexteDonnee m_contexteDonnee = null;
        private Dictionary<string, CParametreVisuChampTableauCroise> m_dicColToParametreCol = new Dictionary<string, CParametreVisuChampTableauCroise>();
        private DataTable m_table = null;
        private List<CFiltreDonneePrecalculee> m_listeFiltresApplicables = new List<CFiltreDonneePrecalculee>();

        private HashSet<CFiltreDonneePrecalculee> m_dicFiltresAppliques = new HashSet<CFiltreDonneePrecalculee>();

        public CPanelVisuDonneePrecalculee()
        {
            InitializeComponent();
        }

        private class CMenuItemAvecFiltre : ToolStripMenuItem
        {
            public CFiltreDonneePrecalculee Filtre { get; set; }

            public CMenuItemAvecFiltre(CFiltreDonneePrecalculee filtre)
                : base(filtre.Libelle)
            {
                Filtre = filtre;
            }
        }


        public void Init(CParametreVisuDonneePrecalculee parametre,
            CContexteDonnee contexte)
        {
            m_parametre = parametre;
            m_contexteDonnee = contexte;
            
            m_listeFiltresApplicables.Clear();
            m_menuFiltres.Items.Clear();
            foreach (CFiltreDonneePrecalculee filtre in parametre.FiltresUtilisateur)
            {
               C2iWnd wnd = filtre.Filtre.FormulaireEdition;
                if  (wnd.Childs.Count() > 0 && filtre.Filtre.ListeVariables.Count() > 0 )
                {
                    CMenuItemAvecFiltre menuItem = new CMenuItemAvecFiltre(filtre);
                    menuItem.Click += new EventHandler(menuItem_Click);
                    m_menuFiltres.Items.Add(menuItem);
                    filtre.Filtre.ContexteDonnee = contexte;
                }
            }
            m_imageHasFiltreRef.Visible = m_menuFiltres.Items.Count > 0;

            m_lnkExport.Visible = m_parametre.ShowExportButton;

            FillGrid(false);

        }



        private void  menuItem_Click(object sender, EventArgs e)
        {
            CMenuItemAvecFiltre item = sender as CMenuItemAvecFiltre;
            if (item != null)
            {
                CFiltreDonneePrecalculee filtre = item.Filtre;
                if ( CFormFormulairePopup.EditeElement ( filtre.Filtre.FormulaireEdition, 
                    filtre.Filtre, filtre.Libelle) )
                //if (CFormFiltreDynamic.SetValeursFiltre(filtre.Filtre))
                    FillGrid(true);
            }

        } 

        private struct CSortColumn
        {
            public readonly DataColumn Column;
            public readonly int SortOrder;
            public readonly bool Decroissant;

            public CSortColumn ( DataColumn column, int nSortOrder, bool bDecroissant )
            {
                Column = column;
                SortOrder = nSortOrder;
                Decroissant = bDecroissant;
            }
        }

        private string GetDisplayName(string strName)
        {
            return strName + "_DISP";
        }

        private string GetDataName(string strName)
        {
            if (strName.EndsWith("_DISP"))
                return strName.Substring(0, strName.Length - "_DISP".Length);
            return strName;
        }
        
        
        void FillGrid(bool bAvecInterface)
        {
            CResultAErreur result = m_parametre.GetDataTable(m_contexteDonnee);
            if (!result)
            {
                if ( bAvecInterface )
                    CFormAlerte.Afficher(result.Erreur);
                return;
            }
            try
            {
                DataTable table = result.Data as DataTable;
                m_grid.EnableHeadersVisualStyles = false;
                m_grid.ColumnHeadersVisible = m_parametre.ShowHeader;
                m_table = table;
                DataView view = m_table.DefaultView;
                m_grid.AutoGenerateColumns = false;
                m_grid.DataSource = view;

                m_grid.Columns.Clear();

                result = m_parametre.PrepareAffichageDonnees(table, m_contexteDonnee);
                m_grid.BackgroundColor = BackColor;

                m_grid.GridColor = ForeColor;

                DataGridViewCellStyle style = m_grid.ColumnHeadersDefaultCellStyle;
                CompleteFormatCellule(m_parametre.FormatHeader, style);
                style = m_grid.DefaultCellStyle;
                CompleteFormatCellule(m_parametre.FormatRows, style);
                /*style.SelectionBackColor = Color.FromArgb(255 - style.BackColor.R, 255 - style.BackColor.G, 255 - style.BackColor.B);
                style.SelectionForeColor = Color.FromArgb(255 - style.ForeColor.R, 255 - style.ForeColor.G, 255 - style.ForeColor.B);
                */
                if (m_parametre.FormatRows.SelectionBackcolor.A == 0)
                {
                    style.SelectionForeColor = style.BackColor;
                    style.SelectionBackColor = style.ForeColor;
                }
                else
                {
                    style.SelectionForeColor = m_parametre.FormatRows.ForeColor;
                    style.SelectionBackColor = m_parametre.FormatRows.SelectionBackcolor;
                }


                List<CSortColumn> lstSort = new List<CSortColumn>();

                Dictionary<string, CParametreVisuChampTableauCroise> dicChampToParam = new Dictionary<string, CParametreVisuChampTableauCroise>();
                foreach (CParametreVisuChampTableauCroise pChamp in m_parametre.ParametresChamps)
                {
                    dicChampToParam[pChamp.ChampFinal.NomChamp] = pChamp;
                }

                string strColToMap = "";
                foreach (DataColumn col in new ArrayList(table.Columns))
                {
                    strColToMap = col.ColumnName;
                    CParametreVisuChampTableauCroise paramChamp = null;
                    CChampFinalDeTableauCroise champFinal = col.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] as CChampFinalDeTableauCroise;
                    DataGridViewColumn viewCol = null;
                    if (champFinal != null && dicChampToParam.TryGetValue(champFinal.NomChamp, out paramChamp))
                    {
                        if (paramChamp.ActionSurClick != null)
                        {
                            DataGridViewLinkColumn linkCol = new DataGridViewLinkColumn();
                            viewCol = linkCol;
                        }
                        DataColumn displayCol = col;
                        if (paramChamp.ChampFinal is CChampFinalDeTableauCroiseCle &&
                            paramChamp.FormuleData != null)
                        {
                            if (table.Columns.Contains(GetDisplayName(col.ColumnName)))
                                displayCol = table.Columns[GetDisplayName(col.ColumnName)];
                            else
                            {
                                //Crée une colonne _DISPLAY
                                displayCol = new DataColumn(GetDisplayName(col.ColumnName), typeof(string));
                                table.Columns.Add(displayCol);
                            }
                            strColToMap = displayCol.ColumnName;
                        }

                        if (paramChamp.SortOrder != null)
                        {
                            lstSort.Add(new CSortColumn(displayCol, paramChamp.SortOrder.Value, paramChamp.TriDecroissant));
                        }
                    }



                    if (viewCol == null)
                    {
                        viewCol = new DataGridViewTextBoxColumn();
                    }
                    m_grid.Columns.Add(viewCol);
                    viewCol.SortMode = DataGridViewColumnSortMode.Programmatic;
                    viewCol.DataPropertyName = strColToMap;
                    viewCol.HeaderText = col.ColumnName;

                }


                //m_grid.RowCount = Math.Max ( table.Rows.Count, 1 );
                CParametreDonneeCumulee parametreDonnee = null;
                CTypeDonneeCumulee typeDonnee = m_parametre.GetTypeDonneeCumulee(m_contexteDonnee);
                if (typeDonnee != null)
                    parametreDonnee = typeDonnee.Parametre;
                foreach (CParametreVisuChampTableauCroise paramChamp in m_parametre.ParametresChamps)
                {
                    foreach (DataGridViewColumn colView in m_grid.Columns)
                    {
                        DataColumn col = table.Columns[GetDataName(colView.DataPropertyName)];
                        if (col != null)
                        {
                            CChampFinalDeTableauCroise champFinal = col.ExtendedProperties[CTableauCroise.c_ExtendedPropertyToColumnKey] as CChampFinalDeTableauCroise;
                            if (champFinal != null && champFinal.NomChamp == paramChamp.ChampFinal.NomChamp)
                            {
                                m_dicColToParametreCol[colView.DataPropertyName] = paramChamp;
                                CompleteFormatCellule(paramChamp.FormatHeader, colView.HeaderCell.Style);
                                CompleteFormatCellule(paramChamp.FormatParDefaut, colView.DefaultCellStyle);

                                colView.HeaderCell.Value = paramChamp.GetValeurHeader(col, m_parametre);
                                if (paramChamp.FormatHeader.Width != null)
                                    colView.Width = paramChamp.FormatHeader.Width.Value;
                                if (paramChamp.FormuleData != null && champFinal is CChampFinalDeTableauCroiseCle)
                                {
                                    foreach (DataRow row in m_table.Rows)
                                    {
                                        row[GetDisplayName(col.ColumnName)] = paramChamp.GetValeurData(row[col], m_parametre);
                                    }
                                    colView.DataPropertyName = GetDisplayName(col.ColumnName);
                                }

                            }
                        }
                    }
                }

                lstSort.Sort(delegate(CSortColumn a, CSortColumn b)
                {
                    if (a.SortOrder == b.SortOrder)
                        return a.Column.ColumnName.CompareTo(b.Column.ColumnName);
                    return a.SortOrder.CompareTo(b.SortOrder);
                }
                );
                string strSort = "";
                foreach (CSortColumn sortCol in lstSort)
                {
                    strSort += sortCol.Column.ColumnName;
                    if (sortCol.Decroissant)
                        strSort += " desc";
                    strSort += ",";
                }
                if (strSort.Length > 0)
                {
                    strSort = strSort.Substring(0, strSort.Length - 1);
                    view.Sort = strSort;
                }

                AnalyseFiltres();
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                if (bAvecInterface)
                    CFormAlerte.Afficher(result.Erreur);
            }
        }

        //------------------------------------------------------------------------------------
        private void AnalyseFiltres()
        {
            List<CFiltreDonneePrecalculee> lstAppliqués = new List<CFiltreDonneePrecalculee>();
            m_dicFiltresAppliques.Clear();
            foreach (CFiltreDonneePrecalculee filtre in m_parametre.FiltresUtilisateur)
            {
                CResultAErreur result = filtre.Filtre.GetFiltreData();
                if (result)
                {
                    CFiltreData filtreData = result.Data as CFiltreData;
                    if (filtreData != null && filtreData.HasFiltre && filtreData.Get2iString() != "1=1")
                    {
                        lstAppliqués.Add(filtre);
                        m_dicFiltresAppliques.Add(filtre);
                    }
                }
            }
            string strLib = "";
            foreach (CFiltreDonneePrecalculee filtre in lstAppliqués)
            {
                strLib += filtre.Libelle;
                if (filtre.FormuleDescription != null)
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(filtre.Filtre);
                    CResultAErreur result = filtre.FormuleDescription.Eval(ctx);
                    if (result && result.Data != null)
                        strLib += " : " + result.Data.ToString();
                }
                strLib += ", ";
            }
            if (strLib.Length > 0)
                strLib = strLib.Substring(0, strLib.Length - 2);
            m_lblDetailFiltre.Text = strLib;
        }


        private void m_imageHasFiltreRef_Click(object sender, EventArgs e)
        {
            if (m_menuFiltres.Items.Count > 0)
            {
                m_menuFiltres.Show(m_imageHasFiltreRef, new Point(0, m_imageHasFiltreRef.Height));
            }
        }

        private void m_menuFiltres_Opening(object sender, CancelEventArgs e)
        {
            foreach (object item in m_menuFiltres.Items)
            {
                CMenuItemAvecFiltre menu = item as CMenuItemAvecFiltre;
                if (menu != null)
                {
                    if (m_dicFiltresAppliques.Contains(menu.Filtre))
                        menu.Image = m_imageHasFiltreRef.Image;
                    else
                        menu.Image = null;
                }
            }
        }

        private void CompleteFormatCellule(CFormatChampTableauCroise format,
           DataGridViewCellStyle style)
        {
            bool bChangeFont = format.FontName != "" || format.FontSize != 0 || format.Bold != null;
            if (bChangeFont)
            {
                string strFontName = "Arial";
                float fSize = 8;
                bool bBold = false;
                if (style.Font != null)
                {
                    strFontName = style.Font.FontFamily.Name;
                    fSize = style.Font.Size;
                    bBold = style.Font.Bold;
                }
                if (format.FontName != "")
                    strFontName = format.FontName;
                if (format.FontSize != 0)
                    fSize = format.FontSize;
                if (format.Bold != null)
                    bBold = format.Bold.Value;
                style.Font = new Font(strFontName, fSize, bBold?FontStyle.Bold:FontStyle.Regular);
            }
            if (format.ForeColor.A != 0)
                style.ForeColor = format.ForeColor;
            if (format.BackColor.A != 0)
                style.BackColor = format.BackColor;
            if (format.SelectionBackcolor.A == 0 )
            {
                if (m_parametre.FormatRows.SelectionBackcolor.A == 0)
                {
                    style.SelectionBackColor = style.ForeColor;
                    style.SelectionForeColor = style.BackColor;
                }
            }
            else
            {
                style.SelectionBackColor = format.SelectionBackcolor;
                style.SelectionForeColor = style.ForeColor;
            }
            if (format.Alignement != null)
            {
                switch (format.Alignement.Value)
                {
                    case C2iWndTextBox.TypeAlignement.Centre:
                        style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case C2iWndTextBox.TypeAlignement.Droite:
                        style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case C2iWndTextBox.TypeAlignement.Gauche:
                        style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    default:
                        break;
                }
            }
        }

        //------------------------------------------------------------------------------
        private void m_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn col = m_grid.Columns[e.ColumnIndex];
            CParametreVisuChampTableauCroise parametre = null;
            if (m_dicColToParametreCol.TryGetValue(col.DataPropertyName, out parametre) )
            {
                if (parametre.ActionSurClick != null)
                {
                    try
                    {
                        CExecuteurActionSur2iLink.ExecuteAction(this, parametre.ActionSurClick, parametre.GetObjetPourFormuleCellule(m_parametre,
                            ((DataView)m_grid.DataSource)[e.RowIndex].Row,
                            e.ColumnIndex));
                    }
                    catch { }
                }
            }
        }

        private void m_grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn col = m_grid.Columns[e.ColumnIndex];
            e.CellStyle = m_grid.DefaultCellStyle.Clone() as DataGridViewCellStyle;
            CParametreVisuChampTableauCroise parametre = null;
            if (e.RowIndex < m_table.Rows.Count)
            {
                DataRow row = (m_grid.DataSource as DataView)[e.RowIndex].Row;
                if (m_dicColToParametreCol.TryGetValue(col.DataPropertyName, out parametre))
                {
                    if ( parametre.ChampFinal is CChampFinalDeTableauCroiseDonnee )
                        e.Value = parametre.GetValeurData(e.Value, m_parametre);

                    CFormatChampTableauCroise format = parametre.FormatParDefaut;
                    if (parametre.FormatParDefaut.IsDynamic)
                    {
                        CFormatChampTableauCroise formatDyn = parametre.GetFormatData(row[e.ColumnIndex], m_parametre);
                        if (formatDyn != null)
                            format = formatDyn;
                    }
                    CompleteFormatCellule(format, e.CellStyle);
                }
            }
        }

        private void CPanelVisuDonneePrecalculee_Load(object sender, EventArgs e)
        {
            CWin32Traducteur.Translate(this);
        }

        private void m_lnkExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IExporteurDataset exporteur = null;
            IDestinationExport destination = null;
            if (CFormOptionsExport.EditeOptions(ref destination, ref exporteur))
            {
                if (exporteur != null)
                {
                    DataSet dsExport = new DataSet();
                    DataTable tableExport = new DataTable("EXPORT");
                    foreach (DataGridViewColumn colView in m_grid.Columns)
                    {
                        if (!tableExport.Columns.Contains(colView.HeaderText))
                            tableExport.Columns.Add(colView.HeaderText);
                    }
                    int rowIndex = 0;
                    foreach (DataGridViewRow rowView in m_grid.Rows)
                    {
                        tableExport.Rows.Add(tableExport.NewRow());
                        foreach (DataGridViewColumn colView in m_grid.Columns)
                        {
                            int colIndex = colView.Index;
                            tableExport.Rows[rowIndex][colIndex] = rowView.Cells[colIndex].Value;
                        }
                        rowIndex++;
                    }

                    dsExport.Tables.Add(tableExport);
                    CResultAErreur result = exporteur.Export(dsExport, destination);
                    if (!result)
                    {
                        CFormAlerte.Afficher(result);
                        return;
                    }
                    else
                    {
                        CFormAlerte.Afficher(I.T("Export completed|30037"));
                    }
                }
            }

        }
    }
}
