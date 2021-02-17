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
using sc2i.expression;

namespace sc2i.win32.data.dynamic.import.sources
{
    [AutoExec("Autoexec")]
    public partial class CControleEditeSourceImportFormula : UserControl, IEditeurSourceSmartImport
    {
        private DataTable m_sourceTable = null;
        private CSourceSmartImportFormula m_source = null;

        //----------------------------------------------------------
        public CControleEditeSourceImportFormula()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------
        public static void Autoexec()
        {
            CControleEditeSourceImport.RegisterEditeur(typeof(CSourceSmartImportFormula), typeof(CControleEditeSourceImportFormula));
        }

        //-------------------------------------------------
        public void SetIsDrawingImage(bool bIsDrawingImage)
        {
            if ( bIsDrawingImage )
            {
                m_txtFormule.Visible = false;
                m_lblFormule.Visible = true;
                m_lblFormule.Dock = DockStyle.Fill;
            }
            else
            {
                m_txtFormule.Visible = true;
                m_lblFormule.Visible = false;
                m_txtFormule.Dock = DockStyle.Fill;
            }
        }

        //-------------------------------------------------
        public DataTable SourceTable
        {
            get
            {
                return m_sourceTable;
            }
            set {
                m_txtFormule.Init(new CFournisseurPropDynForDataTable(value), null);
                m_sourceTable = value; 
            }
        }

        //-------------------------------------------------
        private bool m_bLockEvents = false;
        public void SetSource(CSourceSmartImport source, CSetupSmartImportItem currentItem)
        {
            m_bLockEvents = true;

            m_source = source as CSourceSmartImportFormula;
            if (m_source == null)
                m_source = new CSourceSmartImportFormula();
            m_txtFormule.Formule = m_source.Formule;
            m_lblFormule.Text = m_source.Formule != null ? m_source.Formule.GetString() : "";
            m_bLockEvents = false;
        }

        //-------------------------------------------------
        public sc2i.common.CResultAErreur MajChamps()
        {
            if (m_source != null)
            {
                m_source.Formule = m_txtFormule.Formule;
                m_lblFormule.Text = m_source.Formule != null ? m_source.Formule.GetString() : "";
            }
                
            return CResultAErreur.True;

        }

        //-------------------------------------------------
        public event EventHandler ValueChanged;

        private void m_txtFormule_OnChangeTexteFormule(object sender, EventArgs e)
        {
            if (ValueChanged != null && !m_bLockEvents)
                ValueChanged(this, null);
        }

    }
}
