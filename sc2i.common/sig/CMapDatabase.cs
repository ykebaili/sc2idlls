using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace sc2i.common.sig
{
    public class CMapDatabase : IDisposable
    {
        public const string c_defaultLayerName = "DEFAULT";

        private Dictionary<string, Bitmap> m_dicImages = new Dictionary<string, Bitmap>();

        private Dictionary<string, CMapLayer> m_dicLayers = new Dictionary<string,CMapLayer>();

        //-----------------------------------------------------
        public CMapDatabase()
        {
        }

        //-----------------------------------------------------
        public virtual void Dispose()
        {
            foreach ( Bitmap img in m_dicImages.Values )
            {
                img.Dispose();
            }
            m_dicImages.Clear();
        }

        //---------------------------------------------------------
        public void ClearLayers ( )
        {
            m_dicLayers.Clear();
        }

        //---------------------------------------------------------
        public CMapLayer DefaultLayer
        {
            get
            {
                return GetLayer(c_defaultLayerName, true);
            }
        }

        //---------------------------------------------------------
        public CMapLayer GetLayer ( string strLayer )
        {
            return GetLayer ( strLayer, false );
        }

        //---------------------------------------------------------
        public CMapLayer GetLayer ( string strLayer, bool bCreate )
        {
            CMapLayer layer = null;
            m_dicLayers.TryGetValue ( strLayer.ToUpper(), out layer );
            if (bCreate && layer == null )
            {
                layer = new CMapLayer(this, strLayer.ToUpper());
                m_dicLayers[layer.LayerName] = layer;
            }
            return layer;
        }

        //---------------------------------------------------------
        public void RemoveLayer ( string strLayer )
        {
            if ( m_dicLayers.ContainsKey ( strLayer.ToUpper() ))
                m_dicLayers.Remove ( strLayer.ToUpper( ) );
        }

        //---------------------------------------------------------
        public IEnumerable<CMapLayer> Layers
        {
            get
            {
                List<CMapLayer> lst = new List<CMapLayer>(m_dicLayers.Values);
                lst.Sort((x, y) => x.ZOrder.CompareTo(y.ZOrder));
                return lst.AsReadOnly();
            }
        }

        //---------------------------------------------------------
        public void AddImage(string strId, Image img)
        {
            Bitmap old = null;
            m_dicImages.TryGetValue(strId, out old);
            if (old != null)
                old.Dispose();
            m_dicImages[strId] = new Bitmap(img);
        }

        //---------------------------------------------------------
        public Bitmap GetImage(string strId)
        {
            Bitmap img = null;
            m_dicImages.TryGetValue(strId, out img);
            return img;
        }


        //---------------------------------------------------------
        public IMapItem FindItemFromTag ( object tag )
        {
            if (tag == null)
                return null;
            foreach ( CMapLayer layer in Layers )
            {
                foreach (IMapItem item in layer.Items )
                {
                    if (item.Tag != null && (tag.Equals(item.Tag) || tag == item.Tag))
                        return item;
                }
            }
            return null;
        }
            

        
    }
}
