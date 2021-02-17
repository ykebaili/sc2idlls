using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.process.workflow.blocs;
using sc2i.data.dynamic;
using sc2i.win32.data.navigation;
using sc2i.data;
using sc2i.expression;
using sc2i.common;
using sc2i.process.workflow;
using sc2i.win32.common;
using sc2i.win32.data;

namespace sc2i.win32.process.workflow.bloc
{
    public partial class CFormEditionBlocWorkflowChoix : Form
    {
        private CBlocWorkflowChoix m_blocWorkflow = null;
       
        
        //-------------------------------------------------------
        public CFormEditionBlocWorkflowChoix()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
            m_lblOther.Text = CBlocWorkflowChoix.c_codeRetourAutres;
        }

        //-------------------------------------------------------
        public static bool EditeBloc(CBlocWorkflowChoix bloc)
        {
            if (bloc == null)
                return false;
            CFormEditionBlocWorkflowChoix form = new CFormEditionBlocWorkflowChoix();
            form.m_blocWorkflow = bloc;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        //-------------------------------------------------------
        private void InitChamps()
        {
            m_wndListeFormules.Init(m_blocWorkflow.FormulesCodesRetour.ToArray(), typeof(CEtapeWorkflow), new CFournisseurPropDynStd());
        }

        //-------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            m_blocWorkflow.FormulesCodesRetour = m_wndListeFormules.GetFormules();
            return result;
        }


        
        //-------------------------------------------------------
        private void CFormEditionBlocWorkflowChoix_Load(object sender, EventArgs e)
        {
            if ( !DesignMode)
                InitChamps();
        }

        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

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

        private void m_wndListeFormules_Load(object sender, EventArgs e)
        {

        }

    }

    //-------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurBlocWorkflowChoix : IEditeurBlocWorkflow
    {
        public static void Autoexec()
        {
            CAllocateurEditeurBlocWorkflow.RegisterEditeur(typeof(CBlocWorkflowChoix),
                typeof(CEditeurBlocWorkflowChoix));
        }

        //-------------------------------------------------------
        public bool EditeBloc(CBlocWorkflow bloc)
        {
            return CFormEditionBlocWorkflowChoix.EditeBloc ( bloc as CBlocWorkflowChoix);
        }

    }

}
