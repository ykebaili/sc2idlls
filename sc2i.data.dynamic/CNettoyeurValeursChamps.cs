using System;
using System.Collections;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Supprime les relations entre un élément à champs et un champ si
	/// cette relation ne doit pas exister
	/// </summary>
	public class CNettoyeurValeursChamps
	{
		public CNettoyeurValeursChamps()
		{
		}

		public static void NettoieChamps ( IElementAChamps element )
		{
#if PDA
			return;
#else
			Hashtable table = new Hashtable();
			foreach ( CChampCustom champ in element.GetChampsHorsFormulaire() )
				table[champ.Id] = true;
			foreach ( CFormulaire formulaire in element.GetFormulaires() )
				foreach ( CRelationFormulaireChampCustom rel in formulaire.RelationsChamps )
					table[rel.Champ.Id] = true;
			Hashtable relationsTrouvees = new Hashtable();
			ArrayList lstToDel = new ArrayList();
			foreach ( CRelationElementAChamp_ChampCustom relChamp in element.RelationsChampsCustom )
			{
				if ( relationsTrouvees[relChamp.ChampCustom.Id] != null )
					lstToDel.Add ( relChamp );
				else if ( table[relChamp.ChampCustom.Id] == null )
					lstToDel.Add ( relChamp );
				else
					relationsTrouvees[relChamp.ChampCustom.Id] = true;
			}
			foreach ( CRelationElementAChamp_ChampCustom rel in lstToDel )
				rel.Delete();
#endif
		}

		//Retourne la liste des relations à champ qui sont normales (liées au definisseur de champs)
		public static CRelationElementAChamp_ChampCustom[] RelationsChampsNormales(IElementAChamps element)
		{
			Hashtable table = new Hashtable();
			foreach ( CChampCustom champ in element.GetChampsHorsFormulaire() )
				table[champ.Id] = true;
			foreach ( CFormulaire formulaire in element.GetFormulaires() )
				foreach ( CRelationFormulaireChampCustom rel in formulaire.RelationsChamps )
					table[rel.Champ.Id] = true;
			Hashtable relationsTrouvees = new Hashtable();
			foreach ( CRelationElementAChamp_ChampCustom relChamp in element.RelationsChampsCustom )
			{
				if ( table[relChamp.ChampCustom.Id] != null )
					relationsTrouvees[relChamp] = true;
			}
			ArrayList lst = new ArrayList();
			foreach ( CRelationElementAChamp_ChampCustom rel in relationsTrouvees.Keys )
				lst.Add ( rel );
			return (CRelationElementAChamp_ChampCustom[])lst.ToArray(typeof(CRelationElementAChamp_ChampCustom));
		}
	}
}
