using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.common.Restrictions
{
    /// <summary>
    /// Tout objet qui a des modifications dans un contexte de modification
    /// identifié par une chaine de caractères.
    /// Par exemple, les modifs sur un CObjetDonnee ont un contexte de modification
    /// qui est une chaine. C'est utilisé pour la gestion des droits
    /// </summary>
    public interface IObjetAModificationContextuelle
    {
        string ContexteDeModification { get;}
    }
}
