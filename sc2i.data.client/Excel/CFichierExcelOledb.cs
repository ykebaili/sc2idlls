using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;


namespace sc2i.data.Excel
{
    /// <summary>
    /// représente un fichier Excel, et fournit des fonctions de lecture et d'écriture dans le fichier Excel
    /// avec la technologie OleDB
    /// </summary>
    public class CFichierExcelOleDb : IDisposable
    {
		#region Variables
		private int[] m_PKCol;
		private string m_strExcelFilename;
		private bool m_blnMixedData = true;
		private bool m_blnHeaders = false;		
		private string m_strSheetName;
		private string m_strSheetRange;
		private bool m_blnKeepConnectionOpen=false;
		private OleDbConnection m_oleConnection; 
		private OleDbCommand m_oleCmdSelect;
		private OleDbCommand m_oleCmdUpdate;
		#endregion



        public CFichierExcelOleDb()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int[] PKCols
		{
			get {return m_PKCol;}
			set {m_PKCol=value;}
		}

		public string ColName(int intCol)
		{
			string sColName="";
			if (intCol<26)
				sColName= Convert.ToString(Convert.ToChar((Convert.ToByte((char) 'A')+intCol)) );
			else
			{
				int intFirst = ((int) intCol / 26);
				int intSecond = ((int) intCol % 26);
				sColName= Convert.ToString(Convert.ToByte((char) 'A')+intFirst);
				sColName += Convert.ToString(Convert.ToByte((char) 'A')+intSecond);
			}
			return sColName;
		}

		public int ColNumber(string strCol)
		{
			strCol = strCol.ToUpper(); 
			int intColNumber=0;
			if (strCol.Length>1) 
			{
				intColNumber = Convert.ToInt16(Convert.ToByte(strCol[1])-65);  
				intColNumber += Convert.ToInt16(Convert.ToByte(strCol[1])-64)*26; 
			}
			else
				intColNumber = Convert.ToInt16(Convert.ToByte(strCol[0])-65);  
			return intColNumber;
		}
	

        /// <summary>
        /// Retourne la liste des noms des Feuilles de calcul du fichier Excel
        /// </summary>
        /// <returns></returns>
		public String[] GetExcelSheetNames()
		{
			
			System.Data.DataTable dt = null;

			try
			{
				if (m_oleConnection ==null)
                    OpenConnection();
				
				// Get the data table containing the schema
				dt = m_oleConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
 
				if(dt == null)
				{
					return null;
				}

				String[] excelSheets = new String[dt.Rows.Count];
				int i = 0;

				// Add the sheet name to the string array.
				foreach(DataRow row in dt.Rows)
				{
					string strSheetTableName = row["TABLE_NAME"].ToString();
                    strSheetTableName = strSheetTableName.Trim('\'');
					excelSheets[i] = strSheetTableName.Substring(0,strSheetTableName.Length-1); 
					i++;
				}
				

				return excelSheets;
			}
			catch(Exception)
			{
				return null;
			}
			finally
			{
				// Clean up.
				if(this.KeepConnectionOpen==false)
				{
					this.Close();
				}
				if(dt != null)
				{
					dt.Dispose();
					dt=null;
				}
			}
		}
															
		public string ExcelFilename
		{
            get { return m_strExcelFilename; }
            set { m_strExcelFilename = value; }
		}

		public string SheetName
		{
			get { return m_strSheetName;}
			set { m_strSheetName=value;}
		}

		public string SheetRange
		{
			get 
            {
                return m_strSheetRange;
            }
			set 
			{
				if (value.IndexOf(":")==-1) 
                    throw new Exception("Invalid range length"); 
				m_strSheetRange=value;
            }
		}
		
		public bool KeepConnectionOpen
		{
			get { return m_blnKeepConnectionOpen;}
			set {m_blnKeepConnectionOpen=value;}
		}

		public bool Headers
		{
			get { return m_blnHeaders;}
			set { m_blnHeaders=value;}
		}

		public bool MixedData
		{
			get {return m_blnMixedData;}
			set {m_blnMixedData=value;}
		}

        

		private string ExcelConnectionOptions()
		{
			string strOptions="";
			
            if (this.MixedData == true)
				strOptions += "IMEX=2;";
			if (this.Headers == true)
				strOptions += "HDR=Yes;";
			else	
				strOptions += "HDR=No;";

			return strOptions;
		}

		
		
		private string ExcelConnectionString()
		{
            //return
            //    @"Provider=Microsoft.Jet.OLEDB.4.0;" + 
            //    @"Data Source=" + m_strExcelFilename  + ";" + 
            //    @"Extended Properties=" + Convert.ToChar(34).ToString() + 
            //    @"Excel 8.0;"+ ExcelConnectionOptions() + Convert.ToChar(34).ToString(); 

            /* Avec les versions pre-2007 des fichiers Excel, nous avions l'habitude de pouvoir y accéder en tant que base de données fichier, via OLEDB en utilisant le moteur Jet, par une chaîne de connexion de ce type :
            Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties="Excel 8.0;HDR=YES;IMEX=1";
            Mais Jet ne supporte pas l'ouverture des fichiers Excel 2007 (xlsb, xlsm et xlsx), ce qui vous sera signalé par l'erreur :
            External table is not in the expected format.
            Ou en français : La table externe n'est pas dans le format attendu.
            Pour celà il faut passer par le nouveau "Microsoft Office 2007 Access Database Engine" : */

            return
                @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data Source=" + m_strExcelFilename + ";" +
                @"Extended Properties=" + Convert.ToChar(34).ToString() +
                @"Excel 12.0;" + ExcelConnectionOptions() + Convert.ToChar(34).ToString(); 

		}


		public void OpenConnection()
		{
			try
			{
				if (m_oleConnection !=null)
				{
					if (m_oleConnection.State==ConnectionState.Open)
					{
						m_oleConnection.Close();
					}
					m_oleConnection=null;
				}

				if (System.IO.File.Exists(m_strExcelFilename)==false)
				{
					throw new Exception("Excel file " + m_strExcelFilename +  "could not be found.");
				}
				m_oleConnection = new OleDbConnection(ExcelConnectionString());  
				m_oleConnection.Open();   				
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Close()
		{
			if (m_oleConnection !=null)
			{
				if (m_oleConnection.State != ConnectionState.Closed) 
					m_oleConnection.Close(); 
				m_oleConnection.Dispose();
				m_oleConnection=null;
			}
		}

		private bool SetSheetQuerySelect()
		{
			try
			{
				if (m_oleConnection == null)
				{
					throw new Exception("Connection is unassigned or closed."); 
				}

				if (m_strSheetName == null || m_strSheetName.Length ==0)
					throw new Exception("Sheetname was not assigned."); 

				m_oleCmdSelect =new OleDbCommand(
					@"SELECT * FROM [" 
					+ m_strSheetName 
					+ "$" + m_strSheetRange
					+ "]", m_oleConnection);   
			
				return true;
			}			
			catch (Exception ex)
			{
				throw ex;
			}
			

		}

        private string AddWithComma(string strSource,string strAdd)
		{
			if (strSource !="") strSource = strSource += ", ";
			return strSource + strAdd;
		}

		private string AddWithAnd(string strSource,string strAdd)
		{
			if (strSource !="") strSource = strSource += " and ";
			return strSource + strAdd;
		}
		

		private OleDbDataAdapter SetSheetQueryAdapter(DataTable dt)
		{
			// Deleting in Excel workbook is not possible
			//So this command is not defined
			try
			{
				if (m_oleConnection == null)
				{
					throw new Exception("Connection is unassigned or closed."); 
				}


				if (m_strSheetName.Length ==0)
					throw new Exception("Sheetname was not assigned."); 
				
				if (PKCols == null)
					throw new Exception("Cannot update excel sheet with no primarykey set."); 
				if (PKCols.Length<1) 
					throw new Exception("Cannot update excel sheet with no primarykey set."); 
				    
				OleDbDataAdapter oleda = new OleDbDataAdapter(m_oleCmdSelect); 				
				string strUpdate="";
				string strInsertPar="";
				string strInsert="";
				string strWhere="";
				
				
				for (int iPK=0;iPK<PKCols.Length;iPK++)
				{
					strWhere = AddWithAnd(strWhere,dt.Columns[iPK].ColumnName +  "=?"); 
				}
				strWhere =" Where "+strWhere;

				for (int iCol=0;iCol<dt.Columns.Count;iCol++)
				{
					strInsert= AddWithComma(strInsert,dt.Columns[iCol].ColumnName);
					strInsertPar= AddWithComma(strInsertPar,"?");
					strUpdate= AddWithComma(strUpdate,dt.Columns[iCol].ColumnName)+"=?";
				}

				string strTable = "["+ this.SheetName + "$" + this.SheetRange + "]";  
				strInsert = "INSERT INTO "+ strTable + "(" + strInsert +") Values (" + strInsertPar + ")";
				strUpdate = "Update " + strTable + " Set " + strUpdate + strWhere;
				
				
				oleda.InsertCommand = new OleDbCommand(strInsert,m_oleConnection);
				oleda.UpdateCommand = new OleDbCommand(strUpdate,m_oleConnection); 
				OleDbParameter oleParIns = null;
				OleDbParameter oleParUpd = null;
				for (int iCol=0;iCol<dt.Columns.Count;iCol++)
				{
					oleParIns = new OleDbParameter("?",dt.Columns[iCol].DataType.ToString());
					oleParUpd = new OleDbParameter("?",dt.Columns[iCol].DataType.ToString());
					oleParIns.SourceColumn =dt.Columns[iCol].ColumnName;
					oleParUpd.SourceColumn =dt.Columns[iCol].ColumnName;
					oleda.InsertCommand.Parameters.Add(oleParIns);
					oleda.UpdateCommand.Parameters.Add(oleParUpd);
					oleParIns=null;
					oleParUpd=null;
				}

				for (int iPK=0;iPK<PKCols.Length;iPK++)
				{
					oleParUpd = new OleDbParameter("?",dt.Columns[iPK].DataType.ToString());
					oleParUpd.SourceColumn =dt.Columns[iPK].ColumnName;
					oleParUpd.SourceVersion = DataRowVersion.Original;
					oleda.UpdateCommand.Parameters.Add(oleParUpd);
				}
				return oleda;
			}			
			catch (Exception ex)
			{
				throw ex;
			}
			
		}

		
		private bool SetSheetQuerySingelValUpdate(string strVal)
		{
			try
			{
				if (m_oleConnection == null)
				{
					throw new Exception("Connection is unassigned or closed."); 
				}

				if (m_strSheetName.Length ==0)
					throw new Exception("Sheetname was not assigned."); 

				m_oleCmdUpdate =new OleDbCommand(
					@" Update [" 
					+ m_strSheetName 
					+ "$" + m_strSheetRange
					+ "] set F1=" + strVal, m_oleConnection);   
				return true;
			}			
			catch (Exception ex)
			{
				throw ex;
			}
			

		}

		

		public void SetPrimaryKey(int intCol)
		{
			m_PKCol = new int[1] { intCol };			
		}


		private void SetPrimaryKey(DataTable dt)
		{
			try
			{
				if (PKCols != null)
				{
					//set the primary key
					if (PKCols.Length>0)
					{
						DataColumn[] dc;
						dc = new DataColumn[PKCols.Length];
                        for (int i = 0; i < PKCols.Length; i++)
                        {
                            dc[i] = dt.Columns[PKCols[i]];
                        }
				
						
						dt.PrimaryKey = dc;

					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	
		public DataTable GetTable()
		{
			return GetTable("ExcelTable");
		}

	    public DataTable GetTable(string strTableName)
		{
			try
			{
				//Open and query
				if (m_oleConnection ==null) 
                    OpenConnection();
				if (m_oleConnection.State != ConnectionState.Open)
					throw new Exception("Connection cannot open error."); 
				if (SetSheetQuerySelect()==false) 
                    return null;

				//Fill table
				OleDbDataAdapter oleAdapter = new OleDbDataAdapter();   
				oleAdapter.SelectCommand = m_oleCmdSelect;   
				DataTable dt = new DataTable(strTableName);
				oleAdapter.FillSchema(dt, SchemaType.Source);  
				oleAdapter.Fill(dt);
				if (this.Headers == false)
				{
					if (m_strSheetRange.IndexOf(":")>0)
					{
						string FirstCol = m_strSheetRange.Substring(0,m_strSheetRange.IndexOf(":")-1); 
						int intCol = this.ColNumber(FirstCol);
						for (int intI=0;intI<dt.Columns.Count;intI++)
						{
							dt.Columns[intI].Caption =ColName(intCol+intI);
						}
					}
				}
				SetPrimaryKey(dt);
				//Cannot delete rows in Excel workbook
				dt.DefaultView.AllowDelete =false;
			
				//Clean up
				m_oleCmdSelect.Dispose();
				m_oleCmdSelect=null;
				oleAdapter.Dispose();
				oleAdapter=null;
				if (KeepConnectionOpen==false)
                    Close();
				return dt;			

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		private void CheckPKExists(DataTable dt)
		{
			if (dt.PrimaryKey.Length==0) 
				if (this.PKCols !=null)
				{
					SetPrimaryKey(dt);
				}
				else
					throw new Exception("Provide an primary key to the datatable"); 
		}
		public DataTable SetTable(DataTable dt)
		{
			try
			{
				DataTable dtChanges = dt.GetChanges();
				if (dtChanges == null) throw new Exception("There are no changes to be saved!"); 
				CheckPKExists(dt);
				//Open and query
				if (m_oleConnection ==null) OpenConnection();
				if (m_oleConnection.State != ConnectionState.Open)
					throw new Exception("Connection cannot open error."); 
				if (SetSheetQuerySelect()==false) return null;

				//Fill table
				OleDbDataAdapter oleAdapter = SetSheetQueryAdapter(dtChanges);		
				
				oleAdapter.Update(dtChanges); 
				//Clean up
				m_oleCmdSelect.Dispose();
				m_oleCmdSelect=null;
				oleAdapter.Dispose();
				oleAdapter=null;
				if (KeepConnectionOpen==false) Close();
				return dt;			
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		

		public void SetSingleCellRange(string strCell)
		{
			m_strSheetRange = strCell + ":" + strCell;
		}

        public object GetValue(string strCol, string strLine)
        {
            return GetValue(strCol + strLine); // Pour faire B4 par exemple
        }

		public object GetValue(string strCell)
		{
			SetSingleCellRange(strCell);
			object objValue=null;
			//Open and query
			if (m_oleConnection ==null) 
                OpenConnection();
			if (m_oleConnection.State != ConnectionState.Open)
				throw new Exception("Connection is not open error."); 

			if (SetSheetQuerySelect()==false)
                return null;

			objValue = m_oleCmdSelect.ExecuteScalar();

			m_oleCmdSelect.Dispose();
			m_oleCmdSelect=null;	

			if (KeepConnectionOpen==false)
                Close();
			
            return objValue;
		}


        public void SetValue(string strCell, object objValue)
        {

            try
            {

                SetSingleCellRange(strCell);
                //Open and query
                if (m_oleConnection == null) OpenConnection();
                if (m_oleConnection.State != ConnectionState.Open)
                    throw new Exception("Connection is not open error.");

                if (SetSheetQuerySingelValUpdate(objValue.ToString()) == false) return;
                objValue = m_oleCmdUpdate.ExecuteNonQuery();

                m_oleCmdUpdate.Dispose();
                m_oleCmdUpdate = null;
                if (KeepConnectionOpen == false) Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (m_oleCmdUpdate != null)
                {
                    m_oleCmdUpdate.Dispose();
                    m_oleCmdUpdate = null;
                }
            }

        }






		#region Dispose
		public void Dispose()
		{
			if (m_oleConnection !=null)
			{
				m_oleConnection.Dispose();
				m_oleConnection=null;
			}
			if (m_oleCmdSelect!=null)
			{
				m_oleCmdSelect.Dispose(); 
					m_oleCmdSelect=null;
			}
			// Dispose of remaining objects.
		}
        #endregion
	

    }
}
