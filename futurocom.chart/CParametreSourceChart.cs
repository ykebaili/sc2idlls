using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.expression;

namespace futurocom.chart
{
    /// <summary>
    /// Paramètre de source pour les fournisseurs de valeurs de série
    /// (FV = Fournisseur Valeur)
    /// </summary>
    public abstract class CParametreSourceChart : 
        I2iSerializable, 
        I2iCloneableAvecTraitementApresClonage,
        IObjetAIContexteDonnee
    {
        private static List<Type> m_listeTypesConnus = new List<Type>();
        private CChartSetup m_chartSetup;

        private string m_strSourceName = "";
        private string m_strId = "";

        [NonSerialized]
        private IContexteDonnee m_contexteDonnee = null;

        //-------------------------------------------------------
        public CParametreSourceChart(CChartSetup chartSetup)
        {
            m_strId = Guid.NewGuid().ToString();
            SourceName = SourceTypeName;
            m_chartSetup = chartSetup; ;
        }



        //-------------------------------------------------------
        public static void RegisterTypeParametre(Type tp)
        {
            if (!m_listeTypesConnus.Contains(tp))
                m_listeTypesConnus.Add(tp);
        }

        //-------------------------------------------------------
        public CChartSetup ChartSetup
        {
            get
            {
                return m_chartSetup;
            }
        }

        //-------------------------------------------------------
        public static IEnumerable<Type> TypesSourcesConnus
        {
            get
            {
                return m_listeTypesConnus.AsReadOnly();
            }
        }

        //-------------------------------------------------------
        public abstract string SourceTypeName{get;}

        //-------------------------------------------------------
        public virtual string SourceName
        {
            get
            {
                return m_strSourceName;
            }
            set
            {
                m_strSourceName = value;
            }
        }

        //-------------------------------------------------------
        public virtual string SourceId
        {
            get
            {
                return m_strId;
            }
        }

        //-------------------------------------------------------
        public CParametreSourceChart GetCloneAvecMemeId()
        {
            CParametreSourceChart p = CCloner2iSerializable.Clone(this, null, new object[]{m_chartSetup}) as CParametreSourceChart;
            p.m_strId = m_strId;
            return p;
        }

        //-------------------------------------------------------
        public abstract object GetSource(CChartSetup chart);

        //-------------------------------------------------------
        public abstract CResultAErreur MySerialize(C2iSerializer serializer);

        //-------------------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //-------------------------------------------------------
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            serializer.TraiteString(ref m_strId);
            serializer.TraiteString(ref m_strSourceName);

            result = MySerialize(serializer);
            if (!result)
                return result;

            if ( serializer.IsForClone)
                m_strId = Guid.NewGuid().ToString();

            return result;
        }

        //-------------------------------------------------------
        public void TraiteApresClonage(I2iSerializable source)
        {
            m_strId = Guid.NewGuid().ToString();
            IContexteDonnee = ((CParametreSourceChart)source).IContexteDonnee;
        }

        //-------------------------------------------------------
        public abstract CTypeResultatExpression TypeSource {get;}



        #region IObjetAIContexteDonnee Membres

        public virtual IContexteDonnee IContexteDonnee
        {
            get { return m_contexteDonnee; }
            set
            {
                m_contexteDonnee = value;
            }
        }

        #endregion

        public abstract void ClearCache();
    }
}
