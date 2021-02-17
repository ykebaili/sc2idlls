using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sc2i.formulaire.datagrid
{
    public abstract class CGridFilterForWndDataGrid
    {
        public abstract string Label { get; }

        public abstract bool IsValueIn(object valeur);

    }
}
