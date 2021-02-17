using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.common.unites;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionConvertUnit : C2iExpressionAnalysable
	{
        private string m_strUnitId = "";

		/// //////////////////////////////////////////
		public C2iExpressionConvertUnit()
		{
		}

        /// //////////////////////////////////////////
        public C2iExpressionConvertUnit(string strUnitId)
		{
            m_strUnitId = strUnitId;
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				":"+m_strUnitId,
				typeof(CValeurUnite),
				I.TT(GetType(), "@1(value) \nConvert value into @1|20116"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(double)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(CValeurUnite)));
            return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
            CValeurUnite valeur = valeursParametres[0] as CValeurUnite;
            if (valeur == null)
            {
                try
                {
                    double fVal = Convert.ToDouble(valeursParametres[0]);
                    result.Data = new CValeurUnite(fVal, m_strUnitId);
                    return result;
                }
                catch { }
            }
            try{
                result.Data = valeur.ConvertTo ( m_strUnitId );
            }
            catch ( Exception e )
            {
                result.EmpileErreur ( new CErreurException(e));
                result.EmpileErreur(I.T("Error during unit conversion|20115"));
            }
            return result;
		}

        /// //////////////////////////////////////////
        private int GetNumVersion()
        {
            return 0;
        }

        /// //////////////////////////////////////////
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (result)
                serializer.TraiteString(ref m_strUnitId);
            return result;
        }

		
	}
}
