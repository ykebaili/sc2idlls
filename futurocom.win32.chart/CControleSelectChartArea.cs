using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;


using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.formulaire;
using futurocom.chart;
using futurocom.chart.ChartArea;

namespace futurocom.win32.chart
{
	[AutoExec("Autoexec")]
    public partial class CControleSelectChartArea : UserControl, IEditeurSelectChartArea
    {
        private IWindowsFormsEditorService m_service = null;
        private string m_strIdSel = null;
        private CChartSetup m_chartSetup = null;

        public CControleSelectChartArea()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CProprieteSelectChartAreaEditor.SetTypeEditeur(typeof(CControleSelectChartArea));
        }


        

        
        #region IEditeurProprieteFiltreDynamique Membres

        public string SelectArea ( CChartSetup chartSetup, string strIdInitial, IServiceProvider provider)
        {
            // Uses the IWindowsFormsEditorService to display a 
            // drop-down UI in the Properties window.
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
				m_service = edSvc;
                m_strIdSel = strIdInitial;
                m_chartSetup = chartSetup;
                // Affiche le controle dans une fenêtre déroulante
                edSvc.DropDownControl(this);

                return m_strIdSel;
            }

            return strIdInitial;
        }

        #endregion

        //------------------------------------------------------------------------------
        private bool m_bIsFilling = false;
        private void CControleSelectChartArea_Load(object sender, EventArgs e)
        {
            m_bIsFilling = true;
            if (m_chartSetup != null)
            {

                m_listBox.BeginUpdate();
                m_listBox.Items.Clear();
                m_listBox.Items.Add("(None)");
                
                foreach (CChartArea area in m_chartSetup.Areas)
                {
                    int nIndex = m_listBox.Items.Add(area);
                    if (m_strIdSel != null && m_strIdSel == area.AreaId)
                        m_listBox.SelectedIndex = nIndex;
                }
                m_listBox.EndUpdate();
            }
            m_bIsFilling = false;
        }

        //------------------------------------------------------------------------------
        private void m_listBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!m_bIsFilling)
            {
                CChartArea area = m_listBox.SelectedItem as CChartArea;
                if (area != null)
                    m_strIdSel = area.AreaId;
                else
                    m_strIdSel = null;
                if (m_service != null)
                    m_service.CloseDropDown();
            }
        }
    }
}
