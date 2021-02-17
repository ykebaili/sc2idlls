using System;
using System.Collections;

using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CAllocateurComposantFiltre.
	/// </summary>
	public class CAllocateurComposantFiltre : IAllocateurExpression
	{
		private Hashtable m_tableIdOperateurToComposantFonction;

		private string m_strTableDeBase = "";
		/// ///////////////////////////////////////////
		public CAllocateurComposantFiltre( string strTableDeBase )
		{
			if ( m_tableIdOperateurToComposantFonction == null )
			{
				m_tableIdOperateurToComposantFonction = new Hashtable();
				foreach ( Type tp in typeof(CComposantFiltreFonction).Assembly.GetTypes() )
				{
					if ( typeof(CComposantFiltreFonction).IsAssignableFrom(tp) && !tp.IsAbstract)
					{
						CComposantFiltreFonction comp = (CComposantFiltreFonction)Activator.CreateInstance(tp);
						COperateurAnalysable operateur = comp.GetOperateur();
						m_tableIdOperateurToComposantFonction[operateur.Id] = tp;
					}
				}
			}

			m_strTableDeBase = strTableDeBase;
		}

		/// ///////////////////////////////////////////
		public IExpression GetExpression ( string strIdExpression )
		{
			Type tp = (Type)m_tableIdOperateurToComposantFonction[strIdExpression];
			if ( tp == null )
				return new CComposantFiltreOperateur(strIdExpression);
			return (IExpression )Activator.CreateInstance(tp);
		}

		/// ///////////////////////////////////////////
		public string TableDeBase
		{
			get
			{
				return m_strTableDeBase;
			}
		}

		/// ///////////////////////////////////////////
		public IExpression GetExpressionConstante ( object valeur )
		{
			return new CComposantFiltreConstante ( valeur );
		}

		/// ///////////////////////////////////////////
		public IExpression GetExpressionChamp ( IDefinitionChampExpression def )
		{
			if ( def.Nom.Length > 0  && def.Nom[0] == '@' )
				return new CComposantFiltreVariable ( def.Nom );
			else
				return new CComposantFiltreChamp ( def.Nom, m_strTableDeBase );
		}

		/// ///////////////////////////////////////////
		public IExpression GetExpressionIndexeur ( IExpression expressionChamp, IExpression expressionIndex )
		{
			return null;//Les indexeurs ne sont pas supportés
		}


		/// ///////////////////////////////////////////
		public IExpression GetExpressionParentheses()
		{
			return new CComposantFiltreOperateur ( "1000" );
		}
	}
}
