using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionVariable : C2iExpressionAnalysable
	{
		/// //////////////////////////////////////////
		public C2iExpressionVariable()
		{
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 
				0, 
				"Var", 
				typeof(object),
				I.TT(GetType(), "Var ( Name, type [,array] )\r\nDeclares the variable name of type 'type'. if the array parameter is <> 0, the type is a table|135"),
				CInfo2iExpression.c_categorieDivers );
			info.AddDefinitionParametres( new CInfoUnParametreExpression (I.T("Variable|20040"), typeof(object) ),
                new CInfoUnParametreExpression(I.T("Type|20038"), typeof(string)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Variable|20040"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Type|20038"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Array type|20041"), typeof(int)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Variable|20040"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Type|20038"), typeof(string)),
                new CInfoUnParametreExpression(I.T("Array type|20041"), typeof(bool)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Variable|20040"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Type|20038"), typeof(Type)),
                new CInfoUnParametreExpression(I.T("Array type|20041"), typeof(bool)));
            info.AddDefinitionParametres(new CInfoUnParametreExpression(I.T("Variable|20040"), typeof(object)),
                new CInfoUnParametreExpression(I.T("Type|20038"), typeof(Type)));
			return info;
		}

		//-------------------------------------------------------
		public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		
		//-------------------------------------------------------
		protected override CResultAErreur  ProtectedEval(CContexteEvaluationExpression ctx)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				C2iExpressionChamp champ = Parametres2i[0] as C2iExpressionChamp;
				if ( champ == null || champ.DefinitionPropriete as CDefinitionProprieteDynamiqueVariableFormule == null)
				{
					result.EmpileErreur(I.T("Bad parameter for VAR|20005"));
					return result;
				}
				
				ctx.AddVariable ( champ.DefinitionPropriete as CDefinitionProprieteDynamiqueVariableFormule );
				result.Data = null;
			}
			catch
			{
				result.EmpileErreur(I.T("Error during the variable cration|132"));
			}
			return result;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur AfterAnalyse(CAnalyseurSyntaxique analyseur)
		{
			CResultAErreur result = CResultAErreur.True;
			bool bIsTableau = false;
			Type tp = null;
			CAnalyseurSyntaxiqueExpression analyseurExpression = analyseur as CAnalyseurSyntaxiqueExpression;
			if ( analyseurExpression != null )
			{
				C2iExpressionChamp champ = Parametres2i[0] as C2iExpressionChamp;
				//Vérifie que les paramètres sont bien des constantes string
				bool bParamOk = false;
				if ( Parametres.Count >= 2 )
				{
					if ( Parametres[0] is C2iExpressionChamp &&
						(Parametres[1] is C2iExpressionConstante || Parametres[1] is C2iExpressionTypesDynamics) && 
						(Parametres.Count !=3 || Parametres[2] is C2iExpressionConstante || Parametres[2] is C2iExpressionVrai || Parametres[2] is C2iExpressionFaux ))
					{
                        if (Parametres[1] is C2iExpressionTypesDynamics)
                        {
                            tp = ((C2iExpressionTypesDynamics)Parametres[1]).TypeReprésenté;
                        }
                        else if (Parametres[1] is Type)
                            tp = Parametres[1] as Type;
                        else
                        {
                            string strType = ((C2iExpressionConstante)Parametres2i[1]).Valeur.ToString();
                            tp = CActivatorSurChaine.GetType(strType, true);
                        }
						if ( tp == null )
						{
                            result.EmpileErreur(I.T("The @1 type doesn't exist|136", Parametres[1].ToString()));
							return result;
						}
						else 
						{
							if ( Parametres.Count == 3 && 
                                Parametres2i[2] is C2iExpressionConstante )
                            {   
								if ( ((C2iExpressionConstante)Parametres[2]).Valeur is int )
								    bIsTableau = (int)(((C2iExpressionConstante)Parametres[2]).Valeur) != 0;
                                if ( ((C2iExpressionConstante)Parametres[2]).Valeur is bool)
                                    bIsTableau = (bool)((C2iExpressionConstante)Parametres[2]).Valeur;
                            }
                            if ( Parametres.Count == 3 && Parametres[2] is C2iExpressionVrai )
                                bIsTableau = true;
                            if ( Parametres.Count == 3 && Parametres[2]  is C2iExpressionFaux )
                                bIsTableau = false;
							bParamOk = true;
						}
						
					}
                    
                    
				}
				if ( !bParamOk )
				{
					result.EmpileErreur(I.T("The Var function paramters are invalid (two constant string are awaited and possibly one Boolean for the table)|138"));
					return result;
				}

				CDefinitionProprieteDynamiqueVariableFormule def = new CDefinitionProprieteDynamiqueVariableFormule(
							champ.DefinitionPropriete.Nom,
							new CTypeResultatExpression(tp, bIsTableau),
							false);
				champ.DefinitionPropriete = def;
				analyseurExpression.AddVariable(def);
			}
			return base.AfterAnalyse (analyseur);
		}


		
	}
}
