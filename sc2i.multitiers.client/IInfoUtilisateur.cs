using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using sc2i.common;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Fournit des informations sur l'utilisateur en cours
	/// </summary>
	public interface IInfoUtilisateur
	{
		string NomUtilisateur{get;}
		IDonneeDroitUtilisateur GetDonneeDroit ( string strDroit );
		CRestrictionUtilisateurSurType GetRestrictionsSur ( Type tp, int? nIdVersion );

		/// <summary>
		/// Retourne la liste des types sur lesquels l'utilisateur
		/// a des restrictions sur certains objets de ce type
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		ReadOnlyCollection<Type> GetTypesARestrictionsSurObjets(int? nIdVersion);


		CRestrictionUtilisateurSurType GetRestrictionsSurObjet ( object objet, int? nIdVersion );
		CListeRestrictionsUtilisateurSurType GetListeRestrictions( int? nIdVersion );
		CDbKey KeyUtilisateur{get;}

		//Retourne la liste des ids de groupe auquel appartient l'utilisateur
		CDbKey[] ListeKeysGroupes{get;}

	}
}
