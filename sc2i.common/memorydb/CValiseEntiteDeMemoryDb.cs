using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace sc2i.common.memorydb
{
    [Serializable]
    public class CValiseEntiteDeMemoryDb : ISerializable
    {
        private const string c_cleDataSerialize = "VALISE_DATA";
        [NonSerialized]
        private CEntiteDeMemoryDb m_entite = null;

        //--------------------------------------------
        public CValiseEntiteDeMemoryDb(CEntiteDeMemoryDb entite)
        {
            m_entite = entite;
        }

        //--------------------------------------------
        public CValiseEntiteDeMemoryDb(SerializationInfo info, StreamingContext context)
        {
            byte[] serVal = (byte[])info.GetValue(c_cleDataSerialize, typeof(byte[]));
            if ( serVal == null || serVal.Length == 0 )
                return;
            MemoryStream stream = new MemoryStream(serVal);
            BinaryReader reader = new BinaryReader ( stream );
            CMemoryDb memDb = new CMemoryDb();
            CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader );
            CResultAErreur result = serializer.TraiteObject<CEntiteDeMemoryDb>(ref m_entite, new object[]{memDb});
            reader.Close();
            stream.Close();
        }

        //--------------------------------------------
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (m_entite == null)
                info.AddValue(c_cleDataSerialize, new byte[0]);
            else
            {
                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream);
                CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                CResultAErreur result = serializer.TraiteObject<CEntiteDeMemoryDb>(ref m_entite);
                writer.Close();
                stream.Close();
                if (!result)
                    throw new Exception(result.Erreur.ToString());
                
                info.AddValue(c_cleDataSerialize, stream.GetBuffer());
            }
        }


        //--------------------------------------------
        public CEntiteDeMemoryDb Entite
        {
            get
            {
                return m_entite;
            }
            set
            {
                m_entite = value;
            }
        }

        //--------------------------------------------
        public static implicit operator CEntiteDeMemoryDb(CValiseEntiteDeMemoryDb valise)
        {
            return valise.Entite;
        }

        //--------------------------------------------
        public static implicit operator CValiseEntiteDeMemoryDb(CEntiteDeMemoryDb entite)
        {
            return new CValiseEntiteDeMemoryDb(entite);
        }

       
    }
}
