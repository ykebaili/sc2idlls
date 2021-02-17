using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.recherche;
using sc2i.data.dynamic;
using sc2i.process.workflow.blocs;
using System.IO;
using System.Collections.Generic;

namespace sc2i.process.workflow
{
	/// <summary>
	/// Représente une étape workflow
	/// </summary>
    /// <remarks>
    /// Un workflow est la représentation d'un enchainement d'actions, réalisées
    /// par différents acteurs de l'application.<BR></BR>
    /// Chaque workflow consiste en une successions d'étapes, chaque étape
    /// définissant les actions réalisées durant cette étape.
    /// <BR></BR>
    /// Lors de l'exécution d'une étape, un élément de type 
    /// <see cref="CEtapeWorkflow">Etape</see> est créé. Cet élément
    /// suivra la vie de l'étape de workflow
    /// </remarks>
	[AutoExec("Autoexec")]
    [Table(CTypeEtapeWorkflow.c_nomTable, CTypeEtapeWorkflow.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CTypeEtapeWorkflowServeur")]
	[DynamicClass("Workflow step type")]
    [Unique(false,
        "Workflow step type name should be unique|20060",
        CTypeEtapeWorkflow.c_champLibelle, CTypeWorkflow.c_champId)]
	public class CTypeEtapeWorkflow :
        CElementAChamp,
        IDefinisseurChampCustomRelationObjetDonnee,
        IDefinisseurEvenements
        
	{
		public const string c_nomTable = "WORKFLOW_STEP_TYPE";
		public const string c_champId = "WKFSTPTP_ID";
		public const string c_champLibelle = "WKFSTPTP_NAME";
		public const string c_champDescription = "WKFSTPTP_DESC";
        public const string c_champModeAsynchrone = "WKFSTPTP_ASYNCHRONOUS";
        public const string c_champDefaultStart = "WKFSTPTP_DEFAULT_START";
        public const string c_champExecutionAutomatique = "WKFSTTPTP_AUTOEXEC";
        public const string c_champHasUserInterface = "WKFSTTPTP_HAS_USER_INTF";
        public const string c_champStepType = "WKFSTTP_BLOC_TYPE";

        public const string c_champDataBloc = "WKFSTPTP_BLOC";
        
        
        public const string c_champDataInitialisations = "WKFSTPTP_ASGN_FORMULAS";

        public const string c_champCacheBloc = "WKFSTPTP_CACHE_BLOC";

        public const string c_roleChampCustom = "WORKFLOWSTEP_TYPE";


#if PDA
		/// ///////////////////////////////////////////////////////
		public CEtapeWorkflow(  )
			:base()
		{
		}
#endif
		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CTypeEtapeWorkflow( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CTypeEtapeWorkflow ( DataRow row )
			:base(row)
		{
		}

        /// //////////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
            ModeAsynchrone = false;
            ExecutionAutomatique = false;
            IsDefautStart = false;
            HasUserInterface = false;
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

        
		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Worfklow step type @1|20059",Libelle);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Nom de l'étape. Ce nom doit être unique.
        ///</summary>
        [TableFieldPropertyAttribute(c_champLibelle, 255)]
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

        //-----------------------------------------------------------
        /// <summary>
        /// Si vrai, indique que l'étape s'execute automatiquement si elle
        /// est démarrée par la session de l'utilisateur loggé.
        /// </summary>
        [TableFieldProperty(c_champExecutionAutomatique)]
        [DynamicField("Autoexec")]
        public bool ExecutionAutomatique
        {
            get
            {
                return (bool)Row[c_champExecutionAutomatique];
            }
            set
            {
                Row[c_champExecutionAutomatique] = value;
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// True means, this step type needs a user action (so needs assignments)
        /// </summary>
        [TableFieldProperty(c_champHasUserInterface)]
        [DynamicField("Has user interface")]
        public bool HasUserInterface
        {
            get
            {
                return Row.Get<bool>(c_champHasUserInterface);
            }
            set
            {
                Row[c_champHasUserInterface] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Indique si cette étape est l'étape de démarrage par défaut de son workflow
        /// </summary>
        [TableFieldProperty(c_champDefaultStart)]
        [DynamicField("Is default start")]
        public bool IsDefautStart
        {
            get
            {
                return (bool)Row[c_champDefaultStart];
            }
            set
            {
                if ( value )
                {
                    if ( Workflow != null )
                    {
                        foreach ( CTypeEtapeWorkflow etape in Workflow.Etapes )
                        {
                            if ( etape.Equals ( this ) )
                                etape.Row[c_champDefaultStart] = true;
                            else
                                etape.Row[c_champDefaultStart] = false;
                        }
                    }
                }
                else
                    Row[c_champDefaultStart] = false;
            }
        }


		/// //////////////////////////////////////////////////
		///<summary>
        ///Description de l'étape
        /// </summary>
        /// <remarks>
        /// Il est recommandé à l'administrateur d'utiliser la description pour décrire le fonctionnement 
        /// de l'étape.
        /// <p>Une bonne documentation des étapes peut simplifier le travail de maintenance du paramétrage.</p>
        /// </remarks>
        [TableFieldProperty(c_champDescription, 1024)]
        [DynamicField("Field description")]
		public string Description
		{
			get
			{
				return (string)Row[c_champDescription];
			}
			set
			{
				Row[c_champDescription] = value;
			}
		}


        //-------------------------------------------------------------------
        /// <summary>
        /// Identifie le type de workflow auquel appartient l'étape.
        /// </summary>
        [Relation(
            CTypeWorkflow.c_nomTable,
            CTypeWorkflow.c_champId,
            CTypeWorkflow.c_champId,
            true,
            true,
            false)]
        [DynamicField("Workflow type")]
        public CTypeWorkflow Workflow
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
        /// Liste les liens sortant de cette étape
        /// </summary>
        [RelationFille(typeof(CLienEtapesWorkflow), "EtapeSource")]
        [DynamicChilds("Output Links", typeof(CLienEtapesWorkflow))]
        public CListeObjetsDonnees LiensSortants
        {
            get
            {
                return GetDependancesListe(CLienEtapesWorkflow.c_nomTable, CLienEtapesWorkflow.c_champIdEtapeSource);
            }
        }

        
        //---------------------------------------------
        /// <summary>
        /// Liste les liens entrant dans cette étape
        /// </summary>
        [RelationFille(typeof(CLienEtapesWorkflow), "EtapeDestination")]
        [DynamicChilds("Input Links", typeof(CLienEtapesWorkflow))]
        public CListeObjetsDonnees LiensEntrants
        {
            get
            {
                return GetDependancesListe(CLienEtapesWorkflow.c_nomTable, CLienEtapesWorkflow.c_champIdEtapeDestination);
            }
        }

        //---------------------------------------------
        /// <summary>
        /// Liste des liens aux champs personnalisés définis par ce type d'étape de workflow
        /// </summary>
        [RelationFille(typeof(CRelationTypeEtapeWorkflow_ChampCustom), "Definisseur")]
        [DynamicChilds("Custom fields relations", typeof(CRelationTypeEtapeWorkflow_ChampCustom))]
        public CListeObjetsDonnees RelationsChampsCustomListe
        {
            get
            {
                return GetDependancesListe(CRelationTypeEtapeWorkflow_ChampCustom.c_nomTable, c_champId);
            }
        }


        //---------------------------------------------
        /// <summary>
        /// Liste des liens aux formulaires définis par ce type d'étape de workflow
        /// </summary>
        [RelationFille(typeof(CRelationTypeEtapeWorkflow_Formulaire), "Definisseur")]
        [DynamicChilds("Forms list", typeof(CRelationTypeEtapeWorkflow_Formulaire))]
        public CListeObjetsDonnees RelationsFormulairesListe
        {
            get
            {
                return GetDependancesListe(CRelationTypeEtapeWorkflow_Formulaire.c_nomTable, c_champId);
            }
        }


        //---------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        [RelationFille(typeof(CEtapeWorkflow), "TypeEtape")]
        [DynamicChilds("Workflow steps", typeof(CEtapeWorkflow))]
        public CListeObjetsDonnees Etapes
        {
            get
            {
                return GetDependancesListe(CEtapeWorkflow.c_nomTable, c_champId);
            }
        }


        //---------------------------------------------
        public CRoleChampCustom RoleChampCustomDesElementsAChamp
        {
            get { return CRoleChampCustom.GetRole(CEtapeWorkflow.c_roleChampCustom); }
        }

        //---------------------------------------------
        public IRelationDefinisseurChamp_ChampCustom[] RelationsChampsCustomDefinis
        {
            get
            {
                return (IRelationDefinisseurChamp_ChampCustom[])RelationsChampsCustomListe.ToArray(typeof(IRelationDefinisseurChamp_ChampCustom));
            }
        }

        //---------------------------------------------
        public IRelationDefinisseurChamp_Formulaire[] RelationsFormulaires
        {
            get
            {
                return (IRelationDefinisseurChamp_Formulaire[])RelationsFormulairesListe.ToArray(typeof(IRelationDefinisseurChamp_Formulaire));
            }
        }

        /// /////////////////////////////////////////////
        public CChampCustom[] TousLesChampsAssocies
        {
            get
            {
                Hashtable tableChamps = new Hashtable();
                FillHashtableChamps(tableChamps);
                CChampCustom[] liste = new CChampCustom[tableChamps.Count];
                int nChamp = 0;
                foreach (CChampCustom champ in tableChamps.Values)
                    liste[nChamp++] = champ;
                return liste;
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Remplit une hashtable IdChamp->Champ
        /// avec tous les champs liés.(hiérarchique)
        /// </summary>
        /// <param name="tableChamps">HAshtable à remplir</param>
        private void FillHashtableChamps(Hashtable tableChamps)
        {
            foreach (IRelationDefinisseurChamp_ChampCustom relation in RelationsChampsCustomDefinis)
                tableChamps[relation.ChampCustom.Id] = relation.ChampCustom;
            foreach (IRelationDefinisseurChamp_Formulaire relation in RelationsFormulaires)
            {
                foreach (CRelationFormulaireChampCustom relFor in relation.Formulaire.RelationsChamps)
                    tableChamps[relFor.Champ.Id] = relFor.Champ;
            }
        }

        //-------------------------------------------------------------------
        /// /////////////////////////////////////////////////////////
        [TableFieldProperty(c_champDataInitialisations, NullAutorise = true)]
        public CDonneeBinaireInRow DataInitialisations
        {
            get
            {
                if (Row[c_champDataInitialisations] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champDataInitialisations);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataInitialisations, donnee);
                }
                object obj = Row[c_champDataInitialisations];
                return ((CDonneeBinaireInRow)obj).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champDataInitialisations] = value;
            }
        }

        //-------------------------------------------------------------------
        [DynamicField("Step Initialisation Settings")]
        [BlobDecoder]
        public CParametresInitialisationEtape ParametresInitialisation
        {
            get
            {
                CParametresInitialisationEtape parametre = null;
                CDonneeBinaireInRow donnee = DataInitialisations;
                if (donnee != null && donnee.Donnees != null)
                {
                    MemoryStream stream = new MemoryStream(donnee.Donnees);
                    BinaryReader reader = new BinaryReader(stream);
                    CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                    CResultAErreur result = serializer.TraiteObject<CParametresInitialisationEtape>(ref parametre);
                    reader.Close();
                    stream.Close();
                    stream.Dispose();
                }
                if (parametre == null)
                    parametre = new CParametresInitialisationEtape();

                return parametre;
            }
            set
            {
                if (value == null)
                {
                    CDonneeBinaireInRow donnee = DataInitialisations;
                    donnee.Donnees = null;
                    DataInitialisations = donnee;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    CParametresInitialisationEtape parametre = value;
                    CResultAErreur result = serializer.TraiteObject<CParametresInitialisationEtape>(ref parametre);
                    if (result)
                    {
                        CDonneeBinaireInRow donnee = DataInitialisations;
                        donnee.Donnees = stream.GetBuffer();
                        DataInitialisations = donnee;
                    }
                    stream.Close();
                    writer.Close();
                    stream.Dispose();
                }
            }
        }


        //-------------------------------------------------------------------
        /// /////////////////////////////////////////////////////////
        [TableFieldProperty(c_champDataBloc, NullAutorise = true)]
        public CDonneeBinaireInRow DataBloc
        {
            get
            {
                if (Row[c_champDataBloc] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champDataBloc);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataBloc, donnee);
                }
                object obj = Row[c_champDataBloc];
                return ((CDonneeBinaireInRow)obj).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champDataBloc] = value;
            }
        }

        //-------------------------------------------------------------------
        [TableFieldProperty(c_champCacheBloc, IsInDb = false)]
        [NonCloneable]
        [DynamicField("Workflow Bloc")]
        [BlobDecoder]
        public CBlocWorkflow Bloc
        {
            get
            {
                CBlocWorkflow bloc = Row[c_champCacheBloc] as CBlocWorkflow;
                if (bloc == null)
                {
                    CDonneeBinaireInRow donnee = DataBloc;
                    if (donnee != null && donnee.Donnees != null)
                    {
                        MemoryStream stream = new MemoryStream(donnee.Donnees);
                        BinaryReader reader = new BinaryReader(stream);
                        CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                        serializer.AttacheObjet(typeof(CContexteDonnee), ContexteDonnee);
                        CResultAErreur result = serializer.TraiteObject<CBlocWorkflow>(ref bloc, this);
                        serializer.DetacheObjet(typeof(CContexteDonnee), ContexteDonnee);
                        reader.Close();
                        stream.Close();
                        stream.Dispose();
                    }
                    if (bloc == null)
                        bloc = new CBlocWorkflowFormulaire(this);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champCacheBloc, bloc);
                }
                bloc.TypeEtape = this;
                return bloc;
            }
            set
            {
                if (value == null)
                {
                    CDonneeBinaireInRow donnee = DataBloc;
                    donnee.Donnees = null;
                    DataBloc = donnee;
                    Row[c_champStepType] = "";
                    HasUserInterface = false;

                }
                else
                {
                    Row[c_champStepType] = value.BlocTypeCode;
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    CBlocWorkflow dessin = value;
                    CResultAErreur result = serializer.TraiteObject<CBlocWorkflow>(ref dessin, this);
                    if (result)
                    {
                        CDonneeBinaireInRow donnee = DataBloc;
                        donnee.Donnees = stream.GetBuffer();
                        DataBloc = donnee;
                    }
                    stream.Close();
                    writer.Close();
                    stream.Dispose();
                    HasUserInterface = value.IsBlocAInterfaceUtilisateur;
                }
                CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champCacheBloc, DBNull.Value);
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Retourne le type d'étape dont il s'agit (condition, ...)
        /// </summary>
        [TableFieldProperty(c_champStepType, 64)]
        [DynamicField("Kind of step")]
        public string TypeBlocCode
        {
            get
            {
                return (string)Row[c_champStepType];
            }
        }


        
        //-----------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        [TableFieldProperty(c_champModeAsynchrone)]
        [DynamicField("Asynchronous mode")]
        public bool ModeAsynchrone
        {
            get
            {
                return (bool)Row[c_champModeAsynchrone];
            }
            set
            {
                Row[c_champModeAsynchrone] = value;
            }
        }

        [DynamicField("Possible return codes")]
        public string[] PossibleReturnCodes
        {
            get
            {
                if (Bloc != null)
                    return Bloc.CodesRetourPossibles;
                return new string[0];
            }
        }


        //---------------------------------------------------------
        public Type[] TypesCibleEvenement
        {
            get { return new Type[] { typeof(CEtapeWorkflow) }; }
        }

        //---------------------------------------------------------
        public CListeObjetsDonnees Evenements
        {
            get { return CUtilDefinisseurEvenement.GetEvenementsFor(this); }
        }

        //---------------------------------------------------------
        public CComportementGenerique[] ComportementsInduits
        {
            get { return CUtilDefinisseurEvenement.GetComportementsInduits(this); }
        }

        //-------------------------------------------------------------------
        public static void Autoexec()
        {
            CRoleChampCustom.RegisterRole(c_roleChampCustom, I.T("Workflow Step Type|10002"), typeof(CTypeEtapeWorkflow), typeof(CDefinisseurChampsPourTypeSansDefinisseur));
        }

        //-------------------------------------------------------------------
        public override CRoleChampCustom RoleChampCustomAssocie
        {
            get
            {
                return CRoleChampCustom.GetRole(CTypeEtapeWorkflow.c_roleChampCustom);
            }
        }

        //-------------------------------------------------------------------
        public override CRelationElementAChamp_ChampCustom GetNewRelationToChamp()
        {
            return new CRelationTypeEtapeWorkflow_ChampCustomValeur(ContexteDonnee);
        }

        //-------------------------------------------------------------------
        public override IDefinisseurChampCustom[] DefinisseursDeChamps
        {
            get
            {
                return new IDefinisseurChampCustom[]{
                    new CDefinisseurChampsPourTypeSansDefinisseur(
                        ContexteDonnee, 
                        CRoleChampCustom.GetRole(CTypeEtapeWorkflow.c_roleChampCustom) )};
            }
        }

        //-------------------------------------------------------------------
        [RelationFille(typeof(CRelationTypeEtapeWorkflow_ChampCustomValeur), "ElementAChamps")]
        public override CListeObjetsDonnees RelationsChampsCustom
        {
            get
            {
                return GetDependancesListe(CRelationTypeEtapeWorkflow_ChampCustomValeur.c_nomTable, c_champId);
            }
        }
    }
		
}
