using System;
using System.Collections;

using sc2i.common;
using System.Threading;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSetVariable : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionSetVariable()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				6, 
				":=", 
				typeof(object),
				I.TT(GetType(), "Field:=value )\r\nAssign a value to a field or a variable|131"),
				CInfo2iExpression.c_categorieDivers );
			info.AddDefinitionParametres( new CInfoUnParametreExpression (I.T("Variable|20040"), typeof(object) ),
                new CInfoUnParametreExpression (I.T("Value|20039"), typeof(object ) ) );
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres.Count > 1 && Parametres[1] != null)
					return Parametres2i[1].TypeDonnee;
				return base.TypeDonnee;
			}
		}

		/// //////////////////////////////////////////
		private CResultAErreur GetSourceEtChampFinal(C2iExpression expression, CContexteEvaluationExpression ctx, ref object source, ref C2iExpressionChamp expressionChamp)
		{
			CResultAErreur result = CResultAErreur.True;
			expressionChamp = expression as C2iExpressionChamp;
            if (expressionChamp != null)
            {
                return result;
            }
			C2iExpressionObjet expObjet = expression as C2iExpressionObjet;
			if (expObjet == null)
				result.EmpileErreur(I.T("Impossible to affect a value to @1|133", expression.GetString()));
			result = expObjet.Parametres2i[0].Eval(ctx);
			if (!result || result == null)
				return result;
			ctx.PushObjetSource(result.Data, false);
			source = result.Data;
			result = GetSourceEtChampFinal(expObjet.Parametres2i[1], ctx, ref source, ref expressionChamp);
			ctx.PopObjetSource(false);
			return result;
		}


		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval(CContexteEvaluationExpression ctx)
		{
			CResultAErreur result =  CResultAErreur.True;
            try
            {
                C2iExpressionChamp champFinal = null;
                object objSource = ctx.ObjetSource;
                result = GetSourceEtChampFinal(Parametres2i[0], ctx, ref objSource, ref champFinal);
                if (!result)
                    return result;
                if (champFinal == null)
                {
                    result.EmpileErreur(I.T("Impossible to affect a value to @1|133", Parametres2i[0].ToString()));
                    return result;
                }

                result = Parametres2i[1].Eval(ctx);
                if (!result)
                    return result;
                object valeur = result.Data;
                CDefinitionProprieteDynamiqueVariableFormule propFormule = champFinal.DefinitionPropriete as CDefinitionProprieteDynamiqueVariableFormule;
                if (propFormule != null)
                    ctx.SetValeurVariable(propFormule, valeur);
                else
                {
                    if (objSource == null)
                        return result;
                    SynchronizationContext c = ctx.SynchronizationContext;

                    if ( c != null )
                        c.Post(x => CInterpreteurProprieteDynamique.SetValue(objSource, champFinal.DefinitionPropriete.NomPropriete, valeur), null);
                    else
                        result = CInterpreteurProprieteDynamique.SetValue(objSource, champFinal.DefinitionPropriete.NomPropriete, valeur);
                }
                if (result)
                {
                    result.Data = valeur;
                    if (objSource != null)
                        ctx.Cache.ResetCache(objSource);
                }

            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
			return result;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			try 
			{
				C2iExpressionChamp champ = (C2iExpressionChamp)Parametres[0];
				if ( champ.DefinitionPropriete == null || 
					champ.DefinitionPropriete.IsReadOnly )
				{
					result.EmpileErreur(I.T("Impossible to affect a value to @1|133",champ.DefinitionPropriete==null?"null":champ.DefinitionPropriete.Nom));
					return result;
				}
				ctx.SetValeurVariable ( champ.DefinitionPropriete, valeursParametres[1] );
				result.Data = valeursParametres[1];
			}
			catch
			{
				result.EmpileErreur(I.T("Error during the variable creation|132"));
			}
			return result;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = base.VerifieParametres();
			if ( !result )
				return result;
			C2iExpressionChamp expressionChamp = Parametres2i[0] as C2iExpressionChamp;
			if (expressionChamp == null)
			{
				C2iExpressionObjet expressionObjet = Parametres2i[0] as C2iExpressionObjet;
				while (expressionObjet != null && expressionChamp == null)
				{
					expressionChamp = expressionObjet.Parametres2i[1] as C2iExpressionChamp;
					expressionObjet = expressionObjet.Parametres2i[1] as C2iExpressionObjet;
				}
			}
			if (!(expressionChamp is C2iExpressionChamp))
			{
				result.EmpileErreur(I.T("The 'Set' first parameter must be a variable|134"));
				return result;
			}
			return result;
		}


		


		
	}
}
