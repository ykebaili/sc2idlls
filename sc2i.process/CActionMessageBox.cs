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
	/// Description résumée de CActionMessageBox.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionMessageBox : CAction
	{
		public enum TypeMessageBox
		{
			OK,
			OuiNon,
			OKAnnuler
		};

		public const string c_idServiceClientMessageBox = "MESSAGE_BOX";

		private C2iExpression m_expressionMessage = null;
		private TypeMessageBox m_typeMessageBox = TypeMessageBox.OK;

        private int m_nSecondesMaxiAffichage = 0;
		
		private string m_strMessageCache = "";

		/// /////////////////////////////////////////////////////////
		public CActionMessageBox( CProcess process )
			:base(process)
		{
			Libelle = I.T("Message box|204");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Show a message box|205"),
				I.T("Show a message box for the user and give a confirmation possibility|206"),
				typeof(CActionMessageBox),
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

        /// ////////////////////////////////////////////////////////
        public int SecondesMaxiAffichage
        {
            get
            {
                return m_nSecondesMaxiAffichage;
            }
            set
            {
                m_nSecondesMaxiAffichage = value;
            }
        }

		/// ////////////////////////////////////////////////////////
		public TypeMessageBox TypeBox
		{
			get
			{
				return m_typeMessageBox;
			}
			set
			{
				m_typeMessageBox = value;
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
			return 1;
            //1 : ajout de secondes maxi affichage
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

			I2iSerializable objet = (I2iSerializable)m_expressionMessage;
			result = serializer.TraiteObject ( ref objet );
			m_expressionMessage = (C2iExpression)objet;
			
			int nTmp = (int)m_typeMessageBox;
			serializer.TraiteInt ( ref nTmp );
			m_typeMessageBox = (TypeMessageBox)nTmp;

            if (nVersion >= 1)
                serializer.TraiteInt(ref m_nSecondesMaxiAffichage);
		
			return result;
		}

		/// ////////////////////////////////////////////////////////
		public string MessageToDisplay
		{
			get
			{
				return m_strMessageCache;
			}
		}

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur ExecuteAction(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			CSessionClient sessionClient = CSessionClient.GetSessionForIdSession ( contexte.IdSession );
			if ( sessionClient != null )
			{
				if (sessionClient.GetInfoUtilisateur().KeyUtilisateur == contexte.Branche.KeyUtilisateur)
				{
					using (C2iSponsor sponsor = new C2iSponsor())
					{
						CServiceSurClient service = sessionClient.GetServiceSurClient(c_idServiceClientMessageBox);
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
								m_strMessageCache = FormuleMessage.GetString();
							}
							else
								m_strMessageCache = result.Data == null ? "" : result.Data.ToString();
							result = service.RunService(this);
							if (!result)
								return result;
							E2iDialogResult dResult = (E2iDialogResult)result.Data;
							foreach (CLienAction lien in GetLiensSortantHorsErreur())
							{
								if (lien is CLienFromDialog &&
									((CLienFromDialog)lien).ResultAssocie == dResult)
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
			foreach ( CLienAction lien in GetLiensSortantHorsErreur() )
			{
				if ( lien is CLienFromDialog )
					tableLiensExistants[((CLienFromDialog)lien).ResultAssocie] = true;
				else
					tableLiensExistants[typeof(CLienUtilisateurAbsent)] = true;
			}
			
			if ( TypeBox == TypeMessageBox.OK || TypeBox == TypeMessageBox.OKAnnuler )
				if ( tableLiensExistants[E2iDialogResult.OK] == null )
					lst.Add ( new CLienFromDialog ( Process, E2iDialogResult.OK ) );

			if ( TypeBox == TypeMessageBox.OKAnnuler )
				if ( tableLiensExistants[E2iDialogResult.Cancel] == null )
					lst.Add ( new CLienFromDialog ( Process, E2iDialogResult.Cancel ) );

			if ( TypeBox == TypeMessageBox.OuiNon )
			{
				if ( tableLiensExistants[E2iDialogResult.Yes] == null )
					lst.Add ( new CLienFromDialog ( Process, E2iDialogResult.Yes ) );
				if ( tableLiensExistants[E2iDialogResult.No] == null )
					lst.Add ( new CLienFromDialog ( Process, E2iDialogResult.No ) );
			}

			if ( tableLiensExistants[typeof(CLienUtilisateurAbsent)] == null)
				lst.Add ( new CLienUtilisateurAbsent(Process) );

			return ( CLienAction[] )lst.ToArray ( typeof(CLienAction) );
		}




	}
}
