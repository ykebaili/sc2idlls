using System;
using System.Data;

using sc2i.common;
using sc2i.data;


namespace sc2i.data.dynamic
{
	/// <summary>
	/// Lien entre un formulaire et un champ personnalisé
	/// </summary>
    /// <remarks>
    /// Seuls les champs personnalisés utilisée par un formulaire via une zone "DATA" utilisent
    /// cette entité pour être stockés.
    /// </remarks>
	[Table(CRelationFormulaireChampCustom.c_nomTable, CRelationFormulaireChampCustom.c_champId, true)]
	[ObjetServeurURI("CRelationFormulaireChampCustomServeur")]
	[FullTableSync]
    [DynamicClass("Form / Custom field")]
	public class CRelationFormulaireChampCustom : CObjetDonneeAIdNumeriqueAuto, IObjetALectureTableComplete
	{
		public const string c_nomTable = "FORM_FIELD";
		public const string c_champId = "FORM_FLD_ID";
		public const string c_champNumPDA = "FORM_FLD_PDA_NUM";
		public const string c_champLibellePDA = "FORM_FLD_PDA_LABEL";

#if PDA
		/// ///////////////////////////////////
		public CRelationFormulaireChampCustom()
			:base()
		{
		}
#endif
		/// ///////////////////////////////////
		public CRelationFormulaireChampCustom( CContexteDonnee contexte)
			:base ( contexte )
		{
		}

		/// ///////////////////////////////////
		public CRelationFormulaireChampCustom( DataRow row )
			:base ( row )
		{
		}

		/// ///////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Relation between the form @1 and the custom field @2|191",Formulaire.DescriptionElement,Champ.DescriptionElement);
			}
		}

		/// ///////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champNumPDA};
		}
		
		/// ///////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// ///////////////////////////////////
		///<summary>
        ///Formulaire lié
        /// </summary>
        [Relation(CFormulaire.c_nomTable, CFormulaire.c_champId, CFormulaire.c_champId, true, true)]
		[DynamicField("Form")]
        public CFormulaire Formulaire
		{
			get
			{
				return (CFormulaire)GetParent(CFormulaire.c_champId, typeof(CFormulaire));
			}
			set
			{
				SetParent ( CFormulaire.c_champId, value );
			}
		}

		/// ///////////////////////////////////
		///<summary>
        ///Champ personnalisé lié
        /// </summary>
        [Relation(CChampCustom.c_nomTable, CChampCustom.c_champId, CChampCustom.c_champId, true, false)]
		[DynamicField("Custom field")]
        public CChampCustom Champ
		{
			get
			{
				return (CChampCustom)GetParent ( CChampCustom.c_champId, typeof(CChampCustom));
			}
			set
			{
				SetParent ( CChampCustom.c_champId, value );
			}
		}

        /// ///////////////////////////////////
        [TableFieldProperty(c_champNumPDA)]
        [DynamicField("Web order number")]
		public int NumWeb
		{
			get
			{
				return (int)Row[c_champNumPDA];
			}
			set
			{
				Row[c_champNumPDA] = value;
			}
		}

        /// ///////////////////////////////////
        [TableFieldProperty(c_champLibellePDA, 255)]
        [DynamicField("Web label")]
        public string LibelleWeb
        {
            get
            {
                return (string)Row[c_champLibellePDA];
            }
            set
            {
                Row[c_champLibellePDA] = value;
            }
        }

    }
}
