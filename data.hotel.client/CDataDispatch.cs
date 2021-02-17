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
    public class CDataDispatch : CEntiteDeMemoryDbAIdAuto
    {
        private static Dictionary<string, string> m_cacheDispatch = new Dictionary<string, string>();

        public const string c_nomTable = "DATA_DISPATCH";
        public const string c_champId = "DIS_ID";
        public const string c_champStartEntityId = "DIS_ETT_ID_START";

        //--------------------------------------------------------
        public CDataDispatch(CMemoryDb db)
            :base(db)
        {

        }

        //--------------------------------------------------------
        public CDataDispatch(DataRow row)
            : base(row)
        {
        }

        //--------------------------------------------------------
        [MemoryField(c_champStartEntityId)]
        public string StartEntityId
        {
            get
            {
                return Row.Get<string>(c_champStartEntityId);
            }
            set
            {
                Row[c_champStartEntityId] = value;
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
        [MemoryParent(true)]
        public CDataRoom DestinationRoom
        {
            get
            {
                return GetParent<CDataRoom>();
            }
            set
            {
                SetParent<CDataRoom>(value);
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
        protected override CResultAErreur MySerialize(sc2i.common.C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = SerializeChamps(serializer, c_champStartEntityId);
            if (result)
                result = SerializeParent<CDataHotel>(serializer);
            if (result)
                result = SerializeParent<CDataRoom>(serializer);
            return result;
        }

        //--------------------------------------------------------
        public static void ResetDispatchCache()
        {
            m_cacheDispatch.Clear();
        }

        //--------------------------------------------------------
        public static CDataRoom GetRoomFor ( string strIdEntite, CMemoryDb db )
        {
            string strRoomId = "";
            if ( m_cacheDispatch.TryGetValue ( strIdEntite, out strRoomId ))
            {
                CDataRoom room = new CDataRoom(db);
                if (room.ReadIfExist(strRoomId))
                    return room;
            }
            CListeEntitesDeMemoryDb<CDataDispatch> lstDispatch = new CListeEntitesDeMemoryDb<CDataDispatch>(db);
            lstDispatch.Filtre = new CFiltreMemoryDb(CDataDispatch.c_champStartEntityId + " like @1", strIdEntite[0]+"%");
            lstDispatch.Sort = CDataDispatch.c_champStartEntityId + " desc";
            CDataRoom roomDest = null;
            foreach (CDataDispatch dd in lstDispatch)
            {
                if (strIdEntite.StartsWith(dd.StartEntityId))
                    roomDest = dd.DestinationRoom; ;
            }
            if (roomDest == null)
            {
                //Cherche le dispatch vide
                lstDispatch.Filtre = new CFiltreMemoryDb(CDataDispatch.c_champStartEntityId + "=@1", "");
                if (lstDispatch.Count() > 0)
                    roomDest = lstDispatch.ElementAt(0).DestinationRoom;
            }
            if (roomDest != null)
                m_cacheDispatch[strIdEntite] = roomDest.Id;
            return roomDest;            
        }
    }
}
