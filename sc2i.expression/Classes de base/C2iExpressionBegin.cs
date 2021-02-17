using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionBegin : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionBegin()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Begin", 
				typeof(object), 
				"Begin ( Exp1; exp2; exp3; ... )\r\nExecute séquentiellement un groupe de formules. retoure par défaut le résultat de la dernière formule",
				CInfo2iExpression.c_categorieDivers );
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres( true, new CInfoUnParametreExpression ( I.T("Statement|20037"),  typeof(object))));
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			//Variable
			return -1;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{

				if ( Parametres.Count > 0 && Parametres[Parametres.Count-1] != null)
				{
					return Parametres2i[Parametres.Count-1].TypeDonnee;
				}
				return new CTypeResultatExpression ( typeof(object), false);
			}
		}


		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				result.Data = valeursParametres[valeursParametres.Length-1];
			}
			catch
			{
				result.EmpileErreur(I.T("Error while 'begin' evaluation |308"));
			}
			return result;
		}

		
	}
}
