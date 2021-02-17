using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionDate : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDate()
		{
		}

	

		/// //////////////////////////////////////////
		protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Date", 
				typeof(DateTime),
				I.TT(GetType(), "Date(text)\nConvert a text in date. The text can be with format YYYY/MM/DD. If the parameter is a type dates, the date returns (without hour) corresponding|165"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(string)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20040"), typeof(DateTime)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20040"), typeof(CDateTimeEx)));
  			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( valeursParametres[0] is DateTime )
			{
				result.Data = ((DateTime)valeursParametres[0]).Date;
			}
			else if (valeursParametres[0] is CDateTimeEx )
			{
				result.Data = ((CDateTimeEx)valeursParametres[0]).DateTimeValue.Date;
			}
			else
			{
				try
				{
					result.Data = DateTime.Parse(valeursParametres[0].ToString());
				}
				catch
				{
					string[] strZones = valeursParametres[0].ToString().Split('/');
					if ( strZones.Length != 3 )
						strZones = valeursParametres[0].ToString().Split('-');
					if ( strZones.Length == 3 )
					{
						int nYear, nMonth, nDay;
						try
						{
							nYear = Int32.Parse ( strZones[0] );
							nMonth = Int32.Parse(strZones[1]);
							nDay = Int32.Parse(strZones[2]);
							DateTime dt = new DateTime ( nYear, nMonth, nDay );
							result.Data = dt;
						}
						catch ( Exception e )
						{
							result.EmpileErreur(new CErreurException(e));
						}
					}
					if ( !result )
						result.EmpileErreur(I.T("Error during the date conversion: the text '@1' is not convertible to a date|166", valeursParametres[0].ToString()));
				}
			}
			return result;
		}

		
	}
}
