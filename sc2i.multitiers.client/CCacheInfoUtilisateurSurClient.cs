using System;
using System.Collections;

using sc2i.common;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace sc2i.multitiers.client
{
	/// <summary>
	/// Description résumée de CCacheInfoUtilisateurSurClient.
	/// </summary>
	public class CCacheInfoUtilisateurSurClient : MarshalByRefObject, IInfoUtilisateur, IDisposable
	{
#if DEBUG
		public void ClearCacheRestriction()
		{
			m_infosRestrictionsReferentiel = null;
			m_dicRestrictionsParVersion.Clear();
		}
#endif
		/// <summary>
		/// Stocke les informations concernant les restrictions
		/// </summary>
		private class CInfosRestriction
		{
			private CListeRestrictionsUtilisateurSurType m_listeRestrictions = new CListeRestrictionsUtilisateurSurType();
			private ReadOnlyCollection<Type> m_listeTypesARestrictionsSurObjets = new ReadOnlyCollection<Type>(new List<Type>());

			public CInfosRestriction(
				CListeRestrictionsUtilisateurSurType listeRestrictions,
				ReadOnlyCollection<Type> listeTypesARestrictionsSurObjets)
			{
				m_listeRestrictions = listeRestrictions;
				if (m_listeRestrictions == null)
					m_listeRestrictions = new CListeRestrictionsUtilisateurSurType();
				m_listeTypesARestrictionsSurObjets = listeTypesARestrictionsSurObjets;
				if (m_listeTypesARestrictionsSurObjets == null)
					m_listeTypesARestrictionsSurObjets = new ReadOnlyCollection<Type>(new List<Type>());
			}

			public CListeRestrictionsUtilisateurSurType ListeRestrictions
			{
				get
				{
					return m_listeRestrictions; ;
				}
			}

			public ReadOnlyCollection<Type> ListeTypesARestrictionsSurObjets
			{
				get
				{
					return m_listeTypesARestrictionsSurObjets;
				}
			}
		}

		private CRecepteurNotification m_recepteurNotificationChangementDroit;

		//Gère le cache des restrictions pour chaque version de données
		private Dictionary<int, CInfosRestriction> m_dicRestrictionsParVersion = new Dictionary<int, CInfosRestriction>();

		private IInfoUtilisateur m_infoUtilisateurSurServeur;
		private string m_strNom = "";
        private CDbKey m_keyUtilisateur = null;
		private int m_nIdSession;
		private CDbKey[] m_listeKeysGroupes = new CDbKey[0];

		//Droit->IdonneeDroit
		private Hashtable m_tableDonneesDroit = new Hashtable();

		private CInfosRestriction m_infosRestrictionsReferentiel = null;
		
		private bool m_bReload = true;

		private C2iSponsor m_sponsor = new C2iSponsor();

		
		/// //////////////////////////////////////////////////////////////////
		public CCacheInfoUtilisateurSurClient( int nIdSession, IInfoUtilisateur infoServeur)
		{
			m_nIdSession = nIdSession;
			m_infoUtilisateurSurServeur = infoServeur;
			m_sponsor.Register(m_infoUtilisateurSurServeur);
			InitFromInfoServeur();
			m_recepteurNotificationChangementDroit = new CRecepteurNotification( m_nIdSession, typeof(CDonneeNotificationChangementDroitUtilisateur));
			m_recepteurNotificationChangementDroit.OnReceiveNotification += new NotificationEventHandler(OnNotificationChangementDroit);
		}

		/// //////////////////////////////////////////////////////////////////
		public IInfoUtilisateur InfoUtilisateurSurServeur
		{
			get
			{
				try
				{
					//Vérifie que l'objet est valide
					//TESTDBKEYOK
                    CDbKey keyBidon = m_infoUtilisateurSurServeur.KeyUtilisateur;
					return m_infoUtilisateurSurServeur;
				}
				catch
				{//Tente de retrouver l'info
					CSessionClient session = CSessionClient.GetSessionForIdSession(m_nIdSession);
					session.RefreshUtilisateur();
					m_sponsor.Unregister(m_infoUtilisateurSurServeur);
					CCacheInfoUtilisateurSurClient info = session.GetInfoUtilisateur() as CCacheInfoUtilisateurSurClient;
					if (info != null)
					{
						m_infoUtilisateurSurServeur = info.m_infoUtilisateurSurServeur;
						return m_infoUtilisateurSurServeur;
					}
					else
						return info;
				}
			}					
		}

		/// //////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			m_sponsor.Unregister(m_infoUtilisateurSurServeur);
			m_infoUtilisateurSurServeur = null;
			m_sponsor.Dispose();
			m_sponsor = null;
            if (m_recepteurNotificationChangementDroit != null)
                m_recepteurNotificationChangementDroit.Dispose();
            m_recepteurNotificationChangementDroit = null;
		}

		/// //////////////////////////////////////////////////////////////////
		protected void OnNotificationChangementDroit ( IDonneeNotification donnee )
		{
			if ( !(donnee is CDonneeNotificationChangementDroitUtilisateur))
				return;
			if (((CDonneeNotificationChangementDroitUtilisateur)donnee).KeyUtilisateur == m_keyUtilisateur )
				m_bReload = true;
		}

		/// //////////////////////////////////////////////////////////////////
		public void InitFromInfoServeur()
		{
			if ( m_infoUtilisateurSurServeur == null )
				return;
			m_strNom = m_infoUtilisateurSurServeur.NomUtilisateur;
			m_keyUtilisateur = m_infoUtilisateurSurServeur.KeyUtilisateur;
            //m_nIdUtilisateur = m_infoUtilisateurSurServeur.
			m_listeKeysGroupes = m_infoUtilisateurSurServeur.ListeKeysGroupes;
			m_tableDonneesDroit = new Hashtable();
			m_infosRestrictionsReferentiel = null;
			m_dicRestrictionsParVersion.Clear();
			m_bReload = false;
		}

		/// //////////////////////////////////////////////////////////////////
		private void AssureData()
		{
			if ( m_bReload )
				InitFromInfoServeur();
		}

		/// //////////////////////////////////////////////////////////////////
		public string NomUtilisateur
		{
			get
			{
				AssureData();
				return m_strNom;
			}
		}

		/// //////////////////////////////////////////////////////////////////
		public CDbKey KeyUtilisateur
		{
			get
			{
				AssureData();
                return m_keyUtilisateur;
			}
		}

        /// //////////////////////////////////////////////////////////////////
        public CDbKey[] ListeKeysGroupes
		{
			get
			{
				AssureData();
				return m_listeKeysGroupes;
			}
		}

		/// //////////////////////////////////////////////////////////////////
		public IDonneeDroitUtilisateur GetDonneeDroit ( string strDroit )
		{
			AssureData();
			object objDroit = m_tableDonneesDroit[strDroit];
			if ( objDroit != null && objDroit is IDonneeDroitUtilisateur )
				return ( IDonneeDroitUtilisateur )m_tableDonneesDroit[strDroit];
			if ( objDroit != null )//L'utilisateur n'a pas ce droit
				return null;
			objDroit = InfoUtilisateurSurServeur.GetDonneeDroit(strDroit);
			if ( objDroit == null )
				m_tableDonneesDroit[strDroit] = "";
			else
				m_tableDonneesDroit[strDroit] = objDroit;
			return (IDonneeDroitUtilisateur)objDroit;
		}

		/// //////////////////////////////////////////////////////////////////
		private void AssureRestrictions( int? nIdVersion)
		{
			if (nIdVersion == null)
			{
				if (m_infosRestrictionsReferentiel == null)
				{
					CListeRestrictionsUtilisateurSurType listeRestrictions = InfoUtilisateurSurServeur.GetListeRestrictions(nIdVersion);
					ReadOnlyCollection<Type> listeTypesARestrictions = InfoUtilisateurSurServeur.GetTypesARestrictionsSurObjets(nIdVersion);
					m_infosRestrictionsReferentiel = new CInfosRestriction(listeRestrictions, listeTypesARestrictions);
				}
			}
			else
			{
				CInfosRestriction infosPourVersion = null;
				if (!m_dicRestrictionsParVersion.TryGetValue((int)nIdVersion, out infosPourVersion))
				{
					CListeRestrictionsUtilisateurSurType listeRestrictions = InfoUtilisateurSurServeur.GetListeRestrictions(nIdVersion);
					ReadOnlyCollection<Type> listeTypesARestrictions = InfoUtilisateurSurServeur.GetTypesARestrictionsSurObjets(nIdVersion);
					infosPourVersion = new CInfosRestriction(listeRestrictions, listeTypesARestrictions);
					m_dicRestrictionsParVersion[(int)nIdVersion] = infosPourVersion;
				}
			}
		}

		/// //////////////////////////////////////////////////////////////////
		private CInfosRestriction GetInfosRestrictions ( int? nIdVersion )
		{
			AssureData ( );
			AssureRestrictions ( nIdVersion );
			if ( nIdVersion == null )
				return m_infosRestrictionsReferentiel;
			CInfosRestriction infos = null;
			if ( !m_dicRestrictionsParVersion.TryGetValue((int)nIdVersion, out infos ))
				infos = new CInfosRestriction(null, null);
			return infos;
		}




		/// //////////////////////////////////////////////////////////////////
		public CListeRestrictionsUtilisateurSurType GetListeRestrictions(int? nIdVersion)
		{
			return GetInfosRestrictions ( nIdVersion ).ListeRestrictions;
		}

		/// //////////////////////////////////////////////////////////////////
		public ReadOnlyCollection<Type> GetTypesARestrictionsSurObjets( int? nIdVersion )
		{
			return GetInfosRestrictions(nIdVersion).ListeTypesARestrictionsSurObjets;
		}

		/// //////////////////////////////////////////////////////////////////
		public CRestrictionUtilisateurSurType GetRestrictionsSur ( Type tp, int? nIdVersion )
		{
			return GetInfosRestrictions(nIdVersion).ListeRestrictions.GetRestriction(tp);
		}

		/// //////////////////////////////////////////////////////////////////
		public CRestrictionUtilisateurSurType GetRestrictionsSurObjet ( object objet, int? nIdVersion )
		{
			if ( objet == null )
				return GetRestrictionsSur ( typeof(int), nIdVersion );//Aucune restriction !!
            Type tp = objet.GetType();

            IConteneurObjetARestriction conteneur = objet as IConteneurObjetARestriction;
            if (conteneur != null)
                tp = conteneur.TypePourRestriction;

            CRestrictionUtilisateurSurType rest = (CRestrictionUtilisateurSurType)GetInfosRestrictions(nIdVersion).ListeRestrictions.GetRestriction(tp).Clone();
			rest.ApplyToObjet ( objet );
			return rest;
		}

	}
}
