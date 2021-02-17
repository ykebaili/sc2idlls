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
using sc2i.process;

namespace sc2i.win32.process.workflow.bloc
{
    public partial class CFormEditionBlocWorkflowProcess : Form
    {
        private CBlocWorkflowProcess m_blocProcess = null;
        private CProcess m_process = new CProcess();
        
        
        //-------------------------------------------------------
        public CFormEditionBlocWorkflowProcess()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //-------------------------------------------------------
        public static bool EditeBloc(CBlocWorkflowProcess bloc)
        {
            if (bloc == null)
                return false;
            CFormEditionBlocWorkflowProcess form = new CFormEditionBlocWorkflowProcess();
            form.m_blocProcess = bloc;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        //-------------------------------------------------------
        private void InitChamps()
        {
            CFiltreData filtre = new CFiltreData ( CProcessInDb.c_champTypeCible +"=@1",
                    typeof(CEtapeWorkflow).ToString() );
            m_txtSelectProcess.Init ( typeof(CProcessInDb),
                filtre,
                "Libelle",
                true);
            if (m_blocProcess.DbKeyProcess != null)
            {
                CProcessInDb process = new CProcessInDb(CContexteDonneeSysteme.GetInstance());
                if (process.ReadIfExists(m_blocProcess.DbKeyProcess))
                    m_txtSelectProcess.ElementSelectionne = process;
            }
            m_process = m_blocProcess.Process;
            m_txtInstructions.Text = m_blocProcess.Instructions;
            m_chkManualStart.Checked = m_blocProcess.DemarrageManuel;
            CParametresInitialisationEtape parametres = m_blocProcess.TypeEtape != null ? m_blocProcess.TypeEtape.ParametresInitialisation : new CParametresInitialisationEtape();
            m_panelAffectations.Init(parametres);
            m_chkUtiliserSortieProcessCommeCodeRetour.Checked = m_blocProcess.UtiliserLaValeurDeSortieDeProcessCommeCodeRetour;

            UpdateVisuProcess();
        }

        //----------------------------------------------------------------
        private void UpdateVisuProcess()
        {
            CProcessInDb prc = m_txtSelectProcess.ElementSelectionne as CProcessInDb;
            if (prc == null)
            {
                if (m_process == null)
                    m_process = new CProcess(m_blocProcess.TypeEtape.ContexteDonnee);
                m_process.TypeCible = typeof(CEtapeWorkflow);
                m_processEditor.Process = m_process;
                if (!m_tab.TabPages.Contains(m_pageProcess))
                {
                    m_tab.TabPages.Insert(0,m_pageProcess);
                }
            }
            else
                if (m_tab.TabPages.Contains(m_pageProcess))
                    m_tab.TabPages.Remove(m_pageProcess);
        }
    
        //-------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            //m_blocFormulaire.Instructions = m_txtInstructions.Text;
            CProcessInDb process = m_txtSelectProcess.ElementSelectionne as CProcessInDb;
            if (process != null)
            {
                m_blocProcess.DbKeyProcess = process.DbKey;

            }
            else
                m_blocProcess.DbKeyProcess = null;
            CResultAErreurType<CParametresInitialisationEtape> resParam = m_panelAffectations.MajChamps();
            if (resParam != null)
            {
                if (m_blocProcess.TypeEtape != null)
                    m_blocProcess.TypeEtape.ParametresInitialisation = resParam.DataType;
            }
            else
            {
                result.EmpileErreur(resParam.Erreur);
            }
            m_blocProcess.DemarrageManuel = m_chkManualStart.Checked;
            m_blocProcess.Instructions = m_txtInstructions.Text;

            if (m_blocProcess.DbKeyProcess == null)
            {
                m_process = m_processEditor.Process;
                m_blocProcess.Process = m_process;
            }
            else
                m_blocProcess.Process = null;
            m_blocProcess.UtiliserLaValeurDeSortieDeProcessCommeCodeRetour = m_chkUtiliserSortieProcessCommeCodeRetour.Checked;

            return result;
        }


        //-------------------------------------------------------
        private void CFormEditionBlocWorkflowProcess_Load(object sender, EventArgs e)
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
        private void m_txtSelectProcess_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateVisuProcess();
        }

    }

    //-------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurBlocWorkflowProcess : IEditeurBlocWorkflow
    {
        public static void Autoexec()
        {
            CAllocateurEditeurBlocWorkflow.RegisterEditeur(typeof(CBlocWorkflowProcess),
                typeof(CEditeurBlocWorkflowProcess));
        }

        //-------------------------------------------------------
        public bool EditeBloc(CBlocWorkflow bloc)
        {
            return CFormEditionBlocWorkflowProcess.EditeBloc ( bloc as CBlocWorkflowProcess);
        }

    }

}
