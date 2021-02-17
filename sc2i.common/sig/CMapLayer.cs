using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.sig
{
    public class CMapLayer
    {
        private string m_strLayerName = "";
        private List<IMapItem> m_listeItems = new List<IMapItem>();
        private int m_nZOrder = 0;

        private bool m_bIsVisible = true;

        private CMapDatabase m_database = null;

        //----------------------------------------------
        public CMapLayer(CMapDatabase database)
        {
            m_database = database;
        }

        //----------------------------------------------
        public CMapLayer(CMapDatabase database, string strName)
            :this(database)
        {
            m_strLayerName = strName;
        }

        //----------------------------------------------
        public int ZOrder
        {
            get
            {
                return m_nZOrder;
            }
            set
            {
                m_nZOrder = value;
            }
        }

        //----------------------------------------------
        public CMapDatabase Database
        {
            get
            {
                return m_database;
            }
        }

        //----------------------------------------------
        public bool IsVisible
        {
            get
            {
                return m_bIsVisible;
            }
            set
            {
                m_bIsVisible = value;
            }
        }


        //----------------------------------------------
        public string LayerName
        {
            get
            {
                return m_strLayerName;
            }
            set
            {
                m_strLayerName = value;
            }
        }

        //----------------------------------------------
        public void AddItem(IMapItem item)
        {
            if ( !m_listeItems.Contains(item))
                m_listeItems.Add(item);
        }

        //----------------------------------------------
        public void RemoveItem(IMapItem item)
        {
            m_listeItems.Remove(item);
        }

        //----------------------------------------------
        public void ClearItems()
        {
            m_listeItems.Clear();
        }

        //----------------------------------------------
        public IEnumerable<IMapItem> Items
        {
            get
            {
                return m_listeItems;
            }
        }

    }
}
