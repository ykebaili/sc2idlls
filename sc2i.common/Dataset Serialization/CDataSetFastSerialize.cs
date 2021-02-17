using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace sc2i.common
{
	[Serializable]
	public class CDataSetFastSerialize : ISerializable, IDisposable
	{
		private DataSet m_dataset = null;

		public CDataSetFastSerialize(DataSet ds)
		{
			m_dataset = ds;
		}

        public void Dispose()
        {
            if (m_dataset != null)
                m_dataset.Dispose();
            m_dataset = null;
        }

		public DataSet DataSetObject
		{
			get
			{
				return m_dataset;
			}
		}

		//Serialization
		public CDataSetFastSerialize(SerializationInfo info, StreamingContext context)
		{
			byte[] data = (byte[])info.GetValue("DATASET_DATA", typeof(byte[]));
			m_dataset = AdoNetHelper.DeserializeDataSet(data);
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			byte[] data = AdoNetHelper.SerializeDataSet(m_dataset);
			info.AddValue("DATASET_DATA", data);
		}

		//Conversion
		public static implicit operator DataSet(CDataSetFastSerialize fastDataSet)
		{
			return fastDataSet.DataSetObject;
		}

		public static implicit operator CDataSetFastSerialize(DataSet ds)
		{
			return new CDataSetFastSerialize(ds);
		}
	}
}
