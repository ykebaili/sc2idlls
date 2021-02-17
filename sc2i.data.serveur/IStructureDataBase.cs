using System;
using System.Collections.Generic;
using System.Text;

using sc2i.common;

namespace sc2i.data.serveur
{
	public interface IStructureDataBase
	{
		int GetLastVersion();
		C2iDataBaseUpdateOperationList GetListeTypeOfVersion(int nVersion);
	}
}
