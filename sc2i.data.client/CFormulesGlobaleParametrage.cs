using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.data;
using System.IO;
using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.data
{
    //------------------------------------------------------------
    public class CDefinitionFormuleGlobaleParametrage
    {
        public readonly string Key;
        public readonly string Libelle;
        public readonly Type TypeSource;


        //------------------------------------------------------------
        public CDefinitionFormuleGlobaleParametrage ( string strKey, string strLibelle, Type typeSource )
        {
            Key = strKey;
            Libelle = strLibelle;
            TypeSource = typeSource;
        }
    }

    //------------------------------------------------------------
    public static class CFormulesGlobaleParametrage
    {
        private static Dictionary<string, CDefinitionFormuleGlobaleParametrage> m_dicKeyToDesc = new Dictionary<string, CDefinitionFormuleGlobaleParametrage>();
        private static Dictionary<string, C2iExpression> m_cacheFormule = new Dictionary<string, C2iExpression>();
        private static CRecepteurNotification m_recepteurNotifications = null;

        //--------------------------------------------------------------------------------
        private class CDonneeNotificationChangementFormuleGlobale : IDonneeNotification
        {
            int m_nIdSessionEnvoyeur = -1;
            //--------------------------------------------------------------------------------
            private CDonneeNotificationChangementFormuleGlobale(int nIdSession)
            {
                m_nIdSessionEnvoyeur = nIdSession;
            }

            //--------------------------------------------------------------------------------
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

            //--------------------------------------------------------------------------------
            public int  PrioriteNotification
            {
	            get { return 100;}
            }
        }

        //--------------------------------------------------------------------------
        private static void AssureRecepteurNotifications(int nIdSession)
        {
            if (m_recepteurNotifications == null)
            {
                m_recepteurNotifications = new CRecepteurNotification(nIdSession, typeof(CDonneeNotificationResetCacheDatabaseRegistre));
                m_recepteurNotifications.OnReceiveNotification += new NotificationEventHandler(m_recepteurNotifications_OnReceiveNotification);
            }
        }

        //--------------------------------------------------------------------------
        public static CDefinitionFormuleGlobaleParametrage[] GetDefinitionsFormules()
        {
            List<CDefinitionFormuleGlobaleParametrage> lst = new List<CDefinitionFormuleGlobaleParametrage>();
            foreach (KeyValuePair<string, CDefinitionFormuleGlobaleParametrage> kv in m_dicKeyToDesc)
                lst.Add(kv.Value);
            lst.Sort((x, y) => x.Libelle.CompareTo(y.Libelle));
            return lst.ToArray();
        }

        //------------------------------------------------------------
        public static CDefinitionFormuleGlobaleParametrage GetDefinition(string strKey)
        {
            CDefinitionFormuleGlobaleParametrage def = null;
            m_dicKeyToDesc.TryGetValue(strKey, out def);
            return def;
        }
                

        //--------------------------------------------------------------------------
        public static void m_recepteurNotifications_OnReceiveNotification(IDonneeNotification donnee)
        {
 	        m_cacheFormule.Clear();
        } 

        //--------------------------------------------------------------------------
        public static void RegisterFormuleGlobale(string strKey, string strLibelle, Type typeSource)
        {
            m_dicKeyToDesc[strKey] = new CDefinitionFormuleGlobaleParametrage(strKey, strLibelle, typeSource);
        }

        //--------------------------------------------------------------------------
        public static C2iExpression GetFormule(int nIdSession, string strKey)
        {
            AssureRecepteurNotifications(nIdSession);
            C2iExpression formule = null;
            if (m_cacheFormule.TryGetValue(strKey, out formule))
                return formule;
            CDataBaseRegistrePourClient reg = new CDataBaseRegistrePourClient(nIdSession);
            byte[] bts = reg.GetValeurBlob("GLOBFOR_"+strKey);
            if (bts != null)
            {
                MemoryStream stream = new MemoryStream(bts);
                BinaryReader reader = new BinaryReader(stream);
                CSerializerReadBinaire ser = new CSerializerReadBinaire(reader);
                CResultAErreur result = ser.TraiteObject<C2iExpression>(ref formule);
                if (result)
                {
                    m_cacheFormule[strKey] = formule;
                }
                reader.Close();
                stream.Close();
                stream.Dispose();
            }
            return formule;
        }

        //--------------------------------------------------------------------------
        public static void SetFormule(int nIdSession, string strKey, C2iExpression formule)
        {
            AssureRecepteurNotifications(nIdSession);
            CDataBaseRegistrePourClient reg = new CDataBaseRegistrePourClient(nIdSession);
            if (formule == null)
            {
                reg.SetValeurBlob("GLOBFOR_"+strKey, null);
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(stream);
                CSerializerSaveBinaire ser = new CSerializerSaveBinaire(writer);
                CResultAErreur res = ser.TraiteObject<C2iExpression>(ref formule);
                if (res)
                {
                    reg.SetValeurBlob("GLOBFOR_"+strKey, stream.GetBuffer());
                    CDonneeNotificationResetCacheDatabaseRegistre notif = new CDonneeNotificationResetCacheDatabaseRegistre(nIdSession);
                    CEnvoyeurNotification.EnvoieNotifications ( new IDonneeNotification[]{notif} );
                }
                writer.Close();
                stream.Close();
                stream.Dispose();

            }
        }

    }
}
