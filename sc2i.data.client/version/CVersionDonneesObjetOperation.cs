using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Text;
using sc2i.common;
using System.IO;
using sc2i.data;


namespace sc2i.data
{

    /// <summary>
    /// Détail d'une opération sur un champ d'une entité alterée par une version.
    /// </summary>
    /// <remarks>
    /// Chaque 'Data version object detail' correspond au détail d'une modification.
    /// Il y a une donnée par champ modifié sur l'entité.
    /// <BR></BR>
    /// Un 'Data version object detail' appartient toujours à une entité 'Data version object', qui permet
    /// de retrouver la version concernée.
    /// </remarks>
	[ObjetServeurURI("CVersionDonneesObjetOperationServeur")]
	[Table(CVersionDonneesObjetOperation.c_nomTable, CVersionDonneesObjetOperation.c_champId, true)]
	[NoRelationTypeId]
	[DynamicClass("Data version object detail")]
    [NoIdUniversel]
	public class CVersionDonneesObjetOperation : 
		CObjetDonneeAIdNumeriqueAuto,
		IElementAChampPourVersion,
		IObjetSansVersion,
		IObjetDonneeAValeurParDefautOptim
	{
		public const string c_valeurNull = "@null";
		
		public const string c_nomTable = "DATA_VERSION_OBJ_DT";

		public const string c_champId = "DVOBDT_ID";
		public const string c_champChamp = "DVOBDT_FIELD";
		public const string c_champTypeChamp = "DVOBT_FIELD_TYPE";
		public const string c_champValeur = "DVOBDT_VALUE";
		public const string c_champTypeValeur = "DVOBDT_VALUE_TYPE";
		public const string c_champValeurBlob = "DVOBDT_VALUE_BLOB";
		public const string c_champOperation = "DVOBDT_OPERATION";
		public const string c_champTimeStamp = "DVOBDT_TIMESTAMP";

		//-------------------------------------------------------
		public CVersionDonneesObjetOperation(CContexteDonnee contexte)
			: base(contexte)
		{
		}

		//-------------------------------------------------------
		public CVersionDonneesObjetOperation(DataRow row)
			: base(row)
		{
		}


		//-------------------------------------------------------
		public override string[] GetChampsTriParDefaut()
		{
			return new string[] { c_champId };
		}

		//-------------------------------------------------------
		/// <summary>
		/// Surchargé pour optim
		/// </summary>
		protected override void InitValeurDefaut()
		{
			//Rien, puisque les valeurs sont définies par défaut dans les colonnes
			TimeStamp = DateTime.Now;
		}

		protected override void MyInitValeurDefaut()
		{
			//Ne rien mettre ici, car c'est InitValeurDefaut qui est appellée
		}

		//-------------------------------------------------------
		public override string DescriptionElement
		{
			get { return I.T("Data version object data|30001"); }
		}


		//-------------------------------------------------------------------
		/// <summary>
		/// Retourne l'élément général de modification de l'entité concernée.
		/// </summary>
		[Relation(
			CVersionDonneesObjet.c_nomTable,
			CVersionDonneesObjet.c_champId,
			CVersionDonneesObjet.c_champId,
			true,
			true,
			true)]
		[DynamicField("Object version")]
		public CVersionDonneesObjet VersionObjet
		{
			get
			{
				return (CVersionDonneesObjet)GetParent(CVersionDonneesObjet.c_champId, typeof(CVersionDonneesObjet));
			}
			set
			{
				SetParent(CVersionDonneesObjet.c_champId, value);
			}
		}

	


		//---------------------------------------------
		/// <summary>
		/// Identifiant du champ. Pour des champs de type "DB_FIELD", il
		/// s'agit du nom du champ dans la base de données
		/// Si le type est "CUST_FIELD", il s'agit alors d'un champ custom, ...
		/// </summary>
		[TableFieldProperty(CVersionDonneesObjetOperation.c_champChamp, 255)]
		[DynamicField("Field key")]
		public string FieldKey
		{
			get
			{
				return (string)Row[c_champChamp];
			}
			set
			{
				Row[c_champChamp] = value;
			}
		}


		//-------------------------------------------------------
		/// <summary>
		/// Retourne le libellé (convivial) du champ auquel fait référence cet version
		/// </summary>
		[DynamicField("Field")]
		public IChampPourVersion Champ
		{
			get
			{
				IJournaliseurDonneesChamp journaliseur = CGestionnaireAChampPourVersion.GetJournaliseur(TypeChamp);
				if(journaliseur != null)
					return journaliseur.GetChamp(this);
				return null;
			}
			set
			{
				if (value == null)
				{
					FieldKey = "";
					TypeChamp = "";
				}
				else
				{
					FieldKey = value.FieldKey;
					TypeChamp = value.TypeChampString;
				}
			}
		}

		//---------------------------------------------
		/// <summary>
		/// Indique le type du champ qui a été modifié.
		/// </summary>
		/// <remarks>
		/// Si ce type est "DB_FIELD", il s'agit alors d'un champ standard de la base
		/// de données. S'il est "CUST_FIELD", il s'agit d'un champ custom.<BR>
		/// D'autres extensions peuvent être ajouté par certaines applications
		/// </remarks>
		[TableFieldProperty ( CVersionDonneesObjetOperation.c_champTypeChamp, 255)]
		[DynamicField("Field type")]
		public string TypeChamp
		{
			get
			{
				return (string)Row[c_champTypeChamp];
			}
			set
			{
				Row[c_champTypeChamp] = value;
			}
		}
		

		//---------------------------------------------
		/// <summary>
		/// Valeur de la modification sous forme de texte.
		/// Pour les nombres, le séparateur de décimal est le point
		/// Pour les dates, le format est AAAAMMDDHHmmss
		/// Pour les booléens, la valeur 1 vaut vrai, 0 vaut faux
		/// </summary>
		[TableFieldProperty ( CVersionDonneesObjetOperation.c_champValeur, 4000 )]
		[DynamicField("String value")]
		public string ValeurString
		{
			get
			{
				return (string)Row[c_champValeur];
			}
			set
			{
				Row[c_champValeur] = value;
			}
		}

		//---------------------------------------------
		/// <summary>
		/// Type de la donnée modifiée. Ce type est stocké suivant un code système.
		/// </summary>
        [TableFieldProperty(CVersionDonneesObjetOperation.c_champTypeValeur, 1024)]
		[DynamicField("Value type")]
		public string TypeValeurString
		{
			get
			{
				return (string)Row[c_champTypeValeur];
			}
			set
			{
				Row[c_champTypeValeur] = value;
			}
		}

		//---------------------------------------------
		/// <summary>
        /// Code du type d'opération. 
		/// </summary>
        /// <remarks>
        /// /// Les types de modifications possibles sont  :<BR></BR>
        /// <LI>0  : Création de l'entité</LI>
        /// <LI>10 : Suppression de l'entité</LI>
        /// <LI>20 : Modification</LI>
        /// <LI>30 : Aucune modification</LI>
        /// </remarks>
        [TableFieldProperty(CVersionDonneesObjetOperation.c_champOperation)]
		[DynamicField("Operation code")]
		public int CodeTypeOperation
		{
			get
			{
				return (int)Row[c_champOperation];
			}
			set
			{
				Row[c_champOperation] = value;
			}
		}

		//---------------------------------------------
		[DynamicField("Operation type")]
		public CTypeOperationSurObjet TypeOperation
		{
			get
			{
				return new CTypeOperationSurObjet((CTypeOperationSurObjet.TypeOperation)CodeTypeOperation);
			}
			set
			{
				if (value != null)
					CodeTypeOperation = value.CodeInt;
			}
		}

		//---------------------------------------------
		/// <summary>
		/// Valeur de blob si ce champ contient un blob
		/// </summary>
		[TableFieldProperty(CVersionDonneesObjetOperation.c_champValeurBlob, NullAutorise = true)]
		public CDonneeBinaireInRow ValeurBlob
		{
			get
			{
				if (Row[c_champValeurBlob] == DBNull.Value)
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champValeurBlob);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champValeurBlob, donnee);
				}
				object obj = Row[c_champValeurBlob];
				return (CDonneeBinaireInRow)obj;
			}
			set
			{
				Row[c_champValeurBlob] = value;
			}
		}

		//---------------------------------------------
		public Type TypeValeur
		{
			get
			{
				return CActivatorSurChaine.GetType(TypeValeurString);
			}
			set
			{
				if (value != null)
					TypeValeurString = value.ToString();
				else
					TypeValeurString = "";
			}
		}


		//-----------------------------------------------------------
		/// <summary>
		/// Date/heure de la modification.
		/// </summary>
        /// <remarks>
        /// Dans une version prévisionnelle, lorsqu'un objet est modifié, le système stocke la date et l'heure
        /// de chaque modification de chaque champ.
        /// </remarks>
		[TableFieldProperty(c_champTimeStamp, true)]
		[DynamicField("TimeStamp")]
		public DateTime? TimeStamp
		{
			get
			{
				return (DateTime?)Row[c_champTimeStamp, true];
			}
			set
			{
				Row[c_champTimeStamp, true] = value;
			}
		}


		//---------------------------------------------
		/// <summary>
		/// Affecte la valeur string et le type de la valeur
		/// </summary>
		/// <param name="val"></param>
		public void SetValeurStd(object val)
		{
			CUtilStockageValeurChamp.StockValeur(Row, ContexteDonnee.IdSession, c_champTypeValeur, c_champValeurBlob, c_champValeur, val);
		}

		//---------------------------------------------
		/// <summary>
		/// Convertit la valeur string standard en valeur du bon type
		/// </summary>
		/// <returns></returns>
		public object GetValeurStd()
		{
			return CUtilStockageValeurChamp.GetValeur(Row, c_champValeur, c_champValeurBlob, c_champTypeValeur, ContexteDonnee);
		}

		//---------------------------------------------
		/// <summary>
		/// Retourne la valeur réelle, (en fonction du journaliseur)
		/// </summary>
		public object GetValeur()
		{
			IJournaliseurDonneesChamp journaliseur = CGestionnaireAChampPourVersion.GetJournaliseur(TypeChamp);
			if(journaliseur != null)
				return journaliseur.GetValeur(this);
			return null;
		}

		//-------------------------------------------------------
		public CResultAErreur AppliqueModification(CObjetDonneeAIdNumerique objet)
		{
			CResultAErreur result = CResultAErreur.True;
			IJournaliseurDonneesChamp journaliseur = CGestionnaireAChampPourVersion.GetJournaliseur(TypeChamp);
			if (journaliseur == null)
			{
				result.EmpileErreur(I.T("Can not apply modifications for field @1 (@2)|30002",
					FieldKey, TypeChamp));
				return result;
			}
			result = journaliseur.AppliqueValeur(VersionObjet.VersionDonnees.Id, Champ, objet, GetValeur());
			return result;
		}


		#region IElementAChampPourVersion Membres

		public Type TypeEntite
		{
			get 
			{ 
				if(VersionObjet != null)
					return VersionObjet.TypeElement;
				return null;
			}
		}

		#endregion

		#region IElementAChampPourVersion Membres


		public string NomChampConvivial
		{
			get
			{
				Type tp = TypeEntite;
				if (tp != null)
				{
				    //Cherche la propriété liée au champ
				    foreach (PropertyInfo prop in tp.GetProperties())
				    {
				        object[] attribs = prop.GetCustomAttributes(typeof(TableFieldPropertyAttribute), true);
				        if (attribs.Length > 0)
				        {
				            string strNomChamp = ((TableFieldPropertyAttribute)attribs[0]).NomChamp;
				            if (strNomChamp == FieldKey)
				            {
				                attribs = prop.GetCustomAttributes(typeof(DynamicFieldAttribute), true);
				                if (attribs.Length != 0)
				                    return ((DynamicFieldAttribute)attribs[0]).NomConvivial;
				            }
				        }
				    }
				}
				return FieldKey;
			}
			set
			{
			}
		}

		#endregion

		public void DefineValeursParDefaut(DataTable table)
		{
			DataColumn col = table.Columns [ CSc2iDataConst.c_champIsDeleted];
			if ( col != null )
				col.DefaultValue = false;

			col = table.Columns[ c_champChamp ];
			if ( col != null )
				col.DefaultValue = "";

			col = table.Columns[c_champOperation];
			if ( col != null )
				col.DefaultValue = (int)CTypeOperationSurObjet.TypeOperation.Modification;

			col = table.Columns [ c_champTypeChamp ];
			if ( col != null )
				col.DefaultValue = "";

			col = table.Columns[c_champTypeValeur];
			if ( col != null )
				col.DefaultValue = "";

			col = table.Columns[ c_champValeur ];
			if ( col != null )
				col.DefaultValue = "";
		}

	}



}
