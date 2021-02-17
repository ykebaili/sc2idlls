using System;

using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Toutes les entit�s qui sont li�es � un d�finisseur d'�venement
	/// </summary>
	public interface IElementAEvenementsDefinis : IObjetDonneeAIdNumeriqueAuto
	{
		//Retourne tous les d�finisseurs de l'�l�ment
		IDefinisseurEvenements[] Definisseurs{get;}
		
		//Retourne vrai si l'�l�ment est d�fini par le d�finisseur demand�
		bool IsDefiniPar ( IDefinisseurEvenements definisseur );

	}

	/// <summary>
	/// Permet de simplifier l'impl�mentation de IsDefiniPar en
	/// utilisatn la propr�t� Definisseurs du IElementAEvenementsDefinis
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
