using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

using sc2i.common;
using sc2i.common.unites;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de CAllocateurExpression.
	/// </summary>
	public class CAllocateur2iExpression : IAllocateurExpression
	{
        

        //Table IDExpression->TypeExpression
        private static Hashtable m_tableTypesExpression = new Hashtable();

        //Liste des types d'expression existants
        private static ArrayList m_listeTypesExpression = new ArrayList();

        

		/// //////////////////////////////////////////////////////////////
		public CAllocateur2iExpression( )
		{
		}


		/// //////////////////////////////////////////////////////////////
		public static void Register2iExpression ( string strId, Type tp )
		{
			Type oldTp = (Type)m_tableTypesExpression[strId];
			if ( oldTp == null )
			{
				m_tableTypesExpression[strId] = tp;
				m_listeTypesExpression.Add ( tp );
			}
			else
			{
				if ( tp != oldTp )
				{
					throw new Exception(I.T("The @1 id expression was recorded several times with two different types|140", strId));
				}
			}
		}

		/// //////////////////////////////////////////////////////////////
		public static int GetNbExpressions()
		{
			return m_listeTypesExpression.Count;
		}

		/// //////////////////////////////////////////////////////////////
		public static IExpression[] ToutesExpressions
		{
			get
			{
				ArrayList lst = new ArrayList();
				foreach ( Type tp in m_listeTypesExpression )
				{
#if PDA
					IExpression exp = (IExpression)Activator.CreateInstance(tp);
#else
					IExpression exp = (IExpression)Activator.CreateInstance(tp, new object[0]);
#endif
					lst.Add ( exp );
				}
                
                
				return ( IExpression[] ) lst.ToArray(typeof(IExpression));
			}
		}

		/// //////////////////////////////////////////////////////////////
		public IExpression GetExpression ( string strIdExpression )
		{
			Type tp = (Type)m_tableTypesExpression[strIdExpression];
            if (tp == null)
            {
                if (strIdExpression.StartsWith(":"))//Une unité ?
                {
                    if ( CUtilUnite.GetIdClasseUnite(strIdExpression.Substring(1)) != null )
                        return new C2iExpressionConvertUnit(strIdExpression.Substring(1));
                }
                return null;
            }
#if PDA
			return (IExpression)Activator.CreateInstance ( tp);
#else
			return (IExpression)Activator.CreateInstance ( tp, new object[0]);
#endif
		}

		/// //////////////////////////////////////////////////////////////
		public IExpression GetExpressionConstante ( object obj )
		{
			return new C2iExpressionConstante ( obj );
		}

		/// //////////////////////////////////////////////////////////////
		public IExpression GetExpressionIndexeur ( IExpression expressionIndexee, IExpression expressionIndex )
		{
			C2iExpressionIndexeur indexeur = new C2iExpressionIndexeur(expressionIndexee, expressionIndex);
			if ( !indexeur.VerifieParametres() )
				return null;
			return indexeur;
		}

		/// ///////////////////////////////////////////////////////////////
		public IExpression GetExpressionParentheses ( )
		{
			return new C2iExpressionParentheses();
		}
	}
}
