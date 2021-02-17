using System;
using System.IO;
using System.Collections;
using System.Threading;

namespace sc2i.common
{
    /// <summary>
    /// //Gère un fichier qui est détruit lorsque l'objet est détruit.
    /// </summary>
    public class CFichierLocalTemporaire : IDisposable
    {
        private string m_strNomFichier;
        private string m_strExtensionFichier;
        /// <summary>
        /// Construit l'objet et lui alloue un nom de fichier
        /// </summary>
        public CFichierLocalTemporaire(string strExtensionFichier)
        {
            m_strExtensionFichier = strExtensionFichier;
            GenereNomFichier("");
        }

        /// <summary>
        /// Le paramètre strNomFichier peut être vide, dans ce cas la fonction génère un GUID
        /// </summary>
        /// <param name="strNomFichier"></param>
        private void GenereNomFichier(string strNomFichier)
        {
            m_strNomFichier = GetTempFolder() + "\\";

            if (String.IsNullOrEmpty(strNomFichier.Trim()))
                m_strNomFichier += Guid.NewGuid();
            else
                m_strNomFichier += strNomFichier;

            if (!String.IsNullOrEmpty(m_strExtensionFichier.Trim()))
                m_strNomFichier += "." + m_strExtensionFichier;

            /*if (m_strNomFichier.LastIndexOf(".") < 0) // Le nom de fichier n'a pas d'extension
            {
                if (!String.IsNullOrEmpty(m_strExtensionFichier.Trim()))
                    m_strNomFichier += "." + m_strExtensionFichier;
            }*/
        }

        /// //////////////////////////////////////////////////
        /// <summary>
        /// Retourne le répertoire temporaire de la machine
        /// </summary>
        /// <returns></returns>
        public static string GetTempFolder()
        {
            return Environment.GetEnvironmentVariable("TEMP");
        }

        /// //////////////////////////////////////////////////
        public string NomFichier
        {
            get
            {
                return m_strNomFichier;
            }
        }

        /// //////////////////////////////////////////////////
        public void CreateNewFichier(string strNomFichier)
        {
            GenereNomFichier(strNomFichier);
            FreeFichier();
        }

        public void CreateNewFichier()
        {
            GenereNomFichier("");
            FreeFichier();
        }

        /// //////////////////////////////////////////////////
        public bool FreeFichier()
        {
            CGestionnaireSuppressionFichiersTemporaires.AddFichierASupprimer(m_strNomFichier);
            return true;
            /*if ( File.Exists ( m_strNomFichier ) )
                File.Delete(m_strNomFichier);*/
        }

        /// <summary>
        /// ///////////////////////////////////////////
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bVal)
        {
            if (bVal)
                FreeFichier();
        }

        /// ///////////////////////////////////////////
        public string Extension
        {
            get
            {
                return m_strExtensionFichier;
            }
            set
            {
                m_strExtensionFichier = value;
            }
        }

    }

    public class CGestionnaireSuppressionFichiersTemporaires : IDisposable
    {
        private static int m_nDelaiSuppressionSecondes = 180;
        private static CGestionnaireSuppressionFichiersTemporaires m_instance;

        private ArrayList m_lstFichiers = new ArrayList();

        private Timer m_timer;

        private class CInfoSuppressionFichier
        {
            public readonly string NomFichier;
            public readonly DateTime DateSuppression;
            public CInfoSuppressionFichier(string strNomFichier)
            {
                NomFichier = strNomFichier;
                DateSuppression = DateTime.Now;
            }
        }

        private CGestionnaireSuppressionFichiersTemporaires()
        {
            m_timer = new Timer(new TimerCallback(OnTimer), null, m_nDelaiSuppressionSecondes * 1000, m_nDelaiSuppressionSecondes * 1000);
        }

        ~CGestionnaireSuppressionFichiersTemporaires()
        {
            Dispose(false);
        }

        public static void DisposeInstance()
        {
            if (m_instance != null)
                m_instance.Dispose();
        }

        #region Membres de IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bVal)
        {
            SupprimeFichiers(true);
            m_lstFichiers.Clear();
            m_timer.Dispose();
        }

        #endregion

        protected static CGestionnaireSuppressionFichiersTemporaires GetInstance()
        {
            lock (typeof(CGestionnaireSuppressionFichiersTemporaires))
            {
                if (m_instance == null)
                    m_instance = new CGestionnaireSuppressionFichiersTemporaires();
            }
            return m_instance;
        }

        public static void AddFichierASupprimer(string strNomFichier)
        {
            CGestionnaireSuppressionFichiersTemporaires instance = GetInstance();
            lock (typeof(CGestionnaireSuppressionFichiersTemporaires))
            {
                instance.m_lstFichiers.Add(new CInfoSuppressionFichier(strNomFichier));
            }
        }

        private void SupprimeFichiers(bool bForcerSuppression)
        {
            ArrayList lstVals;
            lock (typeof(CGestionnaireSuppressionFichiersTemporaires))
            {
                lstVals = new ArrayList(m_lstFichiers);
            }
            ArrayList lstFichiersRemoved = new ArrayList();
            foreach (CInfoSuppressionFichier info in lstVals.ToArray())
            {
                TimeSpan sp = DateTime.Now - info.DateSuppression;
                if (sp.TotalSeconds > m_nDelaiSuppressionSecondes || bForcerSuppression)
                {
                    try
                    {
                        if (File.Exists(info.NomFichier))
                            File.Delete(info.NomFichier);
                        lstFichiersRemoved.Add(info);
                    }
                    catch
                    {
                    }
                }
            }
            lock (typeof(CGestionnaireSuppressionFichiersTemporaires))
            {
                foreach (CInfoSuppressionFichier info in lstFichiersRemoved)
                    m_lstFichiers.Remove(info);
            }
        }

        private void OnTimer(object state)
        {
            SupprimeFichiers(false);
        }

    }
}
