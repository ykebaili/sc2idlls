using System;
using System.Collections;

using sc2i.expression;
using sc2i.common;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CVariableCalculee.
	/// </summary>
    [AutoExec("Autoexec")]
	public class CVariableDynamiqueCalculee : CVariableDynamique, IVariableDynamiqueCalculee
	{
		private C2iExpression m_expressionCalcul = null;
		///////////////////////////////////////////////
		public CVariableDynamiqueCalculee()
		{
			
		}

		/// ///////////////////////////////////////////
		public CVariableDynamiqueCalculee( IElementAVariablesDynamiquesBase elementAVariables )
			:base ( elementAVariables )
		{
		}

        /// ///////////////////////////////////////////
        public static void Autoexec()
        {
            CGestionnaireVariablesDynamiques.RegisterTypeVariable(typeof(CVariableDynamiqueCalculee), I.T("Calculated|20079"));
        }


		/// ///////////////////////////////////////////
		public override string LibelleType
		{
			get
			{
				return I.T("Calculated|35");
			}
		}

			/// ///////////////////////////////////////////
			public C2iExpression Expression
		{
			get
			{
				return m_expressionCalcul;
			}
			set
			{
				m_expressionCalcul = value;
			}
		}

		///////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				if ( m_expressionCalcul != null )
					return m_expressionCalcul.TypeDonnee;
				return new CTypeResultatExpression(typeof(string), false );
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
			I2iSerializable obj = m_expressionCalcul;
			result = serializer.TraiteObject ( ref obj );
			m_expressionCalcul = (C2iExpression)obj;
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


		/// ///////////////////////////////////////////
		public object GetValeur ( IElementAVariablesDynamiquesAvecContexteDonnee elementInterroge )
		{
			if ( Expression == null )
				return null;
			CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(elementInterroge);
			ctx.AttacheObjet ( typeof(CContexteDonnee), elementInterroge.ContexteDonnee );
			CResultAErreur result = Expression.Eval ( ctx );
			if ( !result )
				return null;
			return result.Data;
		}




		/// ///////////////////////////////////////////
        public C2iExpression FormuleDeCalcul
        {
            get { return Expression; }
        }
    }
}
