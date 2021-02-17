using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Permet de créer un IelementAVariableDynamiquesAvecContexte à partir
    /// de n'importe quel IElementAVariablesDynamiques
    /// </summary>
    public class CElementAVariablesDynamiquesAvecContexteFromIElementAVariablesDynamiques : IElementAVariablesDynamiquesAvecContexteDonnee
    {
        private IElementAVariablesDynamiquesBase m_eltAVariables = null;
        private CContexteDonnee m_contexteDonnee = null;


        //-----------------------------------------------------------------------------
        public CElementAVariablesDynamiquesAvecContexteFromIElementAVariablesDynamiques(
            IElementAVariablesDynamiquesBase source,
            CContexteDonnee contexte)
        {
            m_eltAVariables = source;
            m_contexteDonnee = contexte;
        }


        //-----------------------------------------------------------------------------
        public int IdSession
        {
            get 
            { 
                return m_contexteDonnee != null ?m_contexteDonnee.IdSession:-1; 
            }
        }

        //-----------------------------------------------------------------------------
        public CContexteDonnee ContexteDonnee
        {
            get
            {
                return m_contexteDonnee;
            }
        }

        //-----------------------------------------------------------------------------
        public IContexteDonnee IContexteDonnee
        {
            get{
                return ContexteDonnee;
            }
        }


        //-----------------------------------------------------------------------------
        public string GetNewIdForVariable()
        {
            return m_eltAVariables.GetNewIdForVariable();
        }

        //-----------------------------------------------------------------------------
        public void OnChangeVariable(IVariableDynamique variable)
        {
            m_eltAVariables.OnChangeVariable ( variable );
        }

        //-----------------------------------------------------------------------------
        public IVariableDynamique[] ListeVariables
        {
            get{
                return m_eltAVariables.ListeVariables;
            }
        }

        //-----------------------------------------------------------------------------
        public void AddVariable(IVariableDynamique variable)
        {
            m_eltAVariables.AddVariable ( variable );
        }

        //-----------------------------------------------------------------------------
        public void RemoveVariable(IVariableDynamique variable)
        {
            m_eltAVariables.RemoveVariable(variable);
        }

        //-----------------------------------------------------------------------------
        public bool IsVariableUtilisee(IVariableDynamique variable)
        {
            return m_eltAVariables.IsVariableUtilisee ( variable );
        }


        //-----------------------------------------------------------------------------
        public CVariableDynamique GetVariable(string strIdVariable)
        {
            return m_eltAVariables.GetVariable(strIdVariable);
        }

        //-----------------------------------------------------------------------------
        public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
        {
            return m_eltAVariables.SetValeurChamp ( variable, valeur );
        }

        //-----------------------------------------------------------------------------
        public CResultAErreur SetValeurChamp(string strIdVariable, object valeur)
        {
            return m_eltAVariables.SetValeurChamp(strIdVariable, valeur);
        }

        //-----------------------------------------------------------------------------
        public object GetValeurChamp(IVariableDynamique variable)
        {
            return m_eltAVariables.GetValeurChamp ( variable );
        }

        //-----------------------------------------------------------------------------
        public object GetValeurChamp(string strIdVariable)
        {
            return m_eltAVariables.GetValeurChamp(strIdVariable);
        }

        //-----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux)
        {
            IElementAVariablesDynamiques elt = m_eltAVariables as IElementAVariablesDynamiques;
            if (elt != null)
                return elt.GetDefinitionsChamps(typeInterroge, nNbNiveaux);
            return new CFournisseurPropDynStd().GetDefinitionsChamps(typeInterroge, nNbNiveaux);
        }

        //-----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
        {
            IElementAVariablesDynamiques elt = m_eltAVariables as IElementAVariablesDynamiques;
            if (elt != null)
                return elt.GetDefinitionsChamps(typeInterroge, nNbNiveaux, defParente);
            return new CFournisseurPropDynStd().GetDefinitionsChamps(typeInterroge, nNbNiveaux, defParente);
        }

        //-----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
        {
            IElementAVariablesDynamiques elt = m_eltAVariables as IElementAVariablesDynamiques;
            if (elt != null)
                return elt.GetDefinitionsChamps(objet);
            return new CFournisseurPropDynStd().GetDefinitionsChamps(objet);
        }

        //-----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
        {
            IElementAVariablesDynamiques elt = m_eltAVariables as IElementAVariablesDynamiques;
            if (elt != null)
                return elt.GetDefinitionsChamps(objet);
            return new CFournisseurPropDynStd().GetDefinitionsChamps(objet, defParente);
        }

        //-----------------------------------------------------------------------------
        public CDefinitionProprieteDynamique[] GetProprietesInstance()
        {
            return m_eltAVariables.GetProprietesInstance();
        }
    }
}
