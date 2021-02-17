using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Threading;

using sc2i.multitiers.client;
using sc2i.data;
using sc2i.process;
using sc2i.common;
using sc2i.data.dynamic;
using sc2i.expression;

namespace sc2i.process.web
{
	/// <summary>
	/// Description résumée de ActionService.
	/// </summary>
	public class ActionService : System.Web.Services.WebService
	{
		private static CSessionClient m_session = null;
		private static DateTime m_datePeremptionSession = DateTime.Now;

		private static Timer m_timer = new Timer ( new TimerCallback (OnTimerCloseSession), null, 5*60*1000, 5*60*1000 );

		public const string c_tableActions = "ACTIONS";
		public const string c_champIdAction = "ACTION_ID";
		public const string c_champLibelleAction = "ACTION_NAME";
		public const string c_champDescAction = "ACTION_DESC";
		public const string c_champTypeAction = "ACTION_TYPE";
		public const string c_tableVariables = "VARIABLES";
		public const string c_champIdVariable = "VAR_ID";
		public const string c_champNomVariable = "VAR_NAME";
		public const string c_champTypeVariable = "VAR_TYPE";

		public const string c_tableErreur = "ERROR";


		public ActionService()
		{
			//CODEGEN : Cet appel est requis par le Concepteur des services Web ASP.NET
			InitializeComponent();
		}

		#region Code généré par le Concepteur de composants
		
		//Requis par le Concepteur des services Web 
		private IContainer components = null;
				
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		private class CLockerSession{}

		private static void OnTimerCloseSession ( object state )
		{
			lock ( typeof(CLockerSession ) )
			{
				if ( DateTime.Now > m_datePeremptionSession )
				{
					try
					{
						if ( m_session != null )
							m_session.CloseSession();
					}
					catch
					{
					}
					m_session = null;
				}
			}
		}

		/// <summary>
		/// Le data du result contient la session
		/// </summary>
		/// <returns></returns>
		protected virtual CResultAErreur OpenSession( )
		{
			CResultAErreur result =  CResultAErreur.True;
			CSessionClient session = CSessionClient.CreateInstance();
			result =  session.OpenSession(new CAuthentificationSessionSousSession(0), I.T("Web Session @1|30001",DynamicClassAttribute.GetNomConvivial ( GetType() )), ETypeApplicationCliente.Service );
			if ( result )
				result.Data = session;
			return result;
		}

		private bool AssureSession()
		{
			lock ( typeof(CLockerSession ) )
			{
				m_datePeremptionSession = DateTime.Now.AddMinutes ( 5 );
				if ( m_session == null || !m_session.IsConnected )
				{
					CResultAErreur result =  OpenSession();
					if ( !result )
						m_session = null;
					else
						m_session = (CSessionClient)result.Data;
					return result.Result;
				}
				return true;
			}
		}

		[WebMethod]
		public int GetVersion()
		{
			return 0;
		}

		[WebMethod]
		public DataSet GetActions()
		{
			if ( !AssureSession() )
				return null;
			using ( CContexteDonnee contexte = new CContexteDonnee ( m_session.IdSession, true, false ) )
			{
				CListeObjetsDonnees listeProcess = new CListeObjetsDonnees ( contexte, typeof ( CProcessInDb ) );
                listeProcess.Filtre = new CFiltreData(CProcessInDb.c_champWebVisible + "=@1",
                    true);
				ArrayList lstProcess = new ArrayList();
				DataSet ds = new DataSet();
				DataTable table = new DataTable(c_tableActions);
				ds.Tables.Add ( table );
				DataColumn colParente = table.Columns.Add ( c_champIdAction, typeof(int));
				table.Columns.Add ( c_champLibelleAction, typeof ( string) ) ;
				table.Columns.Add ( c_champDescAction, typeof ( string ) );
				table.Columns.Add ( c_champTypeAction, typeof(string));
				table.PrimaryKey = new DataColumn[]{colParente};

				DataTable tableVariables = new DataTable(c_tableVariables);
				ds.Tables.Add ( tableVariables );
				DataColumn colFille = tableVariables.Columns.Add(c_champIdAction, typeof ( int ) );
				tableVariables.Columns.Add (c_champIdVariable, typeof(int));
				tableVariables.Columns.Add(c_champNomVariable, typeof(string));
				tableVariables.Columns.Add(c_champTypeVariable, typeof(int));

				ds.Relations.Add ( colParente, colFille );
				foreach ( CProcessInDb process in listeProcess )
				{
					if ( process.TypeCible == null && process.Process.VariableDeRetour != null )
					{
						DataRow row =  table.NewRow();
						row[c_champIdAction] = process.Id;
						row[c_champLibelleAction] = process.Libelle;
						row[c_champDescAction] = process.Description;
						row[c_champTypeAction] = process.Process.VariableDeRetour.TypeDonnee.TypeDotNetNatif.ToString();
						table.Rows.Add ( row );
						foreach ( IVariableDynamique variable in process.Process.ListeVariables )
						{
							if ( variable is CVariableDynamiqueSaisie )
							{
								DataRow rowVariable = tableVariables.NewRow();
								rowVariable[c_champIdAction] = process.Id;
								rowVariable[c_champIdVariable] = variable.IdVariable;
								rowVariable[c_champNomVariable] = variable.Nom;
								rowVariable[c_champTypeVariable] = (int)((CVariableDynamiqueSaisie)variable).TypeDonnee2i.TypeDonnee;
								tableVariables.Rows.Add ( rowVariable );
							}
						}
					}
				}
				return ds;
			}
		}

		[WebMethod]
		public string StartSimpleAction ( int nIdAction)
		{
			return StartAction ( nIdAction, new string[0] );
		}
		[WebMethod]
			/* les parametres sont passés dans le table de paramètres :
			 * Id de la variable, suivi de sa valeur( en texte ),...
			 * 
			 */
        public string StartAction(int nIdAction, string[] strParametres)
		{
			DataSet ds = new DataSet();
			if ( !AssureSession() )
			{
                return "ERROR : " + I.T("Session error|30002");
			}
			if ( (strParametres.Length %2) != 0 )
			{
				return "ERROR : " + I.T("The parameters are not passed correctly|30003");
			}

			using ( CContexteDonnee contexte = new CContexteDonnee ( m_session.IdSession, true, false ) )
			{
				CProcessInDb processInDb = new CProcessInDb ( contexte ) ;
				if ( !processInDb.ReadIfExists ( nIdAction ) )
				{
					return "ERROR : " + I.T("The action @1 does not exist|30004",nIdAction.ToString());
				}
				
				CProcess process = processInDb.Process;
				#region Affectation des variables du process
				for ( int nVariable = 0; nVariable < strParametres.Length; nVariable+= 2 )
				{
					string strVariable = strParametres[nVariable];
					IVariableDynamique variable = null;
					try
					{
                        variable = process.GetVariable(strVariable);
					}
					catch
					{
                        foreach (CVariableDynamique varTest in process.ListeVariables)
                        {
                            if (varTest.Nom.ToUpper() == strVariable.ToUpper())
                            {
                                variable = varTest;
                                break;
                            }
                        }
                        if (variable == null)
                        {
                            return "ERROR : " + I.T("@1 is not a valid variable id|30005", strVariable);
                        }
					}
					if ( ! (variable is CVariableDynamiqueSaisie) )
					{
						return "ERROR : " + I.T("The variable @1 is not valid or does not exist|30006", strVariable);
					}

					try
					{
						object valeur = ((CVariableDynamiqueSaisie)variable).TypeDonnee2i.StringToType ( strParametres[nVariable+1], null );
						process.SetValeurChamp ( variable, valeur );
					}
					catch
					{
						return "ERROR : " + I.T("Error in affectation of variable @1|30007",variable.Nom);
					}
				}
				#endregion

				//Note : le service web ne sait pas lancer un process dans une version
				CResultAErreur result = CProcessEnExecutionInDb.StartProcess ( process, new CInfoDeclencheurProcess(TypeEvenement.Manuel), m_session.IdSession, null, null);
				if ( !result )
				{
					return "ERROR : " + result.Erreur.ToString();
				}
				if ( result.Data != null )
				{
                    return result.Data.ToString();
                }
				return null;
			}
		}

		[WebMethod]
		public string GetActionTableName()
		{
			return c_tableActions;
		}


		[WebMethod]
		public string GetActionIdFieldName()
		{
			return c_champIdAction;
		}


		[WebMethod]
        public string GetActionLabelFieldName()
		{
			return c_champLibelleAction;
		}


		[WebMethod]
		public string GetActionDescpritionFieldName()
		{
			return c_champDescAction;
		}


		[WebMethod]
		public string GetActionTypeFieldName()
		{
			return c_champTypeAction;
		}


		[WebMethod]
        public string GetVariablesTableNameName()
		{
			return c_tableVariables;
		}


		[WebMethod]
		public string GetVariableIdFieldName()
		{
			return c_champIdVariable;
		}


		[WebMethod]
		public string GetVariableNameFieldName()
		{
			return c_champNomVariable;
		}


		[WebMethod]
		public string GetVariableTypeFieldName()
		{
			return c_champTypeVariable;
		}


	}
}
