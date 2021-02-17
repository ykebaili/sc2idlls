using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace futurocom.easyquery
{
    public class CListeQuerySource : I2iSerializable
    {
        private Dictionary<string, CEasyQuerySource> m_dicSources = new Dictionary<string,CEasyQuerySource>();

        //----------------------------------
        public CListeQuerySource()
        {
        }

        //----------------------------------
        public CListeQuerySource(IEnumerable<CEasyQuerySource> sources)
        {
            if (sources != null)
                foreach (CEasyQuerySource source in sources)
                    AddSource(source);
        }

        //----------------------------------
        [DynamicMethod("Return asked data source from its ID","Data source id")]
        public CEasyQuerySource GetSourceFromId ( string strIdSource )
        {
            CEasyQuerySource source = null;
            m_dicSources.TryGetValue ( strIdSource, out source);
            if (source == null && strIdSource.Length == 0)
                if (m_dicSources.Count == 1)
                    source = m_dicSources.Values.ElementAt(0);
            return source;
        }

        //----------------------------------
        [DynamicMethod("Return asked data source from its name", "Data source name")]
        public CEasyQuerySource GetSourceFromName(string strNomSource)
        {
            string strUp = strNomSource.ToUpper();
            foreach (CEasyQuerySource src in m_dicSources.Values)
                if (src.SourceName.ToUpper() == strUp)
                    return src;
            return null;
        }


        //----------------------------------
        public IEnumerable<CEasyQuerySource> Sources
        {
            get
            {
                return m_dicSources.Values;
            }
        }

        //----------------------------------
        public void AddSource(CEasyQuerySource source)
        {
            if ( source != null )
                m_dicSources[source.SourceId] = source;
        }

        //----------------------------------
        public void ClearSources()
        {
            m_dicSources.Clear();
        }

        //----------------------------------
        public void RemoveSource(CEasyQuerySource source)
        {
            if ( source != null && m_dicSources.ContainsKey(source.SourceId) )
                m_dicSources.Remove ( source.SourceId );
        }

        //----------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //----------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            int nNb = m_dicSources.Count;
            serializer.TraiteInt(ref nNb);
            switch (serializer.Mode)
            {
                case ModeSerialisation.Ecriture:
                    foreach (KeyValuePair<string, CEasyQuerySource> kv in m_dicSources)
                    {
                        string strId = kv.Key;
                        CEasyQuerySource source = kv.Value;
                        result = serializer.TraiteObject<CEasyQuerySource>(ref source);
                        if (!result)
                            return result;
                    }
                    break;
                case ModeSerialisation.Lecture:
                    Dictionary<string, CEasyQuerySource> sources = new Dictionary<string, CEasyQuerySource>();
                    for (int n = 0; n < nNb; n++)
                    {
                        CEasyQuerySource source = null;
                        result = serializer.TraiteObject<CEasyQuerySource>(ref source);
                        if (!result)
                            return result;
                        sources[source.SourceId] = source;
                    }
                    m_dicSources = sources;
                    break;
            }
            return result;
        }

        //------------------------------------------------------------
        public void ClearCache(ITableDefinition table)
        {
            if (table != null)
            {
                CEasyQuerySource source = GetSourceFromId(table.SourceId);
                if (source != null)
                    source.ClearCache(table);
            }
        }

        //------------------------------------------------------------
        public DataTable GetTable(ITableDefinition tableDef)
        {
            if (tableDef == null)
                return null;
            CEasyQuerySource source = GetSourceFromId(tableDef.SourceId);
            if (source != null)
                return source.GetTable(tableDef);
            return null;
        }
    }
}
