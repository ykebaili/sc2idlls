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
    /// Repr�sente une �tape d'un workflow qui est ou a �t� ex�cut�e
    /// </summary>
    /// <remarks>
    /// Chaque fois qu'une �tape de workflow est ex�cut�e, les donn�es de celle-ci
    /// sont g�r�es par un "Workflow step".
    /// <P>Il ne peut exister qu'une seul �tape d'un type d'�tape donn� pour un workflow donn�. Si le workflow provoque des 
    /// retours sur une �tape, l'�xecution pr�c�dente est historis�e, et l'�tape est relanc�e.</P>
    /// <P>Une �tape est d�marr�e automatiquement lorsqu'elle est dans l'�tat <B>En attente</B> et qu'une date de d�but lui est
    /// affect�e.</P>
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
        public const string c_champKeyDemarr�ePar = "WKFSTP_STARTUSR_KEY";
        public const string c_champKeyTermin�ePar = "WKFSTP_ENDUSR_KEY";
        public const string c_champEtat = "WKFSTP_STATE";
        public const string c_champLastError = "WKFSTP_LAST_ERROR";
        public const string c_champCodesRetour = "WKFSTP_RETURN_CODES";
        public const string c_champIdWorkflowLanc� = "WKFSTP_CALLED_WORKFLOW";
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
        /// Date de d�marrage de l'�tape
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
        /// Pour compatiblit� avec le passage de CDbKey
        /// La propri�t� existait avant que toutes les entit�s
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
        /// Date de fin de l'�tape
        /// </summary>
        /// <remarks>
        /// Chaque fois qu'une date de fin est valid�e pour une �tape, une entr�e
        /// d'<see cref="CEtapeWorkflowHistorique">historique</see> est cr��e
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
        /// Libell� de l'�tape
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
        /// Workflow en cours d'ex�cution ayant ex�cut� cette �tape
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
        /// Indique la g�n�ration de d�marrage de workflow
        /// correspondant � l'execution en cours ou la 
        /// derni�re execution de cette �tape
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
        /// Si l'�tape est une �tape lan�ant un workflow, cette propri�t� repr�sente 
        /// le workflow qui a �t� lanc� par l'�tape.
        /// </summary>
        [Relation(
            CWorkflow.c_nomTable,
            CWorkflow.c_champId,
            c_champIdWorkflowLanc�,
            false,
            false,
            false)]
        [DynamicField("Launched workflow")]
        public CWorkflow WorkflowLanc�
        {
            get
            {
                return (CWorkflow)GetParent(c_champIdWorkflowLanc�, typeof(CWorkflow));
            }
            set
            {
                SetParent(c_champIdWorkflowLanc�, value);
            }
        }





        //-------------------------------------------------------------------
        /// <summary>
        /// Indique le mod�le d'�tape auquel correspond cette ex�cution
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
        /// Code des entit�s affect�s � la r�alisation de cette �tape.
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
        /// Liste toutes les affectations de cette �tape
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
        /// Liens vers les valeurs de champs personnalis�s
        /// </summary>
        [RelationFille(typeof(CRelationEtapeWorkflow_ChampCustom), "ElementAChamps")]
        [DynamicChilds("Custom fields relations", typeof(CRelationEtapeWorkflow_ChampCustom))]
        public override CListeObjetsDonnees RelationsChampsCustom
        {
            get { return GetDependancesListe(CRelationEtapeWorkflow_ChampCustom.c_nomTable, c_champId); }
        }


        //---------------------------------------------
        /// <summary>
        /// Liste des entr�es historique de cette �tape.
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
        /// Indique l'id de l'utilisateur ayant d�clench� le d�marrage de l'�tape
        /// <BR></BR>
        /// Une valeur nulle indique que l'id du cr�ateur de l'�tape est inconnu
        /// </summary>
        [TableFieldProperty(c_champKeyDemarr�ePar, 64)]
        [ReplaceField("IdD�marreur", "Started by Id")]
        [DynamicField("Started by Key string")]
        public string KeyD�marreurString
        {
            get
            {
                //TESTDBKEYOK
                return Row.Get<string>(c_champKeyDemarr�ePar);
            }
            set
            {
                //TESTDBKEYOK
                Row[c_champKeyDemarr�ePar, true] = value;
            }
        }

        //-----------------------------------------------------------
        [DynamicField("Start by key")]
        public CDbKey KeyD�marreur
        {
            get
            {
                //TESTDBKEYOK
                return CDbKey.CreateFromStringValue(KeyD�marreurString);
            }
            set
            {
                //TESTDBKEYOK
                if (value != null)
                    KeyD�marreurString = value.StringValue;
                else
                    KeyD�marreurString = "";
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Indique l'id de l'utilisateur ayant d�clench� la fin de l'�tape
        /// <BR></BR>
        /// Une valeur nulle indique que l'id du d�clencheur de fin de l'�tape est inconnu
        /// </summary>
        [TableFieldProperty(c_champKeyTermin�ePar, 64)]
        [ReplaceField("IdTermineur", "Ended by ID")]
        [DynamicField("Ended by Key string")]
        public String KeyTermineurString
        {
            get
            {
                //TESTDBKEYOK
                return Row.Get<string>(c_champKeyTermin�ePar);
            }
            set
            {
                //TESTDBKEYOK
                Row[c_champKeyTermin�ePar, true] = value;
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
        /// Etat de l'�tape
        /// </summary>
        /// <remarks>
        /// Les valeurs possibles sont :
        /// <LI>0 : En attente (l'�tape est en attente de d�marrage. Elle d�marrera d�s qu'une date de d�but lui sera affect�e</LI>
        /// <LI>1 : � d�marrer</LI>
        /// <LI>2 : D�marr�e</LI>
        /// <LI>3 : Termin�e</LI>
        /// <LI>4 : Erreur</LI>
        /// <LI>5 : Annul�e</LI>
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
        /// Etat (avec code et libell�) de l'�tape
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
        /// Dernier message d'erreur rencontr� lors d'une execution de cette �tape, qui n'a pas abouti.
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
        /// Codes retourn�s par cette ex�cution de l'�tape. Les codes sont retourn�s
        /// sous forme d'une cha�ne de caract�res.
        /// </summary>
        /// <remarks>
        /// En fin d'�tape, suivant son type, l'�tape se voit affecter des
        /// codes retour. Ces codes activent ou non les liens sortant de cette �tape.
        /// <BR></BR>
        /// Les codes de retour sont toujours encadr�s par des ~
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
        /// A appeller lorsqu'un �tape contenant un sous workflow
        /// d�marre ou red�marre car le sous workflow a red�marr�.
        /// </summary>
        /// <returns></returns>
        public CResultAErreur OnDemarrageDepuisSousWorkflow()
        {
            CResultAErreur result = CResultAErreur.True;
            if (Etat.Code != EEtatEtapeWorkflow.ADemarrer && Etat.Code != EEtatEtapeWorkflow.D�marr�e)
            {
                result = InternalSetInfosDemarrageInCurrentContext();
                EtatCode = (int)EEtatEtapeWorkflow.D�marr�e;
                if (TypeEtape != null)
                {
                    CBlocWorkflowWorkflow bloc = TypeEtape.Bloc as CBlocWorkflowWorkflow;
                    if (bloc != null)
                        result = bloc.OnBlocRedemarr�ParUneEtapeDuSousWorkflow(this);
                }
            }

            return result;
        }


        //---------------------------------------------------------
        /// <summary>
        /// Stocke les diff�rentes informations pour le d�marrage d'une �tape
        /// (date de d�but, date de fin � null, idD�marreur et IdTermineur � null)
        /// <BR></BR>Cette m�thode ne doit pas �tre utilis�e en dehors d'un appel par l'objet serveur
        /// </summary>
        /// <returns></returns>
        public CResultAErreur InternalSetInfosDemarrageInCurrentContext()
        {
            if (DateDebut != null && DateFin != null)
            {
                //Historise l'�tape
                CEtapeWorkflowHistorique h = new CEtapeWorkflowHistorique(ContexteDonnee);
                h.CreateNewInCurrentContexte();
                h.DateDebut = DateDebut.Value;
                h.DateFin = DateFin.Value;
                //TESTDBKEYOK
                h.KeyD�marreur = KeyD�marreur;
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
            KeyD�marreur = info != null ? info.KeyUtilisateur : null;
            KeyTermineur = null;
            EtatCode = (int)EEtatEtapeWorkflow.ADemarrer;
            if (Workflow.EtapeAppelante != null && Workflow.EtapeAppelante.Etat.Code != EEtatEtapeWorkflow.D�marr�e)
            {
                Workflow.EtapeAppelante.OnDemarrageDepuisSousWorkflow();
            }
            Workflow.IsRunning = true;
            return CResultAErreur.True;
        }

        //---------------------------------------------------------
        /// <summary>
        /// Stocke les informations de fin d'�tape dans l'�tape
        /// <BR></BR>Cette m�thode ne doit pas �tre utilis�e en dehors d'un appel par l'objet serveur
        /// </summary>
        /// <returns></returns>
        public CResultAErreur InternalSetInfosTermin�eInCurrentContexte(EEtatEtapeWorkflow etat)
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
        /// D�marre l'�tape, et sauvegarde les donn�es si c'est  ok
        /// Le d�marrage d'une �tape peut provoquer sa fin �galement.<BR>
        /// </BR>
        /// <BR></BR>Cette m�thode ne doit pas �tre utilis�e en dehors d'un appel par l'objet serveur
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
                result = InternalSetInfosTermin�eInCurrentContexte(EEtatEtapeWorkflow.Termin�e);
            }
            if (result)
                result = ContexteDonnee.SaveAll(true);
            return result;

        }

        //----------------------------------------------------------------------------------
        //Pr�pare l'�tape pour un d�marrage
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
            if (EtatCode != (int)EEtatEtapeWorkflow.D�marr�e)
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
            if (EtatCode != (int)EEtatEtapeWorkflow.D�marr�e)
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
        /// Annule l'ex�cution d'une �tape
        /// </summary>
        [DynamicMethod("Cancel the execution of a step")]
        public void CancelStep()
        {
            if (EtatCode == (int)EEtatEtapeWorkflow.D�marr�e)
            {
                InternalSetInfosTermin�eInCurrentContexte(EEtatEtapeWorkflow.Annul�e);
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
                InternalSetInfosTermin�eInCurrentContexte(EEtatEtapeWorkflow.Termin�e);
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
        /// Etape ayant lanc� cette �tape
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
        /// Liste des �tapes lanc�es par cette �tape
        /// </summary>
        //[RelationFille(typeof(CEtapeWorkflow), "EtapeAppelante")]
        [DynamicChilds("Etapes lanc�es", typeof(CEtapeWorkflow))]
        public CListeObjetsDonnees EtapesLanc�es
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
            get { return "EtapesLanc�es"; }
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
                return EtapesLanc�es;
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
            //V�rifie qu'il n'y a pas d�j� un gel ouvert
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
