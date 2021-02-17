using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.expression;
using sc2i.data.dynamic.NommageEntite;
using sc2i.multitiers.client;
using sc2i.data;
using sc2i.common;

namespace sc2i.data.dynamic.Expressions
{
    [AutoExec("Autoexec")]
    [Serializable]
    public class C2iExpressionEntiteNommee : C2iExpressionConstanteDynamique
    {
        private CDbKey m_keyNommageEntite = null;
        private string m_strNom = "";
        private Type m_typeNommage = null;
        /// <summary>
        /// Fournit à l'analyseur les CNommageEntite
        /// </summary>
        [AutoExec("Autoexec")]
        public class CFournisseurConstantesEntitesNommees : IFournisseurConstantesDynamiques
        {
            private static CRecepteurNotification m_recepteurAjout = null;
            private static CRecepteurNotification m_recepteurModification = null;
            private static List<C2iExpressionEntiteNommee> m_listeExpression = null;

            public static void Autoexec()
            {
                CAnalyseurSyntaxiqueExpression.RegisterFournisseurExpressionsDynamiques(new CFournisseurConstantesEntitesNommees());
            }

            private void AssureRecepteurs()
            {
                if (m_recepteurAjout == null)
                {
                    m_recepteurAjout = new CRecepteurNotification(CContexteDonneeSysteme.GetInstance().IdSession, typeof(CDonneeNotificationAjoutEnregistrement));
                    m_recepteurAjout.OnReceiveNotification += new NotificationEventHandler(m_recepteurAjout_OnReceiveNotification);
                }
                if (m_recepteurModification == null)
                {
                    m_recepteurModification = new CRecepteurNotification(CContexteDonneeSysteme.GetInstance().IdSession, typeof(CDonneeNotificationModificationContexteDonnee));
                    m_recepteurModification.OnReceiveNotification += new NotificationEventHandler(m_recepteurModification_OnReceiveNotification);
                }
            }

            //-----------------------------------------------------------------------------
            private static void m_recepteurModification_OnReceiveNotification(IDonneeNotification donnee)
            {
                lock (typeof(CFournisseurConstantesEntitesNommees))
                {
                    CDonneeNotificationModificationContexteDonnee modif = donnee as CDonneeNotificationModificationContexteDonnee;
                    if (modif == null)
                        return;
                    foreach (sc2i.data.CDonneeNotificationModificationContexteDonnee.CInfoEnregistrementModifie info in modif.ListeModifications)
                    {
                        if (info.NomTable == CNommageEntite.c_nomTable)
                        {
                            m_listeExpression = null;
                            break;
                        }
                    }
                }
            }

            //-----------------------------------------------------------------------------
            private static void m_recepteurAjout_OnReceiveNotification(IDonneeNotification donnee)
            {
                lock ( typeof(CFournisseurConstantesEntitesNommees ))
                {
                    CDonneeNotificationAjoutEnregistrement ajout = donnee as CDonneeNotificationAjoutEnregistrement;
                    if (ajout.NomTable == CNommageEntite.c_nomTable)
                    {
                        m_listeExpression = null;
                    }
                }
            }

           //-----------------------------------------------------------------------------
           private List<C2iExpressionEntiteNommee> GetListe()
           {
               lock ( typeof(CFournisseurConstantesEntitesNommees) )
               {
                   AssureRecepteurs();
                   if ( m_listeExpression == null )
                   {
                       CListeObjetsDonnees lst = new CListeObjetsDonnees ( CContexteDonneeSysteme.GetInstance(), typeof(CNommageEntite ) );
                       m_listeExpression = new List<C2iExpressionEntiteNommee>();
                       foreach ( CNommageEntite  nommage in lst.ToArrayList()  )
                           m_listeExpression.Add ( new C2iExpressionEntiteNommee ( nommage ) );
                   }
                   return m_listeExpression;
               }
           }

            //-----------------------------------------------------------------------------
            public C2iExpressionConstanteDynamique[] GetConstantes()
            {
                return GetListe().ToArray();
            }

            //-----------------------------------------------------------------------------
            public C2iExpressionConstanteDynamique GetConstante(string strConstante)
            {
                return GetListe().FirstOrDefault ( e=>e.m_strNom.ToUpper() == strConstante.ToUpper() );
            }

        }

        //-----------------------------------------------------------------------------
        public C2iExpressionEntiteNommee()
            :base()
        {
        }

        public static void Autoexec()
        {
            CAllocateur2iExpression.Register2iExpression(new C2iExpressionEntiteNommee().IdExpression,
                typeof(C2iExpressionEntiteNommee));
        }

        //-----------------------------------------------------------------------------
        public C2iExpressionEntiteNommee ( CNommageEntite nommage )
        {
            m_keyNommageEntite = nommage.DbKey;
            m_typeNommage = nommage.TypeEntite;
            m_strNom = nommage.NomFort;
        }

        //-----------------------------------------------------------------------------
        [ExternalReferencedEntityDbKey(typeof(CNommageEntite))]
        public CDbKey KeyNommage
        {
            get
            {
                return m_keyNommageEntite;
            }
        }


        //-----------------------------------------------------------------------------
        public override string ConstanteName
        {
            get { return m_strNom; }
        }

        //-----------------------------------------------------------------------------
        protected override CInfo2iExpression GetInfosSansCache()
        {
            return GetInfos();
        }

        //-----------------------------------------------------------------------------
        public override CInfo2iExpression GetInfos()
        {
            CInfo2iExpression info = new CInfo2iExpression(
                0,
                "NAMED_ENTITY",
                m_strNom,
                new CTypeResultatExpression(m_typeNommage, false),
                "",
                I.T("Entities|20050"));
            return info;
        }

        //-----------------------------------------------------------------------------
        public override CResultAErreur MyEval(CContexteEvaluationExpression ctx, object[] valeursParametres)
        {
            CResultAErreur result = CResultAErreur.True;
            CContexteDonnee ctxDonnee = null;
            if (ctx.ObjetSource is IObjetAContexteDonnee)
                ctxDonnee = ((IObjetAContexteDonnee)ctx.ObjetSource).ContexteDonnee;
            if ( ctxDonnee == null )
                ctxDonnee = ctx.GetObjetAttache(typeof(CContexteDonnee)) as CContexteDonnee;
            if (ctxDonnee == null)
                ctxDonnee = CContexteDonneeSysteme.GetInstance();
            CNommageEntite nommage = new CNommageEntite(ctxDonnee);
            //TESTDBKEYOK
            if (nommage.ReadIfExists(m_keyNommageEntite) )//&& nommage.NomFort == m_strNom)
                result.Data = nommage.GetObjetNomme();
            else if (nommage.ReadIfExists(new CFiltreData(CNommageEntite.c_champNomFort + "=@1",
                m_strNom)))
                result.Data = nommage.GetObjetNomme();
            return result;
        }

        //-----------------------------------------------------------------------------
        private int GetNumVersion()
        {
            return 1;
            //1 : passage en DbKey
        }

        //-----------------------------------------------------------------------------
        public override CResultAErreur Serialize(C2iSerializer serializer)
        {
            int nVersion = GetNumVersion();
            CResultAErreur result = serializer.TraiteVersion(ref nVersion);
            if (result)
                result = base.Serialize(serializer);
            if (!result)
                return result;
            //TESTDBKEYOK
            if (nVersion < 1)
            {
                serializer.ReadDbKeyFromOldId(ref m_keyNommageEntite, typeof(CNommageEntite));
            }
            else
                serializer.TraiteDbKey(ref m_keyNommageEntite);
            serializer.TraiteString(ref m_strNom);
            serializer.TraiteType(ref m_typeNommage);
            return result;
        }
    }
}
