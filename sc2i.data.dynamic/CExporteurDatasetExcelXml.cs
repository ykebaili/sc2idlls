using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

using CarlosAg.ExcelXmlWriter;
using sc2i.common;

namespace sc2i.data.dynamic
{
	
	public class CExporteurDatasetExcelXml : IExporteurDataset
	{
		private bool m_bOnlyStructure = false;

		private bool m_bMasquerIdsAuto = false;

		/// //////////////////////////////////////////////
		public CExporteurDatasetExcelXml()
		{
			
		}

		//------------------------------------------------------------------------
		public string ExtensionParDefaut
		{
			get
			{
				return "xls";
			}
		}

		/// //////////////////////////////////////////////
		public bool ExporteStructureOnly
		{
			get
			{
				return m_bOnlyStructure;
			}
			set
			{
				m_bOnlyStructure = value;
			}
		}


		/// //////////////////////////////////////////////
		public bool MasquerIdsAuto
		{
			get
			{
				return m_bMasquerIdsAuto;
			}
			set 
			{
				m_bMasquerIdsAuto = value;
			}
		}

		/// //////////////////////////////////////////////
		private DataType GetExcelType ( Type tp )
		{
			if ( tp == typeof(int) ||
				tp == typeof(Int32) ||
				tp == typeof(Int64) ||
				tp == typeof(Single) ||
				tp == typeof(Double) || 
				tp == typeof(double) )
					return DataType.Number;
			else if ( tp == typeof(DateTime) || tp == typeof(CDateTimeEx) )
					return DataType.DateTime;
			else if ( tp == typeof(Boolean) || tp == typeof(bool) )
					return DataType.Boolean;
			else					return DataType.String;
		}
		

		
		/// //////////////////////////////////////////////
		public CResultAErreur Export(DataSet ds, IDestinationExport destination)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!(destination is CDestinationExportFile))
			{
				result.EmpileErreur(I.T("An Excel export can be done only in a file|171"));
				return result;
			}
			string strNomFichier = ((CDestinationExportFile)destination).FileName;
			
			try
			{
				ds.EnforceConstraints = false;

				if ( File.Exists ( strNomFichier ) )
					File.Delete ( strNomFichier );
            
				
				Workbook book = new Workbook();

				WorksheetStyle style = book.Styles.Add ( "SC2I_DATE");
				style.NumberFormat = I.T("Short DATE|170");

				//Nom de table->true : pour éviter 2 fois le même nom de table
				Hashtable tableTablesCrees = new Hashtable();
				Hashtable tableColumnsToHide = new Hashtable();
				Regex colKeyAuto = new Regex("^Key_");
				if ( m_bMasquerIdsAuto )
				{
					foreach ( DataTable table in ds.Tables )
					{
						foreach ( DataColumn col in table.PrimaryKey )
						{
							if ( col.AutoIncrement || colKeyAuto.IsMatch(col.ColumnName))
								tableColumnsToHide[col] = true;
						}
					}
					foreach ( DataRelation rel in ds.Relations )
					{
						if ( rel.ParentColumns.Length == 1 && 
							tableColumnsToHide.Contains(rel.ParentColumns[0]) &&
							rel.ChildColumns.Length == 1 )
							tableColumnsToHide[rel.ChildColumns[0]] = true;
					}
				}
				foreach ( DataTable table in ds.Tables )
				{
					Worksheet sheet = book.Worksheets.Add(table.TableName);
					
					//Création des colonnes
					WorksheetRow row = sheet.Table.Rows.Add ( );

					DataType[] typeCol = new DataType[table.Columns.Count];
					
					int nCol = 0;
					foreach ( DataColumn col in table.Columns )
					{
						if ( !tableColumnsToHide.Contains(col) )
						{
							WorksheetColumn colSheet = sheet.Table.Columns.Add ( );
							DataType tp = GetExcelType ( col.DataType );
							typeCol[nCol] = tp;
							nCol++;
							row.Cells.Add ( ConvertName(col.ColumnName) );
							if ( tp == DataType.DateTime )
							{
								colSheet.StyleID = "SC2I_DATE";
							}
						}
					}

					if ( !m_bOnlyStructure )
					{
						//Insertion des données
						foreach ( DataRow rowNet in table.Rows )
						{
							row = sheet.Table.Rows.Add();
							nCol = 0;
							foreach ( DataColumn col in table.Columns )
							{
								if ( !tableColumnsToHide.Contains ( col ) )
								{
									object val = rowNet[col];
									if ( val == DBNull.Value )
										row.Cells.Add();
									else
									{
										string strVal = "";
										if ( typeCol[nCol] == DataType.DateTime )
										{
											if ( val is CDateTimeEx )
												val = ((CDateTimeEx)val).DateTimeValue;
											strVal = ((DateTime)val).ToString("yyyy-MM-ddTHH:mm:ss");
										}
										else
											strVal = val.ToString();
										row.Cells.Add ( strVal , typeCol[nCol], null );
									}
									nCol++;
								}
							}
						}
					}
				}
				book.Save ( strNomFichier );

			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				return result;
			}
			finally
			{
				ds.EnforceConstraints = true;
			}
			return result;

		}

		///////////////////////////////////////////////////////////////////////
		private string ConvertName ( string strNom )
		{
			strNom = strNom.Substring(0, Math.Min ( strNom.Length, 63 ));
			strNom = strNom.Replace(" ","_");
			strNom = strNom.Replace("\"","");
			strNom = strNom.Replace("'","");
			strNom = strNom.Replace("-","");
			strNom = strNom.Replace(".","");
			strNom = strNom.Replace("/","");
			string strNewNom = "";
			foreach (char car in strNom )
				if ( car >= '0' && car <='z' )
					strNewNom += car;
			return strNom;
		}

		///////////////////////////////////////////////////////////////////////
		private int GetNumVersion()
		{
            //return 0;
            return 1; // Ajout m_bMasquerIdsAuto
		}

		///////////////////////////////////////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteBool ( ref m_bOnlyStructure );
            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bMasquerIdsAuto);

			return result;
		}


	}
}
