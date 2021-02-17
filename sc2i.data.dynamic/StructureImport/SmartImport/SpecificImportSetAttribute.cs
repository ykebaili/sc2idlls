using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.data.dynamic.StructureImport.SmartImport
{
    /// <summary>
    /// Permet de définir une fonction Set spécifique pour l'import
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SpecificImportSetAttribute : Attribute
    {
        public readonly string NomMethodeSetSpecifique;

        public SpecificImportSetAttribute(string strNomMethodeSetSpecifique)
        {
            NomMethodeSetSpecifique = strNomMethodeSetSpecifique;
        }
    }
}
