using System;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CEnvoyeurNotification.
	/// </summary>
	public class CEnvoyeurNotification
	{
		/// ///////////////////////////////////////////////////////////////////
		public static void EnvoieNotifications ( IDonneeNotification[] donnees )
		{
			if (donnees.Length == 0)
				return;
			IGestionnaireNotification gestionnaire = (IGestionnaireNotification)C2iFactory.GetNewObjetForSession("CGestionnaireNotification", typeof(IGestionnaireNotification), donnees[0].IdSessionEnvoyeur);
			if ( gestionnaire != null )
				gestionnaire.EnvoieNotifications ( donnees );
		}
	}
}
