using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sc2i.common;
using sc2i.data.dynamic.StructureImport.SmartImport;

namespace sc2i.win32.data.dynamic.import.sources
{
    [AutoExec("Autoexec")]
    public partial class CControleEditeSourceImportFixedValue : UserControl, IEditeurSourceSmartImport
    {
        private CSourceSmartImportFixedValue m_source = null;
        private DataTable m_sourceTable = null;
        public CControleEditeSourceImportFixedValue()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------
        public static void Autoexec()
        {
            CControleEditeSourceImport.RegisterEditeur(typeof(CSourceSmartImportFixedValue), typeof(CControleEditeSourceImportFixedValue));
        }

        //---------------------------------------------------------------
        public void SetIsDrawingImage(bool bIsDrawingImage)
        {
        }

        //-------------------------------------------------
        public event EventHandler ValueChanged;

        //---------------------------------------------------------------
        public DataTable SourceTable
        {
            get { return m_sourceTable; }
            set { m_sourceTable = value; }
        }


        //---------------------------------------------------------------
        public void SetSource(CSourceSmartImport source, CSetupSmartImportItem currentItem)
        {
            m_source = source as CSourceSmartImportFixedValue;
            if (m_source == null)
                m_source = new CSourceSmartImportFixedValue();
            if (m_source.Valeur == null)
                m_lblValue.Text = "null";
            else
                m_lblValue.Text = m_source.Valeur.ToString();
        }

        //---------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            return CResultAErreur.True;
        }
    }
}
