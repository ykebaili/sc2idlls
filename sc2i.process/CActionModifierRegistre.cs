using System;
using System.Drawing;
using System.Reflection;
using System.Collections;

using sc2i.drawing;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.data;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionModifierRegistre.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionModifierRegistre : CActionLienSortantSimple
	{
		private string m_strCleRegistre = "";
		private C2iExpression m_expressionValeur = null;
		/// /////////////////////////////////////////
		public CActionModifierRegistre( CProcess process )
			:base(process)
		{
			Libelle = I.T("Register|220");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Write in the register|221"),
				I.T("Allows to store a value in the application register|222"),
				typeof(CActionModifierRegistre),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
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
			serializer.TraiteString ( ref m_strCleRegistre );

			I2iSerializable objet = m_expressionValeur;
			result = serializer.TraiteObject ( ref objet );
			if ( !result )
				return result;
			m_expressionValeur = (C2iExpression)objet;

			return result;
		}

		/// ////////////////////////////////////////////////////////
		public string CleRegistreAModifier
		{
			get
			{
				return m_strCleRegistre;
			}
			set
			{
				m_strCleRegistre = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression ExpressionValeur
		{
			get
			{
				if ( m_expressionValeur == null )
					return new C2iExpressionConstante("");
				return m_expressionValeur;
			}
			set
			{
				m_expressionValeur = value;
			}
		}
		

		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			
			if ( m_strCleRegistre.Trim()=="" )
				result.EmpileErreur(I.T("Indicate the register key to be modified|223"));
					
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			//Calcule la nouvelle valeur
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( Process);
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = ExpressionValeur.Eval ( contexteEval );
			if ( !result )
			{
				result.EmpileErreur(I.T( "Error during @1 formula evaluation|216", ExpressionValeur.ToString()));
				return result;
			}
			object nouvelleValeur = result.Data;
			IDatabaseRegistre registre = (IDatabaseRegistre)C2iFactory.GetNew2iObjetServeur(typeof(IDatabaseRegistre), contexte.IdSession);
			registre.SetValeur ( m_strCleRegistre, nouvelleValeur == null?"":nouvelleValeur.ToString() );
			return result;
		}

		/// ///////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw(ctx);

		}



	}
}
