using System;
using System.IO;
using System.Collections;
using System.Threading;
using System.Data;

using sc2i.common;
using sc2i.data;
using sc2i.data.serveur;
using sc2i.documents;

using sc2i.multitiers.server;
using sc2i.multitiers.client;


namespace sc2i.documents.serveur
{
#if !PDA_DATA
	/// <summary>
	/// Description résumée de CDocumentGEDServeur.
	/// </summary>
	public class CDocumentGEDServeur : CObjetServeur, IDocumentServeur
	{
        
		/*Fonctionnement de la GED
		 * Lorsque la fonction SaveDocumentToGed est appellée,
		 * Un nouveau fichier est stocké dans le répertoire de GEd
		 * Ce fichier est stocké de manière temporaire, il 
		 * ne sera validé que lorsqu'un CDocumentGed pointant sur 
		 * ce fichier aura été sauvegardé.
		 * Si un fichier n'est pas validé au bout de 120 minutes,
		 * il sera automatiquement détruit.
		 * */


        //Si une valeur est trouvé pour cette clé, le système ne gère pas les evenements sur date
        public static string c_cleDesactivationNettoyage = "CDocumentGEDServeur_Inactif";

		private class CReferenceTemporaire
		{
			public CReferenceDocument Reference;
			public DateTime DatePeremption = DateTime.Now.AddMinutes(120);
			public CReferenceTemporaire ( CReferenceDocument reference )
			{
				Reference = reference;
			}

			/// ////////////////////////////////////////
			public void Renew()
			{
				DatePeremption = DateTime.Now.AddMinutes(30);
			}
		}

		private static ArrayList m_listeReferencesTemporaires = new ArrayList();

		private static Timer m_timerNettoyage = null;


		private static string m_strPath = "";
        private static CTypeReferenceDocument[] m_TypesAutorisesPourLesUtilisateurs = CTypeReferenceDocument.Types;
		//-------------------------------------------------------------------
		public CDocumentGEDServeur ( int nIdSession )
			:base ( nIdSession )
		{
		}
		//-------------------------------------------------------------------
		/// <summary>
		/// Récupère une source document pour le document demandé
		/// </summary>
		/// <param name="refDoc"></param>
		/// <returns></returns>
		public CResultAErreur GetDocument(CReferenceDocument refDoc)
		{
			CResultAErreur result = CResultAErreur.True;
			
			CSourceDocument source = null;
            try
            {
                switch (refDoc.TypeReference.Code)
                {
                    case CTypeReferenceDocument.TypesReference.Fichier:
                        source = new CSourceDocumentStream(
                            new FileStream(m_strPath + refDoc.NomFichier
                            , FileMode.Open, FileAccess.Read)
                            );
                        break;
                    case CTypeReferenceDocument.TypesReference.LienDirect:
                        source = new CSourceDocumentLienDirect(refDoc.NomFichier);
                        break;
                    default:
                        result.EmpileErreur(I.T("Document type '@1' not supported|108", refDoc.TypeReference.Libelle));
                        break;
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Impossible to read the file|107"));
                return result;
            }

			result.Data = source;

			return result;
		}

		//-------------------------------------------------------------------
		public CResultAErreur SaveDocument ( CSourceDocument source,
			CTypeReferenceDocument typeReference,
			CReferenceDocument versionPrecedente,
            bool bIncrementeVersionFichier)
		{
			CResultAErreur result = CResultAErreur.True;
			//Copie du fichier
			
            try
            {
                switch (typeReference.Code)
                {
                    case CTypeReferenceDocument.TypesReference.Fichier:
                        if (!(source is CSourceDocumentStream))
                        {
                            result.EmpileErreur(I.T("Document type '@1' source not supported|109", source.GetType().Name));
                            return result;
                        }
                        string strNomFichier = "";
                        if (versionPrecedente != null)
                        {
                            strNomFichier = versionPrecedente.NomFichier;
                            strNomFichier = strNomFichier.Replace("..", ".");//Correction erreurs passées
                            if ( bIncrementeVersionFichier )
                                strNomFichier = IncNomFichier(strNomFichier);
                            string strExt = ((CSourceDocumentStream)source).Extension;
                            if (strExt.ToUpper() != "DAT")
                                strNomFichier = ChangeExtension(strNomFichier, ((CSourceDocumentStream)source).Extension);

                        }
                        else
                        {
                            string strPath = DateTime.Now.Year.ToString() + "\\";
                            strPath += DateTime.Now.Month.ToString() + "\\";
                            strPath += DateTime.Now.Day.ToString() + "\\";
                            strNomFichier = strPath + CGenerateurStringUnique.GetNewNumero(m_nIdSession) + "_0.";
                            strNomFichier += ((CSourceDocumentStream)source).Extension;
                        }
                        //S'assure que le chemin du fichier existe
                        string[] strChemins = strNomFichier.Split('\\');
                        int nChemin = 0;
                        string strTemp = m_strPath;
                        foreach (string strChemin in strChemins)
                        {
                            if (nChemin < strChemins.Length - 1)
                            {
                                if (!Directory.Exists(m_strPath + strChemin))
                                    Directory.CreateDirectory(strTemp + strChemin);
                                strTemp += strChemin + "\\";
                            }
                            nChemin++;
                        }

                        if (strNomFichier.LastIndexOf('.') < 0)
                            strNomFichier += ((CSourceDocumentStream)source).Extension;

                        if (File.Exists(m_strPath + strNomFichier))
                            File.Delete(m_strPath + strNomFichier);
                        FileStream stream = new FileStream(m_strPath + strNomFichier, FileMode.CreateNew);

                        Stream sourceStream = ((CSourceDocumentStream)source).SourceStream;

                        result = CStreamCopieur.CopyStream(sourceStream, stream, 32000);
                        if (!result)
                        {
                            File.Delete(strNomFichier);
                            return result;
                        }
                        CReferenceDocument referenceFinale = new CReferenceDocument();
                        referenceFinale.NomFichier = strNomFichier;
                        referenceFinale.CodeTypeReference = CTypeReferenceDocument.TypesReference.Fichier;
                        referenceFinale.TailleFichier = (int)new FileInfo(m_strPath + strNomFichier).Length;

                        m_listeReferencesTemporaires.Add(new CReferenceTemporaire(referenceFinale));
                        if (m_timerNettoyage == null && C2iAppliServeur.GetValeur(c_cleDesactivationNettoyage) != null)
                            m_timerNettoyage = new Timer(new TimerCallback(NettoyageTemporaires), null, 1000 * 60 * 30, 1000 * 60 * 30);

                        result.Data = referenceFinale;
                        break;
                    case CTypeReferenceDocument.TypesReference.LienDirect :
                        //Vérifie que le lien est accessible depuis le serveur
                        CSourceDocumentLienDirect sourceDirecte = source as CSourceDocumentLienDirect;
                        if (sourceDirecte == null)
                        {
                            result.EmpileErreur(I.T("Document type '@1' source not supported|109", source.GetType().Name));
                            return result;
                        }
                        CReferenceDocument referenceFinaleLien = new CReferenceDocument();
                        referenceFinaleLien.NomFichier = sourceDirecte.NomFichier;
                        referenceFinaleLien.CodeTypeReference = CTypeReferenceDocument.TypesReference.LienDirect;
                        referenceFinaleLien.TailleFichier = (int)new FileInfo(sourceDirecte.NomFichier).Length;

                        if (!File.Exists(sourceDirecte.NomFichier))
                        {
                            result.EmpileErreur("File @1 cannot be stored in EDM, it can not be reached from server|20000", sourceDirecte.NomFichier);
                            return result;
                        }
                        result.Data = referenceFinaleLien;
                        break;
                    default :
                        result.EmpileErreur(I.T("File reference type @1 not supported|110", typeReference.ToString()));
                        return result;
                }
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException ( e ) );
                result.EmpileErreur(I.T("Error while saving EDM |111"));
			}
			return result;

		}

		//-------------------------------------------------------------------
		private string ChangeExtension ( string strFichier, string strNewExtension )
		{
			int nPos = strFichier.LastIndexOf('.');
			if ( nPos < 0 )
			{
				strFichier += "."+strNewExtension;
			}
			else
			{
				strFichier = strFichier.Substring(0, nPos )+"."+strNewExtension;
			}
			return strFichier;
		}

		//-------------------------------------------------------------------
		private static void NettoyageTemporaires ( object state )
		{
			ArrayList lstToDelete = new ArrayList();
			foreach ( CReferenceTemporaire refTemp in m_listeReferencesTemporaires )
			{
				if ( refTemp.DatePeremption < DateTime.Now )
					lstToDelete.Add ( refTemp );
			}

			foreach ( CReferenceTemporaire refTemp in lstToDelete )
			{
				DeleteDocument ( refTemp.Reference );
			}
		}

		/// //////////////////////////////////////////////////////
		protected static CResultAErreur DeleteDocument ( CReferenceDocument reference )
		{
            CResultAErreur result = CResultAErreur.True;
            if (reference.TypeReference.Code == CTypeReferenceDocument.TypesReference.LienDirect)
            {
                //On ne supprime pas le doc, seulement le lien, donc deleteDocument ne fait rien
                return result;
            }
			
			string strNomFichier = m_strPath+reference.NomFichier;
			if ( File.Exists ( strNomFichier ) )
			{
				try
				{
					File.Delete ( strNomFichier );
				}
				catch ( Exception e )
				{
					result.EmpileErreur ( new CErreurException ( e ) );
					result.EmpileErreur(I.T("Error while document deleting|112"));
				}
			}
			return result;
		}

		/// //////////////////////////////////////////////////////
		protected static void ValideDocument ( CReferenceDocument refDoc )
		{
			foreach ( CReferenceTemporaire refTemp in m_listeReferencesTemporaires.ToArray(typeof(CReferenceTemporaire)) )
			{
				if ( refTemp.Reference.Equals ( refDoc ) )
				{
					m_listeReferencesTemporaires.Remove ( refTemp );
					break;
				}
			}

		}

		/// //////////////////////////////////////////////////////
		protected static CResultAErreur PreValideDocument ( CReferenceDocument refDoc )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( refDoc == null )
				return result;
            if (refDoc.TypeReference.Code == CTypeReferenceDocument.TypesReference.LienDirect)
                return result;
			foreach ( CReferenceTemporaire refTemp in m_listeReferencesTemporaires.ToArray(typeof(CReferenceTemporaire)) )
			{
				if ( refTemp.Reference.Equals ( refDoc ) )
				{
					refTemp.Renew();
					return result;
				}
			}
			if ( !File.Exists ( m_strPath+refDoc.NomFichier ) )
				result.EmpileErreur(I.T("Document isn't available in EDM (exceeded time)|113"));
			return result;
		}

		
		//-------------------------------------------------------------------
		private string IncNomFichier(string strNomFichier)
		{
			int pos1 = strNomFichier.LastIndexOf("_");
			int pos2 = strNomFichier.LastIndexOf(".");

			string strTemp1 = strNomFichier.Substring(0, pos1+1);
			string strTemp2 = strNomFichier.Substring(pos2);
			try
			{
				int nNewNum = System.Convert.ToInt32( (strNomFichier.Substring(pos1+1, pos2-pos1-1)) );
				nNewNum++;
				strTemp1 = strTemp1 +  nNewNum.ToString() + strTemp2;
			}
			catch
			{
				strTemp1 = strNomFichier;
			}

			return strTemp1;
		}
		/*//-------------------------------------------------------------------
		public CResultAErreur UpdateDocument(CSourceDocument src, int nIdDoc)
		{
			CResultAErreur result = CResultAErreur.True;

			using(CContexteDonnee ctx = new CContexteDonnee(m_nIdSession,true, false))
			{
				CDocumentGED doc = new CDocumentGED(ctx);
				doc.Id = nIdDoc;

				if (doc.ReferenceDoc.TypeReference.Code == new TypeReferenceDocument( TypeReferenceDocument.TypesReference.Fichier ).Code )
				{
					FileStream stream;

					try
					{
						stream = new FileStream(m_strPath + IncNomFichier(doc.ReferenceDoc.NomFichier),FileMode.Create, FileAccess.Write);
					}
					catch
					{
						result.EmpileErreur("Impossible de créer le fichier");
						return result;
					}

					FileStream sourceStream = ((CSourceDocumentStream)src).SourceStream;

					result = CStreamCopieur.CopyStream(sourceStream, stream, 32000);
					if (!result)
					{
						File.Delete(m_strPath + IncNomFichier(doc.ReferenceDoc.NomFichier));
						return result;
					}
				
					string strOld = doc.ReferenceDoc.NomFichier;
					CReferenceDocument tempRef = new CReferenceDocument();
					tempRef.TypeReference = doc.ReferenceDoc.TypeReference;
					tempRef.NomFichier = IncNomFichier(strOld);
					doc.ReferenceDoc = tempRef;
					doc.NumVersion++;

					try
					{
						File.Delete(m_strPath + strOld);
					}
					catch
					{
						tempRef.NomFichier = strOld;
						doc.ReferenceDoc = tempRef;
					}	
				}
				result = ctx.SaveAll(true);
			}

			return result;
		}*/
		/*//-------------------------------------------------------------------
		public CResultAErreur CreateDocument
			(string strLibelle, TypeReferenceDocument tpRef, CSourceDocument src)
		{
			CResultAErreur result = CResultAErreur.True;


			using (CContexteDonnee ctx = new CContexteDonnee(m_nIdSession, true, false) )
			{
				string strPath = DateTime.Now.Year.ToString() + "\\";
				strPath += DateTime.Now.Month.ToString() + "\\";
				strPath += DateTime.Now.Day.ToString();
				string[] strChemins = strPath.Split('\\');
				string strTemp = m_strPath;
				foreach(string strChemin in strChemins)
				{
					if ( !Directory.Exists(strTemp+strChemin) )
						Directory.CreateDirectory(strTemp+strChemin);
					strTemp+=strChemin+"\\";
				}
				string strUnique = CGenerateurStringUnique.GetNewNumero(m_nIdSession) + "_0";
				string strNomFichier = strTemp + strUnique;

				CDocumentGED doc = new CDocumentGED(ctx);
				doc.CreateNewInCurrentContexte();
				doc.Libelle = strLibelle;
				CReferenceDocument tempRef = new CReferenceDocument();
				tempRef = new CReferenceDocument(); 
				tempRef.TypeReference = tpRef;
				tempRef.NomFichier = strPath + "\\" + strUnique + "." + ((CSourceDocumentStream)src).Extension;
				doc.ReferenceDoc = tempRef;
				if (tpRef.Code == new TypeReferenceDocument( TypeReferenceDocument.TypesReference.Fichier ).Code )
				{
					FileStream stream;
					try
					{
						stream = new FileStream(strNomFichier + "." + ((CSourceDocumentStream)src).Extension,FileMode.Create, FileAccess.Write);
					}
					catch
					{
						result.EmpileErreur("Impossible de créer le fichier");
						return result;
					}
					FileStream sourceStream = ((CSourceDocumentStream)src).SourceStream;

					result = CStreamCopieur.CopyStream(sourceStream, stream, 32000);
					if (!result)
						return result;					
				}
				ctx.SaveAll(true);

				result.Data = doc.Id;

			}
			return result;
		}*/
		//-------------------------------------------------------------------
		public override string GetNomTable ()
		{
			return CDocumentGED.c_nomTable;
		}
		//-------------------------------------------------------------------
		public override CResultAErreur VerifieDonnees( CObjetDonnee objet )
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
				CDocumentGED doc = (CDocumentGED)objet;

                ///Stef, le 16/10/2009 : le nom d'un doc n'est plus unique
				/*if (!CObjetDonneeAIdNumerique.IsUnique(doc, CDocumentGED.c_champLibelle, doc.Libelle))
				{
					result.EmpileErreur(I.T("Another document with this label already exist|114"));
					return result;
				}*/
			}
			catch ( Exception e )
			{
				result.EmpileErreur( new CErreurException ( e ) );
			}
			return result;
		}
		//-------------------------------------------------------------------
		public override Type GetTypeObjets()
		{
			return typeof(CDocumentGED);
		}

        //-------------------------------------------------------------------
        public static void Init ( string strPath )
        {
            Init( strPath, CTypeReferenceDocument.Types );
        }
		//-------------------------------------------------------------------
		public static void Init ( string strPath, CTypeReferenceDocument[] stockageAutorisePourLesUtilisateurs )
		{
			if (strPath == "")
				return;
			if ( strPath[strPath.Length-1] != '\\')
				strPath+="\\";
			m_strPath = strPath;
            m_TypesAutorisesPourLesUtilisateurs = stockageAutorisePourLesUtilisateurs;
		}

        //-------------------------------------------------------------------
        private void GetListesPourValidation(
            ref ArrayList listeFichiersToDelete,
            ref ArrayList listeFichiersToValide,
            ref ArrayList listeFichiersANePasSupprimer)
        {
            CSessionClient session = CSessionClient.GetSessionForIdSession(IdSession);
            listeFichiersToDelete = null;
            listeFichiersToValide = null;
            listeFichiersANePasSupprimer = null;
            if (session != null)
            {
                listeFichiersToDelete = session.GetPropriete(c_strListeFichiersToDelete) as ArrayList;
                listeFichiersToValide = session.GetPropriete(c_strListeFichiersToValidate) as ArrayList;
                listeFichiersANePasSupprimer = session.GetPropriete(c_strListeFichierSANePasSupprimer) as ArrayList;
            }
            else
            {
                listeFichiersToDelete = m_globaleListeANePasSupprimer;
                listeFichiersToValide = m_globaleListeFichiersToValide;
                listeFichiersANePasSupprimer = m_globaleListeANePasSupprimer;
                return;
            }
            if (listeFichiersToDelete == null)
            {
                listeFichiersToDelete = new ArrayList();
                session.SetPropriete(c_strListeFichiersToDelete, listeFichiersToDelete);
            }
            if (listeFichiersToValide == null)
            {
                listeFichiersToValide = new ArrayList();
                session.SetPropriete(c_strListeFichiersToValidate, listeFichiersToValide);
            }
            if (listeFichiersANePasSupprimer == null)
            {
                listeFichiersANePasSupprimer = new ArrayList();
                session.SetPropriete(c_strListeFichierSANePasSupprimer, listeFichiersANePasSupprimer);
            }
        }


		
		//-------------------------------------------------------------------
        private const string c_strListeFichiersToDelete = "LST_EDM_TO_DELETE";
        private const string c_strListeFichiersToValidate = "LST_EDM_TO_VALIDATE";
        private const string c_strListeFichierSANePasSupprimer = "LST_EDM_NOT_TO_DELETE";

		private ArrayList m_globaleListeFichiersToDelete = new ArrayList();
		private ArrayList m_globaleListeFichiersToValide = new ArrayList();
        private ArrayList m_globaleListeANePasSupprimer = new ArrayList();
		private OnCommitTransEventHandler m_commitEventHandlerNote = null;
		public override CResultAErreur SaveAll(CContexteSauvegardeObjetsDonnees contexteSauvegarde, System.Data.DataRowState etatsAPrendreEnCompte)
		{
            ArrayList listeFichiersToDelete = null;
            ArrayList listeFichiersToValide = null;
            ArrayList listeFichiersANePasSupprimer = null;
            GetListesPourValidation(ref listeFichiersToDelete,
                ref listeFichiersToValide,
                ref listeFichiersANePasSupprimer);
            
            CResultAErreur result = CResultAErreur.True;
			m_commitEventHandlerNote = new OnCommitTransEventHandler( OnCommitTrans );
			DataTable table = contexteSauvegarde.ContexteDonnee.Tables[GetNomTable()];
            
			if ( table != null )
			{
				foreach ( DataRow row in table.Rows )
				{
					if ( row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added )
					{
                        if (!CDocumentGED.IsControleDocumentsALaSauvegardeDesactive(contexteSauvegarde.ContexteDonnee))
                            //si désactivation des ids auto, on est dans un processus
                            //de synchronisation, donc , pas de contrôle de document
                        {
                            CDocumentGED doc = new CDocumentGED(row);
                            CReferenceDocument newReference = doc.ReferenceDoc;
                            bool bRefAsChange = true;
                            if (row.RowState == DataRowState.Modified)
                            {
                                doc.VersionToReturn = DataRowVersion.Original;
                                CReferenceDocument oldRef = doc.ReferenceDoc;
                                bRefAsChange = false;
                                if ((newReference == null) != (oldRef == null))
                                    bRefAsChange = true;
                                if (oldRef != null && !oldRef.Equals(newReference))
                                {
                                    listeFichiersToDelete.Add(oldRef);
                                    bRefAsChange = true;
                                }
                                doc.VersionToReturn = DataRowVersion.Current;
                            }
                            if (bRefAsChange)
                                result = PreValideDocument(newReference);
                            if (!result)
                                return result;
                            if (bRefAsChange)
                            {
                                listeFichiersToValide.Add(doc.ReferenceDoc);
                            }
                            listeFichiersANePasSupprimer.Add(doc.ReferenceDoc);
                            listeFichiersToDelete.Remove(doc.ReferenceDoc);//Il ne faut pas le supprimer !
                        }
					}
					if ( row.RowState == DataRowState.Deleted )
					{
						CDocumentGED doc = new CDocumentGED ( row );
						doc.VersionToReturn = DataRowVersion.Original;
                        string strRefString = doc.ReferenceString;
                        if ( !listeFichiersToValide.Contains ( doc.ReferenceDoc ) &&
                            !listeFichiersANePasSupprimer.Contains(doc.ReferenceDoc))
						    listeFichiersToDelete.Add ( doc.ReferenceDoc );
						doc.VersionToReturn = DataRowVersion.Current;
					}
				}
			}
			IDatabaseConnexion con = CSc2iDataServer.GetInstance().GetDatabaseConnexion ( IdSession, GetType() );
			if ( con != null )
				con.OnCommitTrans += m_commitEventHandlerNote;
			result = base.SaveAll (contexteSauvegarde, etatsAPrendreEnCompte);
			return result;
		}


		/// //////////////////////////////////////////////////////
		private CResultAErreur OnCommitTrans()
		{
			CResultAErreur result = CResultAErreur.True;
			try
			{
                ArrayList lstToDelete = null;
                ArrayList lstToValidate = null;
                ArrayList lstNotToDelete = null;
                GetListesPourValidation(ref lstToDelete, ref lstToValidate, ref lstNotToDelete);
				foreach ( CReferenceDocument refDoc in lstToDelete )
					DeleteDocument ( refDoc );
				foreach ( CReferenceDocument refDoc in lstToValidate )
					ValideDocument ( refDoc );
                lstToDelete.Clear();
                lstToValidate.Clear();
                lstNotToDelete.Clear();
			}
			catch ( Exception e )
			{
				result.EmpileErreur ( new CErreurException (e ) );
				result.EmpileErreur(I.T("Error while deleting/validating of EDM Documents|30001"));
			}
			finally
			{
				IDatabaseConnexion con = CSc2iDataServer.GetInstance().GetDatabaseConnexion ( IdSession, GetType() );
				if ( con != null )
					con.OnCommitTrans -= m_commitEventHandlerNote;
			}
			return result;
		}

        public CTypeReferenceDocument[] TypesAutorisesPourLesUtilisateurs
        {
            get
            {
                return m_TypesAutorisesPourLesUtilisateurs;
            }
        }

	}
#else

	/// <summary>
	/// Pour que le namespace existe
	/// </summary>
	class DummyClass
	{
	}
#endif
}
