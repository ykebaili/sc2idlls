using System;
using System.Collections;
using System.Data;
using sc2i.data;


using sc2i.common;
using sc2i.multitiers.server;
using sc2i.multitiers.client;

namespace sc2i.data.serveur
{
	public class CDatabaseRegistre : C2iObjetServeur, IDatabaseRegistre
	{
		private static Hashtable m_cache = new Hashtable();
		public const string c_nomTable = "TIMOS_REGISTRY";

		public const string c_champCle = "REG_KEY";
		public const string c_champValeur = "REG_VALUE";
		public const string c_champBlob = "REG_BLOB";


		private IDatabaseConnexion m_connexion = null;

		///////////////////////////////////////////////////////
		public CDatabaseRegistre()
			:base()
		{
		}

		///////////////////////////////////////////////////////
		public CDatabaseRegistre(int nIdSession)
			:base ( nIdSession )
		{
		}

        
		#region A MODIFIER
		///////////////////////////////////////////////////////
		public CDatabaseRegistre(IDatabaseConnexion connexion )
			:base ( connexion.IdSession )
		{
			m_connexion = connexion;
		}
		///////////////////////////////////////////////////////
		private IDatabaseConnexion Connexion
		{
			get
			{
                if (m_connexion == null)
                {
                    if (m_nIdSession == -1)
                        m_nIdSession = 0;
                    m_connexion = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType());
                }
				return m_connexion;
			}
		}
		///////////////////////////////////////////////////////
		public string GetValeurString(string strCle, string strDefaut)
		{
			string strVal = (string)m_cache[strCle];
			if (strVal != null)
				return strVal;
			lock (typeof(CDatabaseRegistre))
			{
				string strRequete = "select " + c_champValeur
									+ " from " + c_nomTable
									+ " where " + c_champCle + "=" + Connexion.GetStringForRequete(strCle);
				CResultAErreur result = Connexion.ExecuteScalar(strRequete);
				if (!result || !(result.Data is string))
					strVal = strDefaut;
				else
					strVal = result.Data.ToString();
				m_cache[strCle] = strVal;
				return strVal;
			}
		}
		///////////////////////////////////////////////////////
		public CResultAErreur SetValeur(string strCle, string strValeur)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				lock (typeof(CDatabaseRegistre))
				{
					string strRequete = "select " + c_champValeur + " from " + c_nomTable
						+ " where " + c_champCle + "=" + Connexion.GetStringForRequete(strCle);
					result = Connexion.ExecuteScalar(strRequete);
					if (!result || result.Data == null)
					{
						string strInsert = "insert into " + c_nomTable + "(" + c_champCle + "," + c_champValeur + ") " +
							"values(" + Connexion.GetStringForRequete(strCle) + "," + Connexion.GetStringForRequete(strValeur) + ")";
						return Connexion.RunStatement(strInsert);
					}
					else
					{
						string strUpdate = "Update " + c_nomTable + " set " + c_champValeur + "=" + Connexion.GetStringForRequete(strValeur) +
							" where " + c_champCle + "=" + Connexion.GetStringForRequete(strCle);
						return Connexion.RunStatement(strUpdate);
					}
				}
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			finally
			{
                if (result)
                {

                    CDonneeNotificationResetCacheDatabaseRegistre data = new CDonneeNotificationResetCacheDatabaseRegistre(IdSession);
                    CEnvoyeurNotification.EnvoieNotifications(new CDonneeNotificationResetCacheDatabaseRegistre[] { data });
                }
                ResetCacheStatic();
			}
			return result;
		}
		#endregion

		///////////////////////////////////////////////////////
		public void ResetCache()
        {
            CDonneeNotificationResetCacheDatabaseRegistre data = new CDonneeNotificationResetCacheDatabaseRegistre(IdSession);
            CEnvoyeurNotification.EnvoieNotifications(new CDonneeNotificationResetCacheDatabaseRegistre[] { data });
            ResetCacheStatic();
        }

        ///////////////////////////////////////////////////////
        public static void ResetCacheStatic()
		{
			m_cache.Clear();
            
		}

		///////////////////////////////////////////////////////
		public long GetValeurLong ( string strCle, long nDefaut )
		{
			string strVal = GetValeurString ( strCle, nDefaut.ToString() );
			long nTmp = 0;
			try
			{
				nTmp = Int64.Parse ( strVal );
			}
			catch 
			{
				nTmp = nDefaut;
			}
			return nTmp;
		}

		///////////////////////////////////////////////////////
		public double GetValeurDouble ( string strCle, double fDefaut )
		{
			string strVal = GetValeurString ( strCle, fDefaut.ToString() );
			return CConvertisseurDoubleString.Convert ( strVal );
		}

		///////////////////////////////////////////////////////
		public byte[] GetValeurBlob ( string strCle )
		{
			CResultAErreur result = Connexion.ReadBlob(c_nomTable, c_champBlob,
				new CFiltreData(c_champCle + "=@1", strCle));
			if(  result )
				return result.Data as byte[];
			return null;
		}

		///////////////////////////////////////////////////////
		public CResultAErreur SetValeurBlob(string strCle, byte[] data)
		{
			SetValeur(strCle, "");//Pour créer l'enregistrement
			CResultAErreur result = Connexion.SaveBlob(c_nomTable, c_champBlob,
				new CFiltreData(c_champCle + "=@1", strCle),
				data);
			return result;
		}
				


	}
}
