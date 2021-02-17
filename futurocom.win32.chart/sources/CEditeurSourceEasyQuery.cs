using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using futurocom.chart;
using sc2i.expression;
using sc2i.common;

namespace futurocom.win32.chart.sources
{
    [AutoExec("Autoexec")]
    public partial class CEditeurSourceEasyQuery : Form, IFormEditSourceChart
    {
        private CParametreSourceChartEasyQuery m_sourceEasyQuery = null;

        public CEditeurSourceEasyQuery()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        public static void Autoexec()
        {
            CEditeurSourceChart.RegisterEditeur(typeof(CParametreSourceChartEasyQuery), typeof(CEditeurSourceEasyQuery));
        }

        //--------------------------------------------------------------
        private void CEditeurSourceEasyQuery_Load(object sender, EventArgs e)
        {

        }

        //--------------------------------------------------------------
        public void InitChamps(CParametreSourceChart parametre, IFournisseurProprietesDynamiques fournisseur, CObjetPourSousProprietes typeSourceGlobale)
        {
            CParametreSourceChartEasyQuery src = parametre as CParametreSourceChartEasyQuery;
            if (src == null)
                return;
            m_sourceEasyQuery = src;
            m_panelQuery.Init(src.EasyQuery);
            m_txtSourceName.Text = m_sourceEasyQuery.SourceName;
        }

        //--------------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //--------------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            m_sourceEasyQuery.SourceName = m_txtSourceName.Text;
            m_sourceEasyQuery.EasyQuery = m_panelQuery.Query;
            m_sourceEasyQuery.QuerySources = m_panelQuery.Query.Sources;
            Close();
        }

        
    }
}
