using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sc2i.data;
using sc2i.common;
using System.Data;

namespace sc2i.process.Mail
{
    [Table(CRegleMail.c_nomTable, CRegleMail.c_champId, true)]
    [ObjetServeurURI("CRegleMailServeur")]
    [DynamicClass("Mail Rule")]
    public class CRegleMail : CObjetDonneeAIdNumeriqueAuto
    {
        public const string c_nomTable = "MAIL_RULE";
        public const string c_champId = "MAILRULE_ID";
        public const string c_champLibelle = "MAILRULE_LABEL";

        public CRegleMail(CContexteDonnee ctx)
            : base(ctx)
        {

        }

        public CRegleMail(DataRow row)
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
            get { return I.T("Mail Rule @1", Libelle); }
        }

        //-----------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        [TableFieldProperty(c_champLibelle, 200)]
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
        


    }
}
