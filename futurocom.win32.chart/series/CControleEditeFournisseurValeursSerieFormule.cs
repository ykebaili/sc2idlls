using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using futurocom.chart;
using sc2i.expression;
using sc2i.common;

namespace futurocom.win32.chart.series
{
    [AutoExec("Autoexec")]
    public partial class CControleEditeFournisseurValeursSerieFormule : UserControl, IEditeurFournisseurValeursSerieDeTypeConnu
    {
        private CFournisseurValeursSerieFormule m_fournisseurFormule = null;
        private CChartSetup m_chartSetup = null;

        public CControleEditeFournisseurValeursSerieFormule()
        {
            InitializeComponent();
            sc2i.win32.common.CWin32Traducteur.Translate(this);
        }

        //---------------------------------------------------------------------------------------
        public static void Autoexec()
        {
            CGestionnaireEditeursFournisseursValeurs.RegisterEditeur(typeof(CFournisseurValeursSerieFormule),
                typeof(CControleEditeFournisseurValeursSerieFormule));
        }


        //---------------------------------------------------------------------------------------
        public void InitChamps(CChartSetup chartSetup, IFournisseurValeursSerie fournisseur)
        {
            m_fournisseurFormule = fournisseur as CFournisseurValeursSerieFormule;
            m_chartSetup = chartSetup;
            //Trouve la source
            CParametreSourceChart p = chartSetup.ParametresDonnees.GetSourceFV(fournisseur.SourceId);
            Type tp = typeof(string);
            if ( p != null )
            {
                CTypeResultatExpression t = p.TypeSource;
                if ( t != null )
                {
                    tp = p.TypeSource.TypeDotNetNatif;
                    /*if ( t.IsArrayOfTypeNatif )
                    {
                        tp = CActivatorSurChaine.GetType(t.TypeDotNetNatif.ToString()+"[]");
                    }*/
                }
            }
            m_txtFormule.Init( new CFournisseurGeneriqueProprietesDynamiques(), tp);
            m_txtFormule.Formule = m_fournisseurFormule.Formule;
            m_chkForEach.Checked = m_fournisseurFormule.EvaluateOnEachSourceElement;
        }

        //--------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            C2iExpression formule = m_txtFormule.Formule;
            if (formule == null)
            {
                result.EmpileErreur(m_txtFormule.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Invalid formula|20011"));
            }
            else
                m_fournisseurFormule.Formule = formule;
            m_fournisseurFormule.EvaluateOnEachSourceElement = m_chkForEach.Checked;
            return result;
        }

    }
}
