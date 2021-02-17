using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.data
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RelationsToReadAlwaysAttribute : Attribute
    {
        private List<string> m_listeRelationsALire = new List<string>();

        public RelationsToReadAlwaysAttribute(params string[] strRelations)
        {
            m_listeRelationsALire.AddRange(strRelations);
        }

        public IEnumerable<string> RelationsToRead
        {
            get
            {
                return m_listeRelationsALire.ToArray();
            }
        }
    }
}
