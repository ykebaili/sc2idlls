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
    public partial class CControleEditePeriodeDates : UserControl, IEditeurPeriodeActivation
    {
        private CPeriodeActivationDates m_periode = new CPeriodeActivationDates();

        public static void Autoexec()
        {
            CControleEditePeriodeActivation.RegisterEditeurPeriode(typeof(CPeriodeActivationDates), typeof(CControleEditePeriodeDates));
        }

        public CControleEditePeriodeDates()
        {
            InitializeComponent();
        }



        #region IEditeurPeriodeActivation Membres

        public void Init(IPeriodeActivation periode)
        {
            m_periode = periode as CPeriodeActivationDates;
            m_dtDebut.Value =  m_periode.DateDebut;
            m_dtFin.Value= m_periode.DateFin;
        }

        public sc2i.common.CResultAErreur MajChamps()
        {
            CResultAErreur result = CResultAErreur.True;
            m_periode.DateDebut = m_dtDebut.Value;
            m_periode.DateFin = m_dtFin.Value;
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
                if (OnChangeLockEdition != null)
                    OnChangeLockEdition(this, null);
            }
        }

        public event EventHandler OnChangeLockEdition;

        #endregion
    }
}
