using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using sc2i.win32.common;
using sc2i.formulaire;
using sc2i.formulaire.win32;
using sc2i.expression;
using sc2i.formulaire.datagrid;
using System.Collections;
using System.Threading;
using sc2i.formulaire.win32.controles2iWnd.datagrid;

namespace sc2i.formulaire.win32.datagrid
{
	public class CDataGridViewCustomCellFor2iWnd : DataGridViewTextBoxCell
	{
        private CListeRestrictionsUtilisateurSurType m_listeRestrictions = null;
        private const string c_strWaitingData = "###RPG123498QS";
        CControlEncapsuleWndControl m_controleEdition = null;
        private IWndIncluableDansDataGrid m_2iWndForGrid = null;
        private IFournisseurProprietesDynamiques m_fournisseur = null;
        private C2iWndDataGridColumn m_wndCol = null;

        private CGridDataCache m_datas = null;

        private int m_nColumnIndex = 0;

		public CDataGridViewCustomCellFor2iWnd()
			: base()
		{ }
        public CDataGridViewCustomCellFor2iWnd(
            CGridDataCache datas,
            C2iWndDataGridColumn wndCol, 
            int nColumnIndex,
            IWndIncluableDansDataGrid wndAssociee, 
            IFournisseurProprietesDynamiques fournisseur)
			: base()
		{
            m_datas = datas;
            m_nColumnIndex = nColumnIndex;
            m_2iWndForGrid = wndAssociee;
            m_fournisseur = fournisseur;
            m_wndCol = wndCol;
            		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			
		}

        public void InitRestrictions(CListeRestrictionsUtilisateurSurType lstRestrictions)
        {
            m_listeRestrictions = lstRestrictions;
            if (m_controleEdition != null)
                m_controleEdition.InitRestrictions(lstRestrictions);
        }

		
		//Penser à garder la reference vers la variable objet initialFormattedValue > le pointeur est important
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            m_controleEdition = DataGridView.EditingControl as CControlEncapsuleWndControl;

            m_controleEdition.Initialiser(
                m_datas,
                m_nColumnIndex,
                m_2iWndForGrid, m_fournisseur);
            DataGridView grid = DataGridView;
            if (grid != null)
            {
                Control parent = grid.Parent;
                while (parent != null && !(parent is CDataGridForFormulaire))
                    parent = parent.Parent;
                if (parent is CDataGridForFormulaire)
                    m_listeRestrictions = ((CDataGridForFormulaire)parent).ListeRestrictions;
            }
            m_controleEdition.InitRestrictions(m_listeRestrictions);

		}

		
        //-------------------------------------------------
		public override Type ValueType
		{
			get
			{
                return m_2iWndForGrid.ValueTypeForGrid;
			}
			set
			{
				base.ValueType = value;
			}
		}
		public override Type EditType
		{
			get
			{
				return typeof(CControlEncapsuleWndControl);
			}
		}


		public override object Clone()
		{
			CDataGridViewCustomCellFor2iWnd cell = base.Clone() as CDataGridViewCustomCellFor2iWnd;
			if (cell != null)
			{
                cell.m_2iWndForGrid = m_2iWndForGrid;
                cell.m_fournisseur = m_fournisseur;
                cell.m_wndCol = m_wndCol;
                cell.m_datas = m_datas;
                cell.m_nColumnIndex = m_nColumnIndex;
			}
			return cell;
		}

		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)
		{
			if (formattedValue == null || formattedValue == DBNull.Value)
				return DBNull.Value;
			else
			{
				return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
			}
		}

		protected override bool SetValue(int rowIndex, object value)
		{
            if (this.m_controleEdition != null)
            {
                this.m_controleEdition.MajChamps();
            }
            object element = null;
            if (DataGridView != null)
            {
                IList source = DataGridView.DataSource as IList;
                element = source[rowIndex];
            }
            m_datas.SetValeur(element, ColumnIndex, null, null);
            m_datas.GetValeur(element, ColumnIndex, false);
            
			return base.SetValue(rowIndex, value);
		}

		protected override void Paint(
            Graphics graphics, 
            Rectangle clipBounds, 
            Rectangle cellBounds, 
            int rowIndex, 
            DataGridViewElementStates cellState, 
            object value, 
            object formattedValue, 
            string errorText, 
            DataGridViewCellStyle cellStyle, 
            DataGridViewAdvancedBorderStyle advancedBorderStyle, 
            DataGridViewPaintParts paintParts)
		{

            bool bStandardPaint = true;
            object element = null;
            if (DataGridView != null)
            {
                IList source = DataGridView.DataSource as IList;
                element = source[rowIndex];
            }
            
            IWndIncluableDansDataGridADrawCustom ctrlDraw = m_2iWndForGrid as IWndIncluableDansDataGridADrawCustom;
            if (ctrlDraw != null)
                bStandardPaint = ctrlDraw.Paint(
                    graphics,
                    clipBounds,
                    cellBounds,
                    element);
            if (bStandardPaint)
            {
                CCoupleValeurEtValeurDisplay data = m_datas.GetValeur(element, ColumnIndex, m_wndCol != null && m_wndCol.MultiThread);
                if (data != null)
                {
                    if (data.StringValue == CGridDataCache.c_strWaitingData)
                    {
                        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, "", errorText, cellStyle, advancedBorderStyle, paintParts);
                        graphics.DrawImageUnscaled(Properties.Resources.Loading, new Point(cellBounds.Left, cellBounds.Top));
                    }
                    else
                        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, data.StringValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                }
            }
            
		}

	}
}
