using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace sc2i.data
{
    public static class CUtilDataRow
    {
        //--------------------------------------------------------------
        public static void Sc2iDelete(this DataRow row)
        {
            List<DataRow> lstDone = new List<DataRow>();
            try{
            Sc2iDelete(row, lstDone);
            }
            catch ( Exception e )
            {
                lstDone.Reverse();
                foreach ( DataRow rowRev in lstDone )
                    rowRev.RejectChanges();
            }
        }

        //--------------------------------------------------------------
        private static void Sc2iDelete ( DataRow row, List<DataRow> lstDone )
        {
            if (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached)
                return;//elle est déjà supprimée
            foreach (DataRelation rel in row.Table.ChildRelations)
            {
                if (rel.ChildKeyConstraint.DeleteRule == Rule.Cascade)
                {
                    foreach (DataRow child in row.GetChildRows(rel))
                        Sc2iDelete(child);
                }
            }
            row.Delete();
            lstDone.Add(row);
        }
    }
}
