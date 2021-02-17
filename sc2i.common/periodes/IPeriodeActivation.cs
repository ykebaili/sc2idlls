using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.periodeactivation
{
    public interface IPeriodeActivation : I2iSerializable
    {
        string GetLibelleType();
        bool IsInPeriode(DateTime dt);
        string Libelle { get; }
    }

    public interface IPeriodeActivationMultiple : IPeriodeActivation
    {
        void AddPeriode(IPeriodeActivation periode);
        void RemovePeriode(IPeriodeActivation periode);
        IEnumerable<IPeriodeActivation> Periodes{get;}
        void ClearPeriodes();
    }
}
