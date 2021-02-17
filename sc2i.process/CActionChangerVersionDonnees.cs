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
	/// Ouvre un fichier ou une URL sur le poste client
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionChangerVersionDonnees : CActionLienSortantSimple
	{
		

		private C2iExpression m_expressionVersion = null;

		/// /////////////////////////////////////////////////////////
		public CActionChangerVersionDonnees(CProcess process)
			:base(process)
		{
			Libelle = I.T("Change active version|304");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Change active version|304"),
				I.T( "Change the current process data version|305"),
				typeof(CActionChangerVersionDonnees),
				CGestionnaireActionsDisponibles.c_categorieDonnees);
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable ( m_expressionVersion, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleVersion
		{
			get
			{
				if ( m_expressionVersion == null )
					m_expressionVersion = new C2iExpressionConstante(null);
				return m_expressionVersion;
			}
			set
			{
				m_expressionVersion = value;
			}
		}

		
		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;
			if ( m_expressionVersion != null )
			{
				if ( m_expressionVersion.TypeDonnee.TypeDotNetNatif != typeof(int) &&
					m_expressionVersion.TypeDonnee.TypeDotNetNatif != typeof(CVersionDonnees) &&
					(m_expressionVersion.GetType()!= typeof(C2iExpressionNull) ))
					result.EmpileErreur(I.T("Version formula should return a version, a version id or null|306"));
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

			I2iSerializable objet = (I2iSerializable)m_expressionVersion;
			result = serializer.TraiteObject ( ref objet );
			m_expressionVersion = (C2iExpression)objet;
		
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur  MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			result = contexte.ContexteDonnee.SaveAll(true);
			if (!result)
				return result;
			//Calcule la version
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( contexte.Branche.Process );
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = FormuleVersion.Eval ( contexteEval );
			if ( !result )
			{
				result.EmpileErreur(I.T("Error during the version formula evaluation|307"));
				return result;
			}
			int? nIdVersion = null;
			if (result.Data is int)
				nIdVersion = (int)result.Data;
			if (result.Data is CVersionDonnees)
				nIdVersion = ((CVersionDonnees)result.Data).Id;
			result = contexte.ContexteDonnee.SetVersionDeTravail(nIdVersion, true);
			if (!result)
				return result;

			return result;
		}

		




	}
}
