using System;


using System.Collections;


using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionMethodeOuPropriete.
	/// </summary>
	[Serializable]
	public abstract class C2iExpressionMethodeOuPropriete : C2iExpression
	{
		public C2iExpressionMethodeOuPropriete()
		{
			
		}

		/// ///////////////////////////////////////////////////////////
		public override bool CanBeArgumentExpressionObjet
		{
			get
			{
				return true;
			}
		}

		/// //////////////////////////////////////////////////////////////////////////////////
		public override CArbreDefinitionsDynamiques GetArbreProprietesAccedees ( CArbreDefinitionsDynamiques arbreEnCours )
		{
			if ( Parametres.Count != 2 )
				return arbreEnCours;
			CArbreDefinitionsDynamiques arbreObjet = Parametres2i[0].GetArbreProprietesAccedees ( arbreEnCours );
			return Parametres2i[1].GetArbreProprietesAccedees ( arbreObjet );
		}

		
		
		
	}
}
