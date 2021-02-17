using System;
using System.Collections;

using System.Drawing;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.expression;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionErreur.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionErreur : CAction
	{
		private C2iExpression m_expressionMessage = null;
		

		/// /////////////////////////////////////////////////////////
		public CActionErreur( CProcess process )
			:base(process)
		{
			Libelle = I.T("Generate an Error|171");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Generate an Error|171"),
				I.T( "Stop the action execution with a generated error|172"),
				typeof(CActionErreur),
				CGestionnaireActionsDisponibles.c_categorieDeroulement );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleMessage
		{
			get
			{
				if ( m_expressionMessage == null )
					m_expressionMessage = new C2iExpressionConstante("");
				return m_expressionMessage;
			}
			set
			{
				m_expressionMessage = value;
			}
		}

		
		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			
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

			I2iSerializable objet = (I2iSerializable)m_expressionMessage;
			result = serializer.TraiteObject ( ref objet );
			m_expressionMessage = (C2iExpression)objet;
		
			return result;
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			//Calcule le message
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( contexte.Branche.Process);
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = FormuleMessage.Eval ( contexteEval );
			if ( !result )
				return result;
			else
				result.EmpileErreur ( result.Data.ToString() );
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CLienAction[] GetMyLiensSortantsPossibles()
		{
			return new CLienAction[0];
		}




	}
}
