using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sc2i.common.periodeactivation;
using sc2i.common;

namespace sc2i.win32.common.periodeactivation
{
    public  interface IEditeurPeriodeActivation : IControlALockEdition
    {
        void Init(IPeriodeActivation periode);
        CResultAErreur MajChamps();
    }
}
