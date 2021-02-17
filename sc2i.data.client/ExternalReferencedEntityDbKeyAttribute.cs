using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common.Referencement;

namespace sc2i.data
{
    public class ExternalReferencedEntityDbKeyAttribute : ExternalReferencedEntityAttribute
    {
        public ExternalReferencedEntityDbKeyAttribute(Type tp)
            :base(tp)
        {

        }
    }
}
