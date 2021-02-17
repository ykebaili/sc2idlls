 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.chart;
using sc2i.common;
using sc2i.expression;
using sc2i.win32.common;
using sc2i.formulaire;
using futurocom.win32.chart.series;
using System.Windows.Forms.DataVisualization.Charting;
using futurocom.chart.ChartArea;

namespace futurocom.win32.chart
{
    //---------------------------------------------------------
    public partial class CFormEditChartSetup : Form
    {
        private CChartSetup m_chartSetup = null;


        public CFormEditChartSetup()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);

            foreach (ESelectSerieAlignment s in Enum.GetValues(typeof(ESelectSerieAlignment)))
                m_cmbSelectSeriesAlignment.Items.Add(s.ToString());
        }

        //---------------------------------------------------------
        public static bool EditeSetup(CChartSetup chartSetup)
        {
            using (CFormEditChartSetup frm = new CFormEditChartSetup())
            {
                frm.m_chartSetup = chartSetup;
                if (frm.ShowDialog() == DialogResult.OK)
                    return true;
                return false;
            }
        }

        //---------------------------------------------------------
        private void CFormEditChartSetup_Load(object sender, EventArgs e)
        {
            CFournisseurValeursSerieEditor.ChartSetup = m_chartSetup;
            CProprieteSelectChartAreaEditor.ChartSetup = m_chartSetup;
            CProprieteSelectChartLegendEditor.ChartSetup = m_chartSetup;
            CActionSur2iLinkEditor.SetObjet(new CObjetPourSousProprietes(typeof(CValeurPourChartAction)));
            m_panelData.Init(m_chartSetup);
            m_panelSeries.Init(m_chartSetup);
            m_panelAreas.Init(m_chartSetup);
            m_panelLegends.Init(m_chartSetup);
            m_panelVariables.Init(m_chartSetup);

            m_panelFormulaireFiltreAvance.Init(typeof(CChartSetup),
                m_chartSetup,
                m_chartSetup.FormulaireFiltreAvance,
                new CFournisseurGeneriqueProprietesDynamiques());

            m_panelFormulaireFiltreSimple.Init(typeof(CChartSetup),
                m_chartSetup,
                m_chartSetup.FormulaireFiltreSimple,
                new CFournisseurGeneriqueProprietesDynamiques());

            m_cmbSelectSeriesAlignment.SelectedIndex = (int)m_chartSetup.SelectSeriesAlignment;
            m_chkAllow3DSetup.Checked = m_chartSetup.Autoriser3D;
            m_chkAllowZoom.Checked = m_chartSetup.AutoriserZoom;

        }


        //---------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //---------------------------------------------------------
        private void m_btnOk_Click(object sender, EventArgs e)
        {
            CResultAErreur result = MajChamps();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        //---------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            result = m_panelVariables.MajChamps();
            if (result) result = m_panelData.MajChamps();
            if (result) result = m_panelAreas.MajChamps();
            if (result) result = m_panelLegends.MajChamps();
            if (result) result = m_panelSeries.MajChamps();
            if (result)
            {
                m_chartSetup.FormulaireFiltreAvance = m_panelFormulaireFiltreAvance.WndEditee as C2iWndFenetre;
                m_chartSetup.FormulaireFiltreSimple = m_panelFormulaireFiltreSimple.WndEditee as C2iWndFenetre;
                m_chartSetup.SelectSeriesAlignment = (ESelectSerieAlignment)m_cmbSelectSeriesAlignment.SelectedIndex;
                m_chartSetup.Autoriser3D = m_chkAllow3DSetup.Checked ;
                m_chartSetup.AutoriserZoom = m_chkAllowZoom.Checked;
            }
            if (!result)
                return result;
            

            return result;
        }

        //---------------------------------------------------------
        private void m_btnRefreshPreview_Click(object sender, EventArgs e)
        {
            CResultAErreur result = MajChamps();
            if (!result)
            {
                CFormAlerte.Afficher(result.Erreur);
                return;
            }
            C2iWndChart wndChart = new C2iWndChart();
            wndChart.ChartSetup = m_chartSetup;
            m_preview.Init(m_chartSetup, null);
        }

        private void m_btnApply3D_Click(object sender, EventArgs e)
        {
            foreach (ChartArea ms in m_preview.ChartControl.ChartAreas)
            {
                foreach (CChartArea f in m_chartSetup.Areas)
                {
                    if (f.AreaId == ms.Name)
                    {
                        f.Area3DStyle.Enable3D = ms.Area3DStyle.Enable3D;
                        f.Area3DStyle.Inclination = ms.Area3DStyle.Inclination;
                        f.Area3DStyle.Perspective = ms.Area3DStyle.Perspective;
                        f.Area3DStyle.PointDepth = ms.Area3DStyle.PointDepth;
                        f.Area3DStyle.Rotation = ms.Area3DStyle.Rotation;
                    }
                }
            }

        }

        private void m_panelData_Load(object sender, EventArgs e)
        {

        }

        private void c2iTabControl1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void m_panelAreas_MouseUp(object sender, MouseEventArgs e)
        {
                    }


    }


    //---------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurChartSetup : IEditeurChartSetup
    {
        //------------------------------------------------------------------
        public static void Autoexec()
        {
            CProprieteChartSetupEditor.SetTypeEditeur ( typeof(CEditeurChartSetup));
        }
        
        //------------------------------------------------------------------
        public CChartSetup EditeParametreChart(CChartSetup parametre)
        {
            CChartSetup copie = CCloner2iSerializable.Clone(parametre) as CChartSetup;
            copie.IContexteDonnee = parametre.IContexteDonnee;
            copie.ElementAVariablesExternes = parametre.ElementAVariablesExternes;
            CObjetPourSousProprietes oldTp = CProprieteExpressionEditor.ObjetPourSousProprietes;
            IFournisseurProprietesDynamiques oldFournisseur = CProprieteExpressionEditor.FournisseurProprietes;
            try
            {
                CProprieteExpressionEditor.ObjetPourSousProprietes = typeof(CChartSetup);
                CProprieteExpressionEditor.FournisseurProprietes = new CFournisseurGeneriqueProprietesDynamiques();
                if (CFormEditChartSetup.EditeSetup(copie))
                    return copie;
            }
            catch { }
            finally
            {
                CProprieteExpressionEditor.ObjetPourSousProprietes = oldTp;
                CProprieteExpressionEditor.FournisseurProprietes = oldFournisseur;
            }
            return parametre;
        }
        
    }


    
}
