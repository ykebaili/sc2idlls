using System;
using System.Collections.Generic;
using System.Text;

namespace sc2i.documents
{
    [Serializable]
    public class CSourceDocumentLienDirect : CSourceDocument
    {
        private string m_strNomFichierLie = "";

        public CSourceDocumentLienDirect(string strNomFichierLie)
        {
            m_strNomFichierLie = strNomFichierLie;
        }

        public string NomFichier
        {
            get
            {
                return m_strNomFichierLie;
            }
        }

        public override CTypeReferenceDocument TypeReference
        {
            get 
            { 
                return new CTypeReferenceDocument(CTypeReferenceDocument.TypesReference.LienDirect); 
            }
        }
    }

}
