using System;
using System.Collections;
using System.Threading;
using System.Data;

using sc2i.data.dynamic;
using sc2i.data.serveur;
using sc2i.multitiers.server;
using sc2i.common;
using sc2i.data;
using sc2i.process;
using sc2i.multitiers.client;
using System.Text;



namespace sc2i.ProcessEnExecution.serveur
{
	/// <summary>
	/// Description résumée de CFormulaireServeur.
	/// </summary>
	public class CProcessEnExecutionInDbServeur : CObjetServeurAvecBlob, IProcessEnExecutionInDbServeur
	{
#if PDA
		public CProcessEnExecutionInDbServeur()
		{
		}
#endif
		/// //////////////////////////////////////////////////
		public CProcessEnExecutionInDbServeur(int nIdSession)
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CProcessEnExecutionInDb.c_nomTable;
		}

		/// //////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CProcessEnExecutionInDb);
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CProcessEnExecutionInDb ProcessEnExecution = (CProcessEnExecutionInDb)objet;
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}

		/// //////////////////////////////////////////////////
		public override CResultAErreur TraitementAvantSauvegarde(CContexteDonnee contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			DataTable table = contexte.Tables[GetNomTable()];
			if ( table == null )
			{
				foreach ( DataRow row in new ArrayList(table.Rows) )
				{
					if ( row.RowState == DataRowState.Modified )
						row[CProcessEnExecutionInDb.c_champDateModification] = DateTime.Now;
				}
			}
			return result;
		}

		/// //////////////////////////////////////////////////
		private CBrancheProcess m_brancheToExecute;
		private CAction m_actionToExecute;
		private CContexteExecutionAction m_contexteExecutionProcess;
		
		private void DemarreProcess()
		{
			CResultAErreur result = m_brancheToExecute.ExecuteAction ( m_actionToExecute, m_contexteExecutionProcess, true );
			if ( !result )
			{
			}
			else
			{
			}
		}

        /// //////////////////////////////////////////////////
        public CResultAErreur RepriseProcess(int nIdProcessEnExecution,
            int nIdAction,
            IIndicateurProgression indicateur)
        {
            using (C2iSponsor sponsor = new C2iSponsor())
            {
                sponsor.Register(indicateur);
                CResultAErreur result = CResultAErreur.True;

                ///Stef, 290808 : ouvre une session spécifique pour le process
                ///Pour pouvoir gérer le changement d'utilisateur proprement
                CSousSessionClient session = CSousSessionClient.GetNewSousSession(IdSession);
                session.OpenSession(new CAuthentificationSessionSousSession(IdSession), "Continue process " + nIdProcessEnExecution, ETypeApplicationCliente.Process);
                CSessionClient sessionOriginale = CSessionClient.GetSessionForIdSession(IdSession);
                //TESTDBKEYTODO
                if (sessionOriginale.GetInfoUtilisateur() != null)
                    session.ChangeUtilisateur(sessionOriginale.GetInfoUtilisateur().KeyUtilisateur);
                try
                {
                    //Stef 08/12/2007 : le contexte ne reçoit plus les notifications
                    using (CContexteDonnee contexteDonnee = new CContexteDonnee(session.IdSession, true, false))
                    {
                        CProcessEnExecutionInDb processEnExecution = new CProcessEnExecutionInDb(contexteDonnee);
                        if (!processEnExecution.ReadIfExists(nIdProcessEnExecution))
                        {
                            result.EmpileErreur(I.T("Current action @1 doesn't exist|30012", nIdProcessEnExecution.ToString()));
                            return result;
                        }
                        result = contexteDonnee.SetVersionDeTravail(processEnExecution.IdVersionExecution, true);
                        if (!result)
                            return result;
                        CBrancheProcess branche = processEnExecution.BrancheEnCours;
                        CContexteExecutionAction contexteExecution = new CContexteExecutionAction(
                            processEnExecution, branche, branche.Process.GetValeurChamp(CProcess.c_strIdVariableElement), contexteDonnee, indicateur);
                        CAction action = branche.Process.GetActionFromId(nIdAction);
                        if (action == null)
                        {
                            result.EmpileErreur(I.T("Impossible to resume the processing : the action @1 doesn't exist|30013",nIdAction.ToString()));
                            return result;
                        }
                        bool bTrans = false;
                        if (branche.Process.ModeTransactionnel)
                        {
                            session.BeginTrans();
                            bTrans = true;
                        }
                        try
                        {
                            result = branche.ExecuteAction(action, contexteExecution, true);
                        }
                        catch (Exception e)
                        {
                            result.EmpileErreur(new CErreurException(e));
                        }
                        finally
                        {
                            if (bTrans)
                            {
                                if (result)
                                {
                                    result = session.CommitTrans();
                                    if (result)
                                        contexteExecution.OnEndProcess();
                                }
                                else
                                    session.RollbackTrans();
                            }
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    result.EmpileErreur(new CErreurException(e));
                }
                finally
                {
                    try
                    {
                        session.CloseSession();
                    }
                    catch { }
                }
                return result;
            }
        }
	

		/// //////////////////////////////////////////////////
		///LE data du result contient la valeur de retour
		public CResultAErreur StartProcess ( CValise2iSerializable valiseProcess, 
			CReferenceObjetDonnee refCible, 
			int? nIdVersion,
			IIndicateurProgression indicateur )
		{
			using (C2iSponsor sponsor = new C2iSponsor())
			{
				sponsor.Register(indicateur);

				CResultAErreur result = CResultAErreur.True;


				//Stef 08/12/2007 : le contexte ne reçoit plus les notifications
				using (CContexteDonnee contexteDeSession = new CContexteDonnee(IdSession, true, false))
				{
					result = valiseProcess.GetObjet(typeof(CContexteDonnee), contexteDeSession);
					if (!result)
					{
						result.EmpileErreur(I.T("Error while recovering process|30014"));
						return result;
					}
					CProcess leProcessAExecuter = (CProcess)result.Data;

					CObjetDonneeAIdNumerique objetCible = null;


					///Stef, 290808 : ouvre une session spécifique pour le process
					///Pour pouvoir gérer le changement d'utilisateur proprement
					CSousSessionClient session = CSousSessionClient.GetNewSousSession(contexteDeSession.IdSession);
					session.OpenSession(new CAuthentificationSessionSousSession(contexteDeSession.IdSession), leProcessAExecuter.Libelle, ETypeApplicationCliente.Process);
					CSessionClient sessionOriginale = CSessionClient.GetSessionForIdSession(contexteDeSession.IdSession);
					if (sessionOriginale.GetInfoUtilisateur() != null)
						session.ChangeUtilisateur(sessionOriginale.GetInfoUtilisateur().KeyUtilisateur);

					try
					{
						//Charge les éléments pour la nouvelle session;
						//Stef 08/12/2007 : le contexte ne reçoit plus les notifications
                        //Stef le 9/2/2011, si, le contexte reçoit les notifications,
                        //sinon, si un sous process modifie quelque chose, on ne le sait pas
                        //et on a des problèmes de violation d'accès concurentiel
						using (CContexteDonnee contexte = new CContexteDonnee(session.IdSession, true, true))
						{
							result = contexte.SetVersionDeTravail(nIdVersion, true);
							if (!result)
								return result;
							if (refCible != null)
							{
								objetCible = (CObjetDonneeAIdNumerique)refCible.GetObjet(contexte);
								if (objetCible == null)
								{
                                    ///Stef 26/1//2009 : si l'objet n'existe pas, c'est qu'il n'y a rien à déclencher.
                                    ///cela résoud un problème : si un process avant celui-ci a supprimé le targetElement
                                    ///il ne faut pas lancer ce process là !
									//result.EmpileErreur(I.T("Target object @1 does not exist|30015",refCible.ToString()));
									return result;
								}

							}
							result = ExecuteProcess(
								session,
								objetCible,
								leProcessAExecuter,
								contexte, false,
								indicateur);
						}
					}
					catch (Exception e)
					{
						result.EmpileErreur(new CErreurException(e));
					}
					finally
					{
						try
						{
							session.CloseSession();
						}
						catch { }
					}
				}
				return result;
			}
		}

		//Le data du result contient la valeur de retour du process
		protected CResultAErreur ExecuteProcess ( 
			CSessionClient session,
			object objetCible, 
			CProcess leProcessAExecuter,
			CContexteDonnee contexte,
			bool bSauvegardeDuContexteExecutionExterne,
			IIndicateurProgression indicateur)
		{
			using (C2iSponsor sponsor = new C2iSponsor())
			{
				sponsor.Register(indicateur);
				/// Stef, le 2/4/08 : pb rencontré : leProcessAExecuter peut être
				/// lié à un autre CContexteDonnee que celui dans lequel on execute l'action
				/// en effet, il est alloué dans un CContexteDonnee, mais un autre CContexteDonnee
				/// peut être recréé pour que l'execution.
				/// OR, dans ce cas, les variables calculées évaluées par le CProcess se trouvent dans
				/// le contexte du process, donc, pas dans le contexte d'execution et les modifs
				/// sur ces variables ne sont donc pas sauvegardées !
				/// d'où la ligne suivante : leProcessAExecuter.ContexteDonnee = contexte
				leProcessAExecuter.ContexteDonnee = contexte;
				//Fin Stef 2/4/08
				CResultAErreur result = CResultAErreur.True;
				CProcessEnExecutionInDb processEnExec = new CProcessEnExecutionInDb(contexte);
				processEnExec.CreateNewInCurrentContexte();
				if (objetCible is CObjetDonneeAIdNumerique)
					processEnExec.ElementLie = (CObjetDonneeAIdNumerique)objetCible;
				else
					processEnExec.ElementLie = null;
				processEnExec.Libelle = leProcessAExecuter.Libelle;
                processEnExec.DbKeyEvennementDeclencheur = leProcessAExecuter.InfoDeclencheur.DbKeyEvenementDeclencheur;
				processEnExec.IdVersionExecution = contexte.IdVersionDeTravail;

				CBrancheProcess branche = new CBrancheProcess(leProcessAExecuter);
				branche.IsModeAsynchrone = leProcessAExecuter.ModeAsynchrone;
				CSessionClient sessionSource = CSessionClient.GetSessionForIdSession(IdSession);

                //TESTDBKEYTODO
				branche.KeyUtilisateur = sessionSource.GetInfoUtilisateur().KeyUtilisateur;
				branche.ConfigurationImpression = sessionSource.ConfigurationsImpression;

				CContexteExecutionAction contexteExecution = new CContexteExecutionAction(
					processEnExec,
					branche,
					objetCible,
					contexte,
					leProcessAExecuter.ModeAsynchrone ? null : indicateur);
				contexteExecution.SauvegardeContexteExterne = bSauvegardeDuContexteExecutionExterne;
				//Mode synchrone
				if (!leProcessAExecuter.ModeAsynchrone)
				{
                    bool bTrans = false;
                    if (!bSauvegardeDuContexteExecutionExterne && leProcessAExecuter.ModeTransactionnel)
                    {
                        contexte.SaveAll(true);//Sauve le contexte en execution avant démarrage
                        session.BeginTrans();
                        bTrans = true;
                    }
                    try
                    {
                        result = branche.ExecuteAction(leProcessAExecuter.GetActionDebut(), contexteExecution, true);
                    }
                    catch (Exception e)
                    {
                        result.EmpileErreur(new CErreurException(e));
                    }
                    finally
                    {
                        if (bTrans)
                        {
                            if (result)
                            {
                                result = session.CommitTrans();
                                if (result)
                                    contexteExecution.OnEndProcess();
                            }
                            else
                                session.RollbackTrans();
                        }
                    }
                        

                    if (!result)
                        result.EmpileErreur(I.T("Erreur in @1 process|20001", leProcessAExecuter.Libelle));
					if (leProcessAExecuter.VariableDeRetour != null)
						result.Data = leProcessAExecuter.GetValeurChamp(leProcessAExecuter.VariableDeRetour.IdVariable);
					return result;
				}
				else
				{
					//Ouvre une nouvelle session pour éxecuter le process
					if (leProcessAExecuter.ModeAsynchrone)
					{
						CSessionProcessServeurSuivi sessionAsync = new CSessionProcessServeurSuivi();
						result = sessionAsync.OpenSession(new CAuthentificationSessionProcess(),
							"Process " + leProcessAExecuter.Libelle,
							session);
						if (!result)
							return result;
						session = sessionAsync;
					}
					contexteExecution.ChangeIdSession(session.IdSession);
					contexteExecution.HasSessionPropre = true;
					m_brancheToExecute = branche;
					m_actionToExecute = leProcessAExecuter.GetActionDebut();
					m_contexteExecutionProcess = contexteExecution;
					Thread th = new Thread(new ThreadStart(DemarreProcess));
					th.Start();
					return result;
				}
			}
		}

		/// //////////////////////////////////////////////////
		public CResultAErreur StartProcessMultiples ( CValise2iSerializable valiseProcess, 
			CReferenceObjetDonnee[] refsCible, 
			int? nIdVersion,
			IIndicateurProgression indicateur )
		{
			using (C2iSponsor sponsor = new C2iSponsor())
			{
				sponsor.Register(indicateur);

				CResultAErreur result = CResultAErreur.True;
				//Stef 08/12/2007 : le contexte ne reçoit plus les notifications
				using (CContexteDonnee contexteDeSession = new CContexteDonnee(IdSession, true, false))
				{
					result = valiseProcess.GetObjet(typeof(CContexteDonnee), contexteDeSession);
					if (!result)
					{
						result.EmpileErreur(I.T("Error while process recovering|30014"));
						return result;
					}
					CProcess leProcessAExecuter = (CProcess)result.Data;

					CObjetDonneeAIdNumerique objetCible = null;

					///Stef, 290808 : ouvre une session spécifique pour le process
					///Pour pouvoir gérer le changement d'utilisateur proprement
					CSousSessionClient session = CSousSessionClient.GetNewSousSession(contexteDeSession.IdSession);
					session.OpenSession(new CAuthentificationSessionSousSession(contexteDeSession.IdSession), leProcessAExecuter.Libelle, ETypeApplicationCliente.Process);
					CSessionClient sessionOriginale = CSessionClient.GetSessionForIdSession(contexteDeSession.IdSession);
                    //TESTDBKEYOK
					if (sessionOriginale.GetInfoUtilisateur() != null)
						session.ChangeUtilisateur(sessionOriginale.GetInfoUtilisateur().KeyUtilisateur);

					try
					{


						//Charge les éléments pour la nouvelle session;
						//Stef 08/12/2007 : le contexte ne reçoit plus les notifications
						using (CContexteDonnee contexte = new CContexteDonnee(session.IdSession, true, false))
						{
							result = contexte.SetVersionDeTravail(nIdVersion, true);
							if (!result)
								return result;
							if (leProcessAExecuter.SurTableauDeTypeCible)
							{
								ArrayList lst = new ArrayList();
								foreach (CReferenceObjetDonnee refCibleTmp in refsCible)
								{
									object tmp = refCibleTmp.GetObjet(contexte);
									if (tmp != null)
										lst.Add(tmp);
								}
								return ExecuteProcess(session, lst, leProcessAExecuter, contexte, false, indicateur);
							}
							try
							{
								if (!leProcessAExecuter.ModeAsynchrone)
									session.BeginTrans(IsolationLevel.ReadCommitted);
								foreach (CReferenceObjetDonnee refCible in refsCible)
								{
                                    bool bShouldStart = true;
									if (refCible != null)
									{
										objetCible = (CObjetDonneeAIdNumerique)refCible.GetObjet(contexte);
										if (objetCible == null)
										{
                                            ///Stef 26/1//2009 : si l'objet n'existe pas, c'est qu'il n'y a rien à déclenché.
                                            ///cela résoud un problème : si un process avant celui-ci a supprimé le targetElement
                                            ///il ne faut pas lancer ce process là !
											/*result.EmpileErreur(I.T("Target object @1 does not exist|30015", refCible.ToString()));
											return result;*/
                                            bShouldStart = false;
										}
									}
                                    if (bShouldStart)
                                    {
                                        result = ExecuteProcess(
                                            session,
                                            objetCible,
                                            leProcessAExecuter,
                                            contexte, true, indicateur);
                                        if (!result)
                                        {
                                            result.EmpileErreur(I.T("Execution error on element @1|30016", objetCible.DescriptionElement));
                                            return result;
                                        }
                                    }
								}
								if (result)
									result = contexte.SaveAll(true);

							}
							catch (Exception e)
							{
								result.EmpileErreur(new CErreurException(e));
							}
							finally
							{
								if (result)
									result = session.CommitTrans();
								if (!result)
									session.RollbackTrans();
							}
						}
					}
					catch (Exception e)
					{
						result.EmpileErreur(new CErreurException(e));
					}
					finally
					{
						try
						{
							session.CloseSession();
						}
						catch
						{
						}
					}
					return result;
				}
			}
		}

		/// //////////////////////////////////////////////////
		public CResultAErreur Purger ( DateTime dt )
		{
			CResultAErreur result = CResultAErreur.True;
			CSessionClient session = CSessionClient.GetSessionForIdSession ( IdSession );
			try
			{
				using ( CContexteDonnee contexte = new CContexteDonnee ( IdSession, true, false ) )
				{
					CListeObjetsDonnees  liste = new CListeObjetsDonnees ( contexte, typeof(CProcessEnExecutionInDb ) );
					liste.Filtre = new CFiltreData ( CProcessEnExecutionInDb.c_champDateCreation+"<@1 and "+
						"("+CProcessEnExecutionInDb.c_champEtat +"=@2 or "+
						CProcessEnExecutionInDb.c_champEtat+"=@3)",
						dt,
						(int)EtatProcess.Erreur,
						(int)EtatProcess.Termine );
					int nCount = liste.Count;
					for ( int nBloc = 0; nBloc < nCount; nBloc += 500 )
					{
						StringBuilder bl = new StringBuilder();
						int nMin = Math.Min ( nBloc+500, nCount );
						for (int nCpt = nBloc; nCpt < nMin; nCpt++)
						{
							bl.Append(((CProcessEnExecutionInDb)liste[nCpt]).Id);
							bl.Append(",");
						}
						if ( bl.Length > 0 )
						{
							bl.Remove ( bl.Length-1, 1);
							using (CContexteDonnee ctxTmp = new CContexteDonnee ( IdSession, true, false ))
							{
								CListeObjetsDonnees petiteListe = new CListeObjetsDonnees ( ctxTmp, typeof(CProcessEnExecutionInDb) );
								petiteListe.Filtre = new CFiltreData (
								CProcessEnExecutionInDb.c_champId+" in ("+
								bl.ToString()+")");
								result = CObjetDonneeAIdNumerique.Delete ( petiteListe );
								if ( !result )
									return result;
							}
						}
					}
				}
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e) ) ;
			}
			return result;
		}

				

		
	}
}
