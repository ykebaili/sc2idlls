using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionLongitudeToString : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionLongitudeToString()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"LongitudeToString", 
				typeof(string),
				I.TT(GetType(), "LongitudeToString(value, [decimals])\n Convert a decimal longitude value to a string (° min sec)|20093"),
				CInfo2iExpression.c_categorieMathematiques );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(double)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(double)),
                new CInfoUnParametreExpression(I.T("decimals|20095"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
                return new CTypeResultatExpression(typeof(string), false);
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				object val = valeursParametres[0];
                int nPrec = 5;
                if ( valeursParametres.Length > 1 )
                    nPrec = Convert.ToInt32(valeursParametres[1]);
                double fComplet = Convert.ToDouble(val);
                double fVal = Math.Abs(fComplet);
                string strRetour = "";
                double fDeg = (int)fVal;
                double fMin = (int)((fVal - fDeg) * 60.0);
                double fSec = (fVal - fDeg - fMin / 60.0) * 3600.0;
                strRetour = ((int)fDeg).ToString()+"°";
                strRetour += ((int)fMin).ToString()+"'";
                strRetour += fSec.ToString("#.".PadRight(nPrec+2, '#'))+"''";
                strRetour += fComplet < 0 ? I.T("W|20096") : I.T("E|20097");
                result.Data = strRetour;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameter of the function 'LongitudeToString' is incorrect|20094"));
			}
			return result;
		}
	}
}
