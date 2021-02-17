using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.data.dynamic.StructureImport.SmartImport;
using sc2i.common;

namespace sc2i.win32.data.dynamic.import.sources
{
    [AutoExec("Autoexec")]
    public partial class CControleEditeSourceImportField : UserControl, IEditeurSourceSmartImport
    {
        private DataTable m_sourceTable = null;
        private CSourceSmartImportField m_source = null;

        //---------------------------------------------------------------
        public CControleEditeSourceImportField()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------
        public static void Autoexec()
        {
            CControleEditeSourceImport.RegisterEditeur(typeof(CSourceSmartImportField), typeof(CControleEditeSourceImportField));
        }

        //---------------------------------------------------------------
        public void SetIsDrawingImage(bool bIsDrawingImage)
        {
        }

        //-------------------------------------------------
        public DataTable SourceTable
        {
            get
            {
                return m_sourceTable;
            }
            set { m_sourceTable = value; FillCombo(); }
        }

        //-------------------------------------------------
        public void SetSource(CSourceSmartImport source, CSetupSmartImportItem currentItem)
        {
            m_source = source as CSourceSmartImportField;
            if (m_source == null)
                m_source = new CSourceSmartImportField();
            m_cmbField.SelectedItem = m_source.NomChampSource;
        }

        //-------------------------------------------------
        public sc2i.common.CResultAErreur MajChamps()
        {
            if (m_sourceTable != null &&
                m_sourceTable.Columns[m_cmbField.Text] != null &&
                m_source != null)
                m_source.NomChampSource = m_cmbField.Text;
            return CResultAErreur.True;

        }

        //-------------------------------------------------
        private void CControleEditeSourceImportField_Load(object sender, EventArgs e)
        {
            FillCombo();
        }

        //-------------------------------------------------
        public event EventHandler ValueChanged;

        //-------------------------------------------------
        private void FillCombo()
        {
            m_cmbField.BeginUpdate();
            if (m_sourceTable != null)
                foreach (DataColumn col in m_sourceTable.Columns)
                    m_cmbField.Items.Add(col.ColumnName);
            m_cmbField.EndUpdate();
        }

        //-------------------------------------------------
        private void m_cmbField_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, null);
        }

        //-------------------------------------------------

    }
}
