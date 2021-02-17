using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace sc2i.common.memorydb
{
    [Serializable]
    public abstract class CEntiteDeMemoryDbAIdAuto : CEntiteDeMemoryDb
    {
        //-------------------------------------------------
        public CEntiteDeMemoryDbAIdAuto(CMemoryDb db)
            : base(db)
        {
        }

        //-------------------------------------------------
        public CEntiteDeMemoryDbAIdAuto(DataRow row)
            : base(row)
        {
        }

        //-------------------------------------------------
        public void CreateNew()
        {
            CreateNew(CUniqueIdentifier.GetNew());
        }

        //-------------------------------------------------
        public override int GetHashCode()
        {
            return (GetType().ToString() + "/" + Id).GetHashCode();
        }

        //------------------------------------------------------------------------------
        public CEntiteDeMemoryDbAIdAuto GetCopieSansFils()
        {
            CEntiteDeMemoryDbAIdAuto copie = Activator.CreateInstance ( GetType(), new object[]{Database} ) as CEntiteDeMemoryDbAIdAuto;
            copie.CreateNew();
            DataRow row = copie.Row.Row;
            bool bEnforeConstraints = Database.EnforceConstraints;
            Database.EnforceConstraints = false;
            foreach (DataColumn col in Row.Table.Columns)
            {
                if (col.ColumnName != GetChampId())
                    row[col.ColumnName] = Row[col.ColumnName];
            }
            try
            {
                Database.EnforceConstraints = bEnforeConstraints;
            }
            catch (ConstraintException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return copie;
        }
    }
}
