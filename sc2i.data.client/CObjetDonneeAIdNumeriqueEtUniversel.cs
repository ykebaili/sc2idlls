using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace sc2i.data
{
    [Unique(true, "Universal ID should be unique", c_champIdUniversel)]
    public abstract class CObjetDonneeAIdNumeriqueEtUniversel : CObjetDonneeAIdNumeriqueAuto, IObjetDonneeAIdNumeriqueEtUniversel
    {
        public const string c_champIdUniversel = "UNIVERSAL_ID";

        //-----------------------------------------------------------
        public CObjetDonneeAIdNumeriqueEtUniversel(CContexteDonnee ctx)
            : base(ctx)
        {
        }

        //-----------------------------------------------------------
        public CObjetDonneeAIdNumeriqueEtUniversel(DataRow row)
            : base(row)
        {
        }

        //-----------------------------------------------------------
        protected override void  InitValeurDefaut()
        {
            base.InitValeurDefaut();
            IdUniversel = CUniqueIdentifier.GetNew();
 	        
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Id unique universel quelque soit la machine
        /// </summary>
        [TableFieldProperty(c_champIdUniversel, 64)]
        [DynamicField("Universal id")]
        [IndexField]
        [NonCloneable]
        public string IdUniversel
        {
            get
            {
                return (string)Row[c_champIdUniversel];
            }
            set
            {
                Row[c_champIdUniversel] = value;
            }
        }

        //-----------------------------------------------------------
        public bool ReadIfExistsUniversalId(string strId)
        {
            CFiltreData filtre = new CFiltreData(c_champIdUniversel + "=@1",
                strId);
            return ReadIfExists(filtre);
        }

    }
}
