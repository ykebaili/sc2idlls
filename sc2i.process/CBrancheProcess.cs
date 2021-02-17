using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

using System.Threading;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CBrancheProcess.
	/// Correspond à une branche d'un process
	/// </summary>
	public class CBrancheProcess : I2iSerializable
	{
		#region CInfoPileExecution
		//pour la stack trace
		public class CInfoPileExecution : I2iSerializable
		{
			private int m_nIdAction;
			private int m_nIdLienArrivant = -1;

			////////////////////////////////////////////////////////////////////////////////
			public CInfoPileExecution (  )
			{
				m_nIdAction = -1;
				m_nIdLienArrivant = -1;
			}

			////////////////////////////////////////////////////////////////////////////////
			public CInfoPileExecution ( CLienAction lienSource, CAction actionExecutee )
			{
				m_nIdAction = actionExecutee.IdObjetProcess;
				if ( lienSource != null )
					m_nIdLienArrivant = lienSource.IdActionArrivee;
			}

			////////////////////////////////////////////////////////////////////////////////
			private int GetNumVersion()
			{
				return 0;
			}

			////////////////////////////////////////////////////////////////////////////////
			public CResultAErreur Serialize ( C2iSerializer serializer )
			{
				int nVersion = GetNumVersion();
				CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
				if ( !result )
					return result;
				serializer.TraiteInt ( ref m_nIdAction );
				serializer.TraiteInt ( ref m_nIdLienArrivant );
				return result;
			}

			/// //////////////////////////////////
			public int IdAction
			{
				get
				{
					return m_nIdAction;
				}
			}

			/// //////////////////////////////////
			public int IdLienArrivant
			{
				get
				{
					return m_nIdLienArrivant;
				}
			}
		}
		#endregion

		private CProcess m_process = null;
        private CDbKey m_keyUtilisateur = null;
		private int m_nIdActionEnCours = -1;

		//Si asynchrone, la branche possède sa propre session
		private bool m_bIsAsynchrone = false;

		//Indique que l'execution n'a pas lieu dans un contexte séparé
		private bool m_bExecutionSurContexteClient = false;

		//Stocke les variables internes des actions en cours
		//IdAction->Object dépendant de de l'action
		private Hashtable m_tableDataAction = new Hashtable();

		//Liste de CInfoPileExecution
		private ArrayList m_pile = new ArrayList();

		private CConfigurationsImpression m_configurationImpression = new CConfigurationsImpression();

		/// /////////////////////////////////////
		public CBrancheProcess( CProcess process )
		{
			m_process = process;
		}

		/// /////////////////////////////////////
		public CDbKey KeyUtilisateur
		{
			get
			{
				return m_keyUtilisateur;
			}
			set
			{
				m_keyUtilisateur = value;
			}
		}

		/// /////////////////////////////////////
		public CProcess Process
		{
			get
			{
				return m_process;
			}
		}

		/// /////////////////////////////////////
		public CConfigurationsImpression ConfigurationImpression
		{
			get
			{
				return m_configurationImpression;
			}
			set
			{
				m_configurationImpression = value;
			}
		}

		/// /////////////////////////////////////
		public bool IsModeAsynchrone
		{
			get
			{
				return m_bIsAsynchrone;
			}
			set
			{
				m_bIsAsynchrone = value;
			}
		}

		/// /////////////////////////////////////
		public bool ExecutionSurContexteClient
		{
			get
			{
				return m_bExecutionSurContexteClient;
			}
			set
			{
				m_bExecutionSurContexteClient = value;
			}
		}

		/// /////////////////////////////////////
		public int IdActionEnCours
		{
			get
			{
				return m_nIdActionEnCours;
			}
		}

		/// /////////////////////////////////////
		public CInfoPileExecution[] PileExecution
		{
			get
			{
				return (CInfoPileExecution[])m_pile.ToArray ( typeof(CInfoPileExecution) );
			}
		}

		/// /////////////////////////////////////
		private int GetNumVersion()
		{
			return 3;
			//1 : Ajout de la configuration d'impression
			//2 : Ajout de l'execution sur contexte client
            //3 : remplace Id utilisateur par Key utilisateur
		}

		/// /////////////////////////////////////
		public CResultAErreur Serialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
            //TESTDBKEYOK
            if (nVersion < 3)
                serializer.ReadDbKeyFromOldId(ref m_keyUtilisateur, null);
            else
                serializer.TraiteDbKey(ref m_keyUtilisateur);
			serializer.TraiteInt ( ref m_nIdActionEnCours );

			//Sérialize les données des actions
			int nNb = m_tableDataAction.Count;
			serializer.TraiteInt ( ref nNb );
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach ( int nIdAction in m_tableDataAction.Keys )
					{
						int nIdActionPourRef = nIdAction;
						serializer.TraiteInt ( ref nIdActionPourRef );
						I2iSerializable donnee = (I2iSerializable)m_tableDataAction[nIdAction];
						result = serializer.TraiteObject ( ref donnee );
						if ( !result )
							return result;
					}
					break;
				case ModeSerialisation.Lecture :
					m_tableDataAction.Clear();
					for ( int nData = 0; nData < nNb; nData++ )
					{
						int nIdAction = 0;
						serializer.TraiteInt ( ref nIdAction );
						I2iSerializable donnee = null;
						result = serializer.TraiteObject ( ref donnee );
						if ( !result )
							return result;
						m_tableDataAction[nIdAction] = donnee;
					}
					break;
			}

			//Sérialize le process
			I2iSerializable objet = m_process;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_process = (CProcess)objet;

			//Serialize la trace
			result = serializer.TraiteArrayListOf2iSerializable ( m_pile );
			if ( !result )
				return result;

			if ( nVersion >= 1 )
			{
				objet = m_configurationImpression;
				serializer.TraiteObject ( ref objet );
				m_configurationImpression = (CConfigurationsImpression)objet;
			}
			else
				m_configurationImpression = new CConfigurationsImpression();

			if (nVersion >= 2)
				serializer.TraiteBool(ref m_bExecutionSurContexteClient);

			return result;
		}

        private class CLockerAsynchrone
        {
        }

		/// /////////////////////////////////////
		private CAction m_actionAsynchroneAExecuter = null;
		private CContexteExecutionAction m_contexteAsynchrone;
		private void ExecuteActionAsynchrone()
		{
            m_contexteAsynchrone.Branche.ExecuteAction(m_actionAsynchroneAExecuter, m_contexteAsynchrone, true);
            m_contexteAsynchrone.OnEndProcess();
		}

		/// /////////////////////////////////////
		public CResultAErreur ExecuteAction ( CAction action, CContexteExecutionAction contexte, bool bIsActionPrincipaleProcess )
		{
			return ExecuteAction ( action, contexte, contexte.ObjetCible, bIsActionPrincipaleProcess );
		}

		/// <summary>
		/// Execute une action
		/// </summary>
		/// <param name="action"></param>
		/// <param name="contexte"></param>
		/// <param name="bIsActionPrincipaleProcess">Si vrai, considère que le process en execution est fermé en sortie,
		/// si non, il s'agit d'une action appelée dans une boucle par exemple</param>
		/// <returns></returns>
		public CResultAErreur ExecuteAction ( 
			CAction action, 
			CContexteExecutionAction contexte, 
			object objetCible,
			bool bIsActionPrincipaleProcess )
		{
			CResultAErreur result = CResultAErreur.True;
			m_pile.Add ( new CInfoPileExecution ( null, action ));
			m_nIdActionEnCours = action.IdObjetProcess;
			action.Process.SetValeurChamp ( CProcess.c_strIdVariableElement, objetCible );
			bool bFermerLaSessionEnSortant = IsModeAsynchrone && bIsActionPrincipaleProcess;
			CSessionClient sessionClient = CSessionClient.GetSessionForIdSession ( contexte.IdSession );
			CConfigurationsImpression oldConf = null;
			contexte.SetInfoProgression ( action.Libelle );
			try
			{
				if (contexte.IndicateurProgression != null && contexte.IndicateurProgression.CancelRequest)
				{
					result.EmpileErreur(I.T("User cancellation|182"));
					result.Data = null;
					return result;
				}
			}
			catch { }
			if ( sessionClient != null )
				oldConf = sessionClient.ConfigurationsImpression;
			try
			{
				
				if ( sessionClient != null )
					sessionClient.ConfigurationsImpression = ConfigurationImpression;
				action.Process.ContexteExecution = contexte;
				result = action.Execute ( contexte );
				while ( result.Data != null && result)
				{
					//retourne un lien : on continue
					if ( result.Data is CLienAction )
					{
						m_nIdActionEnCours = action.IdObjetProcess;
						CLienAction lien = (CLienAction)result.Data;
						m_pile.Add ( new CInfoPileExecution ( lien, lien.ActionArrivee ));
						contexte.ProcessEnExecution.LibelleActionEnCours = lien.ActionArrivee.Libelle;
						if ( lien is CLienAsynchrone && !IsModeAsynchrone)
						{
							IsModeAsynchrone = true;
							//Execution de l'action en mode asynchrone et fin
							m_actionAsynchroneAExecuter = lien.ActionArrivee;
							m_contexteAsynchrone = contexte;
							Thread th  = new Thread ( new ThreadStart(ExecuteActionAsynchrone) );
							th.Start();
							bFermerLaSessionEnSortant = false;
							result.Data = null;
							return result;
						}
						else
						{
							action = ((CLienAction)result.Data).ActionArrivee;
							action.Process.ContexteExecution = contexte;
							
							contexte.SetInfoProgression ( action.Libelle );
							try
							{
								if (contexte.IndicateurProgression != null && contexte.IndicateurProgression.CancelRequest)
								{
									result.EmpileErreur(I.T("User cancellation|182"));
									result.Data = null;
									return result;
								}
							}
							catch { }

							result = action.Execute ( contexte );
						}
					}
					else if ( result.Data is CMetteurDeProcessEnPause )
					{
						//Mise en pause du process
						result = contexte.ProcessEnExecution.PauseProcess(contexte);
						result.Data = null;
						return result;
					}
					else
						result.Data = null;
				}
				if ( bIsActionPrincipaleProcess && result)
				{
					CResultAErreur resultTmp = contexte.ProcessEnExecution.EnProcess(contexte);
					if ( !resultTmp )
					{
						result.Erreur += resultTmp.Erreur;
						result.Result = false;
					}
				}
			}
			catch(Exception e )
			{
				result.EmpileErreur ( new CErreurException(e));
			}
			finally
			{
				if ( sessionClient != null )
					sessionClient.ConfigurationsImpression = oldConf;
				if ( !result && bIsActionPrincipaleProcess )
				{
					//Pour ne modifier que le process et ne surtout pas faire les sauvegardes autres
					contexte.ProcessEnExecution.BeginEdit();
					contexte.ProcessEnExecution.Etat = EtatProcess.Erreur;
					contexte.ProcessEnExecution.BrancheEnCours = this;
					contexte.ProcessEnExecution.InfoEtat = result.Erreur.ToString();

					if ( IsModeAsynchrone )
					{
						//Notifie l'utilisateur qu'un erreur est survenue
						CBesoinInterventionProcess intervention = new CBesoinInterventionProcess( contexte.ProcessEnExecution.ContexteDonnee );
						intervention.CreateNewInCurrentContexte();
						intervention.Libelle = I.T("@1 action error|259",contexte.ProcessEnExecution.Libelle);
						intervention.DateDemande = DateTime.Now;
						//Crée une boite de message pour afficher l'erreur
						CProcess process = contexte.Branche.Process;
						CActionMessageBox actionNotif = new CActionMessageBox ( process );
						actionNotif.FormuleMessage=new sc2i.expression.C2iExpressionConstante(intervention.Libelle+"\r\n"+result.Erreur.ToString() );
						actionNotif.Libelle = I.T("Error notification|260");
						process.AddAction ( actionNotif );
						intervention.IdAction = actionNotif.IdObjetProcess;
                        //TESTDBKEYOK
						intervention.KeyUtilisateur = KeyUtilisateur;
						intervention.ProcessEnExecution = contexte.ProcessEnExecution;
						contexte.ProcessEnExecution.BrancheEnCours = contexte.Branche;
					}
					contexte.ProcessEnExecution.CommitEdit();
				}
				if ( IsModeAsynchrone && bFermerLaSessionEnSortant)
				{
					//Ferme la session
					try
					{
						CSessionClient session = CSessionClient.GetSessionForIdSession(contexte.IdSession);
						session.CloseSession();
					}
					catch{}
				}
			}
			result.Data = null;
			return result;
		}

		/// /////////////////////////////////////
		public void StockeDataAction ( int nIdAction, IDonneeAction data )
		{
			m_tableDataAction[nIdAction] = data;
		}

		/// /////////////////////////////////////
		public IDonneeAction GetDataAction ( int nIdAction )
		{
			return (IDonneeAction)m_tableDataAction[nIdAction];
		}
		}
}
