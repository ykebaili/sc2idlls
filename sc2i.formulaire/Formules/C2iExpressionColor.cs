using System;
using System.Collections;

using sc2i.common;
using System.Drawing;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionColor : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionColor()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Color", 
				typeof(Color),
				I.TT(GetType(), "Color ([A;] R; G; B) returns a color\nA is the Alpha value (optional), R, G and B are the Red, Green, and Blue values of the color|20001"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("ALpha|20077"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Red value|20078"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Green value|20079"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Blue|20080"), typeof(int)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Red value|20078"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Green value|20079"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Blue|20080"), typeof(int)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
            try
            {
                if (valeursParametres.Length == 4)
                {
                    result.Data = Color.FromArgb(
                        (int)valeursParametres[0],
                        (int)valeursParametres[1],
                        (int)valeursParametres[2],
                        (int)valeursParametres[3]);

                }
                else if (valeursParametres.Length == 3)
                {
                    result.Data = Color.FromArgb(
                        (int)valeursParametres[0],
                        (int)valeursParametres[1],
                        (int)valeursParametres[2]);
                }
                else
                    result.EmpileErreur(I.T("Error in Color parameters|20002"));
            }
            catch
            {
                result.EmpileErreur(I.T("Error in Color parameters|20002"));
            }
			return result;
		}

		
	}
}
