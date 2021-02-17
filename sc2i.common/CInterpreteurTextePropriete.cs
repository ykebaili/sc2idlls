using System;
using System.Reflection;
using System.Collections;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CInterpreteurTextePropriete.
	/// </summary>
	public sealed class CInterpreteurTextePropriete
	{
        private CInterpreteurTextePropriete() { }

		public static object GetValue ( object obj, string strPropriete )
		{
			if (strPropriete == "this")
				return obj;
			MemberInfo membre = null;
			object objToInterroge = null;

			return GetValue ( obj, strPropriete, ref objToInterroge, ref membre );
		}

		/// ///////////////////////////////////////////////////////
		public static object GetValue ( object obj, string strPropriete, ref object objToInterroge, ref MemberInfo membre )
		{
			if ( GetObjetFinalEtMemberInfo ( obj, strPropriete, ref objToInterroge, ref membre) )
			{
				return GetValue ( objToInterroge, membre );
			}
			return null;
		}


		/// ///////////////////////////////////////////////////////
		public static object GetValue ( object obj, MemberInfo membre)
		{
			if ( membre == null || obj == null )
				return null;
			MethodInfo methode = null;
            PropertyInfo mbrProp = (membre as PropertyInfo);
			if ( mbrProp != null )
				methode = mbrProp.GetGetMethod();
            if (methode != null)
                return methode.Invoke(obj, null);
			return null;
		}

		/// <summary>
		/// Récupère l'objet et la méthode finale à interroger à partir d'un objet de base et d'une propriété
		/// </summary>
		/// <param name="objDeBase"></param>
		/// <param name="strPropriete"></param>
		/// <param name="objAInterroger"></param>
		/// <param name="methode"></param>
		public static bool GetObjetFinalEtMemberInfo ( object objDeBase, string strPropriete, ref object objAInterroger, ref MemberInfo membre )
		{
			if ( objDeBase == null )
				return false;
			if ( strPropriete == null )
				return false;
			string[] listeAppels = strPropriete.Split('.');
			if( listeAppels.Length == 0 )
				return true;
			string strMembre = listeAppels[0];
			Type tp = objDeBase.GetType();
			MemberInfo[] membres = tp.GetMember(strMembre);
            if (membres.Length == 0)
            {
                PropertyInfo prop = ReplaceFieldAttribute.FindFieldObsolete(tp, strMembre);
                if (prop == null)
                    return false;
                else
                    membres = new MemberInfo[] { prop };
            }
			membre = membres[0];
			objAInterroger = objDeBase;
			if ( listeAppels.Length > 1 )
				return GetObjetFinalEtMemberInfo ( GetValue ( objDeBase, membre ), strPropriete.Substring(strMembre.Length+1), ref objAInterroger, ref membre );
			return true;
		}

		/*private static bool IsToStringAParametreString ( MemberInfo info )
		{
			if ( info.MemberType != MemberTypes.Method )
				return false;
			if ( info.Name != "ToString" )
				return false;
			MethodInfo methode = (MethodInfo)info;
			if ( methode.GetParameters().Length != 1 )
				return false;
			if ( methode.ReturnType != typeof(string))
				return false;
			ParameterInfo paramInfo = methode.GetParameters()[0];
			if ( paramInfo.ParameterType != typeof(string))
				return false;
			return true;
		}*/

		/// ///////////////////////////////////////////////////
		public static string GetStringValue ( object obj, string strPropriete, string strValSiNull)
		{
			Type tp = null;
			return GetStringValue ( obj, strPropriete, strValSiNull, ref tp );
		}

		/// ///////////////////////////////////////////////////
		public static string GetStringValue ( object obj, string strPropriete, string strValSiNull, ref Type tpDonnee)
		{
			bool bIsChampValide = false;
			return GetStringValue ( obj, strPropriete, strValSiNull, ref bIsChampValide, ref tpDonnee );
		}
		/// ///////////////////////////////////////////////////
		public static string GetStringValue ( object obj, string strPropriete, string strValSiNull, ref bool bIsChampValide )
		{
			Type tp =  null;
			return GetStringValue ( obj, strPropriete, strValSiNull, ref bIsChampValide, ref tp );
		}

		/// ///////////////////////////////////////////////////
		public static string GetStringValue ( object obj, string strPropriete, string strValSiNull, ref bool bIsChampValide, ref Type tp )
		{
			MemberInfo membre = null;
			object objToInterroge = null;
			bIsChampValide = false;
			object valeur = null;
			if ( GetObjetFinalEtMemberInfo ( obj, strPropriete, ref objToInterroge, ref membre) )
			{
				bIsChampValide = true;
				valeur = GetValue ( objToInterroge, membre );
			}
			if ( valeur != null )
				tp = valeur.GetType();
			else
				tp = typeof(DBNull);
			return GetStringValueFormatee ( valeur, strValSiNull, membre );
		}

		/// ///////////////////////////////////////////////////
		public static string GetStringValueFormatee ( object valeur, string strValeurSiNull, MemberInfo method )
		{
			if ( valeur == null)
				return strValeurSiNull;
			if ( method == null )
				return valeur.ToString();
			if (valeur is Double)
			{
				if (Double.IsNaN((double)valeur))
					return strValeurSiNull;
			}
			//Si la méthode a l'attribut DefaultFormat
			object[] attribs = method.GetCustomAttributes(typeof(DefaultFormatAttribute), true);
			if ( attribs.Length != 0 )
			{
				string strFormat = ((DefaultFormatAttribute)attribs[0]).Format;
				//Recherche la méthode ToString avec un paramètre string
				MethodInfo methodToString = valeur.GetType().GetMethod ( "ToString", new Type[]{typeof(string)} );
				if ( methodToString != null )
				{
					return (string)methodToString.Invoke(valeur, new object[]{strFormat});
				}
			}
			if (valeur is bool)
			{
				if ((bool)valeur)
					return "oui";
				else
					return "non";
			}
			return valeur.ToString();
		}

		public static bool SetValue ( object obj, string strPropriete, object valeur )
		{
			if ( obj == null )
				return false;
			object objetAModifier = obj;
			if ( strPropriete.LastIndexOf('.') > 0 )
			{
				//Ce n'est pas une propriété directe
				string strPropToObjetDirect = strPropriete.Substring(0, strPropriete.LastIndexOf('.'));
				objetAModifier = GetValue ( obj, strPropToObjetDirect );
				string[] props = strPropriete.Split('.');
				strPropriete = props[props.Length-1];
			}
			if ( objetAModifier == null )
				return false;
			Type tp = objetAModifier.GetType();
			PropertyInfo prop = tp.GetProperty(strPropriete);
			if ( prop != null )
			{
				MethodInfo method = prop.GetSetMethod();
				if ( method != null )
				{
					method.Invoke(objetAModifier, new object[]{C2iConvert.ChangeType(valeur, prop.PropertyType)});
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Crée une liste à partir d'une liste et d'une propriétés
		/// </summary>
		/// <param name="lst"></param>
		/// <param name="strPropriete"></param>
		/// <returns></returns>
		public static Array CreateListeFrom ( IList lst, string strPropriete, Type tp )
		{
			Array retour = Array.CreateInstance ( tp, lst.Count );
			int nIndice = 0;
			foreach ( object obj in lst )
			{
				object res = GetValue ( obj, strPropriete );
				retour.SetValue ( res, nIndice );
				nIndice++;
			}
			return retour;
		}

		/// <summary>
		/// Crée une liste à plat (qui ajoute donc les listes de proprietes filles) à partir
		/// d'un objet et d'une propriété (les . sont utilisés)
		/// </summary>
		/// <param name="lst"></param>
		/// <param name="strPropriete"></param>
		/// <returns></returns>
		public static ArrayList CreateListePlateFrom ( object objet, string strPropriete )
		{
			if ( objet == null )
				return new ArrayList();
			ArrayList lstVals = new ArrayList();
            IList ilst = (objet as IList);
			if ( ilst == null )
				lstVals.Add ( objet );
			else
				lstVals.AddRange ( ilst );
			if ( String.IsNullOrEmpty(strPropriete) )
				return lstVals;
			string[] strProps = strPropriete.Split('.');
			string strFirstProp = strProps[0];
			string strNextProps = "";
			if ( strProps.Length > 1 )
				strNextProps = strPropriete.Substring(strFirstProp.Length+1);
			

			ArrayList lstFinale = new ArrayList();
			foreach ( object objSource in lstVals )
			{
				if ( objSource != null )
				{
					object objFils = GetValue ( objSource, strFirstProp );
					if ( objFils != null )
					{
						lstFinale.AddRange ( CreateListePlateFrom ( objFils, strNextProps ) );
					}
				}
			}
			return lstFinale;
		}
	}
}
