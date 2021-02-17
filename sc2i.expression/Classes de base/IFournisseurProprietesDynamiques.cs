using System;

namespace sc2i.expression
{
	/// <summary>
	/// Description r�sum�e de IFournisseurProprietesDynamiques.
	/// </summary>
	public interface IFournisseurProprietesDynamiques
	{
		CDefinitionProprieteDynamique[] GetDefinitionsChamps ( Type typeInterroge, int nNbNiveaux );
		CDefinitionProprieteDynamique[] GetDefinitionsChamps(Type typeInterroge, int nNbNiveaux, CDefinitionProprieteDynamique defParente);

		CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet);
		CDefinitionProprieteDynamique[] GetDefinitionsChamps(CObjetPourSousProprietes objet, CDefinitionProprieteDynamique defParente);
	}

    public interface IFournisseurProprietesDynamiquesAVariablesDeFormule : IFournisseurProprietesDynamiques
    {
        CDefinitionProprieteDynamiqueVariableFormule[] VariablesDeFormule { get; }
    }

}
