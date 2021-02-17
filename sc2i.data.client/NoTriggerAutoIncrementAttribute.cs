using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.data
{
    /// <summary>
    /// Evite la création d'un trigger autoincrement pour les tables ORACLE
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NoTriggerAutoIncrementAttribute : Attribute
    {
        public readonly string Champ;

        public NoTriggerAutoIncrementAttribute ( string strChamp )
        {
            Champ = strChamp;
        }
    }
}
