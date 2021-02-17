using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Formate la valeur de Hours en double avec la chaine Format
    /// Formats possibles
    /// "YY"
    /// "MM"
    /// "dd"
    /// "HH"
    /// "mm"
    /// "ss"
    /// Toutes combinaiason de format est possible. Exemple "YY années, MM mois - DD jours et HH:mm:ss"
	/// </summary>
	[Serializable]
	public class C2iExpressionHoursToString : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
        public C2iExpressionHoursToString()
		{
		}


		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"HoursToString", 
				typeof(string),
				I.TT(GetType(), "HoursToString(Hours, Format, NbDecimals)\nReturns a formated string representing the time of the Hours parameter value.\nHours is a double value|302"),
				CInfo2iExpression.c_categorieDate );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Hours|20051"), typeof(double)),
                new CInfoUnParametreExpression(I.T("Format|20049"), typeof(string)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Hours|20051"), typeof(double)),
                new CInfoUnParametreExpression(I.T("Format|20049"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Decimals number|20061"), typeof(int)));
			return info;
		}

		//-------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="valeursParametres"></param>
        /// <returns></returns>
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				double fHeures = Convert.ToDouble(valeursParametres[0]);
				bool bNegatif = false;
				if (fHeures < 0)
				{
					bNegatif = true;
					fHeures = Math.Abs(fHeures);
				}
                string strFormat = valeursParametres[1].ToString();
                int nNbDecimales = 999;
                if(valeursParametres.Length > 2 && valeursParametres[2] != null)
                    nNbDecimales = Convert.ToInt32(valeursParametres[2]);

                double fValYY = 0,
                       fValMM = 0,
                       fValdd = 0,
                       fValHH = 0,
                       fValmm = 0,
                       fValss = 0;

                if (strFormat.Contains("YY"))
                {
                    fValYY = fHeures / (24 * 365.25);
                    fHeures = fHeures % (24 * 365.25);
                }
                if (strFormat.Contains("MM"))
                {
                    fValYY = (int)fValYY;
                    fValMM = fHeures / (24 * 30.4375);
                    fHeures = fHeures % (24 * 30.4375);
                }
                if (strFormat.Contains("dd"))
                {
                    fValMM = (int)fValMM;
                    fValYY = (int)fValYY;
                    fValdd = fHeures / 24;
                    fHeures = fHeures % 24;
                }
                if (strFormat.Contains("HH"))
                {
                    fValdd = (int)fValdd;
                    fValMM = (int)fValMM;
                    fValYY = (int)fValYY;
                    fValHH = fHeures;
                    fHeures = (fHeures - (int)fHeures);
                }
                if (strFormat.Contains("mm"))
                {
                    fValHH = (int)fValHH;
                    fValdd = (int)fValdd;
                    fValMM = (int)fValMM;
                    fValYY = (int)fValYY;
                    fValmm = fHeures * 60;
                    fHeures = (fValmm - (int)fValmm) / 60;
                }
                if (strFormat.Contains("ss"))
                {
                    fValmm = (int)fValmm;
                    fValHH = (int)fValHH;
                    fValdd = (int)fValdd;
                    fValMM = (int)fValMM;
                    fValYY = (int)fValYY;
                    fValss = (int)(fHeures * 3600);
                }
                
                // Arrondi au nompbre décimales demandées
                if (nNbDecimales != 999)
                {
                    fValYY = Sc2iMath.RoundUp(fValYY, nNbDecimales);
                    fValMM = Sc2iMath.RoundUp(fValMM, nNbDecimales);
                    fValdd = Sc2iMath.RoundUp(fValdd, nNbDecimales);
                    fValHH = Sc2iMath.RoundUp(fValHH, nNbDecimales);
                    fValmm = Sc2iMath.RoundUp(fValmm, nNbDecimales);
                    fValss = Sc2iMath.RoundUp(fValss, nNbDecimales);
                }

                if (fValss >= 60.0 && strFormat.Contains("mm"))
                {
                    fValmm += 1;
                    fValss -= 60;
                }
                if (fValmm >= 60.0 && strFormat.Contains("HH"))
                {
                    fValHH += 1;
                    fValmm -= 60.0;
                }
                if (fValHH >= 24.0 && strFormat.Contains("dd"))
                {
                    fValdd += 1;
                    fValHH -= 24;
                }
                if (fValdd >= 31.0 && strFormat.Contains("MM"))
                {
                    fValMM += 1;
                    fValdd -= 31;
                }
                if (fValMM >= 12.0 && strFormat.Contains("YY"))
                {
                    fValYY += 1;
                    fValMM -= 12;
                }

                // Remplace les caractères de formatage par leur valeur
                strFormat = strFormat.Replace("YY", fValYY.ToString());
                strFormat = strFormat.Replace("MM", fValMM.ToString());
                strFormat = strFormat.Replace("dd", fValdd.ToString());
                strFormat = strFormat.Replace("HH", fValHH.ToString());
                string strTemp = fValmm.ToString("0#");
                strFormat = strFormat.Replace("mm", strTemp);
                strTemp = fValss.ToString("0#");
                strFormat = strFormat.Replace("ss", strTemp);
				if (bNegatif)
					strFormat = "-" + strFormat;
                result.Data = strFormat;
			}
			catch
			{
                result.EmpileErreur(I.T("The parameters of the function 'HoursToString' are incorrect|303"));
			}
			return result;
		}

           

		
	}
}
