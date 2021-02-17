using System;
using System.Data;

using sc2i.multitiers.client;
using sc2i.common;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CContexteSauvegardeObjetsDonnees.
	/// </summary>
	public class CContexteSauvegardeObjetsDonnees
	{
		private CContexteDonnee m_contexteDonnees = null;
		private CListeRestrictionsUtilisateurSurType m_restrictions = new CListeRestrictionsUtilisateurSurType();
		private CDonneeNotificationModificationContexteDonnee m_donneeNotification = null;

		private CVersionDonnees m_versionEnCours = null;

		/// ////////////////////////////////////////////////
		public CContexteSauvegardeObjetsDonnees(
			CContexteDonnee contexte,
			CDonneeNotificationModificationContexteDonnee donneeNotification,
			CListeRestrictionsUtilisateurSurType listeRestrictions )
		{
			m_contexteDonnees = contexte;
			m_donneeNotification = donneeNotification;
			m_restrictions = listeRestrictions;
		}

		/// ////////////////////////////////////////////////
		public CContexteDonnee ContexteDonnee
		{
			get
			{
				return m_contexteDonnees;
			}
			set
			{
				m_contexteDonnees = value;
			}
		}

		/// ////////////////////////////////////////////////
		public CDonneeNotificationModificationContexteDonnee DonneeNotification
		{
			get
			{
				return m_donneeNotification;
			}
		}

		/// ////////////////////////////////////////////////
		public CVersionDonnees VersionDonneesAssociee
		{
			get
			{
				return m_versionEnCours;
			}
			set
			{
				m_versionEnCours = value;
			}
		}

		/// ////////////////////////////////////////////////
		public CListeRestrictionsUtilisateurSurType Restrictions
		{
			get
			{
				return m_restrictions;
			}
		}
	}
}
