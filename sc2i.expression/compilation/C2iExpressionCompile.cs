using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionCompile
	/// </summary>
	[Serializable]
	public class C2iExpressionCompile : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionCompile()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression(0, "Compile", typeof(C2iExpression), I.TT(GetType(), "Compile(source, formula)\nCompiles formula based on source object and return formula object|296"), CInfo2iExpression.c_categorieDivers);
            info.AddDefinitionParametres(
                new CInfoUnParametreExpression("Source object", typeof(object)),
                new CInfoUnParametreExpression("Formula text", typeof(string)));
			return info;
		}

		/// //////////////////////////////////////////
        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
        {
            CResultAErreur result = CResultAErreur.True;

            try
            {
                CContexteAnalyse2iExpression ctxAn = new CContexteAnalyse2iExpression(
                    new CFournisseurGeneriqueProprietesDynamiques(),
                    new CObjetPourSousProprietes(valeursParametres[0]));
                CAnalyseurSyntaxiqueExpression an = new CAnalyseurSyntaxiqueExpression(ctxAn);
                result = an.AnalyseChaine((string)valeursParametres[1]);
                return result;
            }
            catch
            {
                result.EmpileErreur("Error while compiling '" + (
                    valeursParametres[1] != null ? valeursParametres[1].ToString() : "NULL") + "'");
                return result;
            }
            return result;
        }

		
	}
}
