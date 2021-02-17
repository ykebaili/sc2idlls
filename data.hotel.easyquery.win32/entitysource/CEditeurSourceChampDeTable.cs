using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.expression;
using sc2i.common;
using futurocom.easyquery;

namespace data.hotel.easyquery.win32.entitysource
{
    [AutoExec("Autoexec")]
    public partial class CEditeurSourceChampDeTable : UserControl, IEditeurSourceEntite
    {
        private CSourceEntitesPourTableDataChampDeTable m_source = null;
        private CODEQTableFromDataHotel m_tableFromHotel = null;
        //---------------------------------------------------------------------
        public CEditeurSourceChampDeTable()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------------
        public static void Autoexec()
        {
            CPanelEditSourceEntites.RegisterTypeEditeurSource ( typeof(CSourceEntitesPourTableDataChampDeTable),
                typeof(CEditeurSourceChampDeTable),
                I.T("Table|20037"));
        }
        //---------------------------------------------------------------------
        public void Init(ISourceEntitesPourTableDataHotel source, CODEQTableFromDataHotel table)
        {
            m_source = source as CSourceEntitesPourTableDataChampDeTable;
            m_tableFromHotel = table;
            if (m_source == null)
                m_source = new CSourceEntitesPourTableDataChampDeTable();
            List<CODEQBase> lstTables = new List<CODEQBase>();
            foreach ( IObjetDeEasyQuery obj in table.Query.Childs )
            {
                CODEQBase t = obj as CODEQBase;
                if (t != null)
                    lstTables.Add(t);
            }
            m_cmbTable.DataSource = lstTables;
            m_cmbTable.DisplayMember = "NomFinal";
            m_cmbTable.ValueMember = "Id";
            m_cmbTable.SelectedValue = m_source.IdTable;
            UpdateColonnes();
        }

        //--------------------------------------------------------------------
        private void UpdateColonnes()
        {
            m_panelChamp.Visible = false;
            string strId = m_cmbTable.SelectedValue as string;
            if (strId != null && strId.Length > 0)
            {
                foreach ( IObjetDeEasyQuery obj in m_tableFromHotel.Query.Childs)
                {
                    CODEQBase table = obj as CODEQBase;
                    if ( table != null && table.Id == strId )
                    {
                        m_panelChamp.Visible = true;
                        m_cmbColonne.DataSource = table.Columns;
                        m_cmbColonne.DisplayMember = "ColumnName";
                        m_cmbColonne.ValueMember = "Id";
                        m_cmbColonne.SelectedValue = m_source.IdColonne;
                    }
                }
            }
        }

        //--------------------------------------------------------------------
        public CResultAErreurType<ISourceEntitesPourTableDataHotel> MajChamps()
        {
            CResultAErreurType<ISourceEntitesPourTableDataHotel> result = new CResultAErreurType<ISourceEntitesPourTableDataHotel>();

            if (!(m_cmbTable.SelectedValue is string))
            {
                result.EmpileErreur(I.T("Select a source table|20040"));
                return result;
            }
            if (!(m_cmbColonne.SelectedValue is string))
            {
                result.EmpileErreur(I.T("Select a source columns|20041"));
                return result;
            }
            m_source.IdTable = m_cmbTable.SelectedValue as string;
            m_source.IdColonne = m_cmbColonne.SelectedValue as string;

            result.DataType = m_source;
            return result;
        }

        //--------------------------------------------------------------------
        private void m_cmbTable_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateColonnes();
        }
        
    }
}

