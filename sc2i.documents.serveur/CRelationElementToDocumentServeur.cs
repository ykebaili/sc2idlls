using System;
using System.IO;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.documents;

#if !PDA_DATA

namespace sc2i.documents.serveur
{
	/// <summary>
	/// Description résumée de CRelationElementToDocumentServeur.
	/// </summary>
	public class CRelationElementToDocumentServeur : CObjetServeurAvecBlob
	{
		public CRelationElementToDocumentServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CRelationElementToDocument.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CRelationElementToDocument relation = (CRelationElementToDocument)objet;

				if ( relation.DocumentGed == null )
				{
					result.EmpileErreur(I.T("The dependent document should not be null|116"));
				}
				if ( relation.ElementLie == null )
				{
					result.EmpileErreur(I.T("The dependent element cannot be null|117"));
				}

			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CRelationElementToDocument);
		}
		//-------------------------------------------------------------------
	}
}
#endif