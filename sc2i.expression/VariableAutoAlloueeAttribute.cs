using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.expression
{
    //Pour les variables de process qui s'allouent toutes seule dés leur première utilisation
    [AttributeUsage(AttributeTargets.Class)]
    public class VariableAutoAlloueeAttribute : Attribute
    {
    }
}
