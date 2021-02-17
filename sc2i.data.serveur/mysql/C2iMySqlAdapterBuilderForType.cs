using System;
using System.Data;
using System.Data.MySqlClient;
using System.Collections;
using System.Reflection;
using sc2i.data;
using System.Collections.Generic;

namespace sc2i.data.serveur
{
    /// <summary>
    /// Fabrique automatiquement les commandes pour un SqlDataAdapter
    /// </summary>
    public class C2iMySqlAdapterBuilderForType : C2iDbAdapterBuilderForType
    {
        private enum ETypeRequete
        {
            Update,
            Insert,
            Delete
        }
        private class CCacheRequete
        {
            private class CCacheRequetesForTable
            {
                private Dictionary<ETypeRequete, IDbCommand> m_dicPourTable = new Dictionary<ETypeRequete, IDbCommand>();
                public void CCacheRequeteForTable()
                {
                }

                private IDbCommand CloneCommande(IDbCommand command)
                {
                    IDbCommand newCommand = Activator.CreateInstance(command.GetType()) as IDbCommand;
                    newCommand.CommandText = command.CommandText;
                    foreach (IDbDataParameter parametre in command.Parameters)
                    {
                        IDbDataParameter newParam = command.CreateParameter();
                        newParam.ParameterName = parametre.ParameterName;
                        newParam.DbType = parametre.DbType;
                        newParam.Size = parametre.Size;
                        newParam.Direction = parametre.Direction;
                        newParam.SourceColumn = parametre.SourceColumn;
                        newParam.SourceVersion = parametre.SourceVersion;
                        newCommand.Parameters.Add(newParam);
                    }
                    return newCommand;
                }


                public IDbCommand GetCache(ETypeRequete typeRequete)
                {
                    IDbCommand command = null;
                    if (m_dicPourTable.TryGetValue(typeRequete, out command))
                        return CloneCommande(command);
                    return null;
                }

                public void SetCache(ETypeRequete typeRequete, IDbCommand command)
                {
                    m_dicPourTable[typeRequete] = CloneCommande(command);
                }
            }

            private Dictionary<string, CCacheRequetesForTable> m_dicPourTables = new Dictionary<string, CCacheRequetesForTable>();

            public IDbCommand GetCache(string strNomTable, ETypeRequete typeRequete)
            {
                CCacheRequetesForTable cache = null;
                if (m_dicPourTables.TryGetValue(strNomTable, out cache))
                    return cache.GetCache(typeRequete);
                return null;
            }

            public void SetCache(string strNomTable, ETypeRequete typeRequete, IDbCommand command)
            {
                CCacheRequetesForTable cache = null;
                if (!m_dicPourTables.TryGetValue(strNomTable, out cache))
                {
                    cache = new CCacheRequetesForTable();
                    m_dicPourTables[strNomTable] = cache;
                }
                cache.SetCache(typeRequete, command);
            }
        }

        private static CCacheRequete m_cacheRequetes = new CCacheRequete();

        private static Dictionary<Type, bool> m_dicTablesModifiesParTrigger = new Dictionary<Type, bool>();



        private int m_nNumeroVariable = 1;
        //Nom de champ->Numero
        private Hashtable m_tableChampToNumero = new Hashtable();
        public C2iMySqlAdapterBuilderForType(Type tp, CMySqlDatabaseConnexion connexion)
            : base(tp, connexion)
        {
        }



        ////////////////////////////////////////////////////
        public override IDataAdapter GetNewAdapter(DataRowState etatsAPrendreEnCompte, bool bDisableIdAuto, params string[] champsExclus)
        {
            CStructureTable structure = CStructureTable.GetStructure(m_type);
            m_etatsAPrendreEnCompte = etatsAPrendreEnCompte;
            m_tblExclusions = new Hashtable();
            foreach (string strChamp in champsExclus)
                m_tblExclusions[strChamp] = strChamp;
            IDbDataAdapter adapter = (IDbDataAdapter)m_connexion.GetTableAdapter(structure.NomTableInDb);

            if (typeof(IObjetDonneeAutoReference).IsAssignableFrom(m_type))
            {
                adapter = new C2iMySqlDataAdapterForClasseAutoReferencee(m_type, adapter);
            }
            if ( adapter is IDataAdapterGerantLesModificationsParTrigger )
            {
                bool bIsModifiedByTrigger = false;
                if (!m_dicTablesModifiesParTrigger.TryGetValue ( m_type, out bIsModifiedByTrigger ))
                {
                    object[] att = m_type.GetCustomAttributes ( typeof(TableAttribute), true ) ;
                    if ( att.Length > 0 )
                    {
                        TableAttribute attr = att[0] as TableAttribute;
                        if ( attr != null )
                            bIsModifiedByTrigger = attr.ModifiedByTrigger;
                        m_dicTablesModifiesParTrigger[m_type] = bIsModifiedByTrigger;
                    }
                }
                ((IDataAdapterGerantLesModificationsParTrigger)adapter).TableIsModifiedByTrigger = bIsModifiedByTrigger;
            }
            adapter.TableMappings.Add("Table", structure.NomTable);
            adapter.SelectCommand = GetSelectCommand(structure);

            if ((etatsAPrendreEnCompte & DataRowState.Added) != 0)
                adapter.InsertCommand = GetInsertCommand(structure, bDisableIdAuto, adapter);
            else
            {
                adapter.InsertCommand = m_connexion.GetConnexion().CreateCommand();
                adapter.InsertCommand.CommandText = GetCommandQuiFaitRien();
                adapter.InsertCommand.Transaction = m_connexion.Transaction;
            }

            if ((etatsAPrendreEnCompte & DataRowState.Deleted) != 0)
                adapter.DeleteCommand = GetDeleteCommand(structure);
            else
            {
                adapter.DeleteCommand = m_connexion.GetConnexion().CreateCommand();
                adapter.DeleteCommand.CommandText = GetCommandQuiFaitRien();
                adapter.DeleteCommand.Transaction = m_connexion.Transaction;
            }

            if ((etatsAPrendreEnCompte & DataRowState.Modified) != 0)
                adapter.UpdateCommand = GetUpdateCommand(structure);
            else
            {
                adapter.UpdateCommand = m_connexion.GetConnexion().CreateCommand();
                adapter.UpdateCommand.CommandText = GetCommandQuiFaitRien();
                adapter.UpdateCommand.Transaction = m_connexion.Transaction;
            }

            return adapter;
        }

        ////////////////////////////////////////////////////
        protected override string GetCommandQuiFaitRien()
        {
            return "SELECT 1 FROM DUAL";
        }

        ////////////////////////////////////////////////////
        public override IDbCommand GetSelectCommand(CStructureTable structure)
        {
            string strReq = "";
            bool bHasBlob = false;
            foreach (CInfoChampTable info in structure.Champs)
            {
                if (info.TypeDonnee == typeof(CDonneeBinaireInRow) || info.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)))
                    bHasBlob = true;
                else
                    strReq += info.NomChamp + ",";
            }

            if (bHasBlob && strReq.Length > 0)
                strReq = "select " + strReq.Substring(0, strReq.Length - 1) + " from " + structure.NomTableInDb;
            else
                strReq = "select * from " + structure.NomTableInDb;
            IDbCommand command = m_connexion.GetConnexion().CreateCommand();
            command.CommandText = strReq;
            command.Transaction = m_connexion.Transaction;
            return command;

        }
        ////////////////////////////////////////////////////
        public override IDbCommand GetUpdateCommand(CStructureTable structure)
        {
            IDbCommand command = m_cacheRequetes.GetCache(structure.NomTable, ETypeRequete.Update);
            if (command == null && m_tblExclusions == null || m_tblExclusions.Count == 0)
            {
                string strReq = "update " + structure.NomTableInDb + " set ";
                List<CInfoChampTable> listeChampsForWhere = new List<CInfoChampTable>();
                List<CInfoChampTable> listeChampsUpdate = new List<CInfoChampTable>();
                foreach (CInfoChampTable champ in structure.Champs)
                {
                    if (!champ.IsId)
                    {
                        if (m_tblExclusions[champ.NomChamp] == null && !champ.ExclureFormStandardUpdate)
                        {
                            if (champ.TypeDonnee != typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)))
                            {
                                strReq += champ.NomChamp + "=" + GetNomParametreFor(champ, DataRowVersion.Current) + ",";
                                listeChampsUpdate.Add(champ);
                            }
                        }
                    }
                    else
                        listeChampsForWhere.Add(champ);
                }
                listeChampsForWhere.AddRange ( listeChampsUpdate );
                strReq = strReq.Substring(0, strReq.Length - 1) + " ";
                command = m_connexion.GetConnexion().CreateCommand();

                //Ajoute les paramètres nouvelle version
                foreach (CInfoChampTable champ in listeChampsUpdate)
                {
                    if (m_tblExclusions[champ.NomChamp] == null)
                    {
                        if (champ.TypeDonnee != typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)))
                            command.Parameters.Add(GetParametreFor(command, champ, DataRowVersion.Current, ParameterDirection.Input));
                    }
                }

                //La mise à jour doit tenir compte des id !!!!
                foreach (CInfoChampTable champ in structure.ChampsId)
                    listeChampsUpdate.Add(champ);
                AddWhereClauseToDeleteAndUpdate(ref strReq, command, listeChampsForWhere.ToArray());
                command.CommandText = strReq;
                m_cacheRequetes.SetCache(structure.NomTable, ETypeRequete.Update, command);
            }
            command.Connection = m_connexion.GetConnexion();
            command.Transaction = m_connexion.Transaction;
            return command;
        }
        ////////////////////////////////////////////////////
        public override IDbCommand GetInsertCommand(CStructureTable structure, bool bDisableIdAuto, IDbDataAdapter adapter)
        {
            bool bGestionAutoId = true;
            CMySqlDatabaseConnexion conMySql = m_connexion as CMySqlDatabaseConnexion;
            if (conMySql != null && structure.ChampsId.Length == 1 && structure.ChampsId[0].IsAutoId)
                conMySql.GetNomSequenceColAuto(structure.NomTableInDb, structure.ChampsId[0].NomChamp, ref bGestionAutoId);
            //Stef 2804 : N'exclue pas les champs exclus de l'insertion !

            IDbCommand command = m_cacheRequetes.GetCache(structure.NomTable, ETypeRequete.Insert);
            if (command == null)
            {
                string strReq = "insert into " + structure.NomTableInDb + "(";
                string strValues = "(";
                foreach (CInfoChampTable champ in structure.Champs)
                {
                    if ((!champ.IsAutoId || bDisableIdAuto || !bGestionAutoId))
                        if (champ.TypeDonnee != typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)))
                        {
                            strReq += champ.NomChamp + ",";
                            strValues += GetNomParametreFor(champ, DataRowVersion.Current) + ",";
                        }
                }
                strReq = strReq.Substring(0, strReq.Length - 1) + ")";
                strValues = strValues.Substring(0, strValues.Length - 1) + ")";
                strReq += " values " + strValues;

                command = m_connexion.GetConnexion().CreateCommand();
                command.CommandText = strReq;
                //Ajoute les paramètres
                foreach (CInfoChampTable champ in structure.Champs)
                {
                    if ((!champ.IsAutoId || bDisableIdAuto || !bGestionAutoId))
                        if (champ.TypeDonnee != typeof(CDonneeBinaireInRow) && !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)))
                        {
                            command.Parameters.Add(GetParametreFor(command, champ, DataRowVersion.Current, ParameterDirection.Input));
                        }
                }
                m_cacheRequetes.SetCache(structure.NomTable, ETypeRequete.Insert, command);
            }


            if (structure.HasChampIdAuto)
            {
                C2iMySqlDataAdapter MySqlAdapter = C2iMySqlDataAdapter.GetMySqlDataAdapter(adapter);
                if (MySqlAdapter != null && !bDisableIdAuto)
                    MySqlAdapter.PreparerInsertionLigneAvecAutoID(structure.NomTableInDb, structure.ChampsId[0].NomChamp);
            }
            command.Connection = m_connexion.GetConnexion();
            command.CommandType = CommandType.Text;
            command.Transaction = m_connexion.Transaction;
            command.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;

            return command;

        }
        ////////////////////////////////////////////////////
        public override IDbCommand GetDeleteCommand(CStructureTable structure)
        {
            IDbCommand command = m_cacheRequetes.GetCache(structure.NomTable, ETypeRequete.Delete);
            if (command == null)
            {
                string strReq = "delete from " + structure.NomTableInDb;
                command = m_connexion.GetConnexion().CreateCommand();

                AddWhereClauseToDeleteAndUpdate(ref strReq, command, structure.Champs);
                command.CommandText = strReq;
                m_cacheRequetes.SetCache(structure.NomTable, ETypeRequete.Delete, command);
            }
            command.Connection = m_connexion.GetConnexion();
            command.Transaction = m_connexion.Transaction;
            return command;
        }


        ////////////////////////////////////////////////////
        protected override IDbDataParameter GetParametreFor(IDbCommand commande, CInfoChampTable champ, DataRowVersion version, ParameterDirection direction)
        {
            //DbType dbType = m_connexion.GetDbType(champ.TypeDonnee);
            IDbDataParameter parametre = commande.CreateParameter();
            parametre.ParameterName = GetNomParametreFor(champ, version);
            parametre.DbType = m_connexion.GetDbType(champ.TypeDonnee);
            if (champ.TypeDonnee == typeof(string))
                parametre.Size = champ.Longueur;
            parametre.Direction = direction;
            parametre.SourceColumn = champ.NomChamp;
            parametre.SourceVersion = version;
            return parametre;
        }

        ////////////////////////////////////////////////////
        protected override string GetNomParametreFor(CInfoChampTable champ, DataRowVersion version)
        {
            int nNumChamp = 0;
            if (m_tableChampToNumero[champ.NomChamp] == null)
            {
                nNumChamp = m_nNumeroVariable++;
                m_tableChampToNumero[champ.NomChamp] = nNumChamp;
            }
            else
                nNumChamp = (int)m_tableChampToNumero[champ.NomChamp];

            string strNom = m_connexion.GetNomParametre("A" + nNumChamp.ToString());
            switch (version)
            {
                case DataRowVersion.Current:
                    strNom += "_NEW";
                    break;
                case DataRowVersion.Original:
                    strNom += "_OLD";
                    break;
                default:
                    strNom += (int)version;
                    break;
            }
            return strNom;
        }

        ////////////////////////////////////////////////////
        protected override void AddWhereClauseToDeleteAndUpdate(ref string strReq, IDbCommand command, CInfoChampTable[] champs)
        {
            strReq += " where ";
            foreach (CInfoChampTable champ in champs)
            {
                if (m_tblExclusions[champ.NomChamp] == null)
                {
                    if (champ.TypeDonnee != typeof(CDonneeBinaireInRow) &&
                        !champ.TypeDonnee.IsSubclassOf(typeof(CDonneeBinaireInRow)) &&
                        !champ.IsLongString)
                    {
                        //SC le 7/2/08, test systèmatique de null, car même si la structure dit que
                        //c'est non null, il se peut qu'on soit en train de faire une MAJ de base,
                        //en ayant autorisé null provisoirement dans la base, et que des
                        //valeurs nulles existent !
                        //SC070208 if (champ.NullAuthorized || champ.TypeDonnee == typeof(object))
                        strReq += "((" + champ.NomChamp + " is null and " +
                            GetNomParametreFor(champ, DataRowVersion.Original) + " is null) or " +
                            champ.NomChamp + "=" + GetNomParametreFor(champ, DataRowVersion.Original) + ") and ";
                        //SC 070208 else
                        //SC 070208 strReq += champ.NomChamp + "=" + GetNomParametreFor(champ, DataRowVersion.Original) + " and ";
                        IDbDataParameter parametre = GetParametreFor(command, champ, DataRowVersion.Original, ParameterDirection.Input);
                        command.Parameters.Add(parametre);
                    }
                }
            }
            if (champs.Length > 0)
                strReq = strReq.Substring(0, strReq.Length - " and ".Length);
        }



    }
}
