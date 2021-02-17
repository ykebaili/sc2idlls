using System;
using System.Collections;
using System.Collections.Generic;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de CContexteAnalyse2iExpression.
	/// </summary>
	[Serializable]
	public class CContexteAnalyse2iExpression : IFournisseurProprietesDynamiques
	{
		public readonly IFournisseurProprietesDynamiques FournisseurProprietes = null;
		public readonly CObjetPourSousProprietes ObjetAnalyse = null;

		//Liste des CVariable déclarées
		//Nom variable->CDefinitionPRoprieteDynamiqueVariableFormule
		public Hashtable m_tableVariables = new Hashtable();

		public CContexteAnalyse2iExpression(
			IFournisseurProprietesDynamiques fournisseurProprietesDynamiques,
			CObjetPourSousProprietes objetAnalyse)
		{
			FournisseurProprietes = fournisseurProprietesDynamiques;
			if (objetAnalyse == null)
				objetAnalyse = new CObjetPourSousProprietes(null);
			ObjetAnalyse = objetAnalyse;
            AddVariablesDeFournisseur();
		}

		public CContexteAnalyse2iExpression(
			IFournisseurProprietesDynamiques fournisseurProprietesDynamiques,
			Type tp)
		{
			FournisseurProprietes = fournisseurProprietesDynamiques;
			ObjetAnalyse = new CObjetPourSousProprietes(tp);
            AddVariablesDeFournisseur();
		}

        private void AddVariablesDeFournisseur()
        {
            IFournisseurProprietesDynamiquesAVariablesDeFormule f = FournisseurProprietes as IFournisseurProprietesDynamiquesAVariablesDeFormule;
            if (f != null)
                foreach (CDefinitionProprieteDynamiqueVariableFormule def in f.VariablesDeFormule)
                    AddVariable(def);
        }



		public void AddVariable ( CDefinitionProprieteDynamiqueVariableFormule def )
		{
			m_tableVariables[def.Nom.ToUpper()] = def;
            m_cacheDefs.Clear();

		}

		public CDefinitionProprieteDynamiqueVariableFormule GetVariable ( string strNom )
		{
			return (CDefinitionProprieteDynamiqueVariableFormule)m_tableVariables[strNom.ToUpper()];
		}
		
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux)
		{
			return GetDefinitionsChamps ( tp, nNbNiveaux, null );
		}

		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type tp, int nNbNiveaux, CDefinitionProprieteDynamique defParente)
		{
            return GetDefinitionsChamps(new CObjetPourSousProprietes(tp), defParente);
		}

		//----------------------------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet)
		{
			return GetDefinitionsChamps ( objet, null );
		}

        private Dictionary<object, CDefinitionProprieteDynamique[]> m_cacheDefs = new Dictionary<object, CDefinitionProprieteDynamique[]>();

		//----------------------------------------------------------------------------------------
		public CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente)
		{
			if (objet != null)
			{
                CDefinitionProprieteDynamique[] defCache = null;
                if (m_cacheDefs.TryGetValue(objet, out defCache))
                    return defCache;
				List<CDefinitionProprieteDynamique> lst = new List<CDefinitionProprieteDynamique>();
                if (FournisseurProprietes != null)
                    lst.AddRange(FournisseurProprietes.GetDefinitionsChamps(objet, defParente));
                foreach (CDefinitionProprieteDynamique def in m_tableVariables.Values)
                    lst.Add(def);
				/*if ( objet.ElementAVariableInstance != null )
					lst.AddRange ( objet.ElementAVariableInstance.GetProprietesInstance() );*/
                defCache = lst.ToArray();
                m_cacheDefs[objet] = defCache;
				return lst.ToArray();
			}
			return new CDefinitionProprieteDynamique[]{};
		}


		
	}
}
