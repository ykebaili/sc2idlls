using System;
using System.Drawing;

using System.Collections;

			

using sc2i.data;
using sc2i.process;
using sc2i.common;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.formulaire;

using sc2i.documents;
using sc2i.multitiers.client;
using System.Collections.Generic;
using sc2i.common.inventaire;
using System.IO;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionModifierPropriete.
	/// </summary>
	[AutoExec("Autoexec")]
    [ReplaceClass("timos.process.CActionCopierMultiLocalDansGed")]
    public class CActionCopierMultiLocalDansGed : CActionFonction
    {
        [Serializable]
        public class CInfoFichierToGed
        {
            private string m_strNomFichier;
            private int m_nIdCategorieGed;

            [NonSerialized]
            private CProcess m_process = null;

            public CInfoFichierToGed()
            {
            }

            public CInfoFichierToGed(string strNomFichier, int nIdCategorie)
            {
                m_strNomFichier = strNomFichier;
                m_nIdCategorieGed = nIdCategorie;
            }

            [DynamicField("File title")]
            public string FileName
            {
                get
                {
                    return Path.GetFileName(m_strNomFichier);
                }
            }
            [DynamicField("File full path")]
            public string FileFullName
            {
                get
                {
                    return m_strNomFichier;
                }
            }

            [DynamicField("Edm category id")]
            public int EDMCategoryId
            {
                get
                {
                    return m_nIdCategorieGed;
                }
            }

            [DynamicField("Active process")]
            public CProcess ActiveProcess
            {
                get{
                    return m_process;
                }
                set{m_process = value;
                }
            }
        }


        public static string c_idServiceSelectMultiForGed = "CLIENT_SELECT_MULTI_DANS_GED";

        private C2iExpression m_formuleListeCategories = null;
        private C2iExpression m_formuleAssocierA = null;
        private C2iExpression m_formuleLibelleDocument = null;
        private C2iExpression m_formuleCleGed = null;


		/// /////////////////////////////////////////
        public CActionCopierMultiLocalDansGed(CProcess process)
			:base(process)
		{
			Libelle = I.T("Add multiple files to EDM document|20105");
			VariableRetourCanBeNull = true;
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("Add multiple files to EDM document|20105"),
                I.T("Add multiple files to EDM document|20105"),
				typeof(CActionCopierMultiLocalDansGed),
				CGestionnaireActionsDisponibles.c_categorieInterface );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return false; }
		}

		/// /////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
		{
			base.AddIdVariablesNecessairesInHashtable ( table );
			AddIdVariablesExpressionToHashtable ( FormuleCle, table );
			AddIdVariablesExpressionToHashtable ( FormuleElementAssocie, table );
			AddIdVariablesExpressionToHashtable ( FormuleListeCategories, table );
            AddIdVariablesExpressionToHashtable(FormuleLibelleDocument, table);

		}

		/// /////////////////////////////////////////
		public override CTypeResultatExpression TypeResultat
		{
			get
			{
				return new CTypeResultatExpression ( typeof(CDocumentGED), true );
			}
		}


		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			return 0;
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize( serializer );

            result = serializer.TraiteObject<C2iExpression>(ref m_formuleAssocierA);
            if ( result )
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleCleGed);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleLibelleDocument);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_formuleListeCategories);


			return result;
		}

		
		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = base.VerifieDonnees();

			return result;
		}

		

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleCle
		{
			get
			{
				if ( m_formuleCleGed == null )
                    m_formuleCleGed = new C2iExpressionConstante("");
                return m_formuleCleGed;
			}
			set
			{
                m_formuleCleGed = value;
			}
		}

        /// ////////////////////////////////////////////////////////
        public C2iExpression FormuleLibelleDocument
        {
            get
            {
                return m_formuleLibelleDocument;
            }
            set
            {
                m_formuleLibelleDocument = value;
            }
        }

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleListeCategories
		{
			get
			{
				return m_formuleListeCategories;
			}
			set
			{
				m_formuleListeCategories = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression FormuleElementAssocie
		{
			get
			{
                return m_formuleAssocierA;
			}
			set
			{
                m_formuleAssocierA = value;
			}
		}

        

        

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			CSessionClient sessionClient = CSessionClient.GetSessionForIdSession ( contexte.IdSession );
            if (sessionClient != null)
            {
                if (sessionClient.GetInfoUtilisateur().KeyUtilisateur == contexte.Branche.KeyUtilisateur)
                {
                    using (C2iSponsor sponsor = new C2iSponsor())
                    {
                        CServiceSurClient service = sessionClient.GetServiceSurClient(c_idServiceSelectMultiForGed);
                        CServiceSurClient serviceGetFile = sessionClient.GetServiceSurClient(CActionCopierLocalDansGed.c_idServiceClientGetFichier);
                        if (service != null && serviceGetFile != null)
                        {
                            sponsor.Register(service);
                            sponsor.Register(serviceGetFile);
                            //Calcule la liste des ids de catégories à gérer
                            List<int> lstIds = new List<int>();
                            if (FormuleListeCategories != null)
                            {
                                CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(contexte.Branche.Process);
                                contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
                                result = FormuleListeCategories.Eval(contexteEval);
                                if (!result)
                                {
                                    return result;
                                }

                                IEnumerable lst = result.Data as IEnumerable;
                                if (lst != null)
                                {
                                    foreach (object obj in lst)
                                    {
                                        if (obj is int)
                                            lstIds.Add((int)obj);
                                        if (obj is CCategorieGED)
                                            lstIds.Add(((CCategorieGED)obj).Id);
                                    }
                                }
                            }
                            result = service.RunService(lstIds);
                            if (result && result.Data is IEnumerable)
                            {
                                List<CDocumentGED> lstDocs = new List<CDocumentGED>();
                                foreach (object obj in (IEnumerable)result.Data)
                                {
                                    CInfoFichierToGed info = obj as CInfoFichierToGed;
                                    if (info != null)
                                    {
                                        string strContenu = info.FileFullName;
                                        CSourceDocument sourceDoc = null;
                                        result = serviceGetFile.RunService(strContenu);
                                        if (!result)
                                            return result;
                                        sourceDoc = result.Data as CSourceDocument;
                                        if (sourceDoc == null)
                                        {
                                            result.EmpileErreur(I.T("Error while retrieving file @1|20020", strContenu));
                                            return result;
                                        }


                                        //On a notre fichier en local, création du document
                                        string strCle = "";
                                        string strLibelle = "";
                                        info.ActiveProcess = contexte.Branche.Process;
                                        CContexteEvaluationExpression ctxEval = new CContexteEvaluationExpression(info);
                                        if (FormuleCle != null)
                                        {
                                            result = FormuleCle.Eval(ctxEval);
                                            if (result && result.Data != null)
                                                strCle = result.Data.ToString();
                                            else
                                            {
                                                result.EmpileErreur(I.T("Document key could not be computed|30050"));
                                                return result;
                                            }
                                        }
                                        if (FormuleLibelleDocument != null)
                                        {
                                            result = FormuleLibelleDocument.Eval(ctxEval);
                                            if (result && result.Data != null)
                                                strLibelle = result.Data.ToString();
                                            else
                                            {
                                                result.EmpileErreur(I.T("Document label could not be computed|30051"));
                                                return result;
                                            }

                                        }
                                        if (strLibelle.Length == 0)
                                            strLibelle = info.FileName;

                                        CObjetDonneeAIdNumerique associeA = null;
                                        if (FormuleElementAssocie != null)
                                        {
                                            result = FormuleElementAssocie.Eval(ctxEval);
                                            if (result)
                                                associeA = result.Data as CObjetDonneeAIdNumerique;
                                        }

                                        


                                        CDocumentGED doc = new CDocumentGED(contexte.ContexteDonnee);
                                        //Si la clé n'est pas nulle, cherche un document avec cette clé
                                        if (strCle.Trim() != "")
                                        {
                                            CFiltreData filtre = new CFiltreData(CDocumentGED.c_champCle + "=@1", strCle);
                                            if (!doc.ReadIfExists(filtre))
                                                doc.CreateNew();
                                            else
                                                doc.BeginEdit();
                                        }
                                        else
                                        {
                                            doc.CreateNew();
                                        }
                                        doc.Libelle = strLibelle;
                                        doc.Cle = strCle;

                                        ArrayList lstToCreate = new ArrayList();
                                        lstToCreate.Add(info.EDMCategoryId);
                                        ArrayList lstToDelete = new ArrayList();
                                        //Affecte les catégories
                                        CListeObjetsDonnees listeCategoriesExistantes = CRelationDocumentGED_Categorie.GetRelationsCategoriesForDocument(doc);
                                        foreach (CRelationDocumentGED_Categorie rel in listeCategoriesExistantes)
                                        {
                                            if (!lstToCreate.Contains(rel.Categorie.Id))
                                                lstToDelete.Add(rel);
                                            lstToCreate.Remove(rel.Categorie.Id);
                                        }
                                        foreach (CRelationDocumentGED_Categorie rel in lstToDelete)
                                            rel.Delete();
                                        foreach (int nId in lstToCreate)
                                        {
                                            CCategorieGED cat = new CCategorieGED(doc.ContexteDonnee);
                                            if (cat.ReadIfExists(nId))
                                            {
                                                CRelationDocumentGED_Categorie rel = new CRelationDocumentGED_Categorie(doc.ContexteDonnee);
                                                rel.CreateNewInCurrentContexte();
                                                rel.Categorie = cat;
                                                rel.Document = doc;
                                            }
                                        }

                                        result = CDocumentGED.SaveDocument(contexte.IdSession, sourceDoc, sourceDoc.TypeReference, doc.ReferenceDoc, true);


                                        if (!result)
                                            return result;
                                        CReferenceDocument refDoc = result.Data as CReferenceDocument;
                                        doc.ReferenceDoc = refDoc;
                                        if (associeA != null)
                                            doc.AssocieA(associeA);
                                        result = doc.CommitEdit();
                                        if (!result)
                                            return result;
                                        lstDocs.Add(doc);
                                        
                                    }
                                }
                                if (VariableResultat != null)
                                    Process.SetValeurChamp(VariableResultat, lstDocs.ToArray());
                                return result;
                            }
                        }
                    }
                }
            }
        
            //Utilisateur pas accessible
            foreach (CLienAction lien in GetLiensSortantHorsErreur())
            {
                if (lien is CLienUtilisateurAbsent)
                {
                    result.Data = lien;
                    return result;
                }
            }
			return result;
		}

        /// ////////////////////////////////////////////////////////
        protected override CLienAction[] GetMyLiensSortantsPossibles()
        {
            ArrayList lst = new ArrayList();
            Hashtable tableLiensExistants = new Hashtable();
            foreach (CLienAction lien in GetLiensSortantHorsErreur())
            {
                tableLiensExistants[lien.GetType()] = true;
            }
            if ( !tableLiensExistants.Contains ( typeof(CLienAction) ))
                lst.Add ( new CLienAction(Process) );
            if ( !tableLiensExistants.Contains ( typeof(CLienUtilisateurAbsent) ))
                lst.Add ( new CLienUtilisateurAbsent(Process) );
            return (CLienAction[])lst.ToArray(typeof(CLienAction));
        }


        
    }
}
