using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.common
{
    public class CDbKeyReaderTracker : IDisposable
    {
        private HashSet<CDbKey> m_setKeys = new HashSet<CDbKey>();

        //-----------------------------------------------
        public CDbKeyReaderTracker()
        {
            C2iSerializer.m_listeDbKeyTrackers.Add(this);
        }

        //-----------------------------------------------
        public void Dispose()
        {
            C2iSerializer.m_listeDbKeyTrackers.Remove(this);
        }

        //-----------------------------------------------
        public void RegisterDbKey(CDbKey key)
        {
            if (key != null)
                m_setKeys.Add(key);
        }

        //-----------------------------------------------
        public IEnumerable<CDbKey> TrackedKeys
        {
            get
            {
                return m_setKeys.ToArray();
            }
        }

    }
}
