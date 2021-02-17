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
	public class C2iExpressionMultiplie : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionMultiplie()
		{
		}

        /// //////////////////////////////////////////
        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = base.GetInfos();
            if ( Parametres.Count >= 2 )
			{
                CTypeResultatExpression tpParametre0 = Parametres2i[0].TypeDonnee;
                CTypeResultatExpression tpParametre1 = Parametres2i[1].TypeDonnee;

				if ( tpParametre0 != null && tpParametre1 != null )
				{
					Type tp1, tp2;
					tp1 = tpParametre0.TypeDotNetNatif;
					tp2 = tpParametre1.TypeDotNetNatif;
					if ( tp1 == tp2 )
						info.TypeDonnee = new CTypeResultatExpression(tp1, false);
					else if ( typeof(int).IsAssignableFrom ( tp1 ) && typeof(int).IsAssignableFrom ( tp2 ) )
						info.TypeDonnee = new CTypeResultatExpression(typeof(int), false);
                    else if (typeof(CValeurUnite).IsAssignableFrom(tp1) || typeof(CValeurUnite).IsAssignableFrom(tp2))
                        info.TypeDonnee = new CTypeResultatExpression(typeof(CValeurUnite), false);
					else
						info.TypeDonnee = new CTypeResultatExpression(typeof(double), false);
				}
			}
            return info;
        }

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(1, "*", typeof(double), I.TT(GetType(), "Operator multiplication|264"), CInfo2iExpression.c_categorieMathematiques);
			
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
			if ( valeursParametres[0] is int && valeursParametres[1] is int )
			{
				result.Data = (int)valeursParametres[0]*(int)valeursParametres[1];
				return result;
			}
            if (valeursParametres[0] is CValeurUnite)
            {
                if (valeursParametres[1] is CValeurUnite)
                {
                    result.Data = (CValeurUnite)valeursParametres[0] * (CValeurUnite)valeursParametres[1];
                    return result;
                }
                try
                {
                    double f = Convert.ToDouble(valeursParametres[1]);
                    result.Data = (CValeurUnite)valeursParametres[0] * f;
                    return result;
                }
                catch { }
            }

            if (valeursParametres[1] is CValeurUnite)
            {
                try
                {
                    double f = Convert.ToDouble(valeursParametres[0]);
                    result.Data = (CValeurUnite)valeursParametres[1] * f;
                    return result;
                }
                catch { }
            }
			//Tente la conversion en double
			try
			{
				double f1, f2;
				f1 = Convert.ToDouble(valeursParametres[0]);
				f2 = Convert.ToDouble(valeursParametres[1]);
				result.Data = f1*f2;
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
