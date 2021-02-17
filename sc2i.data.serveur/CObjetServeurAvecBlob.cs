using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.common;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CObjetLoaderAvecBlob.
	/// </summary>
	public abstract class CObjetServeurAvecBlob : CObjetServeur
	{
		private ArrayList m_listeChampsBlobs = new ArrayList();
		/// ///////////////////////////////////////////////////////
#if PDA
		public CObjetServeurAvecBlob()
			:base ()
		{
		}
#endif
		
		/// ///////////////////////////////////////////////////////
		public CObjetServeurAvecBlob(int nIdSession)
			:base (nIdSession)
		{
		}

		/// ///////////////////////////////////////////////////////
		public override bool HasBlobs()
		{
			return true;
		}

		/// ///////////////////////////////////////////////////////
		public override string GetRequeteSelectForRead(IDatabaseConnexion connexion, params string[] strChampsARetourner)
		{
			CStructureTable structure = CStructureTable.GetStructure(GetTypeObjets());
			string strReq = "";
			ArrayList lstBlobs = new ArrayList();
			string strNomTable = structure.NomTableInDb;
			foreach (CInfoChampTable info in structure.Champs)
				if (info.TypeDonnee != typeof(CDonneeBinaireInRow))
					strReq += strNomTable + "." + info.NomChamp + ",";
				else
					lstBlobs.Add(info.NomChamp);
			lock (m_listeChampsBlobs)
			{
				m_listeChampsBlobs = lstBlobs;
			}
			if (strReq.Length > 0)
				strReq = strReq.Substring(0, strReq.Length - 1);
			strReq = "select " + strReq + " from " + GetNomTableInDb();
			return strReq;
		}

		/// ///////////////////////////////////////////////////////
		public override void AfterFill(DataTable table)
		{
			lock (m_listeChampsBlobs)
			{
				foreach (string strChampBlob in m_listeChampsBlobs)
				{
					if (!table.Columns.Contains(strChampBlob))
						table.Columns.Add(strChampBlob, typeof(CDonneeBinaireInRow)).DefaultValue = DBNull.Value;
				}
			}
		}

		/// ///////////////////////////////////////////////////////
		/*public override DataTable Read ( CFiltreData filtre, int nStart, int nEnd )
		{
			IDatabaseConnexion connexion = CSc2iDataServer.GetInstance().GetDatabaseConnexion (IdSession, GetType());
			string str
			IDataAdapter adapter = connexion.GetSimpleReadAdapter( strReq, filtre );
			DataSet ds = new DataSet();
			adapter.TableMappings.Add("Table",GetNomTable());
			adapter.FillSchema( ds, SchemaType.Mapped );
		    adapter.Fill(ds);
			DataTable tbl = ds.Tables[GetNomTable()];
			foreach ( string strChampBlob in lstBlobs )
				tbl.Columns.Add ( strChampBlob, typeof(CDonneeBinaireInRow)).DefaultValue = DBNull.Value;
			
			nEnd = Math.Min ( nEnd, tbl.Rows.Count );
			if ( nStart >= 0 && nEnd >= 0 )
			{
				DataTable tblNew = tbl.Clone();

				for ( int n = nStart; n < nEnd; n++ )
					tblNew.ImportRow ( tbl.Rows[n] );
				tbl = tblNew;
			}
			return tbl;
		}*/

		//////////////////////////////////////////////////
		public override CDataTableFastSerialize FillSchema()
		{
            DataTable tbl = (DataTable)m_tableSchema[GetNomTable()];
            if (tbl != null)
                return tbl.Clone();
			IDataAdapter adapter = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, GetType()).GetTableAdapter(GetNomTableInDb());
			adapter.TableMappings.Add ("Table", GetNomTable() );
			DataSet ds = new DataSet();
			adapter.FillSchema(ds, SchemaType.Mapped);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable table = ds.Tables[GetNomTable()];
			if (table == null)
			{
				table = ds.Tables[GetNomTableInDb()];
				if (table != null)
					table.TableName = GetNomTable();
			}
			CStructureTable structure = CStructureTable.GetStructure(GetTypeObjets());
			foreach ( CInfoChampTable info in structure.Champs )
				if ( info.TypeDonnee==typeof(CDonneeBinaireInRow) && table.Columns[info.NomChamp] != null)
					table.Columns[info.NomChamp].DataType = typeof(CDonneeBinaireInRow);
            tbl = ds.Tables[GetNomTable()];
            FaitLesCorrectionsSurLeSchema(tbl);
            m_tableSchema[GetNomTable()] = tbl.Clone();
			return tbl;
		}

		/// ///////////////////////////////////////////////////////
		/////////////////////////////////////////////////////////
		public override CResultAErreur SaveAll ( CContexteSauvegardeObjetsDonnees contexteSauvegarde, DataRowState etatsAPrendreEnCompte )
		{
			CResultAErreur result = CResultAErreur.True;
			
			//STEF : J'avais mis base.SaveAll après : pb : lors d'une création, on ne
			//le blob n'était pas sauvé car la sauvegarde d'un blob est un update
			//sur la table
			/*ArrayList lstRowsToSave = new ArrayList();
			foreach ( DataRow row in ds.Tables[GetNomTable()].Rows )
				if ( row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added )
					lstRowsToSave.Add ( row );*/

			//DataTable tableResult = null;

			if ( result )
				result = base.SaveAll(contexteSauvegarde, etatsAPrendreEnCompte);
			/*if ( result )
			{
				tableResult = (DataTable)result.Data;
				try
				{
					//s'il y a des données binaires, il faut les sauver
					DataTable table = ds.Tables[GetNomTable()];
					foreach ( DataColumn col in table.Columns )
					{
						if ( col.DataType == typeof(CDonneeBinaireInRow) )
						{
							string strPrim = table.PrimaryKey[0].ColumnName;
							foreach ( DataRow row in lstRowsToSave )
							{
								if (row[col.ColumnName] != DBNull.Value && 
									row[col.ColumnName] != null 
									)
								{
									object obj = row[col.ColumnName];
									CDonneeBinaireInRow data = (CDonneeBinaireInRow)obj;
									if ( data != null && data.HasChange() )
										result = SaveBlob (col.ColumnName, strPrim+"="+row[strPrim].ToString(), data.Donnees );
									if ( !result )
										break;
							
								}
							}
						}
						if ( !result )
							break;
					}
					if ( result )
						result.Data = tableResult;
			
				}
				catch ( Exception e )
				{
					result.EmpileErreur(new CErreurException (e ) );
					result.EmpileErreur("Erreur lors de la sauvegarde "+GetNomTable() );
				}
			}*/
			
			return result;
		}
	}
}
