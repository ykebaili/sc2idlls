using System;
using sc2i.common;

namespace sc2i.data
{
	public delegate void ObjetDonneeCancelEventHandler(object sender, CObjetDonneeCancelEventArgs args);
	public delegate void ObjetDonneeEventHandler(object sender, CObjetDonneeEventArgs args);
	public delegate void ObjetDonneeResultEventHandler(object sender, CObjetDonneeResultEventArgs args);
	public delegate void ResultEventHandler ( object sender, ref CResultAErreur result );

	/// <summary>
	/// Description résumée de CObjetDonneeEventARgs.
	/// </summary>
	////////////////////////////////////////////////////////////
	public class CObjetDonneeEventArgs
	{
		private CObjetDonnee m_objetDonnee;

		public CObjetDonneeEventArgs( CObjetDonnee objet)
		{
			m_objetDonnee = objet;
		}

		public CObjetDonnee Objet
		{
			get
			{
				return m_objetDonnee;
			}
		}
	}

	////////////////////////////////////////////////////////////
	public class CObjetDonneeCancelEventArgs : CObjetDonneeEventArgs
	{
		private bool m_bCancel = false;
		public CObjetDonneeCancelEventArgs ( CObjetDonnee objet )
			:base ( objet )
		{
		}

		public bool Cancel
		{
			get
			{
				return m_bCancel;
			}
			set
			{
				m_bCancel = value;
			}
		}
	}

	////////////////////////////////////////////////////////////
	public class CObjetDonneeResultEventArgs : CObjetDonneeEventArgs
	{
		private CResultAErreur m_result = CResultAErreur.True;

		public CObjetDonneeResultEventArgs ( CObjetDonnee objet )
			:base ( objet )
		{
		}

		public CResultAErreur Result
		{
			get
			{
				return m_result;
			}
			set
			{
				m_result = value;
			}
		}
	}
}
