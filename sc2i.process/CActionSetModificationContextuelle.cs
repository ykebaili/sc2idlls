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
	/// Description résumée de CActionSetModificationContextuelle.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionSetModificationContextuelle : CActionLienSortantSimple
	{
		private C2iExpression m_expressionContexte = null;
		

		/// /////////////////////////////////////////////////////////
		public CActionSetModificationContextuelle( CProcess process )
			:base(process)
		{
            Libelle = I.T("Change editing context|20023");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Change editing context|20023"),
				I.T( "Change the system editing context|20024"),
				typeof(CActionSetModificationContextuelle),
				CGestionnaireActionsDisponibles.c_categorieDeroulement );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
            AddIdVariablesExpressionToHashtable(m_expressionContexte, table);
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleContexte
		{
			get
			{
				if ( m_expressionContexte == null )
					m_expressionContexte = new C2iExpressionConstante("");
				return m_expressionContexte;
			}
			set
			{
				m_expressionContexte = value;
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

			I2iSerializable objet = (I2iSerializable)m_expressionContexte;
			result = serializer.TraiteObject ( ref objet );
			m_expressionContexte = (C2iExpression)objet;
		
			return result;
		}


		/// ////////////////////////////////////////////////////////
        protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			//Calcule le message
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( contexte.Branche.Process);
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = FormuleContexte.Eval ( contexteEval );
            if (!result)
                return result;
            else
                contexte.ContexteDonnee.IdModificationContextuelle = result.Data.ToString();
			return result;
		}



    }
}
