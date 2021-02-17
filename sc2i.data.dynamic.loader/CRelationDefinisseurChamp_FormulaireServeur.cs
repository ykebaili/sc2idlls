using System;
using System.Collections;

using sc2i.data.dynamic;
using sc2i.data.serveur;

using sc2i.common;

namespace sc2i.data.dynamic.loader
{
	/// <summary>
	/// Description résumée de CRelationDefinisseurChamp_ChampCustomServeur.
	/// </summary>
	public abstract class CRelationDefinisseurChamp_FormulaireServeur : CObjetDonneeServeurAvecCache
	{
		//-------------------------------------------------------------------
#if PDA
		public CRelationDefinisseurChamp_FormulaireServeur()
			:base()
		{
		}
#endif
		/// //////////////////////////////////////////////////////////////////
		public CRelationDefinisseurChamp_FormulaireServeur( int nIdSession )
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CRelationDefinisseurChamp_Formulaire rel = (CRelationDefinisseurChamp_Formulaire)objet;
				if ( rel.Formulaire == null )
					result.EmpileErreur(I.T("The linked form cannot be null|134"));
				if ( rel.Definisseur == null )
					result.EmpileErreur(I.T("The field definer cannot be null|135"));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
	}
}
