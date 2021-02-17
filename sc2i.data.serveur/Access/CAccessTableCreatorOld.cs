using System;
using System.Data;
using System.IO;
using System.Collections;
using sc2i.common;
using sc2i.multitiers.server;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CAccessTableCreator.
	/// </summary>
	public class CAccessTableCreatorOld : C2iObjetServeur
	{
		//------------------------------------------------------------------------------------------------
		public CAccessTableCreatorOld(IDatabaseConnexion connexion)
			:base(connexion.IdSession)
		{
		}

		//------------------------------------------------------------------------------------------------
		public static CResultAErreur CreateDatabase ( IDatabaseConnexion connexion )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( !connexion.IsConnexionValide() )
			{
				if ( connexion is COleDbDatabaseConnexion)
				{
					string strConnexionString = connexion.ConnexionString;
					//Trouve le nom de la base
					Regex findName = new Regex("Data Source[ ]*=[ ]*(.*);?", RegexOptions.IgnoreCase);
					Match match = findName.Match ( strConnexionString );
					string strBase = "";
					if ( match != null )
					{
						strBase = match.Value;
						string[] strVals = strBase.Split('=');
						if(  strVals.Length > 1 )
							strBase = strVals[1].Split(';') [0];
					}
					if ( !File.Exists ( strBase ) )
					{
						System.Reflection.Assembly thisExe;
						thisExe = System.Reflection.Assembly.GetExecutingAssembly();
						Stream source = 
							thisExe.GetManifestResourceStream("sc2i.data.serveur.BASEVIDE.MDB");
						if ( strBase == "" )
						{
							result.EmpileErreur(I.T("Impossible to determine the database name|123"));
							return result;
						}
						Stream dest = new FileStream ( strBase, FileMode.CreateNew );
						result = CStreamCopieur.CopyStream ( source, dest, 2048 );
						dest.Close();
						if ( !result )
							return result;
					}
				}
				else
					result.EmpileErreur(I.T("'CreateDatabase' function not possible for connection @1|124",connexion.GetType().Name));
			}
			return result;
		}


		//------------------------------------------------------------------------------------------------
		public static CResultAErreur CreationOuUpdateTableFromType(Type tp, IDatabaseConnexion connexion)
		{
			return CAccessTableCreatorOld.CreationOuUpdateTableFromType(tp, connexion, new ArrayList() );
		}
		//------------------------------------------------------------------------------------------------
		public static CResultAErreur CreationOuUpdateTableFromType(Type tp, IDatabaseConnexion connexion, ArrayList strChampsAutorisesANull)
		{
			CResultAErreur result = CResultAErreur.True;

			CStructureTable structure = CStructureTable.GetStructure(tp);

			if (!result)
				return result;

			//S'assure que toutes les tables parentes existent
			foreach ( CInfoRelation relation in structure.RelationsParentes )
			{
				if ( relation.TableParente != structure.NomTable )
				{
					string strNomTableParenteInDb = CContexteDonnee.GetNomTableInDbForNomTable(relation.TableParente);
					if (!CAccessTableCreatorOld.TableExists(strNomTableParenteInDb, connexion))
					{
						result = CreationOuUpdateTableFromType ( CContexteDonnee.GetTypeForTable ( relation.TableParente ), connexion );
						if ( !result )
							return result;
					}
				}
			}

			if ( CAccessTableCreatorOld.TableExists(structure.NomTableInDb, connexion) )
				result = CAccessTableCreatorOld.UpdateTable(structure, connexion, strChampsAutorisesANull);
			else
				result = CAccessTableCreatorOld.CreateTable(structure, connexion);
			
			return result;
		}
		//------------------------------------------------------------------------------------------------
		public static CResultAErreur CreateTable(CStructureTable structure, IDatabaseConnexion connexion)
		{
			CResultAErreur result = CResultAErreur.True;
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			#region Creation de la table

			string strRequeteCreation = "";

			strRequeteCreation += "CREATE TABLE " + structure.NomTableInDb + " (";

			if ( structure.HasChampIdAuto )
			{
				strRequeteCreation += structure.ChampsId[0].NomChamp + " "; 
				strRequeteCreation += (new CSQLServeurTypeMappeur()).GetStringDBTypeFromType(structure.ChampsId[0].TypeDonnee) + " ";
				strRequeteCreation += "NOT NULL IDENTITY (1, 1)";
				strRequeteCreation += ",";
			}

			ArrayList lstIndex = new ArrayList();

			foreach(CInfoChampTable info in structure.Champs)
			{
				if  (!info.IsId || !structure.HasChampIdAuto)
				{
					strRequeteCreation += info.NomChamp + " ";
					string strType = (new CSQLServeurTypeMappeur()).GetStringDBTypeFromType(info.TypeDonnee);
					if (info.TypeDonnee == typeof(string))
					{
						if ( info.IsLongString )
							strType = "NText";
						else
							strType += "(" +
								((info.Longueur>0) ? info.Longueur.ToString() : "1024") + 
								")";
					}
					strRequeteCreation += strType;
					strRequeteCreation += " ";
					if (!info.NullAuthorized)
						strRequeteCreation += "NOT NULL";
					strRequeteCreation += ",";
					if ( info.IsIndex )
						lstIndex.Add ( info.NomChamp );
				}
			}
			
			strRequeteCreation = strRequeteCreation.Substring(0,strRequeteCreation.Length-1);
			strRequeteCreation += ")";

			result = connexion.RunStatement(strRequeteCreation);

			if (!result)
			{
				result.EmpileErreur(I.T("Error while creating table @1|125", structure.NomTableInDb));
				return result;
			}

			#endregion

			#region création des indexs
			foreach ( string strChamp in lstIndex )
			{
				string strRequeteIndex = GetRequeteIndex ( structure.NomTableInDb, strChamp );
				result = connexion.RunStatement ( strRequeteIndex );
			}
			#endregion

			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			#region Creation de la contrainte de clé primaire

			string strRequetePrimaryKey = "";
			strRequetePrimaryKey += "ALTER TABLE " + structure.NomTableInDb + " ";
			strRequetePrimaryKey += "ADD CONSTRAINT PK_" + structure.NomTableInDb + " ";
			strRequetePrimaryKey += "PRIMARY KEY (";
			foreach ( CInfoChampTable info in structure.ChampsId )
				strRequetePrimaryKey += info.NomChamp+",";
			strRequetePrimaryKey = strRequetePrimaryKey.Substring(0, strRequetePrimaryKey.Length-1);
			strRequetePrimaryKey += ")";

			result = connexion.RunStatement(strRequetePrimaryKey);

			if (!result)
			{
				result.EmpileErreur(I.T("Error while creating primary key of table @1|126", structure.NomTableInDb));
				return result;
			}

			#endregion
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			#region Creation des contrainte de clés étrangères

			foreach(CInfoRelation rel in structure.RelationsParentes)
			{
				string strRequeteRelation = GetRequeteRelation(rel);
				result = connexion.RunStatement(strRequeteRelation);
				if (!result)
				{
					result.EmpileErreur(I.T("Error while creating foreign key of @1 (@2)|127", structure.NomTableInDb, strRequeteRelation));
					return result;
				}
				if ( result && rel.Indexed )
				{
					string strNomTableFilleInDb = CContexteDonnee.GetNomTableInDbForNomTable(rel.TableFille);
					string strRequeteIndex = GetRequeteIndex ( strNomTableFilleInDb, rel.ChampsFille );
					result = connexion.RunStatement ( strRequeteIndex );
					if ( !result )
					{
						result.EmpileErreur(I.T("Error while creating index in table @1|128",structure.NomTableInDb));
						return result;
					}
				}
			}

			#endregion
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			return result;
		}

		//------------------------------------------------------------------------------------------------
		protected static string GetRequeteRelation ( CInfoRelation rel )
		{
			string strNomTableFille = CContexteDonnee.GetNomTableInDbForNomTable(rel.TableFille);
			string strNomTableParente = CContexteDonnee.GetNomTableInDbForNomTable(rel.TableParente);
			string strRequeteRelation = "";
			strRequeteRelation += "ALTER TABLE " + strNomTableFille + " ";
			strRequeteRelation += "ADD CONSTRAINT " + rel.RelationKey;

			strRequeteRelation += " FOREIGN KEY (" ;
			foreach ( string strChamp in rel.ChampsFille )
				strRequeteRelation += strChamp+",";
			strRequeteRelation = strRequeteRelation.Substring(0, strRequeteRelation.Length-1);
			strRequeteRelation +=") ";
			strRequeteRelation += "REFERENCES " + strNomTableParente + "(" ;
			foreach ( string strChamp in rel.ChampsParent )
				strRequeteRelation += strChamp+",";
			strRequeteRelation = strRequeteRelation.Substring(0, strRequeteRelation.Length-1);
			strRequeteRelation += ")";
			return strRequeteRelation;
		}

		//------------------------------------------------------------------------------------------------
		protected static string GetNomIndex ( string strNomTable, params string[] strFields )
		{
			string strNomIndex = "IX_"+strNomTable;
			foreach ( string  strField in strFields )
				strNomIndex += "_"+strField;
			return strNomIndex;
		}

		//------------------------------------------------------------------------------------------------
		protected static string GetRequeteIndex ( string strNomTable, params string[] strFields )
		{
			string strNomIndex = GetNomIndex ( strNomTable, strFields );
			string strRequeteIndex = "CREATE INDEX "+ strNomIndex+" on "+
				strNomTable+" (";
			foreach ( string strField in strFields )
				strRequeteIndex += strField+",";
			strRequeteIndex = strRequeteIndex.Substring(0, strRequeteIndex.Length-1);
			strRequeteIndex += ")";
			return strRequeteIndex;
		}

		//------------------------------------------------------------------------------------------------
		public  static CResultAErreur SuppressionTable(string strNomTable, IDatabaseConnexion connexion)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( CAccessTableCreatorOld.TableExists(strNomTable, connexion) )
				result = CAccessTableCreatorOld.DeleteTable(strNomTable, connexion);
			return result;
		}
		//------------------------------------------------------------------------------------------------
		public static CResultAErreur DeleteTable(string strNomTable, IDatabaseConnexion connexion)
		{
			string strRequeteSuppressionTable = "DROP TABLE " + strNomTable;
			CResultAErreur result = connexion.RunStatement(strRequeteSuppressionTable);
			if (!result)
				result.EmpileErreur (I.T("Error while deleting of table @1|129", strNomTable));
			return result;
		}
		//------------------------------------------------------------------------------------------------
		public static CResultAErreur UpdateTable(CStructureTable structure, IDatabaseConnexion connexion)
		{
			return CAccessTableCreatorOld.UpdateTable(structure, connexion, new ArrayList() );
		}

		private static bool ExistIndex ( string strNomTable, IDatabaseConnexion connexion, params string[] strChamps )
		{
			//Vérifie l'existance de l'index
			OleDbConnection oldDbCon = (OleDbConnection)((C2iDbDatabaseConnexion)connexion).GetConnexion();
			string strNomIndex = GetNomIndex ( strNomTable, strChamps );
			DataTable table = oldDbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, 
				new object[]{null,null, strNomIndex,null,strNomTable} );
			return table.Rows.Count != 0;
		}

		//------------------------------------------------------------------------------------------------
		private static CResultAErreur CreateOrUpdateIndex ( string strNomTable, IDatabaseConnexion connexion, params string[] strChamps  )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( ExistIndex ( strNomTable,connexion,  strChamps ) )
				return result;
			string strNomIndex = GetNomIndex ( strNomTable, strChamps );
			string strRequeteIndex = GetRequeteIndex ( strNomTable, strChamps );
			result = connexion.RunStatement ( strRequeteIndex );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error while creating index in the table @1|128",strNomTable));
				return result;
			}
			return result;
		}

		//------------------------------------------------------------------------------------------------
		private static CResultAErreur SupprimeIndex ( string strNomTable, IDatabaseConnexion connexion, params string[] strChamps )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( !ExistIndex ( strNomTable, connexion, strChamps ) )
				return result;
			string strNomIndex = GetNomIndex ( strNomTable, strChamps );
			string strRequeteIndex = "DROP INDEX "+strNomTable+"."+strNomIndex;
			result = connexion.RunStatement ( strRequeteIndex );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error while deleting index @1 of the table @2|130",strNomIndex,strNomTable));
				return result;
			}
			return result;
		}


		//------------------------------------------------------------------------------------------------
		public static CResultAErreur UpdateTable(CStructureTable structure, IDatabaseConnexion connexion, ArrayList strChampsAutorisesANull)
		{
			ArrayList champsCrees = new ArrayList();

			CResultAErreur result = CResultAErreur.True;

			IDataAdapter adapter = connexion.GetSimpleReadAdapter("SELECT * FROM " + structure.NomTableInDb);
			DataSet ds = new DataSet();
			adapter.FillSchema(ds, SchemaType.Mapped);
			DataTable dt =	ds.Tables["TABLE"];
			
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			#region M.A.J des champs de la table

			foreach(CInfoChampTable info in structure.Champs)
			{
				bool bExists = true;
				bool bModifiedType = false;
				bool bModifiedNotNull = false;
				bool bModifiedLength = false;

				foreach(DataColumn colonne in dt.Columns)
				{
					if (info.NomChamp==colonne.ColumnName)
					{
						if ( info.IsLongString )
						{
							if ( colonne.MaxLength < 10000 )
								bModifiedLength = true;
						}
						else
							if(colonne.MaxLength!= info.Longueur && info.TypeDonnee == typeof(string))
								bModifiedLength = true;
						if(colonne.DataType != info.TypeDonnee)
							if ( info.TypeDonnee != typeof(CDonneeBinaireInRow) && colonne.DataType!=typeof(byte[]))
							bModifiedType = true;
						if (colonne.AllowDBNull != info.NullAuthorized)
							bModifiedNotNull = true;

						bExists = true;
						break;
					}
					bExists=false;
				}
				if (!bExists)
				{
					string strRequeteUpdate = "ALTER TABLE " + structure.NomTableInDb + " ADD " + info.NomChamp + " ";
					string strType = (new CSQLServeurTypeMappeur()).GetStringDBTypeFromType(info.TypeDonnee);
					
					if (info.TypeDonnee == typeof(string))
					{
						if ( !info.IsLongString )
							strType += "(" +info.Longueur.ToString() + ") ";
						else
							strType = "NText";
					}
					strRequeteUpdate += strType;
					if (!info.NullAuthorized && !strChampsAutorisesANull.Contains(info.NomChamp))
						strRequeteUpdate += " NOT NULL DEFAULT ''";

					result = connexion.RunStatement(strRequeteUpdate);

					if (!result)
					{
						result.EmpileErreur(I.T("Error while fields updating of table @1|131", structure.NomTableInDb));
						return result;
					}

					champsCrees.Add(info.NomChamp);
				}
				else if (bModifiedType || bModifiedNotNull || bModifiedLength)
				{
					if ( info.IsIndex )
						SupprimeIndex ( structure.NomTableInDb, connexion, info.NomChamp );
					string strRequeteUpdate = "ALTER TABLE " + structure.NomTableInDb + " ALTER COLUMN " + info.NomChamp + " ";
					string strType = (new CSQLServeurTypeMappeur()).GetStringDBTypeFromType(info.TypeDonnee);
					
					if (info.TypeDonnee == typeof(string))
					{
						if ( !info.IsLongString )
							strType += "(" +info.Longueur.ToString() + ") ";
						else
							strType = "NText";
					}
					strRequeteUpdate += strType;
					if (!info.NullAuthorized && !strChampsAutorisesANull.Contains(info.NomChamp))
						strRequeteUpdate += " NOT NULL ";

					result = connexion.RunStatement(strRequeteUpdate);

					if (!result)
					{
						result.EmpileErreur(I.T("Error while fields updating of table @1|131", structure.NomTableInDb));
						return result;
					}
				}
				if ( info.IsIndex )
					result = CreateOrUpdateIndex ( structure.NomTableInDb, connexion, info.NomChamp );
				else
					result = SupprimeIndex ( structure.NomTableInDb, connexion, info.NomChamp );
			}

			// Suppression d'une colonne de la table
			foreach(DataColumn colonne in dt.Columns)
			{
				bool bExists = false;
				foreach(CInfoChampTable info in structure.Champs)
				{
					if (info.NomChamp==colonne.ColumnName)
					{
						bExists = true;
						break;
					}
				}
				if (!bExists)
				{
					CResultAErreur tempResult = CAccessTableCreatorOld.SupprimeDependances(structure.NomTableInDb, colonne.ColumnName, connexion);
					if (!tempResult)
					{
						result.Erreur.EmpileErreurs(tempResult.Erreur);
						result.SetFalse();
						return result;
					}

					tempResult = CAccessTableCreatorOld.SupprimeForeignKeys(structure.NomTableInDb, colonne.ColumnName, connexion);
					if (!tempResult)
					{
						result.Erreur.EmpileErreurs(tempResult.Erreur);
						result.SetFalse();
						return result;
					}

					string strReq = "ALTER TABLE "+structure.NomTableInDb+" DROP COLUMN "+colonne.ColumnName;
					result = connexion.RunStatement ( strReq );
			
					if (!result)
					{
						result.EmpileErreur(I.T("Error while deleting column @1 of table @2|132", colonne.ColumnName , structure.NomTableInDb));
						return result;
					}
				}
			}
			
			#endregion
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			#region M.A.J des contraintes de la table

			foreach(CInfoRelation rel in structure.RelationsParentes)
			{
				foreach(string strChampCree in champsCrees)
				{
					foreach ( string strChamp in rel.ChampsFille )
					{
						if ( strChampCree == strChamp )
						{

							string strRequeteRelation = GetRequeteRelation ( rel );
							result = connexion.RunStatement(strRequeteRelation);
							if (!result)
							{
								result.EmpileErreur(I.T("Error while creating foreign key of @1 (@2)|127",structure.NomTableInDb,strRequeteRelation ));
								return result;
							}
							break;
						}
					}
				}
				if ( result )
				{
					string strNomTableFilleInDb = CContexteDonnee.GetNomTableInDbForNomTable(rel.TableFille);
					if ( rel.Indexed )
						result = CreateOrUpdateIndex(strNomTableFilleInDb, connexion, rel.ChampsFille);
					else
						result = SupprimeIndex(strNomTableFilleInDb, connexion, rel.ChampsFille);
				}
				if ( !result )
					return result;
			}
			#endregion
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			return result;
		}
		//--------------------------------------------------------------------------------------
		private static CResultAErreur SupprimeDependances(string strNomTable, string strNomChamp, IDatabaseConnexion connexion)
		{
			CResultAErreur result = CResultAErreur.True;

			string strRequeteGetForeignKey = 
				@" SELECT obj2.name 
						FROM sysobjects AS obj1 
						INNER JOIN syscolumns AS sc ON sc.id=obj1.id 
						INNER JOIN sysconstraints AS scon ON (scon.id=obj1.id AND sc.colid=scon.colid) 
						INNER JOIN sysobjects AS obj2 ON scon.constid=obj2.id 
						AND obj1.name='" + strNomTable + "' " +
				"AND sc.name='" + strNomChamp + "'";

			IDataAdapter adapter = connexion.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
            connexion.FillAdapter(adapter, ds);
            DataTable dt = ds.Tables[0];

			foreach(DataRow row in dt.Rows)
			{
				string strRequeteSuppressionRelation = "ALTER TABLE " + strNomTable + " DROP CONSTRAINT " + (string) row[0];

				result = connexion.RunStatement(strRequeteSuppressionRelation);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting constrain @1 of table @2|133" ,(string)row[0] , strNomTable));
					return result;
				}
			}

			string strRequeteIndexes = 
				@"SELECT     si.name
					FROM sysobjects tbl INNER JOIN
						syscolumns sc ON sc.id = tbl.id INNER JOIN
						sysindexkeys sik ON sik.id = tbl.id AND sc.colid = sik.colid INNER JOIN
						sysindexes si ON si.id = tbl.id AND si.indid = sik.indid
						WHERE     (tbl.name = '"+strNomTable+"') AND (sc.name = '"+strNomChamp+"') and "+
				"si.name like 'IX%'";
			adapter = connexion.GetSimpleReadAdapter(strRequeteIndexes);
			ds = new DataSet();
            connexion.FillAdapter(adapter, ds);
            dt =	ds.Tables[0];

			foreach(DataRow row in dt.Rows)
			{
				string strRequeteSuppressionIndex = " DROP INDEX " +strNomTable+"."+ (string) row[0];

				result = connexion.RunStatement(strRequeteSuppressionIndex);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting index @1 of the table @2|130",(string)row[0], strNomTable));
					return result;
				}
			}
			return result;
		}
		//--------------------------------------------------------------------------------------
		private static CResultAErreur SupprimeForeignKeys(string strNomTable, string strNomChamp, IDatabaseConnexion connexion)
		{
			CResultAErreur result = CResultAErreur.True;

			string strRequeteGetForeignKey = 
				@" SELECT obj2.name 
						FROM sysobjects AS obj1 
						INNER JOIN syscolumns AS sc ON sc.id=obj1.id 
						INNER JOIN sysforeignkeys AS fk ON (fk.fkeyid=obj1.id AND colid=fk.fkey) 
						INNER JOIN sysobjects AS obj2 ON fk.constid=obj2.id 
						WHERE obj1.xtype='U' 
						AND obj1.name='" + strNomTable + "' " +
				"AND sc.name='" + strNomChamp + "'";

			IDataAdapter adapter = connexion.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
            connexion.FillAdapter(adapter, ds);
            DataTable dt = ds.Tables[0];

			foreach(DataRow row in dt.Rows)
			{
				string strRequeteSuppressionRelation = "ALTER TABLE " + strNomTable + " DROP CONSTRAINT " + (string) row[0];

				result = connexion.RunStatement(strRequeteSuppressionRelation);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting constrain @1 of table @2|133" , (string)row[0], strNomTable));
					return result;
				}
			}
			return result;
		}
		//--------------------------------------------------------------------------------------
		public static bool TableExists(string strTableName, IDatabaseConnexion connexion)
		{
			string[] tableauNomsTable = connexion.TablesNames;
			foreach(string obj in tableauNomsTable)
			{
				if (obj == strTableName)
					return true;
			}
			return false;
		}

		//--------------------------------------------------------------------------------------
		public static CResultAErreur SupprimeChamp ( string strTable, string strChamp, IDatabaseConnexion connexion )
		{
			string strReq = "ALTER TABLE "+strTable+" DROP COLUMN "+strChamp;
			CResultAErreur result = connexion.RunStatement ( strReq );
			
			if (!result)
				result.EmpileErreur(I.T("Error while deleting column @1 of table @2|132",strChamp, strTable));
				
			return result;
		}
		//--------------------------------------------------------------------------------------
	}
}
