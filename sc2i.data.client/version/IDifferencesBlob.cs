using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// repr�sente les modifications partielles d'un blob dans une
	/// version pr�visionnelle
	/// </summary>
	public interface IDifferencesBlob : I2iSerializable
	{
		List<CDetailOperationSurObjet> GetDetails();
	}

}
