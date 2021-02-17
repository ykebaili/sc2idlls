using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CRelationListeEntites_EntiteServeur : CObjetServeur
	{
		/// //////////////////////////////////////////////////
#if PDA
		public CRelationListeEntites_EntiteServeur()
			:base ()
		{
			
		}
#endif
		/// //////////////////////////////////////////////////
		public CRelationListeEntites_EntiteServeur( int nIdSession )
			:base ( nIdSession )
		{
			
		}

		//////////////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CRelationListeEntites_Entite.c_nomTable;
		}

		//////////////////////////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CRelationListeEntites_Entite);
		}

		//////////////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{	
				CRelationListeEntites_Entite rel = (CRelationListeEntites_Entite)objet;

				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}

		//////////////////////////////////////////////////////////////////////
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			DataTable table = contexte.Tables[GetNomTable()];
			ArrayList lst = new ArrayList ( table.Rows );
			foreach ( DataRow row in table.Rows )
			{
				if ( row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added )
				{
					CRelationListeEntites_Entite rel = new CRelationListeEntites_Entite ( row );
					rel.ListeEntites.Version++;
				}
			}
			return result;
		}


		


	}
}
