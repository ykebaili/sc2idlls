using System;
using System.Collections;
using System.Reflection;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.expression;

using sc2i.multitiers.client;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Lit une données (texte) dans le registre de sidonie
	/// </summary>
	[Serializable]
	[AutoExec("Autoexec")]
	public class C2iExpressionReadRegistre : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionReadRegistre()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateur2iExpression.Register2iExpression ( new C2iExpressionReadRegistre().IdExpression,
				typeof(C2iExpressionReadRegistre) );
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "ReadRegistry", 
				new CTypeResultatExpression(typeof(string), false),
                I.TT(GetType(), "Reads a value in the SC2I register(key to read, value by default) \n Return the text value of the key in the register. If the key doesn't exist, the default value is returned|226"),
				CInfo2iExpression.c_categorieDivers);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(string), typeof(string) ) );
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 2;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			CResultAErreur result = CResultAErreur.True;

			try
			{
				IDatabaseRegistre registre = (IDatabaseRegistre)C2iFactory.GetNew2iObjetServeur(typeof(IDatabaseRegistre), CSessionClient.GetSessionUnique().IdSession);
				result.Data = registre.GetValeurString(listeParametres[0].ToString(), listeParametres[1].ToString());
				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				return result;
			}
		}
	}
}
