using System;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de IIndicateurProgression.
	/// </summary>
	public interface IIndicateurProgression
	{
		void PushSegment ( int nMin, int nMax );
		void SetBornesSegment ( int nMin, int nMax );
		void PopSegment();
		void SetInfo(string strInfo);
		void SetValue ( int nValue );
		void PushLibelle(string strInfo);
		void PopLibelle();

		void Masquer(bool bMasquer);
	
		//Retourne true si l'annulation du traitement a été demandée par l'utilisateur
		bool CancelRequest{get;}

		bool CanCancel { get;set; }
	}

	public class CConteneurIndicateurProgression : MarshalByRefObject, IIndicateurProgression
	{
		protected static Type m_typeConteneurParDefaut = typeof(CConteneurIndicateurProgression);

		protected IIndicateurProgression m_indicateur;


		protected CConteneurIndicateurProgression (  )
		{
		}

		public virtual IIndicateurProgression Indicateur
		{
			get
			{
				return m_indicateur;
			}
			set
			{
				m_indicateur = value;
			}
		}

		public void PushSegment(int nMin, int nMax)
		{
			try
			{
				if ( m_indicateur != null )
					m_indicateur.PushSegment ( nMin, nMax );
			}
			catch
			{
			}
		}

		public void PushLibelle(string strInfo)
		{
			try
			{
				if (m_indicateur != null)
					m_indicateur.PushLibelle(strInfo);
			}
			catch
			{ }
		}
		public void PopLibelle()
		{
			try
			{
				if (m_indicateur != null)
					m_indicateur.PopLibelle();
			}
			catch
			{ }
		}

		public void SetBornesSegment(int nMin, int nMax)
		{
			try
			{
				if ( m_indicateur != null )
					m_indicateur.SetBornesSegment ( nMin, nMax );
			}
			catch
			{
			}
		}

		public void PopSegment()
		{
			try
			{
				if ( m_indicateur != null )
					m_indicateur.PopSegment();
			}
			catch
			{
			}
		}

		public void SetValue(int nValue)
		{
			try
			{
				if ( m_indicateur != null )
					m_indicateur.SetValue ( nValue );
			}
			catch ( Exception e )
			{
			}
		}

		public void SetInfo(string strInfo)
		{
			try
			{
				if ( m_indicateur != null )
					m_indicateur.SetInfo ( strInfo );
			}
			catch
			{
			}
		}

		public bool CancelRequest
		{
			get
			{
				try
				{
					if ( m_indicateur != null )
						return m_indicateur.CancelRequest;
				}
				catch
				{
				}
				return false;

			}
		}

		public bool CanCancel
		{
			get
			{
				try
				{
					if ( m_indicateur != null )
						return m_indicateur.CanCancel;
				}
				catch
				{
				}
				return false;
			}
			set
			{
				if ( m_indicateur != null )
					m_indicateur.CanCancel = value;
			}
		}


		public void Masquer(bool bMasquer)
		{
			if (m_indicateur != null)
				m_indicateur.Masquer(bMasquer);
		}





#if PDA
#else
		public static CConteneurIndicateurProgression GetConteneur ( IIndicateurProgression indicateur )
		{
            CConteneurIndicateurProgression conteneurIndic = (indicateur as CConteneurIndicateurProgression);
            if (conteneurIndic != null)
                return conteneurIndic;
			CConteneurIndicateurProgression conteneur = ( CConteneurIndicateurProgression )Activator.CreateInstance ( m_typeConteneurParDefaut, true );
			conteneur.Indicateur = indicateur;
			return conteneur;
		}
#endif
	}
}
