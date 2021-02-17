using System;
using System.Data;
using System.Collections;
using System.Linq;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.recherche;
using sc2i.data.dynamic;
using System.Collections.Generic;
using sc2i.multitiers.client;
using System.Text;
using sc2i.process.workflow.blocs;
using sc2i.process.workflow.gels;

namespace sc2i.process.workflow
{
    /// <summary>
    /// Représente une étape d'un workflow qui est ou a été exécutée
    /// </summary>
    /// <remarks>
    /// Chaque fois qu'une étape de workflow est exécutée, les données de celle-ci
    /// sont gérées par un "Workflow step".
    /// <P>Il ne peut exister qu'une seul étape d'un type d'étape donné pour un workflow donné. Si le workflow provoque des 
    /// retours sur une étape, l'éxecution précédente est historisée, et l'étape est relancée.</P>
    /// <P>Une étape est démarrée automatiquement lorsqu'elle est dans l'état <B>En attente</B> et qu'une date de début lui est
    /// affectée.</P>
    /// </remarks>
    [Table(CEtapeWorkflow.c_nomTable, CEtapeWorkflow.c_champId, true)]
    [FullTableSync]
    [ObjetServeurURI("CEtapeWorkflowServeur")]
    [DynamicClass("Workflow step")]
    [AutoExec("Autoexec")]
    [Evenement(CEtapeWorkflow.c_codeEvenementOnRunStep,
        "On Run Step",
        "Is launched when the Step is running")]
    public class CEtapeWorkflow :
        CElementAChamp,
        IElementAEvenementsDefinis,
        IObjetDonneeAutoReferenceNavigable,
        IElementGelable
    {
        public const string c_roleChampCustom = "RUN_WKF_STEP";

        public const string c_nomTable = "WORKFLOW_STEP";
        public const string c_champId = "WKFSTP_ID";
        public const string c_champDateDebut = "WKFSTP_START_DATE";
        public const string c_champDateFin = "WKFSTP_END_DATE";
        public const string c_champLibelle = "WKFSTP_LABEL";
        public const string c_champKeyDemarréePar = "WKFSTP_STARTUSR_KEY";
        public const string c_champKeyTerminéePar = "WKFSTP_ENDUSR_KEY";
        public const string c_champEtat = "WKFSTP_STATE";
        public const string c_champLastError = "WKFSTP_LAST_ERROR";
        public const string c_champCodesRetour = "WKFSTP_RETURN_CODES";
        public const string c_champIdWorkflowLancé = "WKFSTP_CALLED_WORKFLOW";
        public const string c_champIdEtapeAppelante = "WKFSTP_CALLING_STEP";
        public const string c_champRunGeneration = "WKFSTP_RUN_GENERATION";

        public const string c_champAffectations = "WKFSTP_ASSIGN";
        public const string c_codeEvenementOnRunStep = "ONRUNSTEP_EVENT";



#if PDA
		/// ///////////////////////////////////////////////////////
		public CWorkflowStep(  )
			:base()
		{
		}
#endif
        /// <summary>
        /// //////////////////////////////////////////////////
        /// </summary>
        /// <param name="ctx"></param>
        public CEtapeWorkflow(CContexteDonnee ctx)
            : base(ctx)
        {
        }

        /// //////////////////////////////////////////////////
        public CEtapeWorkflow(DataRow row)
            : base(row)
        {
        }

        /// //////////////////////////////////////////////////
        public static void Autoexec()
        {
            CRoleChampCustom.RegisterRole(c_roleChampCustom, I.T("Workflow step|20067"),
                typeof(CEtapeWorkflow),
                typeof(CTypeEtapeWorkflow));
        }

        /// //////////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
            EtatCode = (int)EEtatEtapeWorkflow.EnAttente;
            RunGeneration = 0;
        }

        /// //////////////////////////////////////////////////
        public override string DescriptionElement
        {
            get
            {
                return I.T("Worfklow step @1|20062",
                    Libelle);
            }
        }

        /// //////////////////////////////////////////////////
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champId };
        }

        /// //////////////////////////////////////////////////
        //-----------------------------------------------------------
        /// <summary>
        /// Date de démarrage de l'étape
        /// </summary>
        [TableFieldProperty(c_champDateDebut, NullAutorise = true)]
        [DynamicField("Start date")]
        public DateTime? DateDebut
        {
            get
            {
                if (Row[c_champDateDebut] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDateDebut];
            }
            set
            {
                if (value == null)
                    Row[c_champDateDebut] = DBNull.Value;
                else
                    Row[c_champDateDebut] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Pour compatiblité avec le passage de CDbKey
        /// La propriété existait avant que toutes les entités
        /// aient un id universel et s'appelait UniversalId
        /// </summary>
        public string UniversalId
        {
            get
            {
                return IdUniversel;
            }
        }



        //-----------------------------------------------------------
        /// <summary>
        /// Date de fin de l'étape
        /// </summary>
        /// <remarks>
        /// Chaque fois qu'une date de fin est validée pour une étape, une entrée
        /// d'<see cref="CEtapeWorkflowHistorique">historique</see> est créée
        /// </remarks>
        [TableFieldProperty(c_champDateFin, NullAutorise = true)]
        [DynamicField("End date")]
        public DateTime? DateFin
        {
            get
            {
                if (Row[c_champDateFin] == DBNull.Value)
                    return null;
                return (DateTime?)Row[c_champDateFin];
            }
            set
            {
                if (value == null)
                    Row[c_champDateFin] = DBNull.Value;
                else
                    Row[c_champDateFin] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Libellé de l'étape
        /// </summary>
        [TableFieldProperty(c_champLibelle, 1024)]
        [DynamicField("Label")]
        public string Libelle
        {
            get
            {
                string strVal = (string)Row[c_champLibelle];
                if (strVal.Length == 0 && TypeEtape != null)
                    return TypeEtape.Libelle;
                return strVal;
            }
            set
            {
                Row[c_champLibelle] = value;
            }
        }





        //-------------------------------------------------------------------
        /// <summary>
        /// Workflow en cours d'exécution ayant exécuté cette étape
        /// </summary>
        [Relation(
            CWorkflow.c_nomTable,
            CWorkflow.c_champId,
            CWorkflow.c_champId,
            true,
            false,
            true)]
        [DynamicField("Workflow")]
        public CWorkflow Workflow
        {
            get
            {
                return (CWorkflow)GetParent(CWorkflow.c_champId, typeof(CWorkflow));
            }
            set
            {
                SetParent(CWorkflow.c_champId, value);
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Indique la génération de démarrage de workflow
        /// correspondant à l'execution en cours ou la 
        /// dernière execution de cette étape
        /// </summary>
        [DynamicField("Run generation")]
        [TableFieldProperty(c_champRunGeneration)]
        public int RunGeneration
        {
            get
            {
                return (int)Row[c_champRunGeneration];
            }
            set
            {
                Row[c_champRunGeneration] = value;
            }
        }


        //-------------------------------------------------------------------
        /// <summary>
        /// Si l'étape est une étape lançant un workflow, cette propriété représente 
        /// le workflow qui a été lancé par l'étape.
        /// </summary>
        [Relation(
            CWorkflow.c_nomTable,
            CWorkflow.c_champId,
            c_champIdWorkflowLancé,
            false,
            false,
            false)]
        [DynamicField("Launched workflow")]
        public CWorkflow WorkflowLancé
        {
            get
            {
                return (CWorkflow)GetParent(c_champIdWorkflowLancé, typeof(CWorkflow));
            }
            set
            {
                SetParent(c_champIdWorkflowLancé, value);
            }
        }





        //-------------------------------------------------------------------
        /// <summary>
        /// Indique le modèle d'étape auquel correspond cette exécution
        /// </summary>
        [Relation(
            CTypeEtapeWorkflow.c_nomTable,
            CTypeEtapeWorkflow.c_champId,
            CTypeEtapeWorkflow.c_champId,
            true,
            false,
            true)]
        [DynamicField("Workflow step type")]
        public CTypeEtapeWorkflow TypeEtape
        {
            get
            {
                return (CTypeEtapeWorkflow)GetParent(CTypeEtapeWorkflow.c_champId, typeof(CTypeEtapeWorkflow));
            }
            set
            {
                SetParent(CTypeEtapeWorkflow.c_champId, value);
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Code des entités affectés à la réalisation de cette étape.
        /// </summary>
        [TableFieldProperty(c_champAffectations, 1024)]
        [DynamicField("Assignments code")]
        public string CodeAffectations
        {
            get
            {
                return (string)Row[c_champAffectations];
            }
            set
            {
                Row[c_champAffectations] = value;
            }
        }

        //-----------------------------------------------------------
        public CAffectationsEtapeWorkflow Affectations
        {
            get
            {
                return CAffectationsEtapeWorkflow.FromCode(CodeAffectations);
            }
            set
            {
                if (value != null)
                    CodeAffectations = value.GetCodeString();
                else
                    CodeAffectations = "";
            }
        }

        //-----------------------------------------------------------
        [DynamicMethod("Add an assignment to this step", "Element to affect")]
        public void AddAssignment(IAffectableAEtape affectable)
        {
            if (affectable == null)
                return;
            CAffectationsEtapeWorkflow aff = Affectations;
            aff.AddAffectable(affectable);
            Affectations = aff;
        }

        //-----------------------------------------------------------
        [DynamicMethod("Remove an assignment to this step", "Element to remove")]
        public void RemoveAssignment(IAffectableAEtape affectable)
        {
            if (affectable == null)
                return;
            CAffectationsEtapeWorkflow aff = Affectations;
            aff.RemoveAffectable(affectable);
            Affectations = aff;
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Liste toutes les affectations de cette étape
        /// </summary>
        [DynamicField("Assignments")]
        public IAffectableAEtape[] Assignments
        {
            get
            {
                return Affectations.GetAffectables(ContexteDonnee).ToArray();
            }
        }

        //-----------------------------------------------------------
        [DynamicMethod("Clear all assignments")]
        public void ClearAssignments()
        {
            CodeAffectations = "";
        }



        //-------------------------------------------------------------------
        public override CRoleChampCustom RoleChampCustomAssocie
        {
            get { return CRoleChampCustom.GetRole(c_roleChampCustom); }
        }

        //-------------------------------------------------------------------
        public override CRelationElementAChamp_ChampCustom GetNewRelationToChamp()
        {
            return new CRelationEtapeWorkflow_ChampCustom(ContexteDonnee);
        }

        //-------------------------------------------------------------------
        public override IDefinisseurChampCustom[] DefinisseursDeChamps
        {
            get
            {
                if (TypeEtape == null)
                    return new IDefinisseurChampCustom[0];
                else
                    return new IDefinisseurChampCustom[] { TypeEtape };
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Liens vers les valeurs de champs personnalisés
        /// </summary>
        [RelationFille(typeof(CRelationEtapeWorkflow_ChampCustom), "ElementAChamps")]
        [DynamicChilds("Custom fields relations", typeof(CRelationEtapeWorkflow_ChampCustom))]
        public override CListeObjetsDonnees RelationsChampsCustom
        {
            get { return GetDependancesListe(CRelationEtapeWorkflow_ChampCustom.c_nomTable, c_champId); }
        }


        //---------------------------------------------
        /// <summary>
        /// Liste des entrées historique de cette étape.
        /// </summary>
        [RelationFille(typeof(CEtapeWorkflowHistorique), "Etape")]
        [DynamicChilds("History", typeof(CEtapeWorkflowHistorique))]
        public CListeObjetsDonnees Historiques
        {
            get
            {
                return GetDependancesListe(CEtapeWorkflowHistorique.c_nomTable, c_champId);
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Indique l'id de l'utilisateur ayant déclenché le démarrage de l'étape
        /// <BR></BR>
        /// Une valeur nulle indique que l'id du créateur de l'étape est inconnu
        /// </summary>
        [TableFieldProperty(c_champKeyDemarréePar, 64)]
        [ReplaceField("IdDémarreur", "Started by Id")]
        [DynamicField("Started by Key string")]
        public string KeyDémarreurString
        {
            get
            {
                //TESTDBKEYOK
                return Row.Get<string>(c_champKeyDemarréePar);
            }
            set
            {
                //TESTDBKEYOK
                Row[c_champKeyDemarréePar, true] = value;
            }
        }

        //-----------------------------------------------------------
        [DynamicField("Start by key")]
        public CDbKey KeyDémarreur
        {
            get
            {
                //TESTDBKEYOK
                return CDbKey.CreateFromStringValue(KeyDémarreurString);
            }
            set
            {
                //TESTDBKEYOK
                if (value != null)
                    KeyDémarreurString = value.StringValue;
                else
                    KeyDémarreurString = "";
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Indique l'id de l'utilisateur ayant déclenché la fin de l'étape
        /// <BR></BR>
        /// Une valeur nulle indique que l'id du déclencheur de fin de l'étape est inconnu
        /// </summary>
        [TableFieldProperty(c_champKeyTerminéePar, 64)]
        [ReplaceField("IdTermineur", "Ended by ID")]
        [DynamicField("Ended by Key string")]
        public String KeyTermineurString
        {
            get
            {
                //TESTDBKEYOK
                return Row.Get<string>(c_champKeyTerminéePar);
            }
            set
            {
                //TESTDBKEYOK
                Row[c_champKeyTerminéePar, true] = value;
            }
        }

        //-----------------------------------------------------------
        [DynamicField("Ended by key")]
        public CDbKey KeyTermineur
        {
            get
            {
                //TESTDBKEYOK
                return CDbKey.CreateFromStringValue(KeyTermineurString);
            }
            set
            {
                //TESTDBKEYOK
                if (value != null)
                    KeyTermineurString = value.StringValue;
                else
                    KeyTermineurString = "";
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Etat de l'étape
        /// </summary>
        /// <remarks>
        /// Les valeurs possibles sont :
        /// <LI>0 : En attente (l'étape est en attente de démarrage. Elle démarrera dès qu'une date de début lui sera affectée</LI>
        /// <LI>1 : à démarrer</LI>
        /// <LI>2 : Démarrée</LI>
        /// <LI>3 : Terminée</LI>
        /// <LI>4 : Erreur</LI>
        /// <LI>5 : Annulée</LI>
        /// </remarks>
        [TableFieldProperty(c_champEtat)]
        [DynamicField("State code")]
        public int EtatCode
        {
            get
            {
                return (int)Row[c_champEtat];
            }
            set
            {
                Row[c_champEtat] = value;
            }
        }

        /// <summary>
        /// Etat (avec code et libellé) de l'étape
        /// </summary>
        [DynamicField("State")]
        public CEtatEtapeWorkflow Etat
        {
            get
            {
                return new CEtatEtapeWorkflow((EEtatEtapeWorkflow)EtatCode);
            }
            set
            {
                if (value != null)
                    EtatCode = value.CodeInt;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Dernier message d'erreur rencontré lors d'une execution de cette étape, qui n'a pas abouti.
        /// </summary>
        [TableFieldProperty(c_champLastError, 4000, IsLongString = true)]
        [DynamicField("Last error text")]
        public string LastError
        {
            get
            {
                return (string)Row[c_champLastError];
            }
            set
            {
                Row[c_champLastError] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Codes retournés par cette exécution de l'étape. Les codes sont retournés
        /// sous forme d'une chaîne de caractères.
        /// </summary>
        /// <remarks>
        /// En fin d'étape, suivant son type, l'étape se voit affecter des
        /// codes retour. Ces codes activent ou non les liens sortant de cette étape.
        /// <BR></BR>
        /// Les codes de retour sont toujours encadrés par des ~
        /// 
        /// </remarks>
        [TableFieldProperty(c_champCodesRetour, 2000)]
        [DynamicField("Return codes string")]
        public string CodesRetourString
        {
            get
            {
                return (string)Row[c_champCodesRetour];
            }
            set
            {
                string strTmp = value;
                if (strTmp.Length > 0 && !strTmp.StartsWith("~"))
                    strTmp = "~" + strTmp;
                if (strTmp.Length > 0 && !strTmp.EndsWith("~"))
                    strTmp += "~";
                Row[c_champCodesRetour] = strTmp;
            }
        }

        //--------------------------------------------------------------
        internal static string[] CodesRetourFromString(string strCodes)
        {
            List<string> lstCodes = new List<string>();
            foreach (string strCode in strCodes.Split('~'))
            {
                if (strCode.Trim().Length > 0)
                    lstCodes.Add(strCode);
            }
            return lstCodes.ToArray();
        }

        //--------------------------------------------------------------
        internal static string StringFromCodesRetour(string[] strCodes)
        {
            StringBuilder bl = new StringBuilder();
            if (strCodes != null)
            {
                foreach (string strCode in strCodes)
                {
                    if (strCode.Trim().Length > 0)
                    {
                        bl.Append("~");
                        bl.Append(strCode);
                    }
                }
                if (bl.Length > 0)
                    bl.Append('~');
            }
            return bl.ToString();
        }

        /// <summary>
        /// Liste des codes retour
        /// </summary>
        [DynamicField("Return codes array")]
        public string[] CodesRetour
        {
            get
            {
                return CodesRetourFromString(CodesRetourString);
            }
            set
            {
                CodesRetourString = StringFromCodesRetour(value);
            }

        }



        //---------------------------------------------------------
        /// <summary>
        /// A appeller lorsqu'un étape contenant un sous workflow
        /// démarre ou redémarre car le sous workflow a redémarré.
        /// </summary>
        /// <returns></returns>
        public CResultAErreur OnDemarrageDepuisSousWorkflow()
        {
            CResultAErreur result = CResultAErreur.True;
            if (Etat.Code != EEtatEtapeWorkflow.ADemarrer && Etat.Code != EEtatEtapeWorkflow.Démarrée)
            {
                result = InternalSetInfosDemarrageInCurrentContext();
                EtatCode = (int)EEtatEtapeWorkflow.Démarrée;
                if (TypeEtape != null)
                {
                    CBlocWorkflowWorkflow bloc = TypeEtape.Bloc as CBlocWorkflowWorkflow;
                    if (bloc != null)
                        result = bloc.OnBlocRedemarréParUneEtapeDuSousWorkflow(this);
                }
            }

            return result;
        }


        //---------------------------------------------------------
        /// <summary>
        /// Stocke les différentes informations pour le démarrage d'une étape
        /// (date de début, date de fin à null, idDémarreur et IdTermineur à null)
        /// <BR></BR>Cette méthode ne doit pas être utilisée en dehors d'un appel par l'objet serveur
        /// </summary>
        /// <returns></returns>
        public CResultAErreur InternalSetInfosDemarrageInCurrentContext()
        {
            if (DateDebut != null && DateFin != null)
            {
                //Historise l'étape
                CEtapeWorkflowHistorique h = new CEtapeWorkflowHistorique(ContexteDonnee);
                h.CreateNewInCurrentContexte();
                h.DateDebut = DateDebut.Value;
                h.DateFin = DateFin.Value;
                //TESTDBKEYOK
                h.KeyDémarreur = KeyDémarreur;
                h.KeyTermineur = KeyTermineur;
                h.CodesRetourString = CodesRetourString;
                h.EtatCode = EtatCode;
                h.EtapeAppelante = EtapeAppelante;
                h.Etape = this;
                h.RunGeneration = RunGeneration;
            }
            DateDebut = DateTime.Now;
            RunGeneration = Workflow.RunGeneration;
            DateFin = null;
            CodesRetour = new string[0];
            CSessionClient session = CSessionClient.GetSessionForIdSession(ContexteDonnee.IdSession);
            if (session is CSousSessionClient)
                session = ((CSousSessionClient)session).RootSession;
            IInfoUtilisateur info = session != null ? session.GetInfoUtilisateur() : null;
            KeyDémarreur = info != null ? info.KeyUtilisateur : null;
            KeyTermineur = null;
            EtatCode = (int)EEtatEtapeWorkflow.ADemarrer;
            if (Workflow.EtapeAppelante != null && Workflow.EtapeAppelante.Etat.Code != EEtatEtapeWorkflow.Démarrée)
            {
                Workflow.EtapeAppelante.OnDemarrageDepuisSousWorkflow();
            }
            Workflow.IsRunning = true;
            return CResultAErreur.True;
        }

        //---------------------------------------------------------
        /// <summary>
        /// Stocke les informations de fin d'étape dans l'étape
        /// <BR></BR>Cette méthode ne doit pas être utilisée en dehors d'un appel par l'objet serveur
        /// </summary>
        /// <returns></returns>
        public CResultAErreur InternalSetInfosTerminéeInCurrentContexte(EEtatEtapeWorkflow etat)
        {
            DateFin = DateTime.Now;
            CSessionClient session = CSessionClient.GetSessionForIdSession(ContexteDonnee.IdSession);
            IInfoUtilisateur info = session != null ? session.GetInfoUtilisateur() : null;
            //TESTDBKEYOK
            KeyTermineur = info != null ? info.KeyUtilisateur : null;
            EtatCode = (int)etat;
            return CResultAErreur.True;
        }

        //---------------------------------------------------------
        /// <summary>
        /// Démarre l'étape, et sauvegarde les données si c'est  ok
        /// Le démarrage d'une étape peut provoquer sa fin également.<BR>
        /// </BR>
        /// <BR></BR>Cette méthode ne doit pas être utilisée en dehors d'un appel par l'objet serveur
        /// </summary>
        /// <returns></returns>
        public CResultAErreur InternalRunAndSaveifOk()
        {
            CResultAErreur result = CResultAErreur.True;
            if (TypeEtape != null && TypeEtape.Bloc != null)
            {
                result = TypeEtape.Bloc.RunAndSaveIfOk(this);
            }
            else
            {
                result = InternalSetInfosTerminéeInCurrentContexte(EEtatEtapeWorkflow.Terminée);
            }
            if (result)
                result = ContexteDonnee.SaveAll(true);
            return result;

        }

        //----------------------------------------------------------------------------------
        //Prépare l'étape pour un démarrage
        internal void DemandeDemarrageInCurrentContext(CEtapeWorkflow etapeAppelante)
        {
            InternalSetInfosDemarrageInCurrentContext();
            EtapeAppelante = etapeAppelante;
        }

        //----------------------------------------------------------------------------------
        public void StartWithPath(string strPath)
        {
            InternalSetInfosDemarrageInCurrentContext();
            CBlocWorkflowWorkflow bw = TypeEtape.Bloc as CBlocWorkflowWorkflow;
            if (bw != null)
                bw.StartWithPath(this, strPath);
        }

        //----------------------------------------------------------------------------------
        public CResultAErreur GetErreursManualEndEtape()
        {
            return TypeEtape.Bloc.GetErreursManualEndEtape(this);
        }

        //----------------------------------------------------------------------------------
        public CResultAErreur EndEtapeNoSave()
        {
            CResultAErreur result = CResultAErreur.True;
            if (EtatCode != (int)EEtatEtapeWorkflow.Démarrée)
            {
                result.EmpileErreur(I.T("Can not end step @1 while it's not running|20083", Libelle));
                return result;
            }
            result = TypeEtape.Bloc.EndEtapeNoSave(this);
            return result;
        }


        public CResultAErreur EndEtapeAndSaveIfOk()
        {
            CResultAErreur result = CResultAErreur.True;
            if (EtatCode != (int)EEtatEtapeWorkflow.Démarrée)
            {
                result.EmpileErreur(I.T("Can not end step @1 while it's not running|20083", Libelle));
                return result;
            }
            result = TypeEtape.Bloc.EndAndSaveIfOk(this);
            return result;
        }

        //---------------------------------------------------------
        public IDefinisseurEvenements[] Definisseurs
        {
            get
            {
                if (TypeEtape != null)
                    return new IDefinisseurEvenements[] { TypeEtape };
                return new IDefinisseurEvenements[0];
            }
        }

        //---------------------------------------------------------
        public bool IsDefiniPar(IDefinisseurEvenements definisseur)
        {
            return definisseur != null && definisseur.Equals(TypeEtape);
        }

        //---------------------------------------------------------
        /// <summary>
        /// Annule l'exécution d'une étape
        /// </summary>
        [DynamicMethod("Cancel the execution of a step")]
        public void CancelStep()
        {
            if (EtatCode == (int)EEtatEtapeWorkflow.Démarrée)
            {
                InternalSetInfosTerminéeInCurrentContexte(EEtatEtapeWorkflow.Annulée);
                if (TypeEtape != null && TypeEtape.Bloc != null)
                    TypeEtape.Bloc.OnCancelStep(this);
            }
        }

        //------------------------------------------------------------------
        [DynamicMethod("End step and move to next step in workflow. Returns true if ok", "True to force end even if errors occurs")]
        public bool EndStep(bool bForce)
        {
            CResultAErreur result = EndEtapeNoSave();
            if (!result && bForce)
            {
                InternalSetInfosTerminéeInCurrentContexte(EEtatEtapeWorkflow.Terminée);
                return true;
            }
            return result.Result;
        }


        //------------------------------------------------------------------
        [DynamicMethod("Start or Restart the execution of a step")]
        public void StartStep()
        {
            InternalSetInfosDemarrageInCurrentContext();
        }


        //------------------------------------------------------------------
        /// <summary>
        /// Etape ayant lancé cette étape
        /// </summary>
        [DynamicField("Calling step")]
        public CEtapeWorkflow EtapeAppelante
        {
            get
            {
                return (CEtapeWorkflow)GetParent(c_champIdEtapeAppelante, typeof(CEtapeWorkflow));
            }
            set
            {
                SetParent(c_champIdEtapeAppelante, value);
            }
        }

        //---------------------------------------------
        [TableFieldProperty(c_champIdEtapeAppelante, true)]
        [DynamicField("Calling step id")]
        public int? IdEtapeAppelante
        {
            get
            {
                return (int?)Row.Get<int?>(c_champIdEtapeAppelante);
            }
            set
            {
                Row[c_champIdEtapeAppelante, true] = value;
            }
        }


        //---------------------------------------------
        /// <summary>
        /// Liste des étapes lancées par cette étape
        /// </summary>
        //[RelationFille(typeof(CEtapeWorkflow), "EtapeAppelante")]
        [DynamicChilds("Etapes lancées", typeof(CEtapeWorkflow))]
        public CListeObjetsDonnees EtapesLancées
        {
            get
            {
                return GetDependancesListe(CEtapeWorkflow.c_nomTable, c_champIdEtapeAppelante);
            }
        }

        #region IObjetDonneeAutoReference Membres
        //---------------------------------------------
        public string ChampParent
        {
            get { return c_champIdEtapeAppelante; }
        }

        //---------------------------------------------
        public string ProprieteListeFils
        {
            get { return "EtapesLancées"; }
        }

        //---------------------------------------------
        public IObjetDonneeAutoReference ObjetAutoRefParent
        {
            get
            {
                return EtapeAppelante;
            }
        }

        //---------------------------------------------
        public CListeObjetsDonnees ObjetsAutoRefFils
        {
            get
            {
                return EtapesLancées;
            }
        }

        #endregion


        //---------------------------------------------
        /// <summary>
        /// Donne la liste des gels de la phase
        /// </summary>
        [RelationFille(typeof(CGelEtapeWorkflow), "EtapeWorkflow")]
        [DynamicChilds("Freeze list", typeof(CGelEtapeWorkflow))]
        public CListeObjetsDonnees Gels
        {
            get
            {
                return GetDependancesListe(CGelEtapeWorkflow.c_nomTable, c_champId);
            }
        }

        //---------------------------------------------------------
        [DynamicField("IsFrozen")]
        public bool EstGelee
        {
            get
            {
                bool bGelee = false;
                foreach (CGelEtapeWorkflow gel in Gels)
                {
                    if (gel.DateFin == null)
                    {
                        bGelee = true;
                        break;
                    }
                }
                return bGelee;
            }
        }

        //---------------------------------------------------------
        public CResultAErreur Geler(DateTime dateDebut, CCauseGel cause, string strInfo)
        {
            return Geler(dateDebut, cause, strInfo, null);
        }

        //---------------------------------------------------------
        public CResultAErreur Degeler(DateTime dateFin, string strInfoFinGel)
        {
            return Degeler(dateFin, strInfoFinGel, null);
        }

        //-----------------------------------------------------------------------
        [DynamicMethod("Freezes the phase",
            "Freezing start date",
            "Freezing cause",
            "Freezing information",
            "Key of Freezing Responsible Member")]
        public CResultAErreur FreezeStep(DateTime startDate, CCauseGel freezeCause, string strComment, string strKeyMember)
        {
            return Geler(startDate, freezeCause, strComment, CDbKey.CreateFromStringValue(strKeyMember));
        }

        //-----------------------------------------------------------------------
        public CResultAErreur Geler(DateTime dateDebut, CCauseGel cause, string strInfo, CDbKey keyResponsableDebutGel)
        {
            CResultAErreur result = CResultAErreur.True;
            //Vérifie qu'il n'y a pas déjà un gel ouvert
            if (EstGelee)
            {
                result.EmpileErreur(I.T("Freezing impossible for a Step already freezed|10004"));
                return result;
            }
            CGelEtapeWorkflow gel = new CGelEtapeWorkflow(ContexteDonnee);
            gel.CreateNew();
            gel.DateDebut = dateDebut;
            gel.CauseGel = cause;
            gel.InfosDebutGel = strInfo;
            gel.EtapeWorkflow = this;
            gel.KeyResponsabelDebutGel = keyResponsableDebutGel;
            result = gel.CommitEdit();

            return result;
        }

        //------------------------------------------------------------------------
        [DynamicMethod("Unfrezzes the Step",
            "Freezing end date",
            "Freezing information",
            "Key of Unfreez Responsible Member")]
        public CResultAErreur UnfreezeStep(DateTime endDate, string strComment, string strKeyMember)
        {
            return Degeler(endDate, strComment, CDbKey.CreateFromStringValue(strKeyMember));
        }

        //------------------------------------------------------------------------
        public CResultAErreur Degeler(DateTime dateFin, string strInfoFinGel, CDbKey keyResponsableFinGel)
        {
            CResultAErreur result = CResultAErreur.True;
            CGelEtapeWorkflow gelADegeler = null;
            foreach (CGelEtapeWorkflow gel in Gels)
            {
                if (gel.DateFin == null)
                {
                    gelADegeler = gel;
                    break;
                }
            }
            if (gelADegeler == null)
            {
                result.EmpileErreur(I.T("Unfreezing impossible for an element not freezed|10005"));
                return result;
            }

            gelADegeler.BeginEdit();
            gelADegeler.InfosFinGel = strInfoFinGel;
            gelADegeler.DateFin = dateFin;
            gelADegeler.KeyResponsabelFinGel = keyResponsableFinGel;
            result = gelADegeler.CommitEdit();

            return result;
        }
    }
}
