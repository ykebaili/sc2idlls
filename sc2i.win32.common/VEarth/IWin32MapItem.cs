using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using sc2i.common.sig;

namespace sc2i.win32.common.VEarth
{
    public interface IWin32MapItem
    {
        //Attention : les IWin32MapItem doivent avoir un constructeur
        //qui les construit à partir d'un IMapItem

        IMapItem Item { get; }

        void AddToLayer(GMapOverlay layer);

        void DeleteItem();

    }
}
