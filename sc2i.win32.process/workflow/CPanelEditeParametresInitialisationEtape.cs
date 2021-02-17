using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.process.workflow;
using sc2i.data.dynamic;
using sc2i.common;
using sc2i.expression;

namespace sc2i.win32.process.workflow
{
    public partial class CPanelEditeParametresInitialisationEtape : UserControl, IControlALockEdition
    {
        public CPanelEditeParametresInitialisationEtape()
        {
            InitializeComponent();
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion

        public void Init(CParametresInitialisationEtape parametres)
        {
            m_panelFormules.Init(parametres.Affectations.Formules.ToArray(), typeof(CWorkflow), new CFournisseurPropDynStd());
            m_txtFormuleInitialisation.Init(new CFournisseurPropDynStd(), typeof(CEtapeWorkflow));
            m_txtFormuleInitialisation.Formule = parametres.FormuleInitialisation;
            m_chkRecalculerAffectations.Checked = parametres.RecalculerAffectationsSurRedemarrage;
            m_chkReevaluerFormuleSurRedemarrage.Checked = parametres.ReevaluerInitialisationSurRedemarrage;
        }

        public CResultAErreurType<CParametresInitialisationEtape> MajChamps()
        {
            CParametresInitialisationEtape parametre = new CParametresInitialisationEtape();
            CResultAErreurType<CParametresInitialisationEtape> result = new CResultAErreurType<CParametresInitialisationEtape>();
            foreach (CFormuleNommee formuleNommée in m_panelFormules.GetFormules())
            {
                if (formuleNommée.Formule == null )//|| !typeof(IAffectableAEtape).IsAssignableFrom(formuleNommée.Formule.TypeDonnee.TypeDotNetNatif))
                {
                    result.EmpileErreur(I.T("Formula @1 is invalid (should return an object that can be assigned to a step)|20066",
                        formuleNommée.Libelle));
                }
            }
            C2iExpression formule = m_txtFormuleInitialisation.Formule;
            if (formule == null && !m_txtFormuleInitialisation.ResultAnalyse )
                result.EmpileErreur(m_txtFormuleInitialisation.ResultAnalyse.Erreur);
            if (formule != null)
                parametre.FormuleInitialisation = formule;
            if (result)
            {
                parametre.Affectations.Formules = m_panelFormules.GetFormules();
                result.DataType = parametre;
            }
            parametre.RecalculerAffectationsSurRedemarrage = m_chkRecalculerAffectations.Checked;
            parametre.ReevaluerInitialisationSurRedemarrage = m_chkReevaluerFormuleSurRedemarrage.Checked;  
            return result;
        }

        
    }
}
