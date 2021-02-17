using sc2i.common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futurocom.easyquery
{
    [Serializable]
    public class CEasyQueryAvecSource : CEasyQuery
    {
        //------------------------------------------------
        public CEasyQueryAvecSource()
        {
        }

        //------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }
        
        //------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            if (m_bBaseSerialize)
                return base.MySerialize(serializer);
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if ( result )
                result = base.MySerialize(serializer);
            if ( result )
            {
                List<CEasyQuerySource> lstSources = new List<CEasyQuerySource>(Sources);
                result = serializer.TraiteListe<CEasyQuerySource>(lstSources);
                if ( result &&serializer.Mode == ModeSerialisation.Lecture )
                    Sources = lstSources;
            }
            return result;
        }

        //------------------------------------------------
        private bool m_bBaseSerialize = false;
        private CResultAErreur BaseSerialize (C2iSerializer ser )
        {
            m_bBaseSerialize = true;
            try
            {
                return ((CEasyQuery)this).Serialize(ser);
            }
            finally
            {
                m_bBaseSerialize = false;
            }
        }

        //------------------------------------------------
        public static CEasyQueryAvecSource FromQuery(CEasyQuery query)
        {
            CEasyQueryAvecSource qas = new CEasyQueryAvecSource();
            byte[] data = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    CSerializerSaveBinaire ser = new CSerializerSaveBinaire(writer);
                    CResultAErreur res = query.Serialize(ser);

                    data = stream.GetBuffer();
                    writer.Close();
                    writer.Dispose();
                    if (!res)
                        return null;
                }
            }
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    CSerializerReadBinaire ser = new CSerializerReadBinaire(reader);
                    CResultAErreur res = qas.BaseSerialize(ser);
                    qas.Sources = query.Sources;
                    reader.Close();
                    stream.Close();
                    if (!res)
                        return null;
                }
            }
            return qas;
        }

        //------------------------------------------------
        public CEasyQuery GetEasyQuerySansSource ()
        {
            CEasyQuery qss = new CEasyQuery();
            byte[] data = null;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    CSerializerSaveBinaire ser = new CSerializerSaveBinaire(writer);
                    CResultAErreur res = BaseSerialize(ser);
                    data = stream.GetBuffer();
                    writer.Close();
                    writer.Dispose();
                    if (!res)
                        return null;
                }
            }
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    CSerializerReadBinaire ser = new CSerializerReadBinaire(reader);
                    CResultAErreur res = qss.Serialize(ser);
                    qss.Sources = Sources;
                    reader.Close();
                    stream.Close();
                    if (!res)
                        return null;
                }
            }
            return qss;

        }


        
    }
}
