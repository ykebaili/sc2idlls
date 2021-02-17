using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.common;
using System.Runtime.Remoting;
using System.Security.Principal;
using sc2i.multitiers.client;
using sc2i.multitiers.server;
using sc2i.data.serveur;
using System.Reflection;
using sc2i.data;
using sc2i.process.serveur;

namespace CTesteurBaseAccess
{
    public static class CInitialiseur
    {
        public const string c_mainDataSource = "MAIN_DATA";
        public const string c_droitsDataSource = "USER_RIGHTS_DEF";

        /// /////////////////////////////////////////////////////////
        [STAThread]
        public static CResultAErreur Init(
            string strEventJournalName,
            string strEventJournalTexte,
            IIndicateurProgression indicateurProgress)
        {
            CResultAErreur result = CResultAErreur.True;
            try
            {
                int nValeurIndicateur = 0;
                C2iEventLog.Init(strEventJournalName, strEventJournalTexte, NiveauBavardage.VraiPiplette);
                CConteneurIndicateurProgression indicateur = CConteneurIndicateurProgression.GetConteneur(indicateurProgress);
                if (indicateur != null)
                {
                    indicateur.PushSegment(0, 13);
                }


                CTraducteur.ReadFichier("");

                #region Configuration du remoting

                indicateur.SetValue(nValeurIndicateur++);

                AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

                C2iSponsor.EnableSecurite();
                #endregion

                #region Configuration de l'allocateur d'objets
                Dictionary<string, MarshalByRefObject> dicLocalSingleton = new Dictionary<string, MarshalByRefObject>();
                dicLocalSingleton["sc2i.multitiers.client.IGestionnaireSessions"] = new CGestionnaireSessionSagexProSolo();
                C2iFactory.InitEnLocal(new C2iObjetServeurFactory(), dicLocalSingleton);

                indicateur.SetValue(nValeurIndicateur++);

                CSessionClient session = CSessionClient.CreateInstance();
                result = session.OpenSession(new CAuthentificationSessionServer(), "SagexProSolo", ETypeApplicationCliente.Service);
                if (!result)
                {
                    result.EmpileErreur(I.T("Opening session error|30010"));
                    return result;
                }


                CSc2iDataServer.AddDefinitionConnexion(
                    new CDefinitionConnexionDataSource(
                    c_droitsDataSource,
                    typeof(CGestionnaireDroitsUtilisateurs),
                    ""));

                CSc2iDataServer.SetIdConnexionForClasse(typeof(CDroitUtilisateurServeur), c_droitsDataSource);
                #endregion

                #region Configuration de la base de données
                indicateur.SetValue(nValeurIndicateur++);

                Type typeConnexion = typeof(CAccess97DatabaseConnexion);

                //Récuperation du type de connection

                CSc2iDataServer.Init(
                    new CDefinitionConnexionDataSource(
                    c_mainDataSource,
                    typeConnexion,
                    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"C:\\Documents and Settings\\GENERIC\\Mes documents\\BASEVIDE2000.MDB\"",
                    ""));
                #endregion

                #region Ajout des références DLL
                indicateur.SetValue(nValeurIndicateur++);
                AppDomain.CurrentDomain.Load("sc2i.data.client");
                AppDomain.CurrentDomain.Load("sc2i.data.serveur");
                AppDomain.CurrentDomain.Load("sc2i.data.dynamic");
                AppDomain.CurrentDomain.Load("sc2i.data.dynamic.loader");
                AppDomain.CurrentDomain.Load("sc2i.process");
                AppDomain.CurrentDomain.Load("sc2i.process.serveur");
                AppDomain.CurrentDomain.Load("sc2i.expression");
                AppDomain.CurrentDomain.Load("sc2i.Formulaire");
                AppDomain.CurrentDomain.Load("futurocom.vectordb.data");
                AppDomain.CurrentDomain.Load("futurocom.vectordb.data.server");
                AppDomain.CurrentDomain.Load("futurocomapp.data");
                AppDomain.CurrentDomain.Load("futurocomapp.data.server");
                AppDomain.CurrentDomain.Load("sagexpro.data");
                AppDomain.CurrentDomain.Load("sagexpro.data.serveur");

                
                foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                    CContexteDonnee.AddAssembly(ass);
                #endregion

                #region Teste la connexion
                indicateur.SetValue(nValeurIndicateur++);
                IDatabaseConnexion cnx = CSc2iDataServer.GetInstance().GetDatabaseConnexion(session.IdSession, c_mainDataSource);

                //Attend la connexion pendant au max 5 minutes pour que ça démarre
                DateTime dtStartAttente = DateTime.Now;
                TimeSpan delaiAttente = DateTime.Now - dtStartAttente;
                result = cnx.IsConnexionValide();
                while (!result && delaiAttente.TotalSeconds < 5 * 60)
                {
                    C2iEventLog.WriteErreur(result.MessageErreur);
                    delaiAttente = DateTime.Now - dtStartAttente;
                    C2iEventLog.WriteErreur(I.T("Connection not availiable(@1)|30014", delaiAttente.TotalSeconds.ToString() + " s)") +
                        Environment.NewLine);
                    string messageErreur = I.T("The connection with the database could not have been established. Verify the connection string and check if the database has been started|30015");
                    C2iEventLog.WriteErreur(messageErreur);

                    result.EmpileErreur(messageErreur);
                    return result;
                }

                if (typeof(CSqlDatabaseConnexion).IsAssignableFrom(typeConnexion))
                    cnx.RunStatement("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                
                #endregion


                //Initialisation des classes autoexecutables
                CAutoexecuteurClasses.RunAutoexecs();

                //Initialisation du serveur de documents GED
                //Initialise les restrictions standards

                #region Vérifie que les champs des tables font bien moins de 25 cars
                indicateur.SetValue(nValeurIndicateur++);

                DateTime dt = DateTime.Now;

                foreach (Type tp in CContexteDonnee.GetAllTypes())
                {
                    CStructureTable structure = CStructureTable.GetStructure(tp);
                    if (structure.NomTable.Length > 25)
                        result.EmpileErreur("Table " + structure.NomTable + " (" + tp.ToString() + ")=" + structure.NomTable.Length + "cars");
                    if (structure.NomTable.ToUpper() != structure.NomTable)
                        result.EmpileErreur(I.T("Table name @1 must be uppercase|30018", structure.NomTable));
                    foreach (CInfoChampTable champ in structure.Champs)
                    {
                        if (champ.NomChamp.Length > 25)
                            result.EmpileErreur("Table " + structure.NomTable + " (" + tp.ToString() + ")\t champ " + champ.NomChamp + "=" + champ.NomChamp.Length + "cars");
                        if (champ.NomChamp.ToUpper() != champ.NomChamp)
                            result.EmpileErreur(I.T("The name of the field '@1' of the field '@2' must be uppercase|30019", champ.NomChamp, structure.NomTable));
                    }
                }
                TimeSpan sp = DateTime.Now - dt;
                Console.WriteLine(I.T("Table name verification |30020") + sp.TotalMilliseconds);
                if (!result)
                    return result;
                #endregion

                #region Mise à jour de la structure de la base
                indicateur.SetValue(nValeurIndicateur++);

                CUpdaterDataBase updaterDataBase = CUpdaterDataBase.GetInstance(cnx, new CSagexproStructureBase());

                //S'assure que la gestion des éléments est initialisé dans les licences
                //CLicenceCheckElementNb.GetInstance();

                result = updaterDataBase.UpdateStructureBase(indicateur);
                if (!result)
                    return result;


                #endregion


                //Restrictions sur applications


                //Initialisation du serveur de documents GED
                
                //Initialisation de la base d'utilisateurs AD
                //CAdBase.Init(CfuturocomappServeurRegistre.RacineAd,"","");

                //Initialise les fournisseurs de services
                //CSessionClientSurServeur.RegisterFournisseur( new CFournisseurFiltresForSynchronisation() );

                CGestionnaireEvenements.Init();

               

                
                if (!result)
                    return result;

                //CGestionnaireObjetsAttachesASession.OnAttacheObjet += new LinkObjectEventHandler(CGestionnaireObjetsAttachesASession_OnAttacheObjet);


            }
            catch (Exception e)
            {
                result.EmpileErreur(new CErreurException(e));
            }
            return result;
        }
    }
}
