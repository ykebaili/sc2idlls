using System;
using System.Collections;

using sc2i.common;

namespace sc2i.expression.expressions
{
	/// <summary>
	/// Description résumée de C2iExpressionPlus.
	/// </summary>
	[Serializable]
	public class C2iExpressionListeEnTexte : C2iExpressionObjetNeedTypeParent
	{
		/// //////////////////////////////////////////
		public C2iExpressionListeEnTexte()
		{
		}

		/// //////////////////////////////////////////
		public override bool AgitSurListe
		{
			get
			{
				return true;
			}
		}

	

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "ListToText", new CTypeResultatExpression(typeof(string), false),
				I.TT(GetType(), "ListToText(Formula;separator)\nConvert the list in text. For each value of the list, the format is evaluated and added to the resulting string. Each element is separated by the separator|238"), CInfo2iExpression.c_categorieGroupe);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres( typeof(string),typeof(string) ) );
			return info;
		}

		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return new CTypeResultatExpression ( typeof(string), false );
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
				result.EmpileErreur(I.T("The function ListInText cannot apply to the @1 type|237",ctx.ObjetSource.GetType().ToString()));
				return result;
			}
			ArrayList lst = new ArrayList();


			result = Parametres2i[1].Eval ( ctx );
			if ( !result || result.Data == null )
			{
				result.EmpileErreur(I.T("Error in separator|236"));
				return result;
			}
			string strSep = result.Data.ToString();

			string strResult = "";
			foreach ( object obj in (IEnumerable) ctx.ObjetSource )
			{
				try
				{
					ctx.PushObjetSource ( obj, true );
					result = Parametres2i[0].Eval ( ctx );
					ctx.PopObjetSource(true);
					if ( !result )
						return result;
					if ( result.Data != null )
					{
						string strVal = result.Data.ToString();
						if ( strVal.Length > 0 )
							strResult += strVal+strSep;
					}
				}
				catch {}
			}
			if ( strResult.Length > 0 && strSep.Length > 0)
				strResult = strResult.Substring(0, strResult.Length-strSep.Length );
			result.Data = strResult;
			return result;
		}

        public override CObjetPourSousProprietes GetObjetAnalyseParametresFromObjetAnalyseSource(CObjetPourSousProprietes objetSource)
        {
            return objetSource.GetObjetAnalyseElements();
        }


				
	}
}
