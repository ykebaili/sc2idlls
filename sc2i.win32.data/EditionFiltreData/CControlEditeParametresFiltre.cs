using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using sc2i.win32.common;
using sc2i.data;
using System.Collections;

namespace sc2i.win32.data.EditionFiltreData
{
    public partial class CControlEditeParametresFiltre : UserControl, IControlALockEdition
    {
        private bool m_bLockParametre1 = false;
        public CControlEditeParametresFiltre()
        {
            InitializeComponent();
        }

        #region IControlALockEdition Membres

        public bool LockEdition
        {
            get
            {
                return !m_gestionnaireModeEdition.ModeEdition;
            }
            set
            {
                m_gestionnaireModeEdition.ModeEdition = !value;
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, new EventArgs());
            }
        }

        public event EventHandler OnChangeLockEdition;

        public void AffecteToFiltre ( CFiltreData filtre )
        {
            foreach (CControlEditeParametreDeFiltreData ctrl in m_panelControls.Controls)
            {
                while (filtre.Parametres.Count < ctrl.NumParametre)
                {
                    filtre.Parametres.Add("");
                }
                filtre.Parametres[ctrl.NumParametre-1] = ctrl.Valeur;
            }
        }

        //-*----------------------------------
        public bool LockParametre1
        {
            get
            {
                return m_bLockParametre1;
            }
            set
            {
                m_bLockParametre1 = value;
            }
        }

        public CFiltreData Filtre
        {
            set
            {
                m_panelControls.SuspendDrawing();
                foreach (Control ctrl in new ArrayList(m_panelControls.Controls))
                {
                    m_panelControls.Controls.Remove(ctrl);
                    ctrl.Parent = null;
                    ctrl.Visible = false;
                    ctrl.Dispose();
                }
                int nParametre = 1;
                foreach (object parametre in value.Parametres)
                {
                    CControlEditeParametreDeFiltreData ctrl = new CControlEditeParametreDeFiltreData();
                    m_panelControls.Controls.Add(ctrl);
                    ctrl.NumParametre = nParametre++;
                    ctrl.Valeur = parametre;
                    ctrl.Dock = DockStyle.Top;
                    ctrl.BringToFront();
                    m_gestionnaireModeEdition.SetModeEdition(ctrl, TypeModeEdition.EnableSurEdition);
                    ctrl.LockEdition = !m_gestionnaireModeEdition.ModeEdition;
                    ctrl.OnDelete += new EventHandler(ctrl_OnDelete);
                    if (ctrl.NumParametre == 1 && m_bLockParametre1)
                    {
                        ctrl.LockEdition = true;
                        m_gestionnaireModeEdition.SetModeEdition(ctrl, TypeModeEdition.Autonome);
                    }
                }
                m_panelControls.ResumeDrawing();
            }
       }

        void ctrl_OnDelete(object sender, EventArgs e)
        {
            m_panelControls.SuspendDrawing();
            CControlEditeParametreDeFiltreData ctrl = sender as CControlEditeParametreDeFiltreData;
            if (ctrl != null)
            {
                int nParametre = ctrl.NumParametre;
                m_panelControls.Controls.Remove(ctrl);
                ctrl.Visible = false;
                ctrl.Parent = null;
                ctrl.Dispose();
                foreach (CControlEditeParametreDeFiltreData ctrlTmp in m_panelControls.Controls)
                {
                    if (ctrlTmp.NumParametre >= nParametre)
                        ctrlTmp.NumParametre = ctrlTmp.NumParametre - 1;
                }
            }
            m_panelControls.ResumeDrawing();
        }

        #endregion

        private void m_btnAdd_LinkClicked(object sender, EventArgs e)
        {
            m_panelControls.SuspendDrawing();
            CControlEditeParametreDeFiltreData ctrl = new CControlEditeParametreDeFiltreData();
            ctrl.NumParametre = m_panelControls.Controls.Count + 1;
            m_panelControls.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Top;
            ctrl.BringToFront();
            ctrl.Valeur = "";
            ctrl.OnDelete += new EventHandler(ctrl_OnDelete);
            m_panelControls.ResumeDrawing();
        }
    }
}
