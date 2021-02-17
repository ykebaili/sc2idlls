using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common.customizableList;
using futurocom.easyquery;
using sc2i.common;

namespace futurocom.win32.easyquery
{
    public partial class CControleEditeSortColumn : CCustomizableListControl
    {
        private CODEQSort m_sortTable = null;
        private bool m_bIsComboChampInit = false;
        //--------------------------------------------------------------
        public CControleEditeSortColumn()
        {
            InitializeComponent();
            m_cmbCroissant.Items.Clear();
            m_cmbCroissant.Items.Add(I.T("Ascending|20060"));
            m_cmbCroissant.Items.Add(I.T("Descending|20061"));
            m_cmbCroissant.SelectedIndex = 0;
        }

        //--------------------------------------------------------------
        public CODEQSort SortTable
        {
            get
            {
                return m_sortTable;
            }
            set
            {
                m_sortTable = value;
                m_bIsComboChampInit = false;
            }
        }

        //--------------------------------------------------------------
        public override bool IsFixedSize
        {
            get
            {
                return true;
            }
        }

        //--------------------------------------------------------------
        protected override CResultAErreur MyInitChamps(CCustomizableListItem item)
        {
            CResultAErreur result = base.MyInitChamps(item);
            if (!result)
                return result;
            CItemSort itemSort = item as CItemSort;
            if ( itemSort != null )
            {
                FillComboChamps();
                m_cmbField.SelectedValue = itemSort.SortColonne.IdColonne;
                if (itemSort.SortColonne.Croissant)
                    m_cmbCroissant.SelectedIndex = 0;
                else
                    m_cmbCroissant.SelectedIndex = 1;
            }
            return result;
        }

        //--------------------------------------------------------------
        public override bool HasChange
        {
            get
            {
                return true;
            }
            set
            {
                base.HasChange = value;
            }
        }

        //--------------------------------------------------------------
        protected override CResultAErreur MyMajChamps()
        {
            CResultAErreur result = base.MyMajChamps();
            if (!result)
                return result;
            CItemSort item = CurrentItem as CItemSort;
            if (item != null)
            {
                item.SortColonne.Croissant = m_cmbCroissant.SelectedIndex == 0;
                if (m_cmbField.SelectedValue is string)
                {
                    item.SortColonne.IdColonne = m_cmbField.SelectedValue.ToString();
                }
            }
            return result;
        }

        //--------------------------------------------------------------
        private void FillComboChamps()
        {
            if (m_bIsComboChampInit)
                return;
            if (m_sortTable != null)
            {
                List<KeyValuePair<string, string>> lstCols = new List<KeyValuePair<string, string>>();
                foreach (IColumnDeEasyQuery col in m_sortTable.ColonnesOrdonnees)
                    lstCols.Add(new KeyValuePair<string, string>(col.ColumnName, col.Id));
                m_cmbField.DataSource = lstCols;
                m_cmbField.DisplayMember = "Key";
                m_cmbField.ValueMember = "Value";
                m_bIsComboChampInit = true;
            }


        }
    }


    public class CItemSort : CCustomizableListItem
    {
        private CODEQSort.CSortColonne m_sortColonne = new CODEQSort.CSortColonne();

        //--------------------------------------------------------
        public CItemSort()
        {
        }

        //--------------------------------------------------------
        public CItemSort ( CODEQSort.CSortColonne sort )
        {
            m_sortColonne = sort;
        }

        //--------------------------------------------------------
        public CODEQSort.CSortColonne SortColonne
        {
            get{
                return m_sortColonne;
            }
            set{m_sortColonne = value;
            }
        }
    }
}
