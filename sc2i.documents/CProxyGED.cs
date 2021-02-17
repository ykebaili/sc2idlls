using System;
using System.IO;
using Microsoft.Win32;

using sc2i.common;


namespace sc2i.documents
{
    /// <summary>
    /// Description résumée de CProxyGED.
    /// </summary>
    public class CProxyGED : IDisposable
    {
        private CFichierLocalTemporaire m_fichierLocal = new CFichierLocalTemporaire("dat");
        private string m_strNomFichierLocal = "";
        private CReferenceDocument m_referenceAttachee = null;
        private DateTime m_dateTimeFichierInGed = new DateTime(1900, 1, 1, 12, 45, 23);
        private int m_nIdSession = -1;

        //si la référence attachée est nulle, indique comment le fichier sera geré par TIMOS
        private CTypeReferenceDocument.TypesReference m_typeReferencePourGed = CTypeReferenceDocument.TypesReference.Fichier;

        //---------------------------------------------------------------------
        public CProxyGED(int nIdSession, CReferenceDocument referenceDocumentGed)
        {
            m_referenceAttachee = referenceDocumentGed;
            if (m_referenceAttachee != null)
                m_typeReferencePourGed = m_referenceAttachee.TypeReference.Code;
            m_nIdSession = nIdSession;
        }

        //---------------------------------------------------------------------
        public CProxyGED(int nIdSession, CTypeReferenceDocument.TypesReference typeReferenceAUtiliser)
        {
            m_nIdSession = nIdSession;
            m_typeReferencePourGed = typeReferenceAUtiliser;
        }
        //---------------------------------------------------------------------
        #region Membres de IDisposable
        public void Dispose()
        {
            try
            {
                if (m_fichierLocal != null)
                    m_fichierLocal.Dispose();
                m_fichierLocal = null;
            }
            catch { }
        }
        #endregion
        //---------------------------------------------------------------------
        /// <summary>
        /// Récupère le fichier dans la ged et le copie sur le disque local
        /// YK 02/06/2016 : Ajout d'un paramètre optionnel "Nom du fichier local"
        /// </summary>
        /// <param name="refDoc"></param>
        /// <returns></returns>
        public CResultAErreur CopieFichierEnLocal()
        {
            return CopieFichierEnLocal("");
        }
        
        public CResultAErreur CopieFichierEnLocal(string strNomFichierLocal)
        {
            CResultAErreur result = CResultAErreur.True;
            if (m_referenceAttachee == null)
                return result;

            result = CDocumentGED.GetDocument(m_nIdSession, m_referenceAttachee);
            if (!result)
                return result;

            CSourceDocumentStream source = result.Data as CSourceDocumentStream;
            if (source != null)
            {
                m_fichierLocal.Extension = m_referenceAttachee.GetExtension();

                m_fichierLocal.CreateNewFichier(strNomFichierLocal);
                m_strNomFichierLocal = m_fichierLocal.NomFichier;

                FileStream stream = new System.IO.FileStream(m_fichierLocal.NomFichier, System.IO.FileMode.Create);

                result = CStreamCopieur.CopyStream(source.SourceStream, stream, 32000);

                stream.Close();
                stream.Dispose();
                source.SourceStream.Close();
                source.Dispose();
                m_dateTimeFichierInGed = File.GetLastWriteTime(m_strNomFichierLocal);

                return result;
            }
            CSourceDocumentLienDirect sourceDirecte = result.Data as CSourceDocumentLienDirect;
            if (sourceDirecte != null)
            {
                m_typeReferencePourGed = CTypeReferenceDocument.TypesReference.LienDirect;
                m_strNomFichierLocal = sourceDirecte.NomFichier;
                return result;
            }

            return result;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Attache le proxy à un fichier local. Les fonction d'enregistrement 
        /// dans la ged enverront le fichier local dans la ged
        /// </summary>
        /// <param name="strNomFichierLocal"></param>
        public void AttacheToLocal(string strNomFichierLocal)
        {
            m_strNomFichierLocal = strNomFichierLocal;
        }

        //---------------------------------------------------------------------
        public string NomFichierLocal
        {
            get
            {
                return m_strNomFichierLocal;
            }
        }

        //---------------------------------------------------------------------
        public bool IsFichierRappatrie()
        {
            return m_strNomFichierLocal.Trim() != "";
        }

        //---------------------------------------------------------------------
        public void CreateNewFichier()
        {
            if (TypeReference == CTypeReferenceDocument.TypesReference.Fichier)
            {
                m_fichierLocal.CreateNewFichier();
                m_strNomFichierLocal = m_fichierLocal.NomFichier;
            }
        }
        //---------------------------------------------------------------------
        public CTypeReferenceDocument.TypesReference TypeReference
        {
            get
            {
                if (m_referenceAttachee != null)
                    return m_referenceAttachee.TypeReference.Code;
                else
                    return m_typeReferencePourGed;
            }
        }

        //---------------------------------------------------------------------
        public bool HasChange()
        {
            if (!IsFichierRappatrie())
                return false;
            if (!File.Exists(m_strNomFichierLocal))
                return false;
            if (m_dateTimeFichierInGed == File.GetLastWriteTime(m_strNomFichierLocal))
                return false;
            return true;
        }

        //---------------------------------------------------------------------
        public CResultAErreur UpdateGed()
        {
            return UpdateGed(true);
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Le data du result contient la nouvelle référence au document
        /// </summary>
        /// <returns></returns>
        public CResultAErreur UpdateGed(bool bIncrementeVersionFichier)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                //Regarde si le fichier a été modifié
                try
                {
                    DateTime dt = File.GetLastWriteTime(m_strNomFichierLocal);
                    if (dt == m_dateTimeFichierInGed)
                        return result;
                }
                catch (Exception e)
                {
                    result.EmpileErreur(new CErreurException(e));
                    result.EmpileErreur(I.T("EDM updating error|112"));
                    return result;
                }
                CSourceDocument sourceDoc = null;
                switch (TypeReference)
                {
                    case CTypeReferenceDocument.TypesReference.Fichier:
                        FileStream fs = new FileStream(m_strNomFichierLocal, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        int nIndex = m_strNomFichierLocal.LastIndexOf(".");
                        string strExtension = "";
                        if (nIndex > 0)
                            strExtension = m_strNomFichierLocal.Substring(nIndex + 1);
                        else
                            strExtension = "dat";
                        sourceDoc = new CSourceDocumentStream(fs, strExtension);
                        break;
                    case CTypeReferenceDocument.TypesReference.LienDirect:
                        sourceDoc = new CSourceDocumentLienDirect(m_strNomFichierLocal);
                        break;
                }
                if (sourceDoc == null)
                {
                    result.EmpileErreur(I.T("Error in document transfering to EDM |113"));
                    return result;
                }

                result = CDocumentGED.SaveDocument(m_nIdSession, sourceDoc,
                            new CTypeReferenceDocument(TypeReference),
                            m_referenceAttachee,
                            bIncrementeVersionFichier);
                sourceDoc.Dispose();
                if (result)
                {
                    m_referenceAttachee = (CReferenceDocument)result.Data;
                    m_dateTimeFichierInGed = File.GetLastWriteTime(m_strNomFichierLocal);
                }
            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
                result.EmpileErreur(I.T("Error in document transfering to EDM|113"));
            }
            return result;
        }

        //---------------------------------------------------------------------
        public CReferenceDocument ReferenceAttachee
        {
            get
            {
                return m_referenceAttachee;
            }
        }

        public bool DeleteFichierLocal()
        {
            if (m_fichierLocal != null)
            {
                return m_fichierLocal.FreeFichier();
            }
            return true;
        }

    }
}