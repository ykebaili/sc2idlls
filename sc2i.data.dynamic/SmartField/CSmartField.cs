using System;
using System.Collections;
using System.Data;

using sc2i.data;
using sc2i.common;
using sc2i.expression;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Permet de définir un champ accessible facilement aux utilisateurs
	/// </summary>
    [DynamicClass("Smart field")]
	[Table(CSmartField.c_nomTable, CSmartField.c_champId, true )]
	[FullTableSync]
	[ObjetServeurURI("CSmartFieldServeur")]
    [Unique(false, "Smart field already exists", 
        CSmartField.c_champTypeCible,
        CSmartField.c_champLibelle)]
	public class CSmartField : CObjetDonneeAIdNumeriqueAuto,
		IObjetALectureTableComplete
	{
		public const string c_nomTable = "SMART_FIELD";
		public const string c_champId = "SMTLFD_ID";
        public const string c_champCategorie = "SMTFLT_CAT";
		public const string c_champLibelle = "SMTFLD_LABEL";
        public const string c_champTypeCible = "SMTFLD_TYPE";
        public const string c_champDefinition = "SMTFLD_DEF";

        public const string c_champCacheType = "SMTFLD_TYPE_CACHE";
        public const string c_champCacheDef = "SMTFLD_DEF_CACHE";

		/// /////////////////////////////////////////////
		public CSmartField( CContexteDonnee contexte)
			:base(contexte)
		{
		}
		
	/// /////////////////////////////////////////////
		public CSmartField(DataRow row )
			:base(row)
		{
		}

		/// /////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Smart field @1 for @2|20058",Libelle,
                    TypeCible != null ? DynamicClassAttribute.GetNomConvivial(TypeCible) : "?");
			}
		}

		/// /////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
		}

		/// /////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle, c_champTypeCible};
		}


		

		/// <summary>
		/// Le libellé (nom) du champ
		/// </summary>
		[TableFieldProperty(c_champLibelle, 50)]
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


        //-----------------------------------------------------------
        /// <summary>
        /// Catégorie du champ (permet de ranger les champs)
        /// </summary>
        /// <remarks>
        /// Si la catégorie contient le caractère '/', celui-ci est utilisé
        /// en tant que gestionnaire de hiérarchie de catégorie
        /// </remarks>
        [TableFieldProperty(c_champCategorie, 64)]
        [DynamicField("Category")]
        public string Categorie
        {
            get
            {
                return (string)Row[c_champCategorie];
            }
            set
            {
                Row[c_champCategorie] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Type système des éléments auquel s'applique ce champ
        /// </summary>
        [TableFieldProperty(c_champTypeCible, 255)]
        [DynamicField("Target type string")]
        public string TypeCibleString
        {
            get
            {
                return (string)Row[c_champTypeCible];
            }
            set
            {
                Row[c_champTypeCible] = value;
            }
        }

        //-----------------------------------------------------------
        [TableFieldProperty(c_champCacheType, IsInDb = false, NullAutorise = true)]
        public Type TypeCible
        {
            get
            {
                Type tp = Row[c_champCacheType] as Type;
                if (tp == null)
                {
                    tp = CActivatorSurChaine.GetType(TypeCibleString);
                    if (tp != null)
                        CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheType, tp);
                }
                return tp;
            }
            set
            {
                if (value != null)
                    TypeCibleString = value.ToString();
                else
                    TypeCibleString = "";
                Row[c_champCacheType] = value;
            }
        }


        //-----------------------------------------------------------
        /// <summary>
        /// Champs système, indique la description système du champ pointé par celui-ci
        /// </summary>
        [TableFieldProperty(c_champDefinition, 4000)]
        [DynamicField("Field definition string", c_categorieChampSystème)]
        public string DefinitionString
        {
            get
            {
                return (string)Row[c_champDefinition];
            }
            set
            {
                Row[c_champDefinition] = value;
            }
        }

        //-----------------------------------------------------------
        [TableFieldProperty(c_champCacheDef, IsInDb = false, NullAutorise = true)]
        public CDefinitionProprieteDynamique Definition
        {
            get
            {
                CDefinitionProprieteDynamique def = Row[c_champCacheDef] as CDefinitionProprieteDynamique;
                if (def == null)
                {
                    CStringSerializer ser = new CStringSerializer(DefinitionString, ModeSerialisation.Lecture);
                    def = null;
                    if (!ser.TraiteObject<CDefinitionProprieteDynamique>(ref def))
                        def = null;
                    CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheDef, def);
                }
                return def;
            }
            set
            {
                if (value == null)
                    DefinitionString = "";
                else
                {
                    CStringSerializer ser = new CStringSerializer(ModeSerialisation.Ecriture);
                    CDefinitionProprieteDynamique def = value;
                    CResultAErreur res = ser.TraiteObject<CDefinitionProprieteDynamique>(ref def);
                    if (res)
                        DefinitionString = ser.String;
                }
                Row[c_champCacheDef] = DBNull.Value;
            }
        }
	}
}
