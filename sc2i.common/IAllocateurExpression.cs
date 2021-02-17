using System;

namespace sc2i.common
{
	/// <summary>
	/// Description r�sum�e de IAllocateurExpression.
	/// </summary>
	public interface IAllocateurExpression
	{
		IExpression GetExpression ( string strIdExpression );
		
		//Lance une exception en cas de pb
		IExpression GetExpressionConstante ( object valeur );
		
		//Lance une exception en cas de pb
		//IExpression GetExpressionChamp ( string strNomChamp );

		//Retourne une nouvelle expression partenthese (fonction qui ne fait rien !!! )
		IExpression GetExpressionParentheses();

		//Retourne une nouvelle expression Indexeur ( expression avec deux param�tres : 
		//le premier est le champ � indexer, le second l'index (expression)
		IExpression GetExpressionIndexeur( IExpression expressionIndexee, IExpression expressionIndex );
	}
}
