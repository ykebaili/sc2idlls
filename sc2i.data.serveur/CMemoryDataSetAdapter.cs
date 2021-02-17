using System;
using System.Data;
using System.Data.Common;
using System.Collections;


using sc2i.common;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de C2iMemorySqlAdapter.
	/// </summary>
	public class CMemoryDataSetAdapter : IDataAdapter
	{
		private CMemoryDataSetConnexion m_connexion;
		private string m_strNomTable="";
		private string[] m_strChamps=null;
		private string m_strWhere="";
		private CFiltreData m_filtre;

		private MissingMappingAction m_missingMappinAction = MissingMappingAction.Passthrough;
		private MissingSchemaAction m_missingSchemaAction = MissingSchemaAction.Add;
		private DataTableMappingCollection m_tableMappingCollection = new DataTableMappingCollection();

		/// /////////////////////////////////////////////////////////////////////////////
		public CMemoryDataSetAdapter( CMemoryDataSetConnexion connexion, string strRequete, CFiltreData filtre )
		{
			m_connexion = connexion;
			CResultAErreur result = CExtracteurRequeteSqlSimple.ExtraitElements(strRequete, ref m_strNomTable, ref m_strChamps, ref m_strWhere);
			if ( !result )
				throw new CExceptionErreur(result.Erreur);
			if(  m_strChamps.Length == 1 )
			{
				string strChamp = m_strChamps[0];
				if ( strChamp.Length > 0 && strChamp[strChamp.Length-1] == '*' )
				{
					DataTable table = m_connexion.DataSet.Tables[m_strNomTable];
					int nChamp, nNbChamps = table.Columns.Count;
					m_strChamps = new string[nNbChamps];
					for ( nChamp = 0; nChamp < nNbChamps; nChamp++ )
						m_strChamps[nChamp] = table.Columns[nChamp].ColumnName;
				}
			}
			m_filtre = filtre;
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return m_missingMappinAction;
			}
			set
			{
				m_missingMappinAction = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return m_missingSchemaAction;
			}
			set
			{
				m_missingSchemaAction = value;
			}
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public ITableMappingCollection TableMappings
		{
			get
			{
				return m_tableMappingCollection;
			}
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public DataTable[] FillSchema( DataSet dsDest, SchemaType schemaType )
		{
			DataTable tableSource = m_connexion.DataSet.Tables[m_strNomTable];
			if ( tableSource == null )
				throw new Exception(I.T("Table '@1' not found|169",m_strNomTable));

			//Mapping de la table
			string strMapName=null;
			if ( schemaType == SchemaType.Mapped )
			{
				if ( TableMappings.Contains("Table") )
					strMapName = ((DataTableMapping)TableMappings["Table"]).DataSetTable;
				if ( strMapName == null )
					switch ( MissingMappingAction )
					{
						case MissingMappingAction.Error:
							throw new Exception(I.T("No mappage for table @1|170",m_strNomTable));
						case MissingMappingAction.Ignore:
							return new DataTable[0];
						default :
							strMapName = "Table";
							break;
					}
			}
			else
				strMapName = m_strNomTable;

			//Création de la table si nécéssaire
			DataTable tableDest = dsDest.Tables[strMapName];
			//Si la table n'existe pas, elle est crée
			if ( tableDest == null )
			{
				switch ( MissingSchemaAction )
				{
					case MissingSchemaAction.Error :
						throw new Exception(I.T("The table '@1' doesn't exist in the destination dataset|171",m_strNomTable));
					case MissingSchemaAction.Ignore :
						return new DataTable[0];
				}
				tableDest = dsDest.Tables.Add ( strMapName );
			}
			
			foreach ( string strChamp in m_strChamps )
			{
				DataColumn colSource = tableSource.Columns[strChamp];
				if ( colSource == null )
					throw new Exception(I.T("The column @1 not find in table @2|172",strChamp,m_strNomTable));
				DataColumn colDest = tableDest.Columns[strChamp];
				if ( colDest == null )
				{
					switch ( MissingSchemaAction )
					{
						case MissingSchemaAction.Error :
							throw new Exception(I.T("The table '@1' doesn't exist in the destination dataset|171",m_strNomTable));
						case MissingSchemaAction.Ignore :
							break;
						case MissingSchemaAction.Add :
						case MissingSchemaAction.AddWithKey :
							colDest = new DataColumn ( colSource.ColumnName, colSource.DataType, colSource.Expression );
							tableDest.Columns.Add ( colDest );
							break;
					}
				}
			}
			if ( tableDest.PrimaryKey.Length == 0 && tableSource.PrimaryKey.Length != 0 )
			{
				DataColumn[] prims = new DataColumn[tableSource.PrimaryKey.Length];
				for ( int nCol = 0; nCol < tableSource.PrimaryKey.Length; nCol++ )
					prims[nCol] = tableDest.Columns[tableSource.PrimaryKey[nCol].ColumnName];
				tableDest.PrimaryKey = prims;
			}
				
			return new DataTable[]{tableDest};
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public int Fill ( DataSet dsDest )
		{
			DataTable[] tablesDest = FillSchema(dsDest, SchemaType.Mapped);
			if (tablesDest.Length == 0 )
				return 0;
			DataTable tableDest = tablesDest[0];
			string strFiltre = m_strWhere;
			if ( m_filtre != null )
			{
				string strConvFiltre = new CFormatteurFiltreDataToStringDataTable().GetString(m_filtre);
				if ( strConvFiltre != "" )
				{
					if ( strFiltre != "" )
						strFiltre = "("+strFiltre+") and ";
					strFiltre += strConvFiltre;
				}
			}
			DataView view = new DataView ( m_connexion.DataSet.Tables[tableDest.TableName] );
			view.RowFilter = strFiltre;
			foreach (DataRowView row in view)
			{
				tableDest.ImportRow(row.Row);
				/*DataRow newRow = tableDest.NewRow();
				foreach ( string strChamp in m_strChamps )
				{
					if ( tableDest.Columns[strChamp] != null )
						newRow[strChamp] = row[strChamp];
				}
				tableDest.Rows.Add ( newRow );*/
			}
			return view.Count;
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public IDataParameter[] GetFillParameters()
		{
			return null;
		}

		/// /////////////////////////////////////////////////////////////////////////////
		public int Update ( DataSet dsSource )
		{
			DataTable tableSource = dsSource.Tables[m_strNomTable];
			if ( tableSource == null )
				return 0;
#if PDA
			CUtilDataSet.Merge ( tableSource, m_connexion.DataSet, false );
#else
			m_connexion.DataSet.Merge(tableSource);
#endif
			return 1;
		}

		

		
		
	}
}
