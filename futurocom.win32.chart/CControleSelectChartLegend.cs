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
using futurocom.chart.LegendArea;

namespace futurocom.win32.chart
{
	[AutoExec("Autoexec")]
    public partial class CControleSelectLegendArea : UserControl, IEditeurSelectChartLegend
    {
        private IWindowsFormsEditorService m_service = null;
        private string m_strIdSel = null;
        private CChartSetup m_chartSetup = null;

        public CControleSelectLegendArea()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CProprieteSelectChartLegendEditor.SetTypeEditeur(typeof(CControleSelectLegendArea));
        }

        
        //---------------------------------------------------------------------------------
        public string SelectLegend ( CChartSetup chartSetup, string strIdInitial, IServiceProvider provider)
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


        //------------------------------------------------------------------------------
        private bool m_bIsFilling = false;
        private void CControleSelectLegendArea_Load(object sender, EventArgs e)
        {
            m_bIsFilling = true;
            if (m_chartSetup != null)
            {

                m_listBox.BeginUpdate();
                m_listBox.Items.Clear();
                m_listBox.Items.Add("(None)");
                
                foreach (CLegendArea legend in m_chartSetup.Legends)
                {
                    int nIndex = m_listBox.Items.Add(legend);
                    if (m_strIdSel != null && m_strIdSel == legend.LegendId)
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
                CLegendArea legend = m_listBox.SelectedItem as CLegendArea;
                if (legend != null)
                    m_strIdSel = legend.LegendId;
                else
                    m_strIdSel = null;
                if (m_service != null)
                    m_service.CloseDropDown();
            }
        }
    }
}
