using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CErreurChamp.
	/// </summary>
	[Serializable]
	public class CErreurChamp : CErreurValidation
	{
		public enum EnumCodeErreur
		{
			NonConforme = 0,
			ExisteDeja,
			VideNonAutorise,
			TailleIncorrecte
		}

		private string m_strDesignationChamp = "";
		private EnumCodeErreur m_codeErreur = EnumCodeErreur.NonConforme;
		
		/// //////////////////////////////////////////////////////////
		public CErreurChamp()
			:base()
		{
		}
		
		/// //////////////////////////////////////////////////////////
		public CErreurChamp( 
			string strChamp, 
			EnumCodeErreur code,
			string strMes, 
			bool bIsAvertissement )
			:base ( strMes, bIsAvertissement )
		{
			m_strDesignationChamp = strChamp;
			m_codeErreur = code;
		}


		/// //////////////////////////////////////////////////////////
		public string Champ
		{
			get
			{
				return m_strDesignationChamp;
			}
			set
			{
				m_strDesignationChamp = value;
			}
		}

		/// //////////////////////////////////////////////////////////
		public EnumCodeErreur Code
		{
			get
			{
				return m_codeErreur;
			}
			set
			{
				m_codeErreur = value;
			}
		}
		

	}
}
