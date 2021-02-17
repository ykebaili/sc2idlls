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
using sc2i.data;
using sc2i.expression;

namespace sc2i.win32.data.dynamic.import.sources
{
    [AutoExec("Autoexec")]
    public partial class CControleEditeSourceImportObjetRef : UserControl, IEditeurSourceSmartImport
    {
        private DataTable m_sourceTable = null;
        private CSourceSmartImportReference m_source = null;

        //---------------------------------------------------------------
        public CControleEditeSourceImportObjetRef()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------
        public static void Autoexec()
        {
            CControleEditeSourceImport.RegisterEditeur(typeof(CSourceSmartImportReference), typeof(CControleEditeSourceImportObjetRef));
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
            set { m_sourceTable = value; }
        }

        //-------------------------------------------------
        public void SetSource(CSourceSmartImport source, CSetupSmartImportItem currentItem)
        {
            m_source = source as CSourceSmartImportReference;
            if (m_source == null)
                m_source = new CSourceSmartImportReference();
            FillObjets(currentItem);
        }


        //-------------------------------------------------
        private class CCoupleObjetIdMappage
        {
            public CCoupleObjetIdMappage ( string strLibelleObjet , string strIdMappage)
            {
                LibelleObjet = strLibelleObjet;
                IdMappage = strIdMappage;
            }

            public string LibelleObjet{get;set;}
            public string IdMappage { get; set; }
        }

        //-------------------------------------------------
        private void FillObjets ( CSetupSmartImportItem item )
        {
            m_cmbObjet.BeginUpdate();
            m_cmbObjet.Items.Clear();
            m_cmbObjet.DisplayMember = "LibelleObjet";
            m_cmbObjet.ValueMember = "IdMappage";
            m_cmbObjet.Items.Add(new CCoupleObjetIdMappage(I.T("None|20230"), ""));
            if ( item != null && item.Propriete != null && m_source != null)
            {
                CDefinitionProprieteDynamique def = item.Propriete;
                Type tp = def.TypeDonnee.TypeDotNetNatif;
                CSetupSmartImportItem itemTest = item.ItemParent as CSetupSmartImportItem;
                int nIndexSel = -1;
                while (itemTest != null)
                {
                    CObjetDonnee objet = itemTest.ObjetExempleAssocie;
                    if (objet != null && tp.IsAssignableFrom(objet.GetType()))
                    {
                        m_cmbObjet.Items.Add(new CCoupleObjetIdMappage(
                            objet.DescriptionElement,
                            itemTest.IdMappage
                            ));
                        if ( itemTest.IdMappage == m_source.IdMappageReference)
                            nIndexSel = m_cmbObjet.Items.Count-1;
                    }
                    itemTest = itemTest.ItemParent as CSetupSmartImportItem;
                }
                if (nIndexSel != -1)
                    m_cmbObjet.SelectedIndex = nIndexSel;
                else
                    m_cmbObjet.SelectedIndex = 0;
                
            }
            m_cmbObjet.EndUpdate();

        }

        //-------------------------------------------------
        public sc2i.common.CResultAErreur MajChamps()
        {
            if (m_source != null)
            {
                if (m_cmbObjet.SelectedIndex > 0)
                    m_source.IdMappageReference = ((CCoupleObjetIdMappage)m_cmbObjet.Items[m_cmbObjet.SelectedIndex]).IdMappage;
                else
                    m_source.IdMappageReference = "";
            }
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
            m_cmbObjet.BeginUpdate();
            if (m_sourceTable != null)
                foreach (DataColumn col in m_sourceTable.Columns)
                    m_cmbObjet.Items.Add(col.ColumnName);
            m_cmbObjet.EndUpdate();
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
