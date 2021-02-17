using sc2i.common;
using sc2i.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.process.workflow.gels
{
    [DynamicClass("Workflow Step Freez")]
    [Table(CGelEtapeWorkflow.c_nomTable, CGelEtapeWorkflow.c_champId, true)]
    [FullTableSync]
    [ObjetServeurURI("CGelEtapeWorkflowServeur")]
    public class CGelEtapeWorkflow : CObjetDonneeAIdNumeriqueAuto
    {
        public const string c_nomTable = "WORKFLOW_STEP_FREEZ";
        public const string c_champId = "WKFSTEPFREEZ_ID";
        public const string c_champInfosDebutGel = "WKFSTEPFREEZ_START_INFOS";
        public const string c_champInfosFinGel = "WKFSTEPFREEZ_END_INFOS";
        public const string c_champDateDebut = "WKFSTEPFREEZ_START_DATE";
        public const string c_champDateFin = "WKFSTEPFREEZ_END_DATE";
        public const string c_champIdResponsableGel = "FREEZ_START_RESP_ID";
        public const string c_champIdResponsableDeGel = "FREEZ_END_RESP_ID";

        /// /////////////////////////////////////////////
        public CGelEtapeWorkflow(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        /// /////////////////////////////////////////////
        public CGelEtapeWorkflow(DataRow row)
            : base(row)
        {
        }

        /// /////////////////////////////////////////////
        public override string DescriptionElement
        {
            get
            {
                return I.T("Freezing from @1 to @2|30067", DateDebutString, DateFinString);
            }
        }

        /// /////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
        }

        /// /////////////////////////////////////////////
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champDateDebut };
        }

        /////////////////////////////////////////////
        /// <summary>
        /// Information textuelle sur le début de la période de Gel.<br/>
        /// Indique en général le motif pour lequel il y a eu gel.
        /// </summary>
        [TableFieldProperty(c_champInfosDebutGel, 1024)]
        [DynamicField("Freezing start info")]
        public string InfosDebutGel
        {
            get
            {
                return (string)Row[c_champInfosDebutGel];
            }
            set
            {
                Row[c_champInfosDebutGel] = value;
            }
        }

        ////////////////////////////////////////////////
        /// <summary>
        /// Information textuelle sur la fin de la période de Gel
        /// </summary>
        [TableFieldProperty(c_champInfosFinGel, 1024)]
        [DynamicField("Freezing end info")]
        public string InfosFinGel
        {
            get
            {
                return (string)Row[c_champInfosFinGel];
            }
            set
            {
                Row[c_champInfosFinGel] = value;
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Indique la date et l'heure à laquelle La Phase de Ticket ou l'Intervention a été gelée.
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
        public string DateDebutString
        {
            get
            {
                return DateDebut.ToString("g");
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Indique la date et l'heure à laquelle la Phase de Ticket ou l'Intervention a été dégelée.
        /// </summary>
        [TableFieldProperty(c_champDateFin, NullAutorise = true)]
        [DynamicField("End date")]
        public DateTime? DateFin
        {
            get
            {
                return (DateTime?)Row[c_champDateFin, true];
            }
            set
            {
                Row[c_champDateFin, true] = value;
            }
        }

        //---------------------------------------------------------------
        public string DateFinString
        {
            get
            {
                if (DateFin == null)
                    return "";
                return ((DateTime)DateFin).ToString("g");
            }
        }

        //-----------------------------------------------------------------
        [TableFieldProperty(c_champIdResponsableGel, 1024)]
        [DynamicField("Freezing start responsible Id")]
        public string IdResponsableDebutGel
        {
            get
            {
                return (string)Row[c_champIdResponsableGel];
            }
            set
            {
                Row[c_champIdResponsableGel] = value;
            }
        }

        //---------------------------------------------------------------
        public CDbKey KeyResponsabelDebutGel
        {
            get
            {
                return CDbKey.CreateFromStringValue(IdResponsableDebutGel);
            }
            set
            {
                IdResponsableDebutGel = value.StringValue;
            }
        }


        //-----------------------------------------------------------------
        [TableFieldProperty(c_champIdResponsableDeGel, 1024)]
        [DynamicField("Freezing end responsible Id")]
        public string IdResponsableFinGel
        {
            get
            {
                return (string)Row[c_champIdResponsableDeGel];
            }
            set
            {
                Row[c_champIdResponsableDeGel] = value;
            }
        }

        //---------------------------------------------------------------
        public CDbKey KeyResponsabelFinGel
        {
            get
            {
                return CDbKey.CreateFromStringValue(IdResponsableFinGel);
            }
            set
            {
                IdResponsableFinGel = value.StringValue;
            }
        }

        //-----------------------------------------------------------------
        /// <summary>
        /// Obtient ou définit la <see cref="CCauseGel">cause du gel</see> <br/> (obligatoire)
        /// </summary>
        [Relation(
            CCauseGel.c_nomTable,
            CCauseGel.c_champId,
            CCauseGel.c_champId,
            true,
            false,
            false)]
        [DynamicField("Freezing cause")]
        public CCauseGel CauseGel
        {
            get
            {
                return (CCauseGel)GetParent(CCauseGel.c_champId, typeof(CCauseGel));
            }
            set
            {
                SetParent(CCauseGel.c_champId, value);
            }
        }

        //-------------------------------------------------------------------
        /// <summary>
        /// Obtient ou définit l'étape de Workflow concernée par le gel.
        /// </summary>
        [Relation(
            CEtapeWorkflow.c_nomTable,
            CEtapeWorkflow.c_champId,
            CEtapeWorkflow.c_champId,
            false,
            true,
            false)]
        [DynamicField("Workflow Step")]
        public CEtapeWorkflow EtapeWorkflow
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



    }
}
