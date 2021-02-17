using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
    public interface IObjetDonneeAIdNumeriqueEtUniversel : IObjetDonneeAIdNumerique
    {
        [DynamicField("Universal id")]
        string IdUniversel { get; }

        //-----------------------------------------------------------
        bool ReadIfExistsUniversalId(string strId);
    }
}
