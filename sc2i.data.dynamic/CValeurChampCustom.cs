using System;

using sc2i.common;
using sc2i.data;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Indique une valeur possible pour un <see cref="CChampCustom">Champ personnalisé</see>
	/// </summary>
    /// <remarks>
    /// Chaque valeur possible de champ calculé génère une entité de ce type.
    /// <p>Chaque valeur possible est un couple 'valeur stockée'/'valeur affichée'. La valeur affichée sera 
    /// présentée aux utilisateurs finaux, alors que la valeur stockée indique ce qui sera stocké
    /// dans la base de données. La séparation des données stockées / affichées offre une plus grande
    /// souplesse dans l'évolution du paramétrage.</p>
    /// <p>Les valeurs stockées possibles sont toujours stockées sous forme de texte, converties par le programme
    /// en une donnée compatible avec le type de données du champ concerné</p>
    /// </remarks>
	[ObjetServeurURI("CValeurChampCustomServeur")]
	[FullTableSync]
	[Table(CValeurChampCustom.c_nomTable,CValeurChampCustom.c_champId,true)]
	[DynamicClass("Custom field possible value")]
	public class CValeurChampCustom : CObjetDonneeAIdNumeriqueAuto, IValeurVariable, IObjetALectureTableComplete
	{
		#region Déclaration des constantes
		public const string c_nomTable = "CUSTOM_FIELD_VALUE";
		public const string c_champId = "CUFLD_ID";
		public const string c_champValue = "CUFLD_VALUE";
		public const string c_champDisplay = "CUFLD_DISPLAY";
        public const string c_champIndex = "CUFLD_INDEX";
		#endregion
#if PDA
		/// /////////////////////////////////////////////////////////
		public CValeurChampCustom( )
			:base (  )
		{
			
		}
#endif
		/// <summary>
		/// /////////////////////////////////////////////////////////
		/// </summary>
		/// <param name="ctx"></param>
		public CValeurChampCustom( CContexteDonnee ctx)
			:base ( ctx )
		{
			
		}

		/// /////////////////////////////////////////////////////////
		public CValeurChampCustom ( System.Data.DataRow row )
			:base ( row )
		{
		}

		/// /////////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			Value = "";
		}

		/// ///////////////////////////////////////////////////////
		public override string GetChampId()
		{
			return c_champId;
		}

		/// ////////////////////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champIndex, c_champDisplay};
		}
		

		
		

		////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return c_nomTable;
		}


		/// /////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Field value '@1'/'@2'|235", Value.ToString(), Display);
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Valeur stockée sous forme de texte
		/// </summary>
        [
		TableFieldProperty(c_champValue, 255),
		
		]
        [DynamicField("String value")]
		public string ValueString
		{
			get
			{
				return (string)Row[c_champValue];
			}
			set
			{
				Row[c_champValue] = value;
			}
		}

        ////////////////////////////////////////////////////////////
        [TableFieldProperty(c_champIndex)]
        [DynamicField("Index")]
        public int Index
        {
            get
            {
                return (int)Row[c_champIndex];
            }
            set
            {
                Row[c_champIndex] = value;
            }
        }

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Valeur stockée au format du champ
		/// </summary>
        [DynamicField("Stored value")]
		public object Value
		{
			get
			{
				return ChampCustom.TypeDonneeChamp.StringToType(ValueString, ContexteDonnee);
			}
			set
			{
				ValueString = C2iTypeDonnee.TypeToString ( value );
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Valeur affichée
		/// </summary>
        [
		TableFieldProperty(c_champDisplay, 1024),
		DynamicField("Displayed value")
		]
		public string Display
		{
			get
			{
				return (string)Row[c_champDisplay];
			}
			set
			{
				Row[c_champDisplay] = value.Trim();
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Champ personnalisé auquel se rapporte cette valeur possible
		/// </summary>
        [Relation(CChampCustom.c_nomTable, CChampCustom.c_champId, CChampCustom.c_champId, true, true )]
		[DynamicField("Field")]
		public CChampCustom ChampCustom
		{
			get
			{
				CChampCustom champ = new CChampCustom(ContexteDonnee);
				champ.Id = (int)Row[CChampCustom.c_champId];
				return champ;
			}
			set
			{
				AssureExiste(value);
				Row[CChampCustom.c_champId] = value.Id;
			}
		}

		////////////////////////////////////////////////////////////
	}
}
