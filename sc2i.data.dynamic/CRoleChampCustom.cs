using System;
using System.Collections;
using sc2i.common;

namespace sc2i.data.dynamic
{
	
	public class CRoleChampCustom: IComparable
	{
		//Tables associant les Id de roles aux roles
		private static Hashtable m_tableRoles = new Hashtable();

		private string m_strCodeRole = "";
		private string m_strLibelleRole = "";
		private Type m_typeAssocie = null;
		private Type[] m_typesDefinisseurs = null;

		/// ////////////////////////////////////////////////////////
		private CRoleChampCustom(string strCodeRole, string strLibelleRole, Type typeAssocie, params Type[] typesDefinisseurs)
		{
			m_strCodeRole = strCodeRole;
			m_strLibelleRole = strLibelleRole;
			m_typeAssocie = typeAssocie;
			if (typesDefinisseurs == null)
				m_typesDefinisseurs = new Type[0];
			else
				m_typesDefinisseurs = typesDefinisseurs;
		}

		/// ////////////////////////////////////////////////////////
		public static CRoleChampCustom GetRole ( string strCodeRole )
		{
			return (CRoleChampCustom)m_tableRoles[strCodeRole];
		}

		/// ////////////////////////////////////////////////////////
		public static CRoleChampCustom GetRoleForType ( Type tp )
		{
			foreach ( CRoleChampCustom role in m_tableRoles.Values )
				if ( role.TypeAssocie == tp )
					return role;
			return null;
		}

		/// ////////////////////////////////////////////////////////
		public static string GetLibelleRole ( string strCodeRole )
		{
			CRoleChampCustom role = GetRole ( strCodeRole );
			if ( role != null )
				return role.Libelle;
			return "";
		}

		/// ////////////////////////////////////////////////////////
		public override string ToString()
		{
			return m_strLibelleRole;
		}


		/// ////////////////////////////////////////////////////////
		public int CompareTo(object obj)
		{
			if (! (obj is CRoleChampCustom) ) 
				return 1;

			return ( this.Libelle.CompareTo( ((CRoleChampCustom)obj).Libelle) );
		}

		/// ////////////////////////////////////////////////////////
		[DynamicField("Label")]
		[DescriptionField]
		public string Libelle
		{
			get
			{
				return ToString();
			}
		}

		/// ////////////////////////////////////////////////////////
		[DynamicField("Code")]
		public string CodeRole
		{
			get
			{
				return (m_strCodeRole);
			}
		}

		/// ////////////////////////////////////////////////////////
		public Type TypeAssocie
		{
			get
			{
				return m_typeAssocie;
			}
		}


        /// ////////////////////////////////////////////////////////
        public Type[] TypeDefinisseurs
        {
            get
            {
                return m_typesDefinisseurs;
            }
        }

		/// /////////////////////////////////////////////////////////
		public override int GetHashCode()
		{
			return m_strCodeRole.GetHashCode();
		}

		/// /////////////////////////////////////////////////////////
		public override bool Equals ( object obj )
		{
			return CompareTo ( obj ) == 0;
		}

		///////////////////////////////////////////////////////////////
		public static CRoleChampCustom[] Roles
		{
			get
			{
				ArrayList lst = new ArrayList();
				foreach ( CRoleChampCustom role in m_tableRoles.Values )
				{
					lst.Add ( role );
				}
				lst.Sort();
				return (CRoleChampCustom[])lst.ToArray(typeof(CRoleChampCustom));
			}
		}

		///////////////////////////////////////////////////////////////
        public static void RegisterRole(string strCodeRole, string strLibelleRole, Type typeAssocie, params Type[] typesDefinisseurs)
		{
			CRoleChampCustom role = GetRole(strCodeRole);
			if ( role != null && (role.Libelle != strLibelleRole  ))
				throw new Exception(I.T("The '@1' role ID isn't unique!!! (CRoleChampCustom)|196", strCodeRole));
            role = new CRoleChampCustom(strCodeRole, strLibelleRole, typeAssocie, typesDefinisseurs);
			m_tableRoles[role.CodeRole] = role;
		}	
						
	}
}
