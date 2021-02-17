using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

using sc2i.common;
using System.Data;
using sc2i.expression;

namespace sc2i.data.dynamic
{
    
	/// <summary>
	/// Description résumée de IElementAChamps.
	/// </summary>
	public interface IElementAChamps : IElementAVariables, IObjetDonnee
	{
		//Retourne des objets de type CRelationElementAChamp_ChampCustom
		CListeObjetsDonnees RelationsChampsCustom{get;}

        object GetValeurChamp(int idChamp, DataRowVersion version);
        object GetValeurChamp(int idChamp);
        CResultAErreur SetValeurChamp(int idChamp, object valeur);

		CChampCustom[] GetChampsHorsFormulaire();
		CFormulaire[] GetFormulaires();

        CRoleChampCustom RoleChampCustomAssocie { get; }

		string DescriptionElement{get;}
		int Id{get;}

		//Retourne tous les definisseurs de champs (des IDefinisseurChampCustom)
		IDefinisseurChampCustom[] DefinisseursDeChamps{get;}
	}

    public static class CAddFonctionToIElementAChamps
    {
        public static object GetValeurChamp(this IElementAChamps elt, CDbKey key, DataRowVersion version)
        {
            if (key != null)
            {
                CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                if (champ.ReadIfExists(key))
                    return elt.GetValeurChamp(champ.Id, version);
            }
            return null;
        }

        public static object GetValeurChamp(this IElementAChamps elt, CDbKey key)
        {
            return GetValeurChamp(elt, key, DataRowVersion.Current);
        }
    }

    public interface IObjetDonneeAChamps : IElementAChamps, IObjetDonneeAIdNumeriqueAuto
    {
        string GetNomTableRelationToChamps();
        CListeObjetsDonnees GetRelationsToChamps( int nIdChamp );
        CRelationElementAChamp_ChampCustom GetNewRelationToChamp();
    }

}
