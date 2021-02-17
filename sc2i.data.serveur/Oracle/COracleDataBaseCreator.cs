using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Text.RegularExpressions;

using sc2i.common;
using sc2i.multitiers.server;
using sc2i.data;

namespace sc2i.data.serveur
{
	public class COracleDataBaseCreator : C2iDataBaseCreator
	{
		//------------------------------------------------------------------------------------------------
		public COracleDataBaseCreator(COracleDatabaseConnexion connexion)
		{
			m_connection = connexion;
			m_connection.CommandTimeOut = 5 * 1000 * 60;
			m_mappeur = new COracleTypeMapper();
		}

		private COracleDatabaseConnexion m_connection;
		public override IDatabaseConnexion Connection
		{
			get { return (IDatabaseConnexion)m_connection; }
		}

		private COracleTypeMapper m_mappeur;
		public override IDataBaseTypeMapper DataBaseTypesMappeur
		{
			get { return (IDataBaseTypeMapper)m_mappeur; }
		}

		#region Operations sur la structure
		//DataBase
		public override CResultAErreur CreateDatabase()
		{
			//A CONVERTIR<
			CResultAErreur result = CResultAErreur.True;
			if (!Connection.IsConnexionValide())
			{
				string strConnexionString = Connection.ConnexionString;
				//Trouve le nom de la base
				Regex findName = new Regex("Initial catalog[ ]*=[ ]*(.*);?", RegexOptions.IgnoreCase);
				Match match = findName.Match(strConnexionString);
				string strBase = "";
				if (match != null)
				{
					strBase = match.Value;
					string[] strVals = strBase.Split('=');
					if (strVals.Length > 1)
						strBase = strVals[1];
				}
				if (strBase == "")
				{
					result.EmpileErreur(I.T("Impossible to determine the database name|123"));
					return result;
				}
				Regex replaceName = new Regex("Initial catalog[ ]*=[ ]*(.*)(?<ptv>;?)", RegexOptions.IgnoreCase);
				strConnexionString = replaceName.Replace(strConnexionString, "Initial catalog=master${ptv}");

				OracleConnection oraCon = new OracleConnection(strConnexionString);
				try
				{
					oraCon.Open();
					OracleCommand command = new OracleCommand("Create database " + strBase, oraCon);
					command.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					result.EmpileErreur(new CErreurException(e));
					result.EmpileErreur(I.T("Error while creating database|185"));
				}
				finally
				{
					try
					{
						oraCon.Close();
					}
					catch
					{
					}
				}
				((COracleDatabaseConnexion)Connection).GetConnexion().Open();

				//}
				//else
				//    result.EmpileErreur(I.T("'CreateDatabase' function not valid for connection @1|186", connexion.GetType().Name));
			}

			return result;

		}
		public override CResultAErreur InitialiserDataBase()
		{
			CResultAErreur result = CResultAErreur.True;

			#region Creation de la table des champs auto
			if(TableExists(COracleDatabaseConnexion.c_nomTableSysChampAuto))
				result = DeleteTable(COracleDatabaseConnexion.c_nomTableSysChampAuto);

			if (result)
			{
				//Creation de la table
				result = m_connection.RunStatement(String.Format("Create table {0} ("
												+ "{1} NVARCHAR2(25) NOT NULL, "
												+ "{2} NVARCHAR2(25) NOT NULL, "
												+ "{3} NVARCHAR2(30) NOT NULL, "
												+ "{4} NVARCHAR2(30) NOT NULL) ",
												COracleDatabaseConnexion.c_nomTableSysChampAuto,
												COracleDatabaseConnexion.c_nomChampSysChampAutoNomTable,
												COracleDatabaseConnexion.c_nomChampSysChampAutoNomChamp,
												COracleDatabaseConnexion.c_nomChampSysChampAutoTriggerName,
												COracleDatabaseConnexion.c_nomChampSysChampAutoSeqName));
				//Creation des sequences de numérotation
				if (result)
					result = CreerSequencesNumerotationTable(COracleDatabaseConnexion.c_nomTableSysChampAuto);

				//Creation de la clef primaire
				if (result)
					result = m_connection.RunStatement(GetRequeteCreateClefPrimaire(
						COracleDatabaseConnexion.c_nomTableSysChampAuto, 
						COracleDatabaseConnexion.c_nomChampSysChampAutoNomTable,
						COracleDatabaseConnexion.c_nomChampSysChampAutoNomChamp, 
						COracleDatabaseConnexion.c_nomChampSysChampAutoSeqName, 
						COracleDatabaseConnexion.c_nomChampSysChampAutoTriggerName));
			}
			#endregion
			#region Creation de la table du registre
			if (result && TableExists(CDatabaseRegistre.c_nomTable))
				result = DeleteTable(CDatabaseRegistre.c_nomTable);

			//Creation de la table
			if (result)
				result =
				m_connection.RunStatement(
				"Create table " + CDatabaseRegistre.c_nomTable + " (" +
				CDatabaseRegistre.c_champCle + " " + DataBaseTypesMappeur.GetStringDBTypeFromType(typeof(String)) + "(255) NOT NULL," +
				CDatabaseRegistre.c_champValeur + " " + DataBaseTypesMappeur.GetStringDBTypeFromType(typeof(String)) + "(255),"+
				CDatabaseRegistre.c_champBlob+" "+DataBaseTypesMappeur.GetStringDBTypeFromType ( typeof(byte[]) )+")");

			//Creation des sequences de numérotation
			if (result)
				result = CreerSequencesNumerotationTable(CDatabaseRegistre.c_nomTable);

			//Creation Clef Primaire
			if (result)
				result = m_connection.RunStatement(GetRequeteCreateClefPrimaire(CDatabaseRegistre.c_nomTable, CDatabaseRegistre.c_champCle));
			#endregion

			return result;

		}
		public override int NbTableInitialisation
		{
			get { return 2; }
		}

		//Tables
		protected override DataTable GetDataTableForUpdateTable(CStructureTable structure)
		{
			IDataAdapter adapter = Connection.GetSimpleReadAdapter("SELECT * FROM " + structure.NomTableInDb);
			DataTable dtAcharger = new DataTable(structure.NomTableInDb);
			DataSet ds = new DataSet();
			ds.Tables.Add(dtAcharger);
			adapter.FillSchema(ds, SchemaType.Mapped);
            CUtilDataAdapter.DisposeAdapter(adapter);
			ds.Tables[structure.NomTableInDb].TableName = structure.NomTable;
			return ds.Tables[structure.NomTable];
		}
		public override CResultAErreur CreateTable(CStructureTable structure)
		{
			CResultAErreur result = CResultAErreur.True;
			//Création des séquences de numérotation des entités de la table
			result = result ? CreerSequencesNumerotationTable(structure.NomTableInDb) : result;

			//Creation de la table
			result = result ? base.CreateTable(structure) : result;

			//Champs auto-incrémentés
			result = result ? CreateTable_ChampsAutoIncrementes(structure) : result;
			
			if(!result)
				SupprimerSequencesNumerotationTable(structure.NomTableInDb);

			return result;
		}
		public override CResultAErreur DeleteTable(string strNomTableInDb)
		{
			CResultAErreur result = CResultAErreur.True;
			if (!TableExists(strNomTableInDb))
			{
				result.EmpileErreur(I.T("Impossible to remove the table @1 because it does not exist in the database |30013", strNomTableInDb));
				return result;
			}

			//Récupération de la liste des colonnes de la table
			List<string> cols = GetNomColonnes(strNomTableInDb);

			//Creation d'une colonne bidon pour pouvoir supprimer les autres colonnes proprement
			if (!cols.Contains("COLBIDON"))
				result = Connection.RunStatement("ALTER TABLE " + strNomTableInDb + " ADD (COLBIDON NVARCHAR2(1) NULL)");
			else
				cols.Remove("COLBIDON");

			//Suppression des colonnes proprement
			if(result)
				foreach (string col in cols)
				{
					result = DeleteChamp(strNomTableInDb, col);
					if (!result)
						break;
				}

			//Suppression de la table
			if (result)
				result = Connection.RunStatement("DROP TABLE " + strNomTableInDb);

			//Suppression des séquences de numérotation de la table
			if (result)
				result = SupprimerSequencesNumerotationTable(strNomTableInDb);

			if (!result)
				result.EmpileErreur(I.T("Error while deleting the table @1|129", strNomTableInDb));

			return result;
		}
		public override CResultAErreur GetRelationsExistantes(string strNomTable, ref List<CInfoRelation> relationsTable)
		{
			CResultAErreur result = CResultAErreur.True;

			string strNomTableInContexte = m_connection.GetNomTableInContexteFromNomTableInDb(strNomTable);

			#region 1 - Selection des clefs etrangeres de la table
			string strRequeteGetForeignKey = 
				"SELECT constraint_name AS NomRelation, " +
				"r_constraint_name AS NomRelationParente " +
				"FROM USER_constraints " +
				"WHERE constraint_type='R' " +
				"AND table_name = '" + strNomTable + "'";
			
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter ( adapter );
			if (ds.Tables.Count != 1)
			{
				result.EmpileErreur(I.T("Impossible to recover the foreign keys of table '@1' to update them|213", strNomTable));
				return result;
			}
			DataTable dt = ds.Tables[0];
			#endregion
			#region 2 - Recupération des relations des clé etrangeres

			foreach (DataRow row in dt.Rows)
			{
				string[] strChampsParents;
				string[] strChampsFils;
				string strTableParente;
				string strNomRelation = row[0].ToString();
				string strNomContrainteParente = row[1].ToString();

				#region Recuperation des champs fils
				string strRequeteChampsFils =
					"SELECT COLUMN_NAME " +
					"FROM SYS.USER_CONS_COLUMNS " +
			        "WHERE CONSTRAINT_NAME = '" + strNomRelation + "'";
				IDataAdapter adapterChampsFils = Connection.GetSimpleReadAdapter(strRequeteChampsFils);
				DataSet dsChampsFils = new DataSet();

                Connection.FillAdapter(adapterChampsFils, dsChampsFils);
                CUtilDataAdapter.DisposeAdapter(adapterChampsFils);

				if (dsChampsFils.Tables.Count != 1
					|| dsChampsFils.Tables[0].Rows.Count == 0
					|| dsChampsFils.Tables[0].Columns.Count != 1)
				{
					result.EmpileErreur(I.T("Impossible to recover the fields of table '@1' concerned with the relation '@2'|214", strNomTable, strNomRelation));
					return result;
				}
				ArrayList champsFils = new ArrayList();
				foreach (DataRow rowFils in dsChampsFils.Tables[0].Rows)
					champsFils.Add(rowFils[0].ToString());
				strChampsFils = (string[])champsFils.ToArray(typeof(string));
				#endregion

				#region Recuperation du nom et des colonnes de la table parente
				string strRequeteChampsParents =
					"SELECT COLUMN_NAME, TABLE_NAME " +
					"FROM SYS.USER_CONS_COLUMNS " +
			        "WHERE CONSTRAINT_NAME = '" + strNomContrainteParente + "'";
				IDataAdapter adapterChampsPeres = Connection.GetSimpleReadAdapter(strRequeteChampsParents);
				DataSet dsChampsPeres = new DataSet();
                
                Connection.FillAdapter(adapterChampsPeres, dsChampsPeres);
                CUtilDataAdapter.DisposeAdapter(adapterChampsPeres);

                if (dsChampsFils.Tables.Count != 1
					|| dsChampsFils.Tables[0].Rows.Count == 0
					|| dsChampsFils.Tables[0].Columns.Count != 1)
				{
					result.EmpileErreur(I.T("Impossible to recover the fields of the parent table of the relation '@1' with the table '@2'|215", strNomRelation, strNomTable));
					return result;
				}
				ArrayList champsPeres = new ArrayList();
				foreach (DataRow rowPere in dsChampsPeres.Tables[0].Rows)
					champsPeres.Add(rowPere[0].ToString());
				strTableParente = dsChampsPeres.Tables[0].Rows[0][1].ToString();
				string strTableParenteInContexte = m_connection.GetNomTableInContexteFromNomTableInDb(strTableParente);
				strChampsParents = (string[])champsPeres.ToArray(typeof(string));
				#endregion


				relationsTable.Add(new CInfoRelationAClefDefinissable(strNomRelation, strTableParenteInContexte, strNomTableInContexte, strChampsParents, strChampsFils));
			}
			#endregion

			return result;
		}

		//Sequence de numérotation automatique
		internal CResultAErreur CreerSequencesNumerotationTable(string strNomTableInDb)
		{
			CResultAErreur result = CResultAErreur.True;

			//Creation Sequence
			List<string> PrefixeSeq = new List<string>();
			PrefixeSeq.Add("IX");
			PrefixeSeq.Add("SQ");
			PrefixeSeq.Add("TG");
			PrefixeSeq.Add("FK");
			foreach (string prefix in PrefixeSeq)
			{
				string strNomSequence = "SQ" + prefix + "_" + strNomTableInDb;

				//On va tester si elle existe.. (on ne la remplace pas : JAMAIS)
				if (SequenceExists(strNomSequence))
					continue;

				string strSeq = "CREATE SEQUENCE " + strNomSequence;
				strSeq += " INCREMENT BY 1 START WITH 1";
				result = Connection.RunStatement(strSeq);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while creating the SQ@1 sequence in the table @2|192", prefix, strNomTableInDb));
					break; ;
				}
			}

			return result;
		}
		internal CResultAErreur SupprimerSequencesNumerotationTable(string strNomTableInDb)
		{
			CResultAErreur result = CResultAErreur.True;
			if (strNomTableInDb == COracleDatabaseConnexion.c_nomTableSysChampAuto || 
				!TableExists(COracleDatabaseConnexion.c_nomTableSysChampAuto))
				return result;

			//Creation Sequence
			List<string> PrefixeSeq = new List<string>();
			PrefixeSeq.Add("IX");
			PrefixeSeq.Add("SQ");
			PrefixeSeq.Add("TG");
			PrefixeSeq.Add("FK");
			foreach (string prefix in PrefixeSeq)
			{
				string strNomSequence = "SQ" + prefix + "_" + strNomTableInDb;

				//On va tester si elle existe..
				if (!SequenceExists(strNomSequence))
					continue;

				string strSeq = "DROP SEQUENCE " + strNomSequence;
				result = Connection.RunStatement(strSeq);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting  the SQ@1 sequence in the table @2 |191", prefix, strNomTableInDb));
					break;
				}
			}

			return result;
		}

		//Champs Auto Incremente
		internal CResultAErreur CreateTable_ChampsAutoIncrementes(CStructureTable structure)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (CInfoChampTable info in structure.Champs)
				if (info.IsAutoId)
				{
					result = CreerSystemeAutoIncremente(structure.NomTableInDb, info.NomChamp);
					if (!result)
					{
						result.EmpileErreur(I.T("Error while creating the numbering system of the column @1 of the table @2|189", info.NomChamp, structure.NomTableInDb));
						break;
					}
				}
			return result;
		}

		internal CResultAErreur CreerSystemeAutoIncremente(string strNomTableInDb, string strNomChamp)
		{
			CResultAErreur result = CResultAErreur.True;
            bool bAvecTrigger = true;
            try
            {
                Type tp = CContexteDonnee.GetTypeForTable ( ((COracleDatabaseConnexion)Connection).GetNomTableInContexteFromNomTableInDb ( strNomTableInDb ));
                IEnumerable<object> attrs = from a  in tp.GetCustomAttributes ( typeof(NoTriggerAutoIncrementAttribute), true)
                                                                 where ((NoTriggerAutoIncrementAttribute)a).Champ == strNomChamp
                                                                 select a;
                if (attrs.Count() > 0)
                    bAvecTrigger = false;
            }
            catch
            {
            }
			//Creation Sequence
			string strNomSeq = GetNewNom(EntiteTable.Sequence, strNomTableInDb);
			if (!SequenceExists(strNomSeq))
				result = Connection.RunStatement("CREATE SEQUENCE " + strNomSeq + " INCREMENT BY 1 START WITH 1");

			//Creation Trigger
			string strNomTrigger = " ";
			if (result && bAvecTrigger)
			{
				strNomTrigger = GetNewNom(EntiteTable.Trigger, strNomTableInDb);
				if (!SequenceExists(strNomTrigger))
				{
					string strTrigg = "CREATE TRIGGER " + strNomTrigger;
					strTrigg += " BEFORE INSERT ON " + strNomTableInDb;
					strTrigg += " FOR EACH ROW ";
					strTrigg += " BEGIN SELECT " + strNomSeq + ".NEXTVAL INTO ";
					strTrigg += ":NEW." + strNomChamp + " FROM dual; END;";

					result = Connection.RunStatement(strTrigg);
				}
			}


			if (result)
			{
				string strRefSysChampAutoExist = "SELECT * FROM "+
					COracleDatabaseConnexion.c_nomTableSysChampAuto+" WHERE "
					+COracleDatabaseConnexion.c_nomChampSysChampAutoNomTable+" = '"
					+ strNomTableInDb + "' AND "
					+COracleDatabaseConnexion.c_nomChampSysChampAutoNomChamp+" = '" 
					+ strNomChamp+"'";
				IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRefSysChampAutoExist);
				DataSet dsTmp = new DataSet();
                Connection.FillAdapter(adapter, dsTmp);
                CUtilDataAdapter.DisposeAdapter(adapter);
                string strUpdateRefSysChampAuto ;
                if (dsTmp.Tables[0].Rows.Count == 0)
                {
                    //Referencement
                    strUpdateRefSysChampAuto = "INSERT INTO " + COracleDatabaseConnexion.c_nomTableSysChampAuto +
                                        "(" +
                                        COracleDatabaseConnexion.c_nomChampSysChampAutoNomTable + "," +
                                        COracleDatabaseConnexion.c_nomChampSysChampAutoNomChamp + "," +
                                        COracleDatabaseConnexion.c_nomChampSysChampAutoSeqName + "," +
                                        COracleDatabaseConnexion.c_nomChampSysChampAutoTriggerName + ") " +
                                        "VALUES('" + strNomTableInDb + "', '"
                                                  + strNomChamp + "', '"
                                                  + strNomSeq + "', '"
                                                  + strNomTrigger + "')";
                }
                else
                {
                    strUpdateRefSysChampAuto = 
                        "UPDATE "+COracleDatabaseConnexion.c_nomTableSysChampAuto+" set "+
                        COracleDatabaseConnexion.c_nomChampSysChampAutoSeqName+" = '"+strNomSeq+"', "+
                        COracleDatabaseConnexion.c_nomChampSysChampAutoTriggerName+" = '"+strNomTrigger+"' where "+
                        COracleDatabaseConnexion.c_nomChampSysChampAutoNomTable+"='"+strNomTableInDb+"' and "+
                        COracleDatabaseConnexion.c_nomChampSysChampAutoNomChamp+"='"+strNomChamp+"'";
                    result = Connection.RunStatement(strUpdateRefSysChampAuto);
                }

				result = Connection.RunStatement(strUpdateRefSysChampAuto);

				if (!result)
					result.EmpileErreur(I.T("Impossible to reference the field '@1' automation of @2 table|196", strNomChamp, strNomTableInDb));
				
			}
			if (!result)
                result.EmpileErreur(I.T("Error while creating  sequence of @1 table|195", strNomTableInDb));

			return result;
		}


		internal CResultAErreur SupprimerSystemeAutoIncremente(string strNomTableInDb, string strNomChamp)
		{
			CResultAErreur result = CResultAErreur.True;
			if (TableExists(COracleDatabaseConnexion.c_nomTableSysChampAuto))
				return result;
			IDataAdapter adapter;
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();

            string strColAuto = "SELECT " +
                COracleDatabaseConnexion.c_nomChampSysChampAutoTriggerName + "," +
                COracleDatabaseConnexion.c_nomChampSysChampAutoSeqName + " from " +
                COracleDatabaseConnexion.c_nomTableSysChampAuto +
                            " WHERE TABLE_NAME = '" + strNomTableInDb + "' AND " +
                            "CHAMP_NAME = '" + strNomChamp + "'";

			adapter = Connection.GetSimpleReadAdapter(strColAuto);

            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			dt = ds.Tables[0];

			if (dt.Rows.Count != 0)
			{
				//Sequence
				string strNomSQ = dt.Rows[0]["SQ_NAME"].ToString();
				result = Connection.RunStatement("DROP SEQUENCE " + strNomSQ);
				if (!result)
				{
					result.EmpileErreur("Error wile deleting sequence @1 of table @2|191", strNomSQ, strNomTableInDb);
					return result;
				}

				//Trigger
				string strNomTG = dt.Rows[0]["TG_NAME"].ToString();
				result = Connection.RunStatement("DROP TRIGGER " + strNomTG);
				if (!result)
				{
                    result.EmpileErreur(I.T("Error while deleting  numbering trigger of field @1 in table @2|193", strNomChamp, strNomTableInDb));
					return result;
				}

				string strSuppRefSysAuto = "DELETE FROM "+
					COracleDatabaseConnexion.c_nomTableSysChampAuto+" WHERE "
					+COracleDatabaseConnexion.c_nomChampSysChampAutoNomTable+" = '" 
					+ strNomTableInDb + "' AND "
					+COracleDatabaseConnexion.c_nomChampSysChampAutoNomChamp+ " = '" 
					+ strNomChamp + "' AND "
					+COracleDatabaseConnexion.c_nomChampSysChampAutoSeqName+ " = '" 
					+ strNomSQ + "' AND "
					+COracleDatabaseConnexion.c_nomChampSysChampAutoTriggerName+ " = '" 
					+ strNomTG + "' ";

				result = Connection.RunStatement(strSuppRefSysAuto);

				if (!result)
					result.EmpileErreur(I.T("Error while auto incremental field reference deleting for the field '@1' of table @2 in SYS_CHAMP_AUTO|194", strNomChamp, strNomTableInDb));
			}

			return result;
		}
		internal bool SystemeAutoIncrementeExist(string strNomTable, string strNomChamp)
		{
			if (!TableExists(COracleDatabaseConnexion.c_nomTableSysChampAuto))
				return false;

			IDataAdapter adapter;
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();

            string strColAuto = "SELECT TG_NAME, SQ_NAME FROM " + COracleDatabaseConnexion.c_nomTableSysChampAuto +
                    " WHERE " +
                    COracleDatabaseConnexion.c_nomChampSysChampAutoNomTable + " = '" +
                    strNomTable + "' AND " +
                    COracleDatabaseConnexion.c_nomChampSysChampAutoNomChamp + " = '" +
                    strNomChamp + "' from " +
                    COracleDatabaseConnexion.c_nomTableSysChampAuto;

			adapter = Connection.GetSimpleReadAdapter(strColAuto);
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			dt = ds.Tables[0];

			if (dt.Rows.Count != 0)
				return true;
			else
				return false;
		}
		internal bool IsChampAutoIncremente(string strNomTable, string strNomChamp)
		{
			IDataAdapter adapter;
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();

			string strColAuto = "SELECT "+
				COracleDatabaseConnexion.c_nomChampSysChampAutoTriggerName+","
				+COracleDatabaseConnexion.c_nomChampSysChampAutoSeqName+" FROM "+COracleDatabaseConnexion.c_nomTableSysChampAuto
				+ " WHERE "
				+COracleDatabaseConnexion.c_nomChampSysChampAutoNomTable+" = '" 
				+ strNomTable + "' AND "
				+COracleDatabaseConnexion.c_nomChampSysChampAutoNomChamp+" = '" 
				+ strNomChamp + "'";

			adapter = Connection.GetSimpleReadAdapter(strColAuto);
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			dt = ds.Tables[0];

			if (dt.Rows.Count != 0)
				return true;
			else
				return false;
		}

		//Champs
		public override CResultAErreur CreateChamp(string strNomTable, CInfoChampTable champ)
		{
			CResultAErreur result = base.CreateChamp(strNomTable, champ);
			if (result && champ.IsAutoId && !SystemeAutoIncrementeExist(strNomTable, champ.NomChamp))
				result = CreerSystemeAutoIncremente(strNomTable, champ.NomChamp);
			return result;
		}
		public override CResultAErreur DeleteChamp_Dependances(string strNomTableInDb, string strNomChamp)
		{
			CResultAErreur result = CResultAErreur.True;

			#region Trigger et de la Séquence de numérotation si colonne auto-incrémenté
			result = SupprimerSystemeAutoIncremente(strNomTableInDb, strNomChamp);
			if (!result)
				return result;
			#endregion

			#region Contraintes (y compri clef primaire)
			//Récupère le nom des contraintes de la colonne de la table
			string strRequeteGetConstraints = "SELECT DISTINCT Contrainte.CONSTRAINT_NAME " +
					"FROM SYS.USER_CONS_COLUMNS Colonne, SYS.USER_CONSTRAINTS Contrainte " +
					"WHERE Colonne.CONSTRAINT_NAME = Contrainte.CONSTRAINT_NAME " +
					"AND (Colonne.TABLE_NAME ='" + strNomTableInDb + "') " +
					"AND (Colonne.COLUMN_NAME='" + strNomChamp + "') ";

			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequeteGetConstraints);
			DataSet ds = new DataSet();

            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable dt = ds.Tables[0];

			foreach (DataRow row in dt.Rows)
			{
				string strRequeteSuppressionRelation = "ALTER TABLE " + strNomTableInDb + " DROP CONSTRAINT " + (string)row[0];
				result = Connection.RunStatement(strRequeteSuppressionRelation);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting constrain @1 of table @2|133" ,(string)row[0] , strNomTableInDb));
					return result;
				}
			}
			#endregion

			#region Index (tout les index lié à la colonne)
			string strRequeteIndexes = " SELECT Idx.INDEX_NAME " +
					"FROM SYS.USER_IND_COLUMNS Colonne, SYS.USER_INDEXES Idx " +
					"WHERE Colonne.INDEX_NAME = Idx.INDEX_NAME " +
					"AND (Colonne.TABLE_NAME ='" + strNomTableInDb + "') " +
					"AND (Colonne.COLUMN_NAME='" + strNomChamp + "') ";

			adapter = Connection.GetSimpleReadAdapter(strRequeteIndexes);
			ds = new DataSet();
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			dt = ds.Tables[0];

			foreach (DataRow row in dt.Rows)
			{
				string strRequeteSuppressionIndex = " DROP INDEX " + row[0].ToString();
				result = Connection.RunStatement(strRequeteSuppressionIndex);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting index @1 of the table @2|130", row[0].ToString(), strNomTableInDb));
					return result;
				}
			}
			#endregion

			return result;
		}
		public override CResultAErreur DeleteChamp_ClesEtrangeres(string strNomTable, string strNomChamp)
		{
			CResultAErreur result = CResultAErreur.True;

			//Récupère le nom des contraintes clefs Etrangeres de la colonne de la table
			string strRequeteGetForeignKey = " SELECT DISTINCT Con.CONSTRAINT_NAME " +
					"FROM SYS.USER_CONS_COLUMNS Col, SYS.USER_CONSTRAINTS Con " +
					"WHERE Col.CONSTRAINT_NAME = Con.CONSTRAINT_NAME " +
					"AND (Col.TABLE_NAME ='" + strNomTable + "') " +
					"AND (Col.COLUMN_NAME='" + strNomChamp + "') " +
					"AND (Con.CONSTRAINT_TYPE='R') ";

			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequeteGetForeignKey);
			DataSet ds = new DataSet();
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable dt = ds.Tables[0];
			foreach (DataRow row in dt.Rows)
			{
				string strRequeteSuppressionRelation = "ALTER TABLE " + strNomTable + " DROP CONSTRAINT " + row[0].ToString();
				result = Connection.RunStatement(strRequeteSuppressionRelation);

				if (!result)
				{
					result.EmpileErreur(I.T("Error while deleting constraint @1 of table @2|133" ,(string)row[0], strNomTable));
					return result;
				}
			}
			return result;
		}

		#endregion

		#region Creation de Requete
		public override string GetDeclarationDefaultValueForType(Type tp)
		{
			if(tp == typeof(string))
				return "DEFAULT ' '";
			else if (tp == typeof(DateTime) || tp == typeof(DateTime?) || tp == typeof(CDateTimeEx))
				return "DEFAULT SYSDATE";
			else if (tp == typeof(bool) || tp == typeof(bool?))
				return "DEFAULT 0";
			else
				return base.GetDeclarationDefaultValueForType(tp);
		}

		protected override string GetRequeteCreateClefPrimaire(string strNomTable, params string[] strFields)
		{
			string rqt = base.GetRequeteCreateClefPrimaire(strNomTable, strFields);
			if (m_connection.NomTableSpaceIndex != "")
			    rqt += " using index tablespace " + m_connection.NomTableSpaceIndex;
			return rqt;
		}
		protected override string GetRequeteUpdateChamp(string strNomTable, CInfoChampTable champ, bool bModifiedNotNull, bool bModifiedLength, bool bModifiedType)
		{
			string rqt = "ALTER TABLE " + strNomTable + " MODIFY( " + champ.NomChamp + " ";
			
			if(bModifiedType || bModifiedLength)
				rqt += GetDeclarationChamp_TypeAndLength(champ) + " ";
			if (bModifiedNotNull)
				rqt += GetDeclarationChamp_NotNull(champ, true);

			rqt += ")";
			return rqt;
		}
		protected override string GetRequeteCreateIndex(string strNomTable, bool bCluster, params string[] strFields)
		{
			string rqt = base.GetRequeteCreateIndex(strNomTable, bCluster, strFields);
			string strNomTableSpaceIndex = ((COracleDatabaseConnexion)Connection).NomTableSpaceIndex;
			return (strNomTableSpaceIndex != "") ? rqt + " tablespace " + strNomTableSpaceIndex : rqt;
		}
		protected override string GetRequeteDeleteIndex(string strNomTable, string strNomIndex)
		{
			return "DROP INDEX " + strNomIndex;
		}
		#endregion

		#region Nommage
		//------------------------------------------------------------------------------------------------
		public List<string> GetNomColonnes(string strNomTable)
		{
			List<string> cols = new List<string>();

			string strCols = "SELECT CNAME FROM SYS.COL WHERE (TNAME = '" + strNomTable + "')";
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strCols);
			DataSet ds = new DataSet();
            Connection.FillAdapter(adapter, ds);
            CUtilDataAdapter.DisposeAdapter(adapter);
			DataTable dt = ds.Tables[0];

			foreach (DataRow dr in dt.Rows)
				cols.Add(dr[0].ToString());

			return cols;
		}

		//------------------------------------------------------------------------------------------------
		protected enum EntiteTable
		{
			Trigger,
			ClefEtrangere,
			Index,
			Sequence
		}
		protected string GetNewNom(EntiteTable Entite, string NomTable)
		{
			string NomEntite = "";

			#region 1 - On récupère l'ID de notre nouvelle entité
			string strRequete = "SELECT ";
            string strNomSequence = "SQ";
			DataSet dsTmp = new DataSet();
			IDataAdapter adapter;
			switch (Entite)
			{
                case EntiteTable.Trigger: strNomSequence += "TG_"; break;
                case EntiteTable.ClefEtrangere: strNomSequence += "FK_"; break;
                case EntiteTable.Index: strNomSequence += "IX_"; break;
                case EntiteTable.Sequence: strNomSequence += "SQ_"; break;
				default: break;
			}
            strNomSequence += NomTable;
            if (!SequenceExists(strNomSequence))
                CreerSequencesNumerotationTable( NomTable );
			strRequete += strNomSequence + ".NEXTVAL FROM SYS.\"DUAL\" ";
			adapter = Connection.GetSimpleReadAdapter(strRequete);
            Connection.FillAdapter(adapter, dsTmp);
            CUtilDataAdapter.DisposeAdapter(adapter);
			string nID = dsTmp.Tables[0].Rows[0][0].ToString();
			#endregion

			#region 2 - On Compose le nom de notre entité avec cet ID
			switch (Entite)
			{
				case EntiteTable.Trigger: NomEntite = "T"; break;
				case EntiteTable.ClefEtrangere: NomEntite = "F"; break;
				case EntiteTable.Index: NomEntite = "I"; break;
				case EntiteTable.Sequence: NomEntite = "S"; break;
			}


			NomEntite += CGenerateurStringUnique.GetCodeFor(long.Parse(nID));

			if ((NomEntite.Length + NomTable.Length) > 30)
			{
				int nDiff = Math.Abs(30 - (NomEntite.Length + NomTable.Length));
				string strSubNomTable = NomTable.Substring(0, (NomTable.Length - nDiff));
				NomEntite = strSubNomTable + NomEntite;
			}
			else
				NomEntite = NomTable + NomEntite;
			#endregion

			return NomEntite;

		}

		protected override string GetNewNomClefEtrangere(CInfoRelation rel)
		{
            string strNomTable = CContexteDonnee.GetNomTableInDbForNomTable(rel.TableFille);
            return GetNewNom(EntiteTable.ClefEtrangere, strNomTable);
		}
		protected override string GetNewNomIndex(string strNomTable, params string[] strFields)
		{
			return GetNewNom(EntiteTable.Index, strNomTable);
		}
		protected override string GetNewNomSequence(string strNomTable)
		{
			return GetNewNom(EntiteTable.Sequence, strNomTable);
		}
		protected override string GetNewNomTrigger(string strNomTable)
		{
			return GetNewNom(EntiteTable.Trigger, strNomTable);
		}

		protected override string GetNomIndex(string strNomTable, params string[] strFields)
		{
            /*
			string strRequete = "SELECT IDX.INDEX_NAME ";
			strRequete += "FROM SYS.USER_IND_COLUMNS Col, SYS.USER_INDEXES Idx ";
			strRequete += "WHERE Col.INDEX_NAME = Idx.INDEX_NAME AND (Col.TABLE_NAME ='" + strNomTable + "') ";
			foreach (string strField in strFields)
				strRequete += "AND(Col.COLUMN_NAME='" + strField + "') ";*/
            string strRequete = "SELECT IDX.INDEX_NAME FROM ";
            string strSep = "";
            string [] strCols = new string [strFields.Length];

            for (int i = 0; i < strFields.Length; i++)
            {
                strCols[i] = "Col" + i.ToString();
                strRequete += strSep + "SYS.USER_IND_COLUMNS " + strCols[i];
                strSep = ", ";
            }
            strRequete += ", SYS.USER_INDEXES Idx WHERE ";
            strSep = "";
            for (int j = 0; j < strFields.Length; j++)
            {
                strRequete += strSep + strCols[j] + ".COLUMN_NAME='" + strFields[j] + "' AND " + strCols[j] + ".INDEX_NAME = Idx.INDEX_NAME AND (" + strCols[j] + ".TABLE_NAME ='" + strNomTable + "') ";
                strSep = " AND ";
            }

			DataSet dsTmp = new DataSet();
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strRequete);
            Connection.FillAdapter(adapter, dsTmp);
            CUtilDataAdapter.DisposeAdapter(adapter);
            if (dsTmp.Tables.Count > 0 && dsTmp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsTmp.Tables[0].Rows)
                {
                    //Vérifie que l'index a le bon nombre de colonnes. En effet, un index peut porter
                    //une colonne attendue, mais aussi sur d'autres
                    CResultAErreur result = m_connection.ExecuteScalar(@"select count(*) from
                        SYS.USER_IND_COLUMNS where INDEX_NAME='" + row[0].ToString()+"'");
                    if (result)
                    {
                        int nVal = Convert.ToInt32(result.Data);
                        if (nVal == strFields.Count())
                            return row[0].ToString();
                    }
                }
            }
            return "";
		}

        /// <summary>
        /// Pas de gestion d'index cluster dans Oracle pour le moment
        /// </summary>
        /// <param name="strNomTable"></param>
        /// <param name="strChamps"></param>
        /// <returns></returns>
        public override bool IsCluster(string strNomTable, params string[] strChamps)
        {
            return false;
        }
		#endregion
		#region Verification Nommage
		protected override CResultAErreur CheckNomTable(string strNomTable)
		{
			CResultAErreur result = CResultAErreur.True;

			if (strNomTable.Length > 25)
				result.EmpileErreur(I.T("The name of table '@1' should not exceed 25 characters|187", strNomTable));

			return result;
		}
		protected override CResultAErreur CheckNomColonne(string strNomTable, string strNomCol)
		{
			CResultAErreur result = CResultAErreur.True;
			if (strNomCol.Length > 25)
			{
				result.EmpileErreur(I.T("The name of field '@1' should not exceed 25 characters|189", strNomCol));
				return result;
			}
			if (strNomCol == strNomTable)
			{
				result.EmpileErreur(I.T("The name of field '@1' should not been similar in the table name|190", strNomCol));
				return result;
			}
			return result;
		}
		
		#endregion

		#region Tests d'existance
		//--------------------------------------------------------------------------------------
		public override bool ChampExists(string strTableName, string strChampName)
		{
			List<string> cols = GetNomColonnes(strTableName);
			return cols.Contains(strChampName);
		}
		//--------------------------------------------------------------------------------------
		public override bool SequenceExists(string NomSequence)
		{
			string strSeqExist = "SELECT * FROM SYS.USER_SEQUENCES ";
			strSeqExist += "WHERE SEQUENCE_NAME ='" + NomSequence + "'";
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strSeqExist);
			DataSet dsTmp = new DataSet();
            Connection.FillAdapter(adapter, dsTmp);
            CUtilDataAdapter.DisposeAdapter(adapter);
            if (dsTmp.Tables[0].Rows.Count > 0)
				return true;
			else
				return false;
		}
		//--------------------------------------------------------------------------------------
		public override bool TriggerExists(string NomTrigger)
		{
			string strTriExist = "SELECT * FROM SYS.USER_TRIGGERS ";
			strTriExist += "WHERE TRIGGER_NAME ='" + NomTrigger + "'";
			IDataAdapter adapter = Connection.GetSimpleReadAdapter(strTriExist);
			DataSet dsTmp = new DataSet();
            Connection.FillAdapter(adapter, dsTmp);
            CUtilDataAdapter.DisposeAdapter(adapter);
            if (dsTmp.Tables[0].Rows.Count > 0)
				return true;
			else
				return false;
		}
		//------------------------------------------------------------------------------------------------
		public override bool IndexExists(string strNomTable, params string[] strChamps)
		{
			string strNomIndex = GetNomIndex(strNomTable, strChamps);
			if (strNomIndex == "")
				return false;
			else
				return true;
		}
		#endregion
	}
}
