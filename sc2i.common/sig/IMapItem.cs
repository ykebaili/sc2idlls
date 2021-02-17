using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.common.sig
{
    public delegate void MapItemClickEventHandler ( IMapItem item );
    public interface IMapItem
    {
        object Tag { get; set; }

        CMapLayer Layer { get; }

        string ToolTip { get; set; }
        bool PermanentToolTip { get; set; }

        event MapItemClickEventHandler MouseClicked;

        void OnClick();

    }
}
