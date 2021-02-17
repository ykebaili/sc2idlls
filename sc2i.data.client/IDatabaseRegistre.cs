using System;

using sc2i.common;
using sc2i.multitiers.client;
using System.Collections.Generic;

namespace sc2i.data
{
	public interface IDatabaseRegistre
	{
		///////////////////////////////////////////////////////
		string GetValeurString ( string strCle, string strDefaut );

		///////////////////////////////////////////////////////
		long GetValeurLong ( string strCle, long nDefaut );

		///////////////////////////////////////////////////////
		double GetValeurDouble ( string strCle, double fDefaut );

		///////////////////////////////////////////////////////
		CResultAErreur SetValeur ( string strCle, string strValeur );

		///////////////////////////////////////////////////////
		CResultAErreur SetValeurBlob(string strCle, byte[] valeur);

		///////////////////////////////////////////////////////
		byte[] GetValeurBlob(string strCle);

	}

    /// <summary>
    /// Notification pour indiquer que le cache des valeurs de registre de base doivent être purgés
    /// </summary>
    [Serializable]
    public class CDonneeNotificationResetCacheDatabaseRegistre : IDonneeNotification
    {
        private int m_nIdSessionEnvoyeur = -1;

        public CDonneeNotificationResetCacheDatabaseRegistre(int nIdSessionEnvoyeur)
        {
            m_nIdSessionEnvoyeur = nIdSessionEnvoyeur;
        }

        public int  IdSessionEnvoyeur
        {
	          get 
	        { 
		        return m_nIdSessionEnvoyeur;
	        }
	          set 
	        { 
		        m_nIdSessionEnvoyeur = value;
	        }
        }

        public int  PrioriteNotification
        {
	        get { return 1000; }
        }


    }

	///////////////////////////////////////////////////////
	/// <summary>
	/// Permet d'accéder facilement au registre depuis un poste client
	/// </summary>
	[Serializable]
	public class CDataBaseRegistrePourClient : IDatabaseRegistre
	{
		int m_nIdSession = -1;

        [NonSerialized]
        private static Dictionary<string, object> m_cacheDonnees = new Dictionary<string, object>();

        private static CRecepteurNotification m_recepteurNotifications = null;

		public CDataBaseRegistrePourClient ( int nIdSession )
		{
			m_nIdSession = nIdSession;
            if (m_recepteurNotifications == null)
            {
                m_recepteurNotifications = new CRecepteurNotification(nIdSession, typeof(CDonneeNotificationResetCacheDatabaseRegistre));
                m_recepteurNotifications.OnReceiveNotification += new NotificationEventHandler(m_recepteurNotifications_OnReceiveNotification);
            }
		}

        private class CLockerCacheRegistre { };

		private static void  m_recepteurNotifications_OnReceiveNotification(IDonneeNotification donnee)
        {
            lock (typeof(CLockerCacheRegistre))
            {
                m_cacheDonnees.Clear();
            }
        }

        private static object GetCache(string strCle)
        {
            object val = null;
            lock (typeof(CLockerCacheRegistre))
            {
                m_cacheDonnees.TryGetValue(strCle, out val);
            }
            return val;
        }

        private static void SetCache(string strCle, object val)
        {
            lock (typeof(CLockerCacheRegistre))
            {
                m_cacheDonnees[strCle] = val;
            }
        }

        IDatabaseRegistre GetRegistre()
		{
			return (IDatabaseRegistre)C2iFactory.GetNew2iObjetServeur(typeof(IDatabaseRegistre), m_nIdSession);
		}

		public string GetValeurString(string strCle, string strDefaut)
		{
            string strVal = GetCache(strCle) as string;
            if (strVal != null)
                return strVal;
			strVal = GetRegistre().GetValeurString ( strCle, strDefaut );
            SetCache(strCle, strVal);
            return strVal;
		}

		public long GetValeurLong(string strCle, long nDefaut)
		{
            long? nVal = GetCache(strCle) as long?;
            if (nVal != null)
                return nVal.Value;
            nVal = GetRegistre().GetValeurLong ( strCle, nDefaut );
            SetCache(strCle, nVal.Value);
            return nVal.Value;
		}

		public double GetValeurDouble(string strCle, double fDefaut)
		{
            double? fVal = GetCache(strCle) as double?;
            if (fVal != null)
                return fVal.Value;
			fVal = GetRegistre().GetValeurDouble ( strCle, fDefaut );
            SetCache(strCle, fVal.Value);
            return fVal.Value;
		}

		public CResultAErreur SetValeur(string strCle, string strValeur)
		{
            SetCache(strCle, strValeur);
			return GetRegistre().SetValeur ( strCle, strValeur );
		}

		public CResultAErreur SetValeurBlob(string strCle, byte[] valeur)
		{
            SetCache(strCle, valeur);
			return GetRegistre().SetValeurBlob ( strCle, valeur );
		}

		public byte[] GetValeurBlob(string strCle)
		{
            byte[] btVal = GetCache ( strCle ) as byte[];
            if ( btVal != null )
                return btVal;
            btVal = GetRegistre().GetValeurBlob ( strCle );
            if ( btVal != null )
                SetCache ( strCle, btVal );
            return btVal;
		}
	}
}
