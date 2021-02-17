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
	public class CActionEtiquetterBase : CActionLienSortantSimple
	{
		
		private C2iExpression m_expressionLibelle = null;

		/// /////////////////////////////////////////////////////////
		public CActionEtiquetterBase( CProcess process )
			:base(process)
		{
			Libelle = I.T("Tag the database|20000");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Tag the database|20000"),
				I.T( "Create a tag for the current database|20001"),
				typeof(CActionEtiquetterBase),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
			AddIdVariablesExpressionToHashtable ( m_expressionLibelle, table );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleLibelle
		{
			get
			{
				if ( m_expressionLibelle == null )
					m_expressionLibelle = new C2iExpressionConstante("");
				return m_expressionLibelle;
			}
			set
			{
				m_expressionLibelle = value;
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

			I2iSerializable objet = (I2iSerializable)m_expressionLibelle;
			result = serializer.TraiteObject ( ref objet );
			m_expressionLibelle = (C2iExpression)objet;
		
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;

			//Calcule le fichier ou l'url
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(contexte.Branche.Process);
			contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
			result = FormuleLibelle.Eval(contexteEval);
			if (!result)
			{
				result.EmpileErreur(I.T("Error during the Label evaluation|20002"));
				return result;
			}
			CVersionDonnees version = new CVersionDonnees(contexte.ContexteDonnee);
			version.CreateNew();
			version.CodeTypeVersion = (int)CTypeVersion.TypeVersion.Etiquette;
			version.Libelle = (string)result.Data;
			version.Date = DateTime.Now;
			result = version.CommitEdit();
			if (!result)
				return result;
			foreach (CLienAction lien in GetLiensSortantHorsErreur())
			{
				if (!(lien is CLienUtilisateurAbsent))
				{
					result.Data = lien;
					return result;
				}
			}
			result.Data = null;
			return result;
		}
			

		


	}
}
