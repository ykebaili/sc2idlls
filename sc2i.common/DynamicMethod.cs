using System;
using System.Reflection;
using System.Collections;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de DynamicMethod.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
    public sealed class DynamicMethodAttribute : Attribute
	{
		private string m_strDescriptif = "";
		private string[] m_strDescParametres = new string[0];
		private Type m_typeRetourne = typeof(object);

		/// ///////////////////////////////////////////////////
		public DynamicMethodAttribute( string strDescriptif )
		{
			m_strDescriptif = strDescriptif;
			m_strDescParametres = new string[0];
		}
		/// ///////////////////////////////////////////////////
		public DynamicMethodAttribute( string strDescriptif, params string[] strDescParametres )
		{
			m_strDescriptif = strDescriptif;
			m_strDescParametres = strDescParametres;
		}
		
		/// ///////////////////////////////////////////////////
		public DynamicMethodAttribute ( Type typeRetourne, string strDescriptif )
		{
			m_strDescriptif = strDescriptif;
			m_typeRetourne = typeRetourne;
		}

		/// ///////////////////////////////////////////////////
		public DynamicMethodAttribute ( Type typeRetourne, string strDescriptif, params string[] strDescParametres )
		{
			m_strDescriptif = strDescriptif;
			m_strDescParametres = strDescParametres;
			m_typeRetourne = typeRetourne;
		}


		/// ///////////////////////////////////////////////////
		public string Descriptif
		{
			get
			{
				return m_strDescriptif;
			}
		}

		/// ///////////////////////////////////////////////////
		public string[] InfosParametres
		{
			get
			{
				return m_strDescParametres;
			}
		}

		/// ///////////////////////////////////////////////////
		public int GetNbParametres()
		{
			return m_strDescParametres.Length;
		}

		/// ///////////////////////////////////////////////////
		public string GetDescriptionParametre ( int nParametre )
		{
			if ( nParametre >= 0 && nParametre < m_strDescParametres.Length )
				return m_strDescParametres[nParametre];
			return "";
		}

		/// ///////////////////////////////////////////////////
		public Type ReturnType
		{
			get
			{
				return m_typeRetourne;
			}
		}

	}

    /// /////////////////////////////////////////////
    ///Tout objet qui est une liste et dont les méthodes dynamiques agissent
    ///de fait sur une liste.
    public interface IObjetAMethodesDynamiquesSurListe
    { }

	/// /////////////////////////////////////////////
	[AttributeUsage(AttributeTargets.Method)]
    public sealed class OptimizedByAttribute : Attribute
	{
		public readonly string[] Proprietes;

		/// <summary>
		/// Indique les propriétés à lire pour optimiser cette méthode dynamique
		/// </summary>
		/// <param name="strProps"></param>
		public OptimizedByAttribute ( params string[] strProps )
		{
			Proprietes = strProps;
		}
	}

	/// /////////////////////////////////////////////
	public class CInfoParametreMethodeDynamique
	{
		public readonly string Nom;
		public readonly string Description;
		private Type m_typeParametre;

		public CInfoParametreMethodeDynamique ( string strNom,
			string strDesc,
			Type tp )
		{
			Nom = strNom;
			Description = strDesc;
			m_typeParametre = tp;
		}

        public Type TypeParametre
        {
            get
            {
                return m_typeParametre;
            }
            set
            {
                m_typeParametre = value;
            }
        }

	}
	/// <summary>
	/// /////////////////////////////////////////////
	/// </summary>
	public class CInfoMethodeDynamique
	{
		string m_strNomMethode = "";
		string m_strDescription = "";
		Type m_typeRetour;
		ArrayList m_lstParametres = new ArrayList();

		public CInfoMethodeDynamique ( Type tp, string strNom )
		{
			MethodInfo method = tp.GetMethod(strNom);
			if ( method == null )
			{
				CMethodeSupplementaire methodSupp = CGestionnaireMethodesSupplementaires.GetMethod ( tp, strNom );
				if ( methodSupp == null )
				{
					m_strNomMethode = "#"+strNom+"#";
					m_strDescription = I.T("Method not found  :@1|30109 ",strNom);
					m_typeRetour = typeof(string);
				}
				else
				{
					m_strNomMethode = methodSupp.Name;
					m_strDescription = methodSupp.Description;
					m_lstParametres.AddRange ( methodSupp.InfosParametres );
				}
			}
			else
			{
				m_strNomMethode = method.Name;
				Object[] attribs = method.GetCustomAttributes ( typeof(DynamicMethodAttribute), true );
				if ( attribs.Length != 0)
				{
					DynamicMethodAttribute attrib = (DynamicMethodAttribute)attribs[0];
					m_strDescription = attrib.Descriptif;
					int nParametre = 0;
					foreach ( ParameterInfo info in method.GetParameters() )
					{
						m_lstParametres.Add ( new CInfoParametreMethodeDynamique ( info.Name, attrib.GetDescriptionParametre(nParametre), info.ParameterType ));
						nParametre++;
					}
				}
			}
		}

		/// /////////////////////////////////////////////
		public string NomMethode
		{
			get
			{
				return m_strNomMethode;
			}
		}

		/// /////////////////////////////////////////////
		/// 
		public string Descriptif
		{
			get
			{
				return m_strDescription;
			}
		}

		/// /////////////////////////////////////////////
		public Type TypeRetour
		{
			get
			{
				return m_typeRetour;
			}
		}

		/// /////////////////////////////////////////////
		public CInfoParametreMethodeDynamique[] InfosParametres
		{
			get
			{
				return (CInfoParametreMethodeDynamique[])m_lstParametres.ToArray(typeof(CInfoParametreMethodeDynamique));
			}
		}
	}

}
