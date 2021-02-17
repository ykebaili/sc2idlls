using System;
using System.Collections.Generic;
using System.Text;


using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;

namespace sc2i.data.dynamic.loader
{
	public static class SElementAChampServeur
	{
		public static CResultAErreur VerifieDonnees(IObjetDonneeAChamps objet)
		{
			CResultAErreur result = CResultAErreur.True;
			foreach (CRelationElementAChamp_ChampCustom rel in objet.RelationsChampsCustom)
			{
				CResultAErreur resTmp = CRelationElementAChamp_ChampCustomServeur.VerifieDonneesRelation(rel);
				if (!resTmp)
				{
					result.Erreur.EmpileErreurs(resTmp.Erreur);
					result.Result = false;
				}
			}
			return result;
		}
	}
}
