using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.expression
{
    /// <summary>
    /// Utilisée pour connaitre les types de variables dynamiques qui
    /// existent dans le systeme.
    /// </summary>
    public static class CGestionnaireVariablesDynamiques
    {
        private static Dictionary<Type, CDescripteurTypeVariableDynamique> m_dicTypes = new Dictionary<Type,CDescripteurTypeVariableDynamique>();
        private static HashSet<Type> m_setTypesMasques = new HashSet<Type>();


        //--------------------------------------------------------------------------------
        public static void RegisterTypeVariable ( Type typeVariable, string strLibelle )
        {
            CDescripteurTypeVariableDynamique desc = new CDescripteurTypeVariableDynamique(strLibelle, typeVariable);
            m_dicTypes[typeVariable] = desc;
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Permet d'indiquer qu'un type de variable ne doit pas être visible
        /// générallement lorsqu'un type de variable évolué remplace un type non évolué,
        /// par exemple, CVariableDynamiqueSaisieSimple est masquée par CVariableDynamiqueSaisie
        /// </summary>
        /// <param name="typeVariable"></param>
        public static void MasqueTypeVariable(Type typeVariable)
        {
            m_setTypesMasques.Add(typeVariable);
        }

        //--------------------------------------------------------------------------------
        public static IEnumerable<CDescripteurTypeVariableDynamique> GetTypesVariablesConnus()
        {
            List<CDescripteurTypeVariableDynamique> lst = new List<CDescripteurTypeVariableDynamique>();
            foreach (CDescripteurTypeVariableDynamique desc in m_dicTypes.Values)
            {
                if (!m_setTypesMasques.Contains(desc.TypeVariable))
                    lst.Add(desc);
            }
            lst.Sort((x, y) => x.LibelleTypeVariable.CompareTo(y.LibelleTypeVariable));
            return lst.ToArray();
        }
        
    }

    //--------------------------------------------------------------------------------
    public class CDescripteurTypeVariableDynamique
    {
        public string LibelleTypeVariable;
        public Type TypeVariable;

        public CDescripteurTypeVariableDynamique(string strLibelleTypeVariable,
            Type typeVariable)
        {
            LibelleTypeVariable = strLibelleTypeVariable;
            TypeVariable = typeVariable;
        }
    }
}
