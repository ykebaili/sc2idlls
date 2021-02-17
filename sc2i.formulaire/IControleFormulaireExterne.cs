using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using System.Drawing;

namespace sc2i.formulaire
{
    /// <summary>
    /// Interface de base pour les contrôles qui peuvent être attachés en externe
    /// à un formulaire.
    /// 
    /// </summary>
    public interface IControleFormulaireExterne
    {
        CDefinitionProprieteDynamique[] GetProprietes();

        CDescriptionEvenementParFormule[] GetDescriptionsEvenements();

        string Name { get; }

        Point Location { get; }

        Size Size { get; }

        /// <summary>
        /// au runtime : attache le controle à l'élément qui le gère
        /// dans la structure du formualire. C'est à cet élément qu'il doit passer 
        /// ses evenements
        /// </summary>
        void AttacheToWndFor2iWnd(IRuntimeFor2iWnd wnd);

        object Control { get; }
    }
}
