using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.memorydb
{
    public class CMemoryDbIndex<T> : Dictionary<string, T>
        where T : CEntiteDeMemoryDbAIdAuto
    {

        public CMemoryDbIndex(CMemoryDb db )
        {
            CListeEntitesDeMemoryDbBase liste = new CListeEntitesDeMemoryDbBase(db, typeof(T));
            foreach (T ett in liste)
            {
                this[ett.Id] = ett;
            }
        }

        public T GetFromId(string strId)
        {
            if (strId == null)
                return null;
            T retour = null;
            TryGetValue(strId, out retour);
            return retour;
        }


    }
}
