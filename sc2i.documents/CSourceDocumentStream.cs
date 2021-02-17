using System;

using System.IO;

#if !PDA_DATA

namespace sc2i.documents
{
	/// <summary>
	/// Description résumée de CSourceDocumentStream.
	/// </summary>
	[Serializable]
	public class CSourceDocumentStream : CSourceDocument
	{
		private Stream m_stream;
		private string m_strExtension = "dat";

		public CSourceDocumentStream()
		{
		}

        

		public CSourceDocumentStream(Stream stream)
		{
			m_stream = stream;
		}

        public override void Dispose()
        {
            if (m_stream != null)
                m_stream.Dispose();
            m_stream = null;
        }

		public CSourceDocumentStream(Stream stream, string strExtension)
		{
			m_stream = stream;
			m_strExtension = strExtension;
		}

		public Stream SourceStream
		{
			get
			{
				return m_stream;
			}
			set
			{
				m_stream = value;
			}
		}

		public string Extension
		{
			get
			{
				return m_strExtension;
			}
			set
			{
				m_strExtension = value;
			}
		}

        public override CTypeReferenceDocument TypeReference
        {
            get
            {
                return new CTypeReferenceDocument(CTypeReferenceDocument.TypesReference.Fichier);
            }
        }
	}
}
#endif