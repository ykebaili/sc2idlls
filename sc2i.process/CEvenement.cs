using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;


using sc2i.expression;
using sc2i.common;
using sc2i.data;
using sc2i.data.dynamic;
using sc2i.multitiers.client;
using sc2i.process.recherche;
using sc2i.common.recherche;
using sc2i.data.dynamic.recherche;
using System.Text;


namespace sc2i.process
{
	#region RelationDefinisseurToEvenement
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class RelationDefinisseurToEvenementAttribute : RelationTypeIdAttribute
	{
		public override string TableFille
		{
			get
			{
				return CEvenement.c_nomTable;
			}
		}

		//////////////////////////////////////
		public override int Priorite
		{
			get
			{
				return 340;
			}
		}

		protected override string MyIdRelation
		{
			get
			{
				return "DEF_EVT";
			}
		}

		
		public override string ChampId
		{
			get
			{
				return CEvenement.c_champIdDefinisseur;
			}
		}

		public override string ChampType
		{
			get
			{
				return CEvenement.c_champTypeDefinisseur;
			}
		}
		
		public override bool Composition
		{
			get
			{
				return true;
			}
		}
		public override bool CanDeleteToujours
		{
			get
			{
				return true;
			}
		}

		public override string NomConvivialPourParent
		{
			get
			{
				return I.T("Dependent events|262");
			}
		}

		protected override bool MyIsAppliqueToType(Type tp)
		{
			return typeof(IDefinisseurEvenements).IsAssignableFrom ( tp );
		}
	}
	#endregion
	/// <summary>
	/// Un �v�nement est une suite s�quentielle d'actions �l�mentaires, appel�es briques.<br/>
    /// Chaque brique est li�e � une autre brique via un ou plusieurs liens. Chaque �v�nement<br/>
    /// est un programme, charg� de r�aliser des op�rations de mani�re automatique dans l'application.<br/>
    /// Un �v�nement, pour s'ex�cuter, doit �tre d�clench� (d'o� le terme d'�v�nement);<br/>
    /// Le d�clenchement peut �tre manuel ou automatique sur cr�ation, modification,<br/>
    /// suppression d'une entit� TIMOS, sur une date ou sp�cifique au type d'�l�ment.
	/// </summary>
	public enum TypeEvenement
	{
		Creation = 0,//Sur cr�ation d'un �l�ment du type
		Modification,//Sur modification
		Date,//Programmation sur une date
		Manuel,//lancement manuel
		Specifique,//Sp�cifique au type d'�l�ment
        Suppression//sur suppression
	}

	[Table(CEvenement.c_nomTable, CEvenement.c_champId, false)]
	[ObjetServeurURI("CEvenementServeur")]
	[DynamicClass("Event")]
	[RelationDefinisseurToEvenement]
	public class CEvenement : CObjetDonneeAIdNumeriqueAuto,
		IDeclencheurActionManuelle, IObjetALectureTableComplete
	{
		public const string c_nomTable = "EVENTS";
		public const string c_champId = "EVT_ID";
		public const string c_champLibelle = "EVT_LABEL";
		public const string c_champTypeEvenement = "EVT_TYPE";
		public const string c_champIdEvenementSpecifique = "EVT_SPECIFIC_TYPE_ID";
		public const string c_champMenu = "EVT_MANUAL_MENU";
		public const string c_champIdsGroupesManuels = "EVT_MANUAL_GRP_IDS";
		public const string c_champCondition = "EVT_CONDITION";
		public const string c_champProprieteSurveillee = "EVT_MONITORED_PROP";
		public const string c_champValeurAvant = "EVT_FORMULA_VALUE_BEFORE";
		public const string c_champValeurApres = "EVT_FORMULA_VALUE_AFTER";
		public const string c_champTypeCible = "EVT_TARGET_TYPE";
		public const string c_champActif = "EVT_ACTIVE";
		public const string c_champFormuleDate = "EVT_DATE_FORMULA";
		public const string c_champCodeDeclencheur  ="EVT_LAUNCHER_CODE";
		public const string c_champTypeDefinisseur = "EVT_DEFINER_TYPE";
		public const string c_champIdDefinisseur = "EVT_DEFINER_ID";
		public const string c_champDataProcessPropre = "EVT_DATA_PROCESS";
		public const string c_champDeclenchementUniqueParEntite = "EVT_UNIQUE_LAUNCH";
        public const string c_champOrdreExecution = "EVT_EXEC_ORDER";
        public const string c_champTableau = "EVT_TGT_IS_ARRAY";
		public const string c_champDeclencherSurContexteClient = "EVT_RUN_ON_CLIENT";
		public const string c_champMasquerProgress = "EVT_HIDE_PROGRESS";
        public const string c_champContextesException = "EVT_EXCEPTION_CTX";

		/// /////////////////////////////////////////////////////////
		public CEvenement( CContexteDonnee contexte )
			:base ( contexte )
		{
		}

		/// /////////////////////////////////////////////////////////
		public CEvenement ( DataRow row )
			:base ( row )
		{
		}

		/// /////////////////////////////////////////////////////////
		public override string DescriptionElement
		{
			get
			{
				return I.T("The @1 event|263",Libelle);
			}
		}

		/// /////////////////////////////////////////////////////////
		public override string[] GetChampsTriParDefaut()
		{
			return new string[]{c_champLibelle};
		}

		/// /////////////////////////////////////////////////////////
		protected override void MyInitValeurDefaut()
		{
			Actif = true;
			TypeEvenement = TypeEvenement.Modification;
            Tableau = false;
			DeclencherSurContexteClient = false;
			HideProgress = false;
		}

		/// /////////////////////////////////////////////////////////
		public override CResultAErreur VerifieDonnees(bool bAuMomentDeLaSauvegarde)
		{
			CResultAErreur result = CResultAErreur.True;
			if (TypeEvenement == TypeEvenement.Date)
			{
				if (FormuleDateSurveillee == null)
					result.EmpileErreur(I.T("The date formula is incorrect|264"));
				else
				{
					if (FormuleDateSurveillee.TypeDonnee.TypeDotNetNatif != typeof(DateTime) &&
                        FormuleDateSurveillee.TypeDonnee.TypeDotNetNatif != typeof(DateTime?) && 
                        FormuleDateSurveillee.TypeDonnee.TypeDotNetNatif != typeof(CDateTimeEx))
					{
						result.EmpileErreur(new CErreurValidation(I.T("The date formula doesn't return a date type|265"), true));
					}
				}
			}
			if (!result)
				return result;
			return base.VerifieDonnees(bAuMomentDeLaSauvegarde);
		}

		//------------------------------------------------------------------
        /// <summary>
        /// Si VRAI, fait que l'�v�nement est d�clench� avant la sauvegarde en base de donn�es,
        /// dans le cas contraire, d�clenche apr�s sauvegarde.
        /// </summary>
		[TableFieldProperty(c_champDeclencherSurContexteClient)]
		[DynamicField("Run on client context")]
		public bool DeclencherSurContexteClient
		{
			get
			{
				bool bVal = (bool)Row[c_champDeclencherSurContexteClient];
				return bVal;
			}
			set
			{
				bool bValeur = value;
				//V�rifie que le process est compatible
				if (bValeur && ProcessADeclencher != null)
					bValeur &= ProcessADeclencher.PeutEtreExecuteSurLePosteClient;
				Row[c_champDeclencherSurContexteClient] = bValeur;
			}
		}

		//-----------------------------------------------------------------
        /// <summary>
        /// Libell� de l'�v�nement
        /// </summary>
		[TableFieldProperty(c_champLibelle,255)]
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

		//--------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
		[TableFieldProperty(c_champCodeDeclencheur, 255)]
		[DynamicField("Release code")]
		public string CodeDeclencheur
		{
			get
			{
				return(string)Row[c_champCodeDeclencheur];
			}
			set
			{
				Row[c_champCodeDeclencheur] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeEvenement)]
		public TypeEvenement TypeEvenement
		{
			get
			{
				return (TypeEvenement)Row[c_champTypeEvenement];
			}
			set
			{
				Row[c_champTypeEvenement] = (int)value;
			}
		}

		/// <summary>
		/// Pour un �v�nement manuel, si VRAI, indique que cet �v�nement doit se d�rouler<br/> 
		/// sans montrer la barre de progression correspondante.
		/// </summary>
		[DynamicField("Hide progress")]
		[TableFieldProperty(CEvenement.c_champMasquerProgress)]
		public bool HideProgress
		{
			get
			{
				return (bool)Row[c_champMasquerProgress];
			}
			set
			{
				Row[c_champMasquerProgress] = value;
			}
		}

        /// <summary>
        /// Contextes d'exception s�par�s par des caract�res ";"<br/>
        /// Lorsque le contexte en cours correspond � l'un des<br/>
        /// contextes d'exception, l'�v�nement ne d�clenche pas.
        /// </summary>
        [DynamicField("Exception contexts")]
        [TableFieldProperty(CEvenement.c_champContextesException, 500)]
        public string ContexteExceptionComma
        {
            get
            {
                return (string)Row[c_champContextesException];
            }
            set
            {
                Row[c_champContextesException] = value;
            }
        }

        public HashSet<string> ContextesException
        {
            get
            {
                HashSet<string> dic = new HashSet<string>();
                string[] strCtxs = ContexteExceptionComma.Split(';');
                foreach (string strCtx in strCtxs)
                {
                    if ( strCtx.Length > 0 )
                        dic.Add(strCtx);
                }
                return dic;
            }
            set
            {
                StringBuilder bl = new StringBuilder();
                foreach (string strVal in value)
                {
                    bl.Append(strVal);
                    bl.Append(';');
                }
                if (bl.Length > 0)
                    bl.Remove(bl.Length - 1, 1);
                ContexteExceptionComma = bl.ToString();
            }
        }


		////////////////////////////////////////////////////////////
		/// <summary>
		/// Si evenement sp�cifique, indique l'identifiant de l'�venement
		/// sp�cifique (li� � un atrribut EvenementAttribute) correspondant
		/// � ce d�clenchement
		/// </summary>
		[TableFieldProperty(CEvenement.c_champIdEvenementSpecifique, 128)]
		public string IdEvenementSpecifique
		{
			get
			{
				return (string)Row[c_champIdEvenementSpecifique];
			}
			set
			{
				Row[c_champIdEvenementSpecifique] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		/// <summary>
		/// Menu permettant l'acc�s � l'�v�nement dans le cas d'un �v�nement manuel
        /// <br/>
        /// <br/>
		/// Chaque �l�ment de menu est s�par� par un "/"
		/// </summary>
		[TableFieldProperty(c_champMenu, 255)]
		[DynamicField("Manual menu")]
		public string MenuManuel
		{
			get
			{
				return (string)Row[c_champMenu];
			}
			set
			{
				Row[c_champMenu] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		/// <summary>
		/// Ids des groupes pour lesquels l'utilisation manuelle est autoris�e<BR></BR>
		/// Les ids de groupe sont entour�s par des #
		/// </summary>
		[TableFieldProperty(c_champIdsGroupesManuels, 512)]
		public string KeysGroupesPourExecutionManuelleString
		{
			get
			{
				return (string)Row[c_champIdsGroupesManuels];
			}
			set
			{
				Row[c_champIdsGroupesManuels] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
        public CDbKey[] KeysGroupesPourExecutionManuelle
        {
            get
            {
                string[] strVals = KeysGroupesPourExecutionManuelleString.Split('#');
                List<CDbKey> lst = new List<CDbKey>();
                foreach (string strVal in strVals)
                {
                    if (strVal.Length > 0)
                    {
                        try
                        {
                            //TESTDBKEYOK les groupes pour ex�cution manuelle ne sont plus exploit�s
                            int nId = 0;
                            CDbKey key = null;
                            if (int.TryParse(strVal, out nId))
                                key = CDbKey.GetNewDbKeyOnIdAUtiliserPourCeuxQuiNeGerentPasLeDbKey(nId);
                            else
                                key = CDbKey.CreateFromStringValue(strVal);
                            if ( key != null )
                                lst.Add(key);
                        }
                        catch
                        {
                        }
                    }
                }
                return lst.ToArray();
            }
            set
            {
                string strListe = "";
                if (value != null)
                {
                    foreach (CDbKey key in value)
                        strListe += key.StringValue + "#";
                    if (strListe.Length > 0)
                        strListe = "#" + strListe;
                }
                KeysGroupesPourExecutionManuelleString = strListe;
            }
        }


		// /////////////////////////////////////////////////////////
		/// <summary>
		/// Si VRAI, indique que l'�v�nement ne pourra �tre d�clench� qu'une seule fois (avec succ�s) par entit�.
        /// <br/>
        /// <br/>
		/// Cette propri�t� permet de s'assurer qu'un �v�nement n'est pas d�clench� plusieurs fois pour la 
		/// m�me entit�.
        /// <br/>
        /// <br/>
		/// Avant de d�clencher l'�v�nement, le syst�me v�rifie qu'il n'existe aucun process en ex�cution
		/// d�clench� pour la m�me entit� par cet �v�nement. Le process en ex�cution doit avoir un �tat
		/// diff�rent de 'Erreur (30)'
        /// </summary>
		[TableFieldProperty ( c_champDeclenchementUniqueParEntite )]
		[DynamicField("One release by entity")]
		public bool DeclenchementUniqueParEntite
		{
			get
			{
				return ( bool )Row[c_champDeclenchementUniqueParEntite];
			}
			set
			{
				Row[c_champDeclenchementUniqueParEntite] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		/// <summary>
		/// Lorsque plusieurs �v�nements s'ex�cutent pour une m�me entit�, ils sont ex�cut�s<br/>
		/// dans l'ordre croissant de ce num�ro; lorsqu'un m�me num�ro est attribu� � diff�rents<br/>
        /// �v�nements (par exemple 0), l'ordre de d�clenchement n'est pas garanti.
		/// </summary>
		[TableFieldProperty( c_champOrdreExecution ) ]
		[DynamicField("Execution sequence")]
		public int OrdreExecution
		{
			get
			{
				return (int)Row[c_champOrdreExecution];
			}
			set
			{
				Row[c_champOrdreExecution] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champCondition, 1024)]
		public string FormuleConditionString
		{
			get
			{
				return( string)Row[c_champCondition];
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
				if ( expression == null )
					expression = new C2iExpressionVrai();
				return expression;
			}
			set
			{
				FormuleConditionString = C2iExpression.GetPseudoCode ( value );
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champProprieteSurveillee, 256)]
		public string ProprieteSurveilleeString
		{
			get
			{
				return (string)Row[c_champProprieteSurveillee];
			}
			set
			{
				Row[c_champProprieteSurveillee]=value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public CDefinitionProprieteDynamique ProprieteSurveillee
		{
			get
			{
				string strData = ProprieteSurveilleeString;
				if ( strData == "" )
					return null;
				CStringSerializer serializer = new CStringSerializer(strData, ModeSerialisation.Lecture );
				I2iSerializable objet = null;
				if ( !serializer.TraiteObject ( ref objet ) )
					return null;
				if ( !(objet is CDefinitionProprieteDynamique) )
					return null;
				return (CDefinitionProprieteDynamique)objet;
			}
			set
			{
				if ( value == null )
					ProprieteSurveilleeString = "";
				else
				{
					String strData = "";
					CStringSerializer serializer = new CStringSerializer ( strData, ModeSerialisation.Ecriture );
					I2iSerializable objet = (I2iSerializable)value;
					serializer.TraiteObject ( ref objet );
					ProprieteSurveilleeString = serializer.String;
				}
			}
		}

		

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champValeurAvant, 255)]
		public string FormuleValeurAvantString
		{
			get
			{
				return (string)Row[c_champValeurAvant];
			}
			set
			{
				Row[c_champValeurAvant] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public C2iExpression FormuleValeurAvant
		{
			get
			{
				C2iExpression expression = C2iExpression.FromPseudoCode(FormuleValeurAvantString);
				return expression;
			}
			set
			{
				FormuleValeurAvantString = C2iExpression.GetPseudoCode ( value );
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champValeurApres, 255)]
		public string FormuleValeurApresString
		{
			get
			{
				return (string)Row[c_champValeurApres];
			}
			set
			{
				Row[c_champValeurApres] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public C2iExpression FormuleValeurApres
		{
			get
			{
				C2iExpression expression = C2iExpression.FromPseudoCode(FormuleValeurApresString);
				return expression;
			}
			set
			{
				FormuleValeurApresString = C2iExpression.GetPseudoCode ( value );
			}
		}

		/// /////////////////////////////////////////////////////////
		//Formule appliqu�e au champ date surveill� pour indiquer la date
		//de survenance de l'�venement
		[TableFieldProperty(c_champFormuleDate, 255)]
		public string FormuleDateSurveilleeString
		{
			get
			{
				return (string)Row[c_champFormuleDate];
			}
			set
			{
				Row[c_champFormuleDate] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public C2iExpression FormuleDateSurveillee
		{
			get
			{
				C2iExpression expression = C2iExpression.FromPseudoCode(FormuleDateSurveilleeString);
				return expression;
			}
			set
			{
				FormuleDateSurveilleeString = C2iExpression.GetPseudoCode ( value );
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeCible, 255)] 
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

		/// /////////////////////////////////////////////////////////
		public Type TypeCible 
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeCibleString );
			}
			set
			{
				if ( value != null )
					TypeCibleString = value.ToString();
			}
		}

		//-------------------------------------------------------------
        /// <summary>
        /// Type d'entit� sur lequel l'�v�nement doit se d�clencher<br/>
        /// (exemple : Equipment, Sites, etc.)
        /// </summary>
		[DynamicField("Target type")]
		public string TypeCibleConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial(TypeCible);
			}
		}

		//--------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
		[TableFieldProperty(c_champActif)]
		[DynamicField("Active")]
		public bool Actif
		{
			get
			{
				return (bool)Row[c_champActif];
			}
			set
			{
				Row[c_champActif] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		[TableFieldProperty(c_champTypeDefinisseur, 255)]
		public string TypeDefinisseurString
		{
			get
			{
				return ( string )Row[c_champTypeDefinisseur];
			}
			set
			{
				Row[c_champTypeDefinisseur] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		public Type TypeDefinisseur
		{
			get
			{
				return CActivatorSurChaine.GetType ( TypeDefinisseurString );
			}
			set
			{
				if ( value != null )
					TypeDefinisseurString = value.ToString();
				else
					TypeDefinisseurString = "";
			}
		}

		//----------------------------------------------------------
        /// <summary>
        /// Type convivial de l'objet �ventuel, � partir duquel l'�v�nement a �t� cr��
        /// </summary>
		[DynamicField("Definer type")]
		public string TypeDefinsseurConvivial
		{
			get
			{
				return DynamicClassAttribute.GetNomConvivial(TypeDefinisseur);
			}
		}

		//-------------------------------------------------------------
        /// <summary>
        /// Identifiant (ID) de l'objet �ventuel, � partir duquel l'�v�nement a �t� cr��
        /// </summary>
		[TableFieldProperty(c_champIdDefinisseur)]
		[DynamicField("Definer ID")]
		public int IdDefinisseur
		{
			get
			{
				return ( int )Row[c_champIdDefinisseur];
			}
			set
			{
				Row[c_champIdDefinisseur] = value;
			}
		}

		/// /////////////////////////////////////////////////////////
		/// L'�l�ment qui d�finit cet �venement
		public IDefinisseurEvenements Definisseur
		{
			get
			{
				Type tp = TypeDefinisseur;
				if ( tp == null || !(tp.IsSubclassOf(typeof(CObjetDonneeAIdNumerique))))
					return null;
				CObjetDonneeAIdNumerique objet = (CObjetDonneeAIdNumerique)Activator.CreateInstance ( tp, new object[]{ContexteDonnee} );
				if ( objet != null && objet.ReadIfExists ( IdDefinisseur ) )
					return (IDefinisseurEvenements)objet;
				return null;
			}
			set
			{
				if ( value == null )
				{
					TypeDefinisseur = null;
					IdDefinisseur = -1;
				}
				else
				{
					TypeDefinisseur = value.GetType();
					IdDefinisseur = value.Id;
				}
			}
		}
		
		/// /////////////////////////////////////////////////////////
		public CParametreDeclencheurEvenement ParametreDeclencheur
		{
			get
			{
				CParametreDeclencheurEvenement parametre = new CParametreDeclencheurEvenement();
				parametre.TypeCible = TypeCible;
				parametre.TypeEvenement = TypeEvenement;
				parametre.IdEvenementSpecifique = IdEvenementSpecifique;
				parametre.ProprieteASurveiller = ProprieteSurveillee;
				parametre.FormuleConditionDeclenchement = FormuleCondition;
				parametre.FormuleDateProgramme = FormuleDateSurveillee;
				parametre.FormuleValeurApres = FormuleValeurApres;
				parametre.FormuleValeurAvant = FormuleValeurAvant;
				parametre.MenuManuel = this.MenuManuel;
                //TESTDBKEYOK les groupes pour ex�cution manuelle ne sont plus exploit�s (Avril 2014)
				parametre.KeysGroupesManuel = KeysGroupesPourExecutionManuelle;
				parametre.OrdreExecution = OrdreExecution;
				parametre.HideProgress = HideProgress;
                parametre.ContextesException = ContextesException;
                parametre.Code = CodeDeclencheur;
				
				return parametre;
			}
			set
			{
				if ( value == null )
					return;
				TypeCible = value.TypeCible;
				TypeEvenement = value.TypeEvenement;
				IdEvenementSpecifique = value.IdEvenementSpecifique;
				ProprieteSurveillee = value.ProprieteASurveiller;
				FormuleCondition = value.FormuleConditionDeclenchement;
				FormuleDateSurveillee = value.FormuleDateProgramme;
				FormuleValeurApres = value.FormuleValeurApres;
				FormuleValeurAvant = value.FormuleValeurAvant;
				MenuManuel = value.MenuManuel;
                //TESTDBKEYOK les groupes pour ex�cution manuelle ne sont plus exploit�s (Avril 2014)
				KeysGroupesPourExecutionManuelle = value.KeysGroupesManuel;
				CodeDeclencheur = value.Code;
				OrdreExecution = value.OrdreExecution;
				HideProgress = value.HideProgress;
                ContextesException = value.ContextesException;
			}

		}
		
		/// /////////////////////////////////////////////////////////
		/// <summary>
		/// Indique si l'�venement doit se d�clencher
		/// </summary>
		/// <param name="objet"></param>
		/// <param name="bModeAvecInterface">
		/// si vrai, les �venements de type Manuel ou Ouverture retournent vrai,
		/// sinon, ils retournent syst�matiquement faux
		/// </param>
		/// <returns></returns>
		public bool ShouldDeclenche ( CObjetDonneeAIdNumerique objet, ref CInfoDeclencheurProcess infoDeclencheur )
		{
			CObjetDonneeAIdNumerique objetATester = objet;
            if (objet is CRelationElementAChamp_ChampCustom)
            {
                objetATester = (CObjetDonneeAIdNumerique)((CRelationElementAChamp_ChampCustom)objetATester).ElementAChamps;
                if (objetATester.Row.RowState == DataRowState.Unchanged)
                    objetATester.Row.Row.SetModified();
            }
			if ( HasDefinisseur() && Row.RowState != DataRowState.Deleted)
                //Stef le 9/6/2011 : ne prend pas en compte les suppression
                //pour les �l�ments � evenements d�finis, �a plante
			{
                try
                {
                    if (Definisseur is CComportementGenerique)
                    {
                        if (!((CComportementGenerique)Definisseur).IsAppliqueAObjet(objetATester))
                            return false;
                    }
                    else
                    {
                        if (!(objetATester is IElementAEvenementsDefinis))
                            return false;
                        if (!((IElementAEvenementsDefinis)objetATester).IsDefiniPar((IDefinisseurEvenements)Definisseur))
                            return false;
                    }
                }
                catch { }
			}

			//si l'objet est nouveau et que c'est un �venement sur cr�ation,
			//il ne doit pas avoir d�j� �t� d�clench�
			//Sinon, les �venements sur cr�ation s'executent en boucle : 
			//l'elt est cr��->d�clenchement de l'action. Le d�clenchement de l'action
			//Entraine une sauvegarde, or pendant cette sauvegarde, l'�lement est
			//toujours nouveau, donc red�clenchement de l'action, .... etc
			if ( objetATester.IsNew() && TypeEvenement == TypeEvenement.Creation )
			{
				if ( DejaDeclenchePourEntite(objetATester) )
					return false;
			}

			if ( DeclenchementUniqueParEntite )
			{
				if ( DejaDeclenchePourEntite ( objetATester ) )
					return false;
			}
			bool bModeAvecInterface = false;
			if ( infoDeclencheur != null )
				bModeAvecInterface = infoDeclencheur.TypeDeclencheur == TypeEvenement.Manuel;

			return ParametreDeclencheur.ShouldDeclenche ( objet, bModeAvecInterface, false,ref infoDeclencheur );
		}

		public bool DejaDeclenchePourEntite ( CObjetDonneeAIdNumerique objet )
		{
			CObjetDonneeAIdNumerique objetATester = objet;
			if ( objet is CRelationElementAChamp_ChampCustom )
				objetATester = (CObjetDonneeAIdNumerique)((CRelationElementAChamp_ChampCustom)objetATester).ElementAChamps;
			
			//V�rifie qu'il n'a pas d�j� �t� d�clench�
			CFiltreData filtre = new CFiltreData ( 
				CProcessEnExecutionInDb.c_champTypeElement+"=@1 and "+
				CProcessEnExecutionInDb.c_champIdElement+"=@2 and "+
				CProcessEnExecutionInDb.c_champUniversalIdEvenementDeclencheur+"=@3 and "+
				CProcessEnExecutionInDb.c_champEtat +"<>@4",
				objetATester.GetType().ToString(),
				objetATester.Id,
				IdUniversel,
				(int)EtatProcess.Erreur );
			CProcessEnExecutionInDb processDejaExistant = new CProcessEnExecutionInDb ( objet.ContexteDonnee );
			if ( processDejaExistant.ReadIfExists ( filtre ) )
				return true;
			return false;
		}

		/// <summary>
		///enregistre l'�venement comme devant se d�clencher
		/// </summary>
		/// <param name="objet"></param>
		/// <returns></returns>
		public CResultAErreur EnregistreDeclenchementEvenement ( CObjetDonneeAIdNumerique objet, CInfoDeclencheurProcess infoDeclencheur )
		{
			if ( TypeEvenement == TypeEvenement.Date )
			{
				return RegisterHandlerDate ( objet );
			}
			return RunEvent( CParametreDeclencheurEvenement.GetObjetToRun(objet), infoDeclencheur, null );
		}

		/// /////////////////////////////////////////////////////////
		//Lance la s�quence d'action attach�e � l'�venement
		public CResultAErreur RunEvent ( CObjetDonneeAIdNumerique objet, 
			CInfoDeclencheurProcess infoDeclencheur, 
			IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;
			CProcess process = this.ProcessADeclencher;
            infoDeclencheur.DbKeyEvenementDeclencheur = DbKey;
			if ( process != null )
			{
				if ( process.TypeCible != null && process.TypeCible == objet.GetType() )
					result = CProcessEnExecutionInDb.StartProcess ( process, 
						infoDeclencheur, 
						new CReferenceObjetDonnee(objet),
						objet.ContexteDonnee.IdSession, 
						objet.ContexteDonnee.IdVersionDeTravail,
						indicateur );
				else
					result = CProcessEnExecutionInDb.StartProcess ( process, 
						infoDeclencheur,
						objet.ContexteDonnee.IdSession, 
						objet.ContexteDonnee.IdVersionDeTravail,
						indicateur );
			}
			return result;
		}

		/// /////////////////////////////////////////////////////////
		//Lance la s�quence d'action attach�e � l'�venement
		public CResultAErreur EnregistreDeclenchementEvenementSurClient(
			CObjetDonneeAIdNumerique objet,
			CInfoDeclencheurProcess infoDeclencheur,
			IIndicateurProgression indicateur)
		{
			CResultAErreur result = CResultAErreur.True;
			CProcess process = this.ProcessADeclencher;
            infoDeclencheur.DbKeyEvenementDeclencheur = DbKey;
			if (process != null)
			{
				if (process.TypeCible != null && process.TypeCible == objet.GetType())
					result = CProcessEnExecutionInDb.StartProcessClient(process,
						objet,
						objet.ContexteDonnee,
						indicateur );
				else
					result = CProcessEnExecutionInDb.StartProcessClient(process,
						null,
						objet.ContexteDonnee,
						indicateur);
			}
			return result;
		}

		/// /////////////////////////////////////////////////////////
		//Lance la s�quence d'action attach�e � l'�venement
		public CResultAErreur RunEventMultiple ( CObjetDonneeAIdNumerique[] objets, CInfoDeclencheurProcess infoDeclencheur, IIndicateurProgression indicateur )
		{
			CResultAErreur result = CResultAErreur.True;
			if ( objets.Length == 0 )
				return result;
			CProcess process = this.ProcessADeclencher;
            infoDeclencheur.DbKeyEvenementDeclencheur = DbKey;
			int nIdSession = -1;
			if ( process != null )
			{
				ArrayList lst = new ArrayList();
				CContexteDonnee contexte = null;
				foreach (CObjetDonneeAIdNumerique objet in objets)
				{
					contexte = objet.ContexteDonnee;
					if (process.TypeCible != null && process.TypeCible == objet.GetType())
					{
						lst.Add(new CReferenceObjetDonnee(objet));
						nIdSession = objet.ContexteDonnee.IdSession;
					}
				}
				if ( lst.Count != 0 )
					result = CProcessEnExecutionInDb.StartProcessMultiples ( 
						process, 
						infoDeclencheur, 
						(CReferenceObjetDonnee[])lst.ToArray ( typeof(CReferenceObjetDonnee) ),
						nIdSession,
						contexte.IdVersionDeTravail,
						indicateur );
			}
			return result;
		}

		/// /////////////////////////////////////////////////////////
		private CResultAErreur RegisterHandlerDate ( CObjetDonneeAIdNumerique objet )
		{
			CResultAErreur result = CResultAErreur.True;
			//Travaille dans un contexte s�par�
			using ( CContexteDonnee contexte = new CContexteDonnee(objet.ContexteDonnee.IdSession, true, false) )
			{
				//Cherche un handler pr�c�dent
				CFiltreData filtre = new CFiltreData ( 
					CHandlerEvenement.c_champIdCible + "=@1 and "+
					CHandlerEvenement.c_champTypeCible+"=@2 and "+
					c_champId+"=@3 and "+
					CHandlerEvenement.c_champTypeEvenement+"=@4",
					CParametreDeclencheurEvenement.GetObjetToRun(objet).Id,
					CParametreDeclencheurEvenement.GetObjetToRun(objet).GetType().ToString(),
					this.Id,
					(int)TypeEvenement.Date);
				
				CHandlerEvenement handler = new CHandlerEvenement ( contexte );
				if ( !handler.ReadIfExists ( filtre ) )
					handler = null;

				//Calcule la date
				object valeur = DateTime.Now;
				if ( ProprieteSurveillee != null )
					valeur = CParametreDeclencheurEvenement.GetValeur ( objet, ProprieteSurveillee, DataRowVersion.Current);
				if ( valeur == null  || (!(valeur is DateTime ) && !(valeur is CDateTimeEx)) || FormuleDateSurveillee == null )
				{
					if ( handler != null )
						handler.Delete();
				}
				else
				{
					CObjetForTestValeurChampCustomDateTime objetEval = new CObjetForTestValeurChampCustomDateTime((DateTime)valeur);
					result = FormuleDateSurveillee.Eval ( new CContexteEvaluationExpression ( objetEval ));

					if ( !result || !(result.Data is DateTime) )
					{
						//ERREUR !!!
						if ( handler != null )
							handler.Delete();
					}
					if ( handler == null )
					{
						handler = new CHandlerEvenement ( contexte );
						handler.CreateNewInCurrentContexte();
					}
					handler.TypeEvenement = TypeEvenement.Date;
					handler.DateHeure = (DateTime)result.Data;
					handler.EvenementLie = this;
					handler.ElementSurveille = CParametreDeclencheurEvenement.GetObjetToRun(objet);
                    handler.EtatExecutionInt = (int)EtatHandlerAction.AExecuter;
				}
				result &= contexte.SaveAll( true );
			}
			return result;
		}

		//-------------------------------------------------------
        /// <summary>
        /// Process ex�cut� lorsque l'�v�nement est d�clench�. Si null, le traitement
        /// est d�fini dans l'�v�nement lui-m�me.
        /// </summary>
		[Relation(CProcessInDb.c_nomTable,
			 CProcessInDb.c_champId,
			 CProcessInDb.c_champId,
			 false,
			 false,
			 false)]
		[DynamicField("Process")]
		public CProcessInDb ProcessInDbAssocie
		{
			get
			{
				return ( CProcessInDb )GetParent ( CProcessInDb.c_champId, typeof(CProcessInDb) );
			}
			set
			{
				SetParent ( CProcessInDb.c_champId, value );
				if (value != null)
					DeclencherSurContexteClient &= value.Process.PeutEtreExecuteSurLePosteClient;
			}
		}

		/// ////////////////////////////////////////////////
		[TableFieldProperty(c_champDataProcessPropre,NullAutorise=true)]
		public CDonneeBinaireInRow DataProcessPropre
		{
			get
			{
				if ( Row[c_champDataProcessPropre] == DBNull.Value )
				{
					CDonneeBinaireInRow donnee = new CDonneeBinaireInRow(ContexteDonnee.IdSession ,Row, c_champDataProcessPropre);
					CContexteDonnee.ChangeRowSansDetectionModification(Row, c_champDataProcessPropre, donnee);
				}
				return ((CDonneeBinaireInRow)Row[c_champDataProcessPropre]).GetSafeForRow(Row.Row);
			}
			set
			{
				Row[c_champDataProcessPropre] = value;
			}
		}

		/// ////////////////////////////////////////////////
		///process propre � l'�venement (si l'�venement n'utilise pas un process std)
        [BlobDecoder]
        public CProcess ProcessPropre
		{
			get
			{
				CProcess process = new CProcess( ContexteDonnee );
				if ( DataProcessPropre.Donnees != null )
				{
					MemoryStream stream = new MemoryStream(DataProcessPropre.Donnees);
					BinaryReader reader = new BinaryReader(stream);
					CSerializerReadBinaire serializer = new CSerializerReadBinaire(reader);
					serializer.AttacheObjet ( typeof(CContexteDonnee), ContexteDonnee );
					CResultAErreur result = process.Serialize(serializer);
					process.TypeCible = TypeCible;
					if ( !result )
					{
						throw new Exception(I.T("Impossible to read agan the action data|266"));
					}
					process.Libelle = I.T("Evt @1|267",Libelle);

                    reader.Close();
                    stream.Close();
				}
				return process;
			}
			set
			{
				if ( value == null )
				{
					CDonneeBinaireInRow data = DataProcessPropre;
					data.Donnees = null;
					DataProcessPropre = data;
				}
				else
				{
					value.TypeCible = TypeCible;
					MemoryStream stream = new MemoryStream();
					BinaryWriter writer = new BinaryWriter(stream);
					CSerializerSaveBinaire serializer = new CSerializerSaveBinaire(writer);
					CResultAErreur result = value.Serialize ( serializer );
					if ( result )
					{
						CDonneeBinaireInRow data = DataProcessPropre;
						data.Donnees = stream.GetBuffer();
						DataProcessPropre = data;
					}
					if (value != null)
						DeclencherSurContexteClient &= value.PeutEtreExecuteSurLePosteClient;

                    writer.Close();
                    stream.Close();
				}
			}
		}

		/// ////////////////////////////////////////////////
		public CProcess ProcessADeclencher
		{
			get
			{
				if ( ProcessInDbAssocie == null )
					return ProcessPropre;
				return ProcessInDbAssocie.Process;
			}
		}

		/// ////////////////////////////////////////////////
		public bool HasDefinisseur()
		{
			return IdDefinisseur >= 0 &&  TypeDefinisseur != null;
		}

		/// ////////////////////////////////////////////////
		/// <summary>
		/// Groupe de param�trage �ventuel auquel appartient cet �v�nement
		/// </summary>
		[Relation ( CGroupeParametrage.c_nomTable, CGroupeParametrage.c_champId,
			 CGroupeParametrage.c_champId,
			 false,
			 false,
			 false)]
		[DynamicField("Parameter setting group")]
		public CGroupeParametrage GroupeParametrage
		{
			get
			{
				return (CGroupeParametrage)GetParent (CGroupeParametrage.c_champId, typeof(CGroupeParametrage));
			}
			set
			{
				SetParent ( CGroupeParametrage.c_champId, value );
			}
		}



        //-----------------------------------------------------------
        /// <summary>
        /// Indique si le type cible de l'Ev�nement est un tableau d'entit� (True) ou une entit� unique (False).
        /// </summary>
        [TableFieldProperty(c_champTableau)]
        [DynamicField("Target Is Array")]
        public bool Tableau
        {
            get
            {
                return (bool)Row[c_champTableau];
            }
            set
            {
                Row[c_champTableau] = value;
            }
        }


        /// /////////////////////////////////////////////////////////////
        public void RechercheObjet(object objetCherche, sc2i.common.recherche.CResultatRequeteRechercheObjet resultat)
        {
            CProcess process = ProcessPropre;
            if (process != null)
            {
                resultat.PushChemin(new CNoeudRechercheObjet_ObjetDonnee(this));
                resultat.PushChemin(new CNoeudCheminResultatRechercheObjetLibelleSimple(I.T("Event action|20027")));
                if (process != null)
                    process.ChercheObjet(objetCherche, resultat);
                resultat.PopChemin();
            }
            else if (ProcessInDbAssocie != null)
            {
                CProcessInDb pr = objetCherche as CProcessInDb;
                if (pr != null && pr.Id == ProcessInDbAssocie.Id)
                    resultat.AddResultat(new CNoeudCheminResultatRechercheObjetLibelleSimple(I.T("Event action|20027")));
            }
            CParametreDeclencheurEvenement parametre = ParametreDeclencheur;
            if ( CTesteurUtilisationObjet.DoesUse ( parametre, objetCherche ) )
                resultat.AddResultat(new CNoeudCheminResultatRechercheObjetLibelleSimple(I.T("Event parameters|20028")));
            resultat.PopChemin();
        }
    }

	/// /////////////////////////////////////////////////////////////
	public interface IDeclencheurEvenementsStatiques
	{
        CResultAErreur DeclencheEvenementStatiques(Type typeObjet, CDbKey sbKeyObjet);
	}

}
