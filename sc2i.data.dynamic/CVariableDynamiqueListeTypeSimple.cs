using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CVariableDynamiqueListeTypeSimple.
	/// </summary>
	public class CVariableDynamiqueListeTypeSimple : CVariableDynamique
	{
		private C2iTypeDonnee m_typeDonnee = new C2iTypeDonnee ( sc2i.data.dynamic.TypeDonnee.tString );
		
		/// ////////////////////////////////////////////////////////
		public CVariableDynamiqueListeTypeSimple()
		{
		}

		/// ///////////////////////////////////////////
		public CVariableDynamiqueListeTypeSimple( IElementAVariablesDynamiques elementAVariables )
			:base ( elementAVariables )
		{
		}

		/// ///////////////////////////////////////////
		public override string LibelleType
		{
			get
			{
				return I.T("Simple List|71");
			}
		}

		/// ////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return new CTypeResultatExpression(m_typeDonnee.TypeDotNetAssocie, true);
			}
		}

		/// ///////////////////////////////////////////
		public override bool IsChoixParmis()
		{
			return false;

		}

		/// ///////////////////////////////////////////
		public override bool IsChoixUtilisateur()
		{
			return true;
		}

		/// ////////////////////////////////////////////////////////
		public C2iTypeDonnee TypeDonnee2i
		{
			get
			{
				return m_typeDonnee;
			}
			set
			{
				m_typeDonnee = value;
			}
		}

		/// ///////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}
		
		/// ///////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if ( !result )
				return result;
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

	}
}
