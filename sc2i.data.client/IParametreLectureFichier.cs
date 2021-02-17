using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Data;

namespace sc2i.data
{
    public interface IParametreLectureFichier : I2iSerializable
    {
        CMappageStringsStrings Mappage { get; set; }

        //Si succès, le data du result contient un datatable
        CResultAErreur LectureFichier(string strFileName);

        DataColumn[] GetColonnes();

    }
}
