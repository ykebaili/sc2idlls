using System;
using System.Data;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.multitiers.client;
using sc2i.expression;

namespace sc2i.data.dynamic
{
    /// <summary>
    /// Un Groupe de paramétrage est un objet permettant de classer, regrouper,
    /// certains éléments de paramétrage par affinités (exemple : Application Web...)
    /// </summary>
	[DynamicClass("Parameter setting group")]
    [Table(CGroupeParametrage.c_nomTable, CGroupeParametrage.c_champId, false)]
    [ObjetServeurURI("CGroupeParametrageServeur")]
    public class CGroupeParametrage : CObjetDonneeAIdNumeriqueAuto
    {
        public const string c_nomTable = "SETTING_GROUPE";
        public const string c_champId = "SETGRP_ID";
        public const string c_champLibelle = "SETGRP_LABEL";
        public const string c_champDescription = "SETGRP_DESCRIPTION";
        public const string c_champCondition = "SETGRP_CONDITION";


        /// ////////////////////////////////////////////////
        public CGroupeParametrage(CContexteDonnee contexte)
            : base(contexte)
        {
        }

        /// ////////////////////////////////////////////////
        public CGroupeParametrage(DataRow row)
            : base(row)
        {
        }

        /// ////////////////////////////////////////////////
        public override string DescriptionElement
        {
            get
            {
                return I.T("The @1 parameter setting group|268", Libelle);
            }
        }

        /// ////////////////////////////////////////////////
        public override string[] GetChampsTriParDefaut()
        {
            return new string[] { c_champLibelle };
        }

        /// ////////////////////////////////////////////////
        protected override void MyInitValeurDefaut()
        {
        }

        //-------------------------------------------------
        /// <summary>
        /// Libellé du groupe de paramétrage
        /// </summary>
        [TableFieldProperty(c_champLibelle, 255)]
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

        //------------------------------------------------------
        /// <summary>
        /// Donne ou définit la description du process
        /// </summary>
		[TableFieldProperty(c_champDescription, 1024)]
        [DynamicField("Description")]
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

        /// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champCondition, 1024)]
        public string FormuleConditionString
        {
            get
            {
                return (string)Row[c_champCondition];
            }
            set
            {
                Row[c_champCondition] = value;
            }
        }

        /// /////////////////////////////////////////////////////////
        public C2iExpression FormuleCondition
        {
            get
            {
                C2iExpression expression = C2iExpression.FromPseudoCode(FormuleConditionString);
                if (expression == null)
                    expression = new C2iExpressionVrai();
                return expression;
            }
            set
            {
                FormuleConditionString = C2iExpression.GetPseudoCode(value);
            }
        }

        /*/-----------------------------------------------------------------------------------------------------------------------
        /// ////////////////////////////////////////////////
        /// <summary>
        /// Retourne la liste des Actions appartenant à ce groupe
        /// </summary>
        [RelationFille(typeof(CProcessInDb), "GroupeParametrage")]
        [DynamicChilds("Actions", typeof(CProcessInDb))]
        public CListeObjetsDonnees Actions
        {
            get
            {
                return GetDependancesListe(CProcessInDb.c_nomTable, c_champId);
            }
        }//*/

        /*/-----------------------------------------------------------------------------------------------------------------------
        /// ////////////////////////////////////////////////
        /// <summary>
        /// Retourne la liste des Evénements appartenant à ce groupe
        /// </summary>
        [RelationFille(typeof(CEvenement), "GroupeParametrage")]
        [DynamicChilds("Evenements", typeof(CEvenement))]
        public CListeObjetsDonnees Evenements
        {
            get
            {
                return GetDependancesListe(CEvenement.c_nomTable, c_champId);
            }
        }//*/

    }
}
