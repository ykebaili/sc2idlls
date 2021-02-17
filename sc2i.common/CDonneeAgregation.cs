using System;
using System.Collections;
using System.Globalization;

namespace sc2i.common
{
	public enum OperationsAgregation
	{
		Sum = 0,
		Number=1,
		DistinctNumber=2,
		Max=3,
		Min=4,
		Average=5,
		None = 100
	};

	/// <summary>
	/// Permet de calcule des données agregés par une opération d'agrégation.
	/// </summary>
	/// <remarks>
	/// Dans le principe : on alloue un CDOnneeAgregation en appelant la fonction
	/// statique CDonneeAgregation.GetNewDonneeForOperation.<BR></BR>
	/// On appelle la fonction PrepareCalcule qui initialise la donnée pour le calcule<BR></BR>
	/// On balaie les données à agréger en passant les valeurs à la données via la
	/// fonction IntegreDonnee<BR></BR>
	/// A la fin du parcours des données, la fonction GetValeur finale retourne
	/// la valeur agregée
	/// </remarks>
	public abstract class CDonneeAgregation
	{
		protected CDonneeAgregation()
		{
		}

		/// <summary>
		/// Initialise la donnée d'agrégation pour le calcul
		/// </summary>
		public abstract void PrepareCalcul();
		
		/// <summary>
		/// Intègre une donnée
		/// </summary>
		public abstract void IntegreDonnee(object valeur);
		
		/// <summary>
		/// Retourne la valeur finale d'agrégation
		/// </summary>
		/// <returns></returns>
		public abstract object GetValeurFinale();

		public static CDonneeAgregation GetNewDonneeForOperation ( OperationsAgregation operation )
		{
			switch ( operation )
			{
				case OperationsAgregation.Sum :
					return new CDonneeAgregationSomme();
				case OperationsAgregation.Number :
					return new CDonneeAgregationNombre();
				case OperationsAgregation.DistinctNumber :
					return new CDonneeAgregationNombreDistinct();
				case OperationsAgregation.Max :
					return new CDonneeAgregationMax();
				case OperationsAgregation.Min :
					return new CDonneeAgregationMin();
				case OperationsAgregation.Average :
					return new CDonneeAgregationMoyenne();
			}
			return null;
		}
	}

	//----------------------------------------------
	public class CDonneeAgregationSomme : CDonneeAgregation
	{
		private double m_dSomme;
		
		//----------------------------------------------
		public CDonneeAgregationSomme()
		{
		}

		//----------------------------------------------
		public override void PrepareCalcul()
		{
			m_dSomme = 0;
		}

		//----------------------------------------------
		public override void IntegreDonnee(object valeur)
		{
			try
			{
				double dVal = Convert.ToDouble ( valeur, CultureInfo.InvariantCulture );
				m_dSomme += dVal;
			}
			catch 
			{
			}
		}

		//----------------------------------------------
		public override object GetValeurFinale()
		{
			return m_dSomme;
		}
	}

	//----------------------------------------------
	public class CDonneeAgregationNombre : CDonneeAgregation
	{
		private int m_nNombre;
		
		//----------------------------------------------
		public CDonneeAgregationNombre()
		{
		}

		//----------------------------------------------
		public override void PrepareCalcul()
		{
			m_nNombre = 0;
		}

		//----------------------------------------------
		public override void IntegreDonnee(object valeur)
		{
			m_nNombre++;
		}

		//----------------------------------------------
		public override object GetValeurFinale()
		{
			return m_nNombre;
		}
	}

	//----------------------------------------------
	public class CDonneeAgregationNombreDistinct : CDonneeAgregation
	{
		private Hashtable m_tableValeurs = new Hashtable();
		
		//----------------------------------------------
		public CDonneeAgregationNombreDistinct()
		{
		}

		//----------------------------------------------
		public override void PrepareCalcul()
		{
			m_tableValeurs.Clear();
		}

		//----------------------------------------------
		public override void IntegreDonnee(object valeur)
		{
			if ( valeur == null )
				m_tableValeurs[DBNull.Value] = true;
			else
				m_tableValeurs[valeur] = true;
		}

		//----------------------------------------------
		public override object GetValeurFinale()
		{
			return m_tableValeurs.Count;
		}
	}

	//----------------------------------------------
	public class CDonneeAgregationMin : CDonneeAgregation
	{
		private object m_objValeurMin;
		
		//----------------------------------------------
		public CDonneeAgregationMin()
		{
		}

		//----------------------------------------------
		public override void PrepareCalcul()
		{
			m_objValeurMin = null;
		}

		//----------------------------------------------
		public override void IntegreDonnee(object valeur)
		{
			if ( m_objValeurMin == null )
				m_objValeurMin = valeur;
			else
			{
				try
				{
					if ( m_objValeurMin is IComparable && valeur is IComparable)
						if ( ((IComparable)m_objValeurMin).CompareTo ( valeur ) < 0 )
							m_objValeurMin = valeur;
				}
				catch {}
			}
		}

		//----------------------------------------------
		public override object GetValeurFinale()
		{
			return m_objValeurMin;
		}
	}

	//----------------------------------------------
	public class CDonneeAgregationMax : CDonneeAgregation
	{
		private object m_objValeurMax;
		
		//----------------------------------------------
		public CDonneeAgregationMax()
		{
		}

		//----------------------------------------------
		public override void PrepareCalcul()
		{
			m_objValeurMax = null;
		}

		//----------------------------------------------
		public override void IntegreDonnee(object valeur)
		{
			if ( m_objValeurMax == null )
				m_objValeurMax = valeur;
			else
			{
				try
				{
					if ( m_objValeurMax is IComparable && valeur is IComparable)
						if ( ((IComparable)m_objValeurMax).CompareTo ( valeur ) > 0 )
							m_objValeurMax = valeur;
				}
				catch {}
			}
		}

		//----------------------------------------------
		public override object GetValeurFinale()
		{
			return m_objValeurMax;
		}
	}

	//----------------------------------------------
	public class CDonneeAgregationMoyenne : CDonneeAgregation
	{
		private double m_dSomme;
		private int m_nNombre;
		
		//----------------------------------------------
		public CDonneeAgregationMoyenne()
		{
		}

		//----------------------------------------------
		public override void PrepareCalcul()
		{
			m_dSomme = 0;
			m_nNombre = 0;
		}

		//----------------------------------------------
		public override void IntegreDonnee(object valeur)
		{
			try
			{
				double dVal = Convert.ToDouble ( valeur, CultureInfo.InvariantCulture );
				m_dSomme += dVal;
				m_nNombre++;
			}
			catch 
			{
			}
		}

		//----------------------------------------------
		public override object GetValeurFinale()
		{
			if ( m_nNombre > 0 )
				return m_dSomme/((double)m_nNombre);
			return 0.0d;
		}
	}
}

