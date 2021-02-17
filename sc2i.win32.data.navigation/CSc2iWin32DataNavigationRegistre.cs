using System;

using sc2i.common;
using sc2i.win32.common;

namespace sc2i.win32.data.navigation
{
	/// <summary>
	/// Description résumée de CCafelAppRegistre.
	/// </summary>
	public class CSc2iWin32DataNavigationRegistre : C2iRegistre
	{
		private static string m_strClePrincipale = "";
		
		public CSc2iWin32DataNavigationRegistre()
		{
		}

		public static void Init ( string strClePrincipale )
		{
			m_strClePrincipale = strClePrincipale;
		}

		protected override string GetClePrincipale()
		{
			if ( m_strClePrincipale == "" )
			{
				try
				{
                    throw new Exception(I.T("sc2i.win32.data.navigation has not been initialized|30013 "));
				}
				catch ( Exception e )
				{
					throw new Exception(e.ToString()+"\r\n"+e.StackTrace.ToString() );
				}
			}
			return m_strClePrincipale;
		}

		//Registre utilisateur
		protected override bool IsLocalMachine()
		{
			return false;
		}

		public static int GetWidthValue(string strCle)
		{

			return (new CSc2iWin32DataNavigationRegistre()).GetIntValue ( "Preferences", strCle, 120);
		}

		public static void SetWidthValue(string strCle, int valeur)
		{
			(new CSc2iWin32DataNavigationRegistre()).SetValue ("Preferences", strCle, valeur.ToString() );
		}

		private static string GetNomForRegistre ( System.Windows.Forms.Control liste, string strName )
		{
			string strPath = "Preferences\\Panel_Listes\\";
			if ( liste.FindForm() != null )
				strPath += liste.FindForm().GetType().Name+"_";
			else
			{
				System.Windows.Forms.Control ctrl = liste;
				while (ctrl.Parent != null )
					ctrl = ctrl.Parent;
				strPath += ctrl.GetType().Name+"_";
			}
			strPath +=strName;
			return strPath;
		}


		public static void ReadListViewAutoFilled ( ListViewAutoFilled liste, string strName )
		{
			liste.ReadFromRegistre ( new CSc2iWin32DataNavigationRegistre().GetKey(GetNomForRegistre(liste, strName), false));
		}

		public static void SaveListViewAutoFilled ( ListViewAutoFilled liste, string strName )
		{
			liste.WriteToRegistre	( new CSc2iWin32DataNavigationRegistre().GetKey(GetNomForRegistre(liste, strName), true));
		}

		public static void ReadGlacialList ( GlacialList liste, string strName, string strContexte )
		{
			liste.ReadFromRegistre ( new CSc2iWin32DataNavigationRegistre().GetKey(GetNomForRegistre(liste, strName+strContexte+"_G"), false));
		}

        //------------------------------------------------------------------
        public static bool GetShowListeDeListeSpeed(Type typeElements, string strContexte)
        {
            return new CSc2iWin32DataNavigationRegistre().GetValue("Preferences", typeElements.ToString() + "_" + strContexte, "0")=="1";
        }

        //------------------------------------------------------------------
        public static void SetShowListeDeListeSpeed(Type typeElements, string strContexte, bool bShow)
        {
            new CSc2iWin32DataNavigationRegistre().SetValue("Preferences", typeElements.ToString() + "_" + strContexte, bShow?"1":"0");
        }

        //------------------------------------------------------------------
        public static int LargeurListeDeListeDansPanelListeSpeed
        {
            get
            {
                return new CSc2iWin32DataNavigationRegistre().GetIntValue("Preferences", "List_List_speed_width", 160);
            }
            set
            {
                new CSc2iWin32DataNavigationRegistre().SetValue("Preferences", "List_List_speed_width", value.ToString());
            }
        }


		public static void SaveGlacialList ( GlacialList liste, string strName, string strContexte )
		{
			liste.WriteToRegistre	( new CSc2iWin32DataNavigationRegistre().GetKey(GetNomForRegistre(liste, strName+strContexte+"_G"), true));
		}

		public static string GetStringPreferenceFiltre ( CPanelListeSpeedStandard panel, string strContexte )
		{
			return new CSc2iWin32DataNavigationRegistre().GetValue ( GetNomForRegistre ( panel, panel.Name+strContexte ), "FILTRE", "");
		}

		public static void SetStringPreferenceFiltre ( CPanelListeSpeedStandard panel, string strContexte, string strString )
		{
			new CSc2iWin32DataNavigationRegistre().SetValue ( GetNomForRegistre ( panel, panel.Name+strContexte ), "FILTRE" ,strString );
		}

		public static bool GetPreferenceVueArbre(CPanelListeSpeedStandard panel, string strContexte)
		{
			return new CSc2iWin32DataNavigationRegistre().GetValue(GetNomForRegistre(panel, panel.Name + strContexte), "ARBRE", "0")=="1";
		}

		public static void SetPreferenceVueArbre(CPanelListeSpeedStandard panel, string strContexte, bool bVueArbre)
		{
			new CSc2iWin32DataNavigationRegistre().SetValue(GetNomForRegistre(panel, panel.Name + strContexte), "ARBRE", bVueArbre?"1":"0");
		}
	}
}
