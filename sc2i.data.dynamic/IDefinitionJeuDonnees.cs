using System;
using sc2i.common;
using sc2i.data;
using sc2i.expression;	

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Jeu de données (requete ou structure d'export
	/// </summary>
	public interface IDefinitionJeuDonnees : I2iSerializable
	{
		string LibelleTypeDefinitionJeuDonnee {get;}

		/// <summary>
		/// Retourne dans le data du result un datatable ou un dataset
		/// contenant les données.
		/// </summary>
		/// <param name="elementAVariables">Element fournissant les valeurs de variables éventuellement
		/// nécéssaire à l'acquisition des données</param>
		/// <param name="listeDonnees">liste d'objets à utiliser comme jeu de données source. Si non défini, chaque
		/// définition se charge de créer sa liste, sinon, c'est cette liste qui est utilisée (ignore le filtre du C2iStructureExportAvecFiltre)</param>
		/// <param name="fournisseurProprietes">Fournisseur de propriétés à utiliser. Null si fournisseur std</param>
		/// <returns></returns>
		CResultAErreur GetDonnees ( 
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables, 
			CListeObjetsDonnees listeDonnees, 
			IIndicateurProgression indicateur);

		IElementAVariablesDynamiquesAvecContexteDonnee ElementAVariablesExterne{get;set;}

		CContexteDonnee ContexteDonnee{get;set;}

		/// <summary>
		/// Type de données en entrée du jeu de donnée. Peut être null
		/// </summary>
		Type TypeDonneesEntree{get;}
	}
}
