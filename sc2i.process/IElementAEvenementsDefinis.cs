using System;

using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Toutes les entités qui sont liées à un définisseur d'évenement
	/// </summary>
	public interface IElementAEvenementsDefinis : IObjetDonneeAIdNumeriqueAuto
	{
		//Retourne tous les définisseurs de l'élément
		IDefinisseurEvenements[] Definisseurs{get;}
		
		//Retourne vrai si l'élément est défini par le définisseur demandé
		bool IsDefiniPar ( IDefinisseurEvenements definisseur );

	}

	/// <summary>
	/// Permet de simplifier l'implémentation de IsDefiniPar en
	/// utilisatn la proprété Definisseurs du IElementAEvenementsDefinis
	/// </summary>
	public class CUtilIsElementDefiniPar
	{
		public static bool IsDefiniPar ( CObjetDonneeAIdNumerique element, IDefinisseurEvenements definisseur )
		{
			if ( element == null )
				return false;
			if ( element is IElementAEvenementsDefinis )
			{
				IElementAEvenementsDefinis elementAEvenements = (IElementAEvenementsDefinis)element;
				IDefinisseurEvenements[] definisseurs = elementAEvenements.Definisseurs;
			
				if ( definisseurs == null )
					return false;

				foreach ( IDefinisseurEvenements defEx in definisseurs )
				{
					if ( defEx.Equals(definisseur) )
						return true;
					if ( definisseur is CComportementGenerique )
						foreach ( CComportementGenerique comportement in defEx.ComportementsInduits )
							if ( comportement.Equals ( definisseur ))
								return true;
				}
				return false;
			}
			else if ( definisseur is CComportementGenerique )
			{
				return ((CComportementGenerique)definisseur).IsAppliqueAObjet ( element );

			}
			return false;
				
		}
	}
}
