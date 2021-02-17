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
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionModifierPropriete.
	/// </summary>
	[AutoExec("Autoexec")]
    public class CActionZipGedDansGed : CActionFonction
    {
        /* TESTDBKEYOK (XL)*/

        private List<CDbKey> m_listeDbKeysCategorieStockage = new List<CDbKey>();
		private C2iExpression m_expressionCleGED = new C2iExpressionConstante("");
		private C2iExpression m_expressionLibelleDocument = null;
		private C2iExpression m_expressionDescriptifDocument = null;
        private C2iExpression m_expressionListeDocuments = null;
        //Nom de chaque fichier dans le zip à partir du document ged source
        private C2iExpression m_expressionNomFichier = null;


		/// /////////////////////////////////////////
        public CActionZipGedDansGed(CProcess process)
			:base(process)
		{
			Libelle = I.T("ZIP EDM documents|20050");
			VariableRetourCanBeNull = true;
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
                I.T("ZIP EDM documents|20050"),
                I.T("ZIP EDM documents|20050"),
				typeof(CActionZipGedDansGed),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
		}

		/// /////////////////////////////////////////////////////////
		public override bool PeutEtreExecuteSurLePosteClient
		{
			get { return true; }
		}

		/// /////////////////////////////////////////
		public override void AddIdVariablesNecessairesInHashtable(Hashtable table)
		{
			base.AddIdVariablesNecessairesInHashtable ( table );
			AddIdVariablesExpressionToHashtable ( ExpressionCle, table );
			AddIdVariablesExpressionToHashtable ( ExpressionLibelle, table );
			AddIdVariablesExpressionToHashtable ( ExpressionDescriptif, table );
            AddIdVariablesExpressionToHashtable(ExpressionListeDocuments, table);
            AddIdVariablesExpressionToHashtable(ExpressionNomsFichiers, table);

		}

		/// /////////////////////////////////////////
		public override CTypeResultatExpression TypeResultat
		{
			get
			{
				return new CTypeResultatExpression ( typeof(CDocumentGED), false );
			}
		}


		/// /////////////////////////////////////////
		private int GetNumVersion()
		{
			//return 0;
            return 1; // Passage des Ids de Catégorie en DbKeys
		}


		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MySerialize ( C2iSerializer serializer )
		{
			int nVersion = GetNumVersion();
			CResultAErreur result = serializer.TraiteVersion ( ref nVersion );
			if ( !result )
				return result;
			result = base.MySerialize( serializer );

            result = serializer.TraiteObject<C2iExpression>(ref m_expressionCleGED);
            if ( result )
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionListeDocuments);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionDescriptifDocument);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionLibelleDocument);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionNomFichier);

            int nNbCategories = m_listeDbKeysCategorieStockage.Count;
			serializer.TraiteInt ( ref nNbCategories );
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture:
					foreach ( CDbKey dbKeyCat in m_listeDbKeysCategorieStockage )
					{
                        CDbKey dbKeyTmp = dbKeyCat;
                        serializer.TraiteDbKey(ref dbKeyTmp);
					}
					break;
				case ModeSerialisation.Lecture :
					m_listeDbKeysCategorieStockage.Clear();
					for ( int nVal = 0; nVal < nNbCategories; nVal++ )
					{
                        CDbKey dbKeyTemp = null;
                        if (nVersion < 1)
                            serializer.ReadDbKeyFromOldId(ref dbKeyTemp, typeof(CCategorieGED));
                        else
                            serializer.TraiteDbKey(ref dbKeyTemp);
                        m_listeDbKeysCategorieStockage.Add(dbKeyTemp);
					}
					break;
			}

			return result;
		}

		
		/// ////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees()
		{
			CResultAErreur result = base.VerifieDonnees();

			return result;
		}

		/// ////////////////////////////////////////////////////////
		[ExternalReferencedEntityDbKey(typeof(CCategorieGED))]
        public CDbKey[] ListeDbKeysCategoriesStockage
		{
			get
			{
				return m_listeDbKeysCategorieStockage.ToArray();
			}
            set
            {
                m_listeDbKeysCategorieStockage = new List<CDbKey>(value);
            }
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression ExpressionCle
		{
			get
			{
				if ( m_expressionCleGED == null )
					m_expressionCleGED = new C2iExpressionConstante("");
				return m_expressionCleGED;
			}
			set
			{
				m_expressionCleGED = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression ExpressionLibelle
		{
			get
			{
				if ( m_expressionLibelleDocument == null )
					m_expressionLibelleDocument = new C2iExpressionConstante("");
				return m_expressionLibelleDocument;
			}
			set
			{
				m_expressionLibelleDocument = value;
			}
		}

		/// ////////////////////////////////////////////////////////
		public C2iExpression ExpressionDescriptif
		{
			get
			{
				if ( m_expressionDescriptifDocument == null )
					m_expressionDescriptifDocument = new C2iExpressionConstante("");
				return m_expressionDescriptifDocument;
			}
			set
			{
				m_expressionDescriptifDocument = value;
			}
		}

        
        // ////////////////////////////////////////////////////////
        public C2iExpression ExpressionListeDocuments
        {
            get
            {
                if (m_expressionListeDocuments == null)
                    m_expressionListeDocuments = new C2iExpressionConstante("");
                return m_expressionListeDocuments;
            }
            set
            {
                m_expressionListeDocuments = value;
            }
        }

        // ////////////////////////////////////////////////////////
        public C2iExpression ExpressionNomsFichiers
        {
            get
            {
                if (m_expressionNomFichier == null)
                    m_expressionNomFichier = new C2iExpressionConstante("");
                return m_expressionNomFichier;
            }
            set
            {
                m_expressionNomFichier = value;
            }
        }

		/// ////////////////////////////////////////////////////////
        protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
        {
            CResultAErreur result = CResultAErreur.True;

            if (ExpressionListeDocuments == null)
            {
                result.EmpileErreur(I.T("Document list formula is null|20051"));
                return result;
            }
            CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(contexte.Branche.Process);
            result = ExpressionListeDocuments.Eval(contexteEval);
            if (!result)
                return result;

            IEnumerable enDocs = result.Data as IEnumerable;
            string strCle = "";
            string strDescriptif = "";
            string strLibelle = "";
            result = ExpressionCle.Eval(contexteEval);
            if (result)
                strCle = result.Data.ToString();
            else
            {
                result.EmpileErreur(I.T("Document key could not be computed|30050"));
                return result;
            }
            if (enDocs == null)
            {
                result.EmpileErreur(I.T("Document list formul returns an incorrect value|20052"));
                return result;
            }
            List<CDocumentGED> lstDocs = new List<CDocumentGED>();
            try
            {
                foreach (CDocumentGED doc in enDocs)
                {
                    if ( doc.Cle != strCle )
                        lstDocs.Add(doc);
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error in document list|20053"));
            }

            MemoryStream stream = new MemoryStream();
            ZipOutputStream zipStream = new ZipOutputStream(stream);
            zipStream.SetLevel(9);
            HashSet<string> lstNomsDocuments = new HashSet<string>();
            foreach (CDocumentGED doc in lstDocs)
            {
                CContexteEvaluationExpression ctxDoc = new CContexteEvaluationExpression(doc);
                string strNomFichier = "";
                if (ExpressionNomsFichiers != null)
                {
                    result = ExpressionNomsFichiers.Eval(ctxDoc);
                    if (result && result.Data != null)
                        strNomFichier = result.Data.ToString();
                }
                if (strNomFichier == "")
                    strNomFichier = I.T("File|20054");
                foreach (char c in "\"/\\*?<>|:")
                    if (strNomFichier.Contains(c.ToString()))
                        strNomFichier = strNomFichier.Replace(c, '_');
 
                if (lstNomsDocuments.Contains(strNomFichier.ToUpper()))
                {
                    int nIndex = 1;
                    string strTmp = strNomFichier + "_" + nIndex;
                    while (lstNomsDocuments.Contains(strTmp.ToUpper()))
                    {
                        nIndex++;
                        strTmp = strNomFichier + "_" + nIndex;
                    }
                    strNomFichier = strTmp.ToUpper();
                }
                lstNomsDocuments.Add(strNomFichier.ToUpper());
                strNomFichier += "."+doc.ReferenceDoc.GetExtension();

                using (CProxyGED proxy = new CProxyGED(contexte.IdSession, doc.ReferenceDoc))
                {
                    result = proxy.CopieFichierEnLocal();
                    if (result)
                    {
                        ZipEntry entry = new ZipEntry( ZipEntry.CleanName(strNomFichier));
                        try
                        {
                            FileStream fstream = new FileStream(proxy.NomFichierLocal, FileMode.Open, FileAccess.Read);
                            entry.DateTime = DateTime.Now;
                            entry.Size = fstream.Length;
                            
                            int nBufLength = 1024 * 1024;
                            byte[] buffer = new byte[nBufLength];
                            int nRead = 0;
                            
                            zipStream.PutNextEntry(entry);
                            while ((nRead = fstream.Read(buffer, 0, nBufLength)) != 0)
                            {
                                zipStream.Write(buffer, 0, nRead);
                            }
                            fstream.Close();
                            zipStream.CloseEntry();

                        }
                        catch (Exception e)
                        {
                            result.EmpileErreur(new CErreurException(e));
                            return result;
                        }
                    }
                }
            }
            zipStream.Finish();
            
            stream.Seek(0, SeekOrigin.Begin);

            CSourceDocumentStream sourceDoc = new CSourceDocumentStream(stream, "zip");

            //On a notre stream zippé, création du document
            


            result = ExpressionLibelle.Eval(contexteEval);
            if (result)
                strLibelle = result.Data.ToString();
            else
            {
                result.EmpileErreur(I.T("Document label could not be computed|30051"));
                return result;
            }

            result = ExpressionDescriptif.Eval(contexteEval);
            if (result)
                strDescriptif = result.Data.ToString();
            else
            {
                result.EmpileErreur(I.T("Document description could not be computed|30052"));
                return result;
            }

            CDocumentGED docZip = new CDocumentGED(contexte.ContexteDonnee);
            //Si la clé n'est pas nulle, cherche un document avec cette clé
            if (strCle.Trim() != "")
            {
                CFiltreData filtre = new CFiltreData(CDocumentGED.c_champCle + "=@1", strCle);
                if (!docZip.ReadIfExists(filtre))
                    docZip.CreateNew();
                else
                    docZip.BeginEdit();
            }
            else
            {
                docZip.CreateNew();
            }
            docZip.Libelle = strLibelle;
            docZip.Descriptif = strDescriptif;
            docZip.Cle = strCle;

            List<CDbKey> lstToCreate = new List<CDbKey>(ListeDbKeysCategoriesStockage);
            List<CRelationDocumentGED_Categorie> lstToDelete = new List<CRelationDocumentGED_Categorie>();
            //Affecte les catégories
            CListeObjetsDonnees listeCategoriesExistantes = CRelationDocumentGED_Categorie.GetRelationsCategoriesForDocument(docZip);
            foreach (CRelationDocumentGED_Categorie rel in listeCategoriesExistantes)
            {
                if (!lstToCreate.Contains(rel.Categorie.DbKey))
                    lstToDelete.Add(rel);
                lstToCreate.Remove(rel.Categorie.DbKey);
            }
            foreach (CRelationDocumentGED_Categorie rel in lstToDelete)
                rel.Delete();
            foreach (CDbKey dbKey in lstToCreate)
            {
                CCategorieGED cat = new CCategorieGED(docZip.ContexteDonnee);
                if (cat.ReadIfExists(dbKey))
                {
                    CRelationDocumentGED_Categorie rel = new CRelationDocumentGED_Categorie(docZip.ContexteDonnee);
                    rel.CreateNewInCurrentContexte();
                    rel.Categorie = cat;
                    rel.Document = docZip;
                }
            }
            result = CDocumentGED.SaveDocument(contexte.IdSession, sourceDoc, sourceDoc.TypeReference, docZip.ReferenceDoc, true);
            if (sourceDoc != null)
                sourceDoc.Dispose();
            zipStream.Close();
            zipStream.Dispose();

            stream.Dispose();
            if (!result)
                return result;
            CReferenceDocument refDoc = result.Data as CReferenceDocument;
            docZip.ReferenceDoc = refDoc;
            result = docZip.CommitEdit();
            if (!result)
                return result;
            if (VariableResultat != null)
                Process.SetValeurChamp(VariableResultat, docZip);
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
