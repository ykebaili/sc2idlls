using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Objet possédant un (ou des) blobs, qui ne se stockent que par
	/// différence dans une version prévisionnelle
	/// </summary>
	public interface IObjetABlobAVersionPartielle : IObjetDonneeAIdNumerique
	{
		/// <summary>
		/// Retourne true si le champ blob donné est geré par différence
		/// et non pas de manière complète
		/// </summary>
		/// <param name="strChamp"></param>
		/// <returns></returns>
		bool IsBlobParDifference(string strChamp);

		/// <summary>
		/// Retourne les différences partielles entre deux valeurs de blobs.
		/// null s'il n'y a pas de différences
		/// </summary>
		/// <param name="strChamp"></param>
		/// <param name="data"></param>
		/// <param name="dataOriginal"></param>
		/// <returns></returns>
		IDifferencesBlob GetDifferencesBlob(string strChamp, byte[] data, byte[] dataOriginal);

		
		/// <summary>
		/// réapplique les différences du IDifferencesBlob sur le data et retourne
		/// le nouveau data.
		/// </summary>
		/// <param name="differences"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		byte[] RedoModifs ( IDifferencesBlob differences, byte[]data);
	}

}
