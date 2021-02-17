using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sc2i.common;
using System.Collections;


namespace sc2i.data
{


	public interface IVersionDonneesServeur
	{
		CResultAErreur AppliqueModificationsPrevisionnelles(int nIdVersion);
        CResultAErreur AppliqueModificationsPrevisionnelles(int nIdVersion, IIndicateurProgression indicateur);
		CResultAErreur AnnulerModificationsPrevisionnelles(int nIdVersion);
		CResultAErreur CreatePrevisionnelleFromArchive(int nIdVersion);
		CResultAErreur PurgerHistorique(DateTime dateLimite);
	}

}
