using System;
using System.Collections;
using System.Threading;

using sc2i.data;
using sc2i.data.serveur;
using sc2i.multitiers.client;
using sc2i.multitiers.server;
using sc2i.expression;

using sc2i.common;
using sc2i.process;
using sc2i.data.dynamic;

namespace sc2i.process.serveur
{
	/// <summary>
	/// Description résumée de CHandlerEvenementServeur.
	/// </summary>
	[AutoExec("Autoexec", AutoExecAttribute.BackGroundService)]
	public class CHandlerEvenementServeur : CObjetServeur
	{
        //Si une valeur est trouvé pour cette clé, le système ne gère pas les evenements sur date
        public static string c_cleDesactivation = "CHandlerEvenementServeur_Inactif";

		private static Timer m_timer = null;
		private static CSessionClient m_sessionClient = null;
		private static bool m_bIsSuspended = false;

		//-------------------------------------------------------------------
		public CHandlerEvenementServeur( int nIdSession )
			:base ( nIdSession )
		{
		}

		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CHandlerEvenement.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			
			try
			{
				CHandlerEvenement evt = (CHandlerEvenement)objet;
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CHandlerEvenement);
		}
		
		//-------------------------------------------------------------------
		public static void Autoexec()
		{
            if (C2iAppliServeur.GetValeur(c_cleDesactivation) == null)
            {
                m_timer = new Timer(new TimerCallback(OnTachePlanifiee), null, 4*60000, 60000);
                C2iEventLog.WriteInfo("Gestionnaire d'évenements sur date démarré");
            }
		}

		public static void SuspendGestionnaire(bool bValeur)
		{
			m_bIsSuspended = bValeur;
		}
			

		//-------------------------------------------------------------------
		private static bool m_bTraitementEnCours = false;
		private static void OnTachePlanifiee ( object state )
		{
			if ( m_bTraitementEnCours || m_bIsSuspended)
				return;
			m_bTraitementEnCours = true;
			try
			{
				//La session dévolue aux tâches planifiées est fermée régulierement pour
				//Eviter des problèmes de déconnexion (qui ne sont jamais arrivés, mais
				//on ne sait jamais ).
				if (m_sessionClient == null || !m_sessionClient.IsConnected || m_sessionClient.DateHeureConnexion.AddDays(1) < DateTime.Now )
				{
					if(  m_sessionClient != null )
					{
						try
						{
							m_sessionClient.CloseSession();
						}
						catch
						{
						}
					}
							
					//Ouvre une session pour executer l'action
					m_sessionClient = CSessionClient.CreateInstance();
					CResultAErreur result = m_sessionClient.OpenSession ( new CAuthentificationSessionServer(),
						"Evenements sur date",
						ETypeApplicationCliente.Service );
					if ( !result )
					{
						C2iEventLog.WriteErreur("Erreur ouverture de session pour Evenements sur date");
						return;
					}
				}
				try
				{
					using ( CContexteDonnee contexte = new CContexteDonnee ( m_sessionClient.IdSession, true, false ) )
					{
						CListeObjetsDonnees liste = new CListeObjetsDonnees ( contexte, typeof(CHandlerEvenement) );
						liste.Filtre = new CFiltreData ( CHandlerEvenement.c_champDateHeureDeclenchement+" < @1 and "+
							CHandlerEvenement.c_champEtatExecution+"=@2",
							DateTime.Now,
							(int)EtatHandlerAction.AExecuter
							);
						foreach ( CHandlerEvenement handler in liste )
						{
							CResultAErreur result = CResultAErreur.True;
							C2iExpression formuleCondition = handler.FormuleCondition;
							bool bShouldDeclenche = false;
							CContexteEvaluationExpression ctxEval;
							ctxEval = new CContexteEvaluationExpression(handler.ElementSurveille);
							if (formuleCondition != null)
							{
								result = formuleCondition.Eval(ctxEval);
								if (!result)
								{
									bShouldDeclenche = false;
									C2iEventLog.WriteErreur("Erreur déclenchement handler " + handler.Id + " : erreur lors de l'évaluation de la condition");
								}
								else
								{
									if (result.Data is bool)
										bShouldDeclenche = (bool)result.Data;
									else
									{
										bShouldDeclenche = result.Data != null && result.Data.ToString() != "" && result.Data.ToString() != "0";
									}
								}
							}
							if (bShouldDeclenche)
							{
								if (handler.EvenementLie != null)
								{
									CInfoDeclencheurProcess infoDeclencheur = new CInfoDeclencheurProcess();
									infoDeclencheur.TypeDeclencheur = TypeEvenement.Date;
									infoDeclencheur.Info = handler.EvenementLie.Libelle;
									result = handler.EvenementLie.RunEvent(handler.ElementSurveille, infoDeclencheur, null);
								}
								else if (handler.ProcessSource != null)
								{
									result = handler.RunEvent(
                                        false,
                                        handler.ElementSurveille, 
                                        new CInfoDeclencheurProcess(TypeEvenement.Date), 
                                        null);
								}
                                else if (handler.EtapeWorkflowATerminer != null)
                                {
                                    result = handler.RunEvent(
                                        false,
                                        handler.ElementSurveille,
                                        new CInfoDeclencheurProcess(TypeEvenement.Date),
                                        null );
                                }
							}
                            handler.EndHandler(result);
						}
					}
				}
				catch ( Exception e )
				{
					C2iEventLog.WriteErreur("Erreur Taches planifiées "+e.ToString() );
				}
				finally
				{
				}
			}
			finally
			{
				m_bTraitementEnCours = false;
			}
		}
	}
}
