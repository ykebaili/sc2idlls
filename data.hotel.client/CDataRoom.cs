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
    public class CDataRoom : CEntiteDeMemoryDb
    {
        public const string c_nomTable = "DATA_ROOM";
        public const string c_champId = "ROOM_ID";
        public const string c_champIp = "ROOM_IP";
        public const string c_champPort = "ROOM_PORT";

        //--------------------------------------------------------
        public CDataRoom(CMemoryDb db)
            : base(db)
        {
        }

        //--------------------------------------------------------
        public CDataRoom ( DataRow row )
            :base ( row )
        {

        }

        //--------------------------------------------------------
        [MemoryField(c_champId)]
        public string RoomId
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
        [MemoryField(c_champIp)]
        public string RoomIp
        {
            get
            {
                return Row.Get<string>(c_champIp);
            }
            set
            {
                Row[c_champIp] = value;
            }
        }

        //--------------------------------------------------------
        [MemoryField(c_champPort)]
        public int RoomPort
        {
            get
            {
                return Row.Get<int>(c_champPort);
            }
            set
            {
                Row[c_champPort] = value;
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
            result = SerializeChamps(serializer, c_champId, c_champPort, c_champIp);
            if (result)
                result = SerializeParent<CDataHotel>(serializer);
            return result;
        }
    }
}
