using sc2i.common;
using sc2i.common.memorydb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.hotel.client
{
    [Serializable]
    [MemoryTable(c_nomTable, c_champId)]
    public class CDataHotel : CEntiteDeMemoryDbAIdAuto
    {
        public const string c_nomTable = "DATA_HOTEL";

        public const string c_champId = "HOTEL_ID";

        public const string c_champLibelle = "HOTEL_NAME";

        //---------------------------------------------------
        public CDataHotel ( CMemoryDb db)
            :base(db)
        {

        }

        //---------------------------------------------------
        public CDataHotel ( DataRow row )
            :base(row)
        {
            
        }

        //---------------------------------------------------
        public override void MyInitValeursParDefaut()
        {
            
        }

        //---------------------------------------------------
        [MemoryField(c_champLibelle)]
        public string Label
        {
            get
            {
                return Row.Get<string>(c_champLibelle);
            }
            set
            {
                Row[c_champLibelle] = value;
            }
        }

        //---------------------------------------------------
        [MemoryChild]
        public CListeEntitesDeMemoryDb<CDataRoom> Rooms
        {
            get
            {
                return GetFils<CDataRoom>();
            }
        }

        //---------------------------------------------------
        [MemoryChild]
        public CListeEntitesDeMemoryDb<CDataHotelTable> Tables
        {
            get
            {
                return GetFils<CDataHotelTable>();
            }
        }

        //---------------------------------------------------
        [MemoryChild]
        public CListeEntitesDeMemoryDb<CDataDispatch> DispatchRules
        {
            get
            {
                return GetFils<CDataDispatch>();
            }
        }

        //---------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //---------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            result = SerializeChamps(serializer, c_champLibelle);
            if (result)
                result = SerializeChilds<CDataRoom>(serializer);
            if (result)
                result = SerializeChilds<CDataDispatch>(serializer);
            if (result)
                result = SerializeChilds<CDataHotelTable>(serializer);
            return result;
        }
    }
}
