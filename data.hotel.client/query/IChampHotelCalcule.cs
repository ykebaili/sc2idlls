using sc2i.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client.query
{
    public interface IChampHotelCalcule
    {
        IEnumerable<string> IdsChampsSource { get; }

        string NomChampFinal { get; set; }

        void FinaliseCalcul(
            string strIdTable,
            DataTable tableRemplieEtTriee,
            IDataRoomServer server,
            DateTime? dateDebut,
            DateTime? dateFin);
    }
}
