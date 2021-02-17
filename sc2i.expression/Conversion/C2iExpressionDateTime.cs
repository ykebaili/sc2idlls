    using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionDateTime : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionDateTime()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"DateTime", 
				typeof(DateTime),
				I.TT(GetType(), "Date(text)\nConvert a text in date time. The text can be with format YYYY/MM/DD HH:mm:ss. |20006"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(string)));
            
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				result.Data = DateTime.Parse(valeursParametres[0].ToString());
			}
			catch
			{
				string [] strZonesDateHeure = valeursParametres[0].ToString().Split(' ');
				string strDate = "";
				string strHeure = "";
				if ( strZonesDateHeure.Length > 0 )
					strDate = strZonesDateHeure[0];
				if ( strZonesDateHeure.Length > 1 )
					strHeure = strZonesDateHeure[1];
				string[] strZones = strDate.Split('/');
				if ( strZones.Length != 3 )
					strZones = strDate.Split('-');
				if ( strZones.Length == 3 )
				{
					int nYear, nMonth, nDay;
					try
					{
						nYear = Int32.Parse ( strZones[0] );
						nMonth = Int32.Parse(strZones[1]);
						nDay = Int32.Parse(strZones[2]);
						int nHeure = 0;
						int nMinute = 0;
						int nSeconde = 0;
						strZones = strHeure.Split(':');
						if ( strZones.Length > 0 )
							nHeure = Int32.Parse ( strZones[0] );
						if ( strZones.Length > 1 )
							nMinute = Int32.Parse ( strZones[1] );
						if ( strZones.Length > 2 )
							nSeconde = Int32.Parse ( strZones[2] );
						DateTime dt = new DateTime ( nYear, nMonth, nDay, nHeure, nMinute, nSeconde );
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
			return result;
		}

		
	}
}
