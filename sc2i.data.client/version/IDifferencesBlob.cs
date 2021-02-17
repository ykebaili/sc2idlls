using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// représente les modifications partielles d'un blob dans une
	/// version prévisionnelle
	/// </summary>
	public interface IDifferencesBlob : I2iSerializable
	{
		List<CDetailOperationSurObjet> GetDetails();
	}

}
