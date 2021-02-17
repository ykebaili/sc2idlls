using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sc2i.common;
using System.Collections;

namespace sc2i.data.synchronisation
{
    [Serializable]
    public class CContexteDonneeIntegrateurModifsSecondaire : CContexteDonnee
    {
        [NonSerialized]
        private Dictionary<DataRow, DataRow> m_mapRowsFromSecondaireToMain = new Dictionary<DataRow, DataRow>();

        [NonSerialized]
        private Dictionary<DataRow, int> m_mapOldRowToOldId = new Dictionary<DataRow,int>();

        //-----------------------------------------------------------------------
        public CContexteDonneeIntegrateurModifsSecondaire()
            : base()
        {
        }

        public CContexteDonneeIntegrateurModifsSecondaire(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext contexte)
            : base(info, contexte)
        {
        }
        
        //-----------------------------------------------------------------------
        public CContexteDonneeIntegrateurModifsSecondaire(int nIdSession)
            : base(nIdSession, true, false)
        {
        }

        //-----------------------------------------
        /// <summary>
        /// Déclenche l'évenement AFTER_SYNC_ON_MAIN sur les objets qui implémentent cet
        /// évenement et qui ont été modifiés ou ajoutés dans le contexte de données
        /// </summary>
        /// <param name="contexteDonnee"></param>
        /// <returns></returns>
        public CResultAErreur DeclencheEvenementAfterSyncOnMain(CContexteDonnee contexteDonnee)
        {
            CResultAErreur result = CResultAErreur.True;
            foreach (DataTable table in new ArrayList(contexteDonnee.Tables))
            {
                Type tp = CContexteDonnee.GetTypeForTable(table.TableName);
                object[] evtsAtt = tp.GetCustomAttributes(typeof(EvenementAttribute), true);
                if (evtsAtt.Length > 0)
                {
                    bool bHasEvt = false;
                    foreach (EvenementAttribute att in evtsAtt)
                    {
                        if (att.Identifiant == CUtilSynchronisation.c_strEvenementAfterSyncSurMain)
                        {
                            bHasEvt = true;
                            break;
                        }
                    }
                    if (bHasEvt)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                            {
                                CObjetDonneeAIdNumerique obj = Activator.CreateInstance(tp, row) as CObjetDonneeAIdNumerique;
                                if (obj != null)
                                    obj.EnregistreEvenement(CUtilSynchronisation.c_strEvenementAfterSyncSurMain, false);
                            }
                        }
                    }
                }
            }
            return result;
        }


        //-----------------------------------------------------------------------
        public CResultAErreurType<CMapOldIdToNewId> IntegreModifsDeSecondaireDansMain(CContexteDonnee ctxDonnee)
        {
            CResultAErreurType<CMapOldIdToNewId> result = new CResultAErreurType<CMapOldIdToNewId>();
            CResultAErreur resTmp = CResultAErreur.True;
            EnforceConstraints = false;
            resTmp = AjouteNouveauxFromSecondaire(ctxDonnee);
            if ( resTmp )
                resTmp = TraiteModifiesFromSecondaire(ctxDonnee);
            if ( resTmp )
                resTmp = DeclencheEvenementAfterSyncOnMain(this);
            if (resTmp)
                resTmp = SaveAll(true);
            if (!resTmp)
            {
                result.EmpileErreur(resTmp.Erreur);
                return result;
            }
            CMapOldIdToNewId map = new CMapOldIdToNewId();
            foreach (KeyValuePair<DataRow, int> kv in m_mapOldRowToOldId)
            {
                DataRow rowDest = null;
                if (m_mapRowsFromSecondaireToMain.TryGetValue(kv.Key, out rowDest))
                {
                    map.SetNewIdForElement(kv.Key.Table.TableName, kv.Value,
                        (int)rowDest[rowDest.Table.PrimaryKey[0]]);
                }
            }
            result.DataType = map;
            return result;
        }

        //-----------------------------------------------------------------------
        private CResultAErreur AjouteNouveauxFromSecondaire(CContexteDonnee ctxDonnee)
        {
            CUtilSynchronisation utilSync = new CUtilSynchronisation(IdSession);
            int nIdSyncSessionEnCours = utilSync.IdSyncSession;
            CResultAErreur result = CResultAErreur.True;
            DataTable tableLogsSynchroFromSecondaire = ctxDonnee.Tables[CEntreeLogSynchronisation.c_nomTable];
            if (tableLogsSynchroFromSecondaire == null)
            {
                result.EmpileErreur(I.T("Synchro table missing|20003"));
                return result;
            }

            Dictionary<CIdentifiantElement, int> dicNewIds = new Dictionary<CIdentifiantElement, int>();

            ArrayList lstTables = ctxDonnee.GetTablesOrderInsert();
            foreach (DataTable tableSource in lstTables)
            {
                if (tableSource.PrimaryKey.Length == 1 && m_mappeurTablesToClass.IsSynchronisable ( tableSource.TableName ))
                {
                    DataTable tableDest = GetTableSafe(tableSource.TableName);
                    string strPrimKey = tableDest.PrimaryKey[0].ColumnName;
                    //Identifie tous les ajouts
                    DataRow[] rowsLogAjout = tableLogsSynchroFromSecondaire.Select(
                        CEntreeLogSynchronisation.c_champTable + "='" + tableSource.TableName + "' and " +
                        CEntreeLogSynchronisation.c_champType + "=" + (int)CEntreeLogSynchronisation.TypeModifLogSynchro.tAdd);
                    foreach (DataRow rowLogAjout in rowsLogAjout)
                    {
                        //Cherche la DataRow dans la table source
                        DataRow rowSource = tableSource.Rows.Find(rowLogAjout[CEntreeLogSynchronisation.c_champIdElement]);
                        if (rowSource != null)
                        {
                            //Création d'une nouvelle ligne
                            DataRow rowDest = tableDest.NewRow();
                            //copie de la ligne 
                            foreach (DataColumn col in tableSource.Columns)
                            {
                                if ( rowDest.Table.Columns.Contains ( col.ColumnName ) && 
                                    col.ColumnName != strPrimKey )
                                    rowDest[col.ColumnName] = rowSource[col.ColumnName];
                            }
                            rowDest[CSc2iDataConst.c_champIdSynchro] = nIdSyncSessionEnCours;
                            tableDest.Rows.Add(rowDest);
                            m_mapRowsFromSecondaireToMain[rowSource] = rowDest;
                            m_mapOldRowToOldId[rowSource] = (int)rowSource[strPrimKey];
                            
                            
                            dicNewIds[new CIdentifiantElement(tableDest.TableName, (int)rowSource[strPrimKey])] = (int)rowDest[strPrimKey];
                            //Change l'id dans la rowSource pour qu'il soit
                            //répercuté sur les dépendances
                            rowSource[strPrimKey] = rowDest[strPrimKey];

                        }
                    }
                }
            }

            //change tous les anciens identifiants par les nouveaux
            foreach (KeyValuePair<CIdentifiantElement, int> kv in dicNewIds)
            {
                CIdentifiantElement ident = kv.Key;
                int nNewId = kv.Value;
                foreach (CInfoRelation relation in CContexteDonnee.GetListeRelationsTable(ident.TableName))
                {
                    if (relation.TableParente == ident.TableName)
                    {
                        DataTable tableThis = Tables[relation.TableFille];
                        if (tableThis != null)
                        {
                            DataRow[] rows = tableThis.Select(relation.ChampsFille[0] + "=" + ident.IdElement);
                            foreach (DataRow row in rows)
                                row[relation.ChampsFille[0]] = nNewId;
                        }
                        DataTable tableSource = ctxDonnee.Tables[relation.TableFille];
                        if (tableSource != null)
                        {
                            DataRow[] rows = tableSource.Select(relation.ChampsFille[0] + "=" + ident.IdElement);
                            foreach (DataRow row in rows)
                                row[relation.ChampsFille[0]] = nNewId;
                        }
                    }
                }
                Type tp = CContexteDonnee.GetTypeForTable(ident.TableName);
                //Change les ids des relation typeid
                foreach (RelationTypeIdAttribute rel in RelationsTypeIds)
                {
                    DataTable tableThis = Tables[rel.TableFille];
                    if (tableThis != null)
                    {
                        DataRow[] rows = tableThis.Select(rel.ChampId + "=" + ident.IdElement + " and " +
                            rel.ChampType + "='" + tp.ToString() + "'");
                        foreach (DataRow row in rows)
                            row[rel.ChampId] = nNewId;
                    }
                    DataTable tableSource = ctxDonnee.Tables[rel.TableFille];
                    if (tableSource != null)
                    {
                        DataRow[] rows = tableSource.Select(rel.ChampId + "=" + ident.IdElement + " and " +
                            rel.ChampType + "='" + tp.ToString() + "'");
                        foreach (DataRow row in rows)
                            row[rel.ChampId] = nNewId;
                    }
                }
            }
            return result;
        }

        //-----------------------------------------------------------------------
        private CResultAErreur TraiteModifiesFromSecondaire ( CContexteDonnee ctxSecondaire )
        {
            CResultAErreur result = CResultAErreur.True;
            CUtilSynchronisation utilSync = new CUtilSynchronisation(IdSession);
            int nIdSyncSessionEnCours = utilSync.IdSyncSession;
            ArrayList lstTables = ctxSecondaire.GetTablesOrderInsert();

            DataTable tableLogsSynchroFromSecondaire = ctxSecondaire.Tables[CEntreeLogSynchronisation.c_nomTable];
            if (tableLogsSynchroFromSecondaire == null)
            {
                result.EmpileErreur(I.T("Synchro table missing|20003"));
                return result;
            }

            foreach ( DataTable tableSource in lstTables )
            {
                Type tpObjet = CContexteDonnee.GetTypeForTable ( tableSource.TableName);
                if ( 
                    tableSource.PrimaryKey.Length == 1 && 
                    m_mappeurTablesToClass.IsSynchronisable ( tableSource.TableName ) && 
                    tpObjet != null )
                {
                    DataTable tableDest = GetTableSafe ( tableSource.TableName );
                    string strPrimKey = tableDest.PrimaryKey[0].ColumnName;
                    ArrayList lstRows = new ArrayList ( tableSource.Rows);
                    foreach ( DataRow rowSource in lstRows )
                    {
                        //S'assure que la rowSource a bien été modifiée
                        if (rowSource.RowState == DataRowState.Modified)
                        {
                            DataRow rowDest = tableDest.Rows.Find(rowSource[strPrimKey]);
                            if (rowDest == null)
                            {
                                CObjetDonneeAIdNumerique objet = Activator.CreateInstance(tpObjet, new object[] { this }) as CObjetDonneeAIdNumerique;
                                if (objet != null)
                                {
                                    if (!objet.ReadIfExists((int)rowSource[strPrimKey]))
                                        objet = null;
                                    else
                                        rowDest = objet.Row.Row;
                                }
                            }
                            else
                            {
                                CObjetDonneeAIdNumerique objet = Activator.CreateInstance(tpObjet, new object[] { rowDest }) as CObjetDonneeAIdNumerique;
                                //S'assure de la lecture de la ligne
                                DataRow rowTmp = objet.Row.Row;
                            }

                            if (rowDest != null)//sinon, c'est bizarre, ça
                            //veut dire que la ligne aurait été supprimée dans la base principale
                            {
                                foreach (DataColumn col in tableSource.Columns)
                                {
                                    if (rowDest.Table.Columns.Contains(col.ColumnName) &&
                                        col.ColumnName != strPrimKey)
                                        rowDest[col.ColumnName] = rowSource[col.ColumnName];
                                }
                                rowDest[CSc2iDataConst.c_champIdSynchro] = nIdSyncSessionEnCours;
                            }
                        }
                    }
                }
            }
            return result;
        }



                        





                

                
                




    }
}
