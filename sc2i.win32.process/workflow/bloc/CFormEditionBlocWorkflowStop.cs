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
    public partial class CFormEditionBlocWorkflowStopStep : Form
    {
        private CBlocWorkflowStopStep m_blocWorkflow = null;
       
        
        //-------------------------------------------------------
        public CFormEditionBlocWorkflowStopStep()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //-------------------------------------------------------
        public static bool EditeBloc(CBlocWorkflowStopStep bloc)
        {
            if (bloc == null)
                return false;
            CFormEditionBlocWorkflowStopStep form = new CFormEditionBlocWorkflowStopStep();
            form.m_blocWorkflow = bloc;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        //-------------------------------------------------------
        private void InitChamps()
        {
            //m_wndListeFormules.Init(m_blocWorkflow.FormulesCodesRetour.ToArray(), typeof(CEtapeWorkflow), new CFournisseurPropDynStd());
            
            m_cmbTypeEtape.Init(m_blocWorkflow.TypeEtape.Workflow.Etapes, "Libelle", false);

            CListeObjetsDonnees lstEtapes = m_blocWorkflow.TypeEtape.Workflow.Etapes;
            lstEtapes.Filtre = new CFiltreData(CObjetDonnee.c_champIdUniversel + "=@1",
                m_blocWorkflow.CleStepToStop);
            if (lstEtapes.Count > 0)
                m_cmbTypeEtape.ElementSelectionne = lstEtapes[0] as CTypeEtapeWorkflow;
            m_rbtnAnnuler.Checked = m_blocWorkflow.TypeAction == ETypeActionExterneOnWorkflowStep.Cancel;
            m_rbtnTerminer.Checked = m_blocWorkflow.TypeAction == ETypeActionExterneOnWorkflowStep.End;
        }

        //-------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            CTypeEtapeWorkflow tp = m_cmbTypeEtape.ElementSelectionne as CTypeEtapeWorkflow;
            if (tp != null)
                m_blocWorkflow.CleStepToStop = tp.IdUniversel;
            else
                m_blocWorkflow.CleStepToStop = "";
            m_blocWorkflow.TypeAction = m_rbtnTerminer.Checked ? ETypeActionExterneOnWorkflowStep.End : ETypeActionExterneOnWorkflowStep.Cancel;
            return result;
        }


        
        //-------------------------------------------------------
        private void CFormEditionBlocWorkflowStopStep_Load(object sender, EventArgs e)
        {
            if ( !DesignMode)
                InitChamps();
        }

        //-------------------------------------------------------
        private void m_btnAnnuler_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //-------------------------------------------------------
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

        //-------------------------------------------------------
        private void m_wndListeFormules_Load(object sender, EventArgs e)
        {

        }

        private void c2iPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }

    //-------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurBlocWorkflowStopStep : IEditeurBlocWorkflow
    {
        public static void Autoexec()
        {
            CAllocateurEditeurBlocWorkflow.RegisterEditeur(typeof(CBlocWorkflowStopStep),
                typeof(CEditeurBlocWorkflowStopStep));
        }

        //-------------------------------------------------------
        public bool EditeBloc(CBlocWorkflow bloc)
        {
            return CFormEditionBlocWorkflowStopStep.EditeBloc (bloc as CBlocWorkflowStopStep);
        }

    }

}
