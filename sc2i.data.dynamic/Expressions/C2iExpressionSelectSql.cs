using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.expression;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Permet d'effectuer une sélection directement dans une base
	/// de données SQL.
	/// </summary>
	/// <remarks>
	/// <LI>
	/// Le premier paramètre de la fonction indique liste d'éléments sur laquelle s'applique la 
	/// fonction
	/// </LI>
	/// <LI>
	/// Le second paramètre contient le filtre à appliquer (voir syntaxe de filtres)
	/// </LI>
	/// <LI>
	/// Les paramètres suivants représentent les valeurs des paramètres du filtre
	/// </LI>
	/// </remarks>
	[Serializable]
	[AutoExec("Autoexec")]
	public class C2iExpressionSelectSql : C2iExpressionBaseSql
	{
		/// //////////////////////////////////////////
		public C2iExpressionSelectSql()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateur2iExpression.Register2iExpression ( new C2iExpressionSelectSql().IdExpression,
				typeof(C2iExpressionSelectSql) );
		}

		/// //////////////////////////////////////////
        protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "SqlSelect", 
				new CTypeResultatExpression(typeof(object), true),
				I.TT(GetType(),"SqlSelect(Elements, filter, parameters) \n Returns an element list corresponding to the Sql filter|227"),
				CInfo2iExpression.c_categorieGroupe);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(CObjetDonnee), typeof(string)) );
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(
				new CTypeResultatExpression(typeof(CObjetDonnee), true),
				new CTypeResultatExpression ( typeof(string), false ) ) );
			return info;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return -1;//Nombre variable
		}


		/// //////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( Parametres.Count > 1 && Parametres2i[0] != null )
					return Parametres2i[0].TypeDonnee;
				return new CTypeResultatExpression ( typeof(CObjetDonnee), true );
			}
		}

		/// //////////////////////////////////////////
		public override CResultAErreur MyEval ( CContexteEvaluationExpression ctx, object[] listeParametres )
		{
			return CResultAErreur.False;
		}

		/// //////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			CResultAErreur result = base.VerifieParametres();
			if ( Parametres2i.Length < 2 )
			{
				result.EmpileErreur(I.T("Parameter number is incorrect|202"));
				return result;
			}
			if ( result )
				result = VerifieParametreFiltre ( Parametres2i[0] );
			return result;
		}

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;

			try
			{
				C2iExpression[] parametresFiltre = new C2iExpression[Parametres.Count-2];
				for ( int nIndex = 2; nIndex < Parametres.Count; nIndex++ )
					parametresFiltre[nIndex-2] = Parametres2i[nIndex];
				result = GetFiltre ( ctx, Parametres2i[0], Parametres2i[1], parametresFiltre );
				if ( !result )
					return result;
				if ( !(ctx.ObjetSource is CObjetDonnee) )
				{
					result.EmpileErreur(I.T("SelectSql cannot be applied here|229"));
					return result;
				}
				CFiltreDataAvance filtre = (CFiltreDataAvance)result.Data;
				CListeObjetsDonnees liste = new CListeObjetsDonnees ( ((CObjetDonnee)ctx.ObjetSource).ContexteDonnee,
					CContexteDonnee.GetTypeForTable(filtre.TablePrincipale) );
				liste.Filtre = filtre;
				result.Data = liste;
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException (e ) );
				result.EmpileErreur(I.T("Error in SelectSql|228"));
			}
			return result;

		}

				
	}
}
