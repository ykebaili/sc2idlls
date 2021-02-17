using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;
namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CSmartFieldServeur : CObjetServeur
	{

#if PDA
		///////////////////////////////////////////////////
		public CSmartFieldServeur (  )
			:base (  )
		{
		}
#endif
		///////////////////////////////////////////////////
		public CSmartFieldServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}

		/// ////////////////////////////////////////////////
		public override string GetNomTable ()
		{
			return CSmartField.c_nomTable;
		}

		/// ////////////////////////////////////////////////
		protected override bool UseCache
		{
			get
			{
				return true;
			}
		}

		///////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CSmartField champ = (CSmartField)objet;
			
				if (champ.Libelle == "")
					result.EmpileErreur(I.T("The custom field name cannot be empty|20006"));

                if (champ.Definition == null)
                {
                    result.EmpileErreur(I.T("Smart field definition is incomplete|20007"));
                }
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}

		public override Type GetTypeObjets()
		{
			return typeof(CSmartField);
		}
	}
}
