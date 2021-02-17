using System;
using System.Collections.Generic;
using System.Text;
using sc2i.multitiers.client;
using System.Timers;
using sc2i.common;
using sc2i.data;
using sc2i.process;
using sc2i.data.dynamic;
using System.Runtime.Remoting;
using sc2i.expression;


namespace sc2i.process.serveur
{
	[AutoExec("Autoexec")]
	public class CLanceurProcessCommandLine : MarshalByRefObject, ILanceurProcessCommandLine
	{
		//Tous les arguments en majuscules
		private const string c_ArgHelp = "HELP";
		private const string c_ArgProcess = "PROCESSID";

		public enum EErreurs
		{
			NoParameters = -1,
			ProcessIdIsNotAnInteger = -2,
			MissingArgumentProcessId = -3,
			ErrorWhileInitialisingTimosClient = -4,
			ErrorWhileOpeningSession = -5,
			ProcessDoesntExists = -6,
			InvalidVariableValueOnlySimpleValuesAreAccepted = -7,
			UnknownVariable = -8,
			ErrorWhileRunningProcess = -9
		};


		#region CDonneeSessionProcess
		private class CDonneeSessionProcess : IDisposable
		{
			private CSessionClient m_session;
			private DateTime m_dateDerniereUtilisation = DateTime.Now;
			private bool m_bIsUsed = true;

			public CDonneeSessionProcess(CSessionClient session)
			{
				m_session = session;
			}

			public CSessionClient SessionClient
			{
				get
				{
					return m_session;
				}
			}

			public DateTime DateDerniereUtilisation
			{
				get
				{
					return m_dateDerniereUtilisation;
				}
			}

			public bool IsUsed
			{
				get
				{
					return m_bIsUsed;
				}
			}

			public void StartUsing()
			{
				m_bIsUsed = true;
				m_dateDerniereUtilisation = DateTime.Now;
			}

			public void EndUsing()
			{
				m_bIsUsed = false;
				m_dateDerniereUtilisation = DateTime.Now;
			}

			public void Dispose()
			{
				if (m_session != null)
				{
					m_session.CloseSession();
					m_session.Dispose();
				}
			}
		}
		#endregion

		private static List<CDonneeSessionProcess> m_poolSessions = new List<CDonneeSessionProcess>();
		private static int c_nMinutesAvantSuppressionSessions = 1;
		private static Timer m_timerNettoyage = null;

		public static void Autoexec()
		{
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(CLanceurProcessCommandLine),
					"ILanceurProcessCommandLine", WellKnownObjectMode.SingleCall);
		}


		private static void AssureTimerNettoyage()
		{
			lock (typeof(CLanceurProcessCommandLine))
			{
				if (m_timerNettoyage == null)
				{
					m_timerNettoyage = new Timer(c_nMinutesAvantSuppressionSessions * 1000 * 60);
					m_timerNettoyage.Elapsed += new ElapsedEventHandler(m_timerNettoyage_Elapsed);
					m_timerNettoyage.Start();
				}
			}
		}

		//Nettoyage du pool de sessions
		static void m_timerNettoyage_Elapsed(object sender, ElapsedEventArgs e)
		{
			lock (typeof(CLanceurProcessCommandLine))
			{
				List<CDonneeSessionProcess> copieSessions = new List<CDonneeSessionProcess>();
				foreach (CDonneeSessionProcess donnee in m_poolSessions)
					copieSessions.Add(donnee);
				foreach (CDonneeSessionProcess donneeSession in copieSessions)
				{
					if (!donneeSession.IsUsed)
					{
						TimeSpan sp = DateTime.Now - donneeSession.DateDerniereUtilisation;
						if (sp.TotalMinutes > c_nMinutesAvantSuppressionSessions)
						{
							m_poolSessions.Remove(donneeSession);
							donneeSession.SessionClient.CloseSession();
							donneeSession.Dispose();
						}
					}
				}
			}
		}

		private Dictionary<string, string> m_parametres = new Dictionary<string, string>();

		//-----------------------------------------------------
		/// <summary>
		/// Démarre un process, avec les arguments de ligne de commande de TimosProcess
		/// </summary>
		/// <param name="args"></param>
		/// <returns>Result contient l'erreur, le data du result, contient
		/// le code d'erreur à retourner par TimosProcess</returns>
		public CResultAErreur StartProcess(string[] args)
		{
			CResultAErreur result = CResultAErreur.True;
			int nIdProcess = -1;
			foreach (string strArg in args)
			{
				if (strArg.ToUpper() == c_ArgHelp)
				{
					result = ShowHelp();
					result.Data = 0;
					return result;
				}
				//Trouve la position du signe égal
				int nPos = strArg.IndexOf('=');
				if (nPos < 0)
				{
					result = ShowHelp();
					result.EmpileErreur(I.T("Error in parameters|30007"));
					result.Data = (int)EErreurs.NoParameters;
					return result;
				}
				string strCle = strArg.Substring(0, nPos);
				string strVal = strArg.Substring(nPos + 1);

				if (strCle.ToUpper() == c_ArgProcess)
				{
					try
					{
						nIdProcess = Int32.Parse(strVal);
					}
					catch
					{
						result = ShowHelp();
						result.EmpileErreur(I.T("Error in @1 parameter. Process id should be an integer|30001",c_ArgProcess ));
						result.Data = (int)EErreurs.ProcessIdIsNotAnInteger;
						return result;
					}
				}
				else
				{
					m_parametres[strCle] = strVal;
				}
			}
			if (nIdProcess < 0)
			{
				result.EmpileErreur (I.T("Error :  ProcessId argument not present|30002"));
				result.Data = (int)EErreurs.MissingArgumentProcessId;
				return result;
			}
			return RunProcess(nIdProcess, m_parametres);
		}

		private static CResultAErreur ShowHelp()
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (object value in Enum.GetValues(typeof(EErreurs)))
				result.EmpileErreur((int)value + " : " + CUtilSurEnum.GetNomConvivial(value.ToString()));
			result.EmpileErreur(I.T("Errors : |30003"));
			result.EmpileErreur("[Variable name]=[Variable value] : Specify a process variable value. If the variable name or the variable value contains spaces, use \"|30004");
			result.EmpileErreur(I.T("ProcessId=x : Specify the process to run, where x is the Internal ID of the process to run|30005"));
			result.EmpileErreur(I.T("Help : to display help about TimosProcess|30006"));
			result.EmpileErreur (I.T("TimosProcess command line arguments|30007"));
			return result;
		}

		/// <summary>
		/// Le data du result contient un CDonneeSession
		/// </summary>
		/// <returns></returns>
		private static CResultAErreur GetSession()
		{
			CResultAErreur result = CResultAErreur.True;
			lock (typeof(CLanceurProcessCommandLine))
			{
				foreach (CDonneeSessionProcess donneeSession in m_poolSessions)
				{
					if (!donneeSession.IsUsed)
					{
						donneeSession.StartUsing();
						result.Data = donneeSession;
						return result;
					}
				}
				CSessionProcessServeurSuivi session = new CSessionProcessServeurSuivi();
				result = session.OpenSession(new CAuthentificationSessionProcess(), "TimosProcess", ETypeApplicationCliente.Service);
				if (!result)
				{
					result.Data = (int)EErreurs.ErrorWhileOpeningSession;
					return result;
				}
				CDonneeSessionProcess newDonneeSession = new CDonneeSessionProcess(session);
				newDonneeSession.StartUsing();
				m_poolSessions.Add(newDonneeSession);
				result.Data = newDonneeSession;
				return result;
			}
		}

		//-----------------------------------------------------------
		public static CResultAErreur RunProcess(int nIdProcess, Dictionary<string, string> valeursParametres)
		{
			AssureTimerNettoyage();
			CResultAErreur result = GetSession();
			if (!result)
				return result;
			CDonneeSessionProcess donneeSession = result.Data as CDonneeSessionProcess;
			result = CResultAErreur.True;
			try
			{
				CSessionClient session = donneeSession.SessionClient;
				using (CContexteDonnee contexte = new CContexteDonnee(session.IdSession, true, false))
				{
					CProcessInDb processInDb = new CProcessInDb(contexte);
					if (!processInDb.ReadIfExists(nIdProcess))
					{
						result.EmpileErreur(I.T("The process @1 doesn't exist|30008",nIdProcess.ToString()));
						result.Data = (int)EErreurs.ProcessDoesntExists;
						return result;
					}
					CProcess process = processInDb.Process;
					foreach (KeyValuePair<string, string> parametre in valeursParametres)
					{
						string strParametre = parametre.Key;
						string strValeur = parametre.Value;
						bool bTrouvee = false;
						foreach (IVariableDynamique variable in process.ListeVariables)
						{
							if (strParametre.ToUpper() == variable.Nom.ToUpper())
							{
								if (!variable.TypeDonnee.TypeDotNetNatif.IsValueType && variable.TypeDonnee.TypeDotNetNatif != typeof(string))
								{
									result.EmpileErreur(I.T("Variable @1 cannot be set by TimosProcess. Only simple values can be used|30009",variable.Nom));
									result.Data = (int)EErreurs.InvalidVariableValueOnlySimpleValuesAreAccepted;
									return result;
								}
								try
								{
									object valeur = CUtilTexte.FromUniversalString(strValeur, variable.TypeDonnee.TypeDotNetNatif);
								}
								catch
								{
								}
								process.SetValeurChamp(variable.IdVariable, strValeur);
								bTrouvee = true;
								break;
							}
						}
						if (!bTrouvee)
						{
							result = ShowHelp(process);
							result.EmpileErreur(I.T("Unknown variable @1|30010" , strParametre));
							result.Data = (int)EErreurs.UnknownVariable;
							return result;
						}
					}
					result = CProcessEnExecutionInDb.StartProcess(process, new CInfoDeclencheurProcess(TypeEvenement.Manuel), session.IdSession, null, null);
					if (!result)
					{
						result.Data = (int)EErreurs.ErrorWhileRunningProcess;
						return result;
					}
					if (result.Data != null)
					{
						try
						{
							result.Data = Convert.ToInt32(result.Data);
						}
						catch
						{
						}
						return result;
					}
					else
						result.Data = 0;

				}
			}
			catch (Exception e)
			{
				result.EmpileErreur(new CErreurException(e));
				result.Data = (int)EErreurs.ErrorWhileRunningProcess;
				return result;
			}
			finally
			{
				lock (typeof(CLanceurProcessCommandLine))
				{
					donneeSession.EndUsing();
				}
			}
			return result;
		}

		//-----------------------------------------------------------
		public static CResultAErreur ShowHelp(CProcess process)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (IVariableDynamique variable in process.ListeVariables)
			{
				if (variable.TypeDonnee.TypeDotNetNatif.IsValueType)
				{
					result.EmpileErreur(variable.Nom + " (" + variable.TypeDonnee.TypeDotNetNatif + ")");
				}
			}
			result.EmpileErreur(I.T("Expected arguments for the process|30011"));
			return result;
		}

		public override string ToString()
		{
			return "coucou";
		}
	}
}
