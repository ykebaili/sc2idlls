using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace futurocom.chart
{
    public class CDonneesDeChart : I2iSerializable, IObjetAIContexteDonnee
    {
        //Liste des sources pouvant être utilisées dans les fournisseurs de valeurs de série
        //Une source peut être une formule, un table, ...
        private List<CParametreSourceChart> m_listeSourcesFV = new List<CParametreSourceChart>();

        [NonSerialized]
        private IContexteDonnee m_contexteDonnee = null;

        //-------------------------------------------------
        private CChartSetup m_chartSetup = null;
        
        //-------------------------------------------------
        public CDonneesDeChart(CChartSetup chartSetup)
        {
            m_chartSetup = chartSetup;
        }

        //-------------------------------------------------
        public CChartSetup ChartSetup
        {
            get
            {
                return m_chartSetup;
            }
        }

        //-------------------------------------------------
        public IContexteDonnee IContexteDonnee
        {
            get
            {
                return m_contexteDonnee;
            }
            set
            {
                m_contexteDonnee = value;
                foreach (CParametreSourceChart p in m_listeSourcesFV)
                    p.IContexteDonnee = value;
            }
        }

       
        //-------------------------------------------------
        public IEnumerable<CParametreSourceChart> ParametresSourceFV
        {
            get
            {
                if (ChartSetup != null)
                    foreach (CParametreSourceChart p in m_listeSourcesFV)
                        p.IContexteDonnee = ChartSetup.IContexteDonnee;
                return m_listeSourcesFV.AsReadOnly();
            }
            set
            {
                m_listeSourcesFV.Clear();
                if (value != null)
                    m_listeSourcesFV.AddRange(value);
            }
        }

        //-------------------------------------------------
        public void AddSourceFV(CParametreSourceChart parametre)
        {
            CParametreSourceChart pOld = m_listeSourcesFV.FirstOrDefault(p => p.SourceId == parametre.SourceId);
            if ( pOld != null )
                m_listeSourcesFV.Remove ( pOld );
            m_listeSourcesFV.Add(parametre);
        }

        //-------------------------------------------------
        public void RemoveSourceFV(CParametreSourceChart parametre)
        {
            m_listeSourcesFV.Remove(parametre);
        }


        //-------------------------------------------------
        public CParametreSourceChart GetSourceFV(string strIdSource)
        {
            foreach (CParametreSourceChart parametre in m_listeSourcesFV)
                if (parametre.SourceId == strIdSource)
                    return parametre;
            return null;
        }

        //-------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------
        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteListe<CParametreSourceChart>(m_listeSourcesFV, ChartSetup);
            if (!result) 
                return result;
            return result;
        }


        //----------------------------------------------------
        public void ClearCache()
        {
            foreach (CParametreSourceChart p in ParametresSourceFV)
                p.ClearCache();
        }
    }
}
