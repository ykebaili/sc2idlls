using System;
using System.Collections;

using sc2i.expression;
using sc2i.common;
using sc2i.common.unites;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de IVariableDynamique.
	/// </summary>
	public interface IVariableDynamique
	{
		/// ///////////////////////////////////////////
		string IdVariable{get;set;}

		/// ///////////////////////////////////////////
		string Nom{get;set;}

		/// ///////////////////////////////////////////
		string Description{get;set;}
		
		/// ///////////////////////////////////////////
		CTypeResultatExpression TypeDonnee{get;}

		/// ///////////////////////////////////////////
		///Indique si l'utilisateur choisis parmis des valeurs ou non (combo !)
		bool IsChoixParmis();

		/// ///////////////////////////////////////////
		IList Valeurs{get;}

		/// ///////////////////////////////////////////
		/// Indique si la variable correspond à un choix d'utilisateur
		bool IsChoixUtilisateur();

		/// ///////////////////////////////////////////
		CResultAErreur VerifieValeur ( object valeur );

        /// ///////////////////////////////////////////
        IClasseUnite ClasseUnite { get; }

        /// ///////////////////////////////////////////
        string FormatAffichageUnite { get; }
	}

    public interface IVariableDynamiqueAValeurParDefaut
    {
        object GetValeurParDefaut();
    }

    public interface IVariableDynamiqueCalculee
    {
        C2iExpression FormuleDeCalcul { get; }
    }

    /// <summary>
    /// Variable dynamique qui travaille avec une instance et non pas un type
    /// </summary>
    public interface IVariableDynamiqueInstance : IVariableDynamique
    {
        CObjetPourSousProprietes GetObjetPourAnalyseSousProprietes();
    }


    


        
}
