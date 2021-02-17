using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionSelect : C2iExpressionObjetNeedTypeParent
	{
		/// //////////////////////////////////////////
		public C2iExpressionSelect()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "Select", new CTypeResultatExpression(typeof(object), true), ".Select(Condition)\nRetourne la liste des éléments satisfaisant la condition"+
                Environment.NewLine+
				I.TT(GetType(), ".Select(list)\nReturn the list of the elements whose indices belong to the list|226"), CInfo2iExpression.c_categorieGroupe);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new CTypeResultatExpression(typeof(bool), false)));
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(new CTypeResultatExpression(typeof(int), true) ) );
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( ObjetPourAnalyseSourceConnu != null )
					return ObjetPourAnalyseSourceConnu.TypeResultatExpression;
				return base.TypeDonnee;
			}
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression[] TypesObjetSourceAttendu
		{
			get
			{
				return new CTypeResultatExpression[]
					{
						new CTypeResultatExpression(typeof(object), true)
					};
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			return CResultAErreur.False;
		}

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			 CResultAErreur result = CResultAErreur.True;
			if ( !typeof(IEnumerable).IsAssignableFrom(ctx.ObjetSource.GetType()) )
			{
				result.EmpileErreur(I.T("The function select cannot apply to the @1 type|227",ctx.ObjetSource.GetType().ToString()));
				return result;
			}
			ArrayList lst = new ArrayList();

			if ( Parametres2i[0].TypeDonnee.TypeDotNetNatif == typeof(int) && 
				Parametres2i[0].TypeDonnee.IsArrayOfTypeNatif )
				//Evaluation depuis liste
			{
				IEnumerable lstSource = (IEnumerable)ctx.ObjetSource;
				bool bIsIList = typeof(IList).IsAssignableFrom(ctx.ObjetSource.GetType() );
				ArrayList lstParametres = new ArrayList();
				result = EvalParametres(ctx, lstParametres);
				if ( !result )
					return result;
				if ( !typeof(IEnumerable).IsAssignableFrom(lstParametres[0].GetType() ))
				{
					result.EmpileErreur(I.T("The function select has incorrect 'list' parameters|228"));
					return result;
				}
				foreach ( object objIndice in (IEnumerable)lstParametres[0] )
				{
					try
					{
						int nVal= Convert.ToInt32 ( objIndice );
						if ( bIsIList )
						{
							try
							{
								lst.Add(((IList)lstSource)[nVal]);
							}
							catch
							{
								//Indice hors limites
							}
						}
						else
						{
							int nPos = 0;
							foreach ( object valeur in lstSource )
							{
								if ( nPos == nVal )
								{
									lst.Add(valeur);break;
								}
								nPos++;
							}
						}
					}
					catch
					{
						result.EmpileErreur(I.T("Error in the 'select' list : One of the parameters isn't an integer|229"));
						return result;
					}
				}
			}
			else
			{
				//Evaluation sur condition
				int nIndex = 0;
				foreach ( object obj in (IEnumerable) ctx.ObjetSource )
				{
					try
					{
						ctx.PushObjetSource ( obj, true );
						result = Parametres2i[0].Eval ( ctx );
						ctx.PopObjetSource(true);
						if ( !result )
							return result;
						bool bResult = Convert.ToBoolean ( result.Data );
						if ( bResult )
							lst.Add ( obj );
						nIndex++;
					}
					catch {}
				}
			}
			result.Data = lst;
			return result;
		}

        public override CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource(CObjetPourSousProprietes objetSource)
        {
            return objetSource.GetObjetAnalyseElements();
        }


				
	}
}
