using System;
using sc2i.common;
using sc2i.data;
using sc2i.expression;	

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Jeu de donn�es (requete ou structure d'export
	/// </summary>
	public interface IDefinitionJeuDonnees : I2iSerializable
	{
		string LibelleTypeDefinitionJeuDonnee {get;}

		/// <summary>
		/// Retourne dans le data du result un datatable ou un dataset
		/// contenant les donn�es.
		/// </summary>
		/// <param name="elementAVariables">Element fournissant les valeurs de variables �ventuellement
		/// n�c�ssaire � l'acquisition des donn�es</param>
		/// <param name="listeDonnees">liste d'objets � utiliser comme jeu de donn�es source. Si non d�fini, chaque
		/// d�finition se charge de cr�er sa liste, sinon, c'est cette liste qui est utilis�e (ignore le filtre du C2iStructureExportAvecFiltre)</param>
		/// <param name="fournisseurProprietes">Fournisseur de propri�t�s � utiliser. Null si fournisseur std</param>
		/// <returns></returns>
		CResultAErreur GetDonnees ( 
			IElementAVariablesDynamiquesAvecContexteDonnee elementAVariables, 
			CListeObjetsDonnees listeDonnees, 
			IIndicateurProgression indicateur);

		IElementAVariablesDynamiquesAvecContexteDonnee ElementAVariablesExterne{get;set;}

		CContexteDonnee ContexteDonnee{get;set;}

		/// <summary>
		/// Type de donn�es en entr�e du jeu de donn�e. Peut �tre null
		/// </summary>
		Type TypeDonneesEntree{get;}
	}
}
