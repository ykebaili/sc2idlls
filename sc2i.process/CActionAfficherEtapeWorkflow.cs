using System;
using System.Drawing;
using System.Collections;

using sc2i.common;
using sc2i.expression;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.drawing;
using sc2i.multitiers.client;
using sc2i.process;
using sc2i.process.workflow;


namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionModifierPropriete.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionAfficheEtapeWorkflow : CAction
	{
		public static string c_idServiceClientAfficheEtapeWorkflow = "ACTION_AFF_ETAPE_WKF";

		private C2iExpression m_expressionEtapeAAfficher = null;
        private bool m_bDansNouvelOnglet = false;


		[Serializable]
		public class CParametreAffichageEtapeWorkflow
		{
            public readonly int IdSession;
			public readonly int IdEtapeWorkflow;
            public readonly bool DansNouvelOnglet;
			
            public CParametreAffichageEtapeWorkflow(
                int nIdSession,
                int nIdEtapeworkflow,
                bool bDansNouvelOnglet)
			{
                IdSession = nIdSession;
                IdEtapeWorkflow = nIdEtapeworkflow;
                DansNouvelOnglet = bDansNouvelOnglet;
			}
		}

		/// /////////////////////////////////////////
		public CActionAfficheEtapeWorkflow( CProcess process )
			:base(process)
		{
			Libelle = I.T("Display workflow step|20086");
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Display workflow step|20086"),
				I.T("Display a workflow step on client|20087"),
				typeof(CActionAfficheEtapeWorkflow),
				CGestionnaireActionsDisponibles.c_categorieInterface );
		}

		/// /////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
		{
			AddIdVariablesExpressionToHashtable ( m_expressionEtapeAAfficher, table );
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

            result = serializer.TraiteObject<C2iExpression>(ref m_expressionEtapeAAfficher);
            if (!result )
                return result;
            serializer.TraiteBool(ref m_bDansNouvelOnglet);
			return result;
		}

        /// /////////////////////////////////////////
        public bool DansNouvelOnglet
        {
            get { return m_bDansNouvelOnglet; }
            set { m_bDansNouvelOnglet = value; }
        }


		/// /////////////////////////////////////////
		public C2iExpression FormuleEtapeAAfficher
		{
			get
			{
				return m_expressionEtapeAAfficher;
			}
			set
			{
				m_expressionEtapeAAfficher = value;
			}
		}



		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = CResultAErreur.True;

			if (m_expressionEtapeAAfficher == null )
				result.EmpileErreur(I.T( "Incorrect formula for workflow step to display|20088"));
            else if ( !typeof(CEtapeWorkflow).IsAssignableFrom(m_expressionEtapeAAfficher.TypeDonnee.TypeDotNetNatif))
                result.EmpileErreur(I.T("Incorrect formula for workflow step to display|20088"));
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			//Si la session qui execute est une session de l'utilisateur associé à la branche,
			//Tente d'afficher le AfficheEtapeWorkflow sur cette session,
			//Sinon, enregistre une Intervention sur l'utilisateur
			CSessionClient sessionClient = CSessionClient.GetSessionForIdSession ( contexte.IdSession );
			if ( sessionClient != null )
			{
                //TESTDBKEYOK
				if ( sessionClient.GetInfoUtilisateur().KeyUtilisateur == contexte.Branche.KeyUtilisateur )
				{
					using (C2iSponsor sponsor = new C2iSponsor())
					{

						CServiceSurClient service = sessionClient.GetServiceSurClient(c_idServiceClientAfficheEtapeWorkflow);
						if (service != null)
						{
							sponsor.Register(service);
							//Evalue l'élément à éditer
							CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(Process);
							if (m_expressionEtapeAAfficher == null)
							{
								result.EmpileErreur(I.T( "Incorrect formula for workflow step to display|20157"));
								return result;
							}
							result = m_expressionEtapeAAfficher.Eval(contexteEval);
                            if (!result)
                                return result;
                            CEtapeWorkflow etape = result.Data as CEtapeWorkflow;

                            contexte.AddServiceALancerALaFin(new CContexteExecutionAction.CParametreServiceALancerALaFin(
                                service,
                                new CParametreAffichageEtapeWorkflow(
                                    contexte.IdSession,
                                    etape.Id,
                                    DansNouvelOnglet)));
                            /*
							result = service.RunService(
                                new CParametreAffichageEtapeWorkflow(
                                    contexte.IdSession,
                                    etape.Id,
                                    DansNouvelOnglet));*/
							//Fin du process
							result.Data = null;
							if (result)
							{
								foreach (CLienAction lien in this.GetLiensSortantHorsErreur())
									if (!(lien is CLienUtilisateurAbsent))
										result.Data = lien;
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

		/// ///////////////////////////////////////////////
		protected override void MyDraw(CContextDessinObjetGraphique ctx)
		{
			base.MyDraw(ctx);
		}

		/// /////////////////////////////////////////////////////////
		protected override CLienAction[] GetMyLiensSortantsPossibles()
		{
			CLienAction[] listeLiens = GetLiensSortantHorsErreur();
			bool bHasLienUtilisateurAbsent = false;
			bool bHasLienStd = false;
			foreach ( CLienAction lien in listeLiens )
			{
				if ( lien is CLienUtilisateurAbsent )
					bHasLienUtilisateurAbsent = true;
				else
					bHasLienStd = true;
			}
//			if (DansNavigateurPrincipal)//Si dans navigateur principal, on est forcement en sortie de process
				bHasLienStd = true;

			ArrayList lst = new ArrayList();
			if ( !bHasLienStd )
				lst.Add ( new CLienAction ( Process ) );
			if ( !bHasLienUtilisateurAbsent )
				lst.Add ( new CLienUtilisateurAbsent ( Process ) );

			return ( CLienAction[] )lst.ToArray(typeof(CLienAction));
		}




	}
}
