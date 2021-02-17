using System;
using System.Collections;

using System.Drawing;
using sc2i.common;
using sc2i.expression;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionAttente.
	/// </summary>
	//[AutoExec("Autoexec")]
	public class CActionAttente : CAction
	{
		private C2iExpression m_formuleDateReprise = null;
		public CActionAttente( CProcess process )
			:base(process)
		{
			Libelle = I.T("Waiting|114");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Put on hold|115"),
				I.T("Process put on hold|116"),
				typeof(CActionAttente),
				CGestionnaireActionsDisponibles.c_categorieDeroulement );
		}

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 1;
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable(FormuleDateReprise, table);
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleDateReprise
		{
			get
			{
				return m_formuleDateReprise;
			}
			set
			{
				m_formuleDateReprise = value;
			}
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
			I2iSerializable obj = m_formuleDateReprise;
			serializer.TraiteObject(ref obj);
			m_formuleDateReprise = (C2iExpression)obj;
			return result;
		}

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = base.VerifieDonnees();
			if (!result)
				return result;
			if (FormuleDateReprise == null || !typeof(DateTime).IsAssignableFrom(FormuleDateReprise.TypeDonnee.TypeDotNetNatif))
			{
				result.EmpileErreur(I.T("The recovery date formula must be return a date|117"));
				return result;
			}
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(contexte.Branche.Process);
			if (FormuleDateReprise == null)
			{
				result.EmpileErreur(I.T("The recovery date formula is incorrect|118"));
				return result;
			}
			result = FormuleDateReprise.Eval(ctxEval);
			if (!result || !typeof(DateTime).IsAssignableFrom(result.Data.GetType()))
			{
				result.EmpileErreur(I.T("Error during the recovery date formula evaluation|119"));
				return result;
			}
			DateTime dateReprise = (DateTime)result.Data;
			/*CHandlerEvenement handler = new CHandlerEvenement(contexte.ContexteDonnee);
			handler.ElementSurveille = contexte.ProcessEnExecution;*/
			
			return result;
		}



	}
}
