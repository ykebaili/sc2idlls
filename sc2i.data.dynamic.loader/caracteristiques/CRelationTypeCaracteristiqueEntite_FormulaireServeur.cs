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
	public class CRelationTypeCaracteristiqueEntite_FormulaireServeur : CObjetServeur
	{
		/// //////////////////////////////////////////////////
#if PDA
		public CRelationTypeCaracteristiqueEntite_FormulaireServeur()
			:base ()
		{
			
		}
#endif
		/// //////////////////////////////////////////////////
		public CRelationTypeCaracteristiqueEntite_FormulaireServeur( int nIdSession )
			:base ( nIdSession )
		{
			
		}

		//////////////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return CRelationTypeCaracteristiqueEntite_Formulaire.c_nomTable;
		}

		//////////////////////////////////////////////////////////////////////
		public override Type GetTypeObjets()
		{
			return typeof(CRelationTypeCaracteristiqueEntite_Formulaire);
		}

		//////////////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees(CObjetDonnee objet)
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{	
				
			}
			catch ( Exception e )
			{
				result.EmpileErreur(new CErreurException(e));
			}
			return result;
				
		}


	}
}
