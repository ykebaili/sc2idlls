using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Retourne le nom Système du type d'un objet ou 
	/// convertit un nom convivial en nom système
	/// </summary>
	[Serializable]
	public class C2iExpressionGetTypeInterne : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionGetTypeInterne()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"InternalType", 
				typeof(string),
				I.TT(GetType(), "InternalType (value)\nReturn the type system name of the an object or converts a convivial type name to system type name|176"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value[20039"), typeof(object)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( valeursParametres[0] is String )
			{
				Type tp = DynamicClassAttribute.GetTypeFromNomConvivial ( (string)valeursParametres[0] );
				if ( tp != null )
				{
					result.Data = tp.ToString();
					return result;
				}
				tp = CActivatorSurChaine.GetType((string)valeursParametres[0] );
				if ( tp != null )
				{
					result.Data = tp.ToString();
					return result;
				}
			}
			else
			{
				if ( valeursParametres[0] != null )
				{
					result.Data = valeursParametres[0].GetType().ToString();
					return result;
				}
			}
			result.Data = "";
			return result;

		}

		
	}
}
