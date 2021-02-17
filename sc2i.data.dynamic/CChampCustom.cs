using System;
using System.Collections;
using System.IO;

using sc2i.data;
using sc2i.common;
using sc2i.expression;
using sc2i.common.recherche;
using System.Collections.Generic;
using System.Text;
using sc2i.common.unites;
using System.Data;

namespace sc2i.data.dynamic
{
	/// <summary>
	/// Un champ custom est un champ personnalis�, ajout� � la structure de donn�es de Timos par l'administrateur
    /// de l'application.
	/// </summary>
    /// <remarks>
    /// Chaque champ personnalis� est cr�� pour un type de donn�es particulier et ne peut s'appliquer qu'� celui-ci.<BR></BR>
    /// Un champ personnalis� est con�u npour recevoir un type de donn�e particulier (texte, entier, d�cimal, bool�ean, date/heure
    /// ou entit�).
    /// En r�gle g�n�rale, le champ ne s'applique pas � toutes les entit�s d'un type d'entit�, mais s'applique en fonction de l'appartenance
    /// de l'entit� � un �l�ment d�finissant ses champs personnalis�s. Par exemple, un champ personnalis� sur "SITE" ne s'applique
    /// qu'aux sites ayant un type de site particulier.
    /// </remarks>
	[ObjetServeurURI("CChampCustomServeur")]
	[Table(CChampCustom.c_nomTable,CChampCustom.c_champId,true)]
	[FullTableSync]
	[DynamicClass("Custom field")]
	public class CChampCustom : CObjetDonneeAIdNumeriqueAuto, 
		IVariableDynamique, 
		IObjetALectureTableComplete,
        IObjetCherchable
	{
		/// <summary>
		/// Propri�t� attach�e � la session utilisateur indiquant les restrictions sur les champs custom
		/// </summary>
		public const string c_propSessionRestrictions = "CUST_FIELD_RESTRICTION";
        public const string c_prefixeCleRestriction = "CHAMP_CUST_";

		#region D�claration des constantes
		public const string c_nomTable = "CUSTOM_FIELD";
		public const string c_champId = "CUSTFLD_ID";
		public const string c_champNom = "CUSTFLD_NAME";
		public const string c_champType = "CUSTFLD_TYPE";
		public const string c_champFolder = "CUSTFLD_FOLDER";
		public const string c_champTypeObjetDonnee = "CUSTFLD_LNK_OBJECT_TYPE";
		public const string c_champFiltreObjetDonnee = "CUSTFLD_OBJECT_FILTER";
		public const string c_champLibellePourParent = "CUSTFLD_LBL_FOR_OBJ_PART";
		public const string c_champValeurDefaut = "CUSTFLD_DEFAULT";
		public const string c_champDescription = "CUSTFLD_DESCRIPTION";
		public const string c_champPrecision = "CUSTFLD_PRECISION";
		public const string c_champCodeRole = "CUSTFLD_ROLE";
        public const string c_champCodesRolesSecondaires = "CUSTFLD_SECONDARY_ROLES";
		public const string c_champFormuleValidation = "CUSTFLD_VALIDATION_FORMUL";
		public const string c_champInfoErreurFormat = "CUSTFLD_ERROR_TEXT";
		public const string c_champLibelleConvivial = "CUSTFLD_FRIENDLY_NAME";
		public const string c_champLibelleCourt = "CUSTFLD_SHORT_NAME";
		public const string c_champRestrictionReadOnly = "CUSTFLD_REST_READONLY";
		public const string c_champRestrictionMasquer = "CUSTFLD_REST_MASQUER";
        public const string c_champClasseUnite = "CUSTFLD_UNIT_CLASS";
        public const string c_champFormatUnite = "CUSTFLD_UNIT_FORMAT";

		public const string c_champCacheFormule = "CUSTFLD_FORMULA_CACHE";

		#endregion

#if PDA
		/// ///////////////////////////////////////////////////////
		public CChampCustom(  )
			:base()
		{
		}
#endif
		/// ///////////////////////////////////////////////////////
		public CChampCustom( CContexteDonnee ctx )
			:base(ctx)
		{
		}

		/// ///////////////////////////////////////////////////////
		public CChampCustom ( System.Data.DataRow row )
			:base(row)
		{
		}
		/// ///////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			TypeDonneeChamp = new C2iTypeDonnee(sc2i.data.dynamic.TypeDonnee.tEntier);
			ValeurParDefautString = "";
			Precision = 0;
			RestrictionsReadOnly = 0;
			RestrictionsMasquer = 0;
		}

		/// ///////////////////////////////////////////////////////
		public override string GetChampId()
		{
			return c_champId;
		}

		/// ///////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[] {c_champNom};
		}

        // Implemente le membre IdVariable de l'interface IVariableDynamique
        public string IdVariable
        {
            get
            {
                return IdUniversel;
            }
            set { }
        }

        public static int GetIdFromDbKey(CDbKey dbKey)
        {
            CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
            if (champ.ReadIfExists(dbKey))
                return champ.Id;

            return -1;
        }

		/// ///////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("Custom Field @1|134", Nom);
			}
		}

		////////////////////////////////////////////////////////////
		public override string GetNomTable()
		{
			return c_nomTable;
		}

		/// ///////////////////////////////////////////////////////
		public bool IsChoixParmis()
		{

			return TypeDonneeChamp.TypeDonnee == sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto || Valeurs.Count > 0;
		}

		/// ///////////////////////////////////////////////////////
		public CTypeResultatExpression TypeDonnee
		{
			get
			{
				switch (TypeDonneeChamp.TypeDonnee)
				{
					case sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto:
						return new CTypeResultatExpression(TypeObjetDonnee, false);
					default:
						return new CTypeResultatExpression(TypeDonneeChamp.TypeDotNetAssocie, false);
				}
			}
		}

		/// ///////////////////////////////////////////////////////
		public bool IsChoixUtilisateur()
		{
			return true;
		}

		public CResultAErreur VerifieValeur(object valeur)
		{
			return TesteValeur ( valeur );
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Le nom du champ personnalis�. Ce nom doit �tre unique dans toute la base de donn�es
		/// </summary>
        [
		TableFieldProperty(c_champNom, 128),
		DynamicField("Name")
		]
		public virtual string Nom
		{
			get
			{
				return (string)Row[c_champNom];
			}
			set
			{
				Row[c_champNom] = value;
			}
		}

		////////////////////////////////////////////////////////////
		public C2iTypeDonnee TypeDonneeChamp
		{
			get
			{
				return new C2iTypeDonnee((TypeDonnee)Row[c_champType], TypeObjetDonnee);
			}
			set
			{
				Row[c_champType] = (int)value.TypeDonnee;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Type de donn�e contenu dans le champ
		/// </summary>
        /// <remarks>
        /// Les valeurs possibles sont  : <BR></BR>
        /// <LI>0 : Entier</LI>
		/// <li>1 : Nombre d�cimal</li>LI>
		/// <LI>2 : Texte </LI>
		/// <li>3 : Date / Heure</li>
		/// <LI>4 : Bool�en (valeur oui/Non)</LI>
		/// <li>5 : Entit� (dans ce cas, le champ Associated entity type indique le type de l'entit� contenue</li>
        /// </remarks>
        [TableFieldProperty(c_champType)]
        [DynamicField("Data type")]
		public int TypeDonneeInt
		{
			get
			{
				return (int) Row[c_champType];
			}
			set
			{
				Row[c_champType] = (int)value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// D�finit le type de l'entit� contenue dans le champ si ce champ est destin� � contenir un type d'entit�.
        /// <BR></BR>Le type est 'cod�' sous une forme syst�me indiquant le nom du type.
		/// </summary>
        [TableFieldProperty ( CChampCustom.c_champTypeObjetDonnee, 255)]
		[DynamicField("Associated entity type")]
		public string TypeObjetDonneeString
		{
			get
			{
				return (string)Row[c_champTypeObjetDonnee];
			}
			set
			{
				Row[c_champTypeObjetDonnee] = value;
			}
		}

		////////////////////////////////////////////////////////////
		public Type TypeObjetDonnee
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeObjetDonneeString );
			}
			set
			{
				if (value == null)
					Row[c_champTypeObjetDonnee] = "";
				else
					Row[c_champTypeObjetDonnee] = value.ToString();
			}
		}

		/// /////////////////////////////////////////////////////////////
		[TableFieldProperty(CChampCustom.c_champFiltreObjetDonnee,NullAutorise=true)]
		public CDonneeBinaireInRow FiltreObjetDonneeString
		{
			get
			{
				if ( Row[c_champFiltreObjetDonnee] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession, Row, c_champFiltreObjetDonnee);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champFiltreObjetDonnee, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champFiltreObjetDonnee]).GetSafeForRow(Row.Row);
			}
			set
			{
				Row[c_champFiltreObjetDonnee] = value;
			}
		}

		/// /////////////////////////////////////////////////////////////
		///<summary>
        ///Lorsqu'un champ est destin� � contenir une entit� et non pas une valeur 'simple', la proprit� 'Label fro parent' indique
        ///Comment les valeurs de ce champ sont accessibles pour l'entit� parente.
        ///</summary>
        ///<remarks>
        ///Si l'on d�finit par exemple un champ sur site indiquant le propri�taire du site. Le propri�taire du site �tant un acteur<BR></BR>
        ///Chaque site se voit affecter un acteur propri�taire, mais chaque acteur se voit donc indirectement affecter
        ///des sites dont il est le propri�taire.<BR></BR>
        ///En interrogeant le propri�t� dont le nom correspond � 'label for parent' sur un acteur, on obtient la liste de tous
        ///les sites dont l'acteur est propri�taire.
        ///</remarks>
        [TableFieldProperty ( CChampCustom.c_champLibellePourParent, 128 )]
		[DynamicField("Label for parent")]
		public string LibellePourObjetParent
		{
			get
			{
				return (string)Row[c_champLibellePourParent];
			}
			set
			{
				Row[c_champLibellePourParent] = value;
			}
		}

		

		/// /////////////////////////////////////////////////////////////
        [BlobDecoder]
        public CFiltreDynamique FiltreObjetDonnee
		{
			get
			{
				CFiltreDynamique filtre = new CFiltreDynamique(ContexteDonnee);
				if (FiltreObjetDonneeString.Donnees != null)
				{
					MemoryStream stream = new MemoryStream(FiltreObjetDonneeString.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					CResultAErreur result = filtre.Serialize(serializer);
					if (!result)
					{
						filtre = new CFiltreDynamique(ContexteDonnee);
					}
					filtre.ContexteDonnee = ContexteDonnee;
					filtre.ResetValeursVariables();

                    reader.Close();
                    stream.Close();
				}
				return filtre;
			}
			set
			{
				if (value == null)
				{
					CDonneeBinaireInRow data = FiltreObjetDonneeString;
					data.Donnees = null;
					FiltreObjetDonneeString = data;
				}
				else
				{
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					CResultAErreur result = value.Serialize(serializer);
					if (result)
					{
						if (value != null && value.TypeElements != null)
							Row[c_champTypeObjetDonnee] = value.TypeElements.ToString();
						else
							Row[c_champTypeObjetDonnee] = "";
						CDonneeBinaireInRow data = FiltreObjetDonneeString;
						data.Donnees = stream.GetBuffer();
						FiltreObjetDonneeString = data;
						TypeObjetDonnee = value.TypeElements;
					}

                    writer.Close();
                    stream.Close();
				}
			}
		}

		////////////////////////////////////////////////////////////
		public static C2iTypeDonnee[] TypePossibles
		{
			get
			{
				ArrayList lst = new ArrayList();
				lst.Add ( new C2iTypeDonnee(sc2i.data.dynamic.TypeDonnee.tEntier));
				lst.Add ( new C2iTypeDonnee(sc2i.data.dynamic.TypeDonnee.tDouble));
				lst.Add ( new C2iTypeDonnee(sc2i.data.dynamic.TypeDonnee.tString));
				lst.Add ( new C2iTypeDonnee(sc2i.data.dynamic.TypeDonnee.tDate));
				lst.Add ( new C2iTypeDonnee(sc2i.data.dynamic.TypeDonnee.tBool));
				lst.Add(new C2iTypeDonnee(sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto));
				return (C2iTypeDonnee[])lst.ToArray(typeof(C2iTypeDonnee));
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Permet de sp�cifier une description pour le champ. Cette information est purement informative, pour permettre � l'administrateur
        /// l'application de retrouver le sens des champs personnalis�s qu'il a cr��.
		/// </summary>
        [
		TableFieldProperty(c_champDescription, 1024),
		DynamicField("Description")
		]
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


		////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champValeurDefaut, 2048)]
		public string ValeurParDefautString
		{
			get
			{
				return (string)Row[c_champValeurDefaut];
			}
			set
			{
				Row[c_champValeurDefaut] = value;
			}
		}

		////////////////////////////////////////////////////////////
		public object ValeurParDefaut
		{
			get
			{
				C2iExpression formuleDefaut = FormuleValeurParDefaut;
				if ( formuleDefaut == null )
					return null;
				CContexteEvaluationExpression ctx = new CContexteEvaluationExpression(null);
				CResultAErreur result = formuleDefaut.Eval ( ctx );
				if ( !result )
					return C2iTypeDonnee.StringToType ( TypeDonneeChamp.TypeDonnee, "0", ContexteDonnee );
				return TypeDonneeChamp.ObjectToType ( result.Data, ContexteDonnee );
			}
		}

		/*////////////////////////////////////////////////////////////
		[DynamicField("Default value")]
		public object ValeurParDefaut
		{
			get
			{
				return TypeDonneeChamp.StringToType(ValeurParDefautString);
			}
			set
			{
				ValeurParDefautString = C2iTypeDonnee.TypeToString(value);
			}
		}*/

		////////////////////////////////////////////////////////////
		public C2iExpression FormuleValeurParDefaut
		{
			get
			{
				if ( ValeurParDefautString == "" )
					return null;
				C2iExpression exp = C2iExpression.FromPseudoCode ( ValeurParDefautString );
				if ( exp == null )
					exp = new C2iExpressionConstante ( ValeurParDefautString );
				return exp;
			}
			set
			{
				if ( value == null )
					ValeurParDefautString = "";
				else
					ValeurParDefautString = C2iExpression.GetPseudoCode( value );
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique une valeur de pr�cision pour ce champ.
		/// </summary>
        /// <remark>
        /// Pour un champ de type "D�cimal", cette valeur indique le nombre de chiffres qui seront pr�sent�s apr�s la virgule.<BR>
        /// Pour un champ de type "Texte", cette valeur indique le nombre maximum de caract�res qui seront autoris�s lors de la saisie d'une 
        /// valeur pour ce champ.
        /// </remark>
        [
		TableFieldProperty(c_champPrecision),
		DynamicField("Precision")
		]
		public int Precision
		{
			get
			{
				return (int)Row[c_champPrecision];
			}
			set
			{
				Row[c_champPrecision] = value;
			}
		}

		////////////////////////////////////////////////////////////
        [TableFieldProperty(c_champCodeRole, 20)]
        [DynamicField("Role Code", "System")]
		public string CodeRole
		{
			get
			{
				return (string)Row[c_champCodeRole];
			}
			set
			{
				Row[c_champCodeRole] = value;
			}
		}


        //-----------------------------------------------------------
        [TableFieldProperty(c_champCodesRolesSecondaires, 2000)]
        public string CodesRolesSecondairesString
        {
            get
            {
                return (string)Row[c_champCodesRolesSecondaires];
            }
            set
            {
                Row[c_champCodesRolesSecondaires] = value;
            }
        }

        //-----------------------------------------------------------
        public bool HasRoleSecondaire ( string strCodeRole )
        {
            return CodesRolesSecondairesString.Contains("~"+strCodeRole+"~");
        }

        //-----------------------------------------------------------
        public string[] CodesRolesSecondaires
        {
            get
            {
                string[] strCodes = CodesRolesSecondairesString.Split('~');
                List<string> lstCodes = new List<string>();
                foreach (string strCode in strCodes)
                {
                    if (strCode.Length > 0)
                        lstCodes.Add(strCode);
                }
                return lstCodes.ToArray();
            }
            set
            {
                if (value != null)
                {
                    StringBuilder bl = new StringBuilder();
                    foreach (string strCode in value)
                    {
                        bl.Append("~");
                        bl.Append(strCode);
                    }
                    if (bl.Length > 0)
                        bl.Append("~");
                    CodesRolesSecondairesString = bl.ToString();
                }
                else
                    CodesRolesSecondairesString = "";
            }
        }

        //------------------------------------------------------------------------
        public static CFiltreData GetFiltreChampsForRole(string strCodeRole)
        {
            return new CFiltreData(c_champCodeRole + "=@1 or " +
                c_champCodesRolesSecondaires + " like @2",
                strCodeRole,
                "%~" + strCodeRole + "~%");
        }

        //------------------------------------------------------------------------
        public static CListeObjetsDonnees GetListeChampsForRole(CContexteDonnee ctx,
            string strCodeRole)
        {
            CListeObjetsDonnees lst = new CListeObjetsDonnees(ctx, typeof(CChampCustom),
                GetFiltreChampsForRole ( strCodeRole ));
            return lst;
        }


        //-----------------------------------------------------------
        public void AddRoleSecondaire(string strCodeRole)
        {
            bool bTrouve = false;
            List<string> lstNew = new List<string>();
            foreach (string strCode in CodesRolesSecondaires)
            {
                if (strCode == strCodeRole)
                    bTrouve = true;
                lstNew.Add(strCode);
            }
            if (!bTrouve)
                lstNew.Add(strCodeRole);
            CodesRolesSecondaires = lstNew.ToArray();
        }

        //-----------------------------------------------------------
        public void RemoveRoleSecondaire(string strCodeRole)
        {
            List<string> lstNew = new List<string>();
            foreach (string strCode in CodesRolesSecondaires)
            {
                if (strCode != strCodeRole)
                    lstNew.Add(strCode);
            }
            CodesRolesSecondaires = lstNew.ToArray();
        }




		////////////////////////////////////////////////////////////
		[DynamicField("Role")]
		public CRoleChampCustom Role
		{
			get
			{
				return CRoleChampCustom.GetRole((string)Row[c_champCodeRole]);
			}
			set
			{
				if ( value == null )
					Row[c_champCodeRole] = DBNull.Value;
				else
					Row[c_champCodeRole] = value.CodeRole;
			}
		}

		/// ///////////////////////////////////////////////////////
		[TableFieldProperty(c_champFormuleValidation, 3000)]
		public string DataFormuleValidation
		{
			get
			{
				return (string)Row[c_champFormuleValidation];
			}
			set
			{
				Row[c_champFormuleValidation] = value;
			}
		}

		/// ///////////////////////////////////////////////////////
		[TableFieldProperty(c_champCacheFormule, IsInDb = false)]
		public C2iExpression FormuleValidation
		{
			get
			{
				if (Row[c_champCacheFormule] == DBNull.Value)
				{
					C2iExpression expression = C2iExpression.FromPseudoCode(DataFormuleValidation);
					if (expression == null)
						expression = new C2iExpressionVrai();
					CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheFormule, expression);
				}
				return (C2iExpression)Row[c_champCacheFormule];
			}
			set
			{
				if (value == null)
					DataFormuleValidation = "";
				else
					DataFormuleValidation = C2iExpression.GetPseudoCode(value);
				CContexteDonnee.ChangeRowSansDetectionModification(Row.Row, c_champCacheFormule, DBNull.Value);
			}
		}

		

		////////////////////////////////////////////////////////////
		[TableFieldProperty(c_champInfoErreurFormat, 255)]
		public string TexteErreurFormat
		{
			get
			{
				return (string)Row[c_champInfoErreurFormat];
			}
			set
			{
				Row[c_champInfoErreurFormat] = value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique un nom de champ 'convivial' pour ce champ. Ce nom peut �tre utilis� par exemple
        /// dans des rapports pour pr�senter le champ sous un autre nom que son nom "syst�me"
		/// </summary>
        [TableFieldProperty(c_champLibelleConvivial, 255 )]
		[DynamicField("Friendly label")]
		[DescriptionField]
		public virtual string LibelleConvivial
		{
			get
			{
				string str = (string)Row[c_champLibelleConvivial];
				if ( str == "" )
					str = Nom;
				return str;
			}
			set
			{
				Row[c_champLibelleConvivial] = value;
			}
		}
		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique dans quel dossier sera rang� ce champ.
		/// </summary>
        /// <remarks>
        ///  Lorsque l'application pr�sente une liste des champs existants
        /// pour un type d'entit� donn�, il range chaque champ dans un dossier afin de permettre � l'utilisateur
        /// de retrouver plus rapidement ces champs.
        /// <BR></BR>
        /// La propri�t� 'Folder' permet d'indiquer le dossier dans lequel sera rang� ce champ<BR></BR>
        /// Lors de la cr�ation de champs, il est recommand� d'utiliser des dossier (folder) afin de simplifier et d'optimiser
        /// l'utilisation des informations par l'utilisateur final de l'application, mais �galement pour permettre � l'administrateur
        /// de 'ranger' ses champs.
        /// </remarks>
        [TableFieldProperty(CChampCustom.c_champFolder, 64)]
		[DynamicField("Folder")]
		public string Categorie
		{
			get
			{
				return (string)Row[c_champFolder];
			}
			set
			{
				Row[c_champFolder] = value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique un libell� court pour ce champ. Ce libell� peut �tre utilis� par exemple dans des rapports.
		/// </summary>
        [TableFieldProperty(c_champLibelleCourt, 255)]
		[DynamicField("Short label")]
		public string LibelleCourt
		{
			get
			{
				string str = (string)Row[c_champLibelleCourt];
				if ( str == "" )
					str = LibelleConvivial;
				return str;
			}
			set
			{
				Row[c_champLibelleCourt] = value;
			}
		}

        
		//-----------------------------------------------------------
		/// <summary>
		/// Identififiant de la classe d'unit� utilis�e par
        /// ce champ personnalis�
		/// </summary>
		[TableFieldProperty ( c_champClasseUnite, 64)]
		[DynamicField("Unit class code")]
		public string ClasseUniteCode
		{
			get
			{
				return (string)Row[c_champClasseUnite];
			}
			set
			{
				Row[c_champClasseUnite] = value;
			}
		}

        //-----------------------------------------------------------
        public IClasseUnite ClasseUnite
        {
            get
            {
                return CGestionnaireUnites.GetClasse(ClasseUniteCode);
            }
            set
            {
                if (value != null)
                    ClasseUniteCode = value.GlobalId;
                else
                    ClasseUniteCode = "";
            }
        }

        
		//-----------------------------------------------------------
		/// <summary>
		/// Format � utiliser par d�faut pour ce champ lors de l'affichage
        /// d'une valeur avec unit�.
		/// </summary>
		[TableFieldProperty ( c_champFormatUnite, 64)]
		[DynamicField("Unit display format")]
		public string FormatAffichageUnite
		{
			get
			{
				return (string)Row[c_champFormatUnite];
			}
			set
			{
				Row[c_champFormatUnite] = value;
			}
		}



		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique la position des flags de lecture seule
		/// </summary>
		/// <remarks>
		/// Lors de la modification d'un champ, une combinaison binaire est faite
		/// entre ce flag et celui de la session de l'utilisateur. S'il est diff�rent de 0,
		/// le champ est en lecture seule
		/// </remarks>
		[TableFieldProperty(c_champRestrictionReadOnly)]
		[DynamicField("Readonly restriction")]
		public int RestrictionsReadOnly
		{
			get
			{
				return ( int  )Row[c_champRestrictionReadOnly];
			}
			set
			{
				Row[c_champRestrictionReadOnly] = value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique la position des flags de masquage des donn�es du champ
		/// </summary>
		/// <remarks>
		/// Lors de la modification d'un champ, une combinaison binaire est faite
		/// entre ce flag et celui de la session de l'utilisateur. S'il est diff�rent de 0,
		/// le champ est masqu�
		/// </remarks>
		[TableFieldProperty(c_champRestrictionMasquer)]
		[DynamicField("Hide restriction")]
		public int RestrictionsMasquer
		{
			get
			{
				return ( int  )Row[c_champRestrictionMasquer];
			}
			set
			{
				Row[c_champRestrictionMasquer] = value;
			}
		}

		////////////////////////////////////////////////////////////
		/// <summary>
		/// Indique la liste des valeurs possibles pour ce champ.
		/// </summary>
        [RelationFille(typeof(CValeurChampCustom), "ChampCustom")]
		[DynamicChilds("Values", typeof (CValeurChampCustom) )]
		public CListeObjetsDonnees ListeValeurs
		{
			get
			{
				return GetDependancesListe(CValeurChampCustom.c_nomTable, CChampCustom.c_champId);
			}
		}
		
		////////////////////////////////////////////////////////////
		public IList Valeurs
		{
			get
			{
				if (TypeDonneeChamp.TypeDonnee == sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto && 
					TypeObjetDonnee != null)
				{
					CListeObjetsDonnees liste = new CListeObjetsDonnees(ContexteDonnee, TypeObjetDonnee);
					liste.AppliquerFiltreAffichage = true;
					CFiltreDynamique filtreDyn = FiltreObjetDonnee;
					if (filtreDyn != null)
					{
						CResultAErreur result = filtreDyn.GetFiltreData();
						if (result)
							liste.Filtre = (CFiltreData)result.Data;
					}
					return liste;
				}
				return ListeValeurs;
			}
		}

		////////////////////////////////////////////////////////////
		public object ValueFromDisplay(string strDisplay)
		{
			foreach(CValeurChampCustom valeur in this.ListeValeurs)
			{
				if (valeur.Display == strDisplay)
					return valeur.Value;
			}

			return null;
		}
		
		/// ///// ///// ///// ///// ///// ///// ///// ///// //
		public string DisplayFromValue(object value)
		{
			foreach (CValeurChampCustom valeur in this.ListeValeurs)
			{
				if (valeur.ValueString == C2iTypeDonnee.TypeToString(value))
					return valeur.Display;
			}

			return null;
		}

		////////////////////////////////////////////////////////////
		public CResultAErreur IsCorrectValue(object value)
		{
			CResultAErreur result = CResultAErreur.True;
            if (value is CValeurUnite)
                value = ((CValeurUnite)value).Valeur;

			if(value != null && !this.TypeDonneeChamp.IsDuBonType(value))
				return CResultAErreur.False;

			if ( result )
				result = TesteValeur ( value );

			if ( !result )
				return result;

			if (this.ListeValeurs.Count == 0)
				return result;


			if ( DisplayFromValue(value) != null )
				return result;
            if ( value != null )
			    result.EmpileErreur(I.T("The value '@1' isn't a possible value|135", value != null?value.ToString():"NULL"));
			return result;
		}

		/// ///// ///// ///// ///// ///// ///// ///// ///// //
		public override string ToString()
		{
			return Nom;
		}

		/// ///// ///// ///// ///// ///// ///// ///// ///// //
		///<summary>
        ///Pr�sente une liste des formulaires qui utilisent ce champ
        ///</summary>
        ///<remarks>
        ///Attention, cette liste ne pr�sente que la liste des formulaires qui utilisent directement ce champ
        ///en utilisant une zone 'data'. Les champs exploit�s dans des sous formulaires ou des formulaires dynamiques
        ///ne sont pas list�s ici.
        ///</remarks>
        [RelationFille(typeof(CRelationFormulaireChampCustom), "Champ")]
		[DynamicChilds("Forms relations", typeof ( CRelationFormulaireChampCustom ))]
		public CListeObjetsDonnees RelationsFormulaire
		{
			get
			{
				return GetDependancesListe ( CRelationFormulaireChampCustom.c_nomTable, c_champId );
			}
		}

		//////// ///// ///// ///// ///// ///// ///// ///// //
		/// <summary>
		/// Cl� utilis�e pour identifier le champ dans un CRestrictionSurType
		/// </summary>
		public string CleRestriction
		{
			get
			{
				return GetCleRestriction(DbKey);
			}
		}

		//////// ///// ///// ///// ///// ///// ///// ///// //
		public static string GetCleRestriction(CDbKey dbKey)
		{
            return c_prefixeCleRestriction + dbKey.StringValue;
		}
        		

		/// ///// ///// ///// ///// ///// ///// ///// ///// //
		protected CResultAErreur TesteValeur ( object valeur )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( TypeDonneeChamp.TypeDonnee == sc2i.data.dynamic.TypeDonnee.tObjetDonneeAIdNumeriqueAuto )
			{
				if ( valeur != null && !TypeObjetDonnee.IsAssignableFrom ( valeur.GetType() ))
				{
					result.EmpileErreur(I.T("The '@1' field value is incorrect|10007",LibelleConvivial));
					return result;
				}
			}
					
			object obj = CObjetForTestValeurChampCustom.GetNewFor(this, valeur);
			if ( obj == null )
			{
				result.EmpileErreur(I.T("Impossible to test the value '@1'|10008",valeur.ToString()));
				return result;
			}
			CContexteEvaluationExpression contexte = new CContexteEvaluationExpression(obj);
			result = FormuleValidation.Eval ( contexte );
			if ( !result )
				return result;
			if ( (result.Data is bool && (bool)result.Data) || result.Data.ToString() =="1" )
				return CResultAErreur.True;
			else
			{
				result.Data = null;
                if (TexteErreurFormat != string.Empty)
                    result.EmpileErreur(TexteErreurFormat);
                else
                    result.EmpileErreur(I.T("Incorect value in the field '@1'|244", this.Nom));
			}
			return result;
		}

        //------------------------------------------------------------------------------
        public void ClearAllCustomFieldValues()
        {
            
        }

        //------------------------------------------------------------------------------
        public string GetDisplayValue(object valeur)
        {
            if (valeur == null)
                return "";
            foreach (CValeurChampCustom v in ListeValeurs)
            {
                if (v != null)
                {
                    if (v.Value.Equals(valeur))
                    {
                        return v.Display;
                    }
                }
            }
            return "";
        }

        public static List<string> GetListeRubriques(CContexteDonnee contexteDonnee)
        {
            C2iRequeteAvancee requete = new C2iRequeteAvancee(contexteDonnee.IdVersionDeTravail);
            C2iChampDeRequete champ = new C2iChampDeRequete(
                CChampCustom.c_champFolder, new CSourceDeChampDeRequete(CChampCustom.c_champFolder),
                typeof(string), OperationsAgregation.None, true);
            requete.ListeChamps.Add(champ);
            requete.TableInterrogee = CChampCustom.c_nomTable;
            CResultAErreur result = requete.ExecuteRequete(contexteDonnee.IdSession);
            HashSet<string> setRubriques = new HashSet<string>();
            if (result && result.Data is DataTable)
            {
                DataTable table = result.Data as DataTable;
                foreach ( DataRow row in table.Rows )
                    if ( row[0] != DBNull.Value )
                        setRubriques.Add ( row[0].ToString() );
            }

            requete = new C2iRequeteAvancee(contexteDonnee.IdVersionDeTravail);
            champ = new C2iChampDeRequete(
                CChampCalcule.c_champRubrique, new CSourceDeChampDeRequete(CChampCalcule.c_champRubrique),
                typeof(string), OperationsAgregation.None, true);
            requete.ListeChamps.Add(champ);
            requete.TableInterrogee = CChampCalcule.c_nomTable;
            result = requete.ExecuteRequete(contexteDonnee.IdSession);
            if (result && result.Data is DataTable)
            {
                DataTable table = result.Data as DataTable;
                foreach ( DataRow row in table.Rows )
                    if ( row[0] != DBNull.Value )
                        setRubriques.Add ( row[0].ToString() );
            }
            List<string> lstRubriques = new List<string>();
            foreach ( string strRub in setRubriques )
                lstRubriques.Add ( strRub );
            lstRubriques.Sort ( (x,y)=>x.CompareTo(y));
            return lstRubriques;
        }

        #region IObjetCherchable Membres

        public CRequeteRechercheObjet GetRequeteRecherche()
        {
            CDefinitionProprieteDynamiqueChampCustom defC = new CDefinitionProprieteDynamiqueChampCustom(this);
            return new CRequeteRechercheObjet(
                I.T("Search field '@1'|20038", Nom),
                defC);
        }

        #endregion
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    [AutoExec("Autoexec")]
    public class CConvertisseurCleRestriction : IConvertisseurAncienneCleRestrictionEnNouvelleCleCompatibleIdUniversel
    {
        public CConvertisseurCleRestriction()
        {
        }

        public static void Autoexec()
        {
            CRestrictionUtilisateurSurType.RegisterConvertisseurCleRestriction(new CConvertisseurCleRestriction());
        }

        public string GetCleRestrictionCompatible(string strAncienneCleRestriction)
        {
            if (strAncienneCleRestriction.Length > CChampCustom.c_prefixeCleRestriction.Length && 
                strAncienneCleRestriction.Substring(
                0, CChampCustom.c_prefixeCleRestriction.Length) == CChampCustom.c_prefixeCleRestriction)
            {
                // Lire l'Id du Champ Custom
                int nIdChamp = -1;
                string strExtractIdChamp = strAncienneCleRestriction.Substring(CChampCustom.c_prefixeCleRestriction.Length);
                try
                {
                    nIdChamp = Int32.Parse(strExtractIdChamp);
                    CChampCustom champ = new CChampCustom(CContexteDonneeSysteme.GetInstance());
                    if (champ.ReadIfExists(nIdChamp))
                    {
                        return champ.CleRestriction;
                    }
                }
                catch { }
            }

            return strAncienneCleRestriction;
        }
        
    }
	
}
