using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sc2i.common;
using System.Drawing;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    public enum EOptionCreationElementImport
    {
        None = 0,
        Automatic,
        Confirm
    }
    public class COptionCreationElementImport : CEnumALibelle<EOptionCreationElementImport>
    {
        public COptionCreationElementImport(EOptionCreationElementImport option)
            : base(option)
        {
        }

        public override string Libelle
        {
            get {
                switch (Code)
                {
                    case EOptionCreationElementImport.None:
                        return I.T("Never create|20084");
                    case EOptionCreationElementImport.Automatic:
                        return I.T("Automatic creation|20085");
                    case EOptionCreationElementImport.Confirm:
                        return I.T("Ask confirmation|20086");
                        break;
                }
                return "?";
           
            }
        }

        //-------------------------------------------
        public Bitmap GetImage()
        {
            switch (Code)
            {
                case EOptionCreationElementImport.None:
                    return Resource.NoCreate;
                case EOptionCreationElementImport.Automatic:
                    return Resource.Autocreate;
                    break;
                case EOptionCreationElementImport.Confirm:
                    return Resource.Questioncreate;
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
