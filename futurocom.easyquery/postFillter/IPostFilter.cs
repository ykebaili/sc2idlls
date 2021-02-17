using sc2i.common;
using sc2i.drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futurocom.easyquery.postFillter
{
    public interface IPostFilter : I2iSerializable
    {
        CResultAErreurType<DataTable> FiltreData(DataTable tableSource, CEasyQuery query, CListeQuerySource sources);

        void Draw(IObjetDeEasyQuery objetPossedant, CContextDessinObjetGraphique ctxDessin);
    }
}
