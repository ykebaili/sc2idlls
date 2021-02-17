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
	/// Description résumée de CActionSelectionFichierClient.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionSelectionFichierClient : CActionFonction
	{
		public const string c_idServiceSelectionFichierClient = "SELECT_CLIENT_FILE";

		private C2iExpression m_expressionMessage = null;
        private C2iExpression m_expressionFilter = null;
        private C2iExpression m_expressionRepertoireInitial = null;
        private bool m_bForSave = false;

        private string m_strMessageCache = "";
        private string m_strFiltreCache = "";
        private string m_strRepertoireInitialCache = "";
		
		/// /////////////////////////////////////////////////////////
		public CActionSelectionFichierClient( CProcess process )
			:base(process)
		{
			Libelle = I.T("Select local file|20021");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Select local file|20021"),
				I.T("Select a file name on client computer|20022"),
				typeof(CActionSelectionFichierClient),
				CGestionnaireActionsDisponibles.c_categorieInterface );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
            AddIdVariablesExpressionToHashtable(m_expressionMessage, table);
            AddIdVariablesExpressionToHashtable(m_expressionFilter, table);
            AddIdVariablesExpressionToHashtable(m_expressionRepertoireInitial, table);
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

        /// /////////////////////////////////////////////////////////
        public string MessageToDisplay
        {
            get
            {

                return m_strMessageCache;
            }

        }

        /////////////////////////////////////
        public string FiltreToUse
        {
            get
            {
                return m_strFiltreCache;
            }
        }

        /////////////////////////////////////
        public string InitialDirectory
        {
            get
            {
                return m_strRepertoireInitialCache;
            }
        }


        /// ////////////////////////////////////////////////////////
        public bool ForSave
        {
            get
            {
                return m_bForSave;
            }
            set
            {
                m_bForSave = value;
            }
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

        public C2iExpression FormuleFiltre
        {
            get
            {
                if (m_expressionFilter == null)
                    m_expressionFilter = new C2iExpressionConstante("");
                return m_expressionFilter;
            }
            set
            {
                m_expressionFilter = value;
            }
        }

        public C2iExpression FormuleRepertoireInitial
        {
            get
            {
                if(m_expressionRepertoireInitial == null)
                    m_expressionRepertoireInitial = new C2iExpressionConstante("");
                return m_expressionRepertoireInitial;
            }
            set
            {
                m_expressionRepertoireInitial = value;
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
			//return 0;
            //return 1; // Formule Filtre à appliquer
            return 2; // Formule Répartoire initial
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
            result = serializer.TraiteObject<C2iExpression>(ref m_expressionMessage);
            if (!result)
                return result;
            serializer.TraiteBool(ref m_bForSave);

            if (nVersion >= 1)
            {
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionFilter);
                if (!result)
                    return result;
            }

            if (nVersion >= 2)
            {
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionRepertoireInitial);
                if (!result)
                    return result;
            }

			return result;
		}

        /// ////////////////////////////////////////////////////////
        protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
        {
            return CResultAErreur.True;
        }
		
		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction ( CContexteExecutionAction contexte )
		{
			CResultAErreur result = CResultAErreur.True;
			CSessionClient sessionClient = CSessionClient.GetSessionForIdSession ( contexte.IdSession );
			if ( sessionClient != null )
			{
                //TESTDBKEYOK
				if (sessionClient.GetInfoUtilisateur().KeyUtilisateur == contexte.Branche.KeyUtilisateur)
				{
					using (C2iSponsor sponsor = new C2iSponsor())
					{
						CServiceSurClient service = sessionClient.GetServiceSurClient(c_idServiceSelectionFichierClient);
						if (service != null)
						{
							sponsor.Register(service);
							//Calcule le message
							CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(contexte.Branche.Process);
							contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
							result = FormuleMessage.Eval(contexteEval);
							if (!result)
							{
								result = CResultAErreur.True;
                                result.Data = "##";
							}
                            m_strMessageCache = result.Data as string;

                            result = FormuleFiltre.Eval(contexteEval);
                            if (!result)
                            {
                                result = CResultAErreur.True;
                                result.Data = "*.*";
                            }
                            m_strFiltreCache = result.Data as string;

                            result = FormuleRepertoireInitial.Eval(contexteEval);
                            if (!result)
                            {
                                result = CResultAErreur.True;
                                result.Data = "";
                            }
                            m_strRepertoireInitialCache = result.Data as string;
                            // Lance le service
							result = service.RunService(this);
							if (!result)
								return result;
                            string strNomFichier = result.Data as string;
                            
                            if (VariableResultat != null)
                                Process.SetValeurChamp(VariableResultat , result.Data);
							
                            foreach (CLienAction lien in GetLiensSortantHorsErreur())
							{
								if (lien is CLienFromDialog &&
									((((CLienFromDialog)lien).ResultAssocie == E2iDialogResult.Cancel && strNomFichier=="") ||
                                    (((CLienFromDialog)lien).ResultAssocie == E2iDialogResult.OK && strNomFichier != "" )))
								{
									result.Data = lien;
									return result;
								}
							}
							return result;
						}
					}
				}
			}
			//Utilisateur pas accessible
			foreach ( CLienAction lien in GetLiensSortantHorsErreur() )
			{
				if ( lien is CLienUtilisateurAbsent )
				{
					result.Data = lien;
					return result;
				}
			}
			return result;
		}

        /// ////////////////////////////////////////////////////////
        protected override CLienAction[] GetMyLiensSortantsPossibles()
        {
            ArrayList lst = new ArrayList();
            Hashtable tableLiensExistants = new Hashtable();
            foreach (CLienAction lien in GetLiensSortantHorsErreur())
            {
                if (lien is CLienFromDialog)
                    tableLiensExistants[((CLienFromDialog)lien).ResultAssocie] = true;
                else
                    tableLiensExistants[typeof(CLienUtilisateurAbsent)] = true;
            }

            if (tableLiensExistants[E2iDialogResult.OK] == null)
                    lst.Add(new CLienFromDialog(Process, E2iDialogResult.OK));

            if (tableLiensExistants[E2iDialogResult.Cancel] == null)
                    lst.Add(new CLienFromDialog(Process, E2iDialogResult.Cancel));

            if (tableLiensExistants[typeof(CLienUtilisateurAbsent)] == null)
                lst.Add(new CLienUtilisateurAbsent(Process));

            return (CLienAction[])lst.ToArray(typeof(CLienAction));
        }





        public override CTypeResultatExpression TypeResultat
        {
            get { return new CTypeResultatExpression(typeof(string), false); }
        }

    }
}
