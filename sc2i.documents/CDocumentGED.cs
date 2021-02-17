using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Linq;

using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using System.Collections.Generic;
using sc2i.drawing;
using sc2i.expression;

namespace sc2i.documents
{
	#if !PDA_DATA
	/// <summary>
	/// L'entité 'Document GED' permet d'attacher des documents de types divers<br/>
    /// (texte, images, etc.) aux objets de l'application.
    /// <br/>
    /// <br/>
    /// N'importe quel document peut être attaché à n'importe quelle entité<br/>
    /// du système, à l'aide de cette fonctionnalité.
    /// <br/>
    /// <br/>
    /// Les documents peuvent être classés suivant leur <see cref="CCategorieGED">catégorie</see>.
	/// </summary>
    [Table(CDocumentGED.c_nomTable, CDocumentGED.c_champId, true)]
    [ObjetServeurURI("CDocumentGEDServeur")]
    [DynamicClass("EDM document")]
    [AutoExec("Autoexec")]
    public class CDocumentGED : CObjetDonneeAIdNumeriqueAuto,
                                IObjetDonneeAChamps,
                                IElementAEO
    {

        public const string c_strNoEDMControlOnSave = "NO_EDM_CONTROL_ON_SAVE";


        public const string c_nomTable = "EDM_DOC";

        public const string c_champId = "EDMDOC_ID";
        public const string c_champLibelle = "EDMDOC_LABEL";
        public const string c_champDescriptif = "EDMDOC_DESCRIPTION";
        public const string c_champReference = "EDMDOC_REFERENCE";
        public const string c_champDateCreation = "EDMDOC_CREATIONDATE";
        public const string c_champDateMAJ = "EDMDOC_UPDATE_DATE";
        public const string c_champNumVersion = "EDMDOC_VERSION_NUM";
        public const string c_champCle = "EDMDOC_KEY";
        public const string c_champIsSystem = "EDMDOC_SYSTEM";

        //Attention, ce champ doit impérativement s'appeler OE_TIMOS
        //car il est utilisé dans le test des droits de TIMOS.
        //Il aurait été logique de le renommer, mais c'est hélas
        //impossible. On trainera donc ce nom illogique jusqu'à
        //la fin de la vie du temps du monde.
        public const string c_champCodesEO = "OE_TIMOS";

        public const string c_roleChampCustom = "EDM_DOC";


        private static IRelationsEOProvider m_relationsEOProvider = null;

        public static void SetRelationsEOProvider(IRelationsEOProvider rel)
        {
            m_relationsEOProvider = rel;
        }

        //-------------------------------------------------------------------
        public CDocumentGED(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        //-------------------------------------------------------------------
        public CDocumentGED(DataRow row)
            : base(row)
        {
        }

        //-------------------------------------------------------------------
        public static void Autoexec()
        {
            CRoleChampCustom.RegisterRole(c_roleChampCustom, "EDM Document", typeof(CDocumentGED));
        }

        //-------------------------------------------------------------------
        public CRoleChampCustom RoleChampCustomAssocie
        {
            get
            {
                return CRoleChampCustom.GetRole(c_roleChampCustom);
            }
        }

        //-------------------------------------------------------------------
        public override string DescriptionElement
        {
            get
            {
                return I.T("EDM Document @1|30002", Libelle);
            }
        }

        //-------------------------------------------------------------------
        public override CFiltreData FiltreStandard
        {
            get
            {
                return new CFiltreData(c_champIsSystem + "=@1", false);
            }
        }


        //-------------------------------------------------------------------
        protected override void MyInitValeurDefaut()
        {
            NumVersion = 0;
            DateCreation = DateTime.Now;
            DateMAJ = DateTime.Now;
        }

        //-------------------------------------------------------------------
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champDateMAJ + " desc," + c_champDateCreation + " desc," + c_champLibelle };
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit le numéro de version du document. Ce numéro est incrémenté chaque fois<br/>
        /// que l'association à un fichier (document source) est précisée.<br/>
        /// Tant qu'aucun document n'est associé ce numéro reste à 0.
        /// </summary>
        [TableFieldProperty(c_champNumVersion)]
        [DynamicField("Version number")]
        public int NumVersion
        {
            get
            {
                return (int)Row[c_champNumVersion];
            }
            set
            {
                Row[c_champNumVersion] = value;
            }
        }
        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit le libellé du document
        /// </summary>
        [TableFieldProperty(c_champLibelle, 255)]
        [DynamicField("Label")]
        [DescriptionField]
        public string Libelle
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
        /// Donne ou définit la description du document
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
        /// <summary>
        /// Donne ou définit la clé associée au document. Cette clé,<br/>
        /// qui peut être donnée par un exploitant de l'application,<br/>
        /// facilite les recherches ultérieures sur les documents<br/>
        /// (longueur maximale : 255 caractères)
        /// </summary>
        [TableFieldProperty(c_champCle, 255)]
        [DynamicField("Key")]
        [IndexField]
        public string Cle
        {
            get
            {
                return (string)Row[c_champCle];
            }
            set
            {
                Row[c_champCle] = value;
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit le fait que le document associé est un fichier système (si VRAI);<br/>
        /// C'est notamment le cas d'un document de <see cref="C2iRapportCrystal">rapport Crystal</see>
        /// </summary>
        [TableFieldProperty(c_champIsSystem)]
        [DynamicField("Is system file")]
        public bool IsFichierSysteme
        {
            get
            {
                return (bool)Row[c_champIsSystem];
            }
            set
            {
                Row[c_champIsSystem] = value;
            }
        }


        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit la date de création du Document GED
        /// </summary>
        [
        TableFieldProperty(c_champDateCreation),
        DynamicField("Creation Date")
        ]
        public DateTime DateCreation
        {
            get
            {
                return (DateTime)Row[c_champDateCreation];
            }
            set
            {
                Row[c_champDateCreation] = value;
            }
        }
        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit la date de modification du Document GED,<br/>
        /// c'est à dire la date à laquelle l'association à un fichier (document source) est précisée<br/>
        /// Lorsqu'aucun document n'est associé, cette date est égale à la date de création.
        /// </summary>
        [
        TableFieldProperty(c_champDateMAJ),
        DynamicField("Modification date")
        ]
        public DateTime DateMAJ
        {
            get
            {
                return (DateTime)Row[c_champDateMAJ];
            }
            set
            {
                Row[c_champDateMAJ] = value;
            }
        }
        //-------------------------------------------------------------------
        [TableFieldProperty(c_champReference, 1024)]
        public string ReferenceString
        {
            get
            {
                return (string)Row[c_champReference];
            }
            set
            {
                Row[c_champReference] = value;
            }
        }
        //-------------------------------------------------------------------
        /// <summary>
        /// Donne ou définit la relation avec la référence au document<br/>
        /// permettant l'accès au fichier.
        /// </summary>
        [DynamicField("Reference")]
        public CReferenceDocument ReferenceDoc
        {
            get
            {
                I2iSerializable obj = (I2iSerializable)new CReferenceDocument();
                CStringSerializer ser = new CStringSerializer(ReferenceString, ModeSerialisation.Lecture);
                CResultAErreur result = ser.TraiteObject(ref obj);
                if (!result)
                    return new CReferenceDocument();
                return (CReferenceDocument)obj;
            }
            set
            {
                string strTemp = "";
                CStringSerializer ser = new CStringSerializer(strTemp, ModeSerialisation.Ecriture);
                I2iSerializable obj = value;
                ser.TraiteObject(ref obj);
                ReferenceString = ser.String;
                DateMAJ = DateTime.Now;
                NumVersion++;
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Associe le document à une entité métier
        /// </summary>
        /// <param name="objet">Entité à laquelle le document doit être associé</param>
        /// <returns>TRUE si succès, FALSE si échec</returns>
        [DynamicMethod("Document entity association")]
        public bool AssocieA(CObjetDonneeAIdNumerique objet)
        {
            return LinkTo(objet).Result;
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Retourne la liste des relations d'un document GED, avec des objets de l'application
        /// </summary>
        [RelationFille(typeof(CRelationElementToDocument), "DocumentGed")]
        [DynamicChilds("Elements links", typeof(CRelationElementToDocument))]
        public CListeObjetsDonnees RelationsElements
        {
            get
            {
                return GetDependancesListe(CRelationElementToDocument.c_nomTable, c_champId);
            }
        }

        //-------------------------------------------------------------------
        //Lie un document à un élément
        //Le data du result contient le CRelationElementToDocument
        public CResultAErreur LinkTo(IObjetDonneeAIdNumerique objet)
        {
            CResultAErreur result = CResultAErreur.True;
            if (objet == null)
                return result;
            CRelationElementToDocument relationToDoc = new CRelationElementToDocument(ContexteDonnee);
            CFiltreData filtre = new CFiltreData(c_champId + "=@1 and " +
                CRelationElementToDocument.c_champIdElement + "=@2 and " +
                CRelationElementToDocument.c_champTypeElement + "=@3",
                Id,
                objet.Id,
                objet.GetType().ToString());
            if (!relationToDoc.ReadIfExists(filtre))
            {
                relationToDoc = new CRelationElementToDocument(ContexteDonnee);
                relationToDoc.CreateNewInCurrentContexte();
                relationToDoc.DocumentGed = this;
                relationToDoc.ElementLie = objet;
            }
            result.Data = relationToDoc;
            return result;
        }

        //-------------------------------------------------------------------
        public bool ReadIfExistsCle(string strCle)
        {
            CFiltreData filtre = new CFiltreData(c_champCle + "=@1", strCle);
            return ReadIfExists(filtre);
        }

        ////////////////////////////////////////////////////////////////////////
        public static CListeObjetsDonnees GetListeDocumentsForElement(IObjetDonneeAIdNumerique objet)
        {
            if (objet == null)
                return null;
            CFiltreData filtre = new CFiltreDataAvance(
                CDocumentGED.c_nomTable,
                CRelationElementToDocument.c_nomTable + "." + CRelationElementToDocument.c_champIdElement + "=@1 and " +
                CRelationElementToDocument.c_nomTable + "." + CRelationElementToDocument.c_champTypeElement + "=@2",
                objet.Id,
                objet.GetType().ToString());
            CListeObjetsDonnees liste = new CListeObjetsDonnees(objet.ContexteDonnee, typeof(CDocumentGED));
            liste.RemplissageProgressif = true;
            liste.Filtre = filtre;
            return liste;
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Retourne dans le data du result un CSourceDocument lié sur le document
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static CResultAErreur GetDocument(int nIdSession, CReferenceDocument reference)
        {
            CResultAErreur result = CResultAErreur.True;
            ///TODO
            ///Problème VersionObjet
            IDocumentServeur doc = (IDocumentServeur)CContexteDonnee.GetTableLoader(CDocumentGED.c_nomTable, null, nIdSession);
            result = doc.GetDocument(reference);
            return result;
        }

        //-------------------------------------------------------------------
        public static CResultAErreur SaveDocument(
            int nIdSession, 
            CSourceDocument source, 
            CTypeReferenceDocument tpRef, 
            CReferenceDocument versionPrecedente,
            bool bIncrementeVersionFichier )
        {
            CResultAErreur result = CResultAErreur.True;
            ///TODO
            ///Problème VersionObjet
            IDocumentServeur doc = (IDocumentServeur)CContexteDonnee.GetTableLoader(CDocumentGED.c_nomTable, null, nIdSession);
            result = doc.SaveDocument(source, tpRef, versionPrecedente, bIncrementeVersionFichier);
            return result;
        }

        //-----------------------------------------------------------------------
        /// <summary>
        /// Retourne la liste des relations du Document GED avec des Catégories GED,<br/>
        /// cela donne en fait les catégories auxquelles le document appartient.
        /// </summary>
        [RelationFille(typeof(CRelationDocumentGED_Categorie), "Document")]
        [DynamicChilds("EDM Category Relations", typeof(CRelationDocumentGED_Categorie))]
        public CListeObjetsDonnees RelationsCategories
        {
            get
            {
                return GetDependancesListe(CRelationDocumentGED_Categorie.c_nomTable, c_champId);
            }

        }

        #region IObjetDonneeAChamps Membres

        public string GetNomTableRelationToChamps()
        {
            return CRelationDocumentGED_ChampCustomValeur.c_nomTable;
        }

        public CListeObjetsDonnees GetRelationsToChamps(int nIdChamp)
        {
            CListeObjetsDonnees liste = RelationsChampsCustom;
            liste.InterditLectureInDB = true;
            liste.Filtre = new CFiltreData(CChampCustom.c_champId + " = @1", nIdChamp);
            return liste;
        }

        public CRelationElementAChamp_ChampCustom GetNewRelationToChamp()
        {
            return new CRelationDocumentGED_ChampCustomValeur(ContexteDonnee);
        }

        #endregion

        #region IElementAChamps Membres

        /// <summary>
        /// Retourne la liste des relations entre le Document GED et des valeurs de champs personnalisés
        /// </summary>
        [RelationFille(typeof(CRelationDocumentGED_ChampCustomValeur), "ElementAChamps")]
        [DynamicChilds("Custom fields Relations", typeof(CRelationDocumentGED_ChampCustomValeur))]
        public CListeObjetsDonnees RelationsChampsCustom
        {
            get
            {
                return GetDependancesListe(CRelationDocumentGED_ChampCustomValeur.c_nomTable, c_champId);
            }
        }

        public CChampCustom[] GetChampsHorsFormulaire()
        {
            if (DefinisseursDeChamps.Length == 0)
                return new CChampCustom[0];

            ArrayList lst = new ArrayList();
            foreach (CCategorieGED catGED in DefinisseursDeChamps)
            {
                foreach (CRelationCategorieGED_ChampCustom rel in catGED.RelationsChampsCustomDefinis)
                    lst.Add(rel.ChampCustom);
                foreach (CRelationCategorieGED_Formulaire rel1 in catGED.RelationsFormulaires)
                    foreach (CRelationFormulaireChampCustom rel2 in rel1.Formulaire.RelationsChamps)
                        lst.Remove(rel2.Champ);
            }

            return (CChampCustom[])lst.ToArray(typeof(CChampCustom));
        }

        public CFormulaire[] GetFormulaires()
        {
            if (DefinisseursDeChamps.Length == 0)
                return new CFormulaire[0];

            ArrayList lst = new ArrayList();
            foreach (CCategorieGED catGED in DefinisseursDeChamps)
            {
                foreach (CRelationCategorieGED_Formulaire rel in catGED.RelationsFormulaires)
                    lst.Add(rel.Formulaire);
            }

            return (CFormulaire[])lst.ToArray(typeof(CFormulaire));
        }

        public IDefinisseurChampCustom[] DefinisseursDeChamps
        {
            get
            {
                ArrayList lstDefinisseurs = new ArrayList();
                foreach (CRelationDocumentGED_Categorie rel in RelationsCategories)
                {
                    lstDefinisseurs.Add(rel.Categorie);
                }
                return (IDefinisseurChampCustom[])lstDefinisseurs.ToArray(typeof(IDefinisseurChampCustom));
            }
        }

        #endregion

        #region IElementAVariables Membres

        //----------------------------------------------------
        public object GetValeurChamp(string strIdVariable)
        {
            return CUtilElementAChamps.GetValeurChamp(this, strIdVariable);
        }

        //------------------------------------------------------------------
        public object GetValeurChamp(int idVariable)
        {
            return GetValeurChamp(idVariable, DataRowVersion.Default);
        }

        //------------------------------------------------------------------
        public object GetValeurChamp(int idVariable, DataRowVersion version)
        {
            return CUtilElementAChamps.GetValeurChamp(this, idVariable, version);
        }

        //----------------------------------------------------
        public object GetValeurChamp(IVariableDynamique variable)
        {
            if (variable != null)
                return GetValeurChamp(variable.IdVariable);
            return null;
        }

        //----------------------------------------------------
        public CResultAErreur SetValeurChamp(string strIdVariable, object valeur)
        {
            return CUtilElementAChamps.SetValeurChamp(this, strIdVariable, valeur);
        }

        //----------------------------------------------------
        public CResultAErreur SetValeurChamp(IVariableDynamique variable, object valeur)
        {
            if (variable != null)
                return SetValeurChamp(variable.IdVariable, valeur);
            return CResultAErreur.True;
        }

        //-------------------------------------------------------------------
        public CResultAErreur SetValeurChamp(int idVariable, object valeur)
        {
            return CUtilElementAChamps.SetValeurChamp(this, idVariable, valeur);
        }
        
        #endregion

        //-----------------------------------------------------------------------
        /// <summary>
        /// Si le document ged associé est une image, retourne l'image, sinon retourne null
        /// </summary>
        /// <returns></returns>
        [DynamicMethod("Returns an image of the document, if possible")]
        public Image GetImage()
        {
            using (CProxyGED newProxy = new CProxyGED(ContexteDonnee.IdSession, ReferenceDoc))
            {
                CResultAErreur result = newProxy.CopieFichierEnLocal();
                if (result)
                {
                    try
                    {
                        Image img = Image.FromFile(newProxy.NomFichierLocal);
                        return img;
                    }
                    catch
                    {

                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retourne l'image sous la forme d'un tableau d'octets
        /// </summary>
        /// <returns>Tableau d'octets de l'image</returns>
        [DynamicMethod("Returns image bytes")]
        public byte[] GetImageBytes()
        {
            return GetImageBytesResized(0, 0);
        }

        /// <summary>
        /// Retourne l'image sous la forme d'un tableau d'octets, en retouchant au<br/>
        /// préalable sa taille, en fonction des dimensions maximales passées en paramètres.
        /// </summary>
        /// <param name="nMaxWidth">Largeur maximale</param>
        /// <param name="nMaxHeight">Hauteur maximale</param>
        /// <returns>Tableau d'octets de l'image</returns>
        [DynamicMethod("Returns image bytes", "Max width","Max height")]
        public byte[] GetImageBytesResized(int nMaxWidth, int nMaxHeight)
        {
            byte[] btResult = null;
            Image img = GetImage();
            if (img != null)
            {
                if (nMaxWidth>0 && nMaxHeight > 0 && (img.Width > nMaxWidth || img.Height > nMaxHeight))
                {
                    Size sz = CUtilImage.GetSizeAvecRatio(img, new Size(nMaxWidth, nMaxHeight));
                    Image img2 = CUtilImage.CreateImageImageResizeAvecRatio(img, sz, Color.White);
                    img.Dispose();
                    img = img2;
                }
                MemoryStream stream = new MemoryStream();
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                btResult = stream.GetBuffer();
                stream.Close();
                stream.Dispose();
                img.Dispose();
            }
            return btResult;
        }


        public CTypeReferenceDocument[] TypesAutorisesPourLesUtilisateurs
        {
            get
            {
                IDocumentServeur docServeur = ContexteDonnee.GetTableLoader(CDocumentGED.c_nomTable) as IDocumentServeur;
                return docServeur.TypesAutorisesPourLesUtilisateurs;
            }
        }

        public static void DesactiverControleDocumentsALaSauvegarde(CContexteDonnee ctx, bool bDesactiver)
        {
            ctx.ExtendedProperties[c_strNoEDMControlOnSave] = bDesactiver;
        }

        public static bool IsControleDocumentsALaSauvegardeDesactive(CContexteDonnee ctx)
        {
            bool? bVal = ctx.ExtendedProperties[c_strNoEDMControlOnSave] as bool?;
            return bVal == true;
        }




        #region IElementAEO Membres
        //-----------------------------------------------------------
        /// <summary>
        /// Codes complets (Full_system_code) de toutes les <see cref="CEntiteOrganisationnelle">entités organisationnelles</see> auxquelles est affecté le Document GED<br/>
        /// Le document GED hérite des entités organisationnelles des catégories GED auxquelles il appartient, mais il peut également posséder les siennes propres.
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
                List<IElementAEO> elts = new List<IElementAEO>();
                // Ajoute les Elements liés au Document
                elts.AddRange(from CRelationElementToDocument re in RelationsElements
                        where re.ElementLie is IElementAEO
                        select re.ElementLie as IElementAEO);
                // Ajoute les Catégories du Document
                elts.AddRange(from CRelationDocumentGED_Categorie re in RelationsCategories
                              where re.Categorie is IElementAEO
                              select re.Categorie as IElementAEO);

                return elts.ToArray();
            }
        }

        //-----------------------------------------------------------
        public IElementAEO[] HeritiersEO
        {
            get { return new IElementAEO[0]; }
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
                m_relationsEOProvider.SupprimerEO(this, nIdEO);
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

        //-----------------------------------------------------------
        [DynamicMethod("Add a category to the document", "Category")]
        public void AddCategory(CCategorieGED category)
        {
            if (category == null)
                return;
            CListeObjetsDonnees lstCats = RelationsCategories;
            lstCats.Filtre = new CFiltreData(CCategorieGED.c_champId + "=@1", category.Id);
            if (lstCats.Count == 0)
            {
                CRelationDocumentGED_Categorie rel = new CRelationDocumentGED_Categorie(ContexteDonnee);
                rel.CreateNewInCurrentContexte();
                rel.Document = this;
                rel.Categorie = category;
            }
        }

        //-----------------------------------------------------------
        [DynamicMethod("Remove a category from document", "category")]
        public void RemoveCategory(CCategorieGED category)
        {
            if (category == null)
                return;
            CListeObjetsDonnees lstCats = RelationsCategories;
            lstCats.Filtre = new CFiltreData(CCategorieGED.c_champId + "=@1", category.Id);
            if (lstCats.Count > 0)
            {
                CObjetDonneeAIdNumerique.Delete(lstCats, true);
            }
        }

        //-----------------------------------------------------------
        [DynamicMethod("Returns true if document belongs to category","Category")]
        public bool IsInCategory ( CCategorieGED category )
        {
            if (category == null)
                return false;
            CListeObjetsDonnees lstCats = RelationsCategories;
            lstCats.Filtre = new CFiltreData(CCategorieGED.c_champId + "=@1", category.Id);
            return lstCats.Count != 0;
        }

    }
#else
	/// <summary>
	/// Pour que le namespace sc2i.documents existe
	/// </summary>
	class DummyClass
	{
	}
	#endif
}
