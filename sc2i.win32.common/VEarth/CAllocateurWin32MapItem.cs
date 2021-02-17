using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common.sig;

namespace sc2i.win32.common.VEarth
{

    public static class CAllocateurWin32MapItem
    {
        private static Dictionary<Type, Type> m_typeMapItemToMarker = new Dictionary<Type, Type>();

        //-------------------------------------------------------------------------
        public static void RegisterMarker(Type typeMapItem, Type typeMarker)
        {
            m_typeMapItemToMarker[typeMapItem] = typeMarker;
        }

        //--------------------------------------------------------------------
        public static IWin32MapItem GetNewMapItem(IMapItem item)
        {
            if (item == null)
                return null;
            Type tp = null;
            if (m_typeMapItemToMarker.TryGetValue(item.GetType(), out tp))
            {
                try
                {
                    IWin32MapItem w32Item = (IWin32MapItem)Activator.CreateInstance(tp, new object[] { item });
                    return w32Item;
                }
                catch (Exception e)
                {
                }
            }
            return null;
        }

    }
}
