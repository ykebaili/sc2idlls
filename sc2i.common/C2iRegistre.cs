using System;
using System.Collections;
using System.Globalization;

#if PDA

namespace sc2i.common
{
	/// <summary>
	/// Encapsule le registre windows.
	/// Il est nécéssaire d'hériter cette classe, afin de fournir les méthodes 
	/// IsLocalMachine : Indique si le registre utilisé est local machine ou current user
	/// GetClePrincipale : Indique la clé de base 
	/// </summary>
	public abstract class C2iRegistre
	{
		//retourne la clée principale ( sous la base qui est données pas IsLocalMachine )
		protected abstract string GetClePrincipale();
		
		//Vrai si la clé est dans local machine, sinon c'est localUser
		protected abstract bool IsLocalMachine();

		private RegistryKey GetKeyFullPath ( string strKey, RegistryKey root, bool bCreate )
		{
			RegistryKey key = root.OpenSubKey(strKey, bCreate);
			if ( bCreate && key == null )
			{
				string[] strSubs = strKey.Split('\\');
				key = root;
				RegistryKey subKey;
				for ( int nNum = 0; nNum < strSubs.Length; nNum++ )
				{
					subKey = key.OpenSubKey(strSubs[nNum], true);
					if ( subKey == null )
						subKey = key.CreateSubKey ( strSubs[nNum]);
					key = subKey;
				}
			}
			return key;
		}

		//////////////////////////////////////////////////////
		public	 RegistryKey GetKey(string strKey, bool bCreate)
		{
			return GetKeyFullPath (GetClePrincipale()+strKey, 
				IsLocalMachine() ? Registry.LocalMachine : Registry.CurrentUser, bCreate);
		}

		//////////////////////////////////////////////////////
		/// <summary>
		/// Retourne une valeur de chaine dans le registre, strKey est une sous
		/// clé de GetClePrincipale
		/// </summary>
		/// <param name="strKey"></param>
		/// <param name="strRubrique"></param>
		/// <param name="strDefaut"></param>
		/// <returns></returns>
		protected string GetValue ( string strKey, string strRubrique, string strDefaut )
		{
			string strVal = strDefaut;
			try
			{
				if ( IsLocalMachine() )
					strVal = Registry.CurrentUser.GetValue ( GetClePrincipale()+strKey, strDefaut ).ToString();
				else
					strVal = Registry.LocalMachine.GetValue ( GetClePrincipale()+strKey, strDefaut ).ToString();
			}
			catch
			{
				strVal = strDefaut;
			}
			return strVal;
		}

		//////////////////////////////////////////////////////
		/// <summary>
		/// Retourne une valeur chaine depuis le registre
		/// </summary>
		/// <param name="strKey"></param>
		/// <param name="strRubrique"></param>
		/// <param name="nDefaut"></param>
		/// <returns></returns>
		protected int GetIntValue ( string strKey, string strRubrique, int nDefaut )
		{
			int nVal = nDefaut;
			try
			{
				nVal = Int32.Parse ( GetValue ( strKey, strRubrique, nDefaut.ToString() ));
			}
			catch
			{
				nVal = nDefaut;
			}
			return nVal;
		}
				
		//////////////////////////////////////////////////////
		
		/// <summary>
		/// Met une valeur dans le registre (strKey est une sous-clé de GetClePrincipale).
		/// </summary>
		/// <param name="strKey"></param>
		/// <param name="strRubrique"></param>
		/// <param name="strValue"></param>
		protected void SetValue ( string strKey, string strRubrique, string strValue )
		{
			if ( IsLocalMachine() )
				Registry.CurrentUser.SetValue ( GetClePrincipale()+strKey, strValue );
			else
				Registry.LocalMachine.SetValue ( GetClePrincipale()+strKey, strValue );
			/*RegistryKey key = GetKey ( strKey, true );
			key.SetValue ( strRubrique, strValue );*/
		}
	}
}



#else
using Microsoft.Win32;
using System.Reflection;
using System.Collections.Generic;

namespace sc2i.common
{
	/// <summary>
	/// Encapsule le registre windows.
	/// Il est nécéssaire d'hériter cette classe, afin de fournir les méthodes 
	/// IsLocalMachine : Indique si le registre utilisé est local machine ou current user
	/// GetClePrincipale : Indique la clé de base 
	/// </summary>
	public abstract class C2iRegistre
	{
        public static C2iRegistre m_registreApplication = null;

		//retourne la clée principale ( sous la base qui est données pas IsLocalMachine )
		protected abstract string GetClePrincipale();
		
		//Vrai si la clé est dans local machine, sinon c'est localUser
		protected abstract bool IsLocalMachine();

		private RegistryKey GetKeyFullPath ( string strKey, RegistryKey root, bool bCreate )
		{
			RegistryKey key = root.OpenSubKey(strKey, bCreate);
			if ( bCreate && key == null )
			{
				string[] strSubs = strKey.Split('\\');
				key = root;
				RegistryKey subKey;
				for ( int nNum = 0; nNum < strSubs.Length; nNum++ )
				{
					subKey = key.OpenSubKey(strSubs[nNum], true);
					if ( subKey == null )
						subKey = key.CreateSubKey ( strSubs[nNum]);
					key = subKey;
				}
			}
			return key;
		}

        

        //////////////////////////////////////////////////////
        public RegistryKey GetKey(bool bIsLocalMachine, string strKey, bool bCreate)
        {
            return GetKeyFullPath(GetClePrincipale() + strKey,
                bIsLocalMachine ? Registry.LocalMachine : Registry.CurrentUser, bCreate);
        }

		//////////////////////////////////////////////////////
		public	 RegistryKey GetKey(string strKey, bool bCreate)
		{
            return GetKey(IsLocalMachine(), strKey, bCreate);
		}

        //////////////////////////////////////////////////////
        protected string GetValue(bool bIsLocalMachine, string strKey, string strRubrique, string strDefaut)
        {
            string strVal = strDefaut;
			try
			{
				RegistryKey key = GetKey ( bIsLocalMachine, strKey, false );
				if (key != null)
				{
					object valeur = key.GetValue(strRubrique);
					if (valeur != null)
						strVal = valeur.ToString();
				}
				key.Close();
			}
			catch
			{
				strVal = strDefaut;
			}
			return strVal;
		}

		//////////////////////////////////////////////////////
		/// <summary>
		/// Retourne une valeur de chaine dans le registre, strKey est une sous
		/// clé de GetClePrincipale
		/// </summary>
		/// <param name="strKey"></param>
		/// <param name="strRubrique"></param>
		/// <param name="strDefaut"></param>
		/// <returns></returns>
        public string GetValue(string strKey, string strRubrique, string strDefaut)
        {
            return GetValue(IsLocalMachine(), strKey, strRubrique, strDefaut);
        }

		//////////////////////////////////////////////////////
		/// <summary>
		/// Retourne une valeur chaine depuis le registre
		/// </summary>
		/// <param name="strKey"></param>
		/// <param name="strRubrique"></param>
		/// <param name="nDefaut"></param>
		/// <returns></returns>
		protected int GetIntValue ( string strKey, string strRubrique, int nDefaut )
		{
			int nVal = nDefaut;
			try
			{
				nVal = Int32.Parse(GetValue(strKey, strRubrique, nDefaut.ToString(CultureInfo.InvariantCulture)),
                    CultureInfo.InvariantCulture);
			}
			catch
			{
				nVal = nDefaut;
			}
			return nVal;
		}

		//////////////////////////////////////////////////////
		protected string[] GetListe ( string strKey, string strRubrique, char cSeparator )
		{
			string strValue = GetValue ( strKey, strRubrique, "" );
			if ( String.IsNullOrEmpty(strValue) )
				return new string[0];
			string[] strVals = strValue.Split(cSeparator);
			return strVals;
		}

		//////////////////////////////////////////////////////
		protected void SetListe ( string strKey, string strRubrique, char cSeparator, string[] liste )
		{
			string strTotal = "";
			foreach ( string strTmp in liste )
				strTotal += strTmp+cSeparator;
			if ( strTotal.Length > 0 )
				strTotal = strTotal.Substring(0, strTotal.Length-1 );
			SetValue ( strKey, strRubrique, strTotal );
		}

		//////////////////////////////////////////////////////
		protected void InsertStartOffListe ( 
			string strKey, 
			string strRubrique, 
			char cSeparator,
			int nNbMax,
			string strValeur)
		{
			ArrayList lstTmp = new ArrayList ( GetListe ( strKey, strRubrique, cSeparator ) );
			int nPos = lstTmp.IndexOf ( strValeur );
			if ( nPos >= 0 )
				lstTmp.RemoveAt ( nPos );
			lstTmp.Insert ( 0, strValeur );
			while ( lstTmp.Count > nNbMax )
				lstTmp.RemoveAt ( lstTmp.Count-1 );
			SetListe ( strKey, strRubrique, cSeparator, (string[])lstTmp.ToArray(typeof(string) ) );
		}
				
		//////////////////////////////////////////////////////
		
		/// <summary>
		/// Met une valeur dans le registre (strKey est une sous-clé de GetClePrincipale).
		/// </summary>
		/// <param name="strKey"></param>
		/// <param name="strRubrique"></param>
		/// <param name="strValue"></param>
		public void SetValue ( string strKey, string strRubrique, string strValue )
		{
			RegistryKey key = GetKey ( strKey, true );
			key.SetValue ( strRubrique, strValue );
		}

        /// INSTANCE GENERALE DU REGISTRE ///
        //-------------------------------
        public static void InitRegistreApplication(C2iRegistre registre)
        {
            m_registreApplication = registre;
        }

        //-------------------------------
        public static C2iRegistre RegistreApplication
        {
            get
            {
                return m_registreApplication;
            }
        }

        //-------------------------------
        public  void AddRecent(string strKey, string strRubrique, string strValeur, int nNbMaxStocke)
        {
            if (strValeur.Length > 0)
            {
                List<string> lstRecents = new List<string>(GetRecents(strKey, strRubrique));
                lstRecents.Remove(strValeur);
                lstRecents.Insert(0, strValeur);
                WriteRecents ( strKey, strRubrique, lstRecents );
            }
        }
        
        //-------------------------------
        private void WriteRecents(string strKey, string strRubrique, List<string> lstValues)
        {
            int nIndex = 1;
            foreach (string strVal in lstValues)
            {
                SetValue(strKey, strRubrique + "_" + nIndex, strVal);
                nIndex++;
            }
            while (GetValue(strKey, strRubrique + "_" + nIndex, "") != "")
            {
                SetValue(strKey, strRubrique + "_" + nIndex, "");
            }
        }

        //-------------------------------
        public void RemoveRecent(string strKey, string strRubrique, string strValeur)
        {
            List<string> lstRecents = new List<string>(GetRecents(strKey, strRubrique));
            lstRecents.Remove(strValeur);
            WriteRecents(strKey, strRubrique, lstRecents);
        }



        //-------------------------------
        public string[] GetRecents(string strKey, string strRubrique)
        {
            int nIndex = 1;
            List<string> lstVals = new List<string>();
            string strVal = "";
            while ( (strVal = GetValue ( strKey, strRubrique+"_"+nIndex, "") )!= "" )
            {
                lstVals.Add ( strVal );
                nIndex++;
            }
            return lstVals.ToArray();
        }

        //-------------------------------
        public static string GetValueInRegistreApplication(string strKey, string strRubrique, string strValeurParDefaut)
        {
            if (RegistreApplication != null)
                return RegistreApplication.GetValue(strKey, strRubrique, strValeurParDefaut);
            return strValeurParDefaut;
        }

        public static void SetValueInRegistreApplication ( string strKey, string strRubrique, string strValeur )
        {
            if (RegistreApplication != null)
                RegistreApplication.SetValue(strKey, strRubrique, strValeur);
        }

        public static int GetIntValueInRegistreApplication ( string strKey, string strRubrique, int nValeur )
        {
            if (RegistreApplication != null)
                return RegistreApplication.GetIntValue(strKey, strRubrique, nValeur);
            return nValeur;
        }

        public static void ReadObjetFromRegistre(string strKey, IInitialisableFromRegistre objet)
        {
            foreach (PropertyInfo info in objet.GetType().GetProperties())
            {
                MethodInfo set = info.GetSetMethod();
                if (set != null)
                {
                    try
                    {
                        if (info.PropertyType == typeof(string))
                        {
                            string strVal = RegistreApplication.GetValue(strKey, info.Name, "#~#");
                            if (strVal != "#~#")
                                set.Invoke(objet, new object[] { strVal });
                        }
                        if (info.PropertyType == typeof(int))
                        {
                            int nVal = RegistreApplication.GetIntValue(strKey, info.Name, Int16.MaxValue);
                            if (nVal != Int16.MaxValue)
                                set.Invoke(objet, new object[] { nVal });
                        }
                        if (info.PropertyType == typeof(bool))
                        {
                            string strVal = RegistreApplication.GetValue(strKey, info.Name, "#~#");
                            if (strVal != "#~#")
                            {
                                bool? bVal = CUtilBool.BoolFromString(strVal);
                                if (bVal != null)
                                    set.Invoke(objet, new object[] { bVal });
                            }
                        }
                        if (info.PropertyType.IsEnum)
                        {
                            string strVal = RegistreApplication.GetValue(strKey, info.Name, "#~#");
                            if (strVal != "#~#")
                            {
                                object val = null;
                                try
                                {
                                    val = Enum.Parse(info.PropertyType, strVal, true);
                                }
                                catch { }
                                if (val == null)
                                {
                                    try
                                    {
                                        int nVal = Int32.Parse(strVal);
                                        foreach (object value in Enum.GetValues(info.PropertyType))
                                            if ((int)value == nVal)
                                                val = value;
                                    }
                                    catch { }
                                }
                                if (val != null)
                                    set.Invoke(objet, new object[] { val });
                            }
                        }
                    }
                    catch
                    {
                        
                    }
                }
            }
        }
	}
    
    
    /// <summary>
    /// Une élément qui trouve dans le registre des valeurs à partir de ses propriétés
    /// publiques
    /// </summary>
    /// <remarks>
    /// Attention, seules les propriétés Enum, string, bool, int peuvent être initialisées
    /// </remarks>

    public interface IInitialisableFromRegistre
    {
    }
}



#endif

