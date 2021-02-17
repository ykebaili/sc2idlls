using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.win32.common.customizableList
{
    public class CCustomizableListDragDropData
    {
        public readonly CCustomizableList ListControl;
        public readonly int DraggedIndex;
        public readonly Point InitialPoint;

        public CCustomizableListDragDropData(CCustomizableList listControl,
            int nIndex,
            Point initialPoint)
        {
            ListControl = listControl;
            DraggedIndex = nIndex;
            InitialPoint = initialPoint;
        }
    }
}
