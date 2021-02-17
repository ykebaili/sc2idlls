using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.data.Excel
{
    public interface IPlageDonnees
    {
        string[] Entetes{get;}
        object[] GetFirstLine();
        object[] GetNextLine();

    }
}
