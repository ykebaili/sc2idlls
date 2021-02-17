using System;
using System.Drawing.Printing;
using System.Collections;
using sc2i.multitiers.client;

namespace sc2i.multitiers.server
{
	/// <summary>
	/// Description résumée de CFournisseurNomImprimantesServeur.
	/// </summary>
	public class CFournisseurNomImprimantesServeur : C2iObjetServeur, IFournisseurNomImprimantesServeur
	{
		public CFournisseurNomImprimantesServeur( int nIdSession )
			:base ( nIdSession )
		{
		}

		public string[] GetNomsImprimantesSurServeur()
		{
			ArrayList lst = new ArrayList ( PrinterSettings.InstalledPrinters );
			return ( string[] )lst.ToArray(typeof(string) );
		}
	}
}
