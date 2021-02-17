using System;
using sc2i.common;
using sc2i.expression;
using sc2i.expression.expressions;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Opérateur Et
	/// </summary>
	[Serializable]
	public class CComposantFiltreDynamiqueEt : CComposantFiltreDynamiqueOperateurLogique
	{
		/// ///////////////////////////////////////////////////
		public CComposantFiltreDynamiqueEt()
		{
		}

		public override string GetIdComposantFiltreOperateur()
		{
			return CComposantFiltreOperateur.c_IdOperateurEt;
		}

		/// ///////////////////////////////////////////////////
		public override string Description
		{
			get
			{
				return I.T("And|30");
			}
		}

        protected override sc2i.expression.C2iExpression GetExpressionOperateurLogique()
        {
            return new C2iExpressionET();
        }

	}

}
