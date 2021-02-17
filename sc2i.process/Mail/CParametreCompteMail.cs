using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.multitiers.client;

namespace sc2i.process.Mail
{
    [Serializable]
    public class CParametreCompteMail : I2iSerializable
    {
        // Paramètres du serveur de messagerie
        private string m_strPopServer = "";
        private int m_nPopPort = 110;
        private string m_strUser = "";
        private string m_strPass = "";
        private bool m_bUseSsl = false;
        private bool m_bHeaderOnly = false;

        // Options de remise
        private bool m_bSupprimerLesMessagesDuServeur = false;
        private int m_nSupprimerDuServeurApresNbjours = 10;

        public CParametreCompteMail()
        {

        }
        
        /// <summary>
        /// Le Nom du serveur de mails
        /// </summary>
        [DynamicField("Server Name")]
        public string PopServer
        {
            get
            {
                return m_strPopServer;
            }
            set
            {
                m_strPopServer = value;
            }
        }

        /// <summary>
        /// Le Port du serveur de mails
        /// </summary>
        [DynamicField("Server Port")]
        public int PopPort
        {
            get
            {
                return m_nPopPort;
            }
            set
            {
                m_nPopPort = value;
            }
        }

        /// <summary>
        /// Le Nom de l'utilisteur
        /// </summary>
        [DynamicField("User name")]
        public string User
        {
            get
            {
                return m_strUser;
            }
            set
            {
                m_strUser = value;
            }
        }

        /// <summary>
        /// Le mot de passe de l'utilisateur
        /// </summary>
        [DynamicField("Password")]
        public string Password
        {
            get
            {
                return m_strPass;
            }
            set
            {
                m_strPass = value;
            }
        }

        /// <summary>
        /// Indique si le protocole de communication utilise une connexion sécurisée de type SSL
        /// </summary>
        [DynamicField("Use SSL")]
        public bool UseSsl
        {
            get
            {
                return m_bUseSsl;
            }
            set
            {
                m_bUseSsl = value;
            }
        }

        /// <summary>
        /// Indique si les messages doivent être supprimés du serveur lors après récupération
        /// </summary>
        [DynamicField("Delete from Server")]
        public bool SupprimerDuServeur
        {
            get
            {
                return m_bSupprimerLesMessagesDuServeur;
            }
            set
            {
                m_bSupprimerLesMessagesDuServeur = value;
            }
        }

        /// <summary>
        /// Délai de garde des messages sur le serveur
        /// </summary>
        [DynamicField("Delete delay")]
        public int DelaiSuppression
        {
            get
            {
                return m_nSupprimerDuServeurApresNbjours;
            }
            set
            {
                m_nSupprimerDuServeurApresNbjours = value;
            }
        }

        /// <summary>
        /// Indique si l'on doit charger uniquement l'Entête du Mail
        /// </summary>
        [DynamicField("Header only")]
        public bool HeaderOnly
        {
            get
            {
                return m_bHeaderOnly;
            }
            set
            {
                m_bHeaderOnly = value;
            }
        }

        private int GetNumVersion()
        {
            return 0;
        }

        public CResultAErreur Serialize(C2iSerializer serializer)
        {
            CResultAErreur result = CResultAErreur.True;
            int nVersion = GetNumVersion();

            result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            serializer.TraiteString(ref m_strPopServer);
            serializer.TraiteInt(ref m_nPopPort);
            serializer.TraiteString(ref m_strUser);
            serializer.TraiteString(ref m_strPass);

            serializer.TraiteBool(ref m_bUseSsl);
            serializer.TraiteBool(ref m_bSupprimerLesMessagesDuServeur);
            serializer.TraiteInt(ref m_nSupprimerDuServeurApresNbjours);
            serializer.TraiteBool(ref m_bHeaderOnly);
            
            return result;
        }

    }
}
