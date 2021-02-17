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
using System.Net;

namespace sc2i.process
{
	/// <summary>
	/// Description résumée de CActionModifierPropriete.
	/// </summary>
	[AutoExec("Autoexec")]
    [ReplaceClass("timos.process.CActionCopierLocalDansGed")]
    public class CActionCopierLocalDansGed : CActionFonction
    {
        public static string c_idServiceClientGetFichier = "CLIENT_GET_FILE";

        [Serializable]
        public class CParametresCopierLocalDansGed
        {
            public string NomFichierLocal="";
            public string User="";
            public string Password="";

        }

        /* TESTDBKEYOK (XL)*/

        private List<CDbKey> m_listeDbKeysCategorieStockage = new List<CDbKey>();
		private C2iExpression m_expressionCleGED = new C2iExpressionConstante("");
		private C2iExpression m_expressionLibelleDocument = null;
		private C2iExpression m_expressionDescriptifDocument = null;
        private C2iExpression m_expressionContenu = null;
        private C2iExpression m_expressionUser = null;
        private C2iExpression m_expressionPassword = null;
        private bool m_bCreerFichierTexteAPartirDeLaFormuleContenu = false;


		/// /////////////////////////////////////////
        public CActionCopierLocalDansGed(CProcess process)
			:base(process)
		{
			Libelle = I.T("Create EDM document|20019");
			VariableRetourCanBeNull = true;
		}

		/// /////////////////////////////////////////////////////////
		public static void Autoexec()
		{
			CGestionnaireActionsDisponibles.RegisterTypeAction(
				I.T("Copy local document into EDM|20019"),
				I.T("Copy local document into EDM|20019"),
				typeof(CActionCopierLocalDansGed),
				CGestionnaireActionsDisponibles.c_categorieDonnees );
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
			AddIdVariablesExpressionToHashtable ( ExpressionCle, table );
			AddIdVariablesExpressionToHashtable ( ExpressionLibelle, table );
			AddIdVariablesExpressionToHashtable ( ExpressionDescriptif, table );
            AddIdVariablesExpressionToHashtable(ExpressionContenu, table);
            AddIdVariablesExpressionToHashtable(ExpressionUser, table);
            AddIdVariablesExpressionToHashtable(ExpressionPassword, table);

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
            //return 1;//Ajout de l'option créer un fichier texte à partir du contenu
           // return 2;// Passage des Ids en DbKey
            return 3;//Ajout des options de user et password
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
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionContenu);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionDescriptifDocument);
            if (result)
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionLibelleDocument);

            int nNbCategories = m_listeDbKeysCategorieStockage.Count;
            serializer.TraiteInt(ref nNbCategories);
			switch ( serializer.Mode )
			{
				case ModeSerialisation.Ecriture:
                    foreach (CDbKey dbKey in m_listeDbKeysCategorieStockage)
					{
                        CDbKey dbKeyTmp = dbKey;
                        serializer.TraiteDbKey(ref dbKeyTmp);
					}
					break;
				case ModeSerialisation.Lecture :
                    m_listeDbKeysCategorieStockage.Clear();
                    for (int nVal = 0; nVal < nNbCategories; nVal++)
                    {
                        CDbKey dbKeyTmp = null;
                        if (nVersion < 2)
                            serializer.ReadDbKeyFromOldId(ref dbKeyTmp, typeof(CCategorieGED));
                        else
                            serializer.TraiteDbKey(ref dbKeyTmp);
                        m_listeDbKeysCategorieStockage.Add(dbKeyTmp);
                    }
					break;
			}
            if (nVersion >= 1)
                serializer.TraiteBool(ref m_bCreerFichierTexteAPartirDeLaFormuleContenu);
            if (nVersion >= 3)
            {
                result = serializer.TraiteObject<C2iExpression>(ref m_expressionUser);
                if (result)
                    result = serializer.TraiteObject<C2iExpression>(ref m_expressionPassword);
                if (!result)
                    return result;
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

        /// ////////////////////////////////////////////////////////
        public bool LeContenuEstUnTexte
        {
            get
            {
                return m_bCreerFichierTexteAPartirDeLaFormuleContenu;
            }
            set
            {
                m_bCreerFichierTexteAPartirDeLaFormuleContenu = value;
            }
        }

        // ////////////////////////////////////////////////////////
        public C2iExpression ExpressionContenu
        {
            get
            {
                if (m_expressionContenu == null)
                    m_expressionContenu = new C2iExpressionConstante("");
                return m_expressionContenu;
            }
            set
            {
                m_expressionContenu = value;
            }
        }

        // ////////////////////////////////////////////////////////
        public C2iExpression ExpressionUser
        {
            get
            {
                return m_expressionUser;
            }
            set
            {
                m_expressionUser = value;
            }
        }

        // ////////////////////////////////////////////////////////
        public C2iExpression ExpressionPassword
        {
            get
            {
                return m_expressionPassword;
            }
            set
            {
                m_expressionPassword = value;
            }
        }

		/// ////////////////////////////////////////////////////////
		protected override CResultAErreur MyExecute(CContexteExecutionAction contexte)
		{
			CResultAErreur result = CResultAErreur.True;
			CSessionClient sessionClient = CSessionClient.GetSessionForIdSession ( contexte.IdSession );
            if (sessionClient != null)
            {
                //TESTDBKEYOK
                if (sessionClient.GetInfoUtilisateur().KeyUtilisateur == contexte.Branche.KeyUtilisateur)
                {
                    using (C2iSponsor sponsor = new C2iSponsor())
                    {
                        CServiceSurClient service = sessionClient.GetServiceSurClient(c_idServiceClientGetFichier);
                        CParametresCopierLocalDansGed parametreService = new CParametresCopierLocalDansGed();
                        CFichierLocalTemporaire fichierLocal = null;
                        FileStream localStream = null;
                        if (service != null)
                        {
                            sponsor.Register(service);
                            //Calcule le contenu
                            CContexteEvaluationExpression contexteEval = new CContexteEvaluationExpression(contexte.Branche.Process);
                            contexteEval.AttacheObjet(typeof(CContexteDonnee), contexte.ContexteDonnee);
                            result = ExpressionContenu.Eval(contexteEval);
                            if (!result)
                                return result;
                            parametreService.NomFichierLocal = result.Data as string;

                            if (ExpressionUser != null && (result = ExpressionUser.Eval(contexteEval)))
                                if (result.Data != null)
                                    parametreService.User = result.Data.ToString();

                            if ( ExpressionPassword != null && (result = ExpressionPassword.Eval(contexteEval)))
                                if ( result.Data != null )
                                    parametreService.Password = result.Data.ToString();

                            CSourceDocument sourceDoc = null;
                            if ( parametreService.NomFichierLocal.ToUpper().StartsWith("FTP://"))
                            {
                                CResultAErreurType<CFichierLocalTemporaire> resFic = GetFileFromFtp(
                                    parametreService);
                                if ( !resFic )
                                {
                                    result.EmpileErreur(resFic.Erreur);
                                    return result;
                                }
                                fichierLocal = resFic.DataType;
                                localStream = new FileStream(fichierLocal.NomFichier, FileMode.Open, FileAccess.Read);
                                sourceDoc = new CSourceDocumentStream(localStream, "txt");
                            }
                            else if (!m_bCreerFichierTexteAPartirDeLaFormuleContenu)
                            {
                                result = service.RunService(parametreService);
                                if (!result)
                                    return result;
                                sourceDoc = result.Data as CSourceDocument;
                                if (sourceDoc == null)
                                {
                                    result.EmpileErreur(I.T("Error while retrieving file @1|20020", parametreService.NomFichierLocal));
                                    return result;
                                }
                            }
                            else
                            {
                                fichierLocal = new CFichierLocalTemporaire("txt");
                                localStream = new FileStream(fichierLocal.NomFichier, FileMode.CreateNew, FileAccess.Write);
                                StreamWriter writer = new StreamWriter(localStream);
                                writer.Write(parametreService.NomFichierLocal);
                                writer.Close();
                                localStream.Close();
                                localStream = new FileStream(fichierLocal.NomFichier, FileMode.Open, FileAccess.Read);
                                sourceDoc = new CSourceDocumentStream(localStream, "txt");
                            }
                            //On a notre fichier en local, création du document
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
                            doc.Descriptif = strDescriptif;
                            doc.Cle = strCle;

                            List<CDbKey> lstToCreate = new List<CDbKey>(ListeDbKeysCategoriesStockage);
                            List<CRelationDocumentGED_Categorie> lstToDelete = new List<CRelationDocumentGED_Categorie>();
                            //Affecte les catégories
                            CListeObjetsDonnees listeCategoriesExistantes = CRelationDocumentGED_Categorie.GetRelationsCategoriesForDocument(doc);
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
                                CCategorieGED cat = new CCategorieGED(doc.ContexteDonnee);
                                if (cat.ReadIfExists(dbKey))
                                {
                                    CRelationDocumentGED_Categorie rel = new CRelationDocumentGED_Categorie(doc.ContexteDonnee);
                                    rel.CreateNewInCurrentContexte();
                                    rel.Categorie = cat;
                                    rel.Document = doc;
                                }
                            }
                            result = CDocumentGED.SaveDocument(contexte.IdSession, sourceDoc, sourceDoc.TypeReference, doc.ReferenceDoc, true);
                            if (sourceDoc != null)
                                sourceDoc.Dispose();
                            if ( localStream != null )
                                localStream.Dispose();
                            if (fichierLocal != null)
                            {
                                fichierLocal.Dispose();
                            }
                            

                            if (!result)
                                return result;
                            CReferenceDocument refDoc = result.Data as CReferenceDocument;
                            doc.ReferenceDoc = refDoc;
                            result = doc.CommitEdit();
                            if (!result)
                                return result;
                            if (VariableResultat != null)
                                Process.SetValeurChamp(VariableResultat, doc);
                            return result;
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

        private CResultAErreurType<CFichierLocalTemporaire> GetFileFromFtp(CActionCopierLocalDansGed.CParametresCopierLocalDansGed parametre)
        {
            CResultAErreurType<CFichierLocalTemporaire> result = new CResultAErreurType<CFichierLocalTemporaire>();
            string strExt = "dat";
            int nPosPoint = parametre.NomFichierLocal.LastIndexOf(".");
            if (nPosPoint >= 0)
                strExt = parametre.NomFichierLocal.Substring(nPosPoint + 1);
            CFichierLocalTemporaire fichierTemporaireFromFTP = new CFichierLocalTemporaire(strExt);
            fichierTemporaireFromFTP.CreateNewFichier();

            using (FileStream streamDest = new FileStream(
                fichierTemporaireFromFTP.NomFichier,
                FileMode.CreateNew,
                FileAccess.Write))
            {
                try
                {
                    FtpWebRequest req = (FtpWebRequest)WebRequest.Create(parametre.NomFichierLocal);
                    req.Method = WebRequestMethods.Ftp.DownloadFile;
                    req.Credentials = new NetworkCredential(parametre.User, parametre.Password);

                    FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                    Stream respStream = response.GetResponseStream();
                    byte[] buffer = new byte[256];
                    int nNbLus = 0;
                    while ((nNbLus = respStream.Read(buffer, 0, 256)) != 0)
                        streamDest.Write(buffer, 0, nNbLus);
                    respStream.Close();
                    respStream.Dispose();
                    response.Close();
                    response.Dispose();
                    result.DataType = fichierTemporaireFromFTP;
                }
                catch (Exception e)
                {
                    result.EmpileErreur(new CErreurException(e));
                }
                streamDest.Close();

            }
            return result;
        }

    }
}
