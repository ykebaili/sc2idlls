using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
    //Permet de stocker des objets donnée dont on souhaite se souvenir
    public class CMemoObjetDonnee
    {
        public class CReferenceObjetDonneeAvecLibelle : I2iSerializable
        {
            private CReferenceObjetDonnee m_reference = null;
            private string m_strLibelle = null;

            public CReferenceObjetDonneeAvecLibelle()
            {
            }

            public CReferenceObjetDonneeAvecLibelle(CReferenceObjetDonnee reference)
            {
                m_reference = reference;
            }
            public static implicit operator CReferenceObjetDonnee(CReferenceObjetDonneeAvecLibelle refe)
            {
                return refe.m_reference;
            }

            public string Libelle
            {
                get
                {
                    if (m_strLibelle == null)
                    {
                        CObjetDonnee objet = m_reference.GetObjet(CContexteDonneeSysteme.GetInstance());
                        if (objet != null)
                            m_strLibelle = objet.DescriptionElement;
                        else
                            m_strLibelle = "??";
                    }
                    return m_strLibelle;
                }
            }

            public CReferenceObjetDonnee ReferenceObjet
            {
                get
                {
                    return m_reference;
                }
            }

            private int GetNumVersion()
            {
                return 0;
            }

            public CResultAErreur Serialize(C2iSerializer serializer)
            {
                int nVersion = GetNumVersion();
                CResultAErreur result = serializer.TraiteVersion(ref nVersion);
                if (!result)
                    return result;
                result = serializer.TraiteObject<CReferenceObjetDonnee>(ref m_reference);
                if (!result)
                    return result;
                string strLibelle = "";
                if (serializer.Mode == ModeSerialisation.Ecriture && m_strLibelle == null)
                    //S'assure que le libellé est bien chargé
                    strLibelle = Libelle;
                serializer.TraiteString(ref m_strLibelle);
                return result;
            }

        }

        private static CMemoObjetDonnee m_instance = null;

        public static CMemoObjetDonnee GetInstance()
        {
            if (m_instance == null)
                m_instance = new CMemoObjetDonnee();
            return m_instance;
        }

        private List<CReferenceObjetDonneeAvecLibelle> m_listeObjets = new List<CReferenceObjetDonneeAvecLibelle>();


        public bool AddObjet(CReferenceObjetDonnee objet)
        {
            bool bExiste = false;
            
            foreach (CReferenceObjetDonneeAvecLibelle refe in m_listeObjets)
                if (((CReferenceObjetDonnee)refe).Equals(objet))
                {
                    bExiste = true;
                    break;
                }
            if (!bExiste)
            {
                m_listeObjets.Add(new CReferenceObjetDonneeAvecLibelle(objet));
                return true;
            }
            return false;
        }

        public bool RemoveObjet(CReferenceObjetDonnee objet)
        {
            foreach (CReferenceObjetDonneeAvecLibelle refe in m_listeObjets)
            {
                if (refe.ReferenceObjet != null &&
                    refe.ReferenceObjet.Equals(objet))
                {
                    m_listeObjets.Remove(refe);
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<CReferenceObjetDonneeAvecLibelle> Objets
        {
            get
            {
                return m_listeObjets.AsReadOnly();
            }
        }

        public void Clear()
        {
            m_listeObjets.Clear();
        }

        public void Read()
        {
            string strValue = C2iRegistre.GetValueInRegistreApplication("Preferences", "UserMemo", "");
            if (strValue.Trim() != "")
            {
                List<CReferenceObjetDonneeAvecLibelle> lst = new List<CReferenceObjetDonneeAvecLibelle>();
                CStringSerializer ser = new CStringSerializer(strValue, ModeSerialisation.Lecture);
                if (ser.TraiteListe<CReferenceObjetDonneeAvecLibelle>(lst))
                    m_listeObjets = lst;
            }
        }

        private void Save()
        {
            CStringSerializer ser = new CStringSerializer(ModeSerialisation.Ecriture);
            if (ser.TraiteListe<CReferenceObjetDonneeAvecLibelle>(m_listeObjets))
                C2iRegistre.SetValueInRegistreApplication("Preferences", "UserMemo", ser.String);
        }
    }
}
