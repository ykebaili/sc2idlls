using System;

using sc2i.common;

namespace sc2i.data.dynamic
{
	public enum TypeDonnee
	{
		tEntier = 0,
		tDouble,
		tString,
		tDate,
		tBool,
		tObjetDonneeAIdNumeriqueAuto
		
	};
	/// <summary>
	/// Description résumée de C2iTypeDonnee.
	/// </summary>
	[Serializable]
	public class C2iTypeDonnee
	{
		public const string c_ConstanteNull = "NULL";
		private TypeDonnee m_typeDonnee = TypeDonnee.tEntier;
		private Type m_typeEntite = null;


		/// /////////////////////////////////////////////////////////
		public C2iTypeDonnee( TypeDonnee type)
		{
			m_typeDonnee = type;
		}

		/// /////////////////////////////////////////////////////////
		public C2iTypeDonnee(TypeDonnee type, Type typeEntite)
		{
			m_typeDonnee = type;
			m_typeEntite = typeEntite;
		}

		/// /////////////////////////////////////////////////////////
		public static string GetLibelleType ( TypeDonnee type )
		{
			switch ( type )
			{
				case TypeDonnee.tEntier :
					return I.T("Integer|20");
				case TypeDonnee.tDouble : 
					return I.T("Decimal number|21");
				case TypeDonnee.tString :
					return I.T("Text|22");
				case TypeDonnee.tDate :
					return I.T("Date / Hour|23");
				case TypeDonnee.tBool :
					return I.T("Yes / No|24");
				case TypeDonnee.tObjetDonneeAIdNumeriqueAuto :
					return I.T("Entity|25");
			}
			return "";
		}

		/// /////////////////////////////////////////////////////////
		public static Type GetTypeDotNetFor(TypeDonnee tp, Type typeEntite)
		{
			switch (tp)
			{
				case TypeDonnee.tEntier:
					return typeof(int);
				case TypeDonnee.tDouble:
					return typeof(double);
				case TypeDonnee.tString:
					return typeof(string);
				case TypeDonnee.tDate:
					return typeof(DateTime);
				case TypeDonnee.tBool:
					return typeof(bool);
				case TypeDonnee.tObjetDonneeAIdNumeriqueAuto:
					if (typeEntite != null)
						return typeEntite;
					return typeof(CObjetDonneeAIdNumerique);
			}
			return null;
		}

		/// /////////////////////////////////////////////////////////
		public Type TypeDotNetAssocie
		{
			get
			{
				return GetTypeDotNetFor (TypeDonnee, m_typeEntite );
			}
		}


		/// /////////////////////////////////////////////////////////
		public string Libelle
		{
			get
			{
				return GetLibelleType ( m_typeDonnee );
			}
		}

		/// /////////////////////////////////////////////////////////
		public TypeDonnee TypeDonnee
		{
			get
			{
				return m_typeDonnee;
			}
			set
			{
				m_typeDonnee = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public override string ToString()
		{
			return Libelle;
		}

		/// /////////////////////////////////////////////////////////
		public override int GetHashCode()
		{
			return (int)TypeDonnee;
		}

		/// /////////////////////////////////////////////////////////
		public override bool Equals ( object obj )
		{
			if ( !(obj is C2iTypeDonnee))
				return false;
			return ((C2iTypeDonnee)obj).TypeDonnee == TypeDonnee;
		}

		/// /////////////////////////////////////////////////////////
		public object ObjectToType ( object valeur, CContexteDonnee contexte )
		{
			try
			{
				return Convert.ChangeType ( valeur, TypeDotNetAssocie );
			}
			catch
			{
				if ( valeur == null )
					return null;
				return StringToType ( valeur.ToString(), contexte );
			}
		}

		/// /////////////////////////////////////////////////////////
		public bool IsDuBonType ( object valeur )
		{
			try
			{
				if ( valeur == null )
					return true;
				return TypeDotNetAssocie.IsAssignableFrom(valeur.GetType());
			}
			catch
			{
				return false;
			}
		}
		
		/// /////////////////////////////////////////////////////////
		public static object StringToType ( TypeDonnee type, string strTexte, CContexteDonnee contexteDonnee )
		{
			if ( strTexte.ToUpper() == c_ConstanteNull) 
				return null;
			switch ( type ) 
			{
				case TypeDonnee.tBool :
					return strTexte.ToString() == "1" || strTexte.ToUpper()== "TRUE" ;
				case TypeDonnee.tDate :
					try
					{
						return Convert.ChangeType ( strTexte, typeof(DateTime), null );
					}
					catch
					{
						//Tente le format sc2i de date chaine
						try
						{
							return CUtilDate.FromUniversalString(strTexte);
						}
						catch
						{
							return null;
						}
					}
				case TypeDonnee.tDouble :
					try
					{
						return CUtilDouble.DoubleFromString(strTexte);
					}
					catch
					{
						return null;
					}
				case TypeDonnee.tEntier :
					try
					{
						return Convert.ChangeType ( strTexte, typeof ( int ), null );
					}
					catch
					{
						return null;
					}
				case TypeDonnee.tString :
					return strTexte;
				case TypeDonnee.tObjetDonneeAIdNumeriqueAuto :
					try
					{
						if (contexteDonnee == null)
							return null;
						//Syntaxe : classe|id
						int nPos = strTexte.LastIndexOf("|");
						if (nPos < 0)
							return null;
						string strClasse = strTexte.Substring(0, nPos);
						int nId = Int32.Parse(strTexte.Substring(nPos + 1));
						Type tp = CActivatorSurChaine.GetType(strClasse, true);
						if (tp != null)
						{
							CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[] { contexteDonnee });
							if (objet.ReadIfExists(nId))
								return objet;
						}
						return null;
					}
					catch
					{
						return null;
					}

			}
			return null;
		}


		/// /////////////////////////////////////////////////////////
		public object StringToType ( string  strTexte, CContexteDonnee contexte )
		{
			return StringToType( TypeDonnee, strTexte, contexte );
		}

		/// /////////////////////////////////////////////////////////
		public static string TypeToString ( object valeur )
		{
			if ( valeur == null )
				return "";
			if ( valeur.GetType() ==  typeof(bool))
				return ((bool)valeur)?"1":"0";
			else if ( valeur.GetType() == typeof(double))
				return valeur.ToString();
			else if ( valeur.GetType() == typeof(int))
				return valeur.ToString();
			else if ( valeur.GetType() == typeof(DateTime))
				return CUtilDate.GetUniversalString((DateTime)valeur);
			return valeur.ToString();
		}
	}
}
