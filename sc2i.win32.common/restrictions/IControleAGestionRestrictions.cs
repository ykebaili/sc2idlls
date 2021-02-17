using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using sc2i.common.Restrictions;

namespace sc2i.win32.common
{
    /// <summary>
    /// Pour les contrôles qui savent appliquer des restrictions
    /// </summary>
    public interface IControleAGestionRestrictions
    {
        void AppliqueRestrictions(CListeRestrictionsUtilisateurSurType restrictions,
            IGestionnaireReadOnlySysteme gestionnaireReadOnly);
    }
}
