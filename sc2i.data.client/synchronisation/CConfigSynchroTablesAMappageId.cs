using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using sc2i.common;

namespace sc2i.data.synchronisation
{
    /*OBSOLETE AVEC DBKEY
    /// <summary>
    /// Indique les tables pour lesquelles on ne tente pas de conserver les
    /// identifiants de la base main lorsqu'on 
    /// se synchronise
    /// </summary>
    public class CConfigSynchroTablesAMappageId
    {
        private static CConfigSynchroTablesAMappageId m_instance = null;

        //-----------------------------------------------
        public static EventHandler OnCreateConfigSynchroEventHandler;

        private HashSet<string> m_tablesAMappageId = new HashSet<string>();

        //-----------------------------------------------
        public CConfigSynchroTablesAMappageId()
        {
        }

        //-----------------------------------------------
        public static CConfigSynchroTablesAMappageId Instance
        {
            get
            {
                if ( m_instance == null )
                {
                    m_instance = new CConfigSynchroTablesAMappageId();
                    if ( OnCreateConfigSynchroEventHandler != null )
                        OnCreateConfigSynchroEventHandler ( m_instance, null );
                }
                return m_instance;
            }
        }


        //-----------------------------------------------
        public bool IsTableAMappageId ( string strNomTable )
        {
            return m_tablesAMappageId.Contains ( strNomTable );
        }

        //-----------------------------------------------
        public void AddTableAMappageId(string strNomTable, bool bAddTablesFilles)
        {
            if (!m_tablesAMappageId.Contains(strNomTable))
            {
                m_tablesAMappageId.Add(strNomTable);
                Type tp = CContexteDonnee.GetTypeForTable(strNomTable);
                if (tp != null && bAddTablesFilles)
                {
                    CStructureTable structure = CStructureTable.GetStructure(tp);
                    foreach (CInfoRelation rel in structure.RelationsFilles)
                    {
                        if ( !m_tablesAMappageId.Contains ( rel.TableFille ) )
                            AddTableAMappageId(rel.TableFille, true);
                    }
                }
            }
        }

        //-----------------------------------------------
        public List<string> TablesAMappageId
        {
            get
            {
                return new List<string>(m_tablesAMappageId.ToArray());
            }
        }

        //-----------------------------------------------
        public void ConvertIdsFromMainToSecondaire(CContexteDonnee ctx)
        {
            ConvertIds(ctx, true);
        }

        //-----------------------------------------------
        public void ConvertIdsFromSecondaireToMain(CContexteDonnee ctx)
        {
            ConvertIds(ctx, false);
        }

        //-----------------------------------------------
        private void ConvertIds(CContexteDonnee ctx, bool bFromMainToSecondaire)
        {
            foreach (RelationTypeIdAttribute relAttrib in CContexteDonnee.RelationsTypeIds)
            {
                DataTable table = ctx.Tables[relAttrib.TableFille];
                if (table != null)
                {
                    //S'assure que les parents des relations typeId sont chargés
                    foreach (DataRow row in new ArrayList(table.Rows))
                    {
                        if (!ctx.IsToRead(row) && row[relAttrib.ChampId] != DBNull.Value)
                        {
                            Type tp = CActivatorSurChaine.GetType((string)row[relAttrib.ChampType]);
                            if (tp != null)
                            {
                                CObjetDonneeAIdNumerique obj = Activator.CreateInstance(tp, new object[] { ctx }) as CObjetDonneeAIdNumerique;
                                obj.ReadIfExists((int)row[relAttrib.ChampId]);
                            }
                        }
                    }
                }
            }
                            


            foreach (DataTable table in new ArrayList(ctx.Tables))
            {
                if (IsTableAMappageId(table.TableName))
                {
                    Type typeObjets = CContexteDonnee.GetTypeForTable ( table.TableName );
                    Dictionary<int, int> mapIds = new Dictionary<int,int>();
                    string strPrimKey = table.PrimaryKey[0].ColumnName;

                    int nIndexTmp = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        if ((int)row[strPrimKey] > nIndexTmp)
                            nIndexTmp = (int)row[strPrimKey];
                        nIndexTmp++;
                    }
                    
                    //Durant la mise en place du mappage, les ids sont d'abord convertis sur
                    //un identifiant nouveau inconnu dans la table
                    //puis ils sont convertis en id dans la base finale
                    Dictionary<DataRow, int> dicIndexTmpToIndexFinal = new Dictionary<DataRow, int>();

                    //Toutes les lignes dont on a changé l'id
                    //Y compris les lignes qui n'étaient pas encore mappées, mais dont l'id
                    //correspond à un id dans la base main (création d'entité sur secondaire)
                    List<DataRow> lstRowsChangees = new List<DataRow>();

                    //Toutes les lignes dont l'id a été changée pour la base main
                    //CAD celles qui sont mappées
                    List<DataRow> lstRowsChangeesAvecIdDansBaseMain = new List<DataRow>();

                    //Toutes les lignes qui n'étaient pas encore mappés dans le main,
                    //mais donc l'id existait dans main. Ces lignes, en sortie ont un id négatif
                    List<DataRow> lstRowsNonMappéesMaisAvecIdExistantDansMain = new List<DataRow>();


                    StringBuilder bl = new StringBuilder();
                    foreach ( DataRow row in table.Rows )
                    {
                        bl.Append((int)row[strPrimKey]);
                        bl.Append(',');
                    }
                    if (bl.Length > 0)
                    {
                        bl.Remove(bl.Length - 1,1);
                        CListeObjetDonneeGenerique<CMapIdMainToIdSecInDb> lst = new CListeObjetDonneeGenerique<CMapIdMainToIdSecInDb>(ctx);
                        lst.Filtre = new CFiltreData(
                            CMapIdMainToIdSecInDb.c_champMainId+
                            " in (" + bl.ToString() + ") or "+
                        CMapIdMainToIdSecInDb.c_champSecId+" in ("+bl.ToString()+")");
                        lst.AssureLectureFaite();
                    }
                    bl = new StringBuilder();
                    foreach (DataRow row in table.Rows)
                    {
                        int? nIdDansLautreBase = bFromMainToSecondaire ?
                            CMapIdMainToIdSecInDb.GetIdInSecondaire(ctx, table.TableName, (int)row[strPrimKey], false) :
                            CMapIdMainToIdSecInDb.GetIdInMain(ctx, table.TableName, (int)row[strPrimKey], false);
                        
                        if (nIdDansLautreBase != null)
                        {
                            //Met l'id en négatif pour éviter les collisions d'identifiants
                            bl.Append((int)row[strPrimKey]);
                            bl.Append(",");
                            mapIds[(int)row[strPrimKey]] = nIdDansLautreBase.Value;
                            nIndexTmp++;
                            dicIndexTmpToIndexFinal[row] = nIdDansLautreBase.Value;
                            CContexteDonnee.ChangeRowSansDetectionModification(row, strPrimKey, nIndexTmp);
                            lstRowsChangees.Add(row);
                            lstRowsChangeesAvecIdDansBaseMain.Add(row);
                        }
                        else if (!bFromMainToSecondaire)//La ligne n'est pas mappée
                        {
                            //Est-ce que l'id existe dans la base main ?
                            if (CMapIdMainToIdSecInDb.GetIdInSecondaire(ctx, table.TableName, (int)row[strPrimKey], false) != null)
                            {
                                bl.Append((int)row[strPrimKey]);
                                bl.Append(",");
                                mapIds[(int)row[strPrimKey]] = -(int)row[strPrimKey];
                                lstRowsChangees.Add(row);
                                lstRowsNonMappéesMaisAvecIdExistantDansMain.Add(row);//elles seront changées après
                                CContexteDonnee.ChangeRowSansDetectionModification(row, strPrimKey, -(int)row[strPrimKey]);
                            }
                        }

                    }

                    bool bOldEnforce = ctx.EnforceConstraints;
                    ctx.EnforceConstraints = false;
                    //Affecte les ids finales
                    foreach (DataRow row in lstRowsChangeesAvecIdDansBaseMain)
                    {
                        CContexteDonnee.ChangeRowSansDetectionModification(row, strPrimKey, dicIndexTmpToIndexFinal[row]);
                    }

                    object[] atts = typeObjets.GetCustomAttributes(typeof(NoRelationTypeIdAttribute), true);
                    if ( atts.Length==  0 && bl.Length > 0)
                    {
                        bl.Remove ( bl.Length-1, 1 );
                        //Changement des relTypeId
                        foreach (RelationTypeIdAttribute rel in CContexteDonnee.RelationsTypeIds)
                        {
                            CListeObjetsDonnees lst = new CListeObjetsDonnees(ctx, CContexteDonnee.GetTypeForTable(rel.TableFille));
                            lst.Filtre = new CFiltreData(rel.ChampType + "=@1 and " +
                                rel.ChampId + " in (" + bl.ToString() + ")",
                                typeObjets.ToString());
                            lst.InterditLectureInDB = true;
                            foreach (CObjetDonnee objet in lst.ToArrayList())
                            {
                                int nNewId = (int)mapIds[(int)objet.Row[rel.ChampId]];
                                CContexteDonnee.ChangeRowSansDetectionModification(objet.Row.Row, rel.ChampId, nNewId);
                            }
                        }
                    }
                    if (!bFromMainToSecondaire)
                    {
                        //change les ids dans la table de log
                        DataTable tableSync = ctx.Tables[CEntreeLogSynchronisation.c_nomTable];
                        DataRow[] rows = tableSync.Select(CEntreeLogSynchronisation.c_champTable + "='" + table.TableName + "'");
                        foreach (DataRow row in rows)
                        {
                            int nIdElement = (int)row[CEntreeLogSynchronisation.c_champIdElement];
                            if (mapIds.ContainsKey(nIdElement))
                                row[CEntreeLogSynchronisation.c_champIdElement] = mapIds[nIdElement];
                        }
                        tableSync.AcceptChanges();
                    }
                }
            }
        }

    }
     */
}
