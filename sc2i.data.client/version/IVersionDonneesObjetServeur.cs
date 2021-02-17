using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
	public interface IVersionDonneesObjetServeur
	{
		CResultAErreur AnnuleModificationsPrevisionnelles(int nIdVersionObjet);
	}


}
