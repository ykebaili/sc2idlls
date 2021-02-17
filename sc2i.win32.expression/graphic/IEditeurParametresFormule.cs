using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;

namespace sc2i.win32.expression.graphic
{
    /// <summary>
    /// Interface permettant d'éditer les paramètres d'une formule
    /// </summary>
    public interface IEditeurParametresFormule
    {
        void Init(CRepresentationExpressionGraphique repGraphique, IFournisseurProprietesDynamiques fournisseur, CObjetPourSousProprietes objetAnalyse);
        event EventHandler OnChangeDessin;
    }
}
