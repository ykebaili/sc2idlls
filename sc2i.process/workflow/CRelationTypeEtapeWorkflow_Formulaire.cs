using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.process.workflow
{
    /// <summary>
    /// Relation entre un <see cref="CTypeEtapeWorkflow">Type d'étape de workflow</see> et un
    /// <see cref="sc2i.data.dynamic.CFormulaire">Formulaire personnalisé</see>.
    /// </summary>
    [DynamicClass("Workflow step teype / Custom form")]
	[ObjetServeurURI("CRelationTypeEtapeWorkflow_FormulaireServeur")]
	[Table(CRelationTypeEtapeWorkflow_Formulaire.c_nomTable, CRelationTypeEtapeWorkflow_Formulaire.c_champId, true)]
	[FullTableSync]
	[Unique(false,
		"Another association already exist for the relation Workflow step Type/Custom Form|20064",
		CTypeEtapeWorkflow.c_champId,
		CFormulaire.c_champId)]
    public class CRelationTypeEtapeWorkflow_Formulaire : CRelationDefinisseurChamp_Formulaire
	{
        public const string c_nomTable = "WKFSTPTP_FORM";
        public const string c_champId = "WKFSTPTP_FORM_ID";


		//-------------------------------------------------------------------
		public CRelationTypeEtapeWorkflow_Formulaire(CContexteDonnee ctx)
			: base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CRelationTypeEtapeWorkflow_Formulaire(System.Data.DataRow row)
			: base(row)
		{
		}

		//-------------------------------------------------------------------
        /// <summary>
        /// Type de workflow objet de la relation
        /// </summary>
		[Relation(
			CTypeEtapeWorkflow.c_nomTable,
		   CTypeEtapeWorkflow.c_champId,
		   CTypeEtapeWorkflow.c_champId,
			true,
			true,
			true)]
		[DynamicField("Workflow step type")]
		public override IDefinisseurChampCustom Definisseur
		{
			get
			{
				return (IDefinisseurChampCustom)GetParent(CTypeEtapeWorkflow.c_champId, typeof(CTypeEtapeWorkflow));
			}
			set
			{
				SetParent(CTypeEtapeWorkflow.c_champId, (CTypeEtapeWorkflow)value);
			}
		}
	}
}
