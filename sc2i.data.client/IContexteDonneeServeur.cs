using System;

using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description r�sum�e de IIContexteDonneeServeur.
	/// </summary>
	public interface IContexteDonneeServeur
	{
		/// <summary>
		/// Id du CVersionDonnees associ� au contexte donnee serveur. Cet
		/// est affect� par le contexte serveur lui m�me
		/// </summary>
		int? IdVersionArchiveAssociee { get;}


		CResultAErreur SaveAll( CContexteDonnee contexteToSave, bool bShouldTraiteAvantSauvegarde );
	}
}
