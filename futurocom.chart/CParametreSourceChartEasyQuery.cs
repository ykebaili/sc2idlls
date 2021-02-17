using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;
using futurocom.easyquery;

namespace futurocom.chart
{
    [AutoExec("Autoexec")]
    public class CParametreSourceChartEasyQuery : CParametreSourceChart
    {
         /// <summary>
        /// 
        /// </summary>
        private CEasyQuery m_easyQuery = new CEasyQuery();
        private CListeQuerySource m_listeSources = new CListeQuerySource();

        //-------------------------------------------------------
        public CParametreSourceChartEasyQuery(CChartSetup chartSetup)
            :base(chartSetup)
        {
        }

        //-------------------------------------------------------
        public static void Autoexec()
        {
            RegisterTypeParametre(typeof(CParametreSourceChartEasyQuery));
        }

        //-------------------------------------------------------
        public override string SourceTypeName
        {
            get { return I.T("Query|20003"); }
        }

        //-------------------------------------------------------
        public IEnumerable<CEasyQuerySource> QuerySources
        {
            get
            {
                if (m_listeSources == null)
                    m_listeSources = new CListeQuerySource();
                return m_listeSources.Sources;
            }
            set
            {
                m_listeSources = new CListeQuerySource();
                if (value != null)
                {
                    foreach (CEasyQuerySource src in value)
                        m_listeSources.AddSource(src);
                }
            }
        }

        //-------------------------------------------------------
        public override IContexteDonnee IContexteDonnee
        {
            get
            {
                return base.IContexteDonnee;
            }
            set
            {
                base.IContexteDonnee = value;
                if (m_easyQuery != null)
                    m_easyQuery.IContexteDonnee = value;
            }
        }

        
        //-------------------------------------------------------
        public CEasyQuery EasyQuery
        {
            get
            {
                if (m_easyQuery == null)
                    m_easyQuery = new CEasyQuery();
                m_easyQuery.Sources = QuerySources;
                m_easyQuery.IContexteDonnee = IContexteDonnee;
                m_easyQuery.ElementAVariablesExternes = ChartSetup;
                return m_easyQuery;
            }
            set
            {
                m_easyQuery = value;
            }
        }

        //-------------------------------------------------------
        public override CTypeResultatExpression TypeSource
        {
            get
            {
                return new CTypeResultatExpression(typeof(CEasyQuery), false);
            }
        }

        //-------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------
        public override object GetSource(CChartSetup chartSetup)
        {
            return EasyQuery;
        }

        //-------------------------------------------------------
        public override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            result = serializer.TraiteObject<CEasyQuery>(ref m_easyQuery);
            if (result)
                result = serializer.TraiteObject<CListeQuerySource>(ref m_listeSources);
            if (!result)
                return result;

            return result;
        }

        //----------------------------------------------------
        public override void ClearCache()
        {
            if (m_easyQuery != null)
                m_easyQuery.ClearCache();

        }

    }
}
