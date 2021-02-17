using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace sc2i.common.memorydb
{
    public class CListeEntitesDeMemoryDbBase: IEnumerable, IList
    {
        private Type m_typeElements = null;
        private CMemoryDb m_database = null;
        private CFiltreMemoryDb m_filtrePrincipal = null;
        private CFiltreMemoryDb m_filtre = null;
        private string m_strSortOrder = "";

        private DataView m_view = null;
        
        //----------------------------------------------------------------
        public CListeEntitesDeMemoryDbBase( CMemoryDb db, Type typeElements )
        {
            m_database = db;
            m_typeElements = typeElements;
            CEntiteDeMemoryDb entite = Activator.CreateInstance(typeElements, new object[] { db }) as CEntiteDeMemoryDb;
            m_strSortOrder = entite.GetChampTriParDefaut();
        }

        //----------------------------------------------------------------
        public CListeEntitesDeMemoryDbBase(CMemoryDb db, Type typeElements, CFiltreMemoryDb filtrePrincipal)
            :this(db, typeElements)
        {
            m_filtrePrincipal = filtrePrincipal;
        }

        //----------------------------------------------------------------
        public CFiltreMemoryDb Filtre
        {
            get
            {
                return m_filtre;
            }
            set
            {
                m_view = null;
                m_filtre = value;
            }
        }

        //----------------------------------------------------------------
        public string Sort
        {
            get
            {
                return m_strSortOrder;
            }
            set
            {
                m_strSortOrder = value;
                if (m_view != null)
                    m_view.Sort = m_strSortOrder;
            }
        }

        //----------------------------------------------------------------
        public Type TypeElements
        {
            get
            {
                return m_typeElements;
            }
        }

        //----------------------------------------------------------------
        protected DataView View
        {
            get
            {
                if (m_view != null)
                    return m_view;
                m_view = new DataView(m_database.GetTable(m_typeElements));
                CFiltreMemoryDb filtre = null;
                if ( m_filtre != null || m_filtrePrincipal != null )
                    filtre = CFiltreMemoryDb.GetAndFiltre ( m_filtrePrincipal, m_filtre );
                if (filtre != null && filtre.HasFiltre)
                    m_view.RowFilter = filtre.GetFiltreDataTable();
                if (m_strSortOrder != null)
                    m_view.Sort = m_strSortOrder;
                return m_view;
            }
        }

        //----------------------------------------------------------------
        public int Count()
        {
            return View.Count;
        }

        //----------------------------------------------------------------
        public void Refresh()
        {
            m_view = null;
        }

        //----------------------------------------------------------------
        public class CEnumeratorViewEntiteNG : IEnumerator
        {
            private DataView m_view = null;
            private int m_nIndex = -1;
            private Type m_typeEntite = null;

            //----------------------------------------------------------------
            public CEnumeratorViewEntiteNG(DataView view, Type typeEntite)
            {
                m_view = view;
                m_typeEntite = typeEntite;
            }

            //----------------------------------------------------------------
            public CEntiteDeMemoryDb Current
            {
                get
                {
                    if (m_nIndex >= 0 && m_nIndex < m_view.Count)
                        return Activator.CreateInstance(m_typeEntite, new object[] { m_view[m_nIndex].Row }) as CEntiteDeMemoryDb;
                    return null;
                }
            }

            //----------------------------------------------------------------
            public void Dispose()
            {

            }

            //----------------------------------------------------------------
            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            //----------------------------------------------------------------
            public bool MoveNext()
            {
                if (m_nIndex < m_view.Count - 1)
                {
                    m_nIndex++;
                    return true;
                }
                return false;
            }

            //----------------------------------------------------------------
            public void Reset()
            {
                m_nIndex = 0;
            }

        }

        //----------------------------------------------------------------
        public IEnumerator GetEnumerator()
        {
            return new CEnumeratorViewEntiteNG ( View, m_typeElements );
        }

        #region IList Membres

        public int Add(object value)
        {
            throw new NotSupportedException("Not supported function");
        }

        public void Clear()
        {
            throw new NotSupportedException("Not supported function");
        }

        public bool Contains(object value)
        {
            return IndexOf(value) >= 0;
        }

        public int IndexOf(object value)
        {
            int nVal = 0;
            foreach (CEntiteDeMemoryDb objet in this)
            {
                if (objet.Equals(value))
                    return nVal;
                nVal++;
            }
            return -1;
        }

        public void Insert(int index, object value)
        {
            throw new NotSupportedException("Not supported function");
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(object value)
        {
            throw new NotSupportedException("Not supported function");
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException("Not supported function");
        }

        public object this[int index]
        {
            get
            {
                if (m_view != null)
                {
                    try
                    {
                        object entite = Activator.CreateInstance(m_typeElements, new object[] { m_view[index].Row });
                        return entite;
                    }
                    catch
                    {
                        return null;
                    }
                }
                return null;
            }
            set
            {
                throw new NotSupportedException("Not supported function");
            }
        }

        #endregion

        #region ICollection Membres

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        int ICollection.Count
        {
            get 
            {
                return Count();    
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return null; }
        }

        #endregion
    }

    //----------------------------------------------------------------
    public class CListeEntitesDeMemoryDb<T> : CListeEntitesDeMemoryDbBase, IEnumerable<T>
        where T : CEntiteDeMemoryDb
    {
        

        //----------------------------------------------------------------
        public CListeEntitesDeMemoryDb(CMemoryDb db)
            :base(db, typeof(T))
        {
        }

        //----------------------------------------------------------------
        public CListeEntitesDeMemoryDb(CMemoryDb db, CFiltreMemoryDb filtrePrincipal)
            : base(db, typeof(T), filtrePrincipal )
        {
        }

        

        //----------------------------------------------------------------
        public class CEnumeratorViewEntite<TypeEnum> : IEnumerator<TypeEnum>
            where TypeEnum : CEntiteDeMemoryDb
        {
            private DataView m_view = null;
            private int m_nIndex = -1;

            //----------------------------------------------------------------
            public CEnumeratorViewEntite(DataView view)
            {
                m_view = view;
            }

            //----------------------------------------------------------------
            public TypeEnum Current
            {
                get
                {
                    if (m_nIndex >= 0 && m_nIndex < m_view.Count)
                        return Activator.CreateInstance(typeof(TypeEnum), new object[] { m_view[m_nIndex].Row }) as TypeEnum;
                    return null;
                }
            }

            //----------------------------------------------------------------
            public void Dispose()
            {

            }

            //----------------------------------------------------------------
            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            //----------------------------------------------------------------
            public bool MoveNext()
            {
                if (m_nIndex < m_view.Count - 1)
                {
                    m_nIndex++;
                    return true;
                }
                return false;
            }

            //----------------------------------------------------------------
            public void Reset()
            {
                m_nIndex = 0;
            }

        }

        //----------------------------------------------------------------
        public IEnumerator<T> GetEnumerator()
        {
            return new CEnumeratorViewEntite<T>(View);
        }

        //----------------------------------------------------------------
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new CEnumeratorViewEntite<T>(View);
        }

    }
}
