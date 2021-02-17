using System;
using System.Data;

using sc2i.data;
using sc2i.common;
using sc2i.common.unites;


namespace sc2i.data.dynamic
{
   
	/// <summary>
	/// Description résumée de CRelationElementAChamp_ChampCustom.
	/// </summary>
    /// <seealso cref="CChampCustom"/>
	[NoRelationTypeId]
    [InsertAfterRelationTypeId]
	public abstract class CRelationElementAChamp_ChampCustom : CObjetDonneeAIdNumeriqueAuto, IObjetDonneeAValeurParDefautOptim
	{
		public const string c_champValeurInt = "FIELD_VALUE_INT";
		public const string c_champValeurString = "FIELD_VALUE_STRING";
		public const string c_champValeurDouble = "FIELD_VALUE_DOUBLE";
		public const string c_champValeurDate = "FIELD_VALUE_DATE";
		public const string c_champValeurBool = "FIELD_VALUE_BOOL";
		public const string c_champValeurNull = "FIELD_VALUE_IS_NULL";

		//-------------------------------------------------------------------
#if PDA
		public CRelationElementAChamp_ChampCustom()
			:base()
		{
		}
#endif
		//-------------------------------------------------------------------
		public CRelationElementAChamp_ChampCustom(CContexteDonnee ctx)
			:base(ctx)
		{
		}
		//-------------------------------------------------------------------
		public CRelationElementAChamp_ChampCustom(System.Data.DataRow row)
			:base(row)
		{
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Surchargée pour optimisation
		/// </summary>
		protected override void InitValeurDefaut()
		{
			//Rien car c'est dans les defaultValue des colonnes qu'on trouve les valeurs par défaut
            // YK 26/03/2014 : Si quand même, il faut initialiser l'Id Universel
            Row[c_champIdUniversel] = CUniqueIdentifier.GetNew();
		}
			
		//-------------------------------------------------------------------
		protected override void MyInitValeurDefaut()
		{
		}

		//-------------------------------------------------------------------
        [DynamicField("Element")]
		public abstract IElementAChamps ElementAChamps{get;set;}

		//-------------------------------------------------------------------
		public abstract Type GetTypeElementAChamps();

		//-------------------------------------------------------------------
		public override string DescriptionElement
		{
			get
			{
				return I.T("Relation between @1 and @2|100", ElementAChamps.DescriptionElement, ChampCustom.DescriptionElement);
			}
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{GetChampId()};
		}

		//-------------------------------------------------------------------
		public virtual object GetValeur ( DataRowVersion version )
		{
			if ( ChampCustom == null )
				return "";
			if ( IsNull )
				return null;
			try
			{
				switch ( ChampCustom.TypeDonneeChamp.TypeDonnee )
				{
					case TypeDonnee.tBool :
						return (bool)Row[c_champValeurBool, version];
					case TypeDonnee.tDate :
						return (DateTime)Row[c_champValeurDate, version];
                    case TypeDonnee.tDouble:
                        if (ChampCustom.ClasseUnite != null)
                        {
                            string strFormat = (string)Row[c_champValeurString, version];
                            if (strFormat == null || strFormat.Trim().Length == 0)
                            {
                                strFormat = ChampCustom.FormatAffichageUnite;
                                if (strFormat.Trim().Length == 0)
                                {
                                }
                            }
                            if (strFormat == null)
                                strFormat = "";
                            return new CValeurUnite((double)Row[c_champValeurDouble],
                                ChampCustom.ClasseUnite.UniteBase,
                                strFormat);
                        }
                        return (double)Row[c_champValeurDouble, version];
					case TypeDonnee.tEntier :
						return (int)Row[c_champValeurInt, version];
					case TypeDonnee.tString :
						return (string)Row[c_champValeurString, version];
					case TypeDonnee.tObjetDonneeAIdNumeriqueAuto:
						{
							string strType = (string)Row[c_champValeurString, version];
							int nId = (int)Row[c_champValeurInt, version];
							Type tp = CActivatorSurChaine.GetType(strType, true);
							if (tp != null)
							{
								CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance(tp, new object[] { ContexteDonnee });
								if (objet.ReadIfExists(nId))
									return objet;
							}
							return null;
						}

				}
			}
			catch
			{
			}
			return ChampCustom.TypeDonneeChamp.ObjectToType ( "", ContexteDonnee );
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Valeur du champ. Il n'est pas possible de réaliser un filtre sur ce champ. POur filtrer, il faut utiliser le champ stockant la donnée typée
		/// </summary>
        [DynamicField("Value")]
		public virtual object Valeur
		{
			get
			{
				return GetValeur(VersionToReturn);
			}
			set
			{
				if ( value == null || value == DBNull.Value )
				{
					IsNull = true;
					ValeurString = "";
					ValeurInt = -1;
                    ValeurBool = false;
                    ValeurDouble = 0;
					return;
				}
				
				TypeDonnee tp = TypeDonnee.tString;
				if ( ChampCustom != null )
					tp = ChampCustom.TypeDonneeChamp.TypeDonnee;
                IsNull = false;
                // Si c'est un champs de type Entité
				if (tp == TypeDonnee.tObjetDonneeAIdNumeriqueAuto)
				{
					ValeurBool = false;
                    //STEF 17/3/09 : ne change pas la valeur date,
                    //car sinon, même si rien ne change, la date change
					//ValeurDate = DateTime.Now;
					ValeurDouble = 0;

                    if (value != null)
                    {
                        if (value is int)
                        {
                            ValeurString = ChampCustom.TypeObjetDonnee.ToString();
                            ValeurInt = (int)value;
                        }
                        else
                        {
                            try
                            {
                                ValeurString = value.GetType().ToString();
                                ValeurInt = ((CObjetDonneeAIdNumerique)value).Id;
                            }
                            catch
                            { }
                        }
                    }
                    else
                    {
                        ValeurInt = 0;
                        ValeurString = "";
                    }
				}
                // C'est une champ de type Valeur
				else
				{
                    switch (ChampCustom.TypeDonneeChamp.TypeDonnee)
                    {
                        case TypeDonnee.tEntier:
                            try
                            {
                                ValeurInt = Convert.ToInt32(value);
                                ValeurString = C2iTypeDonnee.TypeToString(ValeurInt);
                            }
                            catch
                            {
                                ValeurInt = 0;
                                ValeurString = "0";
                            }
                            break;
                        case TypeDonnee.tDouble:
                            try
                            {
                                if (value is CValeurUnite)
                                {
                                    //Convertit la valeur dans la classe de base
                                    CValeurUnite valU = value as CValeurUnite;
                                    try
                                    {
                                        IClasseUnite classe = ChampCustom.ClasseUnite;
                                        if (classe != null)
                                            valU = valU.ConvertTo(classe.UniteBase);
                                        ValeurDouble = valU.Valeur;
                                        ValeurString = valU.Format;
                                    }
                                    catch
                                    {
                                        ValeurDouble = valU.Valeur;
                                    }
                                }
                                else
                                {
                                    ValeurDouble = Convert.ToDouble(value);
                                    ValeurString = C2iTypeDonnee.TypeToString(ValeurDouble);
                                }
                            }
                            catch
                            {
                                ValeurDouble = 0;
                                ValeurString = "0";
                            }
                            break;
                        case TypeDonnee.tString:
                            ValeurString = value.ToString();
                            break;
                        case TypeDonnee.tDate:
                            try
                            {
                                if (value is CDateTimeEx)
                                    ValeurDate = ((CDateTimeEx)value).DateTimeValue;
                                else if (value is DateTime)
                                    ValeurDate = (DateTime)value;
                                else
                                    ValeurDate = DateTime.Parse(value.ToString());
                            }
                            catch
                            {
                                ValeurDate = DateTime.Now;
                            }
                            break;
                        case TypeDonnee.tBool:
                            try
                            {
                                ValeurBool = Convert.ToBoolean(value);
                                ValeurString = C2iTypeDonnee.TypeToString(ValeurBool);
                            }
                            catch
                            {
                                ValeurBool = value.ToString() == "1";
                                ValeurString = C2iTypeDonnee.TypeToString(ValeurBool);
                            }
                            break;
                    }
				}
			}
		}

        //-------------------------------------------------------------------
        /// <summary>
        /// Retourne la valeur du champ avec son unité, s'il s'agit d'un
        /// champ avec unité.
        /// </summary>
        [DynamicField("Unit value")]
        public CValeurUnite ValeurUnite
        {
            get{
                IClasseUnite classe = CGestionnaireUnites.GetClasse(ChampCustom.ClasseUniteCode);
                CValeurUnite valeur = new CValeurUnite(ValeurDouble, classe != null?
                    classe.UniteBase:"");
                return valeur;
            }
            set
            {
                if (value != null)
                {
                    IClasseUnite classe = CGestionnaireUnites.GetClasse(ChampCustom.ClasseUniteCode);
                    if (classe != null)
                    {
                        CValeurUnite valeur = value.ConvertTo(classe.UniteBase);
                        ValeurDouble = valeur.Valeur;
                    }
                }
            }
        }


		//-------------------------------------------------------------------
		/// <summary>
		/// Valeur du champ si le champ est un champ texte
		/// </summary>
        [TableFieldProperty(c_champValeurString, 4000)]
        [DynamicField("String value")]
		public string ValeurString
		{
			get
			{
				return (string)Row[c_champValeurString];
			}
			set
			{
				Row[c_champValeurString] = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Valeur du champ si le champ est un champ entier
		/// </summary>
        [TableFieldProperty(c_champValeurInt)]
        [DynamicField("Int value")]
		public int ValeurInt
		{
			get
			{
				return (int)Row[c_champValeurInt];
			}
			set
			{
				Row[c_champValeurInt] = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Valeur du champ si le champ est un champ décimal
		/// </summary>
        [TableFieldProperty(c_champValeurDouble)]
        [DynamicField("Decimal value")]
		public double ValeurDouble
		{
			get
			{
				return (double)Row[c_champValeurDouble];
			}
			set
			{
				Row[c_champValeurDouble] = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Valeur du champ si le champ est un champ date
		/// </summary>
        [TableFieldProperty(c_champValeurDate)]
        [DynamicField("Date value")]
		public DateTime ValeurDate
		{
			get
			{
				return (DateTime)Row[c_champValeurDate];
			}
			set
			{
				Row[c_champValeurDate] = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Valeur du champ si le champ est un booléen
		/// </summary>
        [TableFieldProperty(c_champValeurBool)]
        [DynamicField("Bool value")]
		public bool ValeurBool
		{
			get
			{
				return (bool)Row[c_champValeurBool];
			}
			set
			{
				Row[c_champValeurBool] = value;
			}
		}

		//-------------------------------------------------------------------
		/// <summary>
		/// Indique si le champ a une valeur nulle.
		/// </summary>
        [TableFieldProperty(c_champValeurNull)]
        [DynamicField("Is null")]
		public bool IsNull
		{
			get
			{
				return (bool)Row[c_champValeurNull];
			}
			set
			{
				Row[c_champValeurNull] = value;
			}
		}

		
		//-------------------------------------------------------------------
		/// <summary>
		/// Champ personnalisé auquel correspond cette valeur.
		/// </summary>
        [Relation(
            CChampCustom.c_nomTable,
            CChampCustom.c_champId,
            CChampCustom.c_champId,
            true,
            true,
            true,
            IsCluster = true,
            NePasClonerLesFils=true)]
		[DynamicField("Custom Field")]
		public CChampCustom ChampCustom
		{
			get
			{
				return ( CChampCustom )GetParent (CChampCustom.c_champId, typeof(CChampCustom));
			}
			set
			{
				SetParent ( CChampCustom.c_champId, value );
			}
		}

		#region IObjetDonneeAValeurParDefautOptim Membres

		public virtual void DefineValeursParDefaut(DataTable table)
		{
			DataColumn col = table.Columns[CSc2iDataConst.c_champIsDeleted];
			if ( col != null )
				col.DefaultValue = false;

			col = table.Columns[c_champValeurInt];
			if (col != null)
				col.DefaultValue = 0;

			col = table.Columns[c_champValeurDate];
			if (col != null)
				col.DefaultValue = DateTime.Now;

			col = table.Columns[c_champValeurDouble];
			if (col != null)
				col.DefaultValue = 0.0;

			col = table.Columns[c_champValeurBool];
			if (col != null)
				col.DefaultValue = false;

			col = table.Columns[c_champValeurString];
			if (col != null)
				col.DefaultValue = "";

			col = table.Columns[c_champValeurNull];
			if ( col != null )
				col.DefaultValue = false;

		}

		#endregion
	}
}
