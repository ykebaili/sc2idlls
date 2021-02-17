using System;
using System.Collections.Generic;
using System.Text;

using sc2i.multitiers.client;
using sc2i.data;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Tous les éléments qui sont lié à des Entités organisationnelles
	/// qui permettent de limiter les droits sur les éléments de l'application
	/// </summary>
	/// <remarks>
	/// le fait d'appartenir ou non à des entités organisationnelles permet
	/// de limiter les utilisateurs qui peuvent voir les entités<BR></BR>
	/// Chaque IElementAEO possède un champ, nommé "TIMOS_EO", qui contient
	/// les codes systèmes des EO auquel appartient l'entité, séparés
	/// par des ~. Lorsqu'un utilisateur liste les éléments, il ne
	/// prend que les éléments qui ne sont pas liés à des eos, ou 
	/// les éléments qui sont liés aux EO de l'entité.
	/// </remarks>
	public interface IElementAEO : IObjetDonneeAIdNumerique, IObjetARestrictionSurEntite
	{
		/// <summary>
		/// Retourne tous les codes des entités organisationnelles liés
		/// </summary>
		string CodesEntitesOrganisationnelles { get;set;}

		CListeObjetsDonnees EntiteOrganisationnellesDirectementLiees { get; }

		/// <summary>
		/// Retourne les éléments donnant des EOs à cet élément 
		/// (élément dont cet élément hérite les EOs)
		/// </summary>
		IElementAEO[] DonnateursEO { get; }

		/// <summary>
		/// Retourne la liste des éléments qui héritent des EO de
		/// cet élément
		/// </summary>
		IElementAEO[] HeritiersEO { get;}

        /// <summary>
        /// Affecte une nouvelle EO à un élement
        /// </summary>
        /// <param name="nIdEO">L'Id de l'EO à ajouter</param>
        /// <returns>Resultat</returns>
        CResultAErreur AjouterEO(int nIdEO);
        
        /// <summary>
        /// Supprime l'affecation d'une EO à un élement
        /// </summary>
        /// <param name="nIdEO">L'Id de l'EO à supprimer</param>
        /// <returns>Resultat</returns>
        CResultAErreur SupprimerEO(int nIdEO);

        /// <summary>
        /// Met à jour les EO directement liées à l'élement.
        /// Ajoute les EO non affectée présentes dasn la liste des nouveaux Ids.
        /// Supprime les EO éffectées non présentes dans la liste des noveaux Ids.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="nIdsOE"></param>
        /// <returns></returns>
        CResultAErreur SetAllOrganizationalEntities(int[] nIdsOE);

	}

	
}
