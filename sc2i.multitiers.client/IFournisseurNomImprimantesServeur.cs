using System;

namespace sc2i.multitiers.client
{

	public class CFournisseurNomImprimantes
	{
		public static string[] GetNomsImprimantesServeur ( int nIdSession )
		{
			IFournisseurNomImprimantesServeur fournisseur = (IFournisseurNomImprimantesServeur)C2iFactory.GetNewObjetForSession ( "CFournisseurNomImprimantesServeur", typeof(IFournisseurNomImprimantesServeur), nIdSession );
			return fournisseur.GetNomsImprimantesSurServeur();
		}
	}

	/// <summary>
	/// Description r�sum�e de IFournisseurNomImprimantesServeur.
	/// </summary>
	public interface IFournisseurNomImprimantesServeur
	{
		string[] GetNomsImprimantesSurServeur();
	}
}
