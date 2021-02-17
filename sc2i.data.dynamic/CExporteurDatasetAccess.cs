using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Collections;

using sc2i.common;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CExporteurDatasetAccess.
	/// </summary>
	public enum VersionAccessExport
	{
		Access2000=10,
		Access2003
	}
	public class CExporteurDatasetAccess : IExporteurDataset
	{
		private bool m_bOnlyStructure = false;
		private OleDbConnection m_connection = null;
		private OleDbTransaction m_transaction = null;

		private VersionAccessExport m_versionAccess = VersionAccessExport.Access2000;
		


		/// //////////////////////////////////////////////
		public CExporteurDatasetAccess()
		{
			
		}

		//------------------------------------------------------------------------
		public string ExtensionParDefaut
		{
			get
			{
				return "mdb";
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
		public VersionAccessExport VersionAccess
		{
			get
			{
				return m_versionAccess;
			}
			set
			{
				m_versionAccess = value;
			}
		}

		/// //////////////////////////////////////////////
		private string GetNomRessourceForVersion ( )
		{
			switch ( m_versionAccess )
			{
				case VersionAccessExport.Access2000 :
					return "sc2i.data.dynamic.BASEVIDE2000.MDB";
				case VersionAccessExport.Access2003 :
					return "sc2i.data.dynamic.BASEVIDE2003.MDB";
			}
			return "";
		}

		/// //////////////////////////////////////////////
		/// Calcule la taille des champs texte qui n'ont pas de taille
		private void CalculeLongueurMaxChampsTexte ( DataSet ds )
		{
			foreach ( DataTable table in ds.Tables )
			{
				Hashtable tableMaxLength = new Hashtable();
				foreach ( DataColumn col in table.Columns )
				{
					if ( col.DataType == typeof(string) &&
						col.MaxLength < 0 )
						tableMaxLength[col] = 1;
				}
				ArrayList lstColonnes = new ArrayList ( tableMaxLength.Keys );
				foreach ( DataRow row in table.Rows )
				{
					foreach ( DataColumn col in lstColonnes )
					{
						int nLength = (int)tableMaxLength[col];
						object val = row[col];
						if ( val != DBNull.Value && ((string)val).Length > nLength )
						{
							nLength = ((string)val).Length;
							tableMaxLength[col] = nLength;
						}
					}
				}
				foreach ( DictionaryEntry entry in tableMaxLength )
					((DataColumn)entry.Key).MaxLength = (int)entry.Value;
			}
		}


		/// //////////////////////////////////////////////
		public CResultAErreur Export(DataSet ds, IDestinationExport destination)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!(destination is CDestinationExportFile))
			{
				result.EmpileErreur(I.T("An Access export can be done only in a file|168"));
				return result;
			}
			string strNomFichier = ((CDestinationExportFile)destination).FileName;
			
			try
			{
				CalculeLongueurMaxChampsTexte(ds);
				ds.EnforceConstraints = false;
				System.Reflection.Assembly thisExe;
				thisExe = System.Reflection.Assembly.GetExecutingAssembly();
				Stream source = 
					thisExe.GetManifestResourceStream(GetNomRessourceForVersion());

				if ( File.Exists ( strNomFichier ) )
					File.Delete ( strNomFichier );
            
				Stream dest = new FileStream ( strNomFichier, FileMode.CreateNew );
				result = CStreamCopieur.CopyStream ( source, dest, 2048 );
				dest.Close();
				if ( !result )
					return result;
				


				m_connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source="+
					strNomFichier+
					";Mode=Share Deny None;Extended Properties=\"\";"+
					"Jet OLEDB:System database=\"\";Jet OLEDB:Registry Path=\"\";"+
					"Jet OLEDB:Engine Type=4;Jet OLEDB:Database Locking Mode=0;"+
					"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;"+
					"Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;"+
					"Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False" );
				m_connection.Open ();

				m_transaction = m_connection.BeginTransaction();


				//Nom de table->true : pour éviter 2 fois le même nom de table
				Hashtable tableTablesCrees = new Hashtable();
				foreach ( DataTable table in ds.Tables )
				{
					result = CreateTable ( table, tableTablesCrees );
					if ( !result )
						break;
				}

				if ( result )
				{
					if (!m_bOnlyStructure )
					{
						foreach ( DataTable table in ds.Tables )
						{
							result = CreateData ( table );
							if ( !result )
								break;
						}
					}

					if ( result )
					{
						m_transaction.Commit();
						m_transaction = m_connection.BeginTransaction();
						int nNumRelation = 0;
						foreach ( DataRelation relation in ds.Relations )
						{
							result = CreateForeignKeys( relation, nNumRelation++ );
							if ( !result )
								break;
						}
					}
				}

				if ( result )
					m_transaction.Commit();
				else
					m_transaction.Rollback();
				m_connection.Close();

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
		private string GetStringOleDbForColumn ( DataColumn col )
		{
			Type tp = col.DataType;
			if ( tp == typeof(String) )
			{
				if ( col.MaxLength <= 255 && col.MaxLength > 0  )
					return "NVARCHAR";
				else
					return "MEMO";
			}
			if ( tp == typeof(int))
				return "int";
			if ( tp == typeof(double))
				return "float";
			if ( tp == typeof(DateTime) || tp == typeof(CDateTimeEx))
				return "datetime";
			if ( tp == typeof(bool))
				return "bit";
            if ( tp == typeof(byte[]))
                return "image";
            if (tp == typeof(decimal))
                return "float";
			return null;
		}

		///////////////////////////////////////////////////////////////////////
		private string GetNomUnique ( string strNomObjet, Hashtable tableObjetsExistants )
		{
			string strBaseNomObjet = strNomObjet;
			string strNomFinal = strNomObjet;
			int nIndex = 1;
			while ( tableObjetsExistants[strNomFinal] != null )
			{
				strNomFinal = strBaseNomObjet+nIndex.ToString();
				if(  strNomFinal.Length > 64 )
				{
					strBaseNomObjet = strBaseNomObjet.Substring ( 0, 64-nIndex.ToString().Length );
					strNomFinal = strBaseNomObjet+nIndex.ToString();
				}
			}
			tableObjetsExistants[strNomFinal] = true;
			return strNomFinal;
		}


		///////////////////////////////////////////////////////////////////////
		private CResultAErreur CreateTable ( DataTable table, Hashtable tableTablesCrees )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				string strNomTable = GetNomUnique ( ConvertName ( table.TableName ), tableTablesCrees );
				table.TableName = strNomTable;
				string strRequete = "CREATE TABLE "+table.TableName+" ( ";
				Hashtable tableNomsColonnes = new Hashtable();
				foreach ( DataColumn col in table.Columns )
				{
					string strNomColonne = GetNomUnique ( ConvertName ( col.ColumnName ), tableNomsColonnes );
					col.ColumnName = strNomColonne;
					string strType = GetStringOleDbForColumn ( col );
					if ( strType != null )
					{
						strRequete += col.ColumnName+ " "+strType;
						if ( col.AllowDBNull == false )
							strRequete += " not null ";
						else strRequete += " null";
						strRequete += ",";
					}
				}
				strRequete = strRequete.Substring ( 0, strRequete.Length-1 );
				strRequete += ")";

				OleDbCommand command = new OleDbCommand ( strRequete, m_connection, m_transaction) ;
				command.ExecuteNonQuery();

				if ( table.PrimaryKey.Length > 0 )
				{
					strRequete = "ALTER TABLE "+table.TableName+" add primary key (";
					foreach ( DataColumn col in table.PrimaryKey )
						strRequete += col.ColumnName+",";
					strRequete = strRequete.Substring(0, strRequete.Length-1)+")";
					command = new OleDbCommand ( strRequete, m_connection, m_transaction) ;
					command.ExecuteNonQuery();
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException (e ) );
				result.EmpileErreur(I.T("Table creation error @1|169",table.TableName));
			}
			return result;
		}

		///////////////////////////////////////////////////////////////////////
		private CResultAErreur CreateData ( DataTable table )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				OleDbCommand command = new OleDbCommand ();
				command.Connection = m_connection;
				command.Transaction = m_transaction;
				string strValues = "";
				string strRequete = "insert into "+table.TableName+" (";
				
				int nParam = 1;
				foreach ( DataColumn col in table.Columns )
				{
					if ( GetStringOleDbForColumn(col )!=null )
					{
						strRequete += col.ColumnName+",";
						strValues+= "@"+nParam.ToString()+",";
						nParam++;
					}
				}
				strValues = strValues.Substring(0, strValues.Length-1);
				strRequete = strRequete.Substring(0, strRequete.Length-1);
				strRequete += ") values ("+strValues+")";
				command.CommandText = strRequete;
				nParam = 1;
				foreach ( DataRow row in table.Rows )
				{
					command.Parameters.Clear();
					foreach ( DataColumn col in table.Columns )
					{
						if ( GetStringOleDbForColumn(col) != null )
						{
							object val = row[col];
							if ( val is Boolean )
								val = ((bool)val)?1:0;
							if ( val is CDateTimeEx && val != null)
								val =((CDateTimeEx)val).DateTimeValue;
							OleDbParameter param = command.Parameters.Add ("@"+nParam.ToString(), COleDbTypeConvertor.GetTypeOleDbFromType(col.DataType) );
							param.Value = val;
							nParam++;
						}
					}
					command.ExecuteNonQuery();
				}
/*
					

				OleDbDataAdapter adapter = new OleDbDataAdapter ("select * from "+table.TableName, m_connection );
				OleDbCommandBuilder builder = new OleDbCommandBuilder ( adapter );
				adapter.InsertCommand = builder.GetInsertCommand();
				adapter.InsertCommand.Transaction = m_transaction;
				DataTable tableDest = table.Clone();
				foreach ( DataRow row in table.Rows )
				{
					DataRow newRow = tableDest.NewRow();
					newRow.ItemArray = row.ItemArray;
					tableDest.Rows.Add ( newRow );
				}
				adapter.Update ( tableDest );*/
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException (e ) );
				result.EmpileErreur(I.T("Table creation error @1|169", table.TableName));
			}
			return result;
		}

		///////////////////////////////////////////////////////////////////////
		private CResultAErreur CreateForeignKeys ( DataRelation relation, int nNumRelation )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				string strRequete = "ALTER TABLE "+relation.ChildTable.TableName+" add foreign key (";
				foreach ( DataColumn col in relation.ChildColumns )
					strRequete += col.ColumnName+",";
				strRequete = strRequete.Substring(0,strRequete.Length-1)+")";
				strRequete += " references "+relation.ParentTable.TableName+" (";
				foreach ( DataColumn col in relation.ParentColumns )
					strRequete += col.ColumnName+",";
				strRequete = strRequete.Substring(0,strRequete.Length-1)+")";
			
				OleDbCommand command = new OleDbCommand ( strRequete, m_connection, m_transaction) ;
				command.ExecuteNonQuery();

				//Crée les index
				strRequete = "CREATE INDEX FK_"+nNumRelation.ToString()+" on "
					+relation.ChildTable.TableName+" (";
				foreach ( DataColumn col in relation.ChildColumns )
					strRequete += col.ColumnName+",";
				strRequete = strRequete.Substring(0,strRequete.Length-1)+")";
				command = new OleDbCommand ( strRequete, m_connection, m_transaction) ;
				command.ExecuteNonQuery();

			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
			}
			return result;
		}

		private int GetNumVersion()
		{
			return 0;
		}

		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			serializer.TraiteBool ( ref m_bOnlyStructure );

			int nVersionAccess = (int)m_versionAccess;
			serializer.TraiteInt ( ref nVersionAccess );
			m_versionAccess = (VersionAccessExport)nVersionAccess;
			return result;
		}
				




	}
}
