using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.multitiers.server;
using sc2i.data;
using sc2i.common;
using System.Data;
using sc2i.multitiers.client;
using sc2i.data.serveur;
using sc2i.data.dynamic;

namespace sc2i.process.serveur
{
    public class CActionSupprimerEntiteServeur : C2iObjetServeur, IActionSupprimerEntiteServeur
    {
        private static List<Type> m_listeTypesValeursChamps = null;
        //------------------------------------------------------------
        public CActionSupprimerEntiteServeur(int nIdSession)
            : base(nIdSession)
        {
        }

        //------------------------------------------------------------
        public CResultAErreur PurgeEntites(Type typeObjets, int[] lstIds)
        {
            CResultAErreur result = CResultAErreur.True;
            CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
            IInfoUtilisateur info = session != null ? session.GetInfoUtilisateur() : null;
            if (info == null)
            {
                result.EmpileErreur(I.T("Invalid session in Purge entities|20014"));
                return result;
            }
            if (info.GetDonneeDroit(CDroitDeBaseSC2I.c_droitAdministrationSysteme) == null)
            {
                result.EmpileErreur(I.T("Purge entity is allowed to administrators only|20015"));
                return result;
            }
            result = session.BeginTrans();
            if (!result)
                return result;
            CDonneeNotificationModificationContexteDonnee dataModif = new CDonneeNotificationModificationContexteDonnee(IdSession);
            result = PurgeEntites(typeObjets, lstIds, dataModif);
            if (result)
            {
                CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[] { dataModif });
                result = session.CommitTrans();
            }
            else
                session.RollbackTrans();
            return result;
        }
            

        //------------------------------------------------------------
        private CResultAErreur TestTypePurgeable(Type typeObjets)
        {
            CResultAErreur result = CResultAErreur.True;
            if (typeObjets == null)
            {
                result.EmpileErreur(I.T("Invalid element type in purge action|20012"));
                return result;
            }
            if (!typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(typeObjets))
            {
                result.EmpileErreur(I.T("Can not purge elements of type '@1'|20013"),
                    DynamicClassAttribute.GetNomConvivial(typeObjets));
            }
            return result;
        }


        //------------------------------------------------------------
        public CResultAErreur PurgeEntites(
            Type typeObjets,
            int[] nIdsElementsTotal,
            CDonneeNotificationModificationContexteDonnee dataModifs)
        {
            CResultAErreur result = TestTypePurgeable(typeObjets);
            if (!result)
                return result;
            

            //travaille par paquets de 100;
            for (int nPaquet = 0; nPaquet < nIdsElementsTotal.Length; nPaquet += 100)
            {
                List<int> lstTmp = new List<int>();
                int nMin = Math.Min(nPaquet + 100, nIdsElementsTotal.Length);
                for (int n = nPaquet; n < nMin; n++)
                    lstTmp.Add(nIdsElementsTotal[n]);
                int[] nIdsElements = lstTmp.ToArray();
                if (nIdsElements.Count() == 0)
                    return result;

                string strNomTable = CContexteDonnee.GetNomTableForType(typeObjets);
                if (strNomTable == null)
                {
                    result.EmpileErreur(I.T("Can not find table for type '@1'|20011"),
                        DynamicClassAttribute.GetNomConvivial(typeObjets));
                    return result;
                }


                CStructureTable structure = CStructureTable.GetStructure(typeObjets);
                StringBuilder blIds = new StringBuilder();
                foreach (int nId in nIdsElements)
                {
                    blIds.Append(nId);
                    blIds.Append(',');
                }
                if (blIds.Length == 0)
                    return result;
                blIds.Remove(blIds.Length - 1, 1);

                //Si autoref, va tout chercher d'un coup
                if (typeof(IObjetDonneeAutoReference).IsAssignableFrom(typeObjets))
                {
                    HashSet<int> setIds = new HashSet<int>();
                    foreach (int nId in nIdsElements)
                        setIds.Add(nId);
                    //Va cherche toutes les dépendances
                    int nNbLast = setIds.Count();
                    while (true)
                    {
                        foreach (CInfoRelation rel in CContexteDonnee.GetListeRelationsTable(strNomTable))
                        {
                            if (rel.TableFille == rel.TableParente)
                            {
                                int[] idsFilles = null;
                                CFiltreData filtre = new CFiltreData(rel.ChampsFille[0] + " in (" + blIds.ToString() + ")");
                                result = GetIdsFilles(
                                    typeObjets,
                                    filtre,
                                    out idsFilles);
                                if (!result)
                                    return result;
                                foreach (int nId in idsFilles)
                                {
                                    setIds.Add(nId);
                                }
                            }
                        }
                        nIdsElements = setIds.ToArray();
                        if (nNbLast == setIds.Count)
                            break;
                        blIds = new StringBuilder();
                        foreach (int nId in setIds)
                        {
                            blIds.Append(nId);
                            blIds.Append(',');
                        }
                        blIds.Remove(blIds.Length - 1, 1);
                        nNbLast = setIds.Count;
                    }
                }


                //Suppression des relations filles
                foreach (CInfoRelation info in CContexteDonnee.GetListeRelationsTable(strNomTable))
                {
                    if (info.TableParente == strNomTable && info.TableFille != strNomTable)
                    {
                        Type typeFils = CContexteDonnee.GetTypeForTable(info.TableFille);
                        int[] lstIdsFils = null;
                        CFiltreData filtre = new CFiltreData(
                            info.ChampsFille[0] + " in (" + blIds.ToString() + ")");
                        result = GetIdsFilles(typeFils,
                            filtre,
                            out lstIdsFils);
                        if (!result)
                            return result;
                        if (lstIdsFils.Count() > 0)
                            result = PurgeEntites(typeFils, lstIdsFils, dataModifs);
                        if (!result)
                            return result;
                    }
                }
                //Suppression des relations TypeId
                if (typeObjets.GetCustomAttributes(typeof(NoRelationTypeIdAttribute), true).Length == 0)
                {
                    foreach (RelationTypeIdAttribute rel in CContexteDonnee.RelationsTypeIds)
                    {
                        if (rel.IsAppliqueToType(typeObjets))
                        {
                            Type typeFils = CContexteDonnee.GetTypeForTable(rel.TableFille);
                            result = TestTypePurgeable(typeFils);
                            if (!result)
                                return result;
                            CFiltreData filtre = new CFiltreData(
                                rel.ChampId + " in (" + blIds + ") and " +
                                rel.ChampType + "=@1",
                                typeObjets.ToString());
                            int[] lstIdsFils = null;
                            result = GetIdsFilles(
                                        typeFils,
                                        filtre,
                                        out lstIdsFils);
                            if (!result)
                                return result;
                            if (lstIdsFils.Count() > 0)
                                result = PurgeEntites(typeFils, lstIdsFils, dataModifs);
                            if (!result)
                                return result;
                        }
                    }
                }

                //Suppression des valeurs de champs
                if (m_listeTypesValeursChamps == null)
                {
                    m_listeTypesValeursChamps = new List<Type>();
                    foreach (Type tp in CContexteDonnee.GetAllTypes())
                    {
                        if (typeof(CRelationElementAChamp_ChampCustom).IsAssignableFrom(tp))
                            m_listeTypesValeursChamps.Add(tp);
                    }
                }
                foreach (Type tp in m_listeTypesValeursChamps)
                {
                    CFiltreData filtre = new CFiltreData(
                        CRelationElementAChamp_ChampCustom.c_champValeurInt + " in (" +
                        blIds.ToString() + ") and " +
                        CRelationElementAChamp_ChampCustom.c_champValeurString + "=@1",
                        typeObjets.ToString());
                    int[] idsFils = null;
                    result = GetIdsFilles(tp, filtre, out idsFils);
                    if (!result)
                        return result;
                    if (idsFils.Length > 0)
                        result = PurgeEntites(tp, idsFils, dataModifs);
                    if (!result)
                        return result;
                }




                //Prépare les notifications
                foreach (int nId in nIdsElements)
                {
                    dataModifs.AddModifiedRecord(strNomTable,
                        true,
                        new object[] { nId });
                }

                //supprime les éléments
                //Et c'est parti pour la requete de suppression
                IDatabaseConnexion con;
                con = CSc2iDataServer.GetInstance().GetDatabaseConnexion(IdSession, typeObjets);
                string strWhere = structure.ChampsId[0].NomChamp + " in (" + blIds + ")";
                strWhere = COracleDatabaseConnexion.PasseLaLimiteDesMilleIn(strWhere);
                string strNomTableInDb = CContexteDonnee.GetNomTableInDbForNomTable(strNomTable);
                result = con.ExecuteScalar("delete from " + strNomTableInDb + " where " + strWhere);
                if (!result)
                    return result;
            }
            return result;

        }

        //------------------------------------------------------
        private CResultAErreur GetIdsFilles(
            Type typeFils,
            CFiltreData filtre,
            out int[] lstIdsSortie)
        {
            lstIdsSortie = new int[0];
            CResultAErreur result = CResultAErreur.True;
            result = TestTypePurgeable(typeFils);
            if (!result)
                return result;
            CStructureTable structureFille = CStructureTable.GetStructure(typeFils);
            //Sélectionne tous les éléments
            C2iRequeteAvancee requete = new C2iRequeteAvancee(-1);
            requete.TableInterrogee = structureFille.NomTable;
            string strChampIdFille = structureFille.ChampsId[0].NomChamp;
            requete.ListeChamps.Add(new C2iChampDeRequete(
                strChampIdFille,
                new CSourceDeChampDeRequete(strChampIdFille),
                typeof(int),
                OperationsAgregation.None,
                false));
            requete.FiltreAAppliquer = filtre;
            result = requete.ExecuteRequete(IdSession);
            if (!result)
                return result;
            DataTable table = result.Data as DataTable;
            List<int> lstIdsFille = new List<int>();
            foreach (DataRow row in table.Rows)
            {
                lstIdsFille.Add((int)row[strChampIdFille]);
            }
            lstIdsSortie = lstIdsFille.ToArray();
            return result;
        }
        
       
    }
}
