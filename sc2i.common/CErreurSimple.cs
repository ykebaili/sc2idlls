using System;

namespace sc2i.common
{
	/// <summary>
	/// Erreur de base
	/// </summary>
	[Serializable]
	public class CErreurSimple : IErreur
	{
		protected string	m_strErreur="";
		
		public CErreurSimple ()
		{
		}

		public CErreurSimple( string strMes )
		{
			m_strErreur = strMes;
		}

		public string Message
		{
			get { return m_strErreur;}
		}

        public virtual bool IsAvertissement
        {
            get
            {
                return false;
            }
        }
	}
}
