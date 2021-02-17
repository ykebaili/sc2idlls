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
    public class CDataHotelTable : CEntiteDeMemoryDbAIdAuto
    {
        public const string c_extPropTableId = "TABLE_ID";

        public const string c_nomChampTableEntiteId = "SYS_ENTITY_ID";
        public const string c_nomChampTableDate = "SYS_DATE";


        public const string c_nomTable = "HOTEL_TABLE";

        public const string c_champId = "TABLE_ID";
        public const string c_champName = "TABLE_NAME";

        //--------------------------------------------------------
        public CDataHotelTable(CMemoryDb db)
            : base(db)
        {
        }

        //--------------------------------------------------------
        public CDataHotelTable ( DataRow row )
            :base ( row )
        {

        }

        //--------------------------------------------------------
        [MemoryField(c_champName)]
        public string TableName
        {
            get
            {
                return Row.Get<string>(c_champName);
            }
            set
            {
                Row[c_champName] = value;
            }
        }

        //--------------------------------------------------------
        [MemoryParent(true)]
        public CDataHotel Hotel
        {
            get
            {
                return GetParent<CDataHotel>();
            }
            set
            {
                SetParent<CDataHotel>(value);
            }
        }

        //--------------------------------------------------------
        [MemoryChild]
        public CListeEntitesDeMemoryDb<CDataHotelField> Fields
        {
            get
            {
                return GetFils<CDataHotelField>();
            }
        }
        
        //--------------------------------------------------------
        public override void MyInitValeursParDefaut()
        {
            
        }


        //--------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = SerializeChamps(serializer, c_champName);
            if (result)
                result = SerializeParent<CDataHotel>(serializer);
            if (result)
                result = SerializeChilds<CDataHotelField>(serializer);
            return result;
        }
    }
}
