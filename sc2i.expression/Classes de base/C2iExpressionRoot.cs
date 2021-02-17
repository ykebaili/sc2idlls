using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Description résumée de C2iExpressionRoot.
	/// </summary>
	[Serializable]
	public class C2iExpressionRoot : C2iExpression
	{
        private Type m_typeRoot = typeof(string);
		private CObjetPourSousProprietes m_objetPourSousProprietes = null;
		/// ///////////////////////////////////////////////////////
		public C2iExpressionRoot ( )
		{
		}

		/// ///////////////////////////////////////////////////////
		public C2iExpressionRoot ( CObjetPourSousProprietes objetPourSousProprietes )
		{
			if ( objetPourSousProprietes != null )
				m_typeRoot = objetPourSousProprietes.TypeAnalyse;
			m_objetPourSousProprietes = objetPourSousProprietes;
		}

		/// ///////////////////////////////////////////////////////
		public override string IdExpression
		{
			get
			{
				return "Root";
			}
		}

		/// ///////////////////////////////////////////////////////
		public override CObjetPourSousProprietes GetObjetPourSousProprietes()
		{
			if (m_objetPourSousProprietes != null)
				return m_objetPourSousProprietes;
			return base.GetObjetPourSousProprietes();
		}


		/// ///////////////////////////////////////////////////////
		protected override CResultAErreur ProtectedEval ( CContexteEvaluationExpression ctx )
		{
			CResultAErreur result = CResultAErreur.True;
			result.Data = ctx.ObjetBaseRacine;
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
			return CaracteresControleAvant+"Root";
		}

		/// ///////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeDonnee
		{
			get
			{
				return new CTypeResultatExpression ( m_typeRoot, false );
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
			serializer.TraiteType ( ref m_typeRoot );
			return result;
		}
					

		
	}

	/// ///////////////////////////////////////////////////////
	
}
