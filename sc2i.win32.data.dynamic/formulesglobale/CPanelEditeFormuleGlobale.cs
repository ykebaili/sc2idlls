using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.win32.common;
using sc2i.data;

namespace sc2i.win32.data.dynamic.formulesglobale
{
    public partial class CPanelEditeFormuleGlobale : UserControl, IControlALockEdition
    {
        private CDefinitionFormuleGlobaleParametrage m_definitionFormule = null;
        
        
        public CPanelEditeFormuleGlobale()
        {
            InitializeComponent();
        }

        //-------------------------------------------------------------------------
        public void Init(CDefinitionFormuleGlobaleParametrage defFormule)
        {
            m_definitionFormule = defFormule;
            m_lblLibelle.Text = defFormule.Libelle;
            m_txtFormule.Init(new CFournisseurPropDynStd(), defFormule.TypeSource);
            m_txtFormule.Formule = CFormulesGlobaleParametrage.GetFormule(CSessionClient.GetSessionUnique().IdSession, defFormule.Key);
        }

        //-------------------------------------------------------------------------
        public CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            C2iExpression formule = m_txtFormule.Formule;
            if (formule == null && !m_txtFormule.ResultAnalyse)
            {
                result.EmpileErreur(m_txtFormule.ResultAnalyse.Erreur);
                result.EmpileErreur(I.T("Error on @1 formula|20061"), m_definitionFormule.Libelle);
                return result;
            }
            CFormulesGlobaleParametrage.SetFormule(CSessionClient.GetSessionUnique().IdSession, m_definitionFormule.Key, formule);
            return result;
        }

        //-------------------------------------------------------------------------
        



        //--------------------------------------------------
        public bool LockEdition
        {
            get
            {
                return !m_extModeEdition.ModeEdition;
            }
            set
            {
                m_extModeEdition.ModeEdition = !value;
                if ( OnChangeLockEdition != null )
                    OnChangeLockEdition ( this, null );
            }
        }

        //--------------------------------------------------
        public event EventHandler OnChangeLockEdition;
    }
}
