using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using sc2i.formulaire;
using sc2i.common;
using sc2i.win32.common;
using sc2i.expression;
using System.Drawing;
using sc2i.common.Restrictions;
using futurocom.chart;
using futurocom.win32.chart;
using sc2i.formulaire.win32;
using sc2i.formulaire.win32.controles2iWnd;


namespace futurocom.win32.chart
{
	[AutoExec ( "Autoexec")]
	public class CWndFor2iChart : CControlWndFor2iWnd
	{
        private C2iWndChart m_wndChart = null;
        private CCreateur2iFormulaireV2 m_createur = null;
        private IFournisseurProprietesDynamiques m_fournisseur = null;

		//---------------------------------------------------------------
        public static void Autoexec()
        {
			CCreateur2iFormulaireV2.RegisterEditeur(typeof(C2iWndChart), typeof(CWndFor2iChart));
        }

		//---------------------------------------------------------------
		private CControlChart m_controlChart = null;

		//---------------------------------------------------------------
		public CWndFor2iChart()
		{
		}

		//---------------------------------------------------------------
		protected override void MyCreateControle(
			CCreateur2iFormulaireV2 createur,
			C2iWnd wnd,
			Control parent,
			IFournisseurProprietesDynamiques fournisseurProprietes)
		{
            m_createur = createur;
            m_fournisseur = fournisseurProprietes;
            m_wndChart = wnd as C2iWndChart;
			if (m_wndChart == null)
				return;

			m_controlChart = new CControlChart();

            m_controlChart.BeforeCalculate += new EventHandler(m_controlChart_BeforeCalculate);

            CCreateur2iFormulaireV2.AffecteProprietesCommunes(m_wndChart, m_controlChart);
			parent.Controls.Add(m_controlChart);
		}

        //---------------------------------------------------------------
        void m_controlChart_BeforeCalculate(object sender, EventArgs e)
        {
            CUtilControlesWnd.DeclencheEvenement(C2iWndChart.c_strIdBeforeCalculate, this);
        }

		//---------------------------------------------------------------
		public override Control Control
		{
			get
			{
				return m_controlChart;
			}
		}

        //---------------------------------------------------------------
        public C2iWndPanel WndPanel
        {
            get
            {
                return WndAssociee as C2iWndPanel;
            }
        }
		//---------------------------------------------------------------
		protected override void OnChangeElementEdite(object element)
		{
            if (m_controlChart != null && m_wndChart != null)
            {
                m_controlChart.Init(m_wndChart.ChartSetup, element);
            }
		}

		//---------------------------------------------------------------
		protected override CResultAErreur MyMajChamps(bool bControlerLesValeursAvantValidation)
		{
			return CResultAErreur.True;
		}

		//---------------------------------------------------------------
		protected override void MyUpdateValeursCalculees()
		{
		}

		//---------------------------------------------------------------
        protected override void MyAppliqueRestriction(
            CRestrictionUtilisateurSurType restrictionSurObjetEdite,
            CListeRestrictionsUtilisateurSurType listeRestrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly)
		{
		}

        
	}
}
