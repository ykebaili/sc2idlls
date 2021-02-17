using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;

namespace sc2i.win32.expression.variablesDynamiques
{
    public interface IFormEditVariableDynamique
    {
        bool EditeLaVariable(IVariableDynamique variable,
            IElementAVariablesDynamiquesBase eltAVariables);
    }

    public static class CGestionnaireEditeursVariablesDynamiques
    {
        private static Dictionary<Type, Type> m_dicTypeVariableToTypeEditeur = new Dictionary<Type, Type>();

        //-------------------------------------------------------------------------
        public static void RegisterEditeur(Type typeVariable, Type typeEditeur)
        {
            m_dicTypeVariableToTypeEditeur[typeVariable] = typeEditeur;
        }

        //-------------------------------------------------------------------------
        public static Type GetTypeEditeur(Type typeVariable)
        {
            Type typeEditeur = null;
            m_dicTypeVariableToTypeEditeur.TryGetValue(typeVariable, out typeEditeur);
            return typeEditeur;
        }
    }
}
