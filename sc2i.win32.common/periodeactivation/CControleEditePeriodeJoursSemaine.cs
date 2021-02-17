using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common;
using sc2i.common.periodeactivation;

namespace sc2i.win32.common.periodeactivation
{
    [AutoExec("Autoexec")]
    public partial class CControleEditePeriodeJoursSemaine : UserControl, IEditeurPeriodeActivation
    {
        private CPeriodeActivationDansSemaine m_periode = new CPeriodeActivationDansSemaine();

        public static void Autoexec()
        {
            CControleEditePeriodeActivation.RegisterEditeurPeriode(typeof(CPeriodeActivationDansSemaine), typeof(CControleEditePeriodeJoursSemaine));
        }

        //----------------------------------------------
        public CControleEditePeriodeJoursSemaine()
        {
            InitializeComponent();
            foreach (Control ctrl in Controls)
            {
                CheckBox chk = ctrl as CheckBox;
                if (chk != null)
                {
                    try
                    {
                        int nTag = Int32.Parse(chk.Tag.ToString());
                        JoursBinaires j = (JoursBinaires)nTag;
                        DayOfWeek d = CUtilDate.GetDayOfWeekFor(j);
                        chk.Text = CUtilDate.GetNomJour(d, false);
                    }
                    catch { }
                }
            }
        }

        //----------------------------------------------
        public void Init(IPeriodeActivation periode)
        {
            m_periode = periode as CPeriodeActivationDansSemaine;
            foreach ( Control ctrl in Controls )
            {
                CheckBox chk = ctrl as CheckBox;
                if ( chk != null )
                {
                    try{
                        JoursBinaires j = (JoursBinaires)Int32.Parse ( chk.Tag.ToString() );
                            chk.Checked = ((m_periode.JoursActivation) & j)==j;
                    }
                    catch{}
                }
            }
        }

        //----------------------------------------------
        public CResultAErreur MajChamps()
        {
            JoursBinaires jFinal = JoursBinaires.Aucun;
            foreach ( Control ctrl in Controls )
            {
                CheckBox chk = ctrl as CheckBox;
                if ( chk != null && chk.Checked)
                {
                    try{
                           JoursBinaires j = (JoursBinaires)Int32.Parse(chk.Tag.ToString());
                            jFinal |= j;

}
                    catch{}
                }
            }
            m_periode.JoursActivation = jFinal;
            return CResultAErreur.True;
        }

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

    }
}
