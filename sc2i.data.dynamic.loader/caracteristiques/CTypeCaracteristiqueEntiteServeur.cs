using System;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de Class1.
	/// </summary>
	public class CTypeCaracteristiqueEntiteServeur : CObjetServeurAvecBlob
	{
		/// //////////////////////////////////////////////////
#if PDA
		public CTypeCaracteristiqueEntiteServeur()
			:base ( )
		{
			
		}
#endif
		/// //////////////////////////////////////////////////
		public CTypeCaracteristiqueEntiteServeur( int nIdSession )
			:base ( nIdSession )
		{
			
		}

		//////////////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CTypeCaracteristiqueEntite.c_nomTable;
		}

		//////////////////////////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CTypeCaracteristiqueEntite);
		}

		//////////////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{	
				CTypeCaracteristiqueEntite typeCar = (CTypeCaracteristiqueEntite)objet;
				if ( typeCar.Libelle == "" )
				{
					result.EmpileErreur(I.T("The label of characteritic type should not be empty|161"));
				}
		
				return result;
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
				result.EmpileErreur(I.T("Error in characteristic type data|162"));
			}
			return result;
				
		}


	}
}
