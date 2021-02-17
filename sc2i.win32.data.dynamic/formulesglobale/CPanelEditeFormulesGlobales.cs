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
using sc2i.win32.common;
using sc2i.common;
using sc2i.data;

namespace sc2i.win32.data.dynamic.formulesglobale
{
    public partial class CPanelEditeFormulesGlobales : UserControl, IControlALockEdition
    {
        //--------------------------------------------------
        public CPanelEditeFormulesGlobales()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                m_panelControls.SuspendDrawing();
                foreach (CDefinitionFormuleGlobaleParametrage def in CFormulesGlobaleParametrage.GetDefinitionsFormules())
                {
                    CPanelEditeFormuleGlobale panel = new CPanelEditeFormuleGlobale();
                    m_panelControls.Controls.Add(panel);
                    panel.Dock = DockStyle.Top;
                    panel.LockEdition = !m_extModeEdition.ModeEdition;
                    m_extModeEdition.SetModeEdition(panel, TypeModeEdition.EnableSurEdition);
                    panel.Init(def);
                }
                m_panelControls.ResumeDrawing();
            }
        }

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
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        //--------------------------------------------------
        public event EventHandler OnChangeLockEdition;

        //--------------------------------------------------
        public CResultAErreur MajChammps()
        {
            CResultAErreur result = CResultAErreur.True;
            foreach (CPanelEditeFormuleGlobale panel in m_panelControls.Controls)
            {
                result = panel.MajChamps();
                if (!result)
                    return result;
            }
            return result;
        }
    }
}
