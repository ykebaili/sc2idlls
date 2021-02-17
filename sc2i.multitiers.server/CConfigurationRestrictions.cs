using System;
using System.Collections;
using System.IO;

using System.Xml;

using sc2i.multitiers.client;
using sc2i.common;

namespace sc2i.multitiers.server
{
    /// <summary>
    /// Gère des configurations de restrictions standard
    /// à appliquer
    /// </summary>
    public class CConfigurationRestrictions
    {
#if DEBUG
		public static void ClearRestrictions()
		{
			m_tableRestrictions = new Hashtable();
		}
#endif
        //Cle restriction->CListeRestrictionsUtilisateurSurType
        private static Hashtable m_tableRestrictions = new Hashtable();

        public static CResultAErreur InitFromXml(string strFichier)
        {
            /*
             * <configuration>
             *	<sc2iRestrictions>
             *		<Mode cle="  ">
             *			<Restriction type="Cafel.data.partenaire" restriction="NoCreate">
             *				<Propriete name="Libelle" restriction="ReadOnly"/>
             *			</Restriction>
             *		</Mode>
             *	</sc2iRestrictions>
             * <configuration>
             * */
            CResultAErreur result = CResultAErreur.True;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(new StreamReader(strFichier));

                //Noeud login
                XmlNode node = xmlDoc.SelectSingleNode("/configuration/sc2iRestrictions");
                if (node == null)
                {
                    return result;
                }
                XmlNode child = node.FirstChild;
                while (child != null)
                {
                    if (child.Name.ToUpper() == "MODE")
                    {
                        string strMode = child.Attributes["cle"].Value;
                        XmlNode nodeClass = child.FirstChild;
                        while (nodeClass != null)
                        {
                            if (nodeClass.Name.ToUpper() == "RESTRICTION")
                            {
                                CListeRestrictionsUtilisateurSurType liste = (CListeRestrictionsUtilisateurSurType)m_tableRestrictions[strMode];
                                if (liste == null)
                                {
                                    liste = new CListeRestrictionsUtilisateurSurType();
                                    m_tableRestrictions[strMode] = liste;
                                }
                                Type tp = CActivatorSurChaine.GetType(nodeClass.Attributes["type"].Value);
                                if (tp != null)
                                {
                                    CRestrictionUtilisateurSurType restriction = liste.GetRestriction(tp);
                                    string strRestriction = nodeClass.Attributes["restriction"].Value;
                                    restriction.RestrictionUtilisateur = (ERestriction)Enum.Parse(typeof(ERestriction), strRestriction);
                                    XmlNode nodePropriete = nodeClass.FirstChild;
                                    while (nodePropriete != null)
                                    {
                                        if (nodePropriete.Name.ToUpper() == "PROPRIETE")
                                        {
                                            restriction.SetRestrictionLocale(nodePropriete.Attributes["name"].Value,
                                                (ERestriction)Enum.Parse(typeof(ERestriction), nodePropriete.Attributes["restriction"].Value.ToString()));
                                        }
                                        nodePropriete = nodePropriete.NextSibling;
                                    }
                                    liste.AddRestriction(restriction);
                                }
                            }
                            nodeClass = nodeClass.NextSibling;
                        }
                    }
                    child = child.NextSibling;
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error in the restrictions configuration|102"));
            }

            return result;
        }

        /////////////////////////////////////////////////////////////////////////
        public static void AppliqueRestrictions(string strCle, CListeRestrictionsUtilisateurSurType restrictions)
        {
            CListeRestrictionsUtilisateurSurType liste = (CListeRestrictionsUtilisateurSurType)m_tableRestrictions[strCle];
            restrictions.Combine(liste);
        }

        public static void AjouteRestrictions(string strCle, CListeRestrictionsUtilisateurSurType restrictions)
        {
            CListeRestrictionsUtilisateurSurType liste = (CListeRestrictionsUtilisateurSurType)m_tableRestrictions[strCle];
            if (liste == null)
            {
                liste = new CListeRestrictionsUtilisateurSurType();
                m_tableRestrictions[strCle] = liste;
            }

            liste.Combine(restrictions);
        }

        /////////////////////////////////////////////////////////////////////////
        public static void AppliqueRestriction(string strCle, CRestrictionUtilisateurSurType restriction)
        {
            CListeRestrictionsUtilisateurSurType liste = (CListeRestrictionsUtilisateurSurType)m_tableRestrictions[strCle];
            if (liste != null)
            {
                CRestrictionUtilisateurSurType rest = liste.GetRestriction(restriction.TypeAssocie);
                restriction.Combine(rest);
            }
        }

        public static void AjouteRestriction(string strCle, CRestrictionUtilisateurSurType restriction)
        {
            CListeRestrictionsUtilisateurSurType liste = (CListeRestrictionsUtilisateurSurType)m_tableRestrictions[strCle];
            if (liste == null)
            {
                liste = new CListeRestrictionsUtilisateurSurType();
                m_tableRestrictions[strCle] = liste;
            }

            CRestrictionUtilisateurSurType rest = liste.GetRestriction(restriction.TypeAssocie);
            rest.Combine(restriction);
        }

    }
}