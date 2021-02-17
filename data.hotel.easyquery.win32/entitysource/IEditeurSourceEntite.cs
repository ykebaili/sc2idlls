using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery.win32.entitysource
{
    internal interface IEditeurSourceEntite
    {
        void Init(ISourceEntitesPourTableDataHotel source, CODEQTableFromDataHotel table);
        CResultAErreurType<ISourceEntitesPourTableDataHotel> MajChamps();
    }
}
