using System;
using System.Data;
using System.Collections;
using System.Linq;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.recherche;
using sc2i.data.dynamic;
using sc2i.multitiers.client;
using System.Collections.Generic;
using sc2i.process.workflow.blocs;

namespace sc2i.process.workflow
{
	/// <summary>
	/// Représente un workflow en cours d'exécution
	/// </summary>
    /// <remarks>
    /// Chaque fois qu'un workflow est démarré, l'état de ce workflow est géré
    /// par un "workflow". Il représente l'instance du workflow en
    /// cours d'exécution
    /// </remarks>
	[Table(CWorkflow.c_nomTable, CWorkflow.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CWorkflowServeur")]
	[DynamicClass("Workflow")]
    [AutoExec("Autoexec")]
	public class CWorkflow : CElementAChamp, IElementAEvenementsDefinis
	{
        public const string c_roleChampCustom = "RUN_WKF";

		public const string c_nomTable = "WORKFLOW";
		public const string c_champId = "WKF_ID";
        public const string c_champLibelle = "WKF_LABEL";
        public const string c_champDateCreation = "WKF_CREATION_DATE";
        public const string c_champKeyGestionnaire = "WKF_MANAGER_KEY";
        public const string c_champIsRunning = "WKF_IS_RUNNING";
        public const string c_champRunGeneration = "WKF_GENERATION";

        public const string c_champIdEtapeAppelante = "WKF_CALLING_STEP";

#if PDA
		/// ///////////////////////////////////////////////////////
		public CWorkflow(  )
			:base()
		{
		}
#endif
		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CWorkflow( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CWorkflow ( DataRow row )
			:base(row)
		{
		}

        /// //////////////////////////////////////////////////
        public static void Autoexec()
        {
            CRoleChampCustom.RegisterRole(c_roleChampCustom, I.T("Workflow|20066"),
                typeof(CWorkflow),
                typeof(CTypeWorkflow));
        }

		/// //////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
            DateCreation = DateTime.Now;
            CSessionClient session = CSessionClient.GetSessionForIdSession(ContexteDonnee.IdSession);
            IInfoUtilisateur info = session != null?session.GetInfoUtilisateur():null;
            //TESTDBKEYOK
            if ( info != null )
                KeyManager = info.KeyUtilisateur;
            else
                KeyManager = null;
            IsRunning = false;
            RunGeneration = 0;
		}

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Worfklow @1|20061",Libelle);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champDateCreation+" desc"};
		}


        ///---------------
        /// <summary>
        /// Libellé du workflow
        /// </summary>
        [TableFieldProperty(c_champLibelle, 1024)]
        [DynamicField("Label")]
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

        /// <summary>
        /// Génération d'execution. <BR>
        /// S'incrémente à chaque lancement du workflow
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


        //-----------------------------------------------------------
        /// <summary>
        /// Indique si le workflow est en cours d'exécution ou pas.<BR></BR>
        /// Un workflow n'est plus en cours d'exécution quand toutes ses étapes sont terminées.
        /// </summary>
        [TableFieldProperty(c_champIsRunning)]
        [DynamicField("Is running")]
        public bool IsRunning
        {
            get
            {
                return (bool)Row[c_champIsRunning];
            }
            set
            {
                Row[c_champIsRunning] = value;
            }
        }



        //-----------------------------------------------------------
        /// <summary>
        /// Date de création du workflow
        /// </summary>
        [TableFieldProperty(c_champDateCreation)]
        [DynamicField("Creation date")]
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
        /// Type du workflow gérant l'avancement de cette instance  
        /// </summary>
        [Relation(
            CTypeWorkflow.c_nomTable,
            CTypeWorkflow.c_champId,
            CTypeWorkflow.c_champId,
            true,
            false,
            true)]
        [DynamicField("Workflow type")]
        public CTypeWorkflow TypeWorkflow
        {
            get
            {
                return (CTypeWorkflow)GetParent(CTypeWorkflow.c_champId, typeof(CTypeWorkflow));
            }
            set
            {
                SetParent(CTypeWorkflow.c_champId, value);
            }
        }


        //---------------------------------------------
        /// <summary>
        /// Liste les étapes créées pour ce workflow.
        /// <BR></BR>
        /// Attention, les étapes sont créées au fur et à mesure de l'avancement
        /// du workflow
        /// </summary>
        [RelationFille(typeof(CEtapeWorkflow), "Workflow")]
        [DynamicChilds("Steps", typeof(CEtapeWorkflow))]
        public CListeObjetsDonnees Etapes
        {
            get
            {
                return GetDependancesListe(CEtapeWorkflow.c_nomTable, c_champId);
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Liste des étapes en cours d'exécution
        /// </summary>
        [DynamicChilds("Running steps", typeof(CEtapeWorkflow))]
        public CListeObjetsDonnees EtapesEnCours
        {
            get
            {
                CListeObjetsDonnees lst = Etapes;
                lst.FiltrePrincipal = CFiltreData.GetAndFiltre(lst.FiltrePrincipal,
                    new CFiltreData(CEtapeWorkflow.c_champEtat + "=@1",
                        (int)EEtatEtapeWorkflow.Démarrée));
                return lst;
            }
        }

        

        //-------------------------------------------------------------------
        public override CRoleChampCustom RoleChampCustomAssocie
        {
            get { return CRoleChampCustom.GetRole(c_roleChampCustom); }
        }

        //-------------------------------------------------------------------
        public override CRelationElementAChamp_ChampCustom GetNewRelationToChamp()
        {
            return new CRelationWorkflow_ChampCustom(ContexteDonnee);
        }

        //-------------------------------------------------------------------
        public override IDefinisseurChampCustom[] DefinisseursDeChamps
        {
            get
            {
                if (TypeWorkflow == null)
                    return new IDefinisseurChampCustom[0];
                else
                    return new IDefinisseurChampCustom[] { TypeWorkflow };
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Liste des relations du workflow avec les champs personnalisés
        /// </summary>
        [RelationFille(typeof(CRelationWorkflow_ChampCustom), "ElementAChamps")]
        [DynamicChilds("Custom fields relations", typeof(CRelationWorkflow_ChampCustom))]
        public override CListeObjetsDonnees RelationsChampsCustom
        {
            get { return GetDependancesListe(CRelationWorkflow_ChampCustom.c_nomTable, c_champId); }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Clé de l'utilisateur en charge de la gestion de ce workflow
        /// </summary>
        [TableFieldProperty(c_champKeyGestionnaire, 64)]
        [ReplaceField("IdManager","Manager user id")]
        [DynamicField("Manager user key string")]
        public string KeyManagerString
        {
            get
            {
                //TESTDBKEYOK
                return (string)Row[c_champKeyGestionnaire];
            }
            set
            {
                //TESTDBKEYOK
                Row[c_champKeyGestionnaire] = value;
            }
        }

        //-----------------------------------------------------------
        [DynamicField("Manager user key")]
        public CDbKey KeyManager
        {
            get
            {
                //TESTDBKEYOK
                return CDbKey.CreateFromStringValue(KeyManagerString);
            }
            set
            {
                //TESTDBKEYOK
                if (value != null)
                    KeyManagerString = value.StringValue;
                else
                    KeyManagerString = "";
            }
        }





        //-----------------------------------------------------------
        [DynamicMethod("Starts the workflow", "Step type for start (null for default)")]
        public bool StartWorkflow(CTypeEtapeWorkflow typeEtapeDebut)
        {
            return DémarreWorkflow(typeEtapeDebut, false);
        }

        //-----------------------------------------------------------
        [DynamicMethod("Starts the workflow and sub workflows according to a step path array", "Step type code array to start. This method can start many steps. If a step is contains in a sub-workflow, the code should be a path of Workflow steps code, separated by a point")]
        public bool StartWorkflowMultiSteps(ArrayList listEtapesToStart)
        {
            CSessionClient session = CSessionClient.GetSessionForIdSession(ContexteDonnee.IdSession);
            IInfoUtilisateur info = session != null ? session.GetInfoUtilisateur() : null;
            if (info != null)
                KeyManager = info.KeyUtilisateur;
            CResultAErreur result = CResultAErreur.True;
            if (TypeWorkflow.Etapes.Count == 0)
                if (EtapeAppelante != null)
                {
                    EtapeAppelante.EndEtapeNoSave();
                    return true;
                }
            foreach (object stepsPath in listEtapesToStart)
            {
                if (!(stepsPath is string))
                    throw new Exception(I.T("Invalid parameter for 'StartWorkflowMultiSteps'. Parameter must be an array of steps type path (separated by points).|20106"));
                string strPath = (string)stepsPath;
                int nPosPoint = strPath.IndexOf('.');
                string strStepType = "";
                string strSuitePath = "";
                if (nPosPoint > 0)
                {
                    strStepType = strPath.Substring(0, nPosPoint);
                    strSuitePath = strPath.Substring(nPosPoint + 1);
                }
                else
                    strStepType = strPath;
                CTypeEtapeWorkflow typeEtape = null;
                if (strStepType.Trim().Length == 0)
                    typeEtape = TypeWorkflow.EtapeDemarrageDefaut;
                else
                {
                    CListeObjetsDonnees lst = TypeWorkflow.Etapes;
                    lst.Filtre = new CFiltreData(CTypeEtapeWorkflow.c_champIdUniversel + "=@1", strStepType);
                    if (lst.Count > 0)
                        typeEtape = lst[0] as CTypeEtapeWorkflow;
                    if (typeEtape == null)
                        throw new Exception(I.T("Step type @1 can not be found|20107"));
                }

                CResultAErreurType<CEtapeWorkflow> resEtape = CreateOrGetEtapeInCurrentContexte(typeEtape);
                if (resEtape)
                {
                    CEtapeWorkflow etape = resEtape.DataType;
                    etape.StartWithPath(strSuitePath);

                }
                else
                {
                    result.EmpileErreur(resEtape.Erreur);
                }
                IsRunning = true;
                RunGeneration++;
            }
            return result;
        }

        
        public CResultAErreur DémarreWorkflow(CTypeEtapeWorkflow typeEtapeDebut, bool bStartImmediate)
        {
            CSessionClient session = CSessionClient.GetSessionForIdSession(ContexteDonnee.IdSession);
            IInfoUtilisateur info = session != null ? session.GetInfoUtilisateur() : null;
            if (info != null)
                //TESTDBKEYOK
                KeyManager = info.KeyUtilisateur;
            CResultAErreur result = CResultAErreur.True;
            if ( EtapesEnCours.Count != 0 )
            {
                result.EmpileErreur(I.T("Workflow was already started|20073"));
                return result;
            }
            if ( typeEtapeDebut == null || 
                typeEtapeDebut.Workflow != TypeWorkflow//cas pourri : l'étape de début n'est pas valide pour ce workflow ça ne doit pas arriver
                )
            {
                if (TypeWorkflow == null)
                {
                    result.EmpileErreur(I.T("Workflow type should be set before it starts|20077"));
                    return result;
                }
                typeEtapeDebut = TypeWorkflow.EtapeDemarrageDefaut;
                if ( typeEtapeDebut == null )
                {
                    result.EmpileErreur(I.T("Workflow type @1 doesn't have any start point|20074",
                        TypeWorkflow != null?TypeWorkflow.Libelle:"???"));
                    return result;
                }
            }
            using (CContexteDonnee ctx = new CContexteDonnee(ContexteDonnee.IdSession, true, false))
            {
                ///Si bStartImmediate, travaille dans un contexte spécifique qui est sauvé tout de suite
                CContexteDonnee contexteDeTravail = bStartImmediate ? ctx : ContexteDonnee;
                CWorkflow wkf = GetObjetInContexte(contexteDeTravail) as CWorkflow;
                typeEtapeDebut = typeEtapeDebut.GetObjetInContexte(contexteDeTravail) as CTypeEtapeWorkflow;
                CResultAErreurType<CEtapeWorkflow> resEtape = wkf.CreateOrGetEtapeInCurrentContexte(typeEtapeDebut);
                if (resEtape)
                {
                    CEtapeWorkflow etape = resEtape.DataType;
                    etape.DemandeDemarrageInCurrentContext(null);

                }
                else
                {
                    result.EmpileErreur(resEtape.Erreur);
                }
                IsRunning = true;
                RunGeneration++;
                if (result && bStartImmediate)
                    result = ctx.SaveAll(true);
            }
            return result;
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Annule l'execution de toutes les étapes d'un workflow
        /// </summary>
        [DynamicMethod("Stop workflow and cancel all running steps")]
        public void StopWorkflow()
        {
            foreach (CEtapeWorkflow etape in Etapes)
            {
                if (etape.EtatCode == (int)EEtatEtapeWorkflow.Démarrée)
                {
                    etape.CancelStep();
                }
            }
            IsRunning = false;
        }


        //-----------------------------------------------------------
        ///Remarque :! on est obligé de stocker l'id et de ne pas faire d'intégrité
        ///référentiel car la table Etape point déjà sur la table workflow. Ca 
        ///provoquerait une référence cyclique qui peut poser des problèmes à la sauvegarde (
        ///On ne sait plus l'ordre des éléments).
        ///Par contre, il faut faire attention que l'étape soit sauvegardée avant qu'elle soit associée
        ///à un workflow, sinon, on aura un id négatif !!!
        /// <summary>
        /// Id de l'étape ayant déclenché ce workflow si ce workflow est déclenché par une étape d'un workflow parent
        /// </summary>
        [TableFieldProperty(c_champIdEtapeAppelante, NullAutorise = true)]
        [DynamicField("Calling step Id")]
        public int? IdEtapeAppelante
        {
            get
            {
                return Row.Get<int?>(c_champIdEtapeAppelante);
            }
            set
            {
                Row[c_champIdEtapeAppelante, true] = value;
            }
        }

        /// <summary>
        /// Etape ayant déclenché ce workflow si ce workflow est déclenché par une étape d'un workflow parent
        /// </summary>
        [DynamicField("Calling step")]
        public CEtapeWorkflow EtapeAppelante
        {
            get
            {
                if (IdEtapeAppelante != null)
                {
                    CEtapeWorkflow etape = new CEtapeWorkflow(ContexteDonnee);
                    if (IdEtapeAppelante < 0)//Pour pallier aux problèmes d'id négatif stocké qui peut arriver
                    {
                        if (!etape.ReadIfExists(new CFiltreData(CEtapeWorkflow.c_champIdWorkflowLancé + "=@1",
                            Id)))
                            etape = null;
                    }
                    else
                    {
                        if (!etape.ReadIfExists(IdEtapeAppelante.Value))
                            etape = null;
                    }
                    return etape;
                }
                return null;
            }
            set
            {
                if (value == null)
                    IdEtapeAppelante = null;
                else
                    IdEtapeAppelante = value.Id;
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Workflow ayant déclenché ce workflow (il s'agit du workflow auquel appartient l'étape appelante)
        /// </summary>
        [DynamicField("Calling workflow")]
        public CWorkflow WorkflowParent
        {
            get
            {
                CEtapeWorkflow etape = EtapeAppelante;
                if (etape != null)
                    return etape.Workflow;
                return null;
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Démarre les étapes suivantes d'une étape
        /// </summary>
        /// <param name="etape"></param>
        /// <returns></returns>
        public CResultAErreur PrépareSuiteEtapeInCurrentContext(CEtapeWorkflow etape)
        {
            CResultAErreur result = CResultAErreur.True;
            CTypeEtapeWorkflow typeEtape = etape.TypeEtape;
            if (typeEtape.LiensSortants.Count == 0)
            {
                IsRunning = false;
                if (EtapeAppelante != null)
                {
                    //Fin de l'étape appelante
                    result = EtapeAppelante.EndEtapeNoSave();
                    if (!result)
                        return result;
                }
                return result;
            }
            foreach (CLienEtapesWorkflow lien in typeEtape.LiensSortants)
            {
                bool bStartLien = true;
                C2iExpression formuleLien = lien.Formule;
                if (lien.ActivationCode.Trim().Length > 0)
                {
                    bStartLien = false;
                    foreach (string strCodeRetour in etape.CodesRetour)
                    {
                        if (strCodeRetour.ToUpper() == lien.ActivationCode.ToUpper())
                        {
                            bStartLien = true;
                            break;
                        }
                    }
                }
                if (formuleLien != null && bStartLien && !(formuleLien is C2iExpressionVrai))
                {
                    CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(etape);
                    result = formuleLien.Eval(ctx);
                    if (!result)
                    {
                        result.EmpileErreur(I.T("Error on condition formula on link @1 from step @2|20008",
                            lien.Libelle, etape.Libelle));
                        return result;
                    }
                    bStartLien = result.Data != null && CUtilBool.BoolFromString(result.Data.ToString()) == true;
                }
                if (bStartLien)
                {
                    CTypeEtapeWorkflow typeDestination = lien.EtapeDestination;
                    CResultAErreurType<CEtapeWorkflow> resEtape = etape.Workflow.CreateOrGetEtapeInCurrentContexte(typeDestination);
                    if (!resEtape)
                    {
                        result.EmpileErreur(resEtape.Erreur);
                        return result;
                    }
                    else
                        resEtape.DataType.DemandeDemarrageInCurrentContext(etape);
                }
            }
            return result;

        }

        [DynamicMethod("Return step in this workflow for required step type", "Step type universal id")]
        public CEtapeWorkflow GetStepForStepType(string strIdUniverselEtape)
        {
            CListeObjetsDonnees lstEtapes = TypeWorkflow.Etapes;
            lstEtapes.Filtre = new CFiltreData(CObjetDonnee.c_champIdUniversel + "=@1",
                strIdUniverselEtape);
            if (lstEtapes.Count > 0)
            {
                CTypeEtapeWorkflow typeEtape = lstEtapes[0] as CTypeEtapeWorkflow;
                return GetEtapeForType(typeEtape);
            }
            return null;

        }
        public CEtapeWorkflow GetEtapeForType(CTypeEtapeWorkflow typeEtape)
        {
            CListeObjetsDonnees lstEtapes = Etapes;
            lstEtapes.Filtre = new CFiltreData(CTypeEtapeWorkflow.c_champId + "=@1",
                typeEtape.Id);
            if (lstEtapes.Count > 0)
                return lstEtapes[0] as CEtapeWorkflow;
            return null;
        }


        /// <summary>
        /// Retourne l'étape de ce type pour le workflow
        /// </summary>
        /// <param name="workflow"></param>
        /// <returns></returns>
        public CResultAErreurType<CEtapeWorkflow> CreateOrGetEtapeInCurrentContexte(CTypeEtapeWorkflow typeEtape)
        {
            CResultAErreurType<CEtapeWorkflow> result = new CResultAErreurType<CEtapeWorkflow>();
            //Vérifie que le workflow n'a pas déjà une étape de ce type
            if (typeEtape == null)
            {
                result.EmpileErreur(I.T("Create step need a step type|20076"));
                return result;
            }
            //Vérifie que le workflow n'a pas déjà une étape de ce type
            CEtapeWorkflow etape = GetEtapeForType(typeEtape);
            bool bIsRedemarrage = true;
            if (etape == null)
            {
                etape = new CEtapeWorkflow(ContexteDonnee);
                etape.CreateNewInCurrentContexte();
                etape.Workflow = this;
                etape.TypeEtape = typeEtape;
                bIsRedemarrage = false;
            }
            CResultAErreur resTmp = typeEtape.ParametresInitialisation.AppliqueTo(etape, bIsRedemarrage);
            if (!resTmp)
                result.EmpileErreur(resTmp.Erreur);
            if (result)
                result.DataType = etape;
            return result;
        }

        //----------------------------------------------------
        public IDefinisseurEvenements[] Definisseurs
        {
            get 
            {
                List<IDefinisseurEvenements> lstDefs = new List<IDefinisseurEvenements>();
                if (TypeWorkflow != null)
                    lstDefs.Add(TypeWorkflow);
                return lstDefs.ToArray();
            }
        }

        //----------------------------------------------------
        public bool IsDefiniPar(IDefinisseurEvenements definisseur)
        {
            return definisseur != null && definisseur.Equals(TypeWorkflow);
        }

        
        
    }
		
}
