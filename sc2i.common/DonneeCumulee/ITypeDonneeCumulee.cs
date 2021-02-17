using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.DonneeCumulee
{
    public interface ITypeDonneeCumulee
    {
        int Id { get; }

        int GetNbMaxFields(ETypeChampDonneeCumulee type);

        string GetNomCle(int nNum);
        string GetNomValeur(int nNum);
        string GetNomDate(int nNum);
        string GetNomString(int nNum);

        string GetNomChamp(CChampDonneeCumulee champ);

        IEnumerable<CChampDonneeCumulee> GetChampsRenseignes();
    }
}
