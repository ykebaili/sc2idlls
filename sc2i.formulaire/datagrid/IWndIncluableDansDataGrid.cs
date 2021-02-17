using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sc2i.formulaire.datagrid
{
    public interface IWndIncluableDansDataGrid : I2iWndComposantFenetre
    {
        Type ValueTypeForGrid { get; }
        string ConvertObjectValueToStringForGrid(object objectValue);
        object GetObjectValueForGrid(object element);

        IEnumerable<CGridFilterForWndDataGrid> GetPossibleFilters();
    }

    public interface IWndIncluableDansDataGridADrawCustom : IWndIncluableDansDataGrid
    {
        //Retourne true s'il faut appeler la fonction standard de dessin après appel
        //du paint
        bool Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            object element);

        
    }
}
