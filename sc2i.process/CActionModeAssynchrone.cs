using System;
using System.Collections;

using System.Drawing;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.expression;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionMessageBox.
	/// </summary>
	[AutoExec("Autoexec")]
	public class CActionModeAssynchrone : CActionLienSortantSimple
	{
		/// /////////////////////////////////////////////////////////
		public CActionModeAssynchrone( CProcess process )
			:base(process)
		{
			Libelle = I.T("Switch to asynchronous mode|207");
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Asynchronous action|208"),
                I.T("Execute the remaining actions asynchronous mode |209"),
				typeof(CActionModeAssynchrone),
				CGestionnaireActionsDisponibles.c_categorieDeroulement );
		}

		/// ////////////////////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable ( Hashtable table )
		{
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
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

			return result;
		}

		

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			if ( contexte.Branche.IsModeAsynchrone )//On est déjà en asynchrone
				return result;
			//Ouvre une nouvelle session
			CSessionProcessServeurSuivi session = new CSessionProcessServeurSuivi();
			result = session.OpenSession ( new CAuthentificationSessionProcess(),
				I.T("Process @1|198",contexte.ProcessEnExecution.Libelle),
				CSessionClient.GetSessionForIdSession ( contexte.ContexteDonnee.IdSession ));
			if ( !result )
			{
				result.EmpileErreur(I.T("Switch asynchronous mode failure|210"));
				return result;
			}

			//Change l'id de session du contexte de travail
			contexte.ChangeIdSession( session.IdSession );

			if ( GetLiensSortantHorsErreur().Length ==1 )
				result.Data = GetLiensSortantHorsErreur()[0];
			return result;
		}

		/// ////////////////////////////////////////////////////////
		protected override CLienAction[] GetMyLiensSortantsPossibles()
		{
			ArrayList lst = new ArrayList();
			if ( GetLiensSortantHorsErreur().Length == 0 )
				lst.Add ( new CLienAsynchrone(Process) );
			return (CLienAction[]) lst.ToArray(typeof(CLienAction));
		}





	}
}
