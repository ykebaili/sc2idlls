using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Objet poss�dant un (ou des) blobs, qui ne se stockent que par
	/// diff�rence dans une version pr�visionnelle
	/// </summary>
	public interface IObjetABlobAVersionPartielle : IObjetDonneeAIdNumerique
	{
		/// <summary>
		/// Retourne true si le champ blob donn� est ger� par diff�rence
		/// et non pas de mani�re compl�te
		/// </summary>
		/// <param name="strChamp"></param>
		/// <returns></returns>
		bool IsBlobParDifference(string strChamp);

		/// <summary>
		/// Retourne les diff�rences partielles entre deux valeurs de blobs.
		/// null s'il n'y a pas de diff�rences
		/// </summary>
		/// <param name="strChamp"></param>
		/// <param name="data"></param>
		/// <param name="dataOriginal"></param>
		/// <returns></returns>
		IDifferencesBlob GetDifferencesBlob(string strChamp, byte[] data, byte[] dataOriginal);

		
		/// <summary>
		/// r�applique les diff�rences du IDifferencesBlob sur le data et retourne
		/// le nouveau data.
		/// </summary>
		/// <param name="differences"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		byte[] RedoModifs ( IDifferencesBlob differences, byte[]data);
	}

}
