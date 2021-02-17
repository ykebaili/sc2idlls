using System;
using System.Data;
using System.Collections;

using sc2i.common;
using sc2i.data;
using sc2i.expression;
using sc2i.common.recherche;

namespace sc2i.process.workflow
{
	/// <summary>
	/// Repr�sente un lien entre deux �tapes de workflow
	/// </summary>
    /// <remarks>
    /// Chaque lien est caract�ris� par une �tape de sortie (celle d'o� sort
    /// le lien, et une �tape d'entr�e (celle vers laquelle va le lien).
    /// <BR>
    /// </BR>
    /// Durant l'ex�cution du workflow, en fin d'une �tape, le syst�me
    /// scrute toutes les �tapes de workflow et ex�cute celles pour
    /// lesquelles la condition d'ex�cution est "vrai" (true)
    /// </remarks>
	[Table(CLienEtapesWorkflow.c_nomTable, CLienEtapesWorkflow.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CLienEtapesWorkflowServeur")]
	[DynamicClass("Workflow step link")]
	public class CLienEtapesWorkflow : 
        CObjetDonneeAIdNumeriqueAuto
	{
		public const string c_nomTable = "WORKFLOW_STEP_LINK";
		public const string c_champId = "WKFSTPLNK_ID";
        public const string c_champLibelle = "WKFSTPLNK_LABEL";
        public const string c_champIdEtapeSource = "WKFSTPLNK_SRC_ID";
        public const string c_champIdEtapeDestination = "WKFSTPLNK_DST_ID";
        public const string c_champActivationCode = "WKFSTP_ACTIVATION_CODE";

        public const string c_champFormuleActivation = "WKFSTPLNK_FORMULA";

#if PDA
		/// ///////////////////////////////////////////////////////
		public CLienEtapesWorkflow(  )
			:base()
		{
		}
#endif
		/// <summary>
		/// //////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CLienEtapesWorkflow( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// //////////////////////////////////////////////////
		public CLienEtapesWorkflow ( DataRow row )
			:base(row)
		{
		}

        /// //////////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
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

		/// //////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
                return I.T("Workflow step link from @1 to @2|20059",
                    EtapeSource == null ? "" : EtapeSource.Libelle,
                    EtapeDestination == null ? "" : EtapeDestination.Libelle);
			}
		}

		/// //////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champId};
		}



        //-----------------------------------------------------------
        /// <summary>
        /// Libell� du lien
        /// </summary>
        [TableFieldProperty(c_champLibelle, 255)]
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


        //-------------------------------------------------------------------
        /// <summary>
        /// Identifie le workflow auquel appartient le lien.
        /// </summary>
        [Relation(
            CTypeWorkflow.c_nomTable,
            CTypeWorkflow.c_champId,
            CTypeWorkflow.c_champId,
            true,
            true,
            false)]
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

        //-------------------------------------------------------------------
        /// <summary>
        /// Etape de laquelle "sort" le lien
        /// </summary>
        [Relation(
            CTypeEtapeWorkflow.c_nomTable,
            CTypeEtapeWorkflow.c_champId,
            c_champIdEtapeSource,
            true,
            true,
            true)]
        [DynamicField("Source step")]
        public CTypeEtapeWorkflow EtapeSource
        {
            get
            {
                return (CTypeEtapeWorkflow)GetParent(c_champIdEtapeSource, typeof(CTypeEtapeWorkflow));
            }
            set
            {
                SetParent(c_champIdEtapeSource, value);
            }
        }


        //-------------------------------------------------------------------
        /// <summary>
        /// Etape dans laquelle "entre" le lien
        /// </summary>
        [Relation(
            CTypeEtapeWorkflow.c_nomTable,
            CTypeEtapeWorkflow.c_champId,
            c_champIdEtapeDestination,
            true,
            true,
            true)]
        [DynamicField("Destination step")]
        public CTypeEtapeWorkflow EtapeDestination
        {
            get
            {
                return (CTypeEtapeWorkflow)GetParent(c_champIdEtapeDestination, typeof(CTypeEtapeWorkflow));
            }
            set
            {
                SetParent(c_champIdEtapeDestination, value);
            }
        }

        /// //////////////////////////////////////////////////
        [TableFieldProperty(c_champFormuleActivation, 4000, IsLongString=true)]
        public string FormuleString
        {
            get
            {
                return (string)Row[c_champFormuleActivation];
            }
            set
            {
                Row[c_champFormuleActivation] = value;
            }
        }

        //-------------------------------------------------------------------
        public C2iExpression Formule
        {
            get
            {
                C2iExpression expression = C2iExpression.FromPseudoCode(FormuleString);
                if (expression == null)
                    expression = new C2iExpressionVrai();
                return expression;
            }
            set
            {
                if (value == null)
                    FormuleString = "";
                else
                    FormuleString = C2iExpression.GetPseudoCode(value);
            }
        }


        
        //-----------------------------------------------------------
        /// <summary>
        /// Code d'activation : chaque �tape retourne au moins un code de retour. Seuls les liens
        /// associ�s � ce code d'activation seront activ�s.
        /// </summary>
        /// <remarks>
        /// Si le code d'activation d'un lien est vide, il sera toujours activ�
        /// </remarks>
        [TableFieldProperty(c_champActivationCode, 255)]
        [DynamicField("Activation code")]
        public string ActivationCode
        {
            get
            {
                return (string)Row[c_champActivationCode];
            }
            set
            {
                Row[c_champActivationCode] = value;
            }
        }


	


	


        

	


       
    }
		
}
