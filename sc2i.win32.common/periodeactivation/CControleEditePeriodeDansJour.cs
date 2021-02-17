using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common.periodeactivation;
using sc2i.common;

namespace sc2i.win32.common.periodeactivation
{
    [AutoExec("Autoexec")]
    public partial class CControleEditePeriodeDansJour : UserControl, IEditeurPeriodeActivation
    {
        private CPeriodeActivationDansJour m_periode = new CPeriodeActivationDansJour();

        public static void Autoexec()
        {
            CControleEditePeriodeActivation.RegisterEditeurPeriode(typeof(CPeriodeActivationDansJour), typeof(CControleEditePeriodeDansJour));
        }

        public CControleEditePeriodeDansJour()
        {
            InitializeComponent();
        }



        #region IEditeurPeriodeActivation Membres

        public void Init(IPeriodeActivation periode)
        {
            m_periode = periode as CPeriodeActivationDansJour;
            m_wndDebut.ValeurHeure = m_periode.HeureDebut;
            m_wndFin.ValeurHeure = m_periode.HeureFin;
        }

        public sc2i.common.CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_wndDebut.ValeurHeure != null)
                m_periode.HeureDebut = m_wndDebut.ValeurHeure.Value; ;
            if (m_wndFin.ValeurHeure != null)
                m_periode.HeureFin = m_wndFin.ValeurHeure.Value;
            return result;
        }

        #endregion

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
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
