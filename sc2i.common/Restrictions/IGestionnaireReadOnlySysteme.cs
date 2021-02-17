using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.common.Restrictions
{
    public interface IGestionnaireReadOnlySysteme
    {
        //-------------------------------------------------------------------
        /// <summary>
        /// Indique qu'un contrôle est en readonly système
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="bReadOnly"></param>
        void SetReadOnly(object ctrl, bool bReadOnly);

        //-------------------------------------------------------------------
        /// <summary>
        /// Initialise le gestionnaire pour un contrôle.
        /// </summary>
        /// <param name="racine"></param>
        void Init(object racine);

        //-------------------------------------------------------------------
        /// <summary>
        /// Ajoute un contrôle à gérer, équivalent de Init,
        /// mais pour un contrôle ajouté dynamiquement
        /// au contrôle principal sur lequel a été fait l'init
        /// </summary>
        /// <param name="ctrl"></param>
        void AddControl(object ctrl);

        //-------------------------------------------------------------------
        /// <summary>
        /// Nettoie toutes les restrictions
        /// </summary>
        void Clean();
    }
}
