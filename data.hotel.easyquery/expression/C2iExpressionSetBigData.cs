using System;
using System.Collections;

using sc2i.common;
using data.hotel.client;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionSetBigData.
	/// </summary>
	[Serializable]
    [AutoExec("Autoexec")]
	public class C2iExpressionSetBigData : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSetBigData()
		{
		}

        /// //////////////////////////////////////////
        public static void Autoexec()
        {
            CAllocateur2iExpression.Register2iExpression(new C2iExpressionSetBigData().IdExpression,
                typeof(C2iExpressionSetBigData));
        }

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"SetBigData", 
				typeof(bool),
				I.TT(GetType(), "SetBigData(serverIp, serverPort, table, field, entity, date, value)\nStores a value in a big data database|20010"), 
		        CInfo2iExpression.c_categorieDivers);
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("ServerIp|20011"), typeof(string)),
                new CInfoUnParametreExpression(I.T("ServerPort|20012"), typeof(int)),
                new CInfoUnParametreExpression(I.T("Table|20013"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Field|20014"), typeof(string)),
                new CInfoUnParametreExpression(I.T("EntityId|20015"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Date|20016"), typeof(DateTime)),
                new CInfoUnParametreExpression(I.T("Value|20017"), typeof(double)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CDataHotelClient client = new CDataHotelClient(
                    (string)valeursParametres[0], 
                    (int)valeursParametres[1]);
                result = client.GetRoomServer().SendData(
                    (string)valeursParametres[2], 
                    (string)valeursParametres[3],
                    (string)valeursParametres[4],
                    (DateTime)valeursParametres[5],
                    Convert.ToDouble(valeursParametres[6]));
                result.Data = result.Result;
			}
			catch
			{
				result.EmpileErreur(I.T("Error during the evaluation of the function 'SetBigData'|20018"));
				return result;
			}
            return result;
		}
	}
}
