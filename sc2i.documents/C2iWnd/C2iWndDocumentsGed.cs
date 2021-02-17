using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;  

using sc2i.common;
using sc2i.formulaire;
using sc2i.data.dynamic;
using System.Drawing;
using System.Drawing.Design;
using sc2i.expression;
using sc2i.data;
using System.Collections;
using System.ComponentModel;
using System.IO;

namespace sc2i.documents
{
    [WndName("EDM Document")]
    [Serializable]
    public class C2iWndDocumentsGed : C2iWndComposantFenetre
    {
        [Serializable]
        public class CInfoAffectationDocumentToGed : IElementAVariableInstance
        {
            private string m_strNomFichier;
            private Type m_typeSource = null;

            [NonSerialized]
            private IObjetDonnee m_elementSource = null;

            public CInfoAffectationDocumentToGed()
            {
            }
            
            public CInfoAffectationDocumentToGed(Type typeSource)
            {
                m_typeSource = typeSource;
            }

            public CInfoAffectationDocumentToGed(string strNomFichier, CObjetDonnee elementSource)
            {
                m_strNomFichier = strNomFichier;
                m_elementSource = elementSource;
                if(elementSource != null)
                    m_typeSource = elementSource.GetType();
            }

            public Type TypeSource
            {
                get
                {
                    return m_typeSource;
                }
                set
                {
                    m_typeSource = value;
                }
            }
            
            [DynamicField("File_title")]
            public string FileName
            {
                get
                {
                    return Path.GetFileName(m_strNomFichier);
                }
            }

            [DynamicField("File_full_path")]
            public string FileFullName
            {
                get
                {
                    return m_strNomFichier;
                }
            }

            public IObjetDonnee SourceElement
            {
                get
                {
                    return m_elementSource;
                }
                set
                {
                    m_elementSource = value;
                }
            }

            public CDefinitionProprieteDynamique[] GetProprietesInstance()
            {
                CDefinitionProprieteDynamiqueDotNet def = new CDefinitionProprieteDynamiqueDotNet(
                    "Source_Element",
                    "SourceElement", 
                    new CTypeResultatExpression(m_typeSource, false),
                    true,
                    false,
                    "");

                return new CDefinitionProprieteDynamique[] { def };
            }
        }

        private List<C2iExpression> m_formulesElementsAssocies = new List<C2iExpression>();
        private List<C2iExpression> m_formulesCategoriesGed = new List<C2iExpression>();
        private C2iExpression m_formuleInitialisationDocument = null;
        private bool m_bMonoDocument = false;
        private bool m_bDisplayImages = false;
        private bool m_bIncludeSubCategories = false;
        private bool m_bHideHeader = false;
        List<CAffectationsProprietes> m_listeAffectationsInitiales = new List<CAffectationsProprietes>();
        Type m_typeSource = null;

        //-----------------------------------
        public C2iWndDocumentsGed()
            : base()
        {
            Size = new Size(140, 21);
        }

        //-----------------------------------
        private void RecalcLayout()
        {


        }

        //-----------------------------------
        public override bool CanBeUseOnType(Type tp)
        {
            if (tp != null && typeof(CObjetDonneeAIdNumerique).IsAssignableFrom(tp))
            {
                return true;
            }
            return false;
        }

        //-----------------------------------
        public override System.Drawing.Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
                RecalcLayout();
            }
        }


        //-----------------------------------
        /// <summary>
        /// For futur use : permet de ne sélectionner qu'un seul document
        /// ->une nouvelle selection annule et remplace la précédente
        /// </summary>
        private bool MonoDocument
        {
            get
            {
                return m_bMonoDocument;
            }
            set
            {
                m_bMonoDocument = value;
            }
        }

        //-----------------------------------
        public bool DisplayDocuments
        {
            get
            {
                return m_bDisplayImages;
            }
            set
            {
                m_bDisplayImages = value;
            }
        }

        //-----------------------------------
        public bool IncludeSubCategories
        {
            get
            {
                if (CategoriesFormulas.Length > 1)
                    return false;
                return m_bIncludeSubCategories;
            }
            set
            {
                m_bIncludeSubCategories = value;
            }
        }

        //------------------------------------------------------------------------------
        public bool HideHeader
        {
            get
            {
                return m_bHideHeader;
            }
            set
            {
                m_bHideHeader = value;
            }
        }

        //------------------------------------------------------------------------------
        [TypeConverter(typeof(CExpressionOptionsConverter))]
        [System.ComponentModel.Editor(typeof(CProprieteExpressionEditor), typeof(UITypeEditor))]
        [Browsable(false)]//Masqué parceque pas utile en urgence et pb sur éditeur de formule qui
            //n'édite pas sur Document, mais sur le type édité par le contrôle
        public C2iExpression InitializationFormula
        {
            get
            {
                return m_formuleInitialisationDocument;
            }
            set
            {
                m_formuleInitialisationDocument = value;
            }
        }

        //------------------------------------------------------------------------------
        [System.ComponentModel.Editor(typeof(CListeFormulesEditor), typeof(UITypeEditor))]
        public C2iExpression[] AssociationsFormulas
        {
            get
            {
                return m_formulesElementsAssocies.ToArray();
            }
            set
            {
                m_formulesElementsAssocies = new List<C2iExpression>();
                if (value != null)
                    m_formulesElementsAssocies.AddRange(value);
            }
        }

        //------------------------------------------------------------------------------
        [System.ComponentModel.Editor(typeof(CListeFormulesEditor), typeof(UITypeEditor))]
        public C2iExpression[] CategoriesFormulas
        {
            get
            {
                return m_formulesCategoriesGed.ToArray();
            }
            set
            {
                m_formulesCategoriesGed = new List<C2iExpression>();
                if (value != null)
                    m_formulesCategoriesGed.AddRange(value);
            }
        }

        //-------------------------------------------------------------------------
        [System.ComponentModel.Editor(typeof(CProprieteAffectationsProprietesEditor), typeof(UITypeEditor))]
        public List<CAffectationsProprietes> Affectations
        {
            get
            {
                if (m_listeAffectationsInitiales.Count == 0)
                    m_listeAffectationsInitiales.Add(new CAffectationsProprietes());
                return m_listeAffectationsInitiales;
            }
            set
            {
                m_listeAffectationsInitiales = value;
            }
        }

        //-------------------------------------------------------------------------
        public Type TypeSource
        {
            set
            {
                m_typeSource = value;
            }
            get
            {
                return m_typeSource;
            }
        }

        //-------------------------------------------------------------------------
        private int GetNumVersion()
        {
            //return 2;
            return 3; // Ajout des Affectations
        }

        //---------------------------------------------------------------------------
        protected override CResultAErreur MySerialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (!result)
                return result;

            result = serializer.TraiteObject<C2iExpression>(ref m_formuleInitialisationDocument);
            if (!result)
                return result;

            result = serializer.TraiteListe<C2iExpression>(m_formulesElementsAssocies);
            if (!result)
                return result;

            result = serializer.TraiteListe<C2iExpression>(m_formulesCategoriesGed);
            if (!result)
                return result;
            if (nVersion >= 1)
            {
                serializer.TraiteBool(ref m_bMonoDocument);
                serializer.TraiteBool(ref m_bDisplayImages);
                serializer.TraiteBool(ref m_bIncludeSubCategories);
            }
            if ( nVersion >= 2 )
                serializer.TraiteBool ( ref m_bHideHeader );

            if (nVersion >= 3)
            {
                result = serializer.TraiteListe<CAffectationsProprietes>(m_listeAffectationsInitiales);
                if (!result)
                    return result;

                bool bHasType = m_typeSource != null;
                serializer.TraiteBool(ref bHasType);
                if (bHasType)
                {
                    serializer.TraiteType(ref m_typeSource);
                }
            }

            return result;
        }

        //-----------------------------------
        public override bool AcceptChilds
        {
            get
            {
                return false;
            }
        }

        //-----------------------------------
        protected override void MyDraw(sc2i.drawing.CContextDessinObjetGraphique ctx)
        {
            Graphics g = ctx.Graphic;
            Brush b = new SolidBrush(BackColor);
            Rectangle rect = new Rectangle(Position, Size);

            g.DrawImage(Resource.Liste_Documents, rect);

            b.Dispose();

            base.MyDraw(ctx);
        }

        //------------------------------------------------------------------------------------
        public override void DrawInterieur(sc2i.drawing.CContextDessinObjetGraphique ctx)
        {

        }

        //------------------------------------------------------------------------------------
        public override void OnDesignCreate(Type typeEdite)
        {
            m_typeSource = typeEdite;
        }

        //------------------------------------------------------------------------------------
        public override void OnDesignSelect(
            Type typeEdite,
            object objetEdite,
            sc2i.expression.IFournisseurProprietesDynamiques fournisseurProprietes)
        {
            base.OnDesignSelect(typeEdite, objetEdite, fournisseurProprietes);
            CListeFormulesEditor.ObjetPourSousProprietes = new CObjetPourSousProprietes(typeEdite);

            m_typeSource = typeEdite;
            CProprieteAffectationsProprietesEditor.SetObjetSource(
                new CObjetPourSousProprietes(new CInfoAffectationDocumentToGed(typeEdite)));
            CProprieteAffectationsProprietesEditor.SetTypeElementAffecte(typeof(CDocumentGED));
            CProprieteAffectationsProprietesEditor.FournisseurProprietes = fournisseurProprietes;
        }

        //------------------------------------------------------------------------------------
        public IEnumerable<CObjetDonneeAIdNumerique> GetListeAssociations(object sourceDeFormule)
        {
            List<CObjetDonneeAIdNumerique> lst = new List<CObjetDonneeAIdNumerique>();
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(sourceDeFormule);
            foreach (C2iExpression formule in AssociationsFormulas)
            {
                CResultAErreur result = formule.Eval(ctx);
                if (result)
                {
                    object data = result.Data;
                    if (data is CObjetDonneeAIdNumerique)
                        lst.Add(data as CObjetDonneeAIdNumerique);

                    IEnumerable lstObjets = data as IEnumerable;
                    if (lstObjets != null)
                    {
                        foreach (object obj in lstObjets)
                        {
                            if (obj is CObjetDonneeAIdNumerique)
                                lst.Add(obj as CObjetDonneeAIdNumerique);
                        }
                    }
                }
            }
            return lst.AsReadOnly();
        }

        //-----------------------------------
        public IEnumerable<CCategorieGED> GetListeCategories(object sourceDeFormule)
        {
            List<CCategorieGED> lst = new List<CCategorieGED>();
            CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(sourceDeFormule);
            foreach (C2iExpression formule in CategoriesFormulas)
            {
                CResultAErreur result = formule.Eval(ctx);
                if (result)
                {
                    object data = result.Data;
                    if (data is CCategorieGED)
                        lst.Add(data as CCategorieGED);

                    IEnumerable lstObjets = data as IEnumerable;
                    if (lstObjets != null)
                    {
                        foreach (object obj in lstObjets)
                        {
                            if (obj is CCategorieGED)
                                lst.Add(obj as CCategorieGED);
                        }
                    }
                }
            }
            return lst.AsReadOnly();
        }

        //-----------------------------------
        public IEnumerable<CDocumentGED> GetDocuments ( object sourceDeFormule )
        {
            Dictionary<CDocumentGED, int> dicDocs = new Dictionary<CDocumentGED,int>();
            
            IEnumerable<CObjetDonneeAIdNumerique> lstObjets = GetListeAssociations(sourceDeFormule);
            IEnumerable<CCategorieGED> lstCategories = GetListeCategories(sourceDeFormule);

            //Ne prend que les documents qui sont dans toutes les catégories
            //et communes à tous les éléments
            bool bHasCategories = lstCategories.Count() > 0;
            HashSet<int> setCats = new HashSet<int>();
            foreach (CCategorieGED categorie in lstCategories)
                AddCategory(categorie, setCats);

            
            foreach ( CObjetDonneeAIdNumerique objet in lstObjets )
            {
                CListeObjetsDonnees lstDocs = CDocumentGED.GetListeDocumentsForElement ( objet );
                if ( bHasCategories )
                {
                    lstDocs.ReadDependances(CRelationDocumentGED_Categorie.c_nomTable);
                    foreach ( CDocumentGED doc in lstDocs )
                    {
                        int nNbCats = 0;
                        foreach ( CRelationDocumentGED_Categorie rel in doc.RelationsCategories )
                        {
                            if ( setCats.Contains ( (int)rel.Row[CCategorieGED.c_champId] ))
                                nNbCats++;
                        }
                        if ( nNbCats == lstCategories.Count() )
                        {
                            int nTmp = 0;
                            if ( !dicDocs.ContainsKey(doc))
                                dicDocs[doc] = 1;
                            else
                            {
                                nTmp = dicDocs[doc];
                                dicDocs[doc] = nTmp+1;
                            }
                        }
                    }
                }
                else
                {
                    foreach ( CDocumentGED doc in lstDocs )
                    {
                        if ( !dicDocs.ContainsKey ( doc ) )
                            dicDocs[doc] = 1;
                        else
                            dicDocs[doc] = dicDocs[doc]+1;
                    }
                }
            }

            List<CDocumentGED> lstFinale = new List<CDocumentGED>();
            int nObjectif = lstObjets.Count();
            foreach ( KeyValuePair<CDocumentGED, int> kv in dicDocs )
                if ( kv.Value == nObjectif )
                    lstFinale.Add ( kv.Key );
            lstFinale.Sort ( (x,y)=>x.Libelle.CompareTo(y.Libelle));
            return lstFinale.AsReadOnly();
        }

        //-------------------------------------------------------
        private void AddCategory(CCategorieGED cat, HashSet<int> setCats)
        {
            setCats.Add(cat.Id);
            if (IncludeSubCategories)
                foreach (CCategorieGED subCat in cat.CategoriesFilles)
                    AddCategory(subCat, setCats);
        }





       
    }
}