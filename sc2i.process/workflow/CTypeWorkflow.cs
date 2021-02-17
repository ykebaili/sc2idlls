using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.data.dynamic;
using sc2i.common.recherche;
using sc2i.process.workflow.dessin;
using System.IO;
using System.Collections.Generic;
using sc2i.process.workflow.blocs;

namespace sc2i.process.workflow
{
	/// <summary>
	/// Représente un workflow
	/// </summary>
    /// <remarks>
    /// Un type de workflow est la représentation d'un enchainement d'actions, réalisées
    /// par différents acteurs de l'application.<BR></BR>
    /// Chaque type workflow consiste en une succession de types d'étapes, chaque étape
    /// définissant les actions réalisées durant cette étape.
    /// <BR></BR>
    /// Lors de l'exécution d'un type de workflow, un élément de type 
    /// <see cref="CWorkflow">Workflow</see> est créé, cet élément
    /// suivra l'exécution du workflow.
    /// </remarks>
	[Table(CTypeWorkflow.c_nomTable, CTypeWorkflow.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CTypeWorkflowServeur")]
	[DynamicClass("Workflow type")]
    [Unique(false,
        "Workflow name should be unique|20058",
        CTypeWorkflow.c_champLibelle)]
	public class CTypeWorkflow : 
        CObjetDonneeAIdNumeriqueAuto, 
        IObjetALectureTableComplete,
        IDefinisseurChampCustomRelationObjetDonnee,
        IDefinisseurEvenements
	{
		public const string c_nomTable = "WORKFLOW_TYPE";
		public const string c_champId = "WKFTP_ID";
		public const string c_champLibelle = "WKFTP_NAME";
		public const string c_champDescription = "WKFTP_DESC";

        public const string c_champDessin = "WKFTP_DRAWING";

        public const string c_champCacheDessin = "WKFTP_CACHE_DRAWING";

#if PDA
		/// ///////////////////////////////////////////////////////
		public CTypeWorkflow(  )
			:base()
		{
		}
#endif
        /// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CTypeWorkflow( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CTypeWorkflow ( DataRow row )
			:base(row)
		{
		}

		/// //////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Worfklow type @1|20057",Libelle);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// //////////////////////////////////////////////////
		///<summary>
        ///Nom du Workfow. Ce nom doit être unique.
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

		/// //////////////////////////////////////////////////
		///<summary>
        ///Description du Worfklow
        /// </summary>
        /// <remarks>
        /// Il est recommandé à l'administrateur d'utiliser la description pour décrire le workflow
        /// ainsi que d'indiquer dans quel contexte il est utilisé.
        /// <p>Une bonne documentation des workflow peut simplifier le travail de maintenance du paramétrage.</p>
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

        /// //////////////////////////////////////////////////
        //---------------------------------------------
        /// <summary>
        /// Retourne la liste de toutes les étapes
        /// du workflow
        /// </summary>
        [RelationFille(typeof(CTypeEtapeWorkflow), "Workflow")]
        [DynamicChilds("Steps", typeof(CTypeEtapeWorkflow))]
        public CListeObjetsDonnees Etapes
        {
            get
            {
                return GetDependancesListe(CTypeEtapeWorkflow.c_nomTable, c_champId);
            }
        }


        //---------------------------------------------
        /// <summary>
        /// Retourne la liste des liens entre étapes pour ce workflow
        /// </summary>
        [RelationFille(typeof(CLienEtapesWorkflow), "TypeWorkflow")]
        [DynamicChilds("Steps links", typeof(CLienEtapesWorkflow))]
        public CListeObjetsDonnees LiensEtapes
        {
            get
            {
                return GetDependancesListe(CLienEtapesWorkflow.c_nomTable, c_champId);
            }
        }

		//---------------------------------------------
		/// <summary>
		/// Liste des liens aux champs personnalisés définis par ce type de workflow
		/// </summary>
        [RelationFille(typeof(CRelationTypeWorkflow_ChampCustom), "Definisseur")]
		[DynamicChilds("Custom fields relations",typeof(CRelationTypeWorkflow_ChampCustom))]
        public CListeObjetsDonnees RelationsChampsCustomListe
		{
			get
			{
				return GetDependancesListe(CRelationTypeWorkflow_ChampCustom.c_nomTable, c_champId);
			}
		}

		
		//---------------------------------------------
		/// <summary>
		/// Liste des liens aux formulaires définis par ce type de workflow
		/// </summary>
        [RelationFille(typeof(CRelationTypeWorkflow_Formulaire), "Definisseur")]
		[DynamicChilds("forms list",typeof(CRelationTypeWorkflow_Formulaire))]
		public CListeObjetsDonnees RelationsFormulairesListe
		{
			get
			{
				return GetDependancesListe(CRelationTypeWorkflow_Formulaire.c_nomTable, c_champId);
			}
		}


        //---------------------------------------------
        public CRoleChampCustom RoleChampCustomDesElementsAChamp
        {
            get { return CRoleChampCustom.GetRole(CWorkflow.c_roleChampCustom); }
        }

        //---------------------------------------------
        public IRelationDefinisseurChamp_ChampCustom[] RelationsChampsCustomDefinis
        {
            get {  
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
        [TableFieldProperty(c_champDessin, NullAutorise = true)]
        public CDonneeBinaireInRow DataDessin
        {
            get
            {
                if (Row[c_champDessin] == DBNull.Value)
                {
                    CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champDessin);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDessin, donnee);
                }
                object obj = Row[c_champDessin];
                return ((CDonneeBinaireInRow)obj).GetSafeForRow(Row.Row);
            }
            set
            {
                Row[c_champDessin] = value;
            }
        }

        //-------------------------------------------------------------------
        [TableFieldProperty(c_champCacheDessin, IsInDb = false)]
        [NonCloneable]
        [BlobDecoder]
        public CWorkflowDessin Dessin
        {
            get
            {
                CWorkflowDessin dessin = Row[c_champCacheDessin] as CWorkflowDessin;
                if (dessin == null)
                {
                    CDonneeBinaireInRow donnee = DataDessin;
                    if (donnee != null && donnee.Donnees != null)
                    {
                        MemoryStream stream = new MemoryStream(donnee.Donnees);
                        BinaryReader reader = new BinaryReader(stream);
                        CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
                        CResultAErreur result = serializer.TraiteObject<CWorkflowDessin>(ref dessin, this);
                        reader.Close();
                        stream.Close();
                        stream.Dispose();
                    }
                    if (dessin == null)
                        dessin = new CWorkflowDessin(this);
                    CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champCacheDessin, dessin);
                }
                dessin.TypeWorkflow = this;
                return dessin;
            }
            set
            {
                if (value == null)
                {
                    CDonneeBinaireInRow donnee = DataDessin;
                    donnee.Donnees = null;
                    DataDessin = donnee;
                }
                else
                {
                    MemoryStream stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
                    CWorkflowDessin dessin = value;
                    CResultAErreur result = serializer.TraiteObject<CWorkflowDessin>(ref dessin, this);
                    if (result)
                    {
                        CDonneeBinaireInRow donnee = DataDessin;
                        donnee.Donnees = stream.GetBuffer();
                        DataDessin = donnee;
                    }
                    stream.Close();
                    writer.Close();
                    stream.Dispose();
                }
                CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champCacheDessin, DBNull.Value);
            }
        }

        //-------------------------------------------------------------------
        public IEnumerable<CTypeEtapeWorkflow> EtapesNonDessinees
        {
            get
            {
                List<CTypeEtapeWorkflow> lst = Etapes.ToList<CTypeEtapeWorkflow>();
                CWorkflowDessin dessin = Dessin;
                foreach (CTypeEtapeWorkflow typeEtape in lst.ToArray())
                {
                    if (dessin.GetChild(typeEtape.IdUniversel) != null)
                        lst.Remove(typeEtape);
                }
                return lst.AsReadOnly();
            }
        }

        

        //-------------------------------------------------------------------
        [DynamicField("Default start step")]
        public CTypeEtapeWorkflow EtapeDemarrageDefaut
        {
            get
            {
                CListeObjetsDonnees lst = Etapes;
                lst.Filtre = new CFiltreData(CTypeEtapeWorkflow.c_champDefaultStart + "=@1", true);
                if (lst.Count > 0)
                    return lst[0] as CTypeEtapeWorkflow;
                return null;
            }
            set
            {
                foreach (CTypeEtapeWorkflow typeEtape in Etapes)
                {
                    if (typeEtape.Equals(value))
                        typeEtape.IsDefautStart = true;
                    else
                        typeEtape.IsDefautStart = false;
                }
            }
        }





        //------------------------------------------
        public Type[] TypesCibleEvenement
        {
            get { return new Type[]{typeof(CWorkflow)}; }
        }

        //------------------------------------------
        public CListeObjetsDonnees Evenements
        {
            get { return CUtilDefinisseurEvenement.GetEvenementsFor(this); }
        }

        //------------------------------------------
        public CComportementGenerique[] ComportementsInduits
        {
            get { return CUtilDefinisseurEvenement.GetComportementsInduits(this); }
        }
    }
		
}
