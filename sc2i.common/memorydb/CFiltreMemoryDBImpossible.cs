using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.memorydb
{
    [Serializable]
    public class CFiltreMemoryDBImpossible : CFiltreMemoryDb
    {
        //---------------------------------------
        public override string Filtre
        {
            get
            {
                return "1=0";
            }
            set
            {
            }
        }

        public override bool HasFiltre
        {
            get
            {
                return true;
            }
        }

        //////////////////////////////////////////////////
        public override CFiltreMemoryDb GetClone()
        {
            return new CFiltreMemoryDBImpossible();
        }
    }
}
