using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CExceptionErreur.
	/// </summary>
	[Serializable]
	public class CExceptionErreur : Exception
	{
		private CPileErreur m_erreur;

#if PDA
#else
		///////////////////////////////////////////////////////////////////////:
		protected CExceptionErreur( System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext contexte)
			:base ( info, contexte)
		{
		}
#endif

		///////////////////////////////////////////////////////////////////////:
		public CExceptionErreur()
		{
		}

		///////////////////////////////////////////////////////////////////////:
		public CExceptionErreur( CPileErreur erreur )
		{
			if ( erreur != null )
				m_erreur = erreur;
			else
				m_erreur = new CPileErreur();
		}

		/////////////////////////////////////////////////////////////////////////
		public override string Message
		{
			get
			{
				if (m_erreur != null)
					return m_erreur.ToString();
				else
					return I.T("Exception error|30045");
			}
		}

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
        }
	}
}
