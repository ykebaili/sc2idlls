using System;
using System.Collections.Generic;
using System.Text;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.documents
{
    /// <summary>
    /// Relation entre un <see cref="CDocumentGED">Document GED</see> et une 
    /// <see cref="CValeurChampCustom">Valeur de champ personnalisé</see>.
    /// </summary>
    [DynamicClass("EDM Document / Custom field value")]
    [ObjetServeurURI("CRelationDocumentGED_ChampCustomValeurServeur")]
    [Table(CRelationDocumentGED_ChampCustomValeur.c_nomTable, CRelationDocumentGED_ChampCustomValeur.c_champId, true)]
    public class CRelationDocumentGED_ChampCustomValeur : CRelationElementAChamp_ChampCustom
    {
        public const string c_nomTable = "EDM_DOC_CUSTOM_FIELD";
        public const string c_champId = "EDMDOCFLD_ID";

        //-------------------------------------------------------------------
        public CRelationDocumentGED_ChampCustomValeur(CContexteDonnee ctx)
            : base(ctx)
        {
        }
        //-------------------------------------------------------------------
        public CRelationDocumentGED_ChampCustomValeur(System.Data.DataRow row)
            : base(row)
        {
        }

        //-------------------------------------------------------------------
        public override Type GetTypeElementAChamps()
        {
            return typeof(CDocumentGED);
        }

        //-------------------------------------------------------------------
        public override string GetNomTable()
        {
            return c_nomTable;
        }
        //-------------------------------------------------------------------
        public override string GetChampId()
        {
            return c_champId;
        }


        //-------------------------------------------------------------------
        /// <summary>
        /// Document GED, objet de la relation
        /// </summary>
        [Relation(
            CDocumentGED.c_nomTable,
            CDocumentGED.c_champId,
            CDocumentGED.c_champId,
            true,
            true,
            true)]
        [DynamicField("EDM document")]
        public override IElementAChamps ElementAChamps
        {
            get
            {
                return (IElementAChamps)GetParent(CDocumentGED.c_champId, typeof(CDocumentGED));
            }
            set
            {
                SetParent(CDocumentGED.c_champId, (CDocumentGED)value);
            }
        }

    }
}
