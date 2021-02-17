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
    public class CDataHotelField : CEntiteDeMemoryDb
    {
        public const string c_extPropColumnId = "COLUMN_ID";

        public const string c_nomTable = "HOTEL_FIELD";

        public const string c_champId = "FIELD_ID";
        public const string c_champName = "FIELD_NAME";

        //--------------------------------------------------------
        public CDataHotelField(CMemoryDb db)
            : base(db)
        {
        }

        //--------------------------------------------------------
        public CDataHotelField ( DataRow row )
            :base ( row )
        {

        }

        [MemoryField(c_champId)]
        public string Id
        {
            get
            {
                return Row.Get<string>(c_champId);
            }
            set
            {
                Row[c_champId] = value;
            }
        }

        //--------------------------------------------------------
        [MemoryField(c_champName)]
        public string FieldName
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
        public CDataHotelTable HotelTable
        {
            get
            {
                return GetParent<CDataHotelTable>();
            }
            set
            {
                SetParent<CDataHotelTable>(value);
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
                result = SerializeParent<CDataHotelTable>(serializer);
            return result;
        }
    }
}
