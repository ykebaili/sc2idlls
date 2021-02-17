using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.doccode
{
	[Flags]
	public enum EDocRessourceType
	{
		Image = 2,
		PDF = 4,
		Fichier = 8,
		URLWeb = 16,
		Procedure = 32,
		Icone = 64
	}

}
