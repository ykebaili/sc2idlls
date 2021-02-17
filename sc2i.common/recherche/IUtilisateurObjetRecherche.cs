using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.recherche
{
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Un élément qui utilise l'objet recherche
    /// </summary>
    public interface IUtilisateurObjetRecherche
    {
        string LibelleUtilisateurObjetRecherche { get; }
    }

    [Serializable]
    public class CUtilisateurObjetRechercheLibelleOnly : IUtilisateurObjetRecherche
    {
        private string m_strLibelle = "";
        public CUtilisateurObjetRechercheLibelleOnly(string strLibelle)
        {
            m_strLibelle = strLibelle;
        }

        public string LibelleUtilisateurObjetRecherche
        {
            get
            {
                return m_strLibelle;
            }
        }
    }
}
