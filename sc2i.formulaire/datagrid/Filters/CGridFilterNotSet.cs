using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;

namespace sc2i.formulaire.datagrid.Filters
{
    public class CGridFilterNotSet : CGridFilterForWndDataGrid
    {
        //-----------------------------------------
        public override string Label
        {
            get { return I.T("Not set|20034"); }
        }

        //-----------------------------------------
        public override bool IsValueIn(object valeur)
        {
            return valeur == null || valeur == DBNull.Value;
        }

        
    }
}
