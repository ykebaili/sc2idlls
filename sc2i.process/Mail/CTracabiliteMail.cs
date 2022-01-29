using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;
using sc2i.common;
using System.Data;

namespace sc2i.process.Mail
{
    [Table(CTracabiliteMail.c_nomTable, CTracabiliteMail.c_champId, true)]
    [ObjetServeurURI("CTracabiliteMailServeur")]
    public class CTracabiliteMail : CObjetDonneeAIdNumeriqueAuto, IObjetSansVersion
    {
        public const string c_nomTable = "MAIL_TRACKING";
        public const string c_champId = "MAILTRACK_ID";
        public const string c_champMessageUid = "MAILTRACK_MESS_UID";
        public const string c_champDateReception = "MAILTRACK_DATE";

        public CTracabiliteMail(CContexteDonnee ctx)
            : base(ctx)
        {

        }

        public CTracabiliteMail(DataRow row)
            : base(row)
        {

        }


        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champId };
        }

        protected override void MyInitValeurDefaut()
        {
        }

        public override string DescriptionElement
        {
            get
            {
                return "Mail: " + MessageUid;
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Représente l'identifiant unique du Mail.
        /// Permet de savoir si le Mail récupéré du serveur a déjà été enregistré dans la base de données.
        /// </summary>
        [TableFieldProperty(c_champMessageUid, 200)]
        [DynamicField("Message Uid")]
        public string MessageUid
        {
            get
            {
                return (string)Row[c_champMessageUid];
            }
            set
            {
                Row[c_champMessageUid] = value;
            }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// Date de réception du Mail sur le Serveur de Mail
        /// </summary>
        [TableFieldProperty(c_champDateReception)]
        [DynamicField("Receive Date")]
        public DateTime DateReception
        {
            get
            {
                return (DateTime)Row[c_champDateReception];
            }
            set
            {
                Row[c_champDateReception] = value;
            }
        }
        
        //-------------------------------------------------------------------
        /// <summary>
        /// Obtient ou définit le <see cref="CCompteMail">Compte Mail<see/> associé à ce Mail
        /// </summary>
        [Relation(
            CCompteMail.c_nomTable,
            CCompteMail.c_champId,
            CCompteMail.c_champId,
            true,
            true,
            true)]
        [DynamicField("Mail Account")]
        public CCompteMail CompteMail
        {
            get
            {
                return (CCompteMail)GetParent(CCompteMail.c_champId, typeof(CCompteMail));
            }
            set
            {
                SetParent(CCompteMail.c_champId, value);
            }
        }

    }
}
