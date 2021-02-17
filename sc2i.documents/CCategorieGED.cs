using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

using sc2i.data;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.data.dynamic;


namespace sc2i.documents
{
    /// <summary>
    /// Une catégorie GED est un organe de classement pour les <see cref="CDocumentGED">documents</see><br/>
    /// de la Gestion Electronique de Documents (GED).<br/>
    /// Une catégorie GED est un objet hiérarchique; il peut donc se référer<br/>
    /// à un parent et avoir des enfants.<br/>
    /// Une catégorie GED permet également d'attacher des <see cref="sc2i.data.dynamic.CFormulaire">formulaires personnalisés</see><br/>
    /// aux documents de cette catégorie.
    /// </summary>
    [Table(CCategorieGED.c_nomTable, CCategorieGED.c_champId, true)]
    [ObjetServeurURI("CCategorieGEDServeur")]
    [DynamicClass("EDM category")]
    [FullTableSync]
    public class CCategorieGED : CObjetHierarchique,
                                 IObjetALectureTableComplete,
                                 IDefinisseurChampCustomRelationObjetDonnee,
                                 IElementAEO
    {
        public const string c_nomTable = "EDM_CATEGORY";
        public const string c_champId = "EDMCAT_ID";

        public const string c_champLibelle = "EDMCAT_LABEL";
        public const string c_champDescriptif = "EDMCAT_DESCRIPTION";

        public const string c_champCodeSystemePartiel = "PARTIAL_SYSTEM_CODE";
        public const string c_champCodeSystemeComplet = "FULL_SYSTEM_CODE";
        public const string c_champNiveau = "EDMCAT_LEVEL";
        public const string c_champIdParent = "PARENT_ID";

        //Attention, ce champ doit impérativement s'appeler OE_TIMOS
        //car il est utilisé dans le test des droits de TIMOS.
        //Il aurait été logique de le renommer, mais c'est hélas
        //impossible. On trainera donc ce nom illogique jusqu'à
        //la fin de la vie du temps du monde.
        public const string c_champCodesEO = "OE_TIMOS";

        private static IRelationsEOProvider m_relationsEOProvider = null;
        public static void SetRelationsEOProvider(IRelationsEOProvider rel)
        {
            m_relationsEOProvider = rel;
        }

        //-------------------------------------------------------------------
        public CCategorieGED(CContexteDonnee contexte)
            : base(contexte)
        {
        }


        //-------------------------------------------------------------------
        public CCategorieGED(DataRow row)
            : base(row)
        {
        }


        //-------------------------------------------------------------------
        public override string DescriptionElement
        {
            get
            {
				return I.T("EDM category @1|109", Libelle);
            }
        }

        //-------------------------------------------------------------------
        protected override void MyInitValeurDefaut()
        {
            base.MyInitValeurDefaut();
        }

        //-------------------------------------------------------------------
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champLibelle };
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit le libellé de la catégorie
        /// </summary>
        [TableFieldProperty(c_champLibelle, 255)]
        [DynamicField("Label")]
		[DescriptionField]
		public override string Libelle
        {
            get
            {
                return (string)Row[c_champLibelle];
            }
            set
            {
                Row[c_champLibelle] = value;
            }
        }
        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit la description de la catégorie
        /// </summary>
        [TableFieldProperty(c_champDescriptif, 1024)]
        [DynamicField("Description")]
        public string Descriptif
        {
            get
            {
                return (string)Row[c_champDescriptif];
            }
            set
            {
                Row[c_champDescriptif] = value;
            }
        }

        //-------------------------------------------------------------------
        public void AssocieDocument(CDocumentGED document)
        {
            CRelationDocumentGED_Categorie relation = new CRelationDocumentGED_Categorie(document.ContexteDonnee);
            CFiltreData filtre = new CFiltreData(CDocumentGED.c_champId + "=@1 and " +
                c_champId + "=@2",
                document.Id,
                Id);
            if (relation.ReadIfExists(filtre))
                return;
            relation.CreateNewInCurrentContexte();
            relation.Categorie = this;
            relation.Document = document;
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Renvoie la liste des relations de la catégorie avec des documents GED
        /// </summary>
        [RelationFille(typeof(CRelationDocumentGED_Categorie), "Categorie")]
        [DynamicChilds("EDM Document Relations", typeof(CRelationDocumentGED_Categorie))]
        public CListeObjetsDonnees RelationsDocuments
        {
            get
            {
                return GetDependancesListe(CRelationDocumentGED_Categorie.c_nomTable, c_champId);
            }

        }


        #region IDefinisseurChampCustomRelationObjetDonnee Membres

        [RelationFille(typeof(CRelationCategorieGED_ChampCustom), "Definisseur")]
        public CListeObjetsDonnees RelationsChampsCustomListe
        {
            get
            {
                return GetDependancesListe(CRelationCategorieGED_ChampCustom.c_nomTable, c_champId);
            }
        }

        [RelationFille(typeof(CRelationCategorieGED_Formulaire), "Definisseur")]
        public CListeObjetsDonnees RelationsFormulairesListe
        {
            get
            {
                return GetDependancesListe(CRelationCategorieGED_Formulaire.c_nomTable, c_champId);
            }
        }

        #endregion

        #region IDefinisseurChampCustom Membres


        public IRelationDefinisseurChamp_ChampCustom[] RelationsChampsCustomDefinis
        {
            get
            {
                return (IRelationDefinisseurChamp_ChampCustom[])RelationsChampsCustomListe.ToArray(typeof(IRelationDefinisseurChamp_ChampCustom));
            }
        }

        public IRelationDefinisseurChamp_Formulaire[] RelationsFormulaires
        {
            get
            {
                return (IRelationDefinisseurChamp_Formulaire[])RelationsFormulairesListe.ToArray(typeof(IRelationDefinisseurChamp_Formulaire));
            }
        }

        public CRoleChampCustom RoleChampCustomDesElementsAChamp
        {
            get
            {
                return CRoleChampCustom.GetRole(CDocumentGED.c_roleChampCustom);
            }
        }

        public CChampCustom[] TousLesChampsAssocies
        {
            get
            {
                Hashtable tableChamps = new Hashtable();
                FillHashtableChamps(tableChamps);
                CChampCustom[] liste = new CChampCustom[tableChamps.Count];
                int nChamp = 0;
                foreach (CChampCustom champ in tableChamps.Values)
                    liste[nChamp++] = champ;
                return liste;
            }
        }
        //-------------------------------------------------------------------
        /// <summary>
        /// Remplit une hashtable IdChamp->Champ
        /// avec tous les champs liés.(hiérarchique)
        /// </summary>
        /// <param name="tableChamps">HAshtable à remplir</param>
        private void FillHashtableChamps(Hashtable tableChamps)
        {
            foreach (IRelationDefinisseurChamp_ChampCustom relation in RelationsChampsCustomDefinis)
                tableChamps[relation.ChampCustom.Id] = relation.ChampCustom;
            foreach (IRelationDefinisseurChamp_Formulaire relation in RelationsFormulaires)
            {
                foreach (CRelationFormulaireChampCustom relFor in relation.Formulaire.RelationsChamps)
                    tableChamps[relFor.Champ.Id] = relFor.Champ;
            }
        }

        #endregion

        public override int NbCarsParNiveau
        {
            get { return 2; }
        }

        public override string ChampCodeSystemePartiel
        {
            get { return c_champCodeSystemePartiel; }
        }

        public override string ChampCodeSystemeComplet
        {
            get { return c_champCodeSystemeComplet; }
        }

        public override string ChampNiveau
        {
            get { return c_champNiveau; }
        }

        public override string ChampLibelle
        {
            get { return c_champLibelle; }
        }

        public override string ChampIdParent
        {
            get { return c_champIdParent; }
        }

        //----------------------------------------------------
        /// <summary>
        /// Code système propre à la catégorie GED.<br/>
        /// Ce code est exprimé sur 2 caractères alphanumériques
        /// </summary>
        [TableFieldProperty(c_champCodeSystemePartiel, 2)]
        [DynamicField("Partial system code")]
        public override string CodeSystemePartiel
        {
            get
            {
                string strVal = "";
                if (Row[c_champCodeSystemePartiel] != DBNull.Value)
                    strVal = (string)Row[c_champCodeSystemePartiel];
                strVal = strVal.Trim().PadLeft(2, '0');
                return strVal;
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Code système complet de la catégorie GED. Ce code système est<br/>
        /// constitué du code système complet de la catégorie GED parente<br/>
        /// concaténé avec le code système propre de la catégorie GED.<br/>
        /// Exemple : CG_GRDPARENTE -> CG_PARENTE -> CG_FILLE<br/>
        /// si le code de CG_GRDPARENTE est 01, le code de CG_PARENTE est 03 et<br/>
        /// et le code propre de CG_FILLE est 08, le code système complet<br/>
        /// de CG_FILLE est 010308.
        /// </summary>
        [TableFieldProperty(c_champCodeSystemeComplet, 100)]
        [DynamicField("Full system code")]
        public override string CodeSystemeComplet
        {
            get
            {
                return (string)Row[c_champCodeSystemeComplet];
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Niveau de la catégorie GED dans la hiérarchie des catégories (nombre de parents).<br/>
        /// Exemple : CG_GRDPARENTE -> CG_PARENTE -> CG_FILLE<br/>
        /// CG_GRDPARENTE a pour niveau 0, CG_PARENTE a pour niveau 1<br/>
        /// (1 parent en remontant la hiérarchie), CG_FILLE a pour niveau 2<br/>
        /// (2 parents en remontant la hiérarchie)
        /// </summary>
        [TableFieldProperty(c_champNiveau)]
        [DynamicField("Level")]
        public override int Niveau
        {
            get
            {
                return (int)Row[c_champNiveau];
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Donne ou définit la Categorie Parente dans la hiérarchie
        /// </summary>
        [Relation(
            c_nomTable,
            c_champId,
            c_champIdParent,
            false,
            false)]
        [DynamicField("Parent Category")]
        public CCategorieGED CategorieParente
        {
            get
            {
                return (CCategorieGED)ObjetParent;
            }
            set
            {
                ObjetParent = value;
            }
        }

        //----------------------------------------------------
        /// <summary>
        /// Retourne la liste des Catégories filles dans la hiérarchie
        /// </summary>
        [RelationFille(typeof(CCategorieGED), "CategorieParente")]
        [DynamicChilds("Child Categories", typeof(CCategorieGED))]
        public CListeObjetsDonnees CategoriesFilles
        {
            get
            {
                return ObjetsFils;
            }
        }


        #region IElementAEO Membres

        //-----------------------------------------------------------
        /// <summary>
        /// Codes complets (Full_system_code) de toutes les <see cref="CEntiteOrganisationnelle">entités organisationnelles</see> auxquelles est affectée la catégorie GED<br/>
        /// </summary>
        /// <remarks>
        /// Ces codes sont présentés sous la forme d'une chaîne de caractères<br/>
        /// Chaque code est encadré par deux caractères ~ (exemple : ~01051B~0201~061A0304~)
        /// </summary>
        [TableFieldProperty(CDocumentGED.c_champCodesEO, 1024)]
        [DynamicField("Organisational entities codes")]
        public string CodesEntitesOrganisationnelles
        {
            get
            {
                return (string)Row[c_champCodesEO];
            }
            set
            {
                Row[c_champCodesEO] = value;
            }
        }

        //-----------------------------------------------------------
        public CListeObjetsDonnees EntiteOrganisationnellesDirectementLiees
        {
            get
            {
                if (m_relationsEOProvider != null)
                    return m_relationsEOProvider.GetEntiteOrganisationnellesDirectementLiees(this);
                CListeObjetsDonnees lst = new CListeObjetsDonnees(ContexteDonnee, GetType());
                lst.Filtre = new CFiltreDataImpossible();
                return lst;
            }
        }

        //-----------------------------------------------------------
        public IElementAEO[] DonnateursEO
        {
            get
            {
                return new IElementAEO[] { CategorieParente };
            }
        }

        //-----------------------------------------------------------
        public IElementAEO[] HeritiersEO
        {
            get
            {
                ArrayList lstHeritiers = new ArrayList();
                lstHeritiers.AddRange(CategoriesFilles);
                foreach (CRelationDocumentGED_Categorie rel in RelationsDocuments)
                {
                    lstHeritiers.Add(rel.Document);
                }
                return (IElementAEO[])lstHeritiers.ToArray(typeof(IElementAEO));
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Attribue une nouvelle entité organisationnelle à l'élément
        /// </summary>
        /// <param name="nIdEO">Id de l'entité organisationnelle</param>
        /// <returns>retourne le <see cref="CResultAErreur">résultat</see></returns>
        [DynamicMethod(
                "Asigne a new Organisationnal Entity to the Element",
                "The Organisationnal Entity Identifier")]
        public CResultAErreur AjouterEO(int nIdEO)
        {
            if (m_relationsEOProvider != null)
                return m_relationsEOProvider.AjouterEO(this, nIdEO);
            return CResultAErreur.True;
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Ote de l'élément une entité organisationnelle
        /// </summary>
        /// <param name="nIdEO">Id de l'entité à enlever</param>
        /// <returns>retourne le <see cref="CResultAErreur">résultat</see></returns>
        [DynamicMethod(
            "Remove an Organisationnal Entity from the Element",
            "The Organisationnal Entity Identifier")]
        public CResultAErreur SupprimerEO(int nIdEO)
        {
            if (m_relationsEOProvider != null)
                return m_relationsEOProvider.SupprimerEO(this, nIdEO);
            return CResultAErreur.True;
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Positionne toutes les entités organisationnelles de l'élément
        /// </summary>
        /// <param name="nIdsOE">Tableau d'Id des entités organisationnelles à associer</param>
        /// <returns>retourne le <see cref="CResultAErreur">résultat</see></returns>
        [DynamicMethod(
            "Set all Organizational Entities to the Element",
            "An array of Organizational Entity IDs")]
        public CResultAErreur SetAllOrganizationalEntities(int[] nIdsOE)
        {
            if (m_relationsEOProvider != null)
                return m_relationsEOProvider.SetAllOrganizationalEntities(this, nIdsOE);
            return CResultAErreur.True;
        }


        #endregion

        #region IObjetARestrictionSurEntite Membres

        public void CompleteRestriction(CRestrictionUtilisateurSurType restriction)
        {
            if (m_relationsEOProvider != null)
                m_relationsEOProvider.CompleteRestriction(this, restriction);
        }

        #endregion
    }
}
