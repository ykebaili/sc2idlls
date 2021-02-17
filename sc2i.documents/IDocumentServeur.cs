using System;

using sc2i.common;

#if !PDA_DATA

namespace sc2i.documents
{
	/// <summary>
	/// Description résumée de IDocumentServeur.
	/// </summary>
	public interface IDocumentServeur
	{
		CResultAErreur GetDocument(CReferenceDocument refDoc);
		CResultAErreur SaveDocument ( 
            CSourceDocument source, 
            CTypeReferenceDocument typeReference, 
            CReferenceDocument versionPrecedente,
            bool bIncrementeVersionFichier );
        CTypeReferenceDocument[] TypesAutorisesPourLesUtilisateurs { get;}
	}
}
#endif