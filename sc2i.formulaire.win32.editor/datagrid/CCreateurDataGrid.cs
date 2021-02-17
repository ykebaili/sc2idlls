using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.formulaire.datagrid;
using sc2i.formulaire;
using sc2i.formulaire.win32;
using sc2i.expression;
using sc2i.formulaire.win32.controles2iWnd.datagrid;
using sc2i.formulaire.win32.datagrid;
using sc2i.common;
using System.Windows.Forms;

namespace sc2i.formulaire.win32.datagrid
{
    public class CCreateurDataGrid
    {
        private C2iWndDataGrid m_wndGrid = new C2iWndDataGrid();


        public C2iWndDataGridColumn AddColumn(
            string strLibelle,
            IWndIncluableDansDataGrid wnd)
        {
            C2iWndDataGridColumn col = new C2iWndDataGridColumn();
            col.Text = strLibelle;
            col.ColumnWidth = ((C2iWnd)wnd).Size.Width;
            col.Control = (C2iWnd)wnd;
            m_wndGrid.AddChild(col);
            col.Parent = m_wndGrid;
            return col;
        }

        public C2iWndDataGrid WndGrid
        {
            get
            {
                return m_wndGrid;
            }
        }





        public CWndFor2iDataGrid CreateGrid(Control parent, object liste)
        {
            CCreateur2iFormulaireV2 createur = new CCreateur2iFormulaireV2();
            m_wndGrid.Font = parent.Font;
            m_wndGrid.DockStyle = EDockStyle.Fill;
            CResultAErreur result = createur.CreateControlePrincipalEtChilds(parent, m_wndGrid, new CFournisseurGeneriqueProprietesDynamiques());
            CWndFor2iDataGrid grid = null;
            if (result)
            {
                foreach (object o in createur.ControlesPrincipaux)
                    if (o is CWndFor2iDataGrid)
                    {
                        grid = o as CWndFor2iDataGrid;
                        break;
                    }
            }
            if (grid != null)
            {
                grid.Control.Dock = System.Windows.Forms.DockStyle.Fill;
                grid.Source = liste;
            }
            return grid;

        }
    }
}
