using System;
using System.Collections;
using sc2i.expression;
using sc2i.common;
using System.Collections.Generic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CElementAVariablesDynamiques.
	/// </summary>
	/// <summary>
	/// Implémentation de base d'un IElementAVariablesDynamiques
	/// </summary>
	[Serializable]
	public class CElementAVariablesDynamiques : IElementAVariablesDynamiquesAvecContexteDonnee
	{
		private ArrayList m_listeVariables = new ArrayList();
		private Hashtable m_tableValeursChamps = new Hashtable();
		private int m_nIdSession = -1;

		[NonSerialized]
		private CContexteDonnee m_contexteDonnee = null;

		//-------------------------------------------------
		public virtual int IdSession
		{
			get
			{
				if ( m_contexteDonnee == null )
					return m_nIdSession;
				return m_contexteDonnee.IdSession;
			}
		}

		//-------------------------------------------------
		public virtual CContexteDonnee ContexteDonnee
		{
			get
			{
				return m_contexteDonnee;
			}
			set
			{
				m_contexteDonnee = value;
			}
		}

        //-----------------------------------------------------------------------------
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return ContexteDonnee;
            }
        }

		//-------------------------------------------------
		public string GetNewIdForVariable()
		{
            return CUniqueIdentifier.GetNew();
		}

		//-------------------------------------------------
		public virtual void OnChangeVariable(IVariableDynamique variable)
		{
		}

		//-------------------------------------------------
		public virtual IVariableDynamique[] ListeVariables
		{
			get
			{
                return (IVariableDynamique[])m_listeVariables.ToArray(typeof(IVariableDynamique));
			}
		}

        //-------------------------------------------------
        public virtual void AddVariable(IVariableDynamique variable)
        {
            m_listeVariables.Add(variable);
        }

        //-------------------------------------------------
        public virtual void RemoveVariable(IVariableDynamique variable)
        {
            m_listeVariables.Remove(variable);
        }

		//-------------------------------------------------
		public virtual bool IsVariableUtilisee(IVariableDynamique variable)
		{
			if ( variable is CVariableDynamiqueSysteme )
				return true;
			return false;
		}

		//-------------------------------------------------
        public virtual CVariableDynamique GetVariable(string strIdVariable)
        {
            foreach (CVariableDynamique variable in m_listeVariables)
            {
                if (variable.IdVariable == strIdVariable)
                    return variable;
            }
            return null;
        }

		//-------------------------------------------------
		public virtual CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
		{
            if (variable != null)
            {
                SetValeurChamp(variable.IdVariable, valeur);
            }

			return CResultAErreur.True;
		}

		//-------------------------------------------------
        public virtual CResultAErreur SetValeurChamp(string strIdVariable, object valeur)
        {
            m_tableValeursChamps[strIdVariable] = valeur;
            return CResultAErreur.True;
        }

		//-------------------------------------------------
		public virtual object GetValeurChamp(IVariableDynamique variable)
		{
			if ( variable == null )
				return null;
			if(  variable is CVariableDynamiqueCalculee )
			{
				CVariableDynamiqueCalculee variableCalculee = (CVariableDynamiqueCalculee)variable;
				return variableCalculee.GetValeur ( this );
			}
			else if ( variable is CVariableDynamiqueListeObjets )
			{
				CVariableDynamiqueListeObjets variableListe = (CVariableDynamiqueListeObjets)variable;
				return variableListe.GetValeur ( this );
			}
			else
			{
				object valeur = m_tableValeursChamps[variable.IdVariable];
				if ( valeur == null && variable is CVariableDynamiqueSaisie )
					return ((CVariableDynamiqueSaisie)variable).GetValeurParDefaut();
				return valeur;
			}
		}

		//-------------------------------------------------
        public virtual object GetValeurChamp(string strIdVariable)
        {
            foreach (CVariableDynamique variable in m_listeVariables)
                if (variable.IdVariable == strIdVariable)
                    return GetValeurChamp(variable);
            return null;
        }

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetProprietesInstance()
		{
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			foreach (CVariableDynamique variable in ListeVariables)
			{
				bool bHasSubs = !variable.TypeDonnee.IsArrayOfTypeNatif && CFournisseurGeneriqueProprietesDynamiques.HasSubProperties(variable.TypeDonnee.TypeDotNetNatif);
				CDefinitionProprieteDynamique def = new CDefinitionProprieteDynamiqueVariableDynamique(variable, bHasSubs);
                def.Rubrique = variable.Rubrique;
				if (def != null)
					lst.Add(def);
			}
			return lst.ToArray();
		}

		//-------------------------------------------------
		public virtual CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
		{
			/*ITraducteurNomChamps traducteur =  null;
			if ( defParente is ITraducteurNomChamps )
				traducteur = (ITraducteurNomChamps)defParente;*/
			CDefinitionProprieteDynamique[] defs = new CDefinitionProprieteDynamique[0];
			CFournisseurPropDynStd four = new CFournisseurPropDynStd ( true );
			if ( !tp.IsSubclassOf ( GetType() ) && tp!=GetType())
			{
				defs =  four.GetDefinitionsChamps(tp, nNbNiveaux, defParente );
				if ( defParente != null )
					return defs;
			}
			List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
			foreach (CDefinitionProprieteDynamique def in defs)
				lst.Add(def);
			lst.AddRange(GetProprietesInstance());
			return lst.ToArray ( );
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps(objet, null);
		}

		//-------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			if (objet != null)
				return GetDefinitionsChamps(objet.TypeAnalyse, 0, defParente);
			return new CDefinitionProprieteDynamique[0];
		}

		//-------------------------------------------------
		public virtual  CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux)
		{
			return GetDefinitionsChamps ( tp, nNbNiveaux, null );
		}

		//-------------------------------------------------
        public virtual void CopieStatique(IElementAVariablesDynamiquesAvecContexteDonnee elt)
        {
            if (elt != null)
            {
                ContexteDonnee = elt.ContexteDonnee;
                foreach (IVariableDynamique variable in elt.ListeVariables)
                {
                    CVariableDynamiqueStatique newVariable = new CVariableDynamiqueStatique(this);
                    newVariable.IdVariable = variable.IdVariable;
                    newVariable.Nom = variable.Nom;
                    newVariable.Description = variable.Description;
                    newVariable.SetTypeDonnee(variable.TypeDonnee);
                    AddVariable(newVariable);
                    SetValeurChamp(newVariable, elt.GetValeurChamp(variable));
                }
                m_nIdSession = elt.IdSession;
                m_contexteDonnee = elt.ContexteDonnee;
            }
        }

		//-------------------------------------------------
		public static CElementAVariablesDynamiques GetNewFrom ( IElementAVariablesDynamiquesAvecContexteDonnee elt )
		{
			if ( elt == null )
				return null;
			CElementAVariablesDynamiques newElt = new CElementAVariablesDynamiques();
			newElt.CopieStatique( elt );
			return newElt;
		}

		//-------------------------------------------------
		/// <summary>
		/// Crée un élément à variable, contenant une seule variable du type demandé,
		/// avec le nom demandé.
		/// </summary>
		/// <param name="tp"></param>
		/// <param name="strNomVariable"></param>
		/// <returns></returns>
		public static CElementAVariablesDynamiques GetElementAUneVariableType(Type tp, string strNomVariable)
		{
			CElementAVariablesDynamiques elt = new CElementAVariablesDynamiques();

			CVariableDynamiqueSysteme newVariable = new CVariableDynamiqueSysteme(elt);
			newVariable.Nom = strNomVariable ;
            //TESTDBKEYOK (SC)
            //Id doit être égal à 0 pour compatiblité
            newVariable.IdVariable = "0";
			newVariable.SetTypeDonnee(new CTypeResultatExpression(tp, false));
			elt.AddVariable(newVariable);

			return elt;			
		}

	}
}
