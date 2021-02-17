using System;
using System.Collections;

using sc2i.data;
using sc2i.data.serveur;

using sc2i.common;


namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CComportementGeneriqueServeur.
	/// </summary>
	public class CComportementGeneriqueServeur : CObjetServeur
	{
		//-------------------------------------------------------------------
#if PDA
		public CComportementGeneriqueServeur()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CComportementGeneriqueServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CComportementGenerique.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			
			try
			{
				CComportementGenerique comportement = (CComportementGenerique)objet;

				if (comportement.Libelle == "")
					result.EmpileErreur(I.T("The label of the behavior cannot be empty|102"));

				if (!CObjetDonneeAIdNumerique.IsUnique(comportement, CComportementGenerique.c_champLibelle, comportement.Libelle))
					result.EmpileErreur(I.T("A behavior with this label already exists|103"));
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CComportementGenerique);
		}
		//-------------------------------------------------------------------
	}
}
