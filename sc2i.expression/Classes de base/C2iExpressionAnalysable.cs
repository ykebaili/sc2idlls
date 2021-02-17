using System;
using System.Linq;
using System.Collections;
using sc2i.common;
using System.Collections.Generic;
using System.Text;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionAnalysable.
	/// </summary>
	[Serializable]
	public abstract class C2iExpressionAnalysable : C2iExpression
	{
		/// ///////////////////////////////////////////////////////////
		public C2iExpressionAnalysable()
		{
		}

		/// ///////////////////////////////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return GetInfos().IdExpression;
			}
		}

		/// ///////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return GetInfos().TypeDonnee;
			}
		}

		/// ///////////////////////////////////////////////////////////
		protected abstract CInfo2iExpression GetInfosSansCache();

        /// ///////////////////////////////////////////////////////////
        public virtual CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info  = CCacheInfosExpression.GetCache(this);
            if (info == null)
            {
                info = GetInfosSansCache();
                CCacheInfosExpression.SetInfo(this, info);
            }
            return info;
        }


        
                

		/// ///////////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return -1;//Nombre variable
			//return GetInfos().NbParametres;
		}

		/// ///////////////////////////////////////////////////////////
		public override string GetString()
		{
			string strChaine="";
			CInfo2iExpression info = GetInfos();
			if ( info.Niveau > 0 )
			{
                strChaine = (Parametres.Count > 0?((IExpression)Parametres[0]).GetString():"") +
                        CaracteresControleAvant + info.Texte +
                        (Parametres.Count>1?((IExpression)Parametres[1]).GetString():"");
			}
			else
			{
				strChaine = CaracteresControleAvant+ info.Texte+"(";
                foreach (IExpression exp in Parametres)
                {
                    if (exp != null)
                        strChaine += exp.GetString() + ";";
                }
				if ( Parametres.Count > 0 )
					strChaine = strChaine.Substring(0, strChaine.Length-1);
				strChaine +=")";
			}
			return strChaine;
		}

		/// ///////////////////////////////////////////////////////////
		public abstract CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] valeursParametres );

		/// ///////////////////////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			ArrayList lstParametres = new ArrayList();
			CResultAErreur result = EvalParametres ( ctx, lstParametres );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error during the @1 expression evaluation|104",GetString()));
				return result;
			}
			return MyEval ( ctx, lstParametres.ToArray() );
		}

		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = CResultAErreur.True;
			CInfo2iExpression info = GetInfos();
			/*SC LE 9/1 : le nombre de paramètre est donnée par les définitions de paramètres
			 * if ( info.NbParametres != Parametres.Count )
			{
				string strMes = "Le nombre de paramètres est incorrect ("+info.NbParametres.ToString()+" attendu";
				if ( info.NbParametres > 1 )
					strMes+="s";
				result.EmpileErreur(strMes);
				return result;
			}*/
			bool bSurchargeTrouvee = false;
			foreach ( CInfo2iDefinitionParametres defPar in info.InfosParametres )
			{
				if ( defPar.TypesDonnees.Length <= Parametres.Count )
				{
					bSurchargeTrouvee = true;
					int nParametre = 0;
					foreach ( C2iExpression exp in Parametres )
					{
						nParametre++;
						if ( exp == null )
						{
							result.EmpileErreur(I.T("The @1 parameter is null|105",nParametre.ToString()));
							return result;
						}
						else
						{
							if ( nParametre <= defPar.TypesDonnees.Length )
							{
								CTypeResultatExpression typeAttendu = defPar.TypesDonnees[nParametre-1];

								if ( exp.TypeDonnee!=null && !exp.TypeDonnee.CanConvertTo ( typeAttendu ) )
								{
									bSurchargeTrouvee = false;
									break;
								}
							}
							else if ( GetNbParametresNecessaires() != -1 )
							{
								bSurchargeTrouvee = false;
								break;
							}
						}
					}
				}
				if ( bSurchargeTrouvee )
					break;
			}
			if ( !bSurchargeTrouvee )
				result.EmpileErreur(I.T("No '@1' function overload match paramters|106",info.Texte));
			return result;
		}

		private bool FiltreType ( Type m, object filterCriteria )
		{
			return m == filterCriteria;
		}

		

	}
}
