using System;
using System.Data;
using System.Collections;
using sc2i.common;
using System.Collections.Generic;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CUtilDataSet.
	/// </summary>
	public class CUtilDataSet
	{
		/// ////////////////////////////////////////////////////////////////////////
		public static void Merge ( DataTable tableSource, DataSet dsDest, bool bPreseveChanges )
		{
			DataTable tableDest = dsDest.Tables[tableSource.TableName];
			if ( tableDest == null )
			{
				//Création de la table
				tableDest = AddTableCopie ( tableSource, dsDest );
			}
			bool bOldEnforce = dsDest.EnforceConstraints;
			if ( tableSource.Rows.Count == 0 )
				return;
			DataColumn[] keys = tableSource.PrimaryKey;
			int nNbKeys = tableSource.PrimaryKey.Length;

			Hashtable tableSupprimees = null;
			//Si une seule clé, stocke la liste des supprimées dans une hashtable 
			//pour un recherche plus rapide
			if ( nNbKeys == 1 && bPreseveChanges )
			{
				tableSupprimees = new Hashtable();
				DataRow[] rows = tableDest.Select("","",DataViewRowState.Deleted);
				foreach ( DataRow row in rows )
				{
					tableSupprimees[row[keys[0].ColumnName, DataRowVersion.Original]] = true;
				}
			}


			dsDest.EnforceConstraints = false;
			DataColumn colIsToRead = tableDest.Columns[CContexteDonnee.c_colIsToRead];
			try
			{
				foreach ( DataRow row in tableSource.Rows )
				{
					try
					{
						DataRowVersion version = DataRowVersion.Default;
						if ( row.RowState == DataRowState.Deleted )
							version = DataRowVersion.Original;
						object[] cles = new object[nNbKeys];
						for ( int n = 0; n < nNbKeys; n++ )
							cles[n] = row[keys[n], version];

						//Tente d'abord le lien sur la clé primaire
						DataRow rowDest = tableDest.Rows.Find(cles);
						if ( rowDest!=null )
						{
							bool bShouldUpdate = (rowDest.RowState & (DataRowState.Modified | DataRowState.Added | DataRowState.Deleted)) == 0;
							if ( bPreseveChanges && colIsToRead != null)
								bShouldUpdate = (bool)rowDest[colIsToRead];
							if ( bShouldUpdate )
							{
								if ( row.RowState == DataRowState.Deleted && rowDest.RowState != DataRowState.Deleted )
									rowDest.Delete();
								else
								{
									if ( dsDest is CContexteDonnee )
										((CContexteDonnee)dsDest).CopyRow ( row, rowDest,DataRowVersion.Current, true );
									else
										CopyRow ( row, rowDest, DataRowVersion.Current, true );
								}
							}
						}
							//Si la ligne existe dans le contexte de destination mais
							//qu'elle a été supprimée, Find ne la trouve pas, il faut donc
							//Vérifier si elle n'est pas là autrement
						else 
						{
							bool bShouldImporte = true;
							if ( bPreseveChanges )//On n'importe pas les lignes supprimées
								//si on conserve les modifs faites dans la destination
							{
								if ( tableSupprimees != null )
								{
									if ( tableSupprimees.Contains( row[keys[0]] ) )
										bShouldImporte = false;
								}
								else
								{
									CFiltreData filtre = CFiltreData.CreateFiltreAndSurRow ( keys, row, DataRowVersion.Original );
									DataRow[] rows = tableDest.Select(new CFormatteurFiltreDataToStringDataTable().GetString(filtre),"", DataViewRowState.Deleted );
									if ( rows.Length == 1 && rows[0].RowState == DataRowState.Deleted )
										bShouldImporte = false;
								}
							}
							if( bShouldImporte && row.RowState != DataRowState.Deleted)
							{
								tableDest.ImportRow(row);
							}
						}
					}
					catch ( Exception e )
					{
						throw e;
					}
					finally
					{
					
					}
				}
			}
			finally
			{
				dsDest.EnforceConstraints = bOldEnforce;
			}
		}

		////////////////////////////////////////////////////////////////////////////
		public static DataTable AddTableCopie ( DataTable tableSource, DataSet dsDest )
		{
			DataTable table = new DataTable();
			table.CaseSensitive = tableSource.CaseSensitive;
			foreach ( DataColumn colSource in tableSource.Columns )
			{
				DataColumn col = new DataColumn();
				col.AllowDBNull = colSource.AllowDBNull;
				col.AutoIncrement = colSource.AutoIncrement;
				col.AutoIncrementSeed = colSource.AutoIncrementSeed;
				col.AutoIncrementStep = colSource.AutoIncrementStep;
				col.Caption = colSource.Caption;
				col.ColumnMapping = colSource.ColumnMapping;
				col.ColumnName = colSource.ColumnName;
				col.DataType = colSource.DataType;
				col.DefaultValue = colSource.DefaultValue;
				col.Expression = colSource.Expression;
				foreach ( object obj in colSource.ExtendedProperties.Keys )
					col.ExtendedProperties[obj] = colSource.ExtendedProperties[obj];
				col.MaxLength = colSource.MaxLength;
				col.Prefix = colSource.Prefix;
				col.ReadOnly = colSource.ReadOnly;
				col.Unique = colSource.Unique;
				table.Columns.Add ( col );
			}
			table.DisplayExpression = tableSource.DisplayExpression;
			foreach ( object obj in tableSource.ExtendedProperties.Keys )
				table.ExtendedProperties[obj] = tableSource.ExtendedProperties[obj];
			table.MinimumCapacity = tableSource.MinimumCapacity;
			table.Namespace = tableSource.Namespace;
			table.Prefix = tableSource.Prefix;
			ArrayList lstKeys = new ArrayList();
			foreach(  DataColumn col in tableSource.PrimaryKey )
				lstKeys.Add ( table.Columns[col.ColumnName] );
			table.PrimaryKey = (DataColumn[])lstKeys.ToArray(typeof(DataColumn));
			table.TableName = tableSource.TableName;
			dsDest.Tables.Add ( table );
			return table;
		}
		////////////////////////////////////////////////////////////////////////////
		public static void CopyRow ( DataRow rowSource, DataRow rowDest, DataRowVersion versionToCopy, bool bKeepUnchanged,params string[] strExclusions )
		{
			Hashtable tbl = new Hashtable();
			foreach ( string strExclusion in strExclusions )
				tbl.Add(strExclusion,"");
			bool bWasUnchanged = rowDest.RowState == DataRowState.Unchanged;
			bool bOldEnforceConstraints = rowDest.Table.DataSet.EnforceConstraints;
			try
			{
				foreach ( DataColumn col in rowSource.Table.Columns )
				{
					if ( tbl[col.ColumnName] == null && rowDest.Table.Columns[col.ColumnName] != null)
					{
						if ( col.DataType != typeof(CDonneeBinaireInRow) ||
							rowSource[col.ColumnName, versionToCopy] is CDonneeBinaireInRow )
							try
							{
								rowDest[col.ColumnName]=rowSource[col.ColumnName, versionToCopy];
							}
							catch
							{
								if ( rowDest.Table.DataSet.EnforceConstraints )
								{
									rowDest.Table.DataSet.EnforceConstraints = false;
									rowDest[col.ColumnName]=rowSource[col.ColumnName, versionToCopy];
								}
								else
									throw new Exception(I.T("Error|12"));
							}
					}
				}
				if ( rowDest.Table.DataSet.EnforceConstraints != bOldEnforceConstraints )
					rowDest.Table.DataSet.EnforceConstraints = bOldEnforceConstraints;
			}
			catch ( Exception e )
			{
				throw e;
			}
			finally
			{
				if ( rowDest.Table.DataSet.EnforceConstraints != bOldEnforceConstraints )
					rowDest.Table.DataSet.EnforceConstraints = bOldEnforceConstraints;
			}
			if ( bWasUnchanged && bKeepUnchanged )
				rowDest.AcceptChanges();
		}

		public static CResultAErreur ChangeTypeColonne(ref DataColumn colonne, Type newType)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				//Sauve les clés primaires
				List<string> colsClesPrimaire = new List<string>();
				DataTable table = colonne.Table;
				foreach (DataColumn col in table.PrimaryKey)
					colsClesPrimaire.Add(col.ColumnName);
				string strOldName = colonne.ColumnName;
				colonne.ColumnName = Guid.NewGuid().ToString();
				DataColumn newCol = new DataColumn(strOldName, newType);
				newCol.AllowDBNull = true;
				table.Columns.Add(newCol);
				table.PrimaryKey = new DataColumn[0];
				foreach (DataRow row in table.Rows)
				{
					row[newCol] = Convert.ChangeType(row[colonne], newType);
				}
				table.Columns.Remove(colonne);
				List<DataColumn> cols = new List<DataColumn>();
				foreach (string strCol in colsClesPrimaire)
					cols.Add(table.Columns[strCol]);
				table.PrimaryKey = cols.ToArray();
				colonne = newCol;
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
		}
	}

		
}
