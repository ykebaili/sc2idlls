using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.drawing;
using System.Drawing;
using sc2i.common;

namespace sc2i.process.workflow.dessin
{
    public interface IWorflowDessin : I2iObjetGraphique
    {
        string IdUniversel { get; }

        Point[] GetPolygoneDessin();

        CResultAErreur Delete();

        string Text { get; }
    }
}
