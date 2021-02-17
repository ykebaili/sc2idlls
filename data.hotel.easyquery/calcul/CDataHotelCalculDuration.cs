using data.hotel.client.query;
using data.hotel.easyquery.filtre;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery.calcul
{
    public class CDataHotelCalculDuration: IDataHotelCalcul
    {
        private string m_strIdChampSource = "";
        private IDHFiltre m_filtre = null;

        //--------------------------------------------------------------------
        public CDataHotelCalculDuration()
        {
        }

        //--------------------------------------------------------------------
        public string IdChampSource
        {
            get
            {
                return m_strIdChampSource;
            }
            set
            {
                m_strIdChampSource = value;
            }
        }

        //--------------------------------------------------------------------
        public IDHFiltre Filtre
        {
            get
            {
                return m_filtre;
            }
            set
            {
                m_filtre = value;
            }
        }

        //--------------------------------------------------------------------
        public Type TypeFinal
        {
            get
            {
                return typeof(double);
            }
        }

        //--------------------------------------------------------------------
        public IChampHotelCalcule GetChampHotel(object objetInterroge)
        {
            if (Filtre != null && IdChampSource.Length > 0)
            {
                CChampHotelCalculeDuree champ = new CChampHotelCalculeDuree();
                if (Filtre != null)
                    champ.Filtre = Filtre.GetTestFinal(objetInterroge);
                champ.IdChampSource = IdChampSource;
                return champ;
            }
            return null;
        }


        //--------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
            {
                serializer.TraiteString(ref m_strIdChampSource);
                result = serializer.TraiteObject<IDHFiltre>(ref m_filtre);
            }
            return result;
        }
    }
}
