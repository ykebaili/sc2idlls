using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace sc2i.common
{
	[Serializable]
	public class CDataTableFastSerialize : ISerializable, IDisposable
	{
		private DataTable m_dataTable = null;

		public CDataTableFastSerialize(DataTable ds)
		{
			m_dataTable = ds;
		}

		public DataTable DataTableObject
		{
			get
			{
				return m_dataTable;
			}
		}

        public void Dispose()
        {
            if (m_dataTable != null)
                m_dataTable.Dispose();
            m_dataTable = null;
        }

		//Serialization
		public CDataTableFastSerialize(SerializationInfo info, StreamingContext context)
		{
			byte[] data = (byte[])info.GetValue("DataTable_DATA", typeof(byte[]));
			m_dataTable = AdoNetHelper.DeserializeDataTable(data);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			byte[] data = AdoNetHelper.SerializeDataTable(m_dataTable);
			info.AddValue("DataTable_DATA", data);
		}


		//Conversion
		public static implicit operator DataTable ( CDataTableFastSerialize fastDataTable )
		{
			return fastDataTable.DataTableObject;
		}

		public static implicit operator CDataTableFastSerialize(DataTable table)
		{
			return new CDataTableFastSerialize ( table );
		}
	}
}
