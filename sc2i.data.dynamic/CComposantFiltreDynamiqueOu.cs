using System;
using sc2i.common;
using sc2i.expression.expressions;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Opérateur Ou
	/// </summary>
	[Serializable]
	public class CComposantFiltreDynamiqueOu : CComposantFiltreDynamiqueOperateurLogique
	{
		/// ///////////////////////////////////////////////////
		public CComposantFiltreDynamiqueOu()
		{
		}

		/// ///////////////////////////////////////////////////
		public override string GetIdComposantFiltreOperateur()
		{
			return CComposantFiltreOperateur.c_IdOperateurOu;
		}

		/// ///////////////////////////////////////////////////
		public override string Description
		{
			get
			{
				return I.T("Or|31");
			}
		}

        /// ///////////////////////////////////////////////////
        protected override sc2i.expression.C2iExpression GetExpressionOperateurLogique()
        {
            return new C2iExpressionOU();
        }
	}
}

