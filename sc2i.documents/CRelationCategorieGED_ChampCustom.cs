using System;
using System.Collections.Generic;
using System.Text;


using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;


namespace sc2i.documents
{
    /// <summary>
    /// Relation entre une <see cref="CCategorieGED">Catégorie GED</see> et un 
    /// <see cref="CChampCustom">Champ personnalisé</see>.
    /// </summary>
    [DynamicClass("EDM category / Custom field")]
    [ObjetServeurURI("CRelationCategorieGED_ChampCustomServeur")]
    [Table(CRelationCategorieGED_ChampCustom.c_nomTable, CRelationCategorieGED_ChampCustom.c_champId, true)]
    [FullTableSync]
    public class CRelationCategorieGED_ChampCustom : CRelationDefinisseurChamp_ChampCustom
    {
        public const string c_nomTable = "EDMCAT_CUSTOM_FIELD";
        public const string c_champId = "EDMCAT_ID";


        //-------------------------------------------------------------------
        public CRelationCategorieGED_ChampCustom(CContexteDonnee ctx)
            : base(ctx)
        {
        }
        //-------------------------------------------------------------------
        public CRelationCategorieGED_ChampCustom(System.Data.DataRow row)
            : base(row)
        {
        }

        [Relation(
            CCategorieGED.c_nomTable,
            CCategorieGED.c_champId,
            CCategorieGED.c_champId,
            true,
            false,
            true)]
        public override IDefinisseurChampCustom Definisseur
        {
            get
            {
                return (IDefinisseurChampCustom)GetParent(CCategorieGED.c_champId, typeof(CCategorieGED));
            }
            set
            {
                SetParent(CCategorieGED.c_champId, (CCategorieGED)value);
            }
        }
    }
}
