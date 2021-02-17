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
using sc2i.win32.expression;
using sc2i.win32.data.dynamic;
using sc2i.win32.data;

namespace sc2i.win32.process.workflow.bloc
{
    public partial class CFormEditionBlocWorkflowAttente : Form
    {
        private CBlocWorkflowAttente m_blocAttente = null;
        
        //-------------------------------------------------------
        public CFormEditionBlocWorkflowAttente()
        {
            InitializeComponent();
            CWin32Traducteur.Translate(this);
        }

        //-------------------------------------------------------
        public static bool EditeBloc(CBlocWorkflowAttente bloc)
        {
            if (bloc == null)
                return false;
            CFormEditionBlocWorkflowAttente form = new CFormEditionBlocWorkflowAttente();
            form.m_blocAttente = bloc;
            bool bResult = form.ShowDialog() == DialogResult.OK;
            form.Dispose();
            return bResult;
        }

        //-------------------------------------------------------
        private void InitChamps()
        {
            m_txtFormuleElementEdite.Init(new CFournisseurPropDynStd(), typeof(CEtapeWorkflow));
            m_txtFormuleElementEdite.Formule = m_blocAttente.FormuleElementDeclencheur;

            m_panelParametreDeclenchement.Init(m_blocAttente.ParametreDeclencheur);
        }

        //-------------------------------------------------------
        private void UpdatePanelParametre()
        {
            if (!DesignMode)
            {
                C2iExpression formule = m_txtFormuleElementEdite.Formule;
                if (formule == null || !typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(
                    formule.TypeDonnee.TypeDotNetNatif))
                    m_panelParametreDeclenchement.Visible = false;
                else
                {
                    m_panelParametreDeclenchement.Visible = true;
                    m_panelParametreDeclenchement.TypeCible = formule.TypeDonnee.TypeDotNetNatif;
                }
            }
        }
            

        //-------------------------------------------------------
        private CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            
            C2iExpression formule = GetFormuleValideElementEdite(m_txtFormuleElementEdite);
            if (formule == null)
            {
                result.EmpileErreur(I.T("target element formula is not valid|20118"));
                return result;
            }
            else
                m_blocAttente.FormuleElementDeclencheur = formule;

            result = m_panelParametreDeclenchement.MAJ_Champs();
            if (!result)
                return result;
            m_blocAttente.ParametreDeclencheur = m_panelParametreDeclenchement.ParametreDeclencheur;

            return result;
        }

        //-------------------------------------------------------
        private void m_txtFormuleElementEdite_Validated(object sender, EventArgs e)
        {
            UpdatePanelParametre();
        }
       
        //-------------------------------------------------------
        private C2iExpression GetFormuleValideElementEdite(CTextBoxZoomFormule editeur)
        {
            C2iExpression formule = editeur.Formule;
            bool bHasErreur = true;
            if (formule != null)
            {
                CTypeResultatExpression typeResultat = formule.TypeDonnee;
                if (typeResultat != null &&
                    !typeResultat.IsArrayOfTypeNatif &&
                    typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(typeResultat.TypeDotNetNatif))
                    bHasErreur = false;
            }
            if (!bHasErreur)
                return formule;
            return null;
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

        private void CFormEditionBlocWorkflowAttente_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
                InitChamps();
        }

    }

    //-------------------------------------------------------
    [AutoExec("Autoexec")]
    public class CEditeurBlocWorkflowAttente : IEditeurBlocWorkflow
    {
        public static void Autoexec()
        {
            CAllocateurEditeurBlocWorkflow.RegisterEditeur(typeof(CBlocWorkflowAttente),
                typeof(CEditeurBlocWorkflowAttente));
        }

        //-------------------------------------------------------
        public bool EditeBloc(CBlocWorkflow bloc)
        {
            return CFormEditionBlocWorkflowAttente.EditeBloc ( bloc as CBlocWorkflowAttente);
        }

    }

}
