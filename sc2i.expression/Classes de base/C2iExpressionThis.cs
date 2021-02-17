using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionThis.
	/// </summary>
	[Serializable]
	public class C2iExpressionThis : C2iExpression
	{
        private Type m_typeThis = typeof(string);
		/// ///////////////////////////////////////////////////////
		public C2iExpressionThis ( )
		{
		}

		/// ///////////////////////////////////////////////////////
		public C2iExpressionThis ( Type type )
		{
			m_typeThis = type;
		}

		/// ///////////////////////////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return "THIS";
			}
		}

		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur SetTypeObjetInterroge ( CObjetPourSousProprietes objetPourSousProprietes, IFournisseurProprietesDynamiques fournisseur)
		{
            m_typeThis = objetPourSousProprietes.TypeAnalyse;
			return CResultAErreur.True;
		}

		/// ///////////////////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;
            result.Data = ctx.ObjetSource;
			return result;
		}

		/// ///////////////////////////////////////////////////////
		public override int GetNbParametresNecessaires()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////
		public override string GetString()
		{
			return CaracteresControleAvant+"This";
		}

		/// ///////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return new CTypeResultatExpression ( m_typeThis, false );
			}
		}

		/// ///////////////////////////////////////////////////////
		public override CResultAErreur VerifieParametres()
		{
			return CResultAErreur.True;
		}

		/// ///////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ///////////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if(  !result )
				return result;
			serializer.TraiteType ( ref m_typeThis );
			return result;
		}
					

		
	}

	/// ///////////////////////////////////////////////////////
	
}
