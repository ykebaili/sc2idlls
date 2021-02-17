using System;
using System.Collections;

namespace sc2i.common
{
	/// <summary>
	/// Description r�sum�e de IExpression.
	/// </summary>
	public interface IExpression
	{
		//Retourne le nombre de param�tres n�c�ssaires pour la fonction
		//Retourne -1 si nombre illimit�
		int GetNbParametresNecessaires();

		//Retourne la liste des param�tres
		ArrayList Parametres{get;}

		//Retourne la liste des param�tres du type sp�cifi�
		//Cette fonction r�cursive permet de trouver toutes les expressions
		//d'un type sp�cifi� dans une expression (par exemple, recherche
		//de champs)
		ArrayList ExtractExpressionsType ( Type tp );

		//V�rifie que les param�tres sont du bon type
		CResultAErreur VerifieParametres();

		//Retourne l'expression sous forme de chaine
		string GetString();

		//Retourne ou d�finit les caract�res de contr�le rencontr�s avant l'�l�ment dans le texte analys�
		string CaracteresControleAvant{get;set;}


		/// <summary>
		/// Retourne vrai si l'expression peut �tre l'argument d'une syntaxe objet
		/// c'est � dire dans une syntaxe A.B, B est argument d'une syntaxe objet
		/// Typiquement, ce sont les m�thodes et les propri�t�s qui retournent vrai
		/// </summary>
		bool CanBeArgumentExpressionObjet{get;}

		//Est appell� une fois que l'expression a �t� analys�e par un analyseur syntaxique.
		//Permet par exemple � une expression de d�claration de variable d'ajouter la variable
		//� l'analyseur
		CResultAErreur AfterAnalyse ( CAnalyseurSyntaxique analyseur );


	}

}
