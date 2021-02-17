using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using sc2i.common;
using sc2i.common.recherche;
using System.Reflection;

namespace sc2i.expression.recherche
{
    /// <summary>
    /// Teste si une formule utilise une définition de champ
    /// </summary>
    [AutoExec("Autoexec")]
    public class CTesteurUtilisationDefinitionChampInExpression : ITesteurUtilisationObjet
    {
        private static CTesteurUtilisationDefinitionChampInExpression m_instance = null;

        public static CTesteurUtilisationDefinitionChampInExpression GetInstance()
        {
            if (m_instance == null)
                m_instance = new CTesteurUtilisationDefinitionChampInExpression();
            return m_instance;
        }

        public static void Autoexec()
        {
            
            CTesteurUtilisationObjet.RegisterTesteur(GetInstance());
            CTesteurUtilisationObjetInPropriete.RegisterTesteurSupplementaire(typeof(C2iExpression), GetInstance());
        }




        #region ITesteurUtilisationObjet Membres

        //------------------------------------------------------------------------
        public bool DoesUse(object objetUtilisateur, object objetCherche)
        {
            if (objetCherche is CDefinitionProprieteDynamique)
            {
                C2iExpression formule = objetUtilisateur as C2iExpression;
                if (formule != null)
                {
                    IEnumerable<C2iExpressionChamp> lstExps = from C2iExpressionChamp expChamp in formule.ExtractExpressionsType(typeof(C2iExpressionChamp))
                                                              where expChamp.DefinitionPropriete != null && expChamp.DefinitionPropriete.Equals(objetCherche)
                                                              select expChamp;
                    if (lstExps.Count() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
