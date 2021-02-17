using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionLatitudeToString : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
        public C2iExpressionLatitudeToString()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"LatitudeToString", 
				typeof(string),
                I.TT(GetType(), "LatitudeToString(value, [decimals])\n Convert a decimal latitude value to a string (° min sec)|20100"),
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
                strRetour += fComplet < 0 ? I.T("S|20098") : I.T("N|20099");
                result.Data = strRetour;
			}
			catch
			{
				result.EmpileErreur(I.T("The parameter of the function 'LatitudeToString' is incorrect|20101"));
			}
			return result;
		}
	}
}
