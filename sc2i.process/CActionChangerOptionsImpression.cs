using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

using System.Drawing;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.data.dynamic;
using sc2i.expression;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionChangerOptionsImpression.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionChangerOptionsImpression : CActionLienSortantSimple
	{
		public C2iExpression m_formuleImprimanteClient = null;
		public C2iExpression m_formuleImprimanteServeur =  null;

		/// /////////////////////////////////////////////////////////
		public CActionChangerOptionsImpression( CProcess process )
			:base(process)
		{
			Libelle = I.T("Change print settings|123");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Change print settings|123"),
				I.T( "Allow to select the server printer and client printer which will be used by the process in progress|124"),
				typeof(CActionChangerOptionsImpression),
				CGestionnaireActionsDisponibles.c_categorieInterface );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable ( m_formuleImprimanteClient, table );
			AddIdVariablesExpressionToHashtable ( m_formuleImprimanteServeur, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleImprimanteClient
		{
			get
			{
				return m_formuleImprimanteClient;
			}
			set
			{
				m_formuleImprimanteClient = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleImprimanteServeur
		{
			get
			{
				return m_formuleImprimanteServeur;
			}
			set
			{
				m_formuleImprimanteServeur = value;
			}
		}

		

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			if ( FormuleImprimanteClient != null && 
				FormuleImprimanteClient.TypeDonnee.TypeDotNetNatif != typeof(string) )
				result.EmpileErreur(I.T("The client printer formula must be return a text|125"));
			if ( FormuleImprimanteServeur != null && 
				FormuleImprimanteServeur.TypeDonnee.TypeDotNetNatif != typeof(string) )
				result.EmpileErreur(I.T( "The server printer formula must be return a text|126"));
			return result;
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

			I2iSerializable objet = (I2iSerializable)m_formuleImprimanteClient;
			result = serializer.TraiteObject ( ref objet );
			m_formuleImprimanteClient = (C2iExpression)objet;

			 objet = (I2iSerializable)m_formuleImprimanteServeur;
			result = serializer.TraiteObject ( ref objet );
			m_formuleImprimanteServeur = (C2iExpression)objet;
			
		
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression ( Process);
			ctxEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			string strImprimanteClient = "";
			string strImprimanteServeur = "";
			if ( FormuleImprimanteClient != null )
			{
				result = FormuleImprimanteClient.Eval ( ctxEval );
				if ( !result )
				{
					result.EmpileErreur (I.T("Error during the client printer evaluation|127")) ;
					return result;
				}
				strImprimanteClient = result.Data.ToString();
			}
			if ( FormuleImprimanteServeur != null )
			{
				result = FormuleImprimanteServeur.Eval ( ctxEval );
				if (!result )
				{
					result.EmpileErreur(I.T( "Error during the server printer evaluation|128"));
					return result;
				}
				strImprimanteServeur = result.Data.ToString();
			}
			
			
			CConfigurationsImpression config;
			if ( contexte.Branche.ConfigurationImpression != null )
				config = (CConfigurationsImpression)CCloner2iSerializable.Clone ( contexte.Branche.ConfigurationImpression );
			else
				config = new CConfigurationsImpression();
			if ( strImprimanteClient != "" )
				config.NomImprimanteSurClient = strImprimanteClient;
			if ( strImprimanteServeur != "" )
				config.NomImprimanteSurServeur = strImprimanteServeur;
			contexte.Branche.ConfigurationImpression = config;

			CSessionClient session = CSessionClient.GetSessionForIdSession ( contexte.IdSession );
			if ( session != null )
				session.ConfigurationsImpression = config;

			return result;
		}

	}
}
