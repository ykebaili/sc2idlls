using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace futurocom.chart
{
    [AutoExec("Autoexec")]
    public class CParametreSourceChartFormule : CParametreSourceChart
    {
         /// <summary>
        /// 
        /// </summary>
        private C2iExpression m_formule = null;

        //-------------------------------------------------------
        public CParametreSourceChartFormule(CChartSetup chartSetup)
            :base(chartSetup)
        {
        }

        //-------------------------------------------------------
        public static void Autoexec()
        {
            RegisterTypeParametre(typeof(CParametreSourceChartFormule));
        }

        //-------------------------------------------------------
        public override string SourceTypeName
        {
            get { return I.T("Formula|20000"); }
        }

        
        //-------------------------------------------------------
        public C2iExpression Formule
        {
            get
            {
                return m_formule;
            }
            set
            {
                m_formule = value;
            }
        }

        //-------------------------------------------------------
        public override CTypeResultatExpression TypeSource
        {
            get
            {
                if (m_formule != null)
                    return m_formule.TypeDonnee;
                return new CTypeResultatExpression(typeof(string), false);
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
            if (m_formule == null)
                return null;
            //Trouve la source
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(chartSetup);
            CResultAErreur result = m_formule.Eval(ctx);
            if (result)
                return result.Data;
            return null;
        }

        //-------------------------------------------------------
        public override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            result = serializer.TraiteObject<C2iExpression>(ref m_formule);
            if (!result)
                return result;

            return result;
        }

        //----------------------------------------------------
        public override void ClearCache()
        {
        }

    }
}
