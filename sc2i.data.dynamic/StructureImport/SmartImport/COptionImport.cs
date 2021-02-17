using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    public enum EOptionImport
    {
        OnUpdateAndCreate = 0,
        OnCreate,
        Never
    }

    [Serializable]
    public class COptionImport : CEnumALibelle<EOptionImport>
    {
        public COptionImport(EOptionImport option)
            : base(option)
        {
        }



        public override string Libelle
        {
            get {
                switch (Code)
                {
                    case EOptionImport.OnUpdateAndCreate:
                        return I.T("Import on update and on create|20099");
                    case EOptionImport.OnCreate:
                        return I.T("Import only on create|20100");
                    case EOptionImport.Never:
                        return I.T("Never import|20101");
                }
                return "?";
            }
        }
    }
}
