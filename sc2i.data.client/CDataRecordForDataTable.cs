using System;
using System.Data;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de IDataRecordForDataTable.
	/// </summary>
	public class CDataRecordForDataRow : IDataRecord
	{
		protected DataTable m_table;
		protected DataRow m_currentRow;
		
		///////////////////////////////////////////////////////////////
		public CDataRecordForDataRow()
		{
			m_currentRow = null;
			m_table = null;
		}

		///////////////////////////////////////////////////////////////
		public CDataRecordForDataRow(DataRow row)
		{
			m_currentRow = row;
			m_table = row.Table;
		}

		///////////////////////////////////////////////////////////////
		public DataRow CurrentRow
		{
			get { return m_currentRow;}
			set { m_currentRow = value;m_table=value.Table;}
		}

		///////////////////////////////////////////////////////////////
		public int FieldCount
		{
			get { return m_table.Columns.Count;}
		}

		///////////////////////////////////////////////////////////////
		public object this[string strIndex] 
		{
			get { return m_currentRow[strIndex];}
		}

		///////////////////////////////////////////////////////////////
		public object this[int nIndex]
		{
			get { return m_currentRow[nIndex];}
		}

		///////////////////////////////////////////////////////////////
		public bool GetBoolean ( int i )
		{
			return (bool)m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public byte GetByte ( int i )
		{
			return (byte)m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public long GetBytes( int i,long fieldOffset,byte[] buffer,int bufferoffset,int length )
		{
			byte[] bts = (byte[])m_currentRow[i];
			for ( long n = fieldOffset; n < Math.Min(length, bts.Length); n++ )
				buffer[bufferoffset+n-fieldOffset] = bts[n];
			return Math.Max(0,Math.Min(length, bts.Length)-fieldOffset);
		}

		///////////////////////////////////////////////////////////////
		public char GetChar ( int i )
		{
			return (char)m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public long GetChars(int i,long fieldoffset,char[] buffer,int bufferoffset,int length)
		{
			char[] bts = (char[])m_currentRow[i];
			for ( long n = fieldoffset; n < Math.Min(length, bts.Length); n++ )
				buffer[bufferoffset+n-fieldoffset] = bts[n];
			return Math.Max(0,Math.Min(length, bts.Length)-fieldoffset);
		}

		///////////////////////////////////////////////////////////////
		public IDataReader GetData ( int i )
		{
			return null;
		}

		///////////////////////////////////////////////////////////////
		public string GetDataTypeName ( int i )
		{
			return m_table.Columns[i].DataType.ToString();
		}

		///////////////////////////////////////////////////////////////
		public DateTime GetDateTime ( int i )
		{
			return (DateTime)m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public decimal GetDecimal ( int i )
		{
			return ( decimal ) m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public double GetDouble ( int i )
		{
			return ( double)m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public Type GetFieldType ( int i)
		{
			return m_table.Columns[i].DataType;
		}

		///////////////////////////////////////////////////////////////
		public float GetFloat ( int i )
		{
			return (float)m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public Guid GetGuid ( int i )
		{
			return ( Guid ) m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public Int16 GetInt16 ( int i )
		{
			return ( Int16 ) m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public Int32 GetInt32 ( int i )
		{
			return( Int32 ) m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public long GetInt64 ( int i )
		{
			return (Int64) m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public string GetName ( int i )
		{
			return m_table.Columns[i].ColumnName;
		}

		///////////////////////////////////////////////////////////////
		public int GetOrdinal ( string strName )
		{
			return m_table.Columns[strName].Ordinal;
		}

		///////////////////////////////////////////////////////////////
		public string GetString ( int i )
		{
			return m_currentRow[i].ToString();
		}

		///////////////////////////////////////////////////////////////
		public object GetValue ( int i )
		{
			return m_currentRow[i];
		}

		///////////////////////////////////////////////////////////////
		public int GetValues ( object[] values )
		{
			int nIndex;
			for ( nIndex = 0; nIndex < Math.Min ( values.Length, m_table.Columns.Count); nIndex++ )
				values[nIndex] = m_currentRow[nIndex];
			return Math.Min ( values.Length, m_table.Columns.Count );
		}

		///////////////////////////////////////////////////////////////
		public bool IsDBNull ( int i )
		{
			return m_currentRow[i] == DBNull.Value;
		}
	}
}
