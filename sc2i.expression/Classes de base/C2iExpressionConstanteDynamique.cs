using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionConstante.
	/// </summary>
	[Serializable]
	public abstract class C2iExpressionConstanteDynamique : C2iExpressionAnalysable
	{
        public abstract string ConstanteName { get; }

        /// ///////////////////////////////////////////////////////
        public override CResultAErreur VerifieParametres()
        {
            return CResultAErreur.True;
        }

        /// ///////////////////////////////////////////////////////
        public override string GetString()
        {            return CaracteresControleAvant+ConstanteName;
        }

       
	}

	
	
}
