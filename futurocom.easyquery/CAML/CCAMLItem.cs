using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.common;

namespace futurocom.easyquery.CAML
{
    //--------------------------------------------
    [Serializable]
    public abstract class CCAMLItem : I2iSerializable
    {
        private C2iExpression m_condition = new C2iExpressionVrai();

        public abstract string Libelle { get; }
        protected abstract void MyGetXmlText(CEasyQuery query, StringBuilder bl, int nIndent);
        protected abstract void MyGetRowFilter(CEasyQuery query, StringBuilder bl);

        //--------------------------------------------
        protected virtual bool ShouldApply(CEasyQuery query)
        {
            if (m_condition == null)
                return true;
            if (m_condition is C2iExpressionVrai)
                return true;
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(query);
            CResultAErreur result = m_condition.Eval(ctx);
            if (!result)
                return true;
            if (CUtilBool.BoolFromObject(result.Data))
                return true;
            return false;
        }


        //--------------------------------------------
        public void GetXmlText(CEasyQuery query, StringBuilder bl, int nIndent)
        {
            if (ShouldApply(query))
                MyGetXmlText(query, bl, nIndent);
        }

        //--------------------------------------------
        public void GetRowFilter(CEasyQuery query, StringBuilder bl)
        {
            if (ShouldApply(query))
                MyGetRowFilter(query, bl);
        }

        //--------------------------------------------
        protected void AddIndent(StringBuilder bl, int nIndent)
        {
            for (int n = 0; n < nIndent; n++)
                bl.Append(' ');
        }

        //--------------------------------------------
        public C2iExpression Condition
        {
            get
            {
                return m_condition;
            }
            set
            {
                m_condition = value;
            }
        }

        //--------------------------------------------
        private int GetNumVersion()
        {
            return 0;
        }

        //--------------------------------------------
        public virtual CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;
            result = serializer.TraiteObject<C2iExpression>(ref m_condition);
            return result;
        }
    }
}
