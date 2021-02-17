using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sc2i.process.workflow.gels
{
    /// <summary>
    /// Ce sont les causes de gel possibles pour une <see cref="CPhaseTicket"> Phase de ticket </see>ou
    ///  pour une <see cref="CIntervention">Intervention</see>.
    /// </summary>
    /// <remarks>Lorsqu'une Phase de Ticket ou une Intervention est gelée, il est obligatoire de saisir
    /// une cause de gel</remarks>
    [DynamicClass("Freezing cause")]
    [Table(CCauseGel.c_nomTable, CCauseGel.c_champId, true)]
    [FullTableSync]
    [ObjetServeurURI("CCauseGelServeur")]
    [Unique(false, "Ce Libellé de Cause de Gel est déjà utilisé", CCauseGel.c_champLibelle)]
    [ReplaceClass("timos.data.CCauseGel")]
    public class CCauseGel : CObjetDonneeAIdNumeriqueAuto, IObjetALectureTableComplete
    {
        public const string c_nomTiag = "Freezing cause";
        public const string c_nomTable = "FREEZ_CAUSE";

		public const string c_champId = "FREEZ_CAUSE_ID";
		public const string c_champLibelle = "FREEZ_CAUSE_LABEL";
		public const string c_champCode = "FREEZ_CAUSE_CODE";

        /// /////////////////////////////////////////////
        public CCauseGel(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        /// /////////////////////////////////////////////
        public CCauseGel(DataRow row)
            : base(row)
        {
        }

        /// /////////////////////////////////////////////
        public override string DescriptionElement
        {
            get
            {
                return I.T( "Freezing Cause @1|10003",Libelle);
            }
        }

        /// /////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
        }

        /// /////////////////////////////////////////////
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champLibelle };
        }


        /// <summary>
        /// Le libellé de la Cause de gel<br/>(obligatoire)
        /// </summary>
        [TableFieldProperty(c_champLibelle, 100)]
        [DynamicField("Label")]
        [DescriptionField]
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
        /// Le code de la cause de gel<br/>Ce champ n'est pas obligatoire
        /// </summary>
        [TableFieldProperty(c_champCode, 100)]
        [DynamicField("Code")]
        public string Code
        {
            get
            {
                return (string)Row[c_champCode];
            }
            set
            {
                Row[c_champCode] = value;
            }
        }

	}
}
