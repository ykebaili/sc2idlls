using System;
using System.Collections;
using System.Data;
using sc2i.common;
using System.Text;
using System.Collections.Generic;
using sc2i.data.synchronisation;

namespace sc2i.data
{
	/// <summary>
	/// Description résumée de CContexteDonneesSynchro.
	/// </summary>
    [Serializable]
	public class CContexteDonneesSynchro : CContexteDonnee
	{
		private		int m_nIdSynchro;

		////////////////////////////////////////////////////////////////////////
		public CContexteDonneesSynchro( int nIdSession, bool bAutoStructure )
			:base (nIdSession, bAutoStructure, false)
		{
		}

		////////////////////////////////////////////////////////////////////////
		public void CopyFromDataSet ( DataSet ds )
		{
			foreach ( DataTable table in CContexteDonnee.GetTablesOrderInsert(ds) )
			{
				Merge ( table, false, MissingSchemaAction.Add );
			}
		}

		////////////////////////////////////////////////////////////////////////
		public CContexteDonneesSynchro ()
			:base()
		{
		}

		////////////////////////////////////////////////////////////////////////
		public int IdSynchro
		{
			get
			{
				return m_nIdSynchro;
			}
		}

        ////////////////////////////////////////////////////////////////////////
        public CResultAErreurType<List<CModifSynchronisation>> GetModifsFromVersion(
            int nIdSyncStart,
            int nIdSyncEnd,
            CFiltresSynchronisation filtres)
        {
            bool bTmp = false;
            CResultAErreur res = FillWithModifsFromVersion(
                nIdSyncStart,
                nIdSyncEnd,
                ref bTmp,
                filtres,
                false,
                true);
            CResultAErreurType<List<CModifSynchronisation>> result = new CResultAErreurType<List<CModifSynchronisation>>();
            if (!res)
            {
                result.EmpileErreur(res.Erreur);
                result.Result = false;
            }
            else
            {
                result.DataType = res.Data as List<CModifSynchronisation>;
            }
            return result;

        }

        ////////////////////////////////////////////////////////////////////////
        public CResultAErreur IntegreModif(CModifSynchronisation modif)
        {
            DataTable table = GetTableSafe(modif.TableName);
            IObjetServeur loader = GetTableLoader(modif.TableName);
            bool bTmp = false;
            return IntegreModif(ref bTmp, table, loader, modif.FiltreSynchronisation, modif.IdSyncStart, modif.IdSyncEnd);
        }

		////////////////////////////////////////////////////////////////////////
		/// <summary>
        /// Remplit le contexte avec toutes les données modifiées depuis l'Id de synchro demandée
		/// </summary>
		/// <param name="nIdSynchroDebut"></param>
		/// <param name="nIdSynchroFin"></param>
		/// <param name="bHasData"></param>
		/// <param name="filtres"></param>
		/// <param name="bOnlyTablesIndiqueesDansFiltresSynchro"></param>
		/// <returns></returns>
		public CResultAErreur  FillWithModifsFromVersion 
            ( 
            int nIdSynchroDebut, 
            int nIdSynchroFin, 
            ref bool bHasData, 
            CFiltresSynchronisation filtres,
            bool bOnlyTablesIndiqueesDansFiltresSynchro)
		{
            return FillWithModifsFromVersion(
                nIdSynchroDebut,
                nIdSynchroFin,
                ref bHasData,
                filtres,
                bOnlyTablesIndiqueesDansFiltresSynchro,
                false);
        }

        protected CResultAErreur FillWithModifsFromVersion(
            int nIdSynchroDebut,
            int nIdSynchroFin,
            ref bool bHasData,
            CFiltresSynchronisation filtres,
            bool bOnlyTablesIndiqueesDansFiltresSynchro,
            bool bNePasLireLesDonnées )
        {
			m_nIdSynchro = nIdSynchroDebut;
			CResultAErreur result = CResultAErreur.True;
			Clear();

			//Crée la structure
			string[] strTables = m_mappeurTablesToClass.GetListeTables();
            if (bOnlyTablesIndiqueesDansFiltresSynchro)
                strTables = filtres.TablesDefinies;
			foreach ( string strTable in strTables )
			{
				try
				{
					GetTableSafe(strTable);
				}
				catch
				{
					//La table n'existe pas, elle n'est probablement pas dans la structure secondaire
				}
			}

			//va chercher les informations dans la base pour chaque table
			ArrayList lst = GetTablesOrderInsert();
			int nTable = 0;
			bool bOldEnforce = EnforceConstraints;
			EnforceConstraints = false;
            List<CModifSynchronisation> lstModifs = new List<CModifSynchronisation>();
			foreach ( DataTable table in lst )
			{
				if ( m_mappeurTablesToClass.IsSynchronisable(table.TableName) )
				{
					IObjetServeur loader = GetTableLoader(table.TableName);
                    CFiltreData filtreSynchro = GetFiltreSynchro(IdSession, nIdSynchroDebut, nIdSynchroFin, filtres, table);
                    if (bNePasLireLesDonnées)
                    {
                        int nCount = loader.CountRecords(table.TableName, filtreSynchro);
                        if (nCount > 0)
                        {
                            CModifSynchronisation modif = new CModifSynchronisation(
                                table.TableName,
                                nCount,
                                filtreSynchro,
                                nIdSynchroDebut,
                                nIdSynchroFin);
                            lstModifs.Add(modif);
                        }
                    }
                    else
                    {
                        nTable++;
                        result = IntegreModif(
                            ref bHasData, 
                            table, 
                            loader, 
                            filtreSynchro, 
                            nIdSynchroDebut,
                            nIdSynchroFin);
                        if (!result)
                            return result;
                    }
				}
			}
            if (!bNePasLireLesDonnées)
            {
                //Intègre la table des synclog
                CListeObjetDonneeGenerique<CEntreeLogSynchronisation> lstLogs = new CListeObjetDonneeGenerique<CEntreeLogSynchronisation>(this);
                if (nIdSynchroDebut >= 0)
                    lstLogs.Filtre = new CFiltreData(CSc2iDataConst.c_champIdSynchro + ">=@1 and " +
                        CSc2iDataConst.c_champIdSynchro + "<=@2",
                        nIdSynchroDebut,
                        nIdSynchroFin);
                else
                    lstLogs.Filtre = new CFiltreData(CSc2iDataConst.c_champIdSynchro + ">=@1",
                        nIdSynchroFin);
                lstLogs.AssureLectureFaite();
            }

			EnforceConstraints = bOldEnforce;


			Hashtable tableRowAdd = new Hashtable();//Liste des éléments ajoutés
			ArrayList listRowToDelete = new ArrayList();
			//Les éléments sont supprimés s'ils ont été ajoutés et supprimés ensuite

			/*//Charge les logs de données ajoutées et supprimées
			CListeObjetsDonnees lstEntrees = new CListeObjetsDonnees ( this, typeof(CEntreeLogSynchronisation));
			lstEntrees.Filtre = new CFiltreData ( CSc2iDataConst.c_champIdSynchro+">=@1 and "+
				CSc2iDataConst.c_champIdSynchro+"<=@2", nIdSynchroDebut, nIdSynchroFin);
            if ( bOnlyTablesIndiqueesDansFiltresSynchro )
            {
                StringBuilder bl = new StringBuilder();
                foreach ( string strTable in strTables )
                    bl.Append("'"+strTable+"',");
                bl.Remove ( bl.Length-1, 1);
                lstEntrees.Filtre = CFiltreData.GetAndFiltre ( lstEntrees.Filtre,
                    new CFiltreData ( CEntreeLogSynchronisation.c_champTable+" in ("+
                        bl.ToString()+")"));
            }
			foreach ( CEntreeLogSynchronisation entree in lstEntrees )
			{
				if ( entree.TypeModif == CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd )
				{
					DataTable table = GetTableSafe ( entree.TableConcernee );
					CObjetDonneeAIdNumerique obj = (CObjetDonneeAIdNumerique)GetNewObjetForTable ( table );
					obj.Id = entree.IdElement;
					tableRowAdd[entree.TableConcernee+"_"+entree.IdElement] = entree.Row;
					bHasData = true;
				}
				else
				{
					if ( tableRowAdd[entree.TableConcernee+"_"+entree.IdElement] != null )
					{
						listRowToDelete.Add ( entree.Row );
						listRowToDelete.Add ( tableRowAdd[entree.TableConcernee+"_"+entree.IdElement] );
						bHasData = true;
					}
				}
			}

			foreach ( DataRow row in listRowToDelete )
			{
				row.Delete();
			}*/

            if (bNePasLireLesDonnées)
                result.Data = lstModifs;
			return result;
		}

        //------------------------------------------------------------------------------------
        private CResultAErreur IntegreModif(
            ref bool bHasData, 
            DataTable table, 
            IObjetServeur loader, 
            CFiltreData filtreSynchro, 
            int nIdSyncStart,
            int nIdSyncEnd)
        {
            CResultAErreur result = CResultAErreur.True;
            DataTable newTable = loader.Read(filtreSynchro);

            if (newTable.Rows.Count != 0)
                bHasData = true;

            foreach (DataRow row in newTable.Rows)
            {
                if (nIdSyncStart == -1)
                {
                    row.AcceptChanges();
                    row.SetAdded();
                }
                else
                {
                    int? nIdSessionRow = row[CSc2iDataConst.c_champIdSynchro] as int?;
                    if (nIdSessionRow == null &&
                        nIdSessionRow.Value >= nIdSyncStart)
                    {

                        row.AcceptChanges();
                        row.SetModified();
                    }
                }
            }

            IntegreTable(newTable, false);




            //Synchronisation des blobs
            if (loader.HasBlobs())
            {
                string strPrim = table.PrimaryKey[0].ColumnName;
                foreach (DataColumn col in table.Columns)
                {
                    if (col.DataType == typeof(CDonneeBinaireInRow))
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            result = loader.ReadBlob(col.ColumnName, new object[] { row[strPrim] });
                            if (result)
                            {
                                CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(IdSession, row, col.ColumnName);
                                donnee.Donnees = (byte[])result.Data;
                                row[col.ColumnName] = donnee;
                            }

                            if (!result)
                                return result;
                        }
                    }
                }
            }

            //Gestion des ajouts et suppressions
            CListeObjetDonneeGenerique<CEntreeLogSynchronisation> lst = new CListeObjetDonneeGenerique<CEntreeLogSynchronisation>(this);
            if (nIdSyncStart == -1)
            {
                lst.Filtre = new CFiltreData(CEntreeLogSynchronisation.c_champTable + "=@1 and " +
                    CSc2iDataConst.c_champIdSynchro + "<=@2",
                    table.TableName,
                    nIdSyncEnd);
            }
            else
            {
                lst.Filtre = new CFiltreData(CEntreeLogSynchronisation.c_champTable + "=@1 and " +
                    CSc2iDataConst.c_champIdSynchro + "<=@2 and " +
                    CSc2iDataConst.c_champIdSynchro + ">=@3",
                    table.TableName,
                    nIdSyncEnd,
                    nIdSyncStart);
            }
            lst.Tri = CEntreeLogSynchronisation.c_champType;
            foreach (CEntreeLogSynchronisation log in lst)
            {
                if (log.TypeModif == CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd)
                {
                    DataRow row = table.Rows.Find(log.IdElement);
                    if (row != null && row.RowState != DataRowState.Added)
                    {
                        row.AcceptChanges();
                        row.SetAdded();
                    }
                }
                if (log.TypeModif == CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete)
                {
                    DataRow row = table.Rows.Find(log.IdElement);
                    if (row != null)
                    {
                        if (row.RowState == DataRowState.Added)
                            table.Rows.Remove(row);
                        else
                            row.Delete();
                    }
                }
            }
            return result;
        }

        //-------------------------------------------------------------------------------------------
        public List<CInfoSuppressionSynchronisation> GetSuppressions(int nIdSyncStart, int nIdSyncEnd)
        {
            CListeObjetDonneeGenerique<CEntreeLogSynchronisation> lst = new CListeObjetDonneeGenerique<CEntreeLogSynchronisation>(this);
            if (nIdSyncStart == -1)
            {
                lst.Filtre = new CFiltreData(
                    CSc2iDataConst.c_champIdSynchro + "<=@1 and "+
                CEntreeLogSynchronisation.c_champType+"=@2",
                    nIdSyncEnd,
                    (int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete);
            }
            else
            {
                lst.Filtre = new CFiltreData(
                    CSc2iDataConst.c_champIdSynchro + "<=@1 and " +
                    CSc2iDataConst.c_champIdSynchro + ">=@2 and "+
                    CEntreeLogSynchronisation.c_champType+"=@3",
                    nIdSyncEnd,
                    nIdSyncStart,
                    (int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete);
            }
            lst.Tri = CEntreeLogSynchronisation.c_champId;
            List<CInfoSuppressionSynchronisation> lstSuppressions = new List<CInfoSuppressionSynchronisation>();
            foreach (CEntreeLogSynchronisation entree in lst)
            {
                CInfoSuppressionSynchronisation info = new CInfoSuppressionSynchronisation(entree.TableConcernee, entree.IdElement, entree.IdSyncSession);
                lstSuppressions.Add(info);
            }
            return lstSuppressions;
        }

        //-------------------------------------------------------------------------------------------
        private static CFiltreData GetFiltreSynchro(int nIdSession, int nIdSynchroDebut, int nIdSynchroFin, CFiltresSynchronisation filtres, DataTable table)
        {
            CFiltreData filtreSynchro = null;
            if (filtres != null)
                filtreSynchro = filtres.GetFiltreForTable(nIdSession, table.TableName);
            if (filtreSynchro == null)
                filtreSynchro = new CFiltreData();
            int nNumParametreIdSync = filtreSynchro.Parametres.Count + 1;
            string strFiltre = filtreSynchro.Filtre;
            filtreSynchro.Filtre = "(" + CSc2iDataConst.c_champIdSynchro + ">=@" + nNumParametreIdSync.ToString() + " and " +
                CSc2iDataConst.c_champIdSynchro + "<=@" + (nNumParametreIdSync + 1).ToString();
            if (nIdSynchroDebut == -1)
            //C'est la première synchro, intègre les éléments modifiés avant prise en charge
            //des synchros
            {
                if (filtreSynchro is CFiltreDataAvance)
                    filtreSynchro.Filtre += " or hasno(" + CSc2iDataConst.c_champIdSynchro + ")";
                else
                    filtreSynchro.Filtre += " or " + CSc2iDataConst.c_champIdSynchro + " is null";
            }
            filtreSynchro.Filtre += ")";
            if (strFiltre != "")
                filtreSynchro.Filtre += " and (" + strFiltre + ")";
            filtreSynchro.Parametres.Add(nIdSynchroDebut);
            filtreSynchro.Parametres.Add(nIdSynchroFin);
            return filtreSynchro;
        }

		/// //////////////////////////////////////////
		///<summary>
		///Charge les données de la table qui vont devoir être mise à jour
		///à partir des données modifiées dans la table source
		///</summary>
		public CResultAErreur ChargeDonneesAMettreAJour ( DataSet donneesSources )
		{
			CResultAErreur result = CResultAErreur.True;

			ArrayList lstTables = CContexteDonnee.GetTablesOrderInsert ( donneesSources );

			foreach ( DataTable table in lstTables )
			{
				DataTable tableDest = null;
				//S'assure que la table est bien chargée
				try
				{
					tableDest = GetTableSafe ( table.TableName );
				}
				catch
				{
					//La table n'existe pas
				}
				if ( tableDest != null  && m_mappeurTablesToClass.IsSynchronisable(table.TableName) && table.Rows.Count != 0)
				{
					IObjetServeur serveur = CContexteDonnee.GetTableLoader(tableDest.TableName, null, IdSession);
					string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(tableDest.TableName);
					if (serveur.CountRecords(strNomTableInDb, new CFiltreData("1=1")) != 0)//Première maj : copie complète
					{
						string strColPrim = table.PrimaryKey[0].ColumnName;
						string strFiltre = "";
						foreach ( DataRow row in table.Rows )
							strFiltre += row[strColPrim].ToString()+",";
						if ( strFiltre.Length != 0 )
						{
							//Supprime la dernière virgule;
							strFiltre = strFiltre.Substring(0, strFiltre.Length-1);
							strFiltre = strColPrim += " in ("+strFiltre+")";
							IObjetServeur loader = GetTableLoader(table.TableName);
							DataTable tableNew = loader.Read(new CFiltreData(strFiltre));
							if ( table ==  null )
							{
                                result.EmpileErreur(I.T("Error while reading table @1|128", table.TableName));
								return result;
							}
							IntegreTable ( tableNew, false );
						}
					}
				}
			}
			//Charge les éléments à supprimer
			CContexteDonnee contexteForListe = new CContexteDonnee ( IdSession, true, false );
			DataTable tableSync = donneesSources.Tables[CEntreeLogSynchronisation.c_nomTable];
			DataTable tableCopie = CUtilDataSet.AddTableCopie ( donneesSources.Tables[CEntreeLogSynchronisation.c_nomTable], contexteForListe );
			foreach ( DataRow row in tableSync.Rows )
				tableCopie.ImportRow ( row );
			CListeObjetsDonnees liste = new CListeObjetsDonnees(contexteForListe, typeof(CEntreeLogSynchronisation));
			liste.InterditLectureInDB = true;
			liste.Filtre = new CFiltreData(CEntreeLogSynchronisation.c_champType+"=@1",(int)CEntreeLogSynchronisation.TypeModifLogSynchro.tDelete );
			foreach ( CEntreeLogSynchronisation entree in liste )
			{
				DataTable table = GetTableSafe ( entree.TableConcernee );
				CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)GetNewObjetForTable ( table );
				objet.ReadIfExists(entree.IdElement);
			}
			return result;
		}

		/// //////////////////////////////////////////
		public CResultAErreur SaveBlobs()
		{
			CResultAErreur result = CResultAErreur.True;
			foreach ( DataTable table in Tables )
			{
				if ( m_mappeurTablesToClass.IsSynchronisable(table.TableName) )
				{
					IObjetServeur loader = GetTableLoader(table.TableName);
					if ( loader.HasBlobs() )
					{
						string strPrim = table.PrimaryKey[0].ColumnName;
						foreach ( DataColumn col in table.Columns )
						{
							if ( col.DataType == typeof(CDonneeBinaireInRow) )
							{
								foreach ( DataRow row in table.Rows )
								{
									if ( row[col.ColumnName] != DBNull.Value )
									{
										CDonneeBinaireInRow db = (CDonneeBinaireInRow)row[col.ColumnName];
										result = loader.SaveBlob(col.ColumnName, new object[] { row[strPrim] }, db.Donnees, null, null);
										if ( !result )
											return result;
									}
								}
							}
						}
					}
				}  
			}
			return result;
		}
	}
}
