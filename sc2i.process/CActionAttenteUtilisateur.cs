using System;
using System.Collections;


using sc2i.common;
using sc2i.expression;
using sc2i.data;
using sc2i.multitiers.client;


namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionAttenteUtilisateur.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionAttenteUtilisateur : CAction
	{
		private C2iExpression m_formuleLibelle = new C2iExpressionConstante ("");
		private C2iExpression m_formuleCodeAttente = new C2iExpressionConstante("");
        private C2iExpression m_formuleIdUtilisateur = null;

		/// /////////////////////////////////////////////////////////
		public CActionAttenteUtilisateur( CProcess process )
			:base(process)
		{
			Libelle = I.T("User waiting|120");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Wait the user presence|121"),
				I.T("Indicate to the user that the action needs him and puts the action on standby|122"),
				typeof(CActionAttenteUtilisateur),
				CGestionnaireActionsDisponibles.c_categorieInterface );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
            if ( m_formuleIdUtilisateur != null )
                AddIdVariablesExpressionToHashtable(m_formuleIdUtilisateur, table);
            if (m_formuleLibelle != null)
                AddIdVariablesExpressionToHashtable(m_formuleLibelle, table);
            if (m_formuleCodeAttente != null)
                AddIdVariablesExpressionToHashtable(m_formuleCodeAttente, table);
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}


		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleMessage
		{
			get
			{
				return m_formuleLibelle;
			}
			set
			{
				m_formuleLibelle = value;
			}
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

        /// /////////////////////////////////////////////////////////
        public C2iExpression FormuleUtilisateur
        {
            get
            {
                return m_formuleIdUtilisateur;
            }
            set
            {
                m_formuleIdUtilisateur = value;
            }
        }

		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 2;
			//1 : Ajout du code d'attente
            //2 : Ajout de la formule utilisateur
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

			I2iSerializable objet = m_formuleLibelle;
			result = serializer.TraiteObject ( ref objet );
			m_formuleLibelle = (C2iExpression)objet;

			if (nVersion >= 1)
			{
				objet = m_formuleCodeAttente;
				result = serializer.TraiteObject(ref objet);
				m_formuleCodeAttente = (C2iExpression)objet;
                if (!result)
                    return result;
			}
            if (nVersion >= 2)
            {
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleIdUtilisateur);
                if (!result)
                    return result;
            }

			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			

			//Calcule le message
			string strMessage = "";
			CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression ( contexte.Branche.Process);
						contexteEval.AttacheObjet ( typeof(CContexteDonnee), contexte.ContexteDonnee );
			result = FormuleMessage.Eval ( contexteEval );
			if ( !result )
			{
				result = CResultAErreur.True;
				strMessage = FormuleMessage.GetString();
			}
			else
				strMessage = result.Data==null?"":result.Data.ToString();

			string strCodeAttente = "";
			if (FormuleCodeAttente != null)
			{
				result = FormuleCodeAttente.Eval(contexteEval);
				if (!result || result.Data == null)
					strCodeAttente = "";
				else
					strCodeAttente = result.Data.ToString();
			}

            //TESTDBKEYOK (SC)
            CDbKey keyUtilisateur = contexte.Branche.KeyUtilisateur;
            if (m_formuleIdUtilisateur != null)
            {
                //TESTDBKEYOK 
                
                result = m_formuleIdUtilisateur.Eval(contexteEval);
                if (result )
                {
                    if ( result.Data is int)
                    {
                        keyUtilisateur = CUtilInfosUtilisateur.GetKeyUtilisateurFromId((int)result.Data);
                    }
                    if ( result.Data is string )
                    {
                        keyUtilisateur = CDbKey.CreateFromStringValue((string)result.Data);
                    }
                    if ( result.Data is CDbKey )
                        keyUtilisateur = (CDbKey)result.Data;
                    if (keyUtilisateur != null)
                        contexte.Branche.KeyUtilisateur = keyUtilisateur;
                }
            }

			//Si le code d'attente n'est pas null et qu'il y a déjà une attente,
			//il faut la supprimer
			if (strCodeAttente != null && strCodeAttente.Trim() != "")
			{
				CFiltreData filtre = new CFiltreData(
					CBesoinInterventionProcess.c_champCodeAttente + "=@1",
					strCodeAttente);
				CListeObjetsDonnees listeToDelete = new CListeObjetsDonnees(contexte.ContexteDonnee, typeof(CBesoinInterventionProcess));
				listeToDelete.Filtre = filtre;
				if (listeToDelete.Count != 0)
				{
					CObjetDonneeAIdNumerique.Delete(listeToDelete);
				}
			}

			CBesoinInterventionProcess intervention = new CBesoinInterventionProcess(contexte.ContexteDonnee);
			intervention.CreateNewInCurrentContexte();
			intervention.ProcessEnExecution = contexte.ProcessEnExecution;
			//TESTDBKEYOK
            intervention.KeyUtilisateur = keyUtilisateur;
			intervention.DateDemande = DateTime.Now;
			intervention.CodeAttente = strCodeAttente;
			intervention.Libelle = strMessage;
			CLienAction[] liens = GetLiensSortantHorsErreur();
			if ( liens.Length == 1 )
				intervention.IdAction = liens[0].ActionArrivee.IdObjetProcess;
			else
				intervention.IdAction = -1;
			
			//Mise du process en pause !
			result.Data = new CMetteurDeProcessEnPause(  );
			return result;
		}
	}
}
