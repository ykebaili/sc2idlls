using System;

using sc2i.common;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Permet de r�cuperer la valeur d'une variable sur
	/// objet en la castant dans le type qu'on veut
	/// </summary>
	public class CCasteurValeurString
	{
		private string m_strNomVariable;
		private string m_strValeur;
		
		/// /////////////////////////////////////
		public CCasteurValeurString( string strNomVariable, string strValeur)
		{
			m_strNomVariable = strNomVariable;
			m_strValeur = strValeur;
		}

		/// /////////////////////////////////////
		/// ///////////////////////////////////////////////////////
		[DynamicField("Name")]
		public string Nom
		{
			get
			{
				return ( string )m_strNomVariable;
			}
		}

		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Valeur texte de la variable.
		/// </summary>
		[DynamicField("Text value")]
		public string ValeurTexte
		{
			get
			{
				return m_strValeur;
			}
		}

		/// ///////////////////////////////////////////////////////
		/// <summary>
		/// Retourne la valeur enti�re de la variable<BR></BR>
		/// Si la conversion entre la valeur texte et le type 'Nombre entier' �choue,
		/// la valeur est 0.
		/// </summary>
		[DynamicField("Int value")]
		public int ValeurInt
		{
			get
			{
				try
				{
					return Convert.ToInt32 ( ValeurTexte );
				}
				catch
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// Retourne la valeur d�cimale de la variable<BR></BR>
		/// Si la conversion entre la valeur texte et le type 'Nombre d�cimal' �choue,
		/// la valeur est 0.
		/// </summary>
		[DynamicField("decimal value")]
		public Double ValeurDecimale
		{
			get
			{
				try
				{
					return CUtilDouble.DoubleFromString(ValeurTexte);
				}
				catch
				{
					return (double)0;
				}
			}
		}

		/// <summary>
		/// Retourne la valeur de la variable convertie en date<BR></BR>
		/// Si la conversion entre la valeur texte et le type 'date' �choue,
		/// la valeur est 1/1/1900 00:00 (1er janvier 1900 � minuit).
		/// </summary>
		[DynamicField("Date value")]
		public DateTime ValeurDate
		{
			get
			{
				return CUtilDate.GetDateFromString ( ValeurTexte, new DateTime ( 1900,1,1) );
			}
		}

		/// <summary>
		/// Retourne la valeur de la variable convertie en boolean<BR></BR>
		/// </summary>
		/// <remarks>
		/// La valeur est consider�e comme fausse si elle est �gale �
		/// la chaine vide, 0 ou false. Dans tous les autres cas,
		/// la valeur retourn�e est 'vrai'.
		/// </remarks>
		[DynamicField("bool value")]
		public bool ValeurBooleenne
		{
			get
			{
				if ( ValeurTexte == "0" ||
					ValeurTexte == "" ||
					ValeurTexte.ToString().ToUpper() == false.ToString().ToUpper() )
					return false;
				return true;
			}
		}

	}
}
