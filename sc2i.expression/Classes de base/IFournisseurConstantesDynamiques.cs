using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.expression
{
    public interface IFournisseurConstantesDynamiques
    {
        C2iExpressionConstanteDynamique[] GetConstantes();
        C2iExpressionConstanteDynamique GetConstante(string strConstante);
    }
}
