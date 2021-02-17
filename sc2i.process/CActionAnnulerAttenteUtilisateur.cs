using System;
using System.Collections;


using sc2i.common;
using sc2i.expression;
using sc2i.data;


namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionAnnulerAttenteUtilisateur.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionAnnulerAttenteUtilisateur : CAction
	{
		private C2iExpression m_formuleCodeAttente = new C2iExpressionConstante("");
		/// /////////////////////////////////////////////////////////
		public CActionAnnulerAttenteUtilisateur( CProcess process )
			:base(process)
		{
			Libelle = I.T("Cancellation of user attempt|102");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Cancel user waiting|103"),
				I.T("Cancel user waiting already created (with waiting code)|104"),
				typeof(CActionAnnulerAttenteUtilisateur),
				CGestionnaireActionsDisponibles.c_categorieInterface );
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


		///////////////////////////////////
		public C2iExpression FormuleCodeAttente
		{
			get
			{
				return m_formuleCodeAttente;
			}
			set
			{
				m_formuleCodeAttente = value;
			}
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

			I2iSerializable objet = m_formuleCodeAttente;
			result = serializer.TraiteObject ( ref objet );
			m_formuleCodeAttente = (C2iExpression)objet;

			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			

			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( contexte.Branche.Process);
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			string strCodeAttente = "";
			if (FormuleCodeAttente != null)
			{
				result = FormuleCodeAttente.Eval(contexteEval);
				if (!result || result.Data == null)
					strCodeAttente = "";
				else
					strCodeAttente = result.Data.ToString();
			}
			//Si le code d'attente n'est pas null et qu'il y a déjà une attente,
			//il faut la supprimer
			if (strCodeAttente != null && strCodeAttente.Trim() != "")
			{
				CFiltreData filtre = new CFiltreData(CBesoinInterventionProcess.c_champCodeAttente + "=@1",
					strCodeAttente);
				CListeObjetsDonnees listeToDelete = new CListeObjetsDonnees(contexte.ContexteDonnee, typeof(CBesoinInterventionProcess));
				listeToDelete.Filtre = filtre;
				if (listeToDelete.Count != 0)
				{
					CObjetDonneeAIdNumerique.Delete(listeToDelete);
				}
			}
			CLienAction[] liens = GetLiensSortantHorsErreur();
			if (liens.Length > 0)
				result.Data = liens[0];
			return result;
		}
	}
}
