using System;
using System.IO;
using System.Data;

using sc2i.common;
using sc2i.data;


namespace sc2i.documents
{
    /// <summary>
    /// Relation entre un <see cref="CDocumentGED">Document GED</see> et une
    /// <see cref="CCategorieGED">Catégorie GED</see>
    /// </summary>
    [Table(CRelationDocumentGED_Categorie.c_nomTable, CRelationDocumentGED_Categorie.c_champId, true)]
    [ObjetServeurURI("CRelationDocumentGED_CategorieServeur")]
    [DynamicClass("EDM document / Category")]
    public class CRelationDocumentGED_Categorie : CObjetDonneeAIdNumeriqueAuto
    {
        public const string c_nomTable = "DOCUMENT_CATEGORY";

        public const string c_champId = "DOCCAT_ID";

        //-------------------------------------------------------------------
        //Préferer la fonction AssocieDocument de CCategorieDocument
        public CRelationDocumentGED_Categorie(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        //-------------------------------------------------------------------
        public CRelationDocumentGED_Categorie(DataRow row)
            : base(row)
        {
        }

        //-------------------------------------------------------------------
        public override string DescriptionElement
        {
            get
            {
                return I.T("Relation between the document @1 and the EDM category @2|114", Document.Libelle, Categorie.Libelle);
            }
        }

        //-------------------------------------------------------------------
        protected override void MyInitValeurDefaut()
        {
        }


        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champId };
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Catégorie GED, objet de la relation
        /// </summary>
        [Relation(CCategorieGED.c_nomTable,
             CCategorieGED.c_champId,
             CCategorieGED.c_champId,
             true,
             false,
             true)]
        [DynamicField("Category")]
        public CCategorieGED Categorie
        {
            get
            {
                return (CCategorieGED)GetParent(CCategorieGED.c_champId, typeof(CCategorieGED));
            }
            set
            {
                SetParent(CCategorieGED.c_champId, value);
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Document GED, objet de la relation
        /// </summary>
        [Relation(CDocumentGED.c_nomTable,
             CDocumentGED.c_champId,
             CDocumentGED.c_champId,
             true,
             true,
             true)]
        [DynamicField("EDM Document")]
        public CDocumentGED Document
        {
            get
            {
                return (CDocumentGED)GetParent(CDocumentGED.c_champId, typeof(CDocumentGED));
            }
            set
            {
                SetParent(CDocumentGED.c_champId, value);
            }
        }

        //-------------------------------------------------------------------
        public static CListeObjetsDonnees GetRelationsCategoriesForDocument(CDocumentGED doc)
        {
            return doc.GetDependancesListe(c_nomTable, CDocumentGED.c_champId);
        }
    }
}
