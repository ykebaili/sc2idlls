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
    public partial class CFormEditionBlocWorkflowWorkflow : Form
    {
        private CBlocWorkflowWorkflow m_blocWorkflow = null;
        private CDbKey m_keyLastIdTypeEtapeSel = null;
       
        
        //-------------------------------------------------------
        public CFormEditionBlocWorkflowWorkflow()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //-------------------------------------------------------
        public static bool EditeBloc(CBlocWorkflowWorkflow bloc)
        {
            if (bloc == null)
                return false;
            CFormEditionBlocWorkflowWorkflow form = new CFormEditionBlocWorkflowWorkflow();
            form.m_blocWorkflow = bloc;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        //-------------------------------------------------------
        private void InitChamps()
        {
            m_txtSelectWorkflow.InitAvecFiltreDeBase(typeof(CTypeWorkflow),
                "Libelle",
                null, false);
            if (m_blocWorkflow.DbKeyTypeWorkflow != null)
            {
                CTypeWorkflow wkf = new CTypeWorkflow(CSc2iWin32DataClient.ContexteCourant);
                if (wkf.ReadIfExists(m_blocWorkflow.DbKeyTypeWorkflow))
                    m_txtSelectWorkflow.ElementSelectionne = wkf;
            }
            m_keyLastIdTypeEtapeSel = m_blocWorkflow.DbKeyTypeEtapeDémarrage;
            m_txtWorkflowInit.Init(new CFournisseurGeneriqueProprietesDynamiques(), typeof(CEtapeWorkflow));
            m_txtWorkflowInit.Formule = m_blocWorkflow.FormuleInitialisationWorkflowLance;
            InitComboStartStep();

        }

        //-------------------------------------------------------
        private void InitComboStartStep()
        {
            CTypeWorkflow typeWorkflow = m_txtSelectWorkflow.ElementSelectionne as CTypeWorkflow;
            if (typeWorkflow != null)
            {
                m_cmbStartStep.Init(typeWorkflow.Etapes, "Libelle", true);
                m_cmbStartStep.Enabled = true;
                if (m_keyLastIdTypeEtapeSel != null)
                {
                    CTypeEtapeWorkflow typeEtape = new CTypeEtapeWorkflow(CSc2iWin32DataClient.ContexteCourant);
                    if (typeEtape.ReadIfExists(m_keyLastIdTypeEtapeSel) && typeWorkflow.Equals(typeEtape.Workflow))
                        m_cmbStartStep.ElementSelectionne = typeEtape;
                    else
                        m_cmbStartStep.ElementSelectionne = null;
                }
                else
                    m_cmbStartStep.ElementSelectionne = null;
            }
            else
                m_cmbStartStep.Enabled = false;
        }

        //-------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            CTypeWorkflow wkfType = m_txtSelectWorkflow.ElementSelectionne as CTypeWorkflow;
            if (wkfType != null)
                m_blocWorkflow.DbKeyTypeWorkflow = wkfType.DbKey;
            else
                m_blocWorkflow.DbKeyTypeWorkflow = null;

            CTypeEtapeWorkflow typeEtape = m_cmbStartStep.ElementSelectionne as CTypeEtapeWorkflow;
            if (typeEtape != null)
                m_blocWorkflow.DbKeyTypeEtapeDémarrage = typeEtape.DbKey;
            else
                m_blocWorkflow.DbKeyTypeEtapeDémarrage = null;

            if (m_txtWorkflowInit.Formule == null)
            {
                if (!m_txtWorkflowInit.ResultAnalyse)
                    result.EmpileErreur(m_txtWorkflowInit.ResultAnalyse.Erreur);
            }
            else
                m_blocWorkflow.FormuleInitialisationWorkflowLance = m_txtWorkflowInit.Formule;
            
            
            return result;
        }


        
        //-------------------------------------------------------
        private void CFormEditionBlocWorkflowWorkflow_Load(object sender, EventArgs e)
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

        private void m_txtSelectWorkflow_ElementSelectionneChanged(object sender, EventArgs e)
        {
            InitComboStartStep();
        }

        private void m_cmbStartStep_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CTypeEtapeWorkflow typeEtape = m_cmbStartStep.ElementSelectionne as CTypeEtapeWorkflow;
            if (typeEtape != null)
                m_keyLastIdTypeEtapeSel = typeEtape.DbKey;
        }

    }

    //-------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurBlocWorkflowWorkflow : IEditeurBlocWorkflow
    {
        public static void Autoexec()
        {
            CAllocateurEditeurBlocWorkflow.RegisterEditeur(typeof(CBlocWorkflowWorkflow),
                typeof(CEditeurBlocWorkflowWorkflow));
        }

        //-------------------------------------------------------
        public bool EditeBloc(CBlocWorkflow bloc)
        {
            return CFormEditionBlocWorkflowWorkflow.EditeBloc ( bloc as CBlocWorkflowWorkflow);
        }

    }

}
