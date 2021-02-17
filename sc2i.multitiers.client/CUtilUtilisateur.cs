using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.multitiers.client
{
    public interface IFournisseurInformationsUtilisateur
    {
        int GetIdUtilisateurFromKey(CDbKey key);
        CDbKey GetKeyUtilisateurFromId(int nId);
    }


    //Classe permettant d'obtenir des informations sur les utilisateurs
    public static class CUtilInfosUtilisateur
    {
        private static IFournisseurInformationsUtilisateur m_fournisseurInfos = null;

        public static void SetFournisseurInfosUtilisateur(IFournisseurInformationsUtilisateur fournisseur)
        {
            if ( fournisseur != null )
                m_fournisseurInfos = fournisseur;
        }

        public static int GetIdUtilisateurFromKey(CDbKey key)
        {
            if (m_fournisseurInfos != null)
                return m_fournisseurInfos.GetIdUtilisateurFromKey(key);
            return -1;
        }

        public static CDbKey GetKeyUtilisateurFromId(int nId)
        {
            if (m_fournisseurInfos != null)
                return m_fournisseurInfos.GetKeyUtilisateurFromId(nId);
            return null;
        }
        
    }
}
