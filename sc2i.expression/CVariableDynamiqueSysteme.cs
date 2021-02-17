using System;
using System.Collections;

using sc2i.expression;
using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de CVariableCalculee.
	/// </summary>
    [ReplaceClass("sc2i.data.dynamic.CVariableDynamiqueSysteme")]
	public class CVariableDynamiqueSysteme : CVariableDynamique
	{
		private CTypeResultatExpression m_typeDonnee = new CTypeResultatExpression ( typeof(string), false );
		///////////////////////////////////////////////
		public CVariableDynamiqueSysteme()
		{
			
		}

		/// ///////////////////////////////////////////
		public CVariableDynamiqueSysteme( IElementAVariablesDynamiquesBase elementAVariables )
			:base ( elementAVariables )
		{
		}

		/// ///////////////////////////////////////////
		public override string LibelleType
		{
			get
			{
				return I.T("System|20146");
			}
		}
			
		///////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return m_typeDonnee;
			}
		}

		///////////////////////////////////////////////
		public void SetTypeDonnee ( CTypeResultatExpression typeResultat )
		{
			m_typeDonnee = typeResultat;
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
			I2iSerializable obj = m_typeDonnee;
			result = serializer.TraiteObject ( ref obj );
			m_typeDonnee = (CTypeResultatExpression)obj;
			return result;
		}

		/// ///////////////////////////////////////////
		public override bool IsChoixParmis()
		{
			return false;

		}

		/// ///////////////////////////////////////////
		public override bool IsChoixUtilisateur()
		{
			return false;
		}

		/// ///////////////////////////////////////////
		public override IList Valeurs
		{
			get
			{
				return new object[0];
			}
		}

	}

    //------------------------------------------------------------------
    /// <summary>
    /// Idem qu'une variable dynamique système, mais travaille par instance et 
    /// non pas par type
    /// </summary>
    [Serializable]
    [ReplaceClass("sc2i.data.dynamic.CVariableDynamiqueSystemeInstance")]
    public class CVariableDynamiqueSystemeInstance : CVariableDynamiqueSysteme, IVariableDynamiqueInstance
    {
        private object m_instance;
        ///////////////////////////////////////////////
		public CVariableDynamiqueSystemeInstance()
		{
			
		}

		/// ///////////////////////////////////////////
        public CVariableDynamiqueSystemeInstance(IElementAVariablesDynamiques elementAVariables)
			:base ( elementAVariables )
		{
		}

        /// ///////////////////////////////////////////
        public void SetInstance ( object instance )
        {
            m_instance = instance;
            if ( m_instance != null )
                SetTypeDonnee ( new CTypeResultatExpression ( m_instance.GetType(), false ));
        }

        /// //////////////////////////////////////////
        public CObjetPourSousProprietes GetObjetPourAnalyseSousProprietes()
        {
            return new CObjetPourSousProprietes(m_instance);
        }
    }
}
