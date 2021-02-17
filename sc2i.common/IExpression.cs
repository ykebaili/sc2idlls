using System;
using System.Collections;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de IExpression.
	/// </summary>
	public interface IExpression
	{
		//Retourne le nombre de paramètres nécéssaires pour la fonction
		//Retourne -1 si nombre illimité
		int GetNbParametresNecessaires();

		//Retourne la liste des paramètres
		ArrayList Parametres{get;}

		//Retourne la liste des paramètres du type spécifié
		//Cette fonction récursive permet de trouver toutes les expressions
		//d'un type spécifié dans une expression (par exemple, recherche
		//de champs)
		ArrayList ExtractExpressionsType ( Type tp );

		//Vérifie que les paramètres sont du bon type
		CResultAErreur VerifieParametres();

		//Retourne l'expression sous forme de chaine
		string GetString();

		//Retourne ou définit les caractères de contrôle rencontrés avant l'élément dans le texte analysé
		string CaracteresControleAvant{get;set;}


		/// <summary>
		/// Retourne vrai si l'expression peut être l'argument d'une syntaxe objet
		/// c'est à dire dans une syntaxe A.B, B est argument d'une syntaxe objet
		/// Typiquement, ce sont les méthodes et les propriétés qui retournent vrai
		/// </summary>
		bool CanBeArgumentExpressionObjet{get;}

		//Est appellé une fois que l'expression a été analysée par un analyseur syntaxique.
		//Permet par exemple à une expression de déclaration de variable d'ajouter la variable
		//à l'analyseur
		CResultAErreur AfterAnalyse ( CAnalyseurSyntaxique analyseur );


	}

}
