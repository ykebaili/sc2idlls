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
	public abstract class CRelationDefinisseurChamp_ChampCustomServeur : CObjetDonneeServeurAvecCache
	{
		/// //////////////////////////////////////////////////////////////////
#if PDA
		public CRelationDefinisseurChamp_ChampCustomServeur()
			:base()
		{
		}
#endif
		/// //////////////////////////////////////////////////////////////////
		public CRelationDefinisseurChamp_ChampCustomServeur( int nIdSession )
			:base(nIdSession)
		{
		}

		/// //////////////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees ( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CRelationDefinisseurChamp_ChampCustom rel = (CRelationDefinisseurChamp_ChampCustom)objet;
				if ( rel.ChampCustom == null )
					result.EmpileErreur(I.T("The linked field cannot be null|132"));
				if ( rel.Definisseur == null )
					result.EmpileErreur(I.T("The field definer cannot be null|133"));
			}
			catch (Exception e)
			{
				result.EmpileErreur( new CErreurException(e));
			}
			return result;
		}
	}
}
