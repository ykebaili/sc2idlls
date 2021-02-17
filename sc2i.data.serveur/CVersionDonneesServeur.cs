using System;
using System.Collections.Generic;
using System.Text;
using sc2i.common;
using sc2i.data;
using System.Collections;
using System.Data;
using sc2i.multitiers.client;


namespace sc2i.data.serveur
{
	public class CVersionDonneesServeur : CObjetServeur, IVersionDonneesServeur
	{
		public CVersionDonneesServeur(int nIdSession)
			: base(nIdSession)
		{

		}

		//------------------------------------------------
		/// <summary>
		/// Pas de journalisation de ces données
		/// </summary>
		public override IJournaliseurDonneesObjet JournaliseurChamps
		{
			get
			{
				return null;
			}
		}

		//------------------------------------------------
		/// <summary>
		/// Travail toujours directement dans la base !
		/// </summary>
		public override int? IdVersionDeTravail
		{
			get
			{
				return null;
			}
			set
			{
			}
		}


		public override string GetNomTable()
		{
			return CVersionDonnees.c_nomTable;
		}

		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			return CResultAErreur.True;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CVersionDonnees);
		}

        public CResultAErreur AppliqueModificationsPrevisionnelles(int nVersion)
        {
            return AppliqueModificationsPrevisionnelles(nVersion, null);
        }

        public CResultAErreur AppliqueModificationsPrevisionnelles(int nIdVersion, IIndicateurProgression indicateur)
        {
            CResultAErreur result = CResultAErreur.True;
            using (C2iSponsor sponsor = new C2iSponsor())
            {
                if (indicateur != null)
                    sponsor.Register(indicateur);
                CConteneurIndicateurProgression cIndicateur = CConteneurIndicateurProgression.GetConteneur(indicateur);

                CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
                if (session == null)
                {
                    result.EmpileErreur(I.T("Session error|224"));
                    return result;
                }
                try
                {
                    cIndicateur.SetBornesSegment(0, 100);
                    session.BeginTrans();
                    //Déclenchement des évenements avant application
                    using (CContexteDonnee contextePourEvent = new CContexteDonnee(IdSession, true, false))
                    {
                        CVersionDonnees version = new CVersionDonnees(contextePourEvent);
                        if (!version.ReadIfExists(nIdVersion))
                        {
                            result.EmpileErreur(I.T("Data version @1 doesn't exists|222", nIdVersion.ToString()));
                            return result;
                        }
                        result = version.EnregistreEvenement(CVersionDonnees.c_eventBeforeUtiliser, true);
                        if (!result)
                            return result;
                        result = version.EnregistreEvenement(CVersionDonnees.c_eventBeforeAppliquer, true);

                        if (!result)
                            return result;
                    }
                    cIndicateur.SetValue(2);

                    using (CContexteDonnee contextePourVersion = new CContexteDonnee(IdSession, true, false))
                    {
                        contextePourVersion.SetVersionDeTravail(-1, false);
                        CVersionDonnees version = new CVersionDonnees(contextePourVersion);
                        if (!version.ReadIfExists(nIdVersion))
                        {
                            result.EmpileErreur(I.T("Data version @1 doesn't exists|222", nIdVersion.ToString()));
                            return result;
                        }
                        CVersionDonnees versionArchive = new CVersionDonnees(contextePourVersion);
                        versionArchive.CreateNewInCurrentContexte();
                        versionArchive.Libelle = I.T("Apply version @1|223", version.Libelle);
                        if (session.GetInfoUtilisateur() != null)
                            versionArchive.DbKeyUtilisateur = session.GetInfoUtilisateur().KeyUtilisateur;
                        versionArchive.Date = DateTime.Now;
                        versionArchive.CodeTypeVersion = (int)CTypeVersion.TypeVersion.Archive;
                        //Sauve la version archive pour qu'elle ait un id valide
                        CContexteSauvegardeObjetsDonnees ctxSauvegarde = new CContexteSauvegardeObjetsDonnees(contextePourVersion, new CDonneeNotificationModificationContexteDonnee(IdSession), new CListeRestrictionsUtilisateurSurType());
                        result = SaveAll(ctxSauvegarde, DataRowState.Added);

                        CVersionDonnees versionDestination = version.VersionParente;
                        if ( versionDestination != null )
                            versionDestination.VersionObjetsEnLectureNonProgressive.AssureLectureFaite();//Assure la lecture

                        /*if (version.VersionParente != null)
                        {
                            result.EmpileErreur(I.T("You should apply modification for version @1 before applying modification for version @2|10000",
                                version.VersionParente.Libelle,
                                version.Libelle));
                            return result;
                        }*/
                        int nNbTotal = 0;
                        //Charge toutes les tables nécéssaires dans le contexte
                        CListeObjetsDonnees listeCreations = version.VersionsObjets;
                        foreach (CVersionDonneesObjet versionObjet in listeCreations)
                            contextePourVersion.GetTableSafe(CContexteDonnee.GetNomTableForType(versionObjet.TypeElement));

                        nNbTotal = listeCreations.Count;

                        contextePourVersion.BeginModeDeconnecte();

                        ArrayList listeTables = CContexteDonnee.GetTablesOrderInsert(contextePourVersion);

                        List<IDonneeNotification> listeNotifications = new List<IDonneeNotification>();
                        CDonneeNotificationModificationContexteDonnee notifModif = new CDonneeNotificationModificationContexteDonnee(IdSession);
                        listeNotifications.Add(notifModif);
                        //Créations et modifications;
                        cIndicateur.PushSegment(2, 80);
                        cIndicateur.SetBornesSegment(0, nNbTotal);

                        double fNbTraites = 0;

                        foreach (DataTable table in listeTables)
                        {
                            Dictionary<DataRow, bool> dicModifs = new Dictionary<DataRow, bool>();
                            Type tp = CContexteDonnee.GetTypeForTable(table.TableName);
                            if (typeof(IObjetSansVersion).IsAssignableFrom(tp))
                                continue;
                            if (table.PrimaryKey.Length != 1)
                                continue;

                            string strPrimaryKey = table.PrimaryKey[0].ColumnName;

                            CObjetServeur serveur = (CObjetServeur)contextePourVersion.GetTableLoader(table.TableName);
                            IJournaliseurDonneesObjet journaliseur = versionDestination == null ? serveur.JournaliseurChamps : null;

                            cIndicateur.SetInfo(DynamicClassAttribute.GetNomConvivial(tp));


                            //Commence par la création
                            listeCreations.Filtre = new CFiltreData(
                                CVersionDonneesObjet.c_champTypeOperation + "=@1 and " +
                                CVersionDonneesObjet.c_champTypeElement + "=@2",
                                (int)CTypeOperationSurObjet.TypeOperation.Ajout,
                                tp.ToString());
                            foreach (CVersionDonneesObjet versionObjet in listeCreations.ToArrayList())
                            {
                                //Transforme tous les objets en objets normaux
                                CObjetDonneeAIdNumerique objet = versionObjet.Element;
                                if (versionDestination == null)
                                {
                                    objet.Row[CSc2iDataConst.c_champIdVersion] = DBNull.Value;
                                    objet.Row[CSc2iDataConst.c_champOriginalId] = DBNull.Value;
                                }
                                else
                                {
                                    objet.Row[CSc2iDataConst.c_champIdVersion] = versionDestination.Id;
                                    versionObjet.VersionDonnees = versionDestination;
                                }

                                CDonneeNotificationAjoutEnregistrement notif = new CDonneeNotificationAjoutEnregistrement(IdSession, objet.Table.TableName);
                                listeNotifications.Add(notif);

                                //Crée la version de création
                                if (journaliseur != null && journaliseur.IsVersionObjetLinkToElement)
                                {
                                    CVersionDonneesObjet versionCreation = versionArchive.GetVersionObjetAvecCreation(objet.Row.Row);
                                    if (versionCreation != null)
                                        versionCreation.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Ajout;
                                }
                                fNbTraites++;
                                if (fNbTraites % 10 == 0)
                                {
                                    cIndicateur.SetValue((int)fNbTraites);
                                    if (cIndicateur.CancelRequest)
                                    {
                                        result.EmpileErreur(I.T("User cancel|20007"));
                                        return result;
                                    }
                                }

                            }


                            //Modifications
                            CListeObjetsDonnees listeModifications = version.VersionObjetsEnLectureNonProgressive;
                            listeModifications.Filtre = new CFiltreData(
                                CVersionDonneesObjet.c_champTypeOperation + "=@1 and " +
                                CVersionDonneesObjet.c_champTypeElement + "=@2",
                                (int)CTypeOperationSurObjet.TypeOperation.Modification,
                                tp.ToString());
                            int nModif = 0;
                            int nNbToDelete = 0;
                            StringBuilder blIdsToDelete = new StringBuilder();
                            foreach (CVersionDonneesObjet versionObjet in listeModifications)
                            {
                                CObjetDonneeAIdNumerique objetDest;
                                bool bAppliquerModifications = true;
                                if (versionDestination == null)
                                    objetDest = versionObjet.Element;
                                else
                                {
                                    objetDest = Activator.CreateInstance(versionObjet.TypeElement, new object[] { contextePourVersion }) as CObjetDonneeAIdNumerique;

                                    if (!objetDest.ReadIfExists(
                                        new CFiltreData(
                                            "("+CSc2iDataConst.c_champOriginalId + "=@1 or " +
                                            strPrimaryKey+"=@1) and "+
                                        CSc2iDataConst.c_champIdVersion + "=@2",
                                        versionObjet.IdElement,
                                        versionDestination.Id)))//L'objet n'existe pas dans la version de destination
                                    {
                                        if (objetDest.ReadIfExists(new CFiltreData(
                                            CSc2iDataConst.c_champOriginalId + "=@1 and " +
                                        CSc2iDataConst.c_champIdVersion + "=@2",
                                        versionObjet.IdElement,
                                        version.Id)))
                                        {
                                            //Change simplement la version de l'objet dest
                                            objetDest.Row[CSc2iDataConst.c_champIdVersion] = versionDestination.Id;
                                            bAppliquerModifications = false;
                                        }
                                    }
                                }
                                if (objetDest != null)
                                    dicModifs[objetDest.Row.Row] = true;
                                if (bAppliquerModifications)
                                {
                                    result = versionObjet.AppliqueModifications(objetDest);
                                    if (!result)
                                        return result;
                                    //Supprime l'objet
                                    CObjetDonneeAIdNumeriqueAuto objetTmp = (CObjetDonneeAIdNumeriqueAuto)Activator.CreateInstance(objetDest.GetType(), new object[] { contextePourVersion });
                                    CFiltreData filtre = new CFiltreData(
                                        CSc2iDataConst.c_champOriginalId + "=@1 and " +
                                        CSc2iDataConst.c_champIdVersion + "=@2",
                                        versionObjet.IdElement,
                                        version.Id);
                                    filtre.IgnorerVersionDeContexte = true;
                                    if (objetTmp.ReadIfExists(filtre))
                                    {
                                        blIdsToDelete.Append(objetTmp.Id);
                                        blIdsToDelete.Append(',');
                                        nNbToDelete++;
                                    }
                                    nModif++;
                                    if (nNbToDelete > 300 || (nNbToDelete > 0 && nModif == listeModifications.Count))
                                    {
                                        CListeObjetsDonnees lstToDelete = new CListeObjetsDonnees(contextePourVersion, objetTmp.GetType());
                                        blIdsToDelete.Remove(blIdsToDelete.Length - 1, 1);
                                        lstToDelete.Filtre = new CFiltreData(table.PrimaryKey[0].ColumnName + " in (" + blIdsToDelete.ToString() + ")");
                                        lstToDelete.Filtre.IgnorerVersionDeContexte = true;
                                        lstToDelete.InterditLectureInDB = true;
                                        result = CObjetDonneeAIdNumeriqueAuto.Delete(lstToDelete);
                                        if (!result)
                                            return result;
                                        blIdsToDelete = new StringBuilder();
                                        nNbToDelete = 0;
                                    }
                                    if (versionDestination != null)
                                    {
                                        //Cherche la version objet dans la version destination
                                        CVersionDonneesObjet vo = new CVersionDonneesObjet(contextePourVersion);
                                        if (vo.ReadIfExists(new CFiltreData(
                                            CVersionDonnees.c_champId + "=@1 and " +
                                            CVersionDonneesObjet.c_champIdElement + "=@2 and " +
                                            CVersionDonneesObjet.c_champTypeElement + "=@3",
                                            versionDestination.Id,
                                            versionObjet.IdElement,
                                            versionObjet.StringTypeElement)))
                                        {//L'objet existe,
                                            //Il faut lui associer les modifications
                                            if (vo.TypeOperation.Code == CTypeOperationSurObjet.TypeOperation.Modification)
                                            {
                                                //Si l'objet est créé dans cette version, on ne gère pas
                                                //les opérations.
                                                CListeObjetsDonnees lst = vo.ToutesLesOperations;
                                                foreach (CVersionDonneesObjetOperation op in versionObjet.ToutesLesOperations.ToArrayList())
                                                {
                                                    lst.Filtre = new CFiltreData(CVersionDonneesObjetOperation.c_champChamp + "=@1 and " +
                                                        CVersionDonneesObjetOperation.c_champTypeChamp + "=@2",
                                                        op.FieldKey,
                                                        op.TypeChamp);
                                                    if (lst.Count == 1)
                                                    {
                                                        CVersionDonneesObjetOperation oper = lst[0] as CVersionDonneesObjetOperation;
                                                        oper.ValeurBlob = op.ValeurBlob;
                                                        oper.ValeurString = op.ValeurString;
                                                        oper.TypeValeur = op.TypeValeur;
                                                        oper.TypeOperation = op.TypeOperation;
                                                        oper.TimeStamp = op.TimeStamp;
                                                    }
                                                    else
                                                        op.VersionObjet = vo;
                                                }
                                            }
                                        }
                                        else
                                            versionObjet.VersionDonnees = versionDestination;
                                    }
                                }
                                else
                                {
                                    versionObjet.VersionDonnees = versionDestination;
                                }


                                fNbTraites += .5;

                                if (fNbTraites % 10 == 0)
                                {
                                    cIndicateur.SetValue((int)fNbTraites);
                                    if (cIndicateur.CancelRequest)
                                    {
                                        result.EmpileErreur(I.T("User cancel|20007"));
                                        return result;
                                    }
                                }
                            }

                            cIndicateur.SetInfo(I.T("Archiving...|20008"));
                            if (journaliseur != null)
                                foreach (DataRow rowArchive in dicModifs.Keys)
                                {
                                    fNbTraites += .5;
                                    if (fNbTraites % 10 == 0)
                                    {
                                        cIndicateur.SetValue((int)fNbTraites);
                                        if (cIndicateur.CancelRequest)
                                        {
                                            result.EmpileErreur(I.T("User cancel|20007"));
                                            return result;
                                        }
                                    }
                                    journaliseur.JournaliseDonnees(rowArchive, versionArchive);
                                }

                        }
                        //Suppressions
                        cIndicateur.SetInfo(I.T("Removing datas|20009"));
                        listeTables.Reverse();
                        foreach (DataTable table in listeTables)
                        {
                            Type tp = CContexteDonnee.GetTypeForTable(table.TableName);

                            CListeObjetsDonnees listeSuppressions = version.VersionObjetsEnLectureNonProgressive;
                            listeSuppressions.Filtre = new CFiltreData(
                                CVersionDonneesObjet.c_champTypeOperation + "=@1 and " +
                                CVersionDonneesObjet.c_champTypeElement + "=@2",
                                (int)CTypeOperationSurObjet.TypeOperation.Suppression,
                                tp.ToString());
                            CObjetServeur serveur = (CObjetServeur)contextePourVersion.GetTableLoader(table.TableName);
                            if (serveur != null)
                            {
                                IJournaliseurDonneesObjet journaliseur = serveur.JournaliseurChamps;
                                foreach (CVersionDonneesObjet versionObjet in listeSuppressions)
                                {
                                    if (versionDestination == null)
                                    {
                                        //Supprime effectivement l'objet
                                        CObjetDonneeAIdNumerique objet = versionObjet.Element;
                                        if (objet != null)
                                        {
                                            result = objet.CanDelete();
                                            if (!result)
                                                return result;
                                            objet.Row[CSc2iDataConst.c_champOriginalId] = DBNull.Value;
                                            objet.Row[CSc2iDataConst.c_champIdVersion] = versionArchive.Id;
                                            objet.Row[CSc2iDataConst.c_champIsDeleted] = true;
                                            if (journaliseur != null && journaliseur.IsVersionObjetLinkToElement)
                                            {
                                                CVersionDonneesObjet versionDelete = versionArchive.GetVersionObjetAvecCreation(objet.Row);
                                                if (versionDelete != null)
                                                    versionDelete.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Suppression;
                                            }
                                            notifModif.AddModifiedRecord(objet.Row.Table.TableName, true, new object[] { objet.Id });
                                            fNbTraites++;
                                            if (fNbTraites % 10 == 0)
                                            {
                                                cIndicateur.SetValue((int)fNbTraites);
                                                if (cIndicateur.CancelRequest)
                                                {
                                                    result.EmpileErreur(I.T("User cancel|20007"));
                                                    return result;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Cherche dans la version dest s'il y a des choses sur cet objet
                                        CVersionDonneesObjet vo = new CVersionDonneesObjet(contextePourVersion);
                                        if (vo.ReadIfExists(
                                            new CFiltreData(
                                                CVersionDonnees.c_champId + "=@1 and " +
                                                CVersionDonneesObjet.c_champIdElement + "=@2 and " +
                                                CVersionDonneesObjet.c_champTypeElement + "=@3",
                                                versionDestination.Id,
                                                versionObjet.IdElement,
                                                versionObjet.StringTypeElement)))
                                        {
                                            vo.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Suppression;
                                        }
                                        else
                                            versionObjet.VersionDonnees = versionDestination;
                                    }
                                }
                            }
                            if (cIndicateur.CancelRequest)
                            {
                                result.EmpileErreur(I.T("User cancel|20007"));
                                return result;
                            }

                        }
                        cIndicateur.PopSegment();
                        cIndicateur.SetValue(80);
                        //Détache toutes les versions filles
                        foreach (CVersionDonnees versionFille in version.VersionsFilles.ToArrayList())
                            versionFille.VersionParente = versionDestination;

                        if (result)
                        {
                            //Passe la version en archive, car c'est long de la supprimer
                            result = CObjetDonneeAIdNumeriqueAuto.Delete(version.VersionObjetsEnLectureNonProgressive);
                            version.CodeTypeVersion = (int)CTypeVersion.TypeVersion.Etiquette;
                            version.VersionParente = null;
                        }
                        cIndicateur.SetInfo(I.T("Saving|20010"));
                        contextePourVersion.EndModeDeconnecteSansSauvegardeEtSansReject();
                        if (!result)
                            return result;
                        result = contextePourVersion.SaveAll(true);
                        if (result)
                            CEnvoyeurNotification.EnvoieNotifications(listeNotifications.ToArray());
                        cIndicateur.SetValue(100);
                    }
                }
                catch (Exception e)
                {
                    result.EmpileErreur(new CErreurException(e));
                }
                finally
                {
                    if (!result)
                        session.RollbackTrans();
                    else
                        result = session.CommitTrans();
                }
            }
            return result;
        }

		//----------------------------------------------------------------------------
		public CResultAErreur AnnulerModificationsPrevisionnelles(int nIdVersion)
		{
			CResultAErreur result = CResultAErreur.True;
			using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
			{
				contexte.SetVersionDeTravail( -1, false );//Travaille hors versions
				contexte.EnforceConstraints = false;//Pas de contrôle des contraintes
				CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
				result = session.BeginTrans();
				try
				{
					CVersionDonnees version = new CVersionDonnees(contexte);
					if (!version.ReadIfExists(nIdVersion))
					{
						result.EmpileErreur(I.T("Data version @1 doesn't exist|218",nIdVersion.ToString()));
						return result;
					}
					if (version.TypeVersion.Code != CTypeVersion.TypeVersion.Previsionnelle)
					{
						result.EmpileErreur(I.T("Cannot cancel archive operation|220"));
						return result;
					}
					result = version.EnregistreEvenement(CVersionDonnees.c_eventBeforeUtiliser, true);
					if (!result)
						return result;
					CListeObjetsDonnees listeObjetsDeLaVersion = version.VersionsObjets;

					//Supprime toutes les entités à supprimer
					//Charge dans le contexte de donnees, toutes les entités liées à la version
					Dictionary<Type, List<int>> listeIdsALireParTable = new Dictionary<Type,List<int>>();
					CListeObjetsDonnees lstVersionsObjet = version.VersionsObjets;
					lstVersionsObjet.RemplissageProgressif = false;
					lstVersionsObjet.AssureLectureFaite();
					foreach (CVersionDonneesObjet versionObjet in lstVersionsObjet)
					{
						List<int> lstIds = null;
						if (!listeIdsALireParTable.TryGetValue(versionObjet.TypeElement, out lstIds))
						{
							lstIds = new List<int>();
							listeIdsALireParTable[versionObjet.TypeElement] = lstIds;
						}
						lstIds.Add(versionObjet.IdElement);
					}
					foreach ( KeyValuePair<Type, List<int>> entry in listeIdsALireParTable )
					{
						Type typeASupprimer = entry.Key;
						List<int> lstIds = entry.Value;
						int nTailleBloc = 100;
						DataTable table = contexte.GetTableSafe ( CContexteDonnee.GetNomTableForType ( typeASupprimer ) );
						string strPrimKey = table.PrimaryKey[0].ColumnName;
						//Lecture des éléments par 100
						for ( int nBloc = 0; nBloc < lstIds.Count; nBloc += nTailleBloc )
						{
							int nMin = Math.Min ( nBloc+nTailleBloc, lstIds.Count );
							StringBuilder builder = new StringBuilder();
							for ( int nPosId = nBloc; nPosId < nMin; nPosId++ )
							{
								builder.Append ( lstIds[nPosId] );
								builder.Append ( ',' );
							}
							if (builder.Length > 0)
								builder.Remove(builder.Length - 1, 1);

							//On doit passer en modifié tous les originaux, donc, ceux qui ont
							//un id égal aux ids modifiés et qui n'appartiennent pas à cette version
							CListeObjetsDonnees lstToModify = new CListeObjetsDonnees ( contexte, typeASupprimer );
							lstToModify.Filtre = new CFiltreData( 
								strPrimKey+" in ("+builder.ToString()+") and "+
								"("+CSc2iDataConst.c_champIdVersion+"<>@1 or "+ 
								CSc2iDataConst.c_champIdVersion+" is null)",
								version.Id);
							lstToModify.Filtre.IgnorerVersionDeContexte = true;
							lstToModify.AssureLectureFaite();
							ArrayList lstTmp = new ArrayList(lstToModify);
							foreach (CObjetDonnee objet in lstTmp)
							{
								if (objet.Row.RowState == DataRowState.Unchanged)
									objet.Row.Row.SetModified();
							}
						}
						//On doit supprimer tous les éléments liés à cette version, on se fiche des Ids !
						CListeObjetsDonnees lstToDelete = new CListeObjetsDonnees(contexte, typeASupprimer);
						lstToDelete.Filtre = new CFiltreData(
							CSc2iDataConst.c_champIdVersion + "=@1",
								version.Id);
						lstToDelete.Filtre.IgnorerVersionDeContexte = true;
						lstToDelete.AssureLectureFaite();
						ArrayList lstDelete = new ArrayList(lstToDelete);
						foreach (CObjetDonnee objet in lstDelete)
							objet.Row.Row.Delete();
					}

					//Suppression des CVOO
					CListeObjetsDonnees lstCVO = new CListeObjetsDonnees(contexte, typeof(CVersionDonneesObjetOperation));
					lstCVO.Filtre = new CFiltreDataAvance(CVersionDonneesObjetOperation.c_nomTable,
						CVersionDonneesObjet.c_nomTable + "." +
						CVersionDonnees.c_champId + "=@1",
						version.Id);
					ArrayList lst = new ArrayList(lstCVO);
					foreach (CVersionDonneesObjetOperation voo in lst)
						voo.Row.Row.Delete();

					//Suppression des CVO
					lst = new ArrayList(lstVersionsObjet);
					
					foreach ( CVersionDonneesObjet vo in lst )
						vo.Row.Row.Delete();
					contexte.EnforceConstraints = true;
					result = contexte.SaveAll(false);
				}
				catch (Exception e)
				{
					result.EmpileErreur(new CErreurException(e));
				}
				finally
				{
					if (result)
						result = session.CommitTrans();
					else
						session.RollbackTrans();
				}
			}
			return result;
		}


		//--------------------------------------------------------------------------
		public CResultAErreur CreatePrevisionnelleFromArchive(int nIdArchive)
		{
			CResultAErreur result = CResultAErreur.True;
			using (CContexteDonnee contexte = new CContexteDonnee(IdSession, true, false))
			{
				CVersionDonnees versionArchive = new CVersionDonnees(contexte);
				if (!versionArchive.ReadIfExists(nIdArchive))
				{
					result.EmpileErreur(I.T("Data version @1 doesn't exists|20000", nIdArchive.ToString()));
					return result;
				}
				if (versionArchive.TypeVersion.Code != CTypeVersion.TypeVersion.Etiquette &&
					versionArchive.TypeVersion.Code != CTypeVersion.TypeVersion.Archive)
				{
					result.EmpileErreur(I.T("Cannot create a planified version from that version|20001"));
					return result;
				}
				//Création de la nouvelle version
				CVersionDonnees versionPrev = new CVersionDonnees(contexte);
				versionPrev.CreateNew();
				versionPrev.CodeTypeVersion = (int)CTypeVersion.TypeVersion.Previsionnelle;
				versionPrev.Libelle = I.T("From @1|20002", versionArchive.Libelle);
				result = versionPrev.CommitEdit();
				if (!result)
				{
					result.EmpileErreur(I.T("Error while creating version|20003"));
					return result;
				}
				//Trouve tous les objets modifiés depuis cette version
				CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, typeof(CVersionDonneesObjet));
				liste.Filtre = new CFiltreDataAvance(
					CVersionDonneesObjet.c_nomTable,
					CVersionDonnees.c_nomTable + "." +
					CVersionDonnees.c_champTypeVersion + "=@1 and " +
					CVersionDonnees.c_champId + ">=@2",
					(int)CTypeVersion.TypeVersion.Archive,
					versionArchive.Id);
				liste.Tri = CVersionDonneesObjet.c_champId + " desc";//De la modif la plus récente à la plus ancienne
				using (CContexteDonnee contextePrev = new CContexteDonnee(IdSession, true, false))
				{
					result = versionPrev.EnregistreEvenement(CVersionDonnees.c_eventBeforeUtiliser, true);
					if (!result)
						return result;
                    contextePrev.SetVersionDeTravail(versionPrev.Id, false);
                    CContexteDonnee contexteEdition = contextePrev.GetContexteEdition();
					
					Dictionary<Type, Dictionary<int, int>> mappageSupprimesToNew = new Dictionary<Type, Dictionary<int, int>>();
					
					foreach (CVersionDonneesObjet versionObjet in liste)
					{
						CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(versionObjet.TypeElement, new object[] { contexteEdition });
						CFiltreData filtre = new CFiltreData(objet.GetChampId() + "=@1", versionObjet.IdElement);
						filtre.IntegrerLesElementsSupprimes = true;
						filtre.IgnorerVersionDeContexte = true;
						if (objet.ReadIfExists(filtre))
						{
							switch (versionObjet.TypeOperation.Code)
							{
								case CTypeOperationSurObjet.TypeOperation.Ajout:
									//L'élément doit être suppimé
									//Charge l'objet
									{
										result = objet.Delete();
										if (!result)
											return result;
									}
									break;
								case CTypeOperationSurObjet.TypeOperation.Suppression:
									//Chargement de l'élément supprimé
                                    CObjetDonneeAIdNumeriqueAuto newObjet = Activator.CreateInstance(objet.GetType(), new object[] { contexteEdition }) as CObjetDonneeAIdNumeriqueAuto;
                                    if (newObjet == null)
                                    {
                                        result.EmpileErreur(I.T("Cannot instanciate an object of Type @1 because it hasn't autoId|20005", DynamicClassAttribute.GetNomConvivial(objet.GetType())));
                                        return result;
                                    }
									newObjet.CreateNewInCurrentContexte();
									contextePrev.CopyRow(objet.Row, newObjet.Row.Row, CSc2iDataConst.c_champIsDeleted, newObjet.GetChampId());
									Dictionary<int, int> mapIds = null;
									if (!mappageSupprimesToNew.TryGetValue(newObjet.GetType(), out mapIds))
									{
										mapIds = new Dictionary<int, int>();
										mappageSupprimesToNew[newObjet.GetType()] = mapIds;
									}
									mapIds[objet.Id] = newObjet.Id;
									break;
								case CTypeOperationSurObjet.TypeOperation.Modification:
									bool bOldEnforce = contextePrev.EnforceConstraints;
									contextePrev.EnforceConstraints = false;
									foreach (CVersionDonneesObjetOperation operation in versionObjet.Modifications)
									{
										IChampPourVersion champ = operation.Champ;
										IJournaliseurDonneesChamp journaliseur = CGestionnaireAChampPourVersion.GetJournaliseur(champ.TypeChampString);
										journaliseur.AppliqueValeur(versionObjet.VersionDonnees.Id, champ, objet, operation.GetValeur());
									}
									//Si l'objet pointe sur une dépendance restaurée, récupère l'id du nouvel élément
									foreach ( CInfoRelation relation in CContexteDonnee.GetListeRelationsTable ( objet.GetNomTable() ))
									{
										if ( relation.TableFille == objet.GetNomTable() )
										{
											Type tpParent = CContexteDonnee.GetTypeForTable ( relation.TableParente );
											Dictionary<int, int> mapsIds = null;
											if ( mappageSupprimesToNew.TryGetValue ( tpParent, out mapsIds ) )
											{
												object val = objet.Row[relation.ChampsFille[0]];
												if ( val != DBNull.Value )
												{
													if ( mapsIds.ContainsKey ( (int)val ) )
													{
													objet.Row[relation.ChampsFille[0]] = mapsIds[(int)val];
													}
												}
											}
										}
									}
									contextePrev.EnforceConstraints = bOldEnforce;
									break;
							}
						}
					}
					result = contexteEdition.CommitEdit();
				}
				result.Data = versionPrev.Id;
				return result;
			}
		}

		//--------------------------------------------------------------------------
		public CResultAErreur PurgerHistorique(DateTime dt)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				using ( CContexteDonnee contexte = new CContexteDonnee ( IdSession, true, false ) )
				{
                    contexte.EnableTraitementsAvantSauvegarde = false;
                    contexte.EnableTraitementsExternes = false;
					contexte.SetVersionDeTravail ( -1, false );//Travaille hors version
					contexte.EnforceConstraints = false;
					contexte.BeginModeDeconnecte();
					//Récupère toutes les version de données antérieures à la date demandée
					CListeObjetsDonnees listeVersions = new CListeObjetsDonnees ( contexte, typeof(CVersionDonnees ) );
					listeVersions.Filtre = new CFiltreData ( CVersionDonnees.c_champDate+"<@1 and "+
						"("+CVersionDonnees.c_champTypeVersion+"=@2 or "+
						CVersionDonnees.c_champTypeVersion+"=@3)",
						dt,
						(int)CTypeVersion.TypeVersion.Archive,
						(int)CTypeVersion.TypeVersion.Etiquette );
					
					//Lecture des versionsObjet
					listeVersions.ReadDependances ( "VersionsObjets" );
					CListeObjetsDonnees listeSuppressions = new CListeObjetsDonnees(contexte, typeof(CVersionDonneesObjet));
					listeSuppressions.Filtre = new CFiltreData (  CVersionDonneesObjet.c_champTypeOperation+"=@1",
						(int)CTypeOperationSurObjet.TypeOperation.Suppression );
					listeSuppressions.InterditLectureInDB = true;//Les éléments sont déjà lus par le ReadDependances au dessus
					Dictionary<Type, List<int>> tableTypeToIdToDelete = new Dictionary<Type, List<int>>();
					foreach ( CVersionDonneesObjet suppression in listeSuppressions )
					{
						//Récupère l'élément supprimé
						Type typeElement = suppression.TypeElement;
						List<int> lstForType = null;
						if (!tableTypeToIdToDelete.TryGetValue(typeElement, out lstForType))
						{
							lstForType = new List<int>();
							tableTypeToIdToDelete[typeElement] = lstForType;
						}
						lstForType.Add( suppression.IdElement );
					}
					foreach (KeyValuePair<Type, List<int>> kv in tableTypeToIdToDelete)
					{
						contexte.GetTableSafe(CContexteDonnee.GetNomTableForType(kv.Key));
					}
					ArrayList lst = contexte.GetTablesOrderDelete();
					foreach (DataTable table in lst)
					{
						string strNomTable = table.TableName;
						Type tp = CContexteDonnee.GetTypeForTable(strNomTable);
                        if (tp != null && tableTypeToIdToDelete.ContainsKey(tp))
                        {
                            DataTable laTable = contexte.GetTableSafe(CContexteDonnee.GetNomTableForType(tp));
                            List<int> lstForType = tableTypeToIdToDelete[tp]; ;
                            int nTailleBloc = 500;
                            int nCount = lstForType.Count;
                            for (int nBloc = 0; nBloc < nCount; nBloc += nTailleBloc)
                            {
                                int nMin = Math.Min(nBloc + nTailleBloc, nCount);
                                StringBuilder bl = new StringBuilder();
                                for (int nElement = nBloc; nElement < nMin; nElement++)
                                {
                                    bl.Append(lstForType[nElement]);
                                    bl.Append(',');
                                }
                                if (bl.Length > 0)
                                {
                                    bl.Remove(bl.Length - 1, 1);
                                    CListeObjetsDonnees listeObjetsASupprimer = new CListeObjetsDonnees(contexte, tp);
                                    listeObjetsASupprimer.Filtre = new CFiltreData(laTable.PrimaryKey[0].ColumnName + " in (" +
                                        bl.ToString() + ")");
                                    listeObjetsASupprimer.Filtre.IntegrerLesElementsSupprimes = true;
                                    listeObjetsASupprimer.Filtre.IgnorerVersionDeContexte = true;
                                    result = CObjetDonneeAIdNumeriqueAuto.Delete(listeObjetsASupprimer);
                                    if (!result)
                                        return result;
                                }
                            }
                        }
					}

					//On se débrouille pour que le contexte de données contienne les version, versionObjet et VersionObjetData à supprimer
					CListeObjetsDonnees  listeVersionObjets = new CListeObjetsDonnees ( contexte, typeof(CVersionDonneesObjet ) );
					listeVersionObjets.InterditLectureInDB = true;
					listeVersionObjets.ReadDependances ( "ToutesLesOperations");

					ArrayList lstRows = null;
					if ( contexte.Tables[CVersionDonneesObjetOperation.c_nomTable] !=null )
					{
						lstRows = new ArrayList ( contexte.Tables[CVersionDonneesObjetOperation.c_nomTable].Rows );
						foreach ( DataRow row in lstRows )
							row.Delete();
					}
					if (contexte.Tables[CVersionDonneesObjet.c_nomTable] != null)
					{
						lstRows = new ArrayList(contexte.Tables[CVersionDonneesObjet.c_nomTable].Rows);
						foreach (DataRow row in lstRows)
							row.Delete();
					}
					result = CObjetDonneeAIdNumerique.Delete(listeVersions);
					if (!result)
						return result;
					contexte.EnforceConstraints = true;
					result = contexte.CommitModifsDeconnecte();
					if ( !result )
						return result;
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				return result;
			}
			return result;
		}
	}
}
