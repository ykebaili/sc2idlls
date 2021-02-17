using System;

using sc2i.data;
using sc2i.common;
using sc2i.data.dynamic;

namespace sc2i.process.workflow
{
    /// <summary>
    /// Relation entre un <see cref="CTypeWorkflow">Type de workflow</see> et un
    /// <see cref="sc2i.data.dynamic.CFormulaire">Formulaire personnalisé</see>.
    /// </summary>
    [DynamicClass("Workflow type / Custom form")]
	[ObjetServeurURI("CRelationTypeWorkflow_FormulaireServeur")]
	[Table(CRelationTypeWorkflow_Formulaire.c_nomTable, CRelationTypeWorkflow_Formulaire.c_champId, true)]
	[FullTableSync]
	[Unique(false,
		"Another association already exist for the relation Workflow Type/Custom Form|20063",
		CTypeWorkflow.c_champId,
		CFormulaire.c_champId)]
    public class CRelationTypeWorkflow_Formulaire : CRelationDefinisseurChamp_Formulaire
	{
        public const string c_nomTable = "WKFTP_FORM";
        public const string c_champId = "WKFTP_FORM_ID";


		//-------------------------------------------------------------------
		public CRelationTypeWorkflow_Formulaire(CContexteDonnee ctx)
			: base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CRelationTypeWorkflow_Formulaire(System.Data.DataRow row)
			: base(row)
		{
		}

		//-------------------------------------------------------------------
        /// <summary>
        /// Type de workflow objet de la relation
        /// </summary>
		[Relation(
			CTypeWorkflow.c_nomTable,
		   CTypeWorkflow.c_champId,
		   CTypeWorkflow.c_champId,
			true,
			true,
			true)]
		[DynamicField("Workflow type")]
		public override IDefinisseurChampCustom Definisseur
		{
			get
			{
				return (IDefinisseurChampCustom)GetParent(CTypeWorkflow.c_champId, typeof(CTypeWorkflow));
			}
			set
			{
				SetParent(CTypeWorkflow.c_champId, (CTypeWorkflow)value);
			}
		}
	}
}
