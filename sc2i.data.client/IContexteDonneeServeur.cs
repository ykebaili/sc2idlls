using System;

using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de IIContexteDonneeServeur.
	/// </summary>
	public interface IContexteDonneeServeur
	{
		/// <summary>
		/// Id du CVersionDonnees associé au contexte donnee serveur. Cet
		/// est affecté par le contexte serveur lui même
		/// </summary>
		int? IdVersionArchiveAssociee { get;}


		CResultAErreur SaveAll( CContexteDonnee contexteToSave, bool bShouldTraiteAvantSauvegarde );
	}
}
