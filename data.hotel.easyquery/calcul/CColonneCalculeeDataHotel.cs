using data.hotel.easyquery.calcul;
using futurocom.easyquery;
using sc2i.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.easyquery
{
    public class CColonneCalculeeDataHotel : IColumnDeEasyQuery
    {
        private string m_strId = "";
        private string m_strName = "";
        IDataHotelCalcul m_calcul = null;

        //------------------------------------------------------------
        public CColonneCalculeeDataHotel()
        {
            m_strId = CUniqueIdentifier.GetNew();
            
        }

        //------------------------------------------------------------
        public string Id
        {
            get { return m_strId; }
        }

        //------------------------------------------------------------
        public string ColumnName
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        //------------------------------------------------------------
        public IDataHotelCalcul Calcul
        {
            get
            {
                return m_calcul;
            }
            set
            {
                m_calcul = value;
            }
        }

        //------------------------------------------------------------
        public Type DataType
        {
            get
            {
                if (m_calcul != null)
                    return m_calcul.TypeFinal;
                return typeof(double);
            }
            set
            {
                
            }
        }


        //------------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }


        //------------------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( result )
            {
                serializer.TraiteString(ref m_strId);
                serializer.TraiteString(ref m_strName);
                result = serializer.TraiteObject<IDataHotelCalcul>(ref m_calcul);
            }
            return result;
        }


        //------------------------------------------------------------
        public CResultAErreur OnRemplaceColSource(IColumnDeEasyQuery oldCol, IColumnDeEasyQuery newCol)
        {
            return CResultAErreur.True;
        }
    }
}
