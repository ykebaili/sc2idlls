using System;
using System.Collections;
using System.Reflection;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionTexte : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionTexte()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Text", 
				typeof(string),
				I.TT(GetType(), "Text (value [, format]) \nConvert the argument in type 'text' with a possible formattage|174"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Format|20049"), typeof(string)));
            return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			string strFormat = "";
			if ( Parametres.Count == 2 )
				strFormat = valeursParametres[1].ToString();
			object objet = valeursParametres[0];
			if ( objet == null )
				result.Data = null;
			else
			{
				if(  strFormat != "" )
				{
					MethodInfo method = objet.GetType().GetMethod ( "ToString", new Type[]{typeof(string)} );
					if ( method != null )
						result.Data = method.Invoke ( objet, new object[]{strFormat} );
					else
						result.Data = objet.ToString();
				}
				else
					result.Data = objet.ToString();
			}
			return result;
		}

		
	}
}
