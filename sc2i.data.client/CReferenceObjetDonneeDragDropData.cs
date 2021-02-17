using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sc2i.data
{
    public class CReferenceObjetDonneeDragDropData : IDataObject
    {
        private CReferenceObjetDonnee[] m_objets = null;

        //------------------------------------------------------
        public CReferenceObjetDonneeDragDropData(CReferenceObjetDonnee reference)
        {
            if (reference != null)
                m_objets = new CReferenceObjetDonnee[] { reference };
            else
                m_objets = null;
        }

        //------------------------------------------------------
        public CReferenceObjetDonneeDragDropData(CReferenceObjetDonnee[] references)
        {
            if (references != null)
                m_objets = references;
            else
                m_objets = null;
        }

        //------------------------------------------------------
        public CReferenceObjetDonneeDragDropData(CObjetDonnee objet)
        {
            if (objet != null)
                m_objets = new CReferenceObjetDonnee[] { new CReferenceObjetDonnee(objet) };
            else
                m_objets = null;
        }

        //------------------------------------------------------
        public CReferenceObjetDonneeDragDropData(CObjetDonnee[] objets)
        {
            if (objets != null)
            {
                List<CReferenceObjetDonnee> lst = new List<CReferenceObjetDonnee>();
                foreach (CObjetDonnee obj in objets)
                    lst.Add(new CReferenceObjetDonnee(obj));
                m_objets = lst.ToArray();
            }
            else
                m_objets = null;
        }

        //------------------------------------------------------
        public object GetData(Type format)
        {
            if (format == typeof(CReferenceObjetDonnee) && m_objets != null && m_objets.Length > 0)
                return m_objets[m_objets.Length - 1];
            if (format == typeof(CReferenceObjetDonnee[]) && m_objets != null)
                return m_objets;
            return null;
        }

        //------------------------------------------------------
        public object GetData(string format)
        {
            if (format == typeof(CReferenceObjetDonnee).ToString())
                return GetData(typeof(CReferenceObjetDonnee));
            if ( format == typeof(CReferenceObjetDonnee[]).ToString() )
                return GetData ( typeof(CReferenceObjetDonnee[] ));
            return null;
        }

        //------------------------------------------------------
        public object GetData(string format, bool autoConvert)
        {
            return GetData(format);
        }

        //------------------------------------------------------
        public bool GetDataPresent(Type format)
        {
            if ( format == typeof(CReferenceObjetDonnee) || format == typeof(CReferenceObjetDonnee[]) && 
                m_objets != null && m_objets.Length > 0 )
                return true;
            return false;
        }

        //------------------------------------------------------
        public bool GetDataPresent(string format)
        {
            if (format == typeof(CReferenceObjetDonnee).ToString())
                return GetDataPresent(typeof(CReferenceObjetDonnee));
            if (format == typeof(CReferenceObjetDonnee[]).ToString())
                return GetDataPresent(typeof(CReferenceObjetDonnee[]));
            return false;
        }

        //------------------------------------------------------
        public bool GetDataPresent(string format, bool autoConvert)
        {
            return GetDataPresent(format);
        }

        //------------------------------------------------------
        public string[] GetFormats()
        {
            if (m_objets != null && m_objets.Length > 0)
            {
                return new string[]{
                    typeof(CReferenceObjetDonnee).ToString(),
                    typeof(CReferenceObjetDonnee[]).ToString()
                };
            }
            return new string[0];
        }

        //------------------------------------------------------
        public string[] GetFormats(bool autoConvert)
        {
            return GetFormats();
        }

        //------------------------------------------------------
        public void SetData(object data)
        {
            if (data is CReferenceObjetDonnee)
                m_objets = new CReferenceObjetDonnee[] { (CReferenceObjetDonnee)data };
            if (data is CReferenceObjetDonnee[])
                m_objets = data as CReferenceObjetDonnee[];
        }

        //------------------------------------------------------
        public void SetData(Type format, object data)
        {
            SetData(data);
        }

        //------------------------------------------------------
        public void SetData(string format, object data)
        {
        }

        //------------------------------------------------------
        public void SetData(string format, bool autoConvert, object data)
        {
            throw new NotImplementedException();
        }

    }
}
