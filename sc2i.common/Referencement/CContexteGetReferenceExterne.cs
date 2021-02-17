using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace sc2i.common.Referencement
{
    public class CContexteGetReferenceExterne
    {
        public CContexteGetReferenceExterne()
        {

        }

        //Pile d'objets attachés par type d'objets
        private Hashtable m_tableElementsAttaches = new Hashtable();

        /// //////////////////////////////////////////////
        public object GetObjetAttache(Type type)
        {
            Stack pile = (Stack)m_tableElementsAttaches[type];
            if (pile != null && pile.Count > 0)
                return pile.Peek();
            return null;
        }

        /// //////////////////////////////////////////////
        public void AttacheObjet(Type type, object obj)
        {
            Stack pile = (Stack)m_tableElementsAttaches[type];
            if (pile == null)
            {
                pile = new Stack();
                m_tableElementsAttaches[type] = pile;
            }
            pile.Push(obj);
        }

        /// //////////////////////////////////////////////
        public void DetacheObjet(Type type, object obj)
        {
            Stack pile = (Stack)m_tableElementsAttaches[type];
            if (pile != null)
                pile.Pop();
        }

    }
}
