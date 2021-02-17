using System;

using sc2i.common;


namespace sc2i.documents
{
	/// <summary>
	/// Description résumée de CReferenceDocument.
	/// </summary>
	[Serializable]
	public class CReferenceDocument : I2iSerializable
	{
		private CTypeReferenceDocument.TypesReference m_nCodeTypeRef = 0;
		private string m_strNomFichier = "";
        private int m_nSize = 0; // Taille du fichier en octets

		//-------------------------------------------------------------------
		public CReferenceDocument()
		{
		}
		//-------------------------------------------------------------------
		private int GetNumVersion()
		{
			//return 0;
            return 1; // Ajout de la Taille du fichier
		}
		//-------------------------------------------------------------------
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			CResultAErreur result = CResultAErreur.True;

			int nVersion = GetNumVersion();
			result = serializer.TraiteVersion( ref nVersion );
			if ( !result )
				return result;

			int nCode = (int)m_nCodeTypeRef;
			serializer.TraiteInt ( ref nCode );
			m_nCodeTypeRef = (CTypeReferenceDocument.TypesReference)nCode;

			if ( !result )
				return result;
			serializer.TraiteString ( ref m_strNomFichier );

            if (nVersion >= 1)
                serializer.TraiteInt(ref m_nSize);

			return result;
		}
		//-------------------------------------------------------------------
		[DynamicField("Reference type")]
		public CTypeReferenceDocument TypeReference
		{
			get
			{
				return new CTypeReferenceDocument((CTypeReferenceDocument.TypesReference)CodeTypeReference);
			}
			set
			{
				if (value == null)
					return;
				CodeTypeReference = value.Code;
			}
		}
		
		//-------------------------------------------------------------------
		public CTypeReferenceDocument.TypesReference CodeTypeReference
		{
			get
			{
				return (CTypeReferenceDocument.TypesReference)m_nCodeTypeRef;
			}
			set
			{
				m_nCodeTypeRef = value;
			}
		}

		//-------------------------------------------------------------------
        [DynamicField("File Name")]
		public string NomFichier
		{
			get
			{
				return m_strNomFichier;
			}
			set
			{
				m_strNomFichier = value;
			}
		}

        //-------------------------------------------------------------------
        [DynamicField("Size")]
        public int TailleFichier
        {
            get
            {
                return m_nSize;
            }
            set
            {
                m_nSize = value;
            }
        }
       

		//-------------------------------------------------------------------
		public override bool Equals(object obj)
		{
            CReferenceDocument refDoc = obj as CReferenceDocument;
			if ( refDoc == null )
                return false;
            return refDoc.NomFichier.Equals ( NomFichier ) && refDoc.CodeTypeReference.Equals ( CodeTypeReference );
		}

		//-------------------------------------------------------------------
		public override int GetHashCode()
		{
			return (NomFichier+CodeTypeReference.ToString()).GetHashCode();
		}

		//-------------------------------------------------------------------
		public string GetExtension()
		{
			int nIndex = m_strNomFichier.LastIndexOf ( "." );
			if ( nIndex > 0 )
				return m_strNomFichier.Substring ( nIndex+1 );
			return "dat";
		}


	}
}
