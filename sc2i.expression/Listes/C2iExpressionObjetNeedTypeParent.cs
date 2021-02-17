using System;

using sc2i.common;

namespace sc2i.expression
{
	/// <summary>
	/// Classe de base pour les expression objet qui nécessitent de stocker le type
	/// de l'objet source pour savoir ce qu'elles retournent
	/// </summary>
	[Serializable]
	public abstract class C2iExpressionObjetNeedTypeParent : C2iExpressionMethodeAnalysable
	{
		private CObjetPourSousProprietes m_objetPourAnalyseSource = null;
		
		/// //////////////////////////////////////////////////////
		public C2iExpressionObjetNeedTypeParent()
		{
		}

		/// //////////////////////////////////////////////////////
		public CObjetPourSousProprietes ObjetPourAnalyseSourceConnu
		{
			get
			{
				return m_objetPourAnalyseSource;
			}
			set
			{
				m_objetPourAnalyseSource = value;
			}
		}

		/// ///////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}


		/// ///////////////////////////////////////////////////////////
		public override CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.Serialize ( serializer );
			if ( !result )
				return result;
			bool bHasTypeSource = m_objetPourAnalyseSource != null;
			serializer.TraiteBool ( ref bHasTypeSource );
			if ( bHasTypeSource )
			{
				switch ( serializer.Mode )
				{
					case ModeSerialisation.Ecriture :
                        CTypeResultatExpression tp = m_objetPourAnalyseSource.TypeResultatExpression;
                        result = tp.Serialize(serializer);
						break;
					case ModeSerialisation.Lecture :
						CTypeResultatExpression tpLecture = new CTypeResultatExpression();
                        result = tpLecture.Serialize(serializer);
                        m_objetPourAnalyseSource = new CObjetPourSousProprietes(tpLecture);
						break;
				}
			}
			else
				m_objetPourAnalyseSource = null;
			return result;
		}

		
		/// //////////////////////////////////// /////////////////////////////////
		public override CResultAErreur SetTypeObjetInterroge ( CObjetPourSousProprietes objetPourSousProprietes, IFournisseurProprietesDynamiques fournisseur)
		{
			CResultAErreur result = base.SetTypeObjetInterroge ( objetPourSousProprietes, fournisseur );
			m_objetPourAnalyseSource = objetPourSousProprietes;
			return result;
		}
	}
}
