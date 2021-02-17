using System;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;

namespace sc2i.common
{
	/// <summary>
	/// Description résumée de CTraducteur.
	/// </summary>
	public class CTraducteur
	{
		//----------------------------------------------
		//clé fichier->fichier
		private Hashtable m_tableFichiers = new Hashtable();


		private static CTraducteur m_instance;

		private CTraducteur()
		{
			
		}

		//----------------------------------------------
		private class CFichierMessage
		{
			//Numéro de message->Texte
			private Hashtable m_tableMessages = new Hashtable();

			//--------------------------------------
			public CFichierMessage()
			{
			}

            //--------------------------------------
            public void AddLigne(string strLigne)
            {
                int nPos = strLigne.IndexOf(';');
				if ( nPos >= 0 )
				{
					try
					{
						string strNum, strMes;
						strNum = strLigne.Substring(0, nPos );
						strMes = strLigne.Substring ( nPos+1 );
						int nMes = Int32.Parse ( strNum, CultureInfo.CurrentCulture);
						m_tableMessages[nMes] = strMes;
					}
					catch
					{}
                }
            }

			//--------------------------------------
			public CResultAErreur ReadStream ( StreamReader reader )
			{
				CResultAErreur result = CResultAErreur.True;
				m_tableMessages.Clear();
				try
				{
					string strLigne = reader.ReadLine ();
					while ( strLigne != null )
					{
						AddLigne ( strLigne );
						strLigne = reader.ReadLine();
					}
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
				}
				return result;
			}

			//--------------------------------------
			public string Translate ( string strMessage, params string[] strParametres )
			{
				if (strMessage.Length == 0)
					return "";
				int nPos = strMessage.LastIndexOf ('|');
				if ( nPos >= 0 )
				{
					string strSansNum = strMessage.Substring ( 0, nPos );
					try
					{
						int nMes = Int32.Parse ( strMessage.Substring(nPos+1 ), CultureInfo.CurrentCulture);
						string strTmp = (string)m_tableMessages[nMes];
						if ( strTmp == null )
							strTmp = strSansNum;
						for (int nParametre = strParametres.Length - 1; nParametre >= 0; nParametre--)
							strTmp = strTmp.Replace("@"+(nParametre+1), strParametres[nParametre]);
						return strTmp;
					}
					catch
					{
                        return strMessage;
					}
					return strSansNum;
				}
				return strMessage;
			}
		}

		//--------------------------------------
		private static CTraducteur Instance
		{
			get
			{
				if ( m_instance == null )
					m_instance = new CTraducteur();
				return m_instance;
			}
		}

		//--------------------------------------
		public static CResultAErreur InitForModule ( System.Reflection.Assembly ass, string strExtension )
		{
			CResultAErreur result = CResultAErreur.True;
			string strFichier = ass.GetName().CodeBase;
			int nPos = strFichier.LastIndexOf('.');
			if ( nPos >= 0 )
				strFichier = strFichier.Substring(0, nPos)+"."+strExtension;
			if ( File.Exists ( strFichier ) )
				result = ReadFichier ( ass.GetName().Name, strFichier );
			return result;
		}

        //--------------------------------------
        public static CResultAErreur ReadFichier(string strFichier)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                StreamReader reader = new StreamReader(strFichier, System.Text.Encoding.Default);
                string strLigne = reader.ReadLine();
                string strLastModule = "";
                CFichierMessage lastModule = null;
                while (strLigne != null)
                {
                    if (strLigne.Length > 3 && strLigne[0] == '[' && strLigne[strLigne.Length - 1] == ']')
                    {
                        strLastModule = strLigne.Substring(1, strLigne.Length - 2).ToUpper(
                            CultureInfo.CurrentCulture);
                        lastModule = (CFichierMessage)Instance.m_tableFichiers[strLastModule];
                        if (!String.IsNullOrEmpty(strLastModule) && lastModule == null)
                        {
                            lastModule = new CFichierMessage();
                            Instance.m_tableFichiers[strLastModule] = lastModule;
                        }
                    }
                    else
                        if (lastModule != null)
                            lastModule.AddLigne(strLigne);
                    strLigne = reader.ReadLine();
                }
                reader.Close();
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }

  
		//--------------------------------------
		public static CResultAErreur ReadFichier ( string strAssembly, string strFichier )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				strAssembly = strAssembly.ToUpper(CultureInfo.InvariantCulture);
				StreamReader reader = new StreamReader ( strFichier );
				CFichierMessage fichier = new CFichierMessage ( );
				result = fichier.ReadStream ( reader );
				reader.Close();
				if ( result )
					Instance.m_tableFichiers[strAssembly] = fichier;
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}

		private static Dictionary<Assembly, string> m_tableNomsAssemblies = new Dictionary<Assembly, string>();
		internal static string GetCle ( Type tp )
		{
			return GetCle(tp.Assembly);
		}

		internal static string GetCle(Assembly ass)
		{
			string strVal = "";
			if (!m_tableNomsAssemblies.TryGetValue(ass, out strVal))
			{
				strVal = ass.GetName().Name.ToUpper(CultureInfo.InvariantCulture);
				m_tableNomsAssemblies[ass] = strVal;
			}
			return strVal;
		}


		//----------------------------------------------
		public static bool HasTraductionsForAssembly(string strAssembly)
		{
			return Instance.m_tableFichiers.Contains(strAssembly.ToUpper(CultureInfo.InvariantCulture));
		}

		//----------------------------------------------
		public static bool HasTraductionsForType(Type tp)
		{
			return Instance.m_tableFichiers.Contains(GetCle(tp));
		}
		//--------------------------------------
		public static string Translate ( string strAssembly, string strMessage, params string[] strParametres )
		{
			strAssembly = strAssembly.ToUpper(CultureInfo.InvariantCulture);
			if (strMessage.Length == 0)
				return "";
			CFichierMessage fichier = (CFichierMessage)Instance.m_tableFichiers[strAssembly];
			if ( fichier == null )
			{
				int nPos = strMessage.LastIndexOf('|');
                if (nPos >= 0)
                {
                    try
                    {
                        int nMes = Int32.Parse(strMessage.Substring(nPos + 1));
                        strMessage = strMessage.Substring(0, nPos);
                    }
                    catch
                    {
                        return strMessage;
                    }
                }
				for (int nParametre = strParametres.Length - 1; nParametre >= 0; nParametre--)
					strMessage = strMessage.Replace("@"+(nParametre+1), strParametres[nParametre]);
				return strMessage;
			}
			return fichier.Translate ( strMessage, strParametres);
		}
	}

	public sealed class I
	{
        private static double m_fDureeInTranslate = 0;
        private I() { }

		public static string TT(Type tp, string strMessage, params string[] strParametres)
		{
			return CTraducteur.Translate(CTraducteur.GetCle(tp), strMessage, strParametres);
		}

		public static string T(string strMessage, params string[] strParametres)
		{
            DateTime dt = DateTime.Now;
			StackTrace trace = new StackTrace();
			if (trace.FrameCount >= 1)
			{
				string strAssembly = CTraducteur.GetCle(trace.GetFrame(1).GetMethod().Module.Assembly);
                m_fDureeInTranslate +=((TimeSpan)(DateTime.Now-dt)).TotalMilliseconds;
				return CTraducteur.Translate(strAssembly, strMessage, strParametres);
			}
            m_fDureeInTranslate += ((TimeSpan)(DateTime.Now - dt)).TotalMilliseconds;
			return "#ERRMES " + strMessage;
		}
	}
}
