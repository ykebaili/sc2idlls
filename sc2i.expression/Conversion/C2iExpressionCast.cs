using System;
using System.Collections;
using System.Collections.Generic;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionCast : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionCast()
		{
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Cast", 
				typeof(object),
				I.T("Cast(object, type, bArray)\nConvert an object to the type requested (if possible). Type is a string which identifies the type, table indicates if the returned element an array of elements|160"),
				CInfo2iExpression.c_categorieConversion );
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Type|20038"), typeof(string)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Type|20038"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Array type|20041"), typeof(bool)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Value|20039"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Type|20038"), typeof(Type)),
                new CInfoUnParametreExpression(I.T("Array type|20041"), typeof(bool)));
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres.Count >= 2 )
				{
					C2iExpression expType = Parametres2i[1];
					bool bTableau = false;
					if (Parametres.Count >= 3)
					{
						if (Parametres[2] is C2iExpressionVrai)
							bTableau = true;
					}
                    if (expType is C2iExpressionTypesDynamics)
                    {
                        return new CTypeResultatExpression(((C2iExpressionTypesDynamics)expType).TypeReprésenté, bTableau);
                    }
					if ( expType is C2iExpressionConstante )
					{
						string strType = ((C2iExpressionConstante)expType).Valeur.ToString();
						Type tp = CActivatorSurChaine.GetType(strType, true);
						if ( tp != null )
							return new CTypeResultatExpression(tp, bTableau);
					}
				}
				return new CTypeResultatExpression ( typeof(object), false );
			}
		}

		

		/// //////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = base.VerifieParametres();
			if ( !result )
				return result;
			if ( Parametres.Count < 2 )
			{
				result.EmpileErreur(I.T("Incorrect number of parameters|161"));
				return result;
			}
			C2iExpression expType = Parametres2i[1];
			if ( expType is C2iExpressionConstante )
			{
				string strType = ((C2iExpressionConstante)expType).Valeur.ToString();
				if ( CActivatorSurChaine.GetType(strType, true)  == null )
					result.EmpileErreur ( I.T("The type @1 doesn't exist|162", strType));
			}
			if (Parametres.Count >= 3)
			{
				if (!(Parametres2i[2] is C2iExpressionVrai) &&
					!(Parametres2i[2] is C2iExpressionFaux))
				{
					result.EmpileErreur(I.T("The third parameter must be 'true()' or 'false()'|163"));
				}
			}
			return result;
		}


		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres )
		{
			CResultAErreur result = CResultAErreur.True;
			object val = valeursParametres[0];
			string strType = valeursParametres[1].ToString();
            Type tp = valeursParametres[1] as Type;
            if(  tp == null )
                tp = CActivatorSurChaine.GetType(strType);
			bool bArray = false;
			if ( tp == null )
				tp = DynamicClassAttribute.GetTypeFromNomConvivial ( strType );
			if (valeursParametres.Length >= 3)
			{
				if (valeursParametres[2] is bool && (bool)valeursParametres[2])
				{
					bArray = true;
				}
			}
			try
			{
				if (!bArray)
				{
					if (val == null)
						result.Data = null;
					else
                    {
                        if ( tp.IsAssignableFrom ( val.GetType() ) )
                            result.Data = val;
                        else
						result.Data = Convert.ChangeType(val, tp);
                    }
				}
				else
				{
					ArrayList lst = new ArrayList();
					if (val is IEnumerable)
					{
						foreach (object obj in (IEnumerable)val)
						{
							lst.Add(Convert.ChangeType(obj, tp));
						}
					}
					else
						lst.Add(Convert.ChangeType(val, tp));
					result.Data = lst.ToArray ( tp );
				}
				return result;
			}
			catch
			{
				result.EmpileErreur(I.T("Impossible to convert the value in the @1 type|164",strType));
			}
			return result;
		}

		
	}
}
