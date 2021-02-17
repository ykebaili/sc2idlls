using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionBool : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionBool()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Bool", 
				typeof(bool),
				I.TT(GetType(), "Bool (value) Convert a value into Boolean.\n|157") +
	                I.T("If value is null: returns FALSE\n|304")+
	                I.T("If value is boolean: returns the boolean value\n|305")+
	                I.T("If value is int: int value = 1, returns TRUE. Other values, returns FALSE\n|306")+
	                I.T("If value is string: returns TRUE for 'true' 'vrai' 'yes' or 'oui' values, and FALSE for others\n|307"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			object val = valeursParametres[0];
			if ( val == null )
			{
				result.Data = false;
				return result;
			}
			if ( val is bool )
			{
				result.Data = val;
				return result;
			}
			string strVal = val.ToString();
            if (strVal == "1" || strVal.ToUpper() == "TRUE" || strVal.ToUpper() == "VRAI" || strVal.ToUpper() == "OUI" || strVal.ToUpper() == "YES")
			{
				result.Data = true;
				return result;
			}
			result.Data = false;
			return result;
		}

		
	}
}
