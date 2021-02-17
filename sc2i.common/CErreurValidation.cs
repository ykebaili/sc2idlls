using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CErreurValidation.
	/// </summary>
	[Serializable]
	public class CErreurValidation : CErreurSimple
	{
		private bool m_bIsAvertissement;
		
		/// //////////////////////////////////////////////////////////
		public CErreurValidation()
			:base()
		{
		}
		
		/// //////////////////////////////////////////////////////////
		public CErreurValidation( string strMes, bool bIsAvertissement )
			:base ( strMes )
		{
			m_bIsAvertissement = bIsAvertissement;
		}

		/// //////////////////////////////////////////////////////////
		public override bool IsAvertissement
		{
			get
			{
				return m_bIsAvertissement;
			}
		}

		/// //////////////////////////////////////////////////////////
		public static bool IsErreurBloquante ( CResultAErreur result )
		{
			if ( result )
				return false;
			foreach ( IErreur erreur in result.Erreur )
			{
                CErreurValidation erreurVal = (erreur as CErreurValidation);
				if (erreurVal == null)
					return true;
				if ( !erreurVal.IsAvertissement )
					return true;
			}
			return false;
		}

	}
}
