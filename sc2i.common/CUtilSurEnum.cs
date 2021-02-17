using System;
using System.Collections;
using System.Collections.Generic;


namespace sc2i.common
{
	/// <summary>
	/// Opérations sur enums. Tres utile pour les combobox
	/// </summary>
	public static class CUtilSurEnum
	{
		public class CCoupleEnumLibelle
		{
			private int m_nValeur;
			private string m_strLibelle;
			public CCoupleEnumLibelle ( int nValeur, string strLibelle )
			{
				m_nValeur = nValeur;
				m_strLibelle = strLibelle;
			}

			public override string ToString()
			{
				return m_strLibelle;;
			}

			public override bool Equals(object obj)
			{
				if ( obj is int )
					return Valeur == (int)obj;
                CCoupleEnumLibelle couple = (obj as CCoupleEnumLibelle);
				if ( couple != null )
					return Valeur == couple.Valeur;
				return false;
			}

			public override int GetHashCode()
			{
				return m_strLibelle.GetHashCode()+Valeur;
			}

			/// ////////////////////////////
			[DescriptionField]
			public string Libelle
			{
				get
				{
					return m_strLibelle;
				}
			}

			/// ////////////////////////////
			public int Valeur
			{
				get
				{
					return m_nValeur;
				}
			}

		}
		/// <summary>
		/// Isole les mots dans le nom d'enum grâce aux majuscules
		/// et insère des espaces entre eux
		/// </summary>
		/// <param name="strNomEnum"></param>
		/// <returns></returns>

		public static string GetNomConvivial ( string strNomEnum )
		{
			string strNomConvivial = "";
			foreach ( char cTmp in strNomEnum.ToCharArray() )
			{
				if ( String.IsNullOrEmpty(strNomConvivial) )
					strNomConvivial += cTmp;
				else
				{
					if ( cTmp >='A' && cTmp <='Z' )
						strNomConvivial += " ";
					strNomConvivial += cTmp;
				}
			}
			return strNomConvivial;
		}

		public static CCoupleEnumLibelle[] GetCouplesFromEnum ( Type enumType )
		{
			if ( !enumType.IsEnum )
				return new CCoupleEnumLibelle[0];
			ArrayList lstCouple = new ArrayList();
			foreach ( int nVal in Enum.GetValues ( enumType ) )
			{
				lstCouple.Add ( new CCoupleEnumLibelle ( nVal, GetNomConvivial ( Enum.GetName( enumType, nVal ) ) ) );
			}
			return ( CCoupleEnumLibelle[] )lstCouple.ToArray(typeof(CCoupleEnumLibelle));
		}


		/// <summary>
		/// Le type envoyé doit hériter directement de CEnumALibelle
		/// </summary>
		/// <param name="tp"></param>
		/// <returns></returns>
		public static IEnumALibelle[] GetEnumsALibelle(Type tp)
		{
			try
			{
				ArrayList lstVals = new ArrayList();
				foreach (object statut in Enum.GetValues(tp.BaseType.GetGenericArguments()[0]))
					lstVals.Add(Activator.CreateInstance(tp, new object[] { statut }));

				return (IEnumALibelle[])lstVals.ToArray(typeof(IEnumALibelle));
			}
			catch
			{
				return null;
			}
		}


		//[DynamicField("Code")]
		//public abstract int CodeInt { get;set;}

		//[DynamicField("Label")]
		//public abstract string Libelle { get;}
	}
}
