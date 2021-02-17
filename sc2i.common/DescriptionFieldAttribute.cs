using System;
using System.Collections;
using System.Text;
using System.Reflection;

namespace sc2i.common
{
	/// <summary>
	/// Est utilisé pour connaitre la proprieté qui decrit le mieux un élément
	/// en remplacement de description element sur les ObjetDonnee, c'est mieux.
	/// </summary>
	[AttributeUsage ( AttributeTargets.Property )]
	public sealed class DescriptionFieldAttribute : Attribute
	{
		private static Hashtable m_tableCacheDescriptionField = new Hashtable();

		//si true, indique que cet propriete peut être utilisé dans les arbres
		private readonly bool UtiliserDansLesArbres;
		public DescriptionFieldAttribute()
		{
		}

		public DescriptionFieldAttribute(bool bUtiliserDansLesArbres)
		{
			UtiliserDansLesArbres = bUtiliserDansLesArbres;;
		}

		public static string GetDescriptionField ( Type typeElement, string strDefaut )
		{
			return GetDescriptionField(typeElement, strDefaut, false);
		}

		public static string GetDescriptionField(Type typeElement, string strDefaut, bool bPourUtilisationArbres)
		{
			if (typeElement == null)
				return strDefaut;
			string strProp = (string)m_tableCacheDescriptionField[typeElement+","+bPourUtilisationArbres.ToString()];
			if ( strProp != null )
				return strProp;
			Type tp = typeElement;
			while (tp != null)
			{
				foreach (PropertyInfo info in tp.GetProperties())
				{
					object[] attribs = info.GetCustomAttributes(true);
					if (attribs != null)
					{
                        foreach (object attrib in attribs)
                        {
                            DescriptionFieldAttribute descField = (attrib as DescriptionFieldAttribute);
                            if (descField != null)
                            {
                                m_tableCacheDescriptionField[typeElement + "," + descField.UtiliserDansLesArbres] = info.Name;
                            }
                        }
					}
				}
				tp = tp.BaseType;
				strProp = (string)m_tableCacheDescriptionField[typeElement + "," + bPourUtilisationArbres.ToString()];
			}
			strProp = (string)m_tableCacheDescriptionField[typeElement + "," + bPourUtilisationArbres.ToString()];
			if (strProp != null)
				return strProp;
			return strDefaut;
		}

		//-------------------------------------------------------
		public static string GetDescription(object objet)
		{
			if (objet == null)
				return "";
			string strProp = GetDescriptionField(objet.GetType(), "");
			if (!String.IsNullOrEmpty(strProp))
			{
				PropertyInfo info = objet.GetType().GetProperty(strProp);
				if (info != null && info.GetGetMethod() != null)
				{
					object val = info.GetGetMethod().Invoke(objet, null);
					if (val != null)
						return val.ToString();
				}
			}
			return "";
		}
	}
}
