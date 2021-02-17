using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.formulaire
{
    public interface I2iWndAParametres
    {
        IEnumerable<string> GetNomsParametres();
        void SetParameterValue(string strValue, object valeur);
    }
}
