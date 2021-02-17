using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.common;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CListeEntitesServeur.
	/// </summary>
	public class CListeEntitesServeur : CObjetServeurAvecBlob
	{
#if PDA
		public CListeEntitesServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CListeEntitesServeur( int nIdSession )
			:base(nIdSession)
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CListeEntites.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CListeEntites listeEntites = (CListeEntites)objet;

				if ( listeEntites.Libelle.Trim() == "" )
					result.EmpileErreur(I.T("Indicate a name for this list|125"));

				
				if(  listeEntites.TypeElements == null )
				{
					result.EmpileErreur(I.T("The entities list must be associated with an entity type|126"));
				}

				//Controler que le libellé est unique pour le type
				CFiltreData filtre = new CFiltreData ( 
					CListeEntites.c_champId+"<>@1 and "+
					CListeEntites.c_champLibelle+"=@2",
					listeEntites.Id, listeEntites.Libelle );
				if ( CountRecords ( GetNomTable(), filtre ) != 0 )
					result.EmpileErreur(I.T("Another list has this label|127"));

				if ( listeEntites.Code.Trim() != "" )
				{
					//Controler que le code est unique pour le type
					filtre = new CFiltreData ( 
						CListeEntites.c_champId+"<>@1 and "+
						CListeEntites.c_champCode+"=@2",
						listeEntites.Id, listeEntites.Code );
					if ( CountRecords ( GetNomTable(), filtre ) != 0 )
					{
						result.EmpileErreur(I.T("Another list has this code|128"));
					}
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
			return typeof(CListeEntites);
		}
		
		//-------------------------------------------------------------------
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			DataTable table = contexte.Tables[GetNomTable()];
			foreach ( DataRow row in table.Rows )
			{
				if ( row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added )
				{
					//Vérifie que tous les éléments ne sont ajoutés qu'une seule fois
					CListeEntites liste = new CListeEntites ( row );
					CListeObjetsDonnees relations = liste.RelationsEntites;
					relations.Tri = CRelationListeEntites_Entite.c_champIdElement;
					int nLastVal = -1;
					foreach ( CRelationListeEntites_Entite rel in relations )
					{
						if ( rel.IdElement == nLastVal )
							rel.Delete();
						else
							nLastVal = rel.IdElement;
					}
				}
			}
			return result;
		}

	}
}
