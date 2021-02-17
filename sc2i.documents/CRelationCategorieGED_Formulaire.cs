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
    /// <see cref="sc2i.data.dynamic.CFormulaire">Formulaire personnalisé</see>
    /// </summary>
    [DynamicClass("EDM category / Custom form")]
    [ObjetServeurURI("CRelationCategorieGED_FormulaireServeur")]
    [Table(CRelationCategorieGED_Formulaire.c_nomTable, CRelationCategorieGED_Formulaire.c_champId, true)]
    [FullTableSync]

    public class CRelationCategorieGED_Formulaire : CRelationDefinisseurChamp_Formulaire
    {
        #region Déclaration des constantes
        public const string c_nomTable = "EDMCAT_FORM";
        public const string c_champId = "EDMCATFORM_ID";
        #endregion


        //-------------------------------------------------------------------
        public CRelationCategorieGED_Formulaire(CContexteDonnee ctx)
            : base(ctx)
        {
        }
        //-------------------------------------------------------------------
        public CRelationCategorieGED_Formulaire(System.Data.DataRow row)
            : base(row)
        {
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Catégorie GED, objet de la relation
        /// </summary>
        [Relation(
            CCategorieGED.c_nomTable,
            CCategorieGED.c_champId,
            CCategorieGED.c_champId,
            true,
            true,
            true)]
		[DynamicField("EDM category")]
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
