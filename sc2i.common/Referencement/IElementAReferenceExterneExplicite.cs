using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.Referencement
{
    public interface IElementAReferenceExterneExplicite
    {
        object[] GetReferencesExternesExplicites(CContexteGetReferenceExterne contexteGetRef);
    }
}
