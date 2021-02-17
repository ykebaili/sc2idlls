using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data.dynamic.easyquery;
using futurocom.easyquery;

namespace sc2i.data.dynamic.EasyQuery
{
    public interface IODEQTableFromFramework : IObjetDeEasyQuery
    {
        Type TypeElements { get; }
        IEnumerable<CColumnDeEasyQueryChampDeRequete> ChampsDeRequete { get; }

        void AddColonneDeRequete ( CColumnDeEasyQueryChampDeRequete col );
    }
}
