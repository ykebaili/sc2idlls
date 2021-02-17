using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionListe.
	/// </summary>
	[Serializable]
	public class C2iExpressionListe : C2iExpression
	{
		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionListe()
		{
		}

		/// //////////////////////////////////// /////////////////////////////////
		public C2iExpressionListe ( IExpression[] elements )
		{
			foreach ( IExpression expression in elements )
				Parametres.Add ( expression );
		}
		

		/// ///////////////////////////////////////////////////////////
		public override bool CanBeArgumentExpressionObjet
		{
			get
			{
				return false;
			}
		}



		/// //////////////////////////////////// /////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return -1;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override string GetString()
		{
			string strRetour = "";
			foreach ( C2iExpression expression in Parametres2i )
				strRetour += expression.GetString()+";";
			if ( strRetour.Length > 0 )
				strRetour = strRetour.Substring(0, strRetour.Length-1);
			strRetour = "{"+strRetour+"}";
			return CaracteresControleAvant+strRetour;
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return "_LIST";
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				CTypeResultatExpression typeRetenu = new CTypeResultatExpression ( typeof(object), false);
				int nNbParametres = Parametres.Count;
				if ( nNbParametres > 0 )
				{
					typeRetenu = Parametres2i[0].TypeDonnee;
					for ( int nParametre = 0; nParametre < nNbParametres; nParametre++ )
					{
						CTypeResultatExpression typeElement = Parametres2i[nParametre].TypeDonnee;
						if ( !typeElement.CanConvertTo(typeRetenu) )
						{
							if ( !typeRetenu.CanConvertTo(typeElement) )
							{
								typeRetenu = new CTypeResultatExpression(typeof(Object), false);
								break;
							}
							else
							{
								typeRetenu = typeElement;
							}
						}
					}
				}
				return typeRetenu.GetTypeArray();
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = CResultAErreur.True;
			foreach ( C2iExpression expression in Parametres )
			{
				if ( expression != null )
					result = expression.VerifieParametres();
				if ( !result )
					return result;
			}
			return result;
		}

		/// //////////////////////////////////// /////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;
			ArrayList lstResult = new ArrayList();
			result = EvalParametres(ctx, lstResult );
			if ( result )
				result.Data = lstResult;
			return result;
		}

	}
}
