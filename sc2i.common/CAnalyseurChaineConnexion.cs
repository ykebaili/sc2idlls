using System;
using System.Collections;
using System.Globalization;

namespace sc2i.common
{

	/// <summary>
	/// Analyse une chaine de connexion au format 
	/// CHAMP=Valeur;Champ=Valeur;Champ=Valeur
	/// </summary>
	public sealed class CAnalyseurChaineConnexion
	{
		private CAnalyseurChaineConnexion()
		{
			
		}

		/// <summary>
		/// Retourne une hashtable contenant comme clé le nom du champ et la valeur en valeur
		/// </summary>
		/// <param name="strChaineConnexion"></param>
		/// <param name="tableValeurs"></param>
		/// <returns></returns>
		public static CResultAErreur AnalyseChaine ( string strChaineConnexion, Hashtable tableValeurs, bool bConvertNomToMini )
		{
			CResultAErreur result = CResultAErreur.True;
			string[] lstZones = strChaineConnexion.Split(';');
			if ( lstZones.Length == 0 )
			{
				result.EmpileErreur(I.T("Invalid connection string format|30011"));
				return result;
			}
			foreach ( string strZone in lstZones )
			{
				if ( !String.IsNullOrEmpty(strZone.Trim()) )
				{
					string[] donnees = strZone.Split('=');
					if ( donnees.Length != 2 )
					{
						result.EmpileErreur(I.T("Error in the connection string towards '@1'|30012",strZone));
						return result;
					}
					string strChamp = donnees[0];
					if ( bConvertNomToMini )
						strChamp = strChamp.ToLower(CultureInfo.InvariantCulture);
					tableValeurs[strChamp] = donnees[1];
				}
			}
			return result;
		}
	}
}
