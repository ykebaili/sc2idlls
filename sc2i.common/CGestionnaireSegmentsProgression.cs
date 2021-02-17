using System;
using System.Collections;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de IIndicateurDeProgression.
	/// </summary>
	public class CGestionnaireSegmentsProgression
	{
		private class CSegmentProgression
		{
			public readonly int MinReel;
			public readonly int MaxReel;
			private int m_nValeurDuMin;
			private int m_nValeurDuMax = 100;
			
			/// ////////////////////////////////////////////////////////////
			public CSegmentProgression ( int nMinReel, int nMaxReel )
			{
				MinReel = nMinReel;
				MaxReel = nMaxReel;
			}

			/// ///////////////////////////////////////////////
			public int ValeurDuMin
			{
				get
				{
					return m_nValeurDuMin;
				}
				set
				{
					m_nValeurDuMin = value;
				}
			}

			/// ///////////////////////////////////////////////
			public int ValeurDuMax
			{
				get
				{
					return m_nValeurDuMax;
				}
				set
				{
					m_nValeurDuMax = value;
				}
			}

			/// ///////////////////////////////////////////////
			public double GetValeurReelle ( double dValeur )
			{
				if ( dValeur < ValeurDuMin )
					dValeur = ValeurDuMin;
				if ( dValeur > ValeurDuMax )
					dValeur = ValeurDuMax;
				double dAmplitudeVisible = ValeurDuMax-ValeurDuMin;
				double dAmplitudeReelle = MaxReel-MinReel;
				return (dValeur-((double)ValeurDuMin))*dAmplitudeReelle/dAmplitudeVisible+(double)MinReel;
			}
		}


		private ArrayList m_lstSegments = new ArrayList();

		/// ///////////////////////////////////////////////
		public CGestionnaireSegmentsProgression()
		{
			PushSegment ( 0, 100 );
		}

		/// ///////////////////////////////////////////////
		public void PushSegment ( int nMin, int nMax )
		{
			m_lstSegments.Insert (0,  new CSegmentProgression(nMin, nMax) );
		}

		/// ///////////////////////////////////////////////
		public void PopSegment ( )
		{
			if ( m_lstSegments.Count > 1 )
				m_lstSegments.RemoveAt ( 0 );
		}

		/// ///////////////////////////////////////////////
		private CSegmentProgression SegmentEnCours
		{
			get
			{
				return (CSegmentProgression)m_lstSegments[0];
			}
		}
		

		/// ///////////////////////////////////////////////
		public int MinValue
		{
			get
			{
				return SegmentEnCours.ValeurDuMin;
			}
			set
			{
				SegmentEnCours.ValeurDuMin = value;
			}
		}

		/// ///////////////////////////////////////////////
		public int MaxValue
		{
			get
			{
				return SegmentEnCours.ValeurDuMax;
			}
			set
			{
				SegmentEnCours.ValeurDuMax = value;
			}
		}

		/// ///////////////////////////////////////////////
		public int GetValeurReelle ( int nValeur )
		{
			double dValeur = nValeur;
			foreach( CSegmentProgression segment in m_lstSegments )
				dValeur = segment.GetValeurReelle(dValeur);
			return (int)dValeur;
		}
	}
}
