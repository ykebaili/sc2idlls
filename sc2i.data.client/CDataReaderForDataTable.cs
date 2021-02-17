using System;
using System.Collections;
using System.Data;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CDataReaderForDataTable.
	/// </summary>
	public class CDataReaderForDataTable : CDataRecordForDataRow, IDataReader, IEnumerable, IEnumerator
	{
		private int m_nIndex;


		/// /////////////////////////////////////////////////////////
		public CDataReaderForDataTable( DataTable table )
			:base ( )
		{
			m_table = table;
			m_nIndex = -1;
		}

		/// /////////////////////////////////////////////////////////
		public void Dispose()
		{
		}

		/// /////////////////////////////////////////////////////////
		public void Close()
		{
		}

		/// /////////////////////////////////////////////////////////
		public int Depth
		{
			get
			{
				return 0;
			}
		}

		/// /////////////////////////////////////////////////////////
		public DataTable GetSchemaTable()
		{
			return m_table;
		}

		/// /////////////////////////////////////////////////////////
		public bool IsClosed
		{
			get
			{
				return false;
			}
		}

		/// /////////////////////////////////////////////////////////
		public bool NextResult()
		{
			return false;
		}

		/// /////////////////////////////////////////////////////////
		public bool Read()
		{
			m_nIndex ++;
			if ( m_nIndex >= m_table.Rows.Count )
				return false;
			CurrentRow = m_table.Rows[m_nIndex];
			return true;
		}

		/// /////////////////////////////////////////////////////////
		public int RecordsAffected
		{
			get
			{
				return m_table.Rows.Count;
			}
		}

		/// /////////////////////////////////////////////////////////
		public IEnumerator GetEnumerator()
		{
			return this;
		}

		/// /////////////////////////////////////////////////////////
		public object Current
		{
			get
			{
				return CurrentRow;
			}
		}

		/// /////////////////////////////////////////////////////////
		public bool MoveNext()
		{
			return Read();
		}

		/// /////////////////////////////////////////////////////////
		public void Reset()
		{
			m_nIndex = 0;
			if ( m_nIndex > m_table.Rows.Count )
				CurrentRow = m_table.Rows[m_nIndex];
		}

		/// /////////////////////////////////////////////////////////
		public bool ContainsListCollection
		{
			get
			{
				return false;
			}
		}

	}
}
