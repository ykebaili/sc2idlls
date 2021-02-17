using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
    /// <summary>
    /// Encapsule un CListeObjetDonnee avec accès générique
    /// </summary>
    /// <typeparam name="TypeObjets"></typeparam>
    public class CListeObjetDonneeGenerique<TypeObjets> : CListeObjetsDonnees, IList<TypeObjets>
        where TypeObjets : CObjetDonnee
    {
        private CListeObjetsDonnees m_listeObjetsDonnees = null;

        #region CListeObjetDonneesEnumerator
        public class CListeObjetDonneesEnumerator<TypeObjetsEnum> : IEnumeratorBiSensGenerique<TypeObjetsEnum>
            where TypeObjetsEnum : CObjetDonnee
        {
            private CListeObjetDonneeGenerique<TypeObjetsEnum> m_liste;
            private int m_nIndex = -1;

            /// ////////////////////////////////////////////////////////////
            public CListeObjetDonneesEnumerator(CListeObjetDonneeGenerique<TypeObjetsEnum> liste)
            {
                m_liste = liste;
            }

            #region IEnumeratorBiSensGenerique<TypeObjets> Membres

            /// ////////////////////////////////////////////////////////////
            public int CurrentIndex
            {
                get
                {
                    return m_nIndex;
                }

                set
                {
                    m_nIndex = value;
                }
            }

            /// ////////////////////////////////////////////////////////////
            public bool MoveNext()
            {
                if (m_nIndex < m_liste.Count - 1)
                {
                    m_nIndex++;
                    return true;
                }
                return false;
            }

            /// ////////////////////////////////////////////////////////////
            public bool MovePrev()
            {
                if (m_nIndex > 0)
                {
                    m_nIndex--;
                    return true;
                }
                return false;
            }

            /// ////////////////////////////////////////////////////////////
            public void Reset()
            {
                m_nIndex = -1;
            }

            
            #endregion

            #region IEnumerator<TypeObjetsEnum> Membres

            TypeObjetsEnum IEnumerator<TypeObjetsEnum>.Current
            {
                get { return m_liste[m_nIndex] as TypeObjetsEnum; }
            }

            #endregion

            #region IDisposable Membres

            public void Dispose()
            {
                
            }

            #endregion

            #region IEnumerator Membres

            object System.Collections.IEnumerator.Current
            {
                get { return m_liste[m_nIndex]; }
            }

            

            #endregion

        }
        #endregion

        public CListeObjetDonneeGenerique(CContexteDonnee contexteDonnee)
            : base(contexteDonnee, typeof(TypeObjets))
        {
        }

        public CListeObjetDonneeGenerique(CContexteDonnee contexte, CFiltreData filtrePrincipal)
            : base(contexte, typeof(TypeObjets), filtrePrincipal)
        {
        }

        ////////////////////////////////////////////////////////////
        public CListeObjetDonneeGenerique(CContexteDonnee ctx, bool bAppliquerFiltreParDefaut)
            :base(ctx, typeof(TypeObjets), bAppliquerFiltreParDefaut)
		{
		}


        #region IList<TypeObjets> Membres

        public int IndexOf(TypeObjets item)
        {
            return base.IndexOf(item);
        }

        public void Insert(int index, TypeObjets item)
        {
            base.Insert(index, item);
        }

        public new TypeObjets this[int index]
        {
            get
            {
                return base[index] as TypeObjets;
            }
            set
            {
                base[index] = value;
            }
        }

        #endregion

        #region ICollection<TypeObjets> Membres

        public void Add(TypeObjets item)
        {
            base.Add(item);
        }

        public bool Contains(TypeObjets item)
        {
            return base.Contains(item);
        }

        public void CopyTo(TypeObjets[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public bool Remove(TypeObjets item)
        {
            base.Remove(item);
            return false;
        }

        #endregion

        #region IEnumerable<TypeObjets> Membres

        ////////////////////////////////////////////////////////////
        public IEnumeratorBiSensGenerique<TypeObjets> GetEnumeratorBiSensGeneric()
        {
            return new CListeObjetDonneesEnumerator<TypeObjets>(this);
        }

        ////////////////////////////////////////////////////////////
        IEnumerator<TypeObjets> IEnumerable<TypeObjets>.GetEnumerator()
        {
            return GetEnumeratorBiSensGeneric();
        }

        #endregion
    }
}
