using System;
#if PDA
#else
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;
#endif
using System.Xml;
using System.Collections;

using sc2i.common;
using System.Collections.Generic;


namespace sc2i.multitiers.client
{
	public class  C2iFactory
	{
		const int c_idSessionAucune = 0;
		/////////////////////////////////////////////////////////////////////////////////
		private static C2iFactory m_instance = null;
		
		//table des urls avec clé sur le namespace
		private Hashtable m_tableUrls = new Hashtable();
		private string	m_strDefautUrl ="";

        /// <summary>
        /// Utilisé lorsque le serveur et le client sont dans le même process ! (appli mono poste)
        /// </summary>
        private static I2iObjetServeurFactory m_localFactory = null;
        private static Dictionary<string, MarshalByRefObject> m_localDicSingleton = null;

		/////////////////////////////////////////////////////////////////////////////////
		private static C2iFactory GetInstance()
		{
#if PDA
			if ( m_instance == null )
				m_instance = new C2iFactory();
#endif
			if ( m_instance == null )
				throw new Exception ( I.T("C2iFactory wasn't initialized|101") );
			return m_instance;
		}

        /////////////////////////////////////////////////////////////////////////////////
        public static bool IsInit()
        {
            return m_instance != null;
        }

        /////////////////////////////////////////////////////////////////////////////////
        public static CResultAErreur InitEnLocal(
            I2iObjetServeurFactory factory,
            Dictionary<string, MarshalByRefObject> dicLocalSingleton)
        {
            m_localFactory = factory;
            m_localDicSingleton = dicLocalSingleton;
            m_instance = new C2iFactory();
            return CResultAErreur.True;
        }

        /////////////////////////////////////////////////////////////////////////////////
        public static void SetSingletonDef ( string strURI, MarshalByRefObject objet )
        {
            if (m_localDicSingleton == null)
                m_localDicSingleton = new Dictionary<string, MarshalByRefObject>();
            m_localDicSingleton[strURI] = objet;
        }

		/////////////////////////////////////////////////////////////////////////////////
		public static CResultAErreur InitFromFile ( string strFichierConfiguration )
		{
			/*
			Schéma du fichier de configuration : 
	
			<2ifactory>
				<default url="url"/>
				<namespace name="namespace" url="namespaceurl" />
			</2ifactory>
			
			Toutes les classe du namespace "namespace" seront executées à partir de l'adresse "namespaceurl"
			Les autres à partir de l'url default.
			
			Le format des url est du type "tcp://172.16.1.100:8085"
			
			*/
			CResultAErreur result = CResultAErreur.True;
			m_instance = new C2iFactory();
			try
			{
				bool bZoneDetectee = false;
				XmlTextReader reader = new XmlTextReader(strFichierConfiguration);
				while ( reader.Read() )
				{
					if ( reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "sc2ifactory" )
						bZoneDetectee = true;
					if ( bZoneDetectee )
					{
						if ( reader.NodeType == XmlNodeType.Element )
						{
							switch ( reader.Name.ToLower() )
							{
								case "default" :
									m_instance.m_strDefautUrl = reader.GetAttribute("url");
									break;
								case "namespace" :
									string strNameSpace = reader.GetAttribute("name");
									string strUrl = reader.GetAttribute("url");
									m_instance.m_tableUrls[strNameSpace] = strUrl;
									break;
							}
						}
						if ( reader.NodeType == XmlNodeType.EndElement &&
							reader.Name.ToLower() == "sc2ifactory" )
							bZoneDetectee = false;
					}
				}
				reader.Close();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				return result;
			}
			return result;
		}


		/////////////////////////////////////////////////////////////////////////////////
		public static CResultAErreur InitDefaultUrl ( string strDefaultURL )
		{
			CResultAErreur result = CResultAErreur.True;
			m_instance = new C2iFactory();
			m_instance.m_strDefautUrl = strDefaultURL;
			return result;
		}

		/////////////////////////////////////////////////////////////////////////////////
		public static void AddNewNamespaceUrl(string strNameSpace, string strURL )
		{
			GetInstance().m_tableUrls[strNameSpace] = strURL;
		}


		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// converti une URI en fonction des paramètres d'initialisation du Factory
		/// </summary>
		/// <param name="strURI"></param>
		/// <returns></returns>
		private string GetURIFor ( string strClasseURI )
		{
			if (strClasseURI == null)
				return null;
			if ( m_tableUrls[strClasseURI] != null )
				return (string)m_tableUrls[strClasseURI];
			int nPos = strClasseURI.LastIndexOf (".");
			string strNameSpace, strClasse = strClasseURI;
			if ( nPos < 0 )
				strNameSpace = "";
			else
			{
				strNameSpace = strClasseURI.Substring ( 0, nPos );
				strClasse = strClasseURI.Substring ( nPos+1, strClasseURI.Length-nPos-1 );
			}
			string strUrl = (string)m_tableUrls[strNameSpace];
			nPos = strNameSpace.LastIndexOf (".");
			while ( strUrl == null && nPos > 0)
			{
				strNameSpace = strNameSpace.Substring ( 0, nPos );
				strUrl = (string)m_tableUrls[strNameSpace];
				nPos = strNameSpace.LastIndexOf (".");
			}
			if ( strUrl == null )
				strUrl = m_strDefautUrl;
			strUrl += "/"+strClasse;
			return strUrl;
		}

		/////////////////////////////////////////////////////////////////////////////////
		public void GetRacineEtClasse ( string strURI, ref string strRacine, ref string strClasse )
		{
			int nPos = strURI.LastIndexOf ( "/" );
			if ( nPos < 0 )
			{
				strRacine = "";
				strClasse = strURI;
			}
			else
			{
				strRacine = strURI.Substring ( 0, nPos );
				strClasse = strURI.Substring ( nPos+1, strURI.Length-nPos-1 );
			}
		}
		

		/////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne un nouveau I2iMarshalObject pour la session demandée à partir
		/// du nom de classe
		/// </summary>
		/// <param name="strURI"></param>
		/// <param name="typeObjet"></param>
		/// <param name="nIdSession"></param>
		/// <returns></returns>
		public static I2iMarshalObject GetNewObjetForSession ( string strURI, Type typeObjet, int nIdSession )
		{
			//Aloue un objet en utilisant un factory situé sur le serveur de la classe d'objet demandée
			return GetInstance().GetNewObjetForSessionProtected ( strURI, typeObjet, nIdSession );
		}

		/////////////////////////////////////////////////////////////////////////////////
		public I2iMarshalObject GetNewObjetForSessionProtected ( string strURI, Type typeObjet, int nIdSession )
		{
#if PDA
			object obj = GetNewObjectProtected ( strURI, typeObjet, nIdSession );
			return (I2iMarshalObject) obj;
#else
            
			string strRacine="", strClasse="";

			if (strURI == null)
				return null;

			//récupère la racine de l'uri où l'objet se trouve
			GetRacineEtClasse(GetURIFor(strURI), ref strRacine, ref strClasse );

            if (m_localFactory != null)
                return m_localFactory.GetNewObject(strClasse, nIdSession);

			//Alloue un allocateur sur le serveur hébergeant l'objet demandé
			string strFactory = strRacine+"/"+"I2iObjetServeurFactory";
			I2iObjetServeurFactory factory = (I2iObjetServeurFactory) Activator.GetObject ( typeof(I2iObjetServeurFactory), strFactory );
			if ( factory == null )
				return null;
			object obj = factory.GetNewObject(strClasse, nIdSession );
			return (I2iMarshalObject)obj;
#endif
		}



		
		/*////////////////////////////////////////////////////////////////////////////////////////////////
		public static MarshalByRefObject GetNewObjectWithFactory ( Type typeObjet )
		{
			return GetNewObjectWithFactory ( typeObjet.ToString(), typeObjet, CSessionClient.IdSession );
		}
		*/

		////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne un nouvel objet à partir de son type.
		/// L'objet doit être impérativement déclaré dans la config de remoting
		/// </summary>
		/// <param name="typeObjet"></param>
		/// <returns></returns>
		public static MarshalByRefObject GetNewObject ( Type typeObjet )
		{
			return GetInstance().GetNewObjectProtected ( typeObjet, c_idSessionAucune );
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Retourne un nouveau 2iObjetServeur à partir de son type
		/// </summary>
		/// <param name="typeObjet"></param>
		/// <param name="nIdSession"></param>
		/// <returns></returns>
		public static I2iMarshalObject GetNew2iObjetServeur ( Type typeObjet, int nIdSession )
		{
			return (I2iMarshalObject)GetInstance().GetNewObjectProtected ( typeObjet, nIdSession );
		}

		/*
		////////////////////////////////////////////////////////////////////////////////////////////////
		public static MarshalByRefObject GetNewObject ( string strType, Type typeObjet )
		{
			return GetInstance().GetNewObjectProtected ( strType, typeObjet );
		}
		*/

		/*
		/////////////////////////////////////////////////////////////////////////////////
		public static MarshalByRefObject GetNewObjectWithFactory ( string strType, Type leTypeDeBase, int nIdSession )
		{
			return GetInstance().GetNewObjectWithFactoryProtected ( strType, leTypeDeBase, nIdSession );
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		protected MarshalByRefObject GetNewObjectWithFactoryProtected ( string strClasse, Type leTypeDeBase, int nIdSession )
		{
			//Pour simplifier le boulot, seuls les factory des objets sont exportés: 
			//le serveur déclare une classe xxxFactory qui implémente IFactory et retourne
			//un objet du type xxx.
			strClasse += "Factory";
			string strNameSpace = "";
			int nPos = strClasse.LastIndexOf (".");
			if ( nPos >= 0 )
			{
				strNameSpace = strClasse.Substring ( 0, nPos );
				strClasse = strClasse.Substring ( nPos+1, strClasse.Length-nPos-1 );
			}
			string strUrl = (string)m_tableUrls[strNameSpace];
			if ( strUrl == null )
				strUrl = m_strDefautUrl;
			strUrl = strUrl+"/"+strClasse;
			IFactory factory = (IFactory) Activator.GetObject ( leTypeDeBase, strUrl );
			if ( factory == null )
				return null;
			MarshalByRefObject obj =(MarshalByRefObject)factory.GetNewObject ( nIdSession );
			ILease lease = (ILease)RemotingServices.GetLifetimeService ( obj );
			return obj;

		}*/

		////////////////////////////////////////////////////////////////////////////////////////////////
		protected MarshalByRefObject GetNewObjectProtected ( Type typeObjet, int nIdSession )
		{
			//Sépare le namespace du nom de la classe
			string strClasse;
			strClasse = typeObjet.ToString();
			return GetNewObjectProtected ( strClasse, typeObjet, nIdSession );
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		protected MarshalByRefObject GetNewObjectProtected ( string strClasse, Type typeDeBase, int nIdSession )
		{
#if PDA
			Type tp = CActivatorSurChaine.GetType ( strClasse );
			if ( tp != null )
			{
				object obj = Activator.CreateInstance ( tp );
				if ( obj != null )
				{
					((I2iMarshalObject)obj).IdSession = nIdSession;
				}
				return (MarshalByRefObject)obj;
			}
			return null;
#else
            if (m_localFactory != null && m_localDicSingleton != null)
            {
                MarshalByRefObject objLocal = null;
                if (m_localDicSingleton.TryGetValue(strClasse, out objLocal))
                    return objLocal;
            }
			string strNameSpace;
			int nPos = strClasse.LastIndexOf (".");
			if ( nPos < 0 )
				strNameSpace = "";
			else
			{
				strNameSpace = strClasse.Substring ( 0, nPos );
				strClasse = strClasse.Substring ( nPos+1, strClasse.Length-nPos-1 );
			}
			string strUrl = (string)m_tableUrls[strNameSpace];
			nPos = strNameSpace.LastIndexOf (".");
			while ( strUrl == null && nPos > 0)
			{
				strNameSpace = strNameSpace.Substring ( 0, nPos );
				strUrl = (string)m_tableUrls[strNameSpace];
				nPos = strNameSpace.LastIndexOf (".");
			}
			if ( strUrl == null )
				strUrl = m_strDefautUrl;
			strUrl = strUrl+"/"+strClasse;
			MarshalByRefObject obj = (MarshalByRefObject)Activator.GetObject ( typeDeBase, strUrl );
			if ( obj is I2iMarshalObjectDeSession && nIdSession != c_idSessionAucune )
				((I2iMarshalObjectDeSession)obj).IdSession = nIdSession;
			return obj;
#endif
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		public static bool TestServeurParDefaut()
		{
            if (m_localFactory != null)
                return true;
			return GetInstance().TestServeurParDefautProtected();
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		protected bool TestServeurParDefautProtected()
		{
			//Alloue un allocateur sur le serveur 
			string strFactory = m_strDefautUrl+"/"+"I2iObjetServeurFactory";
			I2iObjetServeurFactory factory = (I2iObjetServeurFactory) Activator.GetObject ( typeof(I2iObjetServeurFactory), strFactory );
			if ( factory == null )
				return false;
			try
			{
				factory.ToString();
				return true;
			}
			catch
			{
			}
			return false;
		}

	}
}
