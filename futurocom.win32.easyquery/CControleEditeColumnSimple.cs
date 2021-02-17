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
    public partial class CControleEditeColumnSimple : CCustomizableListControl
    {
        //--------------------------------------------------------------
        public CControleEditeColumnSimple()
        {
            InitializeComponent();
            FillComboTypes();
            
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
            CItemColumnSimple itemSort = item as CItemColumnSimple;
            if ( itemSort != null )
            {
                m_txtColName.Text = itemSort.ColumnSimple.ColumnName;
                m_cmbDataType.SelectedValue = itemSort.ColumnSimple.DataType;
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
            CItemColumnSimple item = CurrentItem as CItemColumnSimple;
            if (item != null)
            {
                item.ColumnSimple.ColumnName = m_txtColName.Text;
                if (m_cmbDataType.SelectedValue is Type)
                {
                    item.ColumnSimple.DataType = m_cmbDataType.SelectedValue as Type;
                }
            }
            return result;
        }

        //--------------------------------------------------------------
        private void FillComboTypes()
        {
            List<KeyValuePair<Type, string>> lstTypes = new List<KeyValuePair<Type, string>>();
            List<Type> lst = new List<Type>();
            lst.Add(typeof(string));
            lst.Add(typeof(int));
            lst.Add(typeof(double));
            lst.Add(typeof(DateTime));
            lst.Add(typeof(bool));
            foreach ( Type tp in lst)
                lstTypes.Add(new KeyValuePair<Type, string>(tp, DynamicClassAttribute.GetNomConvivial(tp)));
            m_cmbDataType.DataSource = lstTypes;
            m_cmbDataType.DisplayMember = "Value";
            m_cmbDataType.ValueMember = "Key";
        }
    }


    public class CItemColumnSimple : CCustomizableListItem
    {
        private CColumnEQSimple m_colonne = new CColumnEQSimple();

        //--------------------------------------------------------
        public CItemColumnSimple()
        {
        }

        //--------------------------------------------------------
        public CItemColumnSimple(CColumnEQSimple col)
        {
            m_colonne = col;
        }

        //--------------------------------------------------------
        public CColumnEQSimple ColumnSimple
        {
            get{
                return m_colonne;
            }
            set
            {
                m_colonne = value;
            }
        }
    }
}
