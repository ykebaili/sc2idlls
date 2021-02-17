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
	/// Historique d'une �tape de workflow
	/// </summary>
    /// <remarks>
    /// Chaque fois qu'une �tape de workflow se termine, un entr�e d'historique est cr��e.
    /// <BR></BR>
    /// Chaque entr�e d'historique retrace 
    /// <LI>La date de d�but d'ex�cution de l'�tape</LI>
    /// <LI>La date de fin d'ex�cution de l'�tape</LI>
    /// <LI>L'id de l'utilisateur ayant d�clench� le d�but de l'�tape</LI>
    /// <LI>L'id de l'utilisateur ayant d�clench� la fin de l'�tape</LI>
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
        public const string c_champKeyDemarr�ePar = "WKFSTP_STARTUSR_KEY";
        public const string c_champKeyTermin�ePar = "WKFSTP_ENDUSR_KEY";
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
        /// Date de d�marrage de l'�tape
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
        /// Date de fin de l'�tape
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
        /// Indique l'id de l'utilisateur ayant d�clench� le d�marrage de l'�tape
        /// <BR></BR>
        /// Une valeur nulle indique que l'id du cr�ateur de l'�tape est inconnu
        /// </summary>
        [TableFieldProperty(c_champKeyDemarr�ePar, 64)]
        [ReplaceField("IdD�marreur","Started by Id")]
        [DynamicField("Started by key string")]
        public string KeyD�marreurString
        {
            get
            {
                return Row.Get<string>(c_champKeyDemarr�ePar);
            }
            set
            {
                Row[c_champKeyDemarr�ePar, true] = value;
            }
        }

        //-----------------------------------------------------------
        [DynamicField("Started by key")]
        public CDbKey KeyD�marreur
        {
            get
            {
                return CDbKey.CreateFromStringValue(KeyD�marreurString);
            }
            set
            {
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
                return Row.Get<string>(c_champKeyTermin�ePar);
            }
            set
            {
                Row[c_champKeyTermin�ePar, true] = value;
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
        /// Etape concern�e par cette entr�e d'historique
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

        //-------------------------------------------------------------------
        /// <summary>
        /// Etape ayant lanc� cette �tape
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


	




        


        
    }
		
}
