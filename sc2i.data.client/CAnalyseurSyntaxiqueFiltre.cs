using System;
using System.Collections;

using sc2i.common;
using System.Text.RegularExpressions;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CAnalyseurSyntaxiqueFiltre.
	/// </summary>
	public class CAnalyseurSyntaxiqueFiltre : CAnalyseurSyntaxique
	{
		/// <summary>
		/// Cache des 50 analyses précédentes
		/// </summary>
		private static CHashtableATailleLimitee m_tableCache = new CHashtableATailleLimitee(50);
		
		private ArrayList m_listeOperateursFonction;

		private CAllocateurComposantFiltre m_allocateur = null;

		//Indique qu'on fait une analyse de syntaxe SQL standard
		private bool m_bModeSyntaxeSqlStd = false;
		
		/// /////////////////////////////////////////////////
		public CAnalyseurSyntaxiqueFiltre( string strTableDeBase )
		{
			if ( m_listeOperateursFonction == null )
			{
				m_listeOperateursFonction = new ArrayList();
				foreach ( Type tp in typeof(CComposantFiltreFonction).Assembly.GetTypes() )
				{
					if ( typeof(CComposantFiltreFonction).IsAssignableFrom(tp) && !tp.IsAbstract)
					{
						CComposantFiltreFonction comp = (CComposantFiltreFonction)Activator.CreateInstance(tp);
						COperateurAnalysable operateur = comp.GetOperateur();
						m_listeOperateursFonction.Add ( operateur );
					}
				}
			}
			m_allocateur = new CAllocateurComposantFiltre( strTableDeBase );
		}

		/// /////////////////////////////////////////////////
		protected bool SyntaxeSqlStandard
		{
			get
			{
				return m_bModeSyntaxeSqlStd;
			}
			set
			{
				m_bModeSyntaxeSqlStd = value;
			}
		}

		/// /////////////////////////////////////////////////
		public static CResultAErreur AnalyseFormule ( string strChaine, string strTableDeBase )
		{
			CResultAErreur result = CResultAErreur.True;
			string strKey = strTableDeBase+"/"+strChaine;
			CComposantFiltre composant = (CComposantFiltre)m_tableCache[strKey];
			if ( composant != null )
			{
				composant = (CComposantFiltre)CCloner2iSerializable.Clone ( composant );
				result.Data = composant;
				return result;
			}

			CAnalyseurSyntaxiqueFiltre analyseur = new CAnalyseurSyntaxiqueFiltre( strTableDeBase );
			analyseur.SyntaxeSqlStandard = false;
			string strCopie = strChaine;

			//Permet de convertir un filtre data en filter data avancé
			Regex exNotLike = new Regex("not like", RegexOptions.IgnoreCase);
			Regex exNotIn = new Regex("not in", RegexOptions.IgnoreCase);
			strCopie = exNotIn.Replace(strCopie, "NotIn");
			strCopie = exNotLike.Replace(strCopie, "NotLike");

			result = analyseur.AnalyseChaine(strCopie);
			if ( !result )
			{
				result.EmpileErreur(I.T("-------------SC2I Syntax-----------------|110"));
				analyseur.SyntaxeSqlStandard = true;
				CResultAErreur resultStd = analyseur.AnalyseChaine(strChaine);
				if ( !resultStd )
				{
					resultStd.EmpileErreur(I.T("-------------SQL Syntax--------------|111"));
					result.Erreur += resultStd.Erreur;
				}
				else
					result = resultStd;
			}
			if ( result.Data is CComposantFiltre )
			{
				composant = (CComposantFiltre)result.Data;
				composant = (CComposantFiltre)CCloner2iSerializable.Clone ( composant );
				m_tableCache.AddElement ( strKey, composant  );
			}
			return result;
		}

		/// /////////////////////////////////////////////////
		protected override char SeparateurParametres
		{
			get
			{
				if ( m_bModeSyntaxeSqlStd )
					return ',';
				else
					return ';';
			}
		}


		/// /////////////////////////////////////////////////
		protected override IAllocateurExpression AllocateurExpression
		{
			get
			{
				return m_allocateur;
			}
		}

		/// /////////////////////////////////////////////////
		protected override ArrayList GetOperateursNiveau ( int nNiveau )
		{
			ArrayList lst = new ArrayList();
			foreach ( COperateurAnalysable operateur in CComposantFiltreOperateur.m_operateurs )
				if ( operateur.Niveau == nNiveau )
					lst.Add ( operateur );
			if ( nNiveau == 0 )
			{
				foreach ( COperateurAnalysable operateur in m_listeOperateursFonction )
					lst.Add ( operateur );
			}
			return lst;
		}

		/// /////////////////////////////////////////////////
		protected override int NiveauOperateurMax
		{
			get
			{
				return 7;
			}
		}

		/// /////////////////////////////////////////////////
		protected override bool BaseNiveau(int nNiveau)
		{
			//Greffon pour le not
			if ( nNiveau == 4 )
			{
				PasseCaracteresControle();
				IsOperateurNiveauN (nNiveau, false);//Not facultatif
			}	
			if ( nNiveau == 0 )
			{
				//Si c'est null, pas de paramètres
				PasseCaracteresControle();
				if ( Identifie ( "null", 0 ) )
				{
					ContexteAnalyse.m_nPositionAnalyse+= "null".Length;
					ContexteAnalyse.PushToken ( new CTokenConstante(ContexteAnalyse.m_nPositionAnalyse, null ));
					return true;
				}
			}
					
			return base.BaseNiveau (nNiveau);
		}

		/// <summary>
		/// ///////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="nNiveau"></param>
		/// <returns></returns>
		protected override IExpression GetExpression ( int nNiveau )
		{
			if ( nNiveau == 4 )
			{
				IExpression expN;
				expN = GetExpression ( nNiveau - 1 );
				if ( expN == null )
					return null;
				int nOldPosPile = ContexteAnalyse.GetPosPile();
				CTokenAnalyseSyntaxique token = ContexteAnalyse.PopToken();
				if ( token == null )
					return expN;
				if ( token.GetNiveauToken() == nNiveau && token is CTokenOperateur)
				{
					//Si on l'a , c'est bien
					CTokenOperateur tokOp = (CTokenOperateur)token;
					IExpression exp = AllocateurExpression.GetExpression(tokOp.Operateur.Id);
					exp.Parametres.Add ( expN );
					CResultAErreur result = exp.VerifieParametres();
					return exp;
				}
				else// sinon, c'est pas grave
					ContexteAnalyse.SetPosPile ( nOldPosPile );
				return expN;

			}
			return base.GetExpression(nNiveau);
		}




		/// /////////////////////////////////////////////////
		
		/// <summary>
		/// Retourne une nouvelle fonction objet
		/// </summary>
		/// <param name="expressionSource">Source de l'appel</param>
		/// <param name="expressionArgumentObjet">Méthode ou propriété appelée</param>
		/// <returns></returns>
		protected override CResultAErreur GetExpressionObjet ( IExpression expressionSource, IExpression expressionArgumentObjet)
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("Object syntax not supported|109"));
			return result;
		}

		/// <summary>
		/// Retourne une nouvelle fonction objet
		/// </summary>
		/// <param name="expressionSource">Source de l'appel</param>
		/// <param name="expressionArgumentObjet">Méthode ou propriété appelée</param>
		/// <returns></returns>
		protected override CResultAErreur GetExpressionMethode ( string strNomMethode )
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("Object syntax not supported|109"));
			return result;
		}

		protected override CResultAErreur GetExpressionThis (  )
		{
			CResultAErreur result = CResultAErreur.False;
			result.EmpileErreur(I.T("Object syntax not supported|109"));
			return result;
		}


		/// /////////////////////////////////////////////////
		protected override bool IntegrerSyntaxeObjet
		{
			get 
			{
				return false;
			}
		}

		/// /////////////////////////////////////////////////
		protected override CResultAErreur GetExpressionChamp ( string strChamp )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( strChamp.Length > 0  && strChamp[0] == '@' )
				result.Data = new CComposantFiltreVariable ( strChamp );
			else
				result.Data = new CComposantFiltreChamp ( strChamp, ((CAllocateurComposantFiltre)m_allocateur).TableDeBase );
			return result;
		}

		/// /////////////////////////////////////////////////
		protected override CResultAErreur GetExpressionListe ( IExpression[] liste )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = new CComposantFiltreListe ( liste );
			return result;
		}

		

		
	}
}
