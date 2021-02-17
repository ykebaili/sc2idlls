using System;
using System.Collections;
using System.Reflection;

using sc2i.common;
using sc2i.data;
using sc2i.expression;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Permet d'effectuer un calcul directement dans une base
	/// de données SQL.<BR></BR>
	/// Le calcul se fait en utilisant la syntaxe SQL
	/// </summary>
	/// <remarks>
	/// Cette fonction ne peut retourner qu'une seule valeur numérique (somme, moyenne, count,...). La clause select doit
	/// tenir compte de cette contrainte.
	/// <LI>
	/// Le premier paramètre indique les éléments à sommer (doit être un champ numérique)
	/// </LI>
	/// <lI>
	/// Le second paramètre indique l'expression SQL à appliquer. (clause select)<BR></BR>
	/// Tous les champs de l'expression doivent appartenir à la table principale
	/// ou être préfixés du nom de leur table parente.<BR></BR>
	/// Il est possible d'utiliser les nom conviviaux à la place des noms de champs
	/// en les incluant dans des []
	/// </lI>
	/// <LI>
	/// Le troisième paramètre contient le filtre à appliquer (voir syntaxe de filtres)
	/// </LI>
	/// <LI>
	/// Les paramètres suivants représentent les valeurs des paramètres du filtre
	/// </LI>
	/// </remarks>
	[Serializable]
	[AutoExec("Autoexec")]
	public class C2iExpressionCalculSql : C2iExpressionBaseSql
	{
		/// //////////////////////////////////////////
		public C2iExpressionCalculSql()
		{
		}

	
		/// //////////////////////////////////////////
		public static void Autoexec()
		{
			CAllocateur2iExpression.Register2iExpression ( new C2iExpressionCalculSql().IdExpression,
				typeof(C2iExpressionCalculSql) );
		}

		/// //////////////////////////////////////////
		protected override CInfo2iExpression GetInfosSansCache()
		{
			CInfo2iExpression info = new CInfo2iExpression( 0, "SqlCalc", 
				new CTypeResultatExpression(typeof(double), false),
				I.TT( GetType(), "SqlCalc(Elements, ExpressionSql, filter, parameters) \n Return a value from an expression Sql. The elements of the list muist satisfy the Sql filter|203"),
				CInfo2iExpression.c_categorieGroupe);
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(typeof(CObjetDonnee), typeof(string), typeof(string)) );
			info.AddDefinitionParametres( new CInfo2iDefinitionParametres(
				new CTypeResultatExpression(typeof(CObjetDonnee), true),
				new CTypeResultatExpression ( typeof(string), false ),
				new CTypeResultatExpression ( typeof(string), false ) ) );
			return info;
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
			if ( Parametres.Count < 3 )
			{
				result.EmpileErreur(I.T("Incorrect number of parameters|202"));
				return result;
			}
			result = VerifieParametreFiltre ( Parametres2i[0] );
			return result;
		}

		/// //////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return -1;//Nombre variable
		}

		/// //////////////////////////////////////////
		public CResultAErreur GetClauseSelect ( string strSelect, Type tpInterroge )
		{
			CResultAErreur result = CResultAErreur.True;
			bool bInCrochet = false;
			bool bGuiSimple = false; 
			bool bGuiDouble = false;
			bool bInText = false;
			string strFinal = "";
			string strNomChamp = "";
			CStructureTable structure = CStructureTable.GetStructure(tpInterroge);
			foreach ( char c in strSelect )
			{
				if ( c == '[' && !bInText )
				{
					strNomChamp = "";
					bInCrochet = true;
				}
				else
				{
					if ( !bInCrochet )
					{
						if ( c == '"' )
						{
							if ( !bInText )
							{
								bInText = true;
								bGuiDouble = true;
							}
							else if ( bGuiDouble )
							{
								bInText = false;
								bGuiDouble = false;
							}
						}
						if ( c == '\'' )
						{
							if ( !bInText )
							{
								bInText = true;
								bGuiSimple = true;
							}
							else if ( bGuiSimple )
							{
								bInText = false;
								bGuiSimple = false;
							}
						}
						strFinal += c;
					}
					if ( bInCrochet )
					{
						if ( c == ']' )
						{
							strNomChamp = strNomChamp.ToUpper();
							bInCrochet = false;
							//Cherche le champ
							bool bTrouve = false;
							foreach ( CInfoChampTable champ in structure.Champs )
							{
								if ( strNomChamp == champ.NomChamp.ToUpper() || 
									strNomChamp == champ.NomConvivial.ToUpper() ||
									strNomChamp == champ.Propriete.ToUpper() )
								{
									strFinal +=" ["+champ.NomChamp+"]";
									bTrouve = true;
									break;
								}
							}
							if ( !bTrouve )
								strFinal += "["+strNomChamp+"]";
						}
						else
							strNomChamp += c;
					}
				}
			}
			if ( bInCrochet )
				result.EmpileErreur(I.T("Hook not closed in select clause|203"));
			else
				result.Data = strFinal;
			return result;
		}

		/// //////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;

			try
			{
				C2iExpression[] parametresFiltre = new C2iExpression[Parametres.Count-3];
				for ( int nIndex = 3; nIndex < Parametres.Count; nIndex++ )
					parametresFiltre[nIndex-3] = Parametres2i[nIndex];
				result = GetFiltre ( ctx, Parametres2i[0], Parametres2i[2], parametresFiltre );
				if ( !result )
					return result;
				if ( !(ctx.ObjetSource is CObjetDonnee) )
				{
					result.EmpileErreur(I.T("CalculSql cannot be applied here|204"));
					return result;
				}
				CObjetDonnee objet = (CObjetDonnee)ctx.ObjetSource;

				CFiltreDataAvance filtre = (CFiltreDataAvance)result.Data;
				Type tpInterroge = CContexteDonnee.GetTypeForTable(filtre.TablePrincipale);
				
				result = Parametres2i[1].Eval(ctx);
				if ( !result )
					return result;
				result = GetClauseSelect ( result.Data.ToString(), tpInterroge );

				IObjetServeur serveur = objet.ContexteDonnee.GetTableLoader ( filtre.TablePrincipale );
				result = serveur.ExecuteScalar ( (string)result.Data, filtre );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException (e ) );
				result.EmpileErreur(I.T("Error in calculSql|205"));
			}
			return result;

		}

				
	}
}
