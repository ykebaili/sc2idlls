using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace sc2i.common
{
	/// <summary>
	/// Description résumée de C2iObjetPersistant.
	/// </summary>
	public abstract class C2iObjetPersistant : IDisposable
	{
		[Serializable]
		private class CClassePourNull
		{
			public CClassePourNull()
			{
			}
		}

		protected C2iObjetPersistant()
		{
		}

		private BinaryFormatter m_formatter;
		private Stream m_stream;
		/////////////////////////////////////////////////////////////////
		protected abstract int GetNumVersion();

		/////////////////////////////////////////////////////////////////
		protected abstract CResultAErreur WriteMyPersistantData (  );

		/////////////////////////////////////////////////////////////////
		protected abstract CResultAErreur ReadMyPersistantData ( int nVersion );

		/////////////////////////////////////////////////////////////////
		protected void Serialize ( object obj )
		{
			if ( obj!= null )
				m_formatter.Serialize(m_stream, obj);
			else
				m_formatter.Serialize(m_stream, new CClassePourNull());
		}

		/////////////////////////////////////////////////////////////////
		protected object DeSerialize()
		{
			object obj = m_formatter.Deserialize(m_stream);
			if ( obj is CClassePourNull )
				return null;
			else
				return obj;
		}

		/////////////////////////////////////////////////////////////////
		public byte[] GetPersistantData()
		{
			m_stream = new MemoryStream();
			m_formatter = new BinaryFormatter();
			Serialize(GetNumVersion());
			if ( !WriteMyPersistantData (  ) )
				return null;
			m_stream.Close();
			return ((MemoryStream)m_stream).ToArray();
		}

		/////////////////////////////////////////////////////////////////
		public CResultAErreur SetPersistantData( Byte[] data )
		{
			CResultAErreur result = CResultAErreur.True;
			m_stream = new MemoryStream(data);
			m_formatter = new BinaryFormatter();

			int nVersion = (int)DeSerialize();
			if ( nVersion > GetNumVersion() )
			{
				result.EmpileErreur(I.T("Persisting data reading error : Incompatible version number|30010"));
				return result;
			}
			result = ReadMyPersistantData( nVersion );
			return result;
		}

        protected virtual void Dispose(bool bVal)
        {
            if (bVal)
                m_stream.Close();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
	}
}
