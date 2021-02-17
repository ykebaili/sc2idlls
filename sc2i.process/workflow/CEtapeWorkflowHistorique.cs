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
using System.Text;

namespace sc2i.process.workflow
{
	/// <summary>
	/// Historique d'une étape de workflow
	/// </summary>
    /// <remarks>
    /// Chaque fois qu'une étape de workflow se termine, un entrée d'historique est créée.
    /// <BR></BR>
    /// Chaque entrée d'historique retrace 
    /// <LI>La date de début d'exécution de l'étape</LI>
    /// <LI>La date de fin d'exécution de l'étape</LI>
    /// <LI>L'id de l'utilisateur ayant déclenché le début de l'étape</LI>
    /// <LI>L'id de l'utilisateur ayant déclenché la fin de l'étape</LI>
    /// </remarks>
	[Table(CEtapeWorkflowHistorique.c_nomTable, CEtapeWorkflowHistorique.c_champId, true )]
	[FullTableSync]
    [ObjetServeurURI("CEtapeWorkflowHistoriqueServeur")]
	[DynamicClass("Workflow step history")]
	public class CEtapeWorkflowHistorique :CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "WORKFLOW_STEP_HIST";
		public const string c_champId = "WKFSTPHST_ID";
        public const string c_champDateDebut = "WKFSTP_START_DATE";
        public const string c_champDateFin = "WKFSTP_END_DATE";
        public const string c_champKeyDemarréePar = "WKFSTP_STARTUSR_KEY";
        public const string c_champKeyTerminéePar = "WKFSTP_ENDUSR_KEY";
        public const string c_champCodesRetour = "WKFSTP_RETURN_CODES";
        public const string c_champEtat = "WKFSTP_STATE_CODE";
        public const string c_champIdEtapeAppelante = "WKFSTP_CALLING_STEP";
        public const string c_champRunGeneration = "WKFSTP_RUN_GENERATION";


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
		public CEtapeWorkflowHistorique( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CEtapeWorkflowHistorique ( DataRow row )
			:base(row)
		{
		}

        
		/// //////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
            DateDebut = DateTime.Now;
            DateFin = DateTime.Now;
		}

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Worfklow step history @1|20078",
                    Etape != null?Etape.Libelle:"");
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}

        /// //////////////////////////////////////////////////
        //-----------------------------------------------------------
        /// <summary>
        /// Date de démarrage de l'étape
        /// </summary>
        [TableFieldProperty(c_champDateDebut)]
        [DynamicField("Start date")]
        public DateTime DateDebut
        {
            get
            {
                return (DateTime)Row[c_champDateDebut];
            }
            set
            {
                   Row[c_champDateDebut] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Date de fin de l'étape
        /// </summary>
        [TableFieldProperty(c_champDateFin)]
        [DynamicField("End date")]
        public DateTime DateFin
        {
            get
            {
                return (DateTime)Row[c_champDateFin];
            }
            set
            {
                Row[c_champDateFin] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Indique l'id de l'utilisateur ayant déclenché le démarrage de l'étape
        /// <BR></BR>
        /// Une valeur nulle indique que l'id du créateur de l'étape est inconnu
        /// </summary>
        [TableFieldProperty(c_champKeyDemarréePar, 64)]
        [ReplaceField("IdDémarreur","Started by Id")]
        [DynamicField("Started by key string")]
        public string KeyDémarreurString
        {
            get
            {
                return Row.Get<string>(c_champKeyDemarréePar);
            }
            set
            {
                Row[c_champKeyDemarréePar, true] = value;
            }
        }

        //-----------------------------------------------------------
        [DynamicField("Started by key")]
        public CDbKey KeyDémarreur
        {
            get
            {
                return CDbKey.CreateFromStringValue(KeyDémarreurString);
            }
            set
            {
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
                return Row.Get<string>(c_champKeyTerminéePar);
            }
            set
            {
                Row[c_champKeyTerminéePar, true] = value;
            }
        }


        //-----------------------------------------------------------
        [DynamicField("Ended by key")]
        public CDbKey KeyTermineur
        {
            get
            {
                return CDbKey.CreateFromStringValue(KeyTermineurString);
            }
            set
            {
                if (value != null)
                    KeyTermineurString = value.StringValue;
                else
                    KeyTermineurString = "";
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Etape concernée par cette entrée d'historique
        /// </summary>
        [Relation(
            CEtapeWorkflow.c_nomTable,
            CEtapeWorkflow.c_champId,
            CEtapeWorkflow.c_champId,
            true,
            true,
            true)]
        [DynamicField("Step")]
        public CEtapeWorkflow Etape
        {
            get
            {
                return (CEtapeWorkflow)GetParent(CEtapeWorkflow.c_champId, typeof(CEtapeWorkflow));
            }
            set
            {
                SetParent(CEtapeWorkflow.c_champId, value);
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
                Row[c_champCodesRetour] = value;
            }
        }

        /// <summary>
        /// Liste des codes retour
        /// </summary>
        [DynamicField("Return codes array")]
        public string[] CodesRetour
        {
            get
            {
                return CEtapeWorkflow.CodesRetourFromString(CodesRetourString);
            }
            set
            {
                CodesRetourString = CEtapeWorkflow.StringFromCodesRetour(value);
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

        //-------------------------------------------------------------------
        /// <summary>
        /// Etape ayant lancé cette étape
        /// </summary>
        [Relation(
            CEtapeWorkflow.c_nomTable,
            CEtapeWorkflow.c_champId,
            c_champIdEtapeAppelante,
            false,
            false,
            false)]
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


	




        


        
    }
		
}
