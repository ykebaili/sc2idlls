using System;
using System.Collections;
using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.win32.process
{
	/// <summary>
	/// Description résumée de CPicxyzDbAppRegistre.
	/// </summary>
	public class CSc2iProcessRegistre : C2iRegistre
	{
		public CSc2iProcessRegistre()
		{
		}


		public static string ClePrincipale
		{
			get
			{
				return "Software\\sc2i\\Process_pref\\";
			}
		}

		protected override string GetClePrincipale()
		{
			return ClePrincipale;
		}

		protected override bool IsLocalMachine()
		{
			return false;
		}

		public string[] LastURLsActionsDistantes
		{
			get
			{
				string strVal = GetValue ( "ACTIONS_DISTANTES","LAST_URLS", "" );
				string[] strDatas = strVal.Split('\t');
				ArrayList lst = new ArrayList();
				foreach ( string strUrl in strDatas )
				{
					if ( strUrl.Trim().Length > 3 )
						lst.Add ( strUrl );
				}
				return ( string[] )lst.ToArray ( typeof(string) );
			}
			set
			{
				int nNb = 0;
				string strData = "";
				foreach ( string strURL in value )
				{
					if (strURL.Length > 3 )
					{
						strData += strURL+"\t";
						nNb++;
					}
					if ( nNb >= 8 )
						break;
				}
				if ( strData.Length > 0 )
					strData = strData.Substring(0, strData.Length-1 );
				SetValue("ACTIONS_DISTANTES","LAST_URLS", strData );
			}
		}

	}
}
