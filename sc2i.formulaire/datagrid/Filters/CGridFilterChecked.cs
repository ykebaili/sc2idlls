using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.formulaire.datagrid.Filters
{
    public class CGridFilterChecked : CGridFilterForWndDataGrid
    {
        //-----------------------------------------
        public override string Label
        {
            get { return I.T("Checked|20032"); }
        }

        //-----------------------------------------
        public override bool IsValueIn(object valeur)
        {
            return valeur is bool && (bool)valeur;
        }


        public static IEnumerable<CGridFilterForWndDataGrid> GetFiltresBool()
        {
            return new CGridFilterForWndDataGrid[]{
                new CGridFilterChecked(),
                new CGridFilterUnChecked(),
                new CGridFilterNotSet()};
        }
    }

    public class CGridFilterUnChecked : CGridFilterForWndDataGrid
    {
        //-----------------------------------------
        public override string Label
        {
            get { return I.T("Unchecked|20033"); }
        }

        //-----------------------------------------
        public override bool IsValueIn(object valeur)
        {
            return valeur is bool && !(bool)valeur;
        }

        
    }
}
