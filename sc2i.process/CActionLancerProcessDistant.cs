using System;
using System.Collections;
using System.Threading;
using System.Data;


using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;
using sc2i.multitiers.client;

//SC 07/01/2013 : plus utilisable car les actions web retourne maintenant
//du texte simple
/*

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionSousProcess.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionLancerProcessDistant : CActionFonction
	{
		public class CInfoProcessDistant
		{
			public readonly string URL= "";
			public readonly int IdProcess = -1;
			private string m_strNomProcess = "";
			public readonly Type TypeRetour = typeof(string);
			public readonly CVariableDynamiqueSaisie[] Variables = new CVariableDynamiqueSaisie[0];

			public CInfoProcessDistant ( 
				string strURL, 
				int nIdProcess, 
				string strNomProcess, 
				Type typeRetour,
				CVariableDynamiqueSaisie[] variables )
			{
				URL = strURL;
				IdProcess = nIdProcess;
				m_strNomProcess = strNomProcess;
				TypeRetour = typeRetour;
				Variables = variables;
			}

			public override int GetHashCode()
			{
				return (URL+IdProcess.ToString()).GetHashCode();
			}

			public override bool Equals(object obj)
			{
				if ( obj == null )
					return false;
				return GetHashCode() == obj.GetHashCode();
			}

			public string NomProcess
			{
				get
				{
					return m_strNomProcess;
				}
			}


		}


		private string m_strURL = "";
		private string m_strNomProcess = "";
		private Type m_typeRetourProcess = typeof(string);
		private int m_nIdProcess = -1;

		//Id variable process -> formule
		private Hashtable m_mapVariablesProcessToFormule = new Hashtable();

		private CVariableDynamiqueSaisie[] m_variablesDistantes = new CVariableDynamiqueSaisie[0];

		/// /////////////////////////////////////////////////////////
		public CActionLancerProcessDistant( CProcess process )
			:base(process)
		{
			Libelle = I.T("Distant action|199");
			m_mapVariablesProcessToFormule = new Hashtable();
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Distant action execution|200"),
				I.T("Start a distant action (via the WEB)|201"),
				typeof(CActionLancerProcessDistant),
				CGestionnaireActionsDisponibles.c_categorieDeroulement );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			foreach ( C2iExpression expression in m_mapVariablesProcessToFormule.Values )
				AddIdVariablesExpressionToHashtable ( expression, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// /////////////////////////////////////////////////////////
		public override CTypeResultatExpression TypeResultat
		{
			get
			{
				return new CTypeResultatExpression ( m_typeRetourProcess, false );
			}
		}

		/// /////////////////////////////////////////
		public string NomProcess
		{
			get
			{
				return m_strNomProcess;
			}
			set
			{
				m_strNomProcess = value;
			}
		}

		/// /////////////////////////////////////////
		public Type TypeRetourProcess
		{
			get
			{
				return m_typeRetourProcess;
			}
			set
			{
				m_typeRetourProcess = value;
			}
		}

		/// /////////////////////////////////////////
		public int IdProcess
		{
			get
			{
				return m_nIdProcess;
			}
			set
			{
				m_nIdProcess = value;
			}
		}

		/// /////////////////////////////////////////
		public string URL
		{
			get
			{
				return m_strURL;
			}
			set
			{
				m_strURL = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public CVariableDynamiqueSaisie[] VariablesDistantes
		{
			get
			{
				return m_variablesDistantes;
			}
			set
			{
				m_variablesDistantes = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression GetExpressionForVariableProcess ( int nIdVariable )
		{
			return ( C2iExpression )m_mapVariablesProcessToFormule[nIdVariable];
		}

		/// ////////////////////////////////////////////////////////
		public void SetExpressionForVariableProcess ( int nIdVariable, C2iExpression expression )
		{
			m_mapVariablesProcessToFormule[nIdVariable] = expression;
		}

		/// ////////////////////////////////////////////////////////
		public void ClearExpressionsVariables()
		{
			m_mapVariablesProcessToFormule.Clear();
		}


		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize( serializer );
			if ( !result )
				return result;

			serializer.TraiteInt ( ref m_nIdProcess );

			serializer.TraiteString ( ref m_strNomProcess );

			serializer.TraiteString ( ref m_strURL );

			serializer.TraiteType( ref m_typeRetourProcess );

			ArrayList lst = new ArrayList ( m_variablesDistantes );
			result = serializer.TraiteArrayListOf2iSerializable ( lst );
			if ( serializer.Mode == ModeSerialisation.Lecture )
			{
				if ( lst.Count > 0 )
					m_variablesDistantes = (CVariableDynamiqueSaisie[])lst.ToArray(typeof(CVariableDynamiqueSaisie));
				else
					m_variablesDistantes = new CVariableDynamiqueSaisie[0];
			}

			I2iSerializable objet = null;
			int nNbVariables = m_mapVariablesProcessToFormule.Count;
			serializer.TraiteInt ( ref nNbVariables );
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture :
					foreach ( int nId in m_mapVariablesProcessToFormule.Keys )
					{
						int nTmp = nId;
						serializer.TraiteInt ( ref nTmp );
						objet = GetExpressionForVariableProcess ( nId );
						result = serializer.TraiteObject ( ref objet );
						if ( !result )
							return result;
					}
					break;
				case ModeSerialisation.Lecture :
					m_mapVariablesProcessToFormule.Clear();
					for ( int nVar = 0; nVar < nNbVariables; nVar++ )
					{
						int nIdVariable = 0;
						serializer.TraiteInt ( ref nIdVariable );
						objet = null;
						result = serializer.TraiteObject ( ref objet );
						if ( !result )
							return result;
						SetExpressionForVariableProcess ( nIdVariable, (C2iExpression)objet);
					}
					break;
			}
		
			return result;
		}

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_nIdProcess <= 0 )
			{
				result.EmpileErreur(I.T("Invalid distant action|202"));
				return result;
			}
			
			//Vérifie le type des variables
			foreach ( int nIdVariable in m_mapVariablesProcessToFormule.Keys )
			{
				IVariableDynamique variable = null;
				foreach ( IVariableDynamique varTest in VariablesDistantes )
				{
					if ( varTest.Id == nIdVariable )
					{
						variable = varTest;
						break;
					}
				}
				if ( variable !=  null )
				{
					CTypeResultatExpression typeVariable = variable.TypeDonnee;
					C2iExpression expression = (C2iExpression)m_mapVariablesProcessToFormule[nIdVariable];
					if ( expression != null )
					{
						if ( !expression.TypeDonnee.Equals(typeVariable) )
						{
							result.EmpileErreur(I.T( "The formula of '@1' variable value must return a @2 type|193", variable.Nom, typeVariable.ToStringConvivial()));
						}
					}
				}
			}
			return result;
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
				
			ArrayList lstValeurs = new ArrayList();
			//Remplit les variables du process
			foreach ( int nIdVariable in m_mapVariablesProcessToFormule.Keys )
			{

				CVariableDynamiqueSaisie variable = null;
				foreach ( CVariableDynamiqueSaisie vari in m_variablesDistantes )
					if ( vari.Id == nIdVariable )
					{
						variable =  vari;
						break;
					}
				if ( variable != null )
				{
					//Evalue la formule
					C2iExpression expression = (C2iExpression)m_mapVariablesProcessToFormule[nIdVariable];
					CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process);
								contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
					result = expression.Eval ( contexteEval );
					if ( !result )
					{
						result.EmpileErreur(I.T( "Error during the variables assignment in sub action|197"));
						result.EmpileErreur(I.T( "@1 variable error|196", variable.Nom));
						return result;
					}
					if ( result.Data != null )
					{
						lstValeurs.Add ( nIdVariable.ToString() );
						lstValeurs.Add ( result.Data.ToString() );
					}
				}
			}

			ActionDistante.ActionService service = new ActionDistante.ActionService();
			service.Url = m_strURL;

			try
			{
				DataSet ds = service.ExecuteAction ( m_nIdProcess, (string[])lstValeurs.ToArray(typeof(string) ) );
				string strTableErreur = service.GetNomTableErreurs();
				DataTable table = ds.Tables[strTableErreur];
				if ( table != null && table.Rows.Count > 0 )
				{
					foreach ( DataRow row in table.Rows )
						result.EmpileErreur ( row[0].ToString() );
					return result;
				}
				if ( ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 1 &&
					ds.Tables[0].Columns.Count == 1 )
					Process.SetValeurChamp ( IdVariableResultat, ds.Tables[0].Rows[0][0] );
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
				return result;
			}
			return result;
		}

		/// //////////////////////////////////////////////////
		public static CInfoProcessDistant[] GetProcessDispo ( string strURL )
		{
			try
			{
				ActionDistante.ActionService service = new ActionDistante.ActionService();
				service.Url = strURL;
				string c_tableActions = service.GetNomTableActions();
				string c_champIdAction = service.GetNomChampIdAction();
				string c_champLibelleAction = service.GetNomChampLibelleAction();
				string c_champDescAction = service.GetNomChampDescAction();
				string c_champTypeAction = service.GetNomChampTypeAction();
				string c_tableVariables = service.GetNomTableVariables();
				string c_champIdVariable = service.GetNomChampIdVariable();
				string c_champNomVariable = service.GetNomChampNomVariable();
				string c_champTypeVariable = service.GetNomChampTypeVariable();

				DataSet ds = service.GetActions();
				if ( ds == null )
					return null;
				
				DataTable table = ds.Tables[c_tableActions];
				ArrayList lstActions = new ArrayList();
				foreach ( DataRow row in table.Rows )
				{
					DataRow[] rowsVar = row.GetChildRows ( table.ChildRelations[0] );
					ArrayList lstVariables = new ArrayList();
					foreach ( DataRow rowVar in rowsVar )
					{
						CVariableDynamiqueSaisie variable = new CVariableDynamiqueSaisie();
						variable.Id = (int)rowVar[c_champIdVariable];
						variable.Nom = (string)rowVar[c_champNomVariable];
						variable.TypeDonnee2i = new C2iTypeDonnee ( (TypeDonnee)(int)rowVar[c_champTypeVariable] );
						lstVariables.Add ( variable );
					}
					CInfoProcessDistant info = new CInfoProcessDistant(
						strURL,
						(int)row[c_champIdAction],
						(string)row[c_champLibelleAction],
						CActivatorSurChaine.GetType((string)row[c_champTypeAction]),
						(CVariableDynamiqueSaisie[])lstVariables.ToArray ( typeof ( CVariableDynamiqueSaisie )) );
					lstActions.Add ( info );
				}
				return ( CInfoProcessDistant[] )lstActions.ToArray ( typeof ( CInfoProcessDistant ));

			}
			catch ( Exception e )
			{
				string strTmp = e.ToString();
				return null;
			}
		}
	}
}
*/