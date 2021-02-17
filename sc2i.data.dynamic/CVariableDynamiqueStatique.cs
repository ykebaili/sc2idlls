using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Variable dynamique pouvant accueillir n'importe quel type de donn�e.
	/// Utilis�e pour stocker la copie de valeur d'une variable dynamique
	/// </summary>
	public class CVariableDynamiqueStatique : CVariableDynamique
	{
		private CTypeResultatExpression m_type = null;

		/// ///////////////////////////////////////////
		public CVariableDynamiqueStatique( )
			:base()
		{
		}

		/// ///////////////////////////////////////////
		public override string LibelleType
		{
			get
			{ 
				return I.T("Storage|16"); 
			}
		}

		/// ///////////////////////////////////////////
		public CVariableDynamiqueStatique( IElementAVariablesDynamiques elementAVariables )
			:base ( elementAVariables )
		{
		}


		/// ///////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return m_type;
			}
		}


		/// ///////////////////////////////////////////
		public void SetTypeDonnee ( CTypeResultatExpression type )
		{
				m_type = type;
		}
		
		/// ///////////////////////////////////////////
		///Indique si l'utilisateur choisis parmis des valeurs ou non (combo !)
		public override bool IsChoixParmis()
		{
			return false;
		}

		/// ///////////////////////////////////////////
		/// Indique si la variable correspond � un choix d'utilisateur
		public override bool IsChoixUtilisateur()
		{
			return false;
		}

		public override CResultAErreur VerifieValeur(object valeur)
		{
			CResultAErreur result = CResultAErreur.True;
			return result;
		}

		/// ///////////////////////////////////////////
		public override IList Valeurs
		{
			get
			{
				return new ArrayList();
			}
		}

		
		/// ///////////////////////////////////////////
		public override string ToString()
		{
			return Nom;
		}

	}
}
