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
	/// Description résumée de CActionPurgeHistoriqueProcess.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionPurgeHistoriqueProcess : CActionLienSortantSimple
	{
		private C2iExpression m_expressionDateLimite = null;

		/// /////////////////////////////////////////////////////////
		public CActionPurgeHistoriqueProcess( CProcess process )
			:base(process)
		{
			Libelle = I.T("Clear process history|20007");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Clear process history|20007"),
				I.T("Clear process history|20007"),
				typeof(CActionPurgeHistoriqueProcess),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable(m_expressionDateLimite, table);
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleDateLimite
		{
			get
			{
				if ( m_expressionDateLimite == null )
					m_expressionDateLimite = new C2iExpressionConstante("");
				return m_expressionDateLimite;
			}
			set
			{
				m_expressionDateLimite = value;
			}
		}

			/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			if (FormuleDateLimite == null )
			{
				result.EmpileErreur(I.T("You have to specify a limit date|20008"));
			}
			else if ( FormuleDateLimite.TypeDonnee.TypeDotNetNatif != typeof(DateTime) )
			{
				result.EmpileErreur ( I.T("Bad limit date|20009"));
			}
			
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

			I2iSerializable objet = (I2iSerializable)m_expressionDateLimite;
			result = serializer.TraiteObject ( ref objet );
			m_expressionDateLimite = (C2iExpression)objet;
			
	
			return result;
		}

		
		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur  MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(contexte.Branche.Process);
			contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
			if (FormuleDateLimite == null)
			{
				result.EmpileErreur(I.T("You have to specify a limit date|20008"));
				return result;
			}
			result = FormuleDateLimite.Eval(contexteEval);
			if (!result)
				return result;
			if (!(result.Data is DateTime))
			{
				result.EmpileErreur(I.T("Bad limit date|20009"));
				return result;
			}
			result = CProcessEnExecutionInDb.Purger((DateTime)result.Data, contexte.IdSession);
			return result;
		}
	}
}
