using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Text;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.multitiers.client;
using sc2i.multitiers.server;
using System.Runtime.Serialization;

namespace sc2i.data.serveur
{
	/// <summary>
	/// Description résumée de CContexteDonneeServeur.
	/// </summary>
	/// 
	//Avant la sauvegarde, appelle cette fonction
	//Les traitements peuvent stocker des données dans la tableData
	//Après la sauvegarde, la fonction est rappellée avec la hashtable
	//Ce qui permet aux appelants de stocker un état entre les deux
	//
	// /!\ CResultAErreur est passé en référence car l'évenement peut être appelé plusieur fois /!\
	public delegate CResultAErreur TraitementSauvegardeExterne(CContexteDonnee contexte, Hashtable tableData);

	public class CContexteDonneeServeur : C2iObjetServeur, IContexteDonneeServeur
	{
		/// <summary>
		/// Indique pour chaque IdSession la version d'archive en cours pour un SaveAll.
		/// Lors de l'appel à save all, le système regarde s'il y a une version d'archive pour la session
		/// si oui, les modifs seront appliquées dans cette version. Si non, une nouvelle version sera créée
		/// </summary>
		//private static Dictionary<int, CVersionDonnees> m_tableSessionToVersionArchiveEnCours = new Dictionary<int, CVersionDonnees>();

		//Id de la version d'archive associée à ce contextedonneeserveur
		private int? m_nIdVersionArchive = null;

		///Version de travail en cours
		private int? m_nIdVersionDeTravail = null;

		/// //////////////////////////////////////////////////////////////////
		public CContexteDonneeServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}


		/////////////////////////////////////////////////////////
        private static List<TraitementSauvegardeExterne> m_listeTraitementsAvantSauvegarde = new List<TraitementSauvegardeExterne>();
        private static List<TraitementSauvegardeExterne> m_listeTraitementsApresSauvegarde = new List<TraitementSauvegardeExterne>();

        /////////////////////////////////////////////////////////
        public static void AddTraitementAvantSauvegarde(TraitementSauvegardeExterne traitement)
        {
            m_listeTraitementsAvantSauvegarde.Add(traitement);
        }

        /////////////////////////////////////////////////////////
        public static void InsertTraitementAvantSauvegarde(TraitementSauvegardeExterne traitement)
        {
            m_listeTraitementsAvantSauvegarde.Insert(0, traitement);
        }

        /////////////////////////////////////////////////////////
        public static void AddTraitementApresSauvegarde(TraitementSauvegardeExterne traitement)
        {
            m_listeTraitementsApresSauvegarde.Add(traitement);
        }

        /////////////////////////////////////////////////////////
        public static void InsertTraitementApresSauvegarde(TraitementSauvegardeExterne traitement)
        {
            m_listeTraitementsApresSauvegarde.Insert(0, traitement);
        }

		//////////////////////////////////////////////////////////
		/// <summary>
		/// Indique la version d'archive associée à ce contexte serveur
		/// </summary>
		public int? IdVersionArchiveAssociee
		{
			get
			{
				return m_nIdVersionArchive;
			}
		}

		/// //////////////////////////////////////////////////////////////////
		///Lorsqu'une table a été traitée avant sauvegarde et qu'elle est modifiée,
		///il faut la retraiter !!
		private void OnChangeRowSurTableTraiteeAvantSauvegarde ( object sender, DataRowChangeEventArgs args )
		{
			if ( !m_listeTablesARetraiterAvantSauvegarde.Contains( args.Row.Table ) &&
				args.Action == DataRowAction.Change )
				m_listeTablesARetraiterAvantSauvegarde.Add ( args.Row.Table );
		}

		/// //////////////////////////////////////////////////////////////////
		/// Lorsqu'une table est ajoutée dans le contexte pendant le traitement avant
		/// sauvegarde, il faut traiter cette table
		private void OnAddTableInContexteDuringTraitementAvantSauvegarde ( object sender, CollectionChangeEventArgs args )
		{
			if ( !m_listeTablesARetraiterAvantSauvegarde.Contains( args.Element ) &&
				args.Action == CollectionChangeAction.Add )
				m_listeTablesARetraiterAvantSauvegarde.Add ( args.Element );
		}

		/// //////////////////////////////////////////////////////////////////
		private CVersionDonnees GetVersionArchive(CContexteDonnee contexte, IInfoUtilisateur infoUtilisateur)
		{
			CVersionDonnees version = null;
			if (IdVersionArchiveAssociee != null)
			{
				version = new CVersionDonnees(contexte);
				if (!version.ReadIfExists((int)m_nIdVersionArchive))
					version = null;
			}
			/*if ( m_tableSessionToVersionArchiveEnCours.ContainsKey(IdSession) )
				version = m_tableSessionToVersionArchiveEnCours[IdSession];*/
			if (version == null)
			{
				StringBuilder builder = new StringBuilder();

				if (infoUtilisateur != null)
				{
					builder.Append(I.T("User : @1|210", infoUtilisateur.NomUtilisateur));
				}
				else
					builder.Append(I.T("Unknown user|211"));
				version = new CVersionDonnees(contexte);
				version.CreateNewInCurrentContexte();
				version.Libelle = builder.ToString();
				version.Date = DateTime.Now;
				version.CodeTypeVersion = (int)CTypeVersion.TypeVersion.Archive;
				version.DbKeyUtilisateur = infoUtilisateur.KeyUtilisateur;
				m_nIdVersionArchive = version.Id;
			}
			return version;
		}


		private ArrayList m_listeTablesARetraiterAvantSauvegarde = null;
		/// //////////////////////////////////////////////////////////////////
		public virtual CResultAErreur SaveAll( 
			CContexteDonnee contexteToSave, 
			bool bShouldTraiteAvantSauvegarde)
		{
			CResultAErreur result = CResultAErreur.True;
			contexteToSave.SetEnableAutoStructure( true );

			m_nIdVersionDeTravail = contexteToSave.IdVersionDeTravail;
			
			DateTime dtTrace = DateTime.Now;
			
			//ATTENTION : ne surtout pas mettre la gestion par tables complètes à vrai
			//car le contexte contient déjà des données des tables qui sont gerées
			//par tables complètes. Donc lorsqu'on y accède, la table est
			//là, donc on considère qu'elle est chargée entierement, mais ce
			//n'est pas le cas car elle ne contient que des données partielles !!!
			//contexteToSave.GestionParTabless = true;

			//Sauvegarde les modifs
			if ( contexteToSave.HasChanges() )
			{
				CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
				//Par la suite, on considère que la session n'est pas nulle
				if ( session == null )
				{
					result.EmpileErreur(I.T("Invalid session|161"));
					return result;
				}
                session.DateHeureDerniereActivite = DateTime.Now;


				result = session.BeginTrans();

				try
				{
                    Hashtable tableData = new Hashtable();
					if ( result && bShouldTraiteAvantSauvegarde)
					{
						bool bHasStartModeDeconnecte = contexteToSave.BeginModeDeconnecte();
						//Fait les traitements avant sauvegarde
						m_listeTablesARetraiterAvantSauvegarde = contexteToSave.GetTablesOrderDelete();
						DataRowChangeEventHandler changeHandler = new DataRowChangeEventHandler ( OnChangeRowSurTableTraiteeAvantSauvegarde );
						contexteToSave.Tables.CollectionChanged += new System.ComponentModel.CollectionChangeEventHandler(OnAddTableInContexteDuringTraitementAvantSauvegarde);
						//Faut bien s'arreter à un moment
						int nLimiteurBoucle = 5;
						contexteToSave.AlwaysPreserveChange = true;
						while ( result && m_listeTablesARetraiterAvantSauvegarde.Count != 0 && nLimiteurBoucle > 0)
						{
							nLimiteurBoucle--;
							ArrayList listeTables = new ArrayList(m_listeTablesARetraiterAvantSauvegarde );
							m_listeTablesARetraiterAvantSauvegarde.Clear();
							foreach ( DataTable table in listeTables )
							{
								if ( table.Rows.Count > 0 )
								{
									table.RowChanged -= changeHandler;
                                    result = CGestionnaireHookTraitementAvantSauvegarde.DoTraitementAvantSauvegarde(contexteToSave, table.TableName, tableData);
                                    if (result)
                                    {
                                        CObjetServeur objServeur = (CObjetServeur)contexteToSave.GetTableLoader(table.TableName);
                                        result = objServeur.TraitementTotalAvantSauvegarde(contexteToSave);
                                    }
									if ( !result )
									{
										result.EmpileErreur(I.T("Error while checking data|162"));
										break;
									}
								}
								table.RowChanged += changeHandler;
							}
						}
						contexteToSave.AlwaysPreserveChange = false;
                        if ( bHasStartModeDeconnecte )
						    contexteToSave.EndModeDeconnecteSansSauvegardeEtSansReject();
					}
					if ( !result )
						return result;

					CDonneeNotificationModificationContexteDonnee donneeNotification = new CDonneeNotificationModificationContexteDonnee(IdSession );
					CListeRestrictionsUtilisateurSurType restrictionsUtilisateur = new CListeRestrictionsUtilisateurSurType();
					
					///Stef 08/04/2008 : supprimé : la session est déjà récuperée plus haut
					//session = CSessionClient.GetSessionForIdSession ( IdSession );

					//La session ne peut pas être null (testé plus haut)
					IInfoUtilisateur infoUtilisateur = session.GetInfoUtilisateur();
					if ( infoUtilisateur != null )
						restrictionsUtilisateur = infoUtilisateur.GetListeRestrictions(contexteToSave.IdVersionDeTravail);
			


					CContexteSauvegardeObjetsDonnees contexteSauvegarde = new CContexteSauvegardeObjetsDonnees ( 
						contexteToSave,
						donneeNotification,
						restrictionsUtilisateur
						);
					
					///Indique si cet appel à SaveAll a créé une version d'archive
                    if (!contexteToSave.DisableHistorisation && CVersionDonneesObjet.EnableJournalisation && contexteToSave.IdVersionDeTravail == null)
					{
						CVersionDonnees version = GetVersionArchive(contexteToSave, infoUtilisateur);
						contexteSauvegarde.VersionDonneesAssociee = version;
					}

					//Sauvegarde les modifs ( ajouts et update )
                    if (result && bShouldTraiteAvantSauvegarde && contexteToSave.EnableTraitementsExternes)
                    {
                        foreach (TraitementSauvegardeExterne traitement in m_listeTraitementsAvantSauvegarde)
                        {
                            result = traitement(contexteToSave, tableData);
                            if (!result)
                                break;
                        }
                    }
                    /*
					if (DoTraitementExterneAvantSauvegarde != null && 
                        bShouldTraiteAvantSauvegarde && 
                        contexteToSave.EnableTraitementsExternes)
						DoTraitementExterneAvantSauvegarde(contexteToSave, tableData, ref result);*/

					if ( result )
						result += MySaveAll( contexteSauvegarde );
					
					//Récupère l'id de la version associée
					if (contexteSauvegarde.VersionDonneesAssociee != null)
						m_nIdVersionArchive = contexteSauvegarde.VersionDonneesAssociee.Id;

                    if (result && bShouldTraiteAvantSauvegarde && contexteToSave.EnableTraitementsExternes)
                    {
                        foreach (TraitementSauvegardeExterne traitement in m_listeTraitementsApresSauvegarde)
                        {
                            result = traitement(contexteToSave, tableData);
                            if (!result)
                                break;
                        }
                    }
					/*if (result && DoTraitementExterneApresSauvegarde != null && 
                        bShouldTraiteAvantSauvegarde && 
                        contexteToSave.EnableTraitementsExternes)
						DoTraitementExterneApresSauvegarde ( contexteToSave, tableData, ref result );*/
					

					if ( result )
					{
						//traitement après sauvegarde
                        ArrayList lstTables = new ArrayList(contexteSauvegarde.ContexteDonnee.Tables);
						foreach ( DataTable table in lstTables )
						{
							if ( table.Rows.Count > 0 )
							{
								CObjetServeur objServeur = (CObjetServeur)contexteToSave.GetTableLoader ( table.TableName );
								result = objServeur.TraitementTotalApresSauvegarde(contexteToSave, result.Result);
								if ( !result )
								{
									result.EmpileErreur(I.T("Error after the data checking|163"));
									break;
								}
							}
						}
					}

					if ( result )
					{
						if ( donneeNotification.ListeModifications.Count != 0 )
							CEnvoyeurNotification.EnvoieNotifications(new IDonneeNotification[] { donneeNotification });
					}
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
					result.EmpileErreur(I.T("Server Error|159"));
                    C2iEventLog.WriteErreur("Save all error\r\n" + result.Erreur.ToString());
				}
				finally
				{
					if ( result )
						result = session.CommitTrans();
					else
						session.RollbackTrans();
				}

				
			}
            //Cherche les éléments non serializable dans les extendedProperties des tables
            foreach (DataTable table in contexteToSave.Tables)
            {
                foreach (DictionaryEntry entry in new ArrayList(table.ExtendedProperties))
                {
                    if (entry.Value != null && 
                        (!(entry.Value.GetType() is ISerializable) &&
                        entry.Value.GetType().GetCustomAttributes(typeof(SerializableAttribute), true).Length == 0))
                    {
                        table.ExtendedProperties.Remove(entry.Key);
                    }
                }
            }
			if ( result )
				result.Data = contexteToSave;
			return result;
		}

		/////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// convertit les éléments du contexte pour qu'ils entrent dans la version en cours
		/// </summary>
		/// <param name="contexte"></param>
		/// <returns></returns>
		protected virtual CResultAErreur ConvertElementsToVersion(CContexteSauvegardeObjetsDonnees contexteSauvegarde, DataTable table, DataRowState stateAPrendreEnCompte)
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteDonnee contexte = contexteSauvegarde.ContexteDonnee;
			if (contexte.IdVersionDeTravail == null || contexte.IdVersionDeTravail < 0 )
				return result;
			CVersionDonnees versionPrev = new CVersionDonnees (contexte );
			if ( !versionPrev.ReadIfExists ( (int)contexte.IdVersionDeTravail ))
			{
				result.EmpileErreur(I.T("Version @1 doesn't exists|218", contexte.IdVersionDeTravail.ToString() ));
				return result;
			}

			Type tp = CContexteDonnee.GetTypeForTable(table.TableName);
			//S'assure que toutes les données de la version sont en mémoire pour cette table
			CListeObjetsDonnees versionsObjets = new CListeObjetsDonnees(contexte, typeof(CVersionDonneesObjet));
			versionsObjets.Filtre = new CFiltreData(CVersionDonnees.c_champId + "=@1 and " +
				CVersionDonneesObjet.c_champTypeElement + "=@2",
				contexte.IdVersionDeTravail,
                tp.ToString());
            versionsObjets.PreserveChanges = true;
            versionsObjets.AssureLectureFaite();
            result = versionsObjets.ReadDependances("ToutesLesOperations");
            IObjetServeur objetServeurDuType = contexteSauvegarde.ContexteDonnee.GetTableLoader(table.TableName);
			//ArrayList lstTables = new ArrayList(contexte.Tables);
			Dictionary<int, int> mapIdsRefsToIdNewInVersion = new Dictionary<int,int>();
			//foreach ( DataTable table in lstTables )
			{
				IJournaliseurDonneesObjet journaliseur = ((CObjetServeur)contexte.GetTableLoader(table.TableName)).JournaliseurChamps;
				if ( typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tp) && !typeof(IObjetSansVersion).IsAssignableFrom ( tp ) )
				{
					if(  table.Columns.Contains ( CSc2iDataConst.c_champIdVersion ) )
					{
						String strPrimKey = table.PrimaryKey[0].ColumnName;
                        List<DataRow> lstRows = new List<DataRow>();
                        foreach (DataRow row in table.Rows)
                            lstRows.Add(row);

                        ///Charge les éléments de la table déjà modifiés dans cette version
                        int nPosLecture = 0;
                        while (nPosLecture < lstRows.Count)
                        {
                            int nFin = Math.Min(nPosLecture + 500, lstRows.Count);
                            StringBuilder bl = new StringBuilder();
                            for (int nRead = nPosLecture; nRead < nFin; nRead++)
                            {
                                DataRow row = lstRows[nRead];
                                if (row.RowState == DataRowState.Modified)
                                {
                                    bl.Append(row[strPrimKey]);
                                    bl.Append(',');
                                }
                            }
                            nPosLecture = nFin;
                            if (bl.Length > 0)
                            {
                                bl.Remove(bl.Length - 1, 1);
                                CListeObjetsDonnees liste = new CListeObjetsDonnees(contexte, tp);
                                liste.Filtre = new CFiltreData(
                                    CSc2iDataConst.c_champIdVersion + "=@1 and " +
                                    CSc2iDataConst.c_champOriginalId + " in (" + bl.ToString() + ")",
                                    contexte.IdVersionDeTravail);
                                liste.PreserveChanges = true;
                                liste.Filtre.IgnorerVersionDeContexte = true;
                                liste.AssureLectureFaite();
                            }
                        }
						foreach ( DataRow row in lstRows )
						{
							if ((row.RowState & stateAPrendreEnCompte) == row.RowState)
							{
								switch (row.RowState)
								{
									case DataRowState.Added:
										//Noté uniquement nouveau dans cette version
										row[CSc2iDataConst.c_champIdVersion] = contexte.IdVersionDeTravail;
										CVersionDonneesObjet vdoCreate = new CVersionDonneesObjet(contexte);
										vdoCreate.CreateNewInCurrentContexte();
										vdoCreate.VersionDonnees = versionPrev;
										vdoCreate.TypeElement = tp;
										vdoCreate.IdElement = (int)row[strPrimKey];
										vdoCreate.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Ajout;
										if (!journaliseur.IsVersionObjetLinkToElement)//notamment pour les champs custom
											journaliseur.JournaliseDonnees(row, versionPrev);
										break;

									case DataRowState.Modified:
										//Si l'objet fait partie de la version, on n'a pas à le chercher,
										//c'est déjà lui !
										if (!contexte.IdVersionDeTravail.Equals(row[CSc2iDataConst.c_champIdVersion]))
										{
											//Cherche l'objet modifié dans la version et dans les suivantes
											CObjetDonneeAIdNumerique objDansVersion = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[] { contexte });
											CFiltreData filtre = new CFiltreData(CSc2iDataConst.c_champIdVersion + "=@1 and " +
												CSc2iDataConst.c_champOriginalId + "=@2",
												(int)contexte.IdVersionDeTravail,
												row[table.PrimaryKey[0]]);
											filtre.IgnorerVersionDeContexte = true;
											if (!objDansVersion.ReadIfExists(filtre, false))
												objDansVersion = null;
											DataRow newRow = null;
											if (objDansVersion == null)
											{
												//Il s'agit d'un élément qui vient du référentiel et qui n'existe pas encore
												//dans cette version
												newRow = table.NewRow();
											}
											else
												newRow = objDansVersion.Row.Row;
											foreach (DataColumn col in table.Columns)
											{
												if (!col.AutoIncrement && col.ColumnName != CSc2iDataConst.c_champOriginalId &&
													col.ColumnName != CSc2iDataConst.c_champIdVersion)
													newRow[col] = row[col];
											}
											newRow[CSc2iDataConst.c_champIdVersion] = contexte.IdVersionDeTravail;

											if (objDansVersion == null)
											{
												newRow[CSc2iDataConst.c_champOriginalId] = row[table.PrimaryKey[0]];
												table.Rows.Add(newRow);
												objDansVersion = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[] { newRow });
											}
											//Stocke les modifications
											CVersionDonneesObjet vdoModif = new CVersionDonneesObjet(contexte);
											if (!vdoModif.ReadIfExists(new CFiltreData(
												CVersionDonneesObjet.c_champIdElement + "=@1 and " +
												CVersionDonneesObjet.c_champTypeElement + "=@2 and " +
												CVersionDonnees.c_champId + "=@3",
												row[strPrimKey],
												tp.ToString(),
												versionPrev.Id)))
											{
												vdoModif.CreateNewInCurrentContexte();
												vdoModif.VersionDonnees = versionPrev;
												vdoModif.TypeElement = tp;
												vdoModif.IdElement = (int)row[strPrimKey];
												vdoModif.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Modification;
											}
											if (vdoModif.TypeOperation.Code == CTypeOperationSurObjet.TypeOperation.Modification)
											{
												//Stocke les modifications
												if (journaliseur != null)
												{
													journaliseur.JournaliseDonnees(row, versionPrev);
												}
												try
												{
													//Il ne faut pas rejeter les changements, sinon, l'appellant trouve ses données
													//changées (reprise de l'original). 
													//En acceptant, la ligne n'est pas sauvegardée, par contre, elle
													//est gardée telle quelle pour la procédure appelant (pas de modification du contexte)
													//Par contre, il faut envoyer une notification de modif,
													contexteSauvegarde.DonneeNotification.AddModifiedRecord(table.TableName, false, new object[] { row[strPrimKey] });
													row.AcceptChanges();//Valide les données et évite la sauvegarde de la ligne
												}
												catch 
												{
													bool bOldEnforce = contexte.EnforceConstraints;
													contexte.EnforceConstraints = false;
													row.AcceptChanges();
													contexte.AssureParents(row);
													contexte.EnforceConstraints = bOldEnforce;
												}
											}
										}
										break;
									case DataRowState.Deleted:
										/*Lors d'une suppression, les compositions sont automatiquement
										/Supprimées. Or, si le contexte contenait des éléments déjà
										 * lors le la suppression du parent, celui ci a été noté comme deleted.
										 * ce qui n'est pas très bien, puisqu'il est déjà supprimé
										 * il ne faut surtout pas stocker son CVO dans la liste des
										 * modifs de la version, car ce n'est pas cette version qui 
										 * a supprimé l'élément, mais c'était déjà fait avant*/
										if (row.Table.Columns[CSc2iDataConst.c_champIsDeleted] != null &&
											row[CSc2iDataConst.c_champIsDeleted, DataRowVersion.Original] is bool &&
											(bool)row[CSc2iDataConst.c_champIsDeleted, DataRowVersion.Original])
										{
											try
											{
												contexteSauvegarde.DonneeNotification.AddModifiedRecord(table.TableName, false, new object[] { row[strPrimKey] });
												row.RejectChanges();
											}
											catch
											{
												bool bOldEnforce = contexte.EnforceConstraints;
												contexte.EnforceConstraints = false;
												row.RejectChanges();
												contexte.AssureParents(row);
												contexte.EnforceConstraints = bOldEnforce;
											}
											break;
										}
										
										//Suppression d'un élément d'une version antérieure
										//Cherche les versions suivantes
                                        bool bIsCrééDansCetteVersion = false;
                                        CVersionDonneesObjet vdoTrouvee = new CVersionDonneesObjet(contexte);
                                        if (vdoTrouvee.ReadIfExists(new CFiltreData(
                                            CVersionDonneesObjet.c_champIdElement + "=@1 and " +
                                            CVersionDonneesObjet.c_champTypeElement + "=@2 and " +
                                            CVersionDonnees.c_champId + "=@3",
                                            row[strPrimKey, DataRowVersion.Original],
                                            tp.ToString(),
                                            versionPrev.Id)))
                                            bIsCrééDansCetteVersion = vdoTrouvee.TypeOperation.Code == CTypeOperationSurObjet.TypeOperation.Ajout;
										StringBuilder blToutes = new StringBuilder();
                                        if (!bIsCrééDansCetteVersion)
                                        {
                                            blToutes.Append(versionPrev.Id);
                                            blToutes.Append(",");
                                        }
										StringBuilder blTmp = new StringBuilder();
										blTmp.Append(versionPrev.Id);
										CListeObjetsDonnees listeSuivantes = new CListeObjetsDonnees(contexte, typeof(CVersionDonnees));
										listeSuivantes.Filtre = new CFiltreData(CVersionDonnees.c_champIdVersionParente + " in (" + blTmp.ToString() + ")");
										while (listeSuivantes.Count > 0)
										{
											blTmp = new StringBuilder();
											foreach (CVersionDonnees versionSuivante in listeSuivantes)
											{
												blToutes.Append(versionSuivante.Id);
												blToutes.Append(',');
												blTmp.Append(versionSuivante.Id);
												blTmp.Append(',');
												blTmp.Remove(blTmp.Length - 1, 1);
											}
											listeSuivantes.Filtre = new CFiltreData(CVersionDonnees.c_champIdVersionParente + " in (" + blTmp.ToString() + ")");
										}
                                        if (blToutes.Length > 0)
                                        {
                                            blToutes.Remove(blToutes.Length - 1, 1);
                                            //Cherche les éléments modifiés dans les versions suivantes
                                            CListeObjetsDonnees listeObjetsModifies = new CListeObjetsDonnees(contexte, tp);
                                            listeObjetsModifies.Filtre = new CFiltreData(CSc2iDataConst.c_champIdVersion + " in (" + blToutes + ") and " +
                                                CSc2iDataConst.c_champOriginalId + "=@1",
                                                row[strPrimKey, DataRowVersion.Original]);
                                            listeObjetsModifies.Filtre.IgnorerVersionDeContexte = true;
                                            //Suppression des éléments modifiés
                                            //bool bHasStartModeDeconnecte = contexte.BeginModeDeconnecte();
                                            result = CObjetDonneeAIdNumerique.Delete(listeObjetsModifies, true);
                                            /*if (bHasStartModeDeconnecte)
                                                contexte.EndModeDeconnecteSansSauvegardeEtSansReject();*/
                                            if (!result)
                                                return result;
                                        }

										//Suppression des informations concernant ces objets pour toutes les version
										CListeObjetsDonnees listeModifs = new CListeObjetsDonnees(contexte, typeof(CVersionDonneesObjet));
										listeModifs.Filtre = new CFiltreData(
											CVersionDonnees.c_champId + " in (" + blTmp.ToString() + ") and " +
											CVersionDonneesObjet.c_champTypeElement + "=@1 and " +
											CVersionDonneesObjet.c_champIdElement + "=@2",
											tp.ToString(),
											row[strPrimKey, DataRowVersion.Original].ToString());
										result = CObjetDonneeAIdNumerique.Delete(listeModifs, true);
										//contexte.EndModeDeconnecteSansSauvegardeEtSansReject();
										if (!result)
											return result;
                                        if (!bIsCrééDansCetteVersion)
                                        {
                                            //Création de l'information de suppression
                                            CVersionDonneesObjet versionObjet = new CVersionDonneesObjet(contexte);
                                            versionObjet.CreateNewInCurrentContexte();
                                            versionObjet.TypeElement = tp;
                                            versionObjet.IdElement = (int)row[strPrimKey, DataRowVersion.Original];
                                            versionObjet.VersionDonnees = versionPrev;
                                            versionObjet.CodeTypeOperation = (int)CTypeOperationSurObjet.TypeOperation.Suppression;
                                            try
                                            {
                                                row.RejectChanges();
                                                contexteSauvegarde.DonneeNotification.AddModifiedRecord(table.TableName, true, new object[] { row[strPrimKey] });
                                                contexteSauvegarde.ContexteDonnee.SetIsHorsVersion(row, true);
                                            }
                                            catch
                                            {
                                                bool bOldEnforce = contexte.EnforceConstraints;
                                                contexte.EnforceConstraints = false;
                                                row.RejectChanges();
                                                contexte.AssureParents(row);
                                                contexte.EnforceConstraints = bOldEnforce;
                                            }
                                        }
										break;
								}
							}
						}
					}
				}
                objetServeurDuType.ClearCache();


			}
			return result;
		}

		/////////////////////////////////////////////////////////////////////////
		///A surcharger si l'ordre des modifs doit être différent
		protected virtual CResultAErreur MySaveAll( CContexteSauvegardeObjetsDonnees contexteSauvegarde )
		{
			CResultAErreur result = CResultAErreur.True;

			Dictionary<DataRow, DataRow> mapRowDeletedToVersion = new Dictionary<DataRow, DataRow>();

			//Lorsqu'on travaille avec les versions, les objets ne sont pas supprimées,
			//ils sont flaggés sur la version en cours
			if (contexteSauvegarde.VersionDonneesAssociee != null)
			{
				//Annule les suppression
				bool bOldEnforce = contexteSauvegarde.ContexteDonnee.EnforceConstraints;
				List<DataRow> rowsRetablies = new List<DataRow>();
				foreach (DataTable table in new ArrayList(contexteSauvegarde.ContexteDonnee.Tables))
				{
					IJournaliseurDonneesObjet journaliseur = ((CObjetServeur)contexteSauvegarde.ContexteDonnee.GetTableLoader(table.TableName)).JournaliseurChamps;
					bool bCreateVersionObjet = journaliseur != null && journaliseur.IsVersionObjetLinkToElement;
					if (table.Columns.Contains(CSc2iDataConst.c_champIdVersion))
					{
						foreach (DataRow row in table.Select("", "", DataViewRowState.Deleted))
						{
							
							if ( bCreateVersionObjet )
							{
								CVersionDonneesObjet versionObjet = contexteSauvegarde.VersionDonneesAssociee.GetVersionObjetAvecCreation(row);
							}
							contexteSauvegarde.ContexteDonnee.EnforceConstraints = false;
							object[] keys = new object[table.PrimaryKey.Length];
							int nIndex = 0;
							foreach (DataColumn col in table.PrimaryKey)
								keys[nIndex++] = row[col, DataRowVersion.Original];
							contexteSauvegarde.DonneeNotification.AddModifiedRecord(table.TableName, true, keys);
							row.RejectChanges();
							//si la version n'est pas encore sauvegardée, sauvegarde la version
							//Pour que son id soit correct
							if (contexteSauvegarde.VersionDonneesAssociee.Id < 0)
							{
								new CVersionDonneesServeur(IdSession).SaveAll(contexteSauvegarde, DataRowState.Added);
							}
							row[CSc2iDataConst.c_champIdVersion] = contexteSauvegarde.VersionDonneesAssociee.Id;
							row[CSc2iDataConst.c_champIsDeleted] = true;
							contexteSauvegarde.DonneeNotification.AddModifiedRecord(row.Table.TableName, true, keys);
							rowsRetablies.Add(row);
						}
					}
				}
				try
				{
					contexteSauvegarde.ContexteDonnee.EnforceConstraints = bOldEnforce;
				}
				catch //en cas d'erreur, c'est peut être qu'il manque un parent à l'objet !
				{
					foreach ( DataRow row in rowsRetablies )
						contexteSauvegarde.ContexteDonnee.AssureParents(row);
					contexteSauvegarde.ContexteDonnee.EnforceConstraints = bOldEnforce;
				}
			}
						

			//Garde le contexte avant add et update
			CContexteDonnee  donneesDelete = null;
			if ( contexteSauvegarde.ContexteDonnee.HasChanges(DataRowState.Deleted) )
			{
				if ( contexteSauvegarde.ContexteDonnee.IdVersionDeTravail != null && 
					contexteSauvegarde.ContexteDonnee.IdVersionDeTravail >= 0 )
				{
					foreach ( DataTable table in new ArrayList(contexteSauvegarde.ContexteDonnee.Tables) )
					{
						if ( table.Rows.Count > 0 )
						{
							result = ConvertElementsToVersion(contexteSauvegarde, table, DataRowState.Deleted);
							if ( !result )
								return result;
						}
					}
				}
				donneesDelete = contexteSauvegarde.ContexteDonnee.GetCompleteChanges(DataRowState.Deleted);
			}
				
			result = DoAddAndUpdateInDB(contexteSauvegarde);
			if ( result && donneesDelete != null )
			{
				CContexteSauvegardeObjetsDonnees contexteDelete = new CContexteSauvegardeObjetsDonnees(
					donneesDelete,
					contexteSauvegarde.DonneeNotification,
					contexteSauvegarde.Restrictions);
				//Le ds ne contient pas la version modifiée ! il faut donc la copier dans ce contexte
                if (!contexteSauvegarde.ContexteDonnee.DisableHistorisation && CVersionDonneesObjet.EnableJournalisation)
				{
					if (contexteSauvegarde.VersionDonneesAssociee != null)
					{
						donneesDelete.Merge(contexteSauvegarde.ContexteDonnee.Tables[CVersionDonnees.c_nomTable], true, MissingSchemaAction.Add);
					}
					if (donneesDelete.Tables.Contains(CVersionDonnees.c_nomTable) && contexteSauvegarde.VersionDonneesAssociee != null)
					{
						DataRow row = donneesDelete.Tables[CVersionDonnees.c_nomTable].Rows.Find(contexteSauvegarde.VersionDonneesAssociee.Id);
						if (row != null)
							contexteDelete.VersionDonneesAssociee = new CVersionDonnees(row);
					}
				}
				result = DoDeleteInDB(contexteDelete);
				
			}
			
				
			return result;
		}

		/////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Appeller ici les fonction à passer au loader avant une mise à jour
		/// ou un delete
		/// </summary>
		/// Cette fonction est notamment surchargée par le contexte de données
		/// Synchro secondaire pour désactiver l'insertion d'identité
		/// <param name="loader"></param>
		protected virtual void PrepareLoader ( IObjetServeur loader )
		{
		}

		/////////////////////////////////////////////////////////////////////////
		protected CResultAErreur DoAddAndUpdateInDB ( CContexteSauvegardeObjetsDonnees contexteSauvegarde )
		{
			CResultAErreur result = CResultAErreur.True;
			ArrayList listeTables = CContexteDonnee.GetTablesOrderInsert(contexteSauvegarde.ContexteDonnee);
			foreach ( DataTable table in listeTables )
			{
				if ( table.Rows.Count != 0  )
				{
					//Stef, le 22/06/08 : la conversion dans la version sont maintenant faites
					//Table par table comme ça, on est sur que les parents de chaque éléments
					//Ont un ID >= 0.
					//Fait les conversions de version
					if (contexteSauvegarde.ContexteDonnee.IdVersionDeTravail != null && contexteSauvegarde.ContexteDonnee.IdVersionDeTravail >= 0)
					{
						result = ConvertElementsToVersion(contexteSauvegarde, table, DataRowState.Modified | DataRowState.Added);
						if (!result)
							return result;
					}


					/// Si on est dans une version, les objets du référentiel ne doivent pas être modifiés
					/// Par exemple : dans une version, création d'une CIV (id=-2000)
					/// Affectation de cet CIV à un acteur. L'objet ayant l'id dans le référentiel
					/// est modifié, mais un autre objet est créé , et un acceptChanges est appelé
					/// sur l'objet du référentiel. Or, lorsqu'on valide la CIV, elle change d'ID (34 par ex)
					/// du coup, l'objet référentiel est automatiquement modfiié, et pas en modified
					/// Mais ce n'est pas ce qu'on veut, car sinon, il va être sauvé avec les modifs,
					/// donc il faut appeller un accept change dessus
					

					IObjetServeur loader = CContexteDonnee.GetTableLoader (table.TableName, m_nIdVersionDeTravail,IdSession);
                    if (contexteSauvegarde.ContexteDonnee.ShouldDesactiveIdAutoOnTable(table.TableName))
                        loader.DesactiverIdentifiantAutomatique = true;
					PrepareLoader ( loader );
					result = loader.SaveAll(contexteSauvegarde, DataRowState.Added | DataRowState.Modified);
					if ( !result )
						break;
				}
			}
			if ( result )
				result = new CVersionDonneesServeur(IdSession).SaveAll(contexteSauvegarde, DataRowState.Added | DataRowState.Modified);
			if ( result )
				result = new CVersionDonneesObjetServeur(IdSession).SaveAll(contexteSauvegarde, DataRowState.Added | DataRowState.Modified);
			if (result )
				result = new CVersionDonneesObjetOperationServeur(IdSession).SaveAll(contexteSauvegarde, DataRowState.Added | DataRowState.Modified);
			
			return result;
		}

		/////////////////////////////////////////////////////////////////////////
		protected CResultAErreur DoDeleteInDB(CContexteSauvegardeObjetsDonnees contexteSauvegarde)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( contexteSauvegarde.ContexteDonnee == null )
				return result;
			ArrayList listeTables = CContexteDonnee.GetTableOrderDelete(contexteSauvegarde.ContexteDonnee);
			foreach ( DataTable table in listeTables )
			{
				if ( table.Rows.Count != 0 )
				{
					
					IObjetServeur loader = CContexteDonnee.GetTableLoader(table.TableName, m_nIdVersionDeTravail, IdSession);
						
					PrepareLoader(loader);
					result = loader.SaveAll(contexteSauvegarde, DataRowState.Deleted);
					if ( !result )
						break;
				}
									
			}
			if (result && CVersionDonneesObjet.EnableJournalisation && !contexteSauvegarde.ContexteDonnee.DisableHistorisation)
			{
				if (result)
					result = new CVersionDonneesServeur(IdSession).SaveAll(contexteSauvegarde, DataRowState.Added);
				if (result)
					result = new CVersionDonneesObjetServeur(IdSession).SaveAll(contexteSauvegarde, DataRowState.Added);
				if (result)
					result = new CVersionDonneesObjetOperationServeur(IdSession).SaveAll(contexteSauvegarde, DataRowState.Added);
			}
			return result;
		}
	}
}
