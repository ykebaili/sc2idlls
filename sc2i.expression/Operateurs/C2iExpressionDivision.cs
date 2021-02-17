using System;
using System.Collections;

using sc2i.common;
using sc2i.common.unites;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionDivision : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDivision()
		{
		}

        /// //////////////////////////////////////////
        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = base.GetInfos();
            Type tpFinal = typeof(double);
            if (Parametres.Count >= 2)
            {
                CTypeResultatExpression tpParametre0 = Parametres2i[0].TypeDonnee;
                CTypeResultatExpression tpParametre1 = Parametres2i[1].TypeDonnee;

                if (tpParametre0 != null && tpParametre1 != null)
                {
                    Type tp1, tp2;
                    tp1 = tpParametre0.TypeDotNetNatif;
                    tp2 = tpParametre1.TypeDotNetNatif;
                    if (typeof(CValeurUnite).IsAssignableFrom(tp1) || typeof(CValeurUnite).IsAssignableFrom(tp2))
                        tpFinal = typeof(CValeurUnite);
                }
            }
            info.TypeDonnee = new CTypeResultatExpression(tpFinal, false);
            return info;
        }

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(1, "/", typeof(double), I.TT(GetType(), "Division operator|259"), CInfo2iExpression.c_categorieMathematiques);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(int)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(double)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(double)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(double)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(double)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(int)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(CValeurUnite)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(CValeurUnite)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(CValeurUnite)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(double)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Left operand|20042"), typeof(double)),
                new CInfoUnParametreExpression(I.T("Right operand|20043"), typeof(CValeurUnite)));

			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			//Tente la conversion en double
			try
			{
                if (valeursParametres[0] is CValeurUnite)
                {
                    if (valeursParametres[1] is CValeurUnite)
                    {
                        result.Data = (CValeurUnite)valeursParametres[0] / (CValeurUnite)valeursParametres[1];
                        return result;
                    }
                    try
                    {
                        double f = Convert.ToDouble(valeursParametres[1]);
                        result.Data = (CValeurUnite)valeursParametres[0] / f;
                        return result;
                    }
                    catch { }
                }
                if (valeursParametres[1] is CValeurUnite)
                {
                    try
                    {
                        double f = Convert.ToDouble(valeursParametres[0]);
                        result.Data = f/(CValeurUnite)valeursParametres[1];
                        return result;
                    }
                    catch { }
                }
				double f1, f2;
				f1 = Convert.ToDouble(valeursParametres[0]);
				f2 = Convert.ToDouble(valeursParametres[1]);
				if(  f2 == 0 )
				{
					result.EmpileErreur(I.T("Error : Division by 0 impossible|260"));
					return result;
				}
				result.Data = f1/f2;
				return result;
			}
			catch
			{
			}
			result.EmpileErreur(I.T("No overload of the function accept the paramters indicated|261"));
			return result;
		}

		
	}
}
