using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Description résumée de CDefinisseurChampCustom.
	/// </summary>
	public interface IDefinisseurChampCustom 
	{
		string DescriptionElement{get;}
		CRoleChampCustom RoleChampCustomDesElementsAChamp{get;}

		CContexteDonnee ContexteDonnee{get;}
		
		IRelationDefinisseurChamp_ChampCustom[] RelationsChampsCustomDefinis{get;}
		IRelationDefinisseurChamp_Formulaire[] RelationsFormulaires{get;}

		CChampCustom[] TousLesChampsAssocies{get;}

		int Id{get;}
	}

	public interface IDefinisseurChampCustomRelationObjetDonnee : IDefinisseurChampCustom
	{
		CListeObjetsDonnees RelationsChampsCustomListe{get;}
		CListeObjetsDonnees RelationsFormulairesListe{get;}
	}
	

	public class CRecuperateurTousChampsAssociesADefinisseur
	{
		public static CChampCustom[] GetTousLesChampsAssociesA ( IDefinisseurChampCustom definisseur )
		{
			Hashtable tableChamps = new Hashtable();
			
			foreach ( IRelationDefinisseurChamp_ChampCustom relation in definisseur.RelationsChampsCustomDefinis )
				tableChamps[relation.ChampCustom.Id] = relation.ChampCustom;
			
			foreach ( IRelationDefinisseurChamp_Formulaire relation in definisseur.RelationsFormulaires )
			{
				foreach ( CRelationFormulaireChampCustom relFor in relation.Formulaire.RelationsChamps )
					tableChamps[relFor.Champ.Id] = relFor.Champ;
			}
			CChampCustom[] liste = new CChampCustom[tableChamps.Count];
			int nChamp = 0;
			foreach ( CChampCustom champ in tableChamps.Values )
				liste[nChamp++] = champ;
			return liste;
		}
	}
}
