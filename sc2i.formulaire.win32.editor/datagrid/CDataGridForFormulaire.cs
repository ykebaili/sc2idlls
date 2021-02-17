using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.formulaire.datagrid;
using sc2i.drawing;
using sc2i.formulaire.win32.datagrid;
using sc2i.expression;
using sc2i.common;
using System.Collections;
using sc2i.formulaire.win32.datagrid.Filtre;
using sc2i.formulaire.datagrid.Filters;

namespace sc2i.formulaire.win32.controles2iWnd.datagrid
{
    public partial class CDataGridForFormulaire : UserControl, IControlALockEdition
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        private CToolStripSelectValues m_valueSelector = new CToolStripSelectValues();

        private Dictionary<int, CGridFilterForWndDataGrid> m_dicFiltre = new Dictionary<int, CGridFilterForWndDataGrid>();
        private bool m_bLockEdition = false;

        private List<object> m_listeObjetsOriginale = new List<object>();
        private C2iWndDataGrid m_wndGrid = null;
        private IFournisseurProprietesDynamiques m_fournisseur = null;
        private object m_elementEdite = null;
        private CWndFor2iDataGrid m_wndFor2iDataGrid = null;

        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;

        private static int c_nWidthImageHeader = 16;

        //Stocke la liste des éléments que le contrôle a ajouté
        private ArrayList m_listeElementsAdd = new ArrayList();

        private CGridDataCache m_cache = null;

        private int? m_nColumnSort = null;
        private bool m_bSortAsc = true;

        public CDataGridForFormulaire()
        {
            InitializeComponent();
            m_cache = new CGridDataCache(this);
            CWin32Traducteur.Translate(m_menuFiltre);

            m_valueSelector.OnOkClicked += new EventHandler(m_valueSelector_OnOkClicked);
        }

        public C2iWndDataGrid WndGrid
        {
            get
            {
                return m_wndGrid;
            }
        }

        public void Init(
            CWndFor2iDataGrid wndFor2iDataGrid,
            C2iWndDataGrid wndGrid,
            object elementEdite,
            List<object> lstObjets,
            IFournisseurProprietesDynamiques fournisseur)
        {
            m_wndFor2iDataGrid = wndFor2iDataGrid;
            m_listeObjetsOriginale = lstObjets;
            m_fournisseur = fournisseur;
            m_wndGrid = wndGrid;
            m_elementEdite = elementEdite;
            if (m_cache != null)
                m_cache.Dispose();
            m_cache = new CGridDataCache(this);


            List<object> listeObjets = new List<object>(lstObjets);

            foreach (object newElt in m_listeElementsAdd)
                if (!lstObjets.Contains(newElt))
                    lstObjets.Add(newElt);

            m_panelTop.Visible = m_wndGrid.HasAddButton || m_wndGrid.HasDeleteButton;
            m_lnkAdd.Visible = m_wndGrid.HasAddButton;
            m_lnkDelete.Visible = m_wndGrid.HasDeleteButton;

            m_grid.AutoGenerateColumns = false;
            Filtrer();
            m_grid.SuspendDrawing();
            //m_grid.DataSource = lstObjets;
            m_grid.Columns.Clear();
            int nMaxHeight = m_grid.ColumnHeadersHeight;
            m_grid.EnableHeadersVisualStyles = false;
            m_grid.RowHeadersVisible = m_wndGrid.RowHeaderWidth != 0;
            m_grid.RowHeadersWidth = Math.Max(m_wndGrid.RowHeaderWidth, 10);
            m_grid.RowHeadersDefaultCellStyle.BackColor = m_wndGrid.RowHeaderColor;
            m_grid.BackgroundColor = m_wndGrid.BackColor;
            m_grid.DefaultCellStyle.SelectionBackColor = m_wndGrid.SelectedCellBackColor;
            m_grid.DefaultCellStyle.SelectionForeColor = m_wndGrid.SelectedCellForeColor;
            m_grid.EditMode = DataGridViewEditMode.EditOnEnter;
            if (m_wndGrid.DefaultRowHeight > 0)
                m_grid.RowTemplate.Height = m_wndGrid.DefaultRowHeight;
            int nCol = 0;
            foreach (I2iObjetGraphique obj in m_wndGrid.Childs)
            {
                C2iWndDataGridColumn col = obj as C2iWndDataGridColumn;
                CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(m_elementEdite);
                //Evalue la formule de visibilité
                if (col.Visiblity != null)
                {
                    
                    CResultAErreur res = col.Visiblity.Eval(ctxEval);
                    if (res && !CUtilBool.BoolFromObject ( res.Data ))
                        col = null;
                }
                if (col != null)
                {
                    IWndIncluableDansDataGrid wndForGrid = col.Control as IWndIncluableDansDataGrid;
                    m_cache.RegisterControle(nCol, wndForGrid);
                    nCol++;
                    if (wndForGrid != null)
                    {
                        DataGridViewColumn gridCol = new DataGridViewColumn(
                            new CDataGridViewCustomCellFor2iWnd(m_cache,col, nCol-1, wndForGrid, m_fournisseur)
                            );
                        if (col.Enabled != null)
                        {
                            CResultAErreur result = col.Enabled.Eval(ctxEval);
                            if (result && !CUtilBool.BoolFromObject(result.Data))
                                gridCol.ReadOnly = true;
                        }
                        gridCol.HeaderText = col.Text;
                        gridCol.Width = col.ColumnWidth;
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = col.BackColor;
                        style.ForeColor = col.ForeColor;
                        style.Font = col.Font;
                        gridCol.HeaderCell.Style = style;

                        if (col.Control != null)
                        {
                            DataGridViewCellStyle styleCell = new DataGridViewCellStyle();
                            styleCell.Font = col.Control.Font;
                            styleCell.BackColor = col.Control.BackColor;
                            styleCell.ForeColor = col.Control.ForeColor;
                            styleCell.WrapMode = DataGridViewTriState.True;
                            gridCol.CellTemplate.Style = styleCell;
                        }
                        m_grid.Columns.Add(gridCol);
                        nMaxHeight = Math.Max(col.Size.Height, nMaxHeight);
                    }
                }
            }
            m_grid.ResumeDrawing();
            m_grid.ColumnHeadersHeight = nMaxHeight;
            InitRestrictions(m_listeRestrictions);

        }

        public CListeRestrictionsUtilisateurSurType ListeRestrictions
        {
            get
            {
                return m_listeRestrictions;
            }
        }

        public void InitRestrictions(CListeRestrictionsUtilisateurSurType listeRestrictions)
        {
            if (listeRestrictions != null)
                m_listeRestrictions = listeRestrictions.Clone() as CListeRestrictionsUtilisateurSurType;
            else
                m_listeRestrictions = null;
            if (m_grid != null)
            {
                foreach (DataGridViewColumn col in m_grid.Columns)
                {
                    CDataGridViewCustomCellFor2iWnd tpl = col.CellTemplate as CDataGridViewCustomCellFor2iWnd;
                    if (tpl != null)
                        tpl.InitRestrictions(listeRestrictions);
                }
            }
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return m_bLockEdition;
            }
            set
            {
                m_bLockEdition = value;
                m_lnkAdd.Enabled = !m_bLockEdition;
                m_lnkDelete.Enabled = !m_bLockEdition;
                m_grid.ReadOnly = m_bLockEdition;
                if (value)
                    m_listeElementsAdd.Clear();
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs()); ;
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        //---------------------------------------------------------------------
        private object m_lastAddedelement = null;
        public object LastAddedElement
        {
            get
            {
                return m_lastAddedelement;
            }
        }

        //---------------------------------------------------------------------
        private void m_lnkAdd_LinkClicked(object sender, EventArgs e)
        {
            CResultAErreur result = CResultAErreur.True;
            IAllocateurSupprimeurElements allocateur = m_elementEdite as IAllocateurSupprimeurElements;
            if (m_elementEdite != null)
            {
                if (m_wndGrid.SourceFormula != null)
                {
                    Type tp = m_wndGrid.SourceFormula.TypeDonnee.TypeDotNetNatif;
                    CAffectationsProprietes affectations = SelectAffectation();
                    if (tp != null && affectations != null)
                    {
                        object obj = null;
                        try
                        {
                            if (allocateur != null)
                            {
                                result = allocateur.AlloueElement(tp);
                                if (result)
                                    obj = result.Data;
                            }
                            else
                                obj = Activator.CreateInstance(tp);
                        }
                        catch (Exception ex)
                        {
                            result.EmpileErreur(new CErreurException(ex));
                        }
                        if (obj == null | !result)
                        {
                            result.EmpileErreur(I.T("Error while allocating element|20003"));
                            CFormAfficheErreur.Show(result.Erreur);
                            return;
                        }
                        result = affectations.AffecteProprietes(obj, m_elementEdite, m_fournisseur);
                        if (!result)
                        {
                            result.EmpileErreur(I.T("Some values cannot be assigned|20004"));
                            CFormAfficheErreur.Show(result.Erreur);
                        }
                        if (!m_listeElementsAdd.Contains(obj))
                        {
                            m_listeElementsAdd.Add(obj);
                            m_lastAddedelement = obj;
                            CUtilControlesWnd.DeclencheEvenement(C2iWndDataGrid.c_strIdEvenementAddElement, m_wndFor2iDataGrid);

                        }
                        m_wndFor2iDataGrid.SetElementEdite(m_elementEdite);
                    }
                }
            }
        }

        //---------------------------------------------------------------------
        private CAffectationsProprietes SelectAffectation()
        {
            if (m_elementEdite == null)
                return null;
            List<CAffectationsProprietes> lst = new List<CAffectationsProprietes>();
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(m_elementEdite);
            foreach (CAffectationsProprietes aff in m_wndGrid.Affectations)
            {
                CResultAErreur result = CResultAErreur.True;
                result.Data = true;
                if (aff.FormuleCondition != null)
                {
                    result = aff.FormuleCondition.Eval(ctx);
                }
                if (result && result.Data is bool && (bool)result.Data)
                    lst.Add(aff);
            }
            if (lst.Count == 1)
                return lst[0];
            if (lst.Count == 0)
                return null;


            CMenuModal menu = new CMenuModal();
            foreach (CAffectationsProprietes affectation in lst)
            {
                CMenuItemAvecAffectation item = new CMenuItemAvecAffectation(affectation);
                menu.Items.Add(item);
            }
            CMenuItemAvecAffectation menuAff = menu.ShowMenu(PointToScreen(new Point(m_lnkAdd.Left, m_lnkAdd.Bottom))) as CMenuItemAvecAffectation;
            if (menuAff != null)
                return menuAff.Affectation;
            return null;
        }

        private class CMenuItemAvecAffectation : ToolStripMenuItem
        {
            private CAffectationsProprietes m_affectation = null;

            public CMenuItemAvecAffectation(CAffectationsProprietes affectation)
                : base(affectation.Libelle)
            {
                m_affectation = affectation;
            }

            public CAffectationsProprietes Affectation
            {
                get
                {
                    return m_affectation;
                }
            }
        }

        private void m_lnkDelete_LinkClicked(object sender, EventArgs e)
        {
            object element = null;
            try
            {
                element = (m_grid.DataSource as IList)[m_grid.CurrentCell.RowIndex];
            }
            catch
            {
            }
            if (element == null)
                return;
            CResultAErreur result = CResultAErreur.True;
            if (m_wndGrid.DeleteConfirmMessage.Trim() != "")
            {
                if (CFormAlerte.Afficher(m_wndGrid.DeleteConfirmMessage, EFormAlerteType.Question) == DialogResult.No)
                    return;
            }
            IAllocateurSupprimeurElements supprimeur = m_elementEdite as IAllocateurSupprimeurElements;
            if (supprimeur != null)
            {
                m_listeElementsAdd.Remove(element);
                result = supprimeur.SupprimeElement(element);
                if (!result)
                {
                    m_listeElementsAdd.Add(element);
                }
                else
                    m_wndFor2iDataGrid.SetElementEdite(m_elementEdite);
            }
            CUtilControlesWnd.DeclencheEvenement(C2iWndDataGrid.c_strIdEvenementDeleteElement, m_wndFor2iDataGrid);

        }

        private int m_nColIndexForFiltre = -1;
        private void FillMenuFiltre(int nColIndex, C2iWndDataGridColumn colonne)
        {
            m_nColIndexForFiltre = nColIndex;
            foreach (ToolStripItem item in new ArrayList(m_menuFiltreSpecial.DropDownItems))
            {
                m_menuFiltreSpecial.DropDownItems.Remove(item);
                if ( item != m_valueSelector )
                    item.Dispose();
            }
            m_menuFiltreSpecial.DropDownItems.Clear();
            CGridFilterForWndDataGrid filtreActif = null;
            m_dicFiltre.TryGetValue(nColIndex, out filtreActif);
            IWndIncluableDansDataGrid wnd = colonne.Control as IWndIncluableDansDataGrid;
            if (wnd != null)
            {
                foreach (CGridFilterForWndDataGrid filtre in wnd.GetPossibleFilters())
                {
                    CGridFilterForWndDataGrid filtreToSet = filtre;
                    if (filtreActif != null && filtreActif.GetType() == filtre.GetType())
                        filtreToSet = filtreActif;
                    ToolStripMenuItem itemFiltre = new ToolStripMenuItem(filtreToSet.Label);
                    itemFiltre.Tag = filtreToSet;
                    itemFiltre.Click += new EventHandler(itemFiltre_Click);
                    itemFiltre.Checked = filtreToSet == filtreActif;
                    m_menuFiltreSpecial.DropDownItems.Add(itemFiltre);
                }
            }
            IEnumerable<object> source = m_listeObjetsOriginale;
            if (source != null)
            {
                CGridFilterListeValeurs filtreListe = filtreActif as CGridFilterListeValeurs;
                List<CCoupleValeurEtValeurDisplay> lstValeurs = new List<CCoupleValeurEtValeurDisplay>();
                if (filtreListe == null)
                    source = m_grid.DataSource as IEnumerable<object>;
                foreach (object obj in source)
                {
                    CCoupleValeurEtValeurDisplay valeur = m_cache.GetValeur(obj, nColIndex, false);
                    if (valeur != null)
                        lstValeurs.Add(valeur);
                }
                m_valueSelector.Selector.FillWithValues(lstValeurs.ToArray(), filtreListe);
                if ( !m_menuFiltre.Items.Contains ( m_valueSelector ) )
                    m_menuFiltre.Items.Add(m_valueSelector);
            }
            
        }

        //-------------------------------------------------------------
        void itemFiltre_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            CGridFilterForWndDataGrid filtre = item != null ? item.Tag as CGridFilterForWndDataGrid : null;
            if (filtre != null)
            {
                IEditeurFiltreGrid editeur = CGestionnaireEditeurFiltres.GetEditeur(filtre.GetType());
                if (editeur != null)
                {
                    if (editeur.EditeFiltre(filtre))
                    {
                        m_dicFiltre[m_nColIndexForFiltre] = filtre;
                        Filtrer();
                    }
                }
                else
                {
                    m_dicFiltre[m_nColIndexForFiltre] = filtre;
                    Filtrer();
                }
            }
        }


        private void m_grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                C2iWndDataGridColumn col = m_wndGrid.GetColumn(e.ColumnIndex);
                Rectangle rct = m_grid.GetColumnDisplayRectangle(e.ColumnIndex, false);
                if (e.X > rct.Width - c_nWidthImageHeader)//Filtre
                {
                    if (col != null && col.AllowFilter)
                    {
                        FillMenuFiltre(e.ColumnIndex, col);
                            DataGridViewColumn gvCol = m_grid.Columns[e.ColumnIndex];
                            rct = m_grid.GetColumnDisplayRectangle(e.ColumnIndex, true);
                            rct.Height = gvCol.HeaderCell.ContentBounds.Height;

                            m_menuFiltre.Show(m_grid, rct.Left, rct.Bottom);
                        /*}
                        else
                        {
                            string strText = null;
                            if (!m_dicFiltre.TryGetValue(e.ColumnIndex, out strText))
                                strText = "";
                            CFormFiltreDataGrid.OnFiltrerDataGridEventHandler handler = new CFormFiltreDataGrid.OnFiltrerDataGridEventHandler(OnFiltrer);
                            CFormFiltreDataGrid.Show(col, e.ColumnIndex, strText, rct.Width, handler);
                        }*/

                    }
                }
                else
                {
                    if (col != null && col.AllowSort)
                    {
                        Sort(e.ColumnIndex);
                    }
                }
            }
        }

        private void OnFiltrer(int nColumn, CGridFilterForWndDataGrid filtre)
        {
            m_dicFiltre[nColumn] = filtre;
            Filtrer();
        }

        void m_valueSelector_OnOkClicked(object sender, EventArgs e)
        {
            if (m_nColIndexForFiltre >= 0)
            {
                m_dicFiltre[m_nColIndexForFiltre] = m_valueSelector.Selector.GetFiltre();
                Filtrer();
            }
        }



        private void Filtrer()
        {
            List<object> lst = m_listeObjetsOriginale;
            if (lst == null)
                return;
            using (CWaitCursor waiter = new CWaitCursor())
            {
                bool bCancel = false;
                foreach (KeyValuePair<int, CGridFilterForWndDataGrid> kv in m_dicFiltre)
                {
                    if (kv.Value != null)
                    {
                        CGridFilterForWndDataGrid filter = kv.Value;
                        C2iWndDataGridColumn col = m_wndGrid.GetColumn(kv.Key);
                        IWndIncluableDansDataGrid ctrl = col.Control as IWndIncluableDansDataGrid;
                        if (ctrl != null)
                        {
                            List<object> lst2 = new List<object>();
                            foreach (object obj in lst)
                            {
                                int nVal = (int)Keys.Escape;

                                nVal = GetKeyState((int)Keys.Escape);
                                if (nVal < 0)
                                {
                                    bCancel = true;
                                    break;
                                }
                                CCoupleValeurEtValeurDisplay dataVal = m_cache.GetValeur(obj, kv.Key, col.MultiThread);
                                if (dataVal != null && filter.IsValueIn(dataVal.ObjectValue))
                                    lst2.Add(obj);
                            }
                            lst = lst2;
                        }
                    }
                    if (bCancel)
                        break;
                }
                if (bCancel)
                {
                    lst = m_listeObjetsOriginale;
                    MessageBox.Show(I.T("Filter has been canceled|20019"));
                }
            }
            if (m_nColumnSort != null)
            {
                try
                {
                    lst.Sort(new CGridSorter(m_nColumnSort.Value, m_bSortAsc, m_cache));
                }
                catch { }
            }
            m_grid.DataSource = lst;
            foreach (DataGridViewColumn col in m_grid.Columns)
            {
                CDataGridViewCustomCellFor2iWnd cell = col.CellTemplate as CDataGridViewCustomCellFor2iWnd;
            }
        }

        private void Sort(int nColonne)
        {
            List<object> lst = m_grid.DataSource as List<object>;
            if (lst != null)
            {
                if (m_nColumnSort != null && Math.Abs(m_nColumnSort.Value) == nColonne)
                    m_bSortAsc = !m_bSortAsc;
                else
                {
                    m_nColumnSort = nColonne;
                    m_bSortAsc = true;
                }
                try
                {
                    lst.Sort(new CGridSorter(m_nColumnSort.Value, m_bSortAsc, m_cache));
                }
                catch { }
                m_grid.DataSource = null;
                m_grid.DataSource = lst;
            }
        }

        private class CGridSorter : IComparer<object>
        {
            private int m_nColonneSort = 0;
            private CGridDataCache m_datas = null;
            private bool m_bAsc = true;

            public CGridSorter(int nColonne, bool bAsc, CGridDataCache datas)
            {
                m_nColonneSort = nColonne;
                m_datas = datas;
                m_bAsc = bAsc;
            }

            #region IComparer Membres

            public int Compare(object x, object y)
            {
                IComparable v1 = m_datas.GetValeur(x, m_nColonneSort, false) as IComparable;
                IComparable v2 = m_datas.GetValeur(y, m_nColonneSort, false) as IComparable;
                if (v1 == null && v2 != null)
                    return m_bAsc ? 1 : -1;
                if (v2 == null && v1 == null)
                    return m_bAsc ? 1 : -1;
                return m_bAsc ? v1.CompareTo(v2) : -v1.CompareTo(v2);
            }

            #endregion
        }

        private void m_grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                Rectangle rct = e.CellBounds;
                rct.Inflate(-1, -1);
                CGridFilterForWndDataGrid filtre = null;
                m_dicFiltre.TryGetValue(e.ColumnIndex, out filtre);
                C2iWndDataGridColumn col = m_wndGrid.GetColumn(e.ColumnIndex);
                bool bHasFiltre = col != null && col.AllowFilter;
                if (bHasFiltre)
                    rct = new Rectangle(rct.Location, new Size(rct.Size.Width - c_nWidthImageHeader, rct.Height));
                DataGridViewCellStyle style = m_grid.Columns[e.ColumnIndex].HeaderCell.Style;
                Brush br = new SolidBrush(style.BackColor);
                e.Graphics.FillRectangle(br, e.CellBounds);
                br.Dispose();
                Pen pen = new Pen(m_grid.GridColor);
                e.Graphics.DrawRectangle(pen, new Rectangle(e.CellBounds.Location, new Size(e.CellBounds.Width - 1, e.CellBounds.Height - 1)));
                br = new SolidBrush(style.ForeColor);
                e.Graphics.DrawString(m_grid.Columns[e.ColumnIndex].HeaderText, style.Font, br, rct);
                br.Dispose();
                if (bHasFiltre)
                {
                    if (filtre != null )
                    {
                        e.Graphics.DrawImageUnscaled(Properties.Resources.filtre, new Point(rct.Right, rct.Top));
                    }
                    else
                    {
                        e.Graphics.DrawImageUnscaled(Properties.Resources.FiltreListe, new Point(rct.Right, rct.Top));
                    }
                }
                e.Handled = true;
            }
        }

        public void OnDataArrivé(object obj, int nCol)
        {
            List<object> lst = m_grid.DataSource as List<object>;
            if (lst != null)
            {
                int nIndex = lst.IndexOf(obj);
                if (nIndex >= 0)
                {
                    m_grid.InvalidateCell(nCol, nIndex);
                }
            }
        }

        public object ActiveElement
        {
            get
            {
                if (!m_bIsBinding && m_grid != null && m_grid.CurrentRow != null)
                    return m_grid.CurrentRow.DataBoundItem;
                return null;
            }
        }

        public event EventHandler ActiveElementChanged;
        private void m_grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void m_grid_SelectionChanged(object sender, EventArgs e)
        {
            if (ActiveElementChanged != null)
                ActiveElementChanged(this, null);
        }

        private bool m_bIsBinding = true;
        private void m_grid_DataSourceChanged(object sender, EventArgs e)
        {
            m_bIsBinding = true;
        }

        private void m_grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            m_bIsBinding = false;
        }

        private void m_grid_CellStyleChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void m_grid_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_rclickMenu.Show(m_grid, new Point(e.X, e.Y));
            }
        }

        private void m_menuSelectAll_Click(object sender, EventArgs e)
        {
            m_grid.SelectAll();
        }

        private void m_menuCopier_Click(object sender, EventArgs e)
        {
            List<DataGridViewCell> lst = new List<DataGridViewCell>();
            if (!(m_grid.DataSource is List<object>))
                return;
            foreach (DataGridViewCell ce in m_grid.SelectedCells)
            {
                if (ce.ColumnIndex >= 0 && ce.RowIndex > 0)
                    lst.Add(ce);
            }
            if (lst.Count == 0)
                return;

            lst.Sort((x, y) => x.RowIndex == y.RowIndex ? x.ColumnIndex.CompareTo(y.ColumnIndex) : x.RowIndex.CompareTo(y.RowIndex));

            int nMinLine = lst.Min(c => c.RowIndex);
            int nMinCol = lst.Min(c => c.ColumnIndex);
            int nMaxLine = lst.Max(c => c.RowIndex);
            int nMaxCol = lst.Max(c => c.ColumnIndex);

            StringBuilder bl = new StringBuilder();
            DataGridViewCell cell = lst[0];
            int nCell = 0;
            for (int n = nMinLine; n <= nMaxLine; n++)
            {
                object obj = null;
                if (n >= 0 && n < ((List<object>)m_grid.DataSource).Count)
                    obj = ((List<object>)m_grid.DataSource)[n];
                if (obj != null)
                {
                    for (int c = nMinCol; c <= nMaxCol; c++)
                    {
                        string strVal = "";
                        if (cell.RowIndex == n && cell.ColumnIndex == c)
                        {
                            CCoupleValeurEtValeurDisplay data = m_cache.GetValeur(obj, c, false);
                            if (data != null)
                                strVal = data.StringValue;
                            nCell++;
                            if (nCell < lst.Count)
                                cell = lst[nCell];
                            else
                                cell = null;
                        }
                        bl.Append(strVal);
                        if ( c < nMaxCol )
                            bl.Append('\t');
                        if (cell == null)
                            break;
                    }
                }
                if ( n < nMaxLine )
                    bl.Append(System.Environment.NewLine);
                if (cell == null)
                    break;
            }
            Clipboard.SetDataObject(bl.ToString());
        }

        //----------------------------------------------------------------------
        private void m_menuNoFilter_Click(object sender, EventArgs e)
        {
            if (m_nColIndexForFiltre >= 0)
            {
                m_dicFiltre[m_nColIndexForFiltre] = null;
                Filtrer();
            }
        }
    }

}
