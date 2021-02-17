using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.drawing
{
    /// <summary>
    /// Objet graphique qui n'accepte pas n'import quel fils
    /// </summary>
    public interface I2iObjetGraphiqueConteneurAFilsChoisis : I2iObjetGraphique
    {
        bool AcceptAllChilds(IEnumerable<I2iObjetGraphique> objets);
    }
}
