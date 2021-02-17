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
	public class CActionSauvegarderDonnees : CActionLienSortantSimple
	{
		/// /////////////////////////////////////////////////////////
		public CActionSauvegarderDonnees( CProcess process )
			:base(process)
		{
			//Libelle = I.T("Save data|237");
			Size = new Size ( 20,20 );
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T( "Save data|237"),
				I.T( "Save modification in the database|238"),
				typeof(CActionSauvegarderDonnees),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
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
			result = contexte.ContexteDonnee.SaveAll(true);
			return result;
		}




	}
}
