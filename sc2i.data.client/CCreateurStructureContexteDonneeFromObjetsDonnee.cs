using System;
using System.Collections;
using System.Data;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CCreateurStructureDataSetFromObjetsDonnee.
	/// </summary>
	public class CCreateurStructureContexteDonneeFromObjetsDonnee
	{
		CContexteDonnee m_contexte;
		/// //////////////////////////////////
		public CCreateurStructureContexteDonneeFromObjetsDonnee( CContexteDonnee ctxToFill)
		{
			m_contexte = ctxToFill;
			
		}

		/// ///////////////////////////////////// //////////////////////////////////
		public CContexteDonnee CreateStructureFromTypes ( params Type[] lesTypes )
		{
			ArrayList lstStructures = new ArrayList();
			foreach ( Type tp in lesTypes )
			{
				CStructureTable structure = CStructureTable.GetStructure(tp);
				lstStructures.Add (structure);
			}
			
			//Création des tables
			foreach ( CStructureTable structure in lstStructures )
			{
				DataTable table = new DataTable(structure.NomTable);
				foreach ( CInfoChampTable champ in structure.Champs )
				{
					DataColumn col = new DataColumn ( champ.NomChamp );
					col.DataType = champ.TypeDonnee;
					if ( champ.Longueur > 0 )
						col.MaxLength = champ.Longueur;
					table.Columns.Add ( col );
				}
				DataColumn[] cols = new DataColumn[structure.ChampsId.Length];
				for ( int nChamp = 0; nChamp < structure.ChampsId.Length; nChamp++ )
					cols[nChamp] = table.Columns[structure.ChampsId[nChamp].NomChamp];
				table.PrimaryKey = cols;
				m_contexte.Tables.Add ( table );
			}
			//Les relations se créent toutes seules par le contexte de données
			return m_contexte;
		}
	}
}
