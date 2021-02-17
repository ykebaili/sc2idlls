using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.data
{
    /// <summary>
    /// Fournit des liens entre un élément à EO et des EOS
    /// </summary>
    public interface IRelationsEOProvider
    {
        CListeObjetsDonnees GetEntiteOrganisationnellesDirectementLiees(IElementAEO elt);

        CResultAErreur AjouterEO(IElementAEO elt, int nIdEO);
        CResultAErreur SupprimerEO(IElementAEO elt, int nIdEO);
        CResultAErreur SetAllOrganizationalEntities(IElementAEO element, int[] nIdsOE);

        void CompleteRestriction(IElementAEO elt, CRestrictionUtilisateurSurType restriction);
    }
}
