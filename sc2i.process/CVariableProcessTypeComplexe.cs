using System;
using System.Collections;

using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CVariableProcessTypeComplexe.
	/// </summary>
	public class CVariableProcessTypeComplexe : CVariableDynamique
	{
		private CTypeResultatExpression m_type;
        // TESTDBKEYOK
        private CDbKey m_dbKeyIdInitial = null;

		/// ///////////////////////////////////////////
		public CVariableProcessTypeComplexe( )
			:base ( )
		{
		}

		/// ///////////////////////////////////////////
		public CVariableProcessTypeComplexe( CProcess process )
			:base ( process )
		{
		}

		/// ///////////////////////////////////////////
		public override string LibelleType
		{
			get
			{
				return I.T("Complex|20");
			}
		}

		/// ////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return m_type;
			}
		}

		/// ////////////////////////////////////////////////////////
		public void SetTypeDonnee ( CTypeResultatExpression type )
		{
			m_type = type;
		}

		/// ////////////////////////////////////////////////////////
        public CDbKey DbKeyElementInitial
		{
			get
			{
				return m_dbKeyIdInitial;
			}
			set
			{
				m_dbKeyIdInitial = value;
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
			return false;
		}


		/// ///////////////////////////////////////////
		private int GetNumVersion()
		{
			//return 1;
            return 2; // Passage de m_nIdInitial en DbKey
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
			I2iSerializable objet = m_type;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_type = (CTypeResultatExpression)objet;

            if (nVersion >= 1)
            {
                if (nVersion < 2)
                    //TESTDBKEYOK
                    serializer.ReadDbKeyFromOldId(ref m_dbKeyIdInitial, m_type != null?m_type.TypeDotNetNatif:null);
                else
                    serializer.TraiteDbKey(ref m_dbKeyIdInitial);
            }
            else
                m_dbKeyIdInitial = null;

			return result;
		}

		/// ///////////////////////////////////////////
		public override IList Valeurs
		{
			get
			{
				return new object[0];
			}
		}

		/// ///////////////////////////////////////////
        public object GetValeurParDefaut(CContexteDonnee contexte)
        {
            if (typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(m_type.TypeDotNetNatif))
            {
                try
                {
                    CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(m_type.TypeDotNetNatif, new object[] { contexte });
                    // TESTDBKEYOK
                    if (m_dbKeyIdInitial != null && objet.ReadIfExists(m_dbKeyIdInitial))
                        return objet;
                }
                catch { }
            }
            if (m_type.TypeDotNetNatif.GetCustomAttributes(typeof(VariableAutoAlloueeAttribute), true).Length > 0 && !m_type.IsArrayOfTypeNatif)
                return Activator.CreateInstance(m_type.TypeDotNetNatif, new object[0]);
            return null;
        }
	}
}
